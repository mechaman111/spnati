from __future__ import annotations

import asyncio
from pathlib import Path
import struct
import json
import time
from typing import (
    ClassVar,
    List,
    Optional,
    Dict,
    Any,
    Union,
    Type,
    Tuple,
    AsyncIterator,
)
import sys
from contextlib import asynccontextmanager
import logging

LOGGER = logging.getLogger("kkl_client")

PROTOCOL_HEADER = b"KKL "

MSG_TYPE_CONTROL = 0x01
MSG_TYPE_RESPONSE = 0x02
MSG_TYPE_IMAGE = 0x03
MSG_TYPE_HEARTBEAT = 0x04
MSG_TYPE_EVENT = 0x05

MAX_REQUEST_ID: int = 2 ** 32


class ConnectionError(Exception):
    pass


class ConnectionClosedError(Exception):
    pass


class UnknownEventError(Exception):
    def __init__(self, event_type: str, *args):
        super().__init__(self, event_type, *args)
        self.event_type = event_type

    def __str__(self) -> str:
        return "Unknown event type '{}'".format(self.event_type)


class KisekaeServerMessage(object):
    def get_type(self) -> int:
        pass


class KisekaeServerResponse(KisekaeServerMessage):
    def get_data(self) -> Optional[object]:
        pass

    def get_id(self) -> int:
        return 0

    def is_success(self) -> bool:
        pass

    def is_complete(self) -> bool:
        pass


class KisekaeImageResponse(KisekaeServerResponse):
    def __init__(self, request_id: int, data: bytes):
        self.request_id = request_id
        self.data = data

    def get_type(self) -> int:
        return MSG_TYPE_IMAGE

    def get_id(self) -> int:
        return self.request_id

    def get_data(self) -> bytes:
        return self.data

    def get_status(self) -> str:
        return "done"

    def is_success(self) -> bool:
        return True

    def is_complete(self) -> bool:
        return True


class KisekaeJSONResponse(KisekaeServerResponse):
    def __init__(self, resp_obj: dict):
        self.resp_obj = resp_obj
        self.request_id: int = resp_obj.get("id", 0)
        self.data: Any = resp_obj.get("data", None)

    def get_type(self) -> int:
        return MSG_TYPE_RESPONSE

    def get_id(self) -> int:
        return self.request_id

    def get_status(self) -> str:
        return self.resp_obj["status"]

    def is_complete(self) -> bool:
        return self.resp_obj["status"] != "in_progress"

    def is_success(self) -> bool:
        return self.resp_obj["status"] == "done"

    def get_reason(self) -> str:
        return self.resp_obj.get("reason", None)

    def get_data(self) -> Any:
        return self.data

    def get_response_obj(self) -> dict:
        return self.resp_obj


class KisekaeServerHeartbeat(KisekaeServerMessage):
    def __init__(self):
        pass

    def get_type(self) -> int:
        return MSG_TYPE_HEARTBEAT

    def get_data(self) -> None:
        return None

    def get_id(self) -> None:
        return None


class SubcodeData:
    def __init__(self, data: dict):
        self.property: str = data["property"]
        self.code_group: int = data["group"]
        self.prefix: str = data["prefix"]
        self.subcode_index: int = data["index"]
        self.value: Any = data["value"]

    def __str__(self) -> str:
        return "{}[{}] ({}) = {}".format(
            self.prefix, self.subcode_index, self.property, json.dumps(self.value)
        )


class KisekaeServerEvent(KisekaeServerMessage):
    _EVENT_MAP: ClassVar[Dict[str, Type[KisekaeServerEvent]]] = {}
    EVENT_TYPE: ClassVar[str] = ""

    def __init_subclass__(cls, event_type: str, **kwargs) -> None:
        super().__init_subclass__(**kwargs)
        cls.EVENT_TYPE = event_type
        if event_type in KisekaeServerEvent._EVENT_MAP:
            raise ValueError(
                "duplicate event type definition for '{}'".format(event_type)
            )
        KisekaeServerEvent._EVENT_MAP[event_type] = cls

    def __init__(self, msg_obj: dict):
        self.event_type = msg_obj["type"]
        self.data = msg_obj["data"]

    def get_type(self) -> int:
        return MSG_TYPE_EVENT

    def get_event_type(self) -> str:
        return self.event_type

    def get_data(self) -> Any:
        return self.data

    @staticmethod
    def deserialize_event(msg_obj: dict) -> KisekaeServerEvent:
        event_type = msg_obj["type"]
        try:
            type_obj = KisekaeServerEvent._EVENT_MAP[event_type]
            return type_obj(msg_obj)
        except KeyError:
            raise UnknownEventError(event_type) from None


class KisekaeMenuClickEvent(KisekaeServerEvent, event_type="menu_click"):
    def __init__(self, msg_obj: dict):
        super().__init__(msg_obj)
        self.tab: str = self.data["tab"]
        self.header: str = self.data["header"]
        self.target_j: int = self.data["targetJ"]
        self.pre_data: List[Dict[str, Any]] = self.data["pre_data"]
        self.post_data: List[Dict[str, Any]] = self.data["post_data"]

    def __str__(self) -> str:
        return "KisekaeMenuClickEvent(tab={}, header={}, targetJ={})".format(
            self.tab, self.header, self.target_j
        )


class KisekaeServerShutdownEvent(KisekaeServerEvent, event_type="shutdown"):
    def __init__(self, msg_obj: dict):
        super().__init__(msg_obj)

    def __str__(self) -> str:
        return "KisekaeServerShutdownEvent()"


class KisekaeDataSetEvent(KisekaeServerEvent, event_type="data_set"):
    def __init__(self, msg_obj: dict):
        super().__init__(msg_obj)
        self.tab: str = self.data["tab"]
        self.character: int = self.data["character"]
        self.raw_data: Dict[str, Any] = self.data["raw"]
        self.subcode_changes: List[SubcodeData] = list(
            map(SubcodeData, self.data["code_data"])
        )

    def __str__(self) -> str:
        return "KisekaeDataSetEvent(character={}, tab={}, {} subcode changes)".format(
            self.character, self.tab, len(self.subcode_changes)
        )


class KisekaeAutosaveEvent(KisekaeServerEvent, event_type="autosave"):
    def __init__(self, msg_obj: dict):
        super().__init__(msg_obj)
        self.save_path: Path = Path(self.data["path"])
        self.code: str = self.data["code"]

    def __str__(self) -> str:
        return "KisekaeAutosaveEvent(save_path={}, code=<{} characters>)".format(
            str(self.save_path), len(self.code)
        )


class KisekaeKeyUpEvent(KisekaeServerEvent, event_type="key_up"):
    def __init__(self, msg_obj: dict):
        super().__init__(msg_obj)
        self.char_code: int = self.data["char_code"]
        self.key_code: int = self.data["key_code"]
        self.location: int = self.data["location"]
        self.ctrl: bool = self.data["ctrl"]
        self.alt: bool = self.data["alt"]
        self.shift: bool = self.data["shift"]

    def __str__(self) -> str:
        return "KisekaeKeyUpEvent(char_code={}, key_code={}, location={}, ctrl={}, alt={}, shift={})".format(
            self.char_code,
            self.key_code,
            self.location,
            self.ctrl,
            self.alt,
            self.shift,
        )


class KisekaeKeyDownEvent(KisekaeServerEvent, event_type="key_down"):
    def __init__(self, msg_obj: dict):
        super().__init__(msg_obj)
        self.char_code: int = self.data["char_code"]
        self.key_code: int = self.data["key_code"]
        self.location: int = self.data["location"]
        self.ctrl: bool = self.data["ctrl"]
        self.alt: bool = self.data["alt"]
        self.shift: bool = self.data["shift"]

    def __str__(self) -> str:
        return "KisekaeKeyDownEvent(char_code={}, key_code={}, location={}, ctrl={}, alt={}, shift={})".format(
            self.char_code,
            self.key_code,
            self.location,
            self.ctrl,
            self.alt,
            self.shift,
        )


class KisekaeServerRequest(object):
    def __init__(self, req_type: str, request_args: Optional[Dict[str, Any]] = None):
        self.request_type: str = req_type
        self.request_args: Dict[str, Any] = {}

        if request_args is not None:
            self.request_args = request_args

    def encode(self, request_id: int = 0) -> bytes:
        out_dict = {"type": self.request_type, "id": request_id}
        for key, value in self.request_args.items():
            out_dict[key] = value

        return json.dumps(out_dict).encode("utf-8")

    @classmethod
    def version(cls):
        return cls("version")

    @classmethod
    def import_full(cls, code: str):
        return cls("import", {"code": code})

    @classmethod
    def import_partial(cls, code: str):
        return cls("import_partial", {"code": code})

    @classmethod
    def screenshot(cls, include_bg: bool):
        return cls("screenshot", {"bg": include_bg})

    @classmethod
    def reset_full(cls):
        return cls("reset_full")

    @classmethod
    def reset_partial(cls):
        return cls("reset_partial")

    @classmethod
    def do_autosave(cls):
        return cls("autosave")

    @classmethod
    def export_code(cls, character: Union[int, str, None] = None):
        if isinstance(character, int):
            if (character < 0) or (character > 8):
                raise IndexError(
                    "character index {} is out of bounds".format(
                        character
                    )
                )
        elif character is not None and character != "current":
            raise ValueError(
                "'character' must be None, 'current', or int between 0-8 (got {})".format(
                    character
                )
            )

        return cls(
            "export",
            {"character": character},
        )

    @classmethod
    def get_character_data(cls, character: int, tab_name: str, tab_parameter: int):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "character_data",
            {
                "op": "get",
                "character": character,
                "tabName": tab_name,
                "tabParameter": tab_parameter,
            },
        )

    @classmethod
    def get_graphics_data(cls, character: int, path: Optional[str]):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "graphics_data",
            {
                "character": character,
                "path": path,
            },
        )

    @classmethod
    def set_character_data(
        cls,
        character: int,
        tab_name: str,
        tab_parameter: int,
        value: Union[int, bool, str],
    ):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "character_data",
            {
                "op": "set",
                "character": character,
                "tabName": tab_name,
                "tabParameter": tab_parameter,
                "value": value,
            },
        )

    @classmethod
    def adjust_transforms(cls, transforms: Dict[str, Dict[str, Dict[str, float]]]):
        return cls(
            "adjust_transforms",
            {"transforms": transforms},
        )


    @classmethod
    def set_alpha(cls, character: int, part: str, color_index: int, alpha_value: int):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "alpha",
            {
                "character": character,
                "part": part,
                "colorIndex": color_index,
                "alpha": alpha_value,
            },
        )

    @classmethod
    def set_alpha_direct(
        cls, character: int, path: str, alpha_value: int, alpha_multiplier: float = 0
    ):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "alpha_direct",
            {
                "op": "set",
                "character": character,
                "path": path,
                "alpha": alpha_value,
                "multiplier": alpha_multiplier,
            },
        )

    @classmethod
    def set_children_alpha_direct(
        cls, character: int, path: str, alpha_value: int, alpha_multiplier: float = 0
    ):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "alpha_direct",
            {
                "op": "set-children",
                "character": character,
                "path": path,
                "alpha": alpha_value,
                "multiplier": alpha_multiplier,
            },
        )

    @classmethod
    def get_alpha_direct(cls, character: int, path: str):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "alpha_direct",
            {
                "op": "get",
                "character": character,
                "path": path,
            },
        )

    @classmethod
    def reset_alpha_direct(cls, character: int, path: str):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "alpha_direct", {"op": "reset", "character": character, "path": path}
        )

    @classmethod
    def reset_all_alpha_direct(cls):
        return cls("alpha_direct", {"op": "reset_all"})


async def send_msg(writer: asyncio.StreamWriter, payload: bytes):
    payload_len = struct.pack("!L", len(payload))
    writer.write(b"KKL \x01" + payload_len + payload)
    await writer.drain()


async def send_command(writer: asyncio.StreamWriter, cmd_obj: KisekaeServerRequest):
    return await send_msg(writer, cmd_obj.encode())


async def _read_msg_header(reader: asyncio.StreamReader) -> Tuple[int, int]:
    i = 0
    while True:
        next_byte = await reader.readexactly(1)

        if next_byte[0] != PROTOCOL_HEADER[i]:
            i = 0
            continue
        else:
            i += 1
            if i >= len(PROTOCOL_HEADER):
                break

    header_data = await reader.readexactly(5)
    return struct.unpack("!BL", header_data)


async def read_msg(reader: asyncio.StreamReader) -> KisekaeServerMessage:
    try:
        msg_type, msg_len = await _read_msg_header(reader)
    except asyncio.IncompleteReadError:
        raise ConnectionClosedError() from None

    recv_buf = bytearray()
    while len(recv_buf) < msg_len:
        if reader.at_eof():
            raise ConnectionClosedError()
        chunk = await reader.read(msg_len - len(recv_buf))
        recv_buf.extend(chunk)

    if msg_type == MSG_TYPE_HEARTBEAT:
        return KisekaeServerHeartbeat()
    elif msg_type == MSG_TYPE_RESPONSE:
        payload = recv_buf.decode("utf-8")
        return KisekaeJSONResponse(json.loads(payload))
    elif msg_type == MSG_TYPE_IMAGE:
        identifier = struct.unpack("!L", recv_buf[:4])[0]
        return KisekaeImageResponse(identifier, bytes(recv_buf[4:]))
    elif msg_type == MSG_TYPE_EVENT:
        payload = recv_buf.decode("utf-8")
        return KisekaeServerEvent.deserialize_event(json.loads(payload))


class KisekaeEventListener(object):
    def __init__(self, client: KisekaeLocalClient):
        self.client: KisekaeLocalClient = client
        self.id: Optional[int] = None
        self.recv_queue: asyncio.Queue = asyncio.Queue()
        self.recv_task: Optional[asyncio.Task] = None
        self.closed: bool = True

    def n_pending(self) -> int:
        return self.recv_queue.qsize()

    def get_nowait(self) -> KisekaeServerEvent:
        return self.recv_queue.get_nowait()

    async def get(self) -> KisekaeServerEvent:
        try:
            return self.recv_queue.get_nowait()
        except asyncio.QueueEmpty:
            pass

        if self.closed:
            raise ConnectionClosedError()

        self.recv_task = asyncio.create_task(self.recv_queue.get())
        try:
            return await self.recv_task
        finally:
            self.recv_task = None

    def close(self):
        if self.recv_task is not None:
            self.recv_task.cancel()
            self.recv_task = None
        self.client.unregister_listener(self)
        self.id = None
        self.closed = True

    def __enter__(self) -> KisekaeEventListener:
        self.id = self.client.register_listener(self)
        return self

    def __exit__(self, exc_type, exc_value, tb):
        self.close()

    def __aiter__(self) -> KisekaeEventListener:
        return self

    async def __anext__(self) -> KisekaeServerEvent:
        if self.id is None:
            self.id = self.client.register_listener(self)

        try:
            return await self.get()
        except (asyncio.CancelledError, ConnectionClosedError):
            raise StopAsyncIteration from None


class KisekaeLocalClient(object):
    def __init__(
        self,
        reader: asyncio.StreamReader,
        writer: asyncio.StreamWriter,
        loop: Optional[asyncio.AbstractEventLoop] = None,
    ):
        self.next_request_id: int = 0
        self.next_listener_id: int = 0
        self.reader: asyncio.StreamReader = reader
        self.writer: asyncio.StreamWriter = writer
        self.last_heartbeat: Optional[float] = None
        self.pending_requests: Dict[int, asyncio.Future] = {}
        self.listeners: Dict[int, KisekaeEventListener] = {}
        self._run_task: asyncio.Task = None
        self._shutdown = False

        self.loop: asyncio.AbstractEventLoop = loop
        if loop is None:
            self.loop = asyncio.get_event_loop()

    @property
    def closed(self) -> bool:
        return self.writer.is_closing()

    def get_request_id(self) -> int:
        self.next_request_id = (self.next_request_id + 1) % MAX_REQUEST_ID
        return self.next_request_id

    def register_listener(self, listener: KisekaeEventListener) -> int:
        self.next_listener_id = self.next_listener_id + 1
        self.listeners[self.next_listener_id] = listener
        listener.closed = False
        return self.next_listener_id

    def unregister_listener(self, listener: KisekaeEventListener):
        try:
            del self.listeners[listener.id]
        except KeyError:
            pass

    def event_listener(self) -> KisekaeEventListener:
        return KisekaeEventListener(self)

    @classmethod
    async def connect(
        cls,
        max_tries: int = -1,
        *,
        loop: Optional[asyncio.AbstractEventLoop] = None,
        host: str = "127.0.0.1",
        port: int = 8008
    ) -> KisekaeLocalClient:
        LOGGER.info("Connecting to KKL at %s:%d...", host, port)

        cur_tries = 0
        while (max_tries == -1) or (cur_tries < max_tries):
            try:
                reader, writer = await asyncio.open_connection(host, port)
            except OSError:
                cur_tries += 1
                if max_tries == -1:
                    LOGGER.warning(
                        "Connection failed, retrying (attempt %d)...", cur_tries
                    )
                else:
                    LOGGER.warning(
                        "Connection failed, retrying (attempt %d/%d)...",
                        cur_tries,
                        max_tries,
                    )

                await asyncio.sleep(5)
                continue

            LOGGER.info("Connected to KKL.")
            return cls(reader, writer)

        LOGGER.error("Could not connect to KKL.")
        raise ConnectionError("could not connect to KKL")

    async def run(self):
        while True:
            try:
                msg = await read_msg(self.reader)
            except UnknownEventError as e:
                LOGGER.error(
                    "Received message for unknown event type '%s'.", e.event_type
                )
                continue

            if msg.get_type() == MSG_TYPE_HEARTBEAT:
                LOGGER.debug("Received server heartbeat.")
                self.last_heartbeat = time.monotonic()
            elif msg.get_type() == MSG_TYPE_EVENT:
                LOGGER.debug("Received %s event.", msg.get_event_type())
                for listener in self.listeners.values():
                    listener.recv_queue.put_nowait(msg)

                if isinstance(msg, KisekaeServerShutdownEvent):
                    LOGGER.info("Received KKL shutdown event.")
                    return await self.close()
            else:
                request_id: int = msg.get_id()
                try:
                    fut: asyncio.Future = self.pending_requests[request_id]
                    if msg.is_complete():
                        fut.set_result(msg)
                    LOGGER.debug("Received response for request %d.", request_id)
                except KeyError:
                    LOGGER.error(
                        "Received response for unknown request %d.", request_id
                    )

    async def send_command(
        self, command: KisekaeServerRequest
    ) -> KisekaeServerResponse:
        req_id: int = self.get_request_id()
        fut: asyncio.Future = self.loop.create_future()
        self.pending_requests[req_id] = fut

        LOGGER.debug("Sending request %d (%s)..", req_id, command.request_type)

        payload = command.encode(req_id)
        payload_len = struct.pack("!L", len(payload))
        self.writer.write(b"KKL \x01" + payload_len + payload)
        await self.writer.drain()

        if not fut.done():
            # Wait for it to come back:
            resp_msg: KisekaeServerResponse = await fut
        else:
            # The server's response came in immediately after the drain call
            # (happens with especially lightweight requests sometimes)
            resp_msg: KisekaeServerResponse = fut.result()

        del self.pending_requests[req_id]
        return resp_msg

    async def close(self):
        if self._shutdown:
            return
        self._shutdown = True

        LOGGER.info("Closing client connection...")

        try:
            for fut in self.pending_requests.values():
                fut.cancel()
            self.pending_requests = {}

            if self._run_task is not None:
                self._run_task.cancel()
                self._run_task = None

            for listener in self.listeners.values():
                listener.close()
        finally:
            self.writer.close()
            await self.writer.wait_closed()

    def _start_run_task(self):
        self._run_task = asyncio.create_task(self.run())

    async def __aenter__(self) -> KisekaeLocalClient:
        self._start_run_task()
        return self

    async def __aexit__(self):
        await self.close()


@asynccontextmanager
async def connect(
    max_tries: int = -1,
    loop: Optional[asyncio.AbstractEventLoop] = None,
    host: str = "127.0.0.1",
    port: int = 8008,
) -> AsyncIterator[KisekaeLocalClient]:
    conn = await KisekaeLocalClient.connect(
        max_tries=max_tries, loop=loop, host=host, port=port
    )
    conn._start_run_task()

    try:
        yield conn
    finally:
        await conn.close()


if __name__ == "__main__":
    import argparse
    import sys

    async def do_reset(client: KisekaeLocalClient, args: argparse.Namespace):
        if args.command == "reset-full":
            return await client.send_command(KisekaeServerRequest.reset_full())
        elif args.command == "reset-partial":
            return await client.send_command(KisekaeServerRequest.reset_partial())

    async def do_autosave(client: KisekaeLocalClient, args: argparse.Namespace):
        return await client.send_command(KisekaeServerRequest.do_autosave())

    async def do_version(client: KisekaeLocalClient, args: argparse.Namespace):
        resp = await client.send_command(KisekaeServerRequest.version())

        if resp.is_success():
            data = resp.get_data()
            print("major: {}".format(data["major"]))
            print("minor: {}".format(data["minor"]))

        return resp

    async def do_screenshot(client: KisekaeLocalClient, args: argparse.Namespace):
        resp = await client.send_command(KisekaeServerRequest.screenshot(args.bg))

        if resp.is_success():
            outfile: Path = args.outfile
            with outfile.open("wb") as f:
                sys.stderr.write(
                    "Received {} bytes of image data.\n".format(len(resp.get_data()))
                )
                sys.stderr.flush()

                f.write(resp.get_data())

        return resp

    async def do_full_import(client: KisekaeLocalClient, args: argparse.Namespace):
        codefile: Path = args.codefile
        with codefile.open("r", encoding="utf-8") as f:
            code: str = f.read()

        resp = await client.send_command(KisekaeServerRequest.import_full(code))

        if resp.is_success():
            outfile: Path = args.outfile
            with outfile.open("wb") as f:
                data = resp.get_data()
                sys.stderr.write("Received {} bytes of image data.\n".format(len(data)))
                sys.stderr.flush()

                f.write(data)

        return resp

    async def do_partial_import(client: KisekaeLocalClient, args: argparse.Namespace):
        codefile: Path = args.codefile
        with codefile.open("r", encoding="utf-8") as f:
            code: str = f.read()

        resp = await client.send_command(KisekaeServerRequest.import_partial(code))
        return resp

    async def do_export_code(client: KisekaeLocalClient, args: argparse.Namespace):
        try:
            char_arg = int(args.character)
        except (TypeError, ValueError):
            char_arg = args.character

        request = KisekaeServerRequest.export_code(char_arg)

        resp = await client.send_command(request)
        if resp.is_success():
            data = resp.get_data()
            if data["character"] is None:
                print("ALL: " + data["code"])
            else:
                print("Character {}: {}".format(data["character"], data["code"]))

        return resp

    async def do_get_character_data(
        client: KisekaeLocalClient, args: argparse.Namespace
    ):
        request = KisekaeServerRequest.get_character_data(
            args.character, args.tab_name, args.tab_parameter
        )

        resp = await client.send_command(request)
        if resp.is_success():
            print(resp.get_data())

        return resp

    async def do_get_graphics_data(
        client: KisekaeLocalClient, args: argparse.Namespace
    ):
        request = KisekaeServerRequest.get_graphics_data(args.character, args.path)

        resp = await client.send_command(request)
        if resp.is_success():
            json.dump(resp.get_data(), sys.stdout)

        return resp

    async def do_set_character_data(
        client: KisekaeLocalClient, args: argparse.Namespace
    ):
        raw_value: str = args.value

        # attempt to convert value into either an int or a bool
        # if both conversions fail, just leave it as a string
        if raw_value.lower() == "true":
            value: bool = True
        elif raw_value.lower() == "false":
            value: bool = False
        else:
            try:
                value: int = int(args.value)
            except ValueError:
                value: str = raw_value.strip()

        request = KisekaeServerRequest.set_character_data(
            args.character, args.tab_name, args.tab_parameter, value
        )

        return await client.send_command(request)

    async def do_set_alpha(client: KisekaeLocalClient, args: argparse.Namespace):
        request = KisekaeServerRequest.set_alpha(
            args.character, args.part, args.color_index, args.alpha_value
        )

        return await client.send_command(request)

    async def do_set_alpha_direct(client: KisekaeLocalClient, args: argparse.Namespace):
        request = KisekaeServerRequest.set_alpha_direct(
            args.character, args.path, args.alpha_value, args.multiplier
        )

        return await client.send_command(request)

    async def do_get_alpha_direct(client: KisekaeLocalClient, args: argparse.Namespace):
        request = KisekaeServerRequest.get_alpha_direct(args.character, args.path)

        resp = await client.send_command(request)
        if resp.is_success():
            data = resp.get_data()
            print("Multiplier: {:.3f}".format(data["multiplier"]))
            print("Alpha value: {:.3f}".format(data["alpha"]))
            print("Alpha property: {:.3f}".format(data["alphaProp"]))
            print("Visible: {}".format(data["visible"]))

        return resp

    async def do_reset_alpha_direct(
        client: KisekaeLocalClient, args: argparse.Namespace
    ):
        return await client.send_command(
            KisekaeServerRequest.reset_alpha_direct(args.character, args.path)
        )

    async def do_reset_all_alpha_direct(
        client: KisekaeLocalClient, args: argparse.Namespace
    ):
        return await client.send_command(KisekaeServerRequest.reset_all_alpha_direct())

    parser = argparse.ArgumentParser(
        description="Sends commands to a local KKL instance."
    )
    parser.add_argument(
        "--verbose",
        "-v",
        action="count",
        default=0,
        dest="log_level",
        help="Verbosity (once for info messages, twice for debug messages)",
    )

    subparsers = parser.add_subparsers(dest="command")

    parser_version = subparsers.add_parser("version")
    parser_version.set_defaults(func=do_version)

    parser_reset = subparsers.add_parser("reset-full", aliases=["reset-partial"])
    parser_reset.set_defaults(func=do_reset)

    parser_autosave = subparsers.add_parser("autosave")
    parser_autosave.set_defaults(func=do_autosave)

    parser_screenshot = subparsers.add_parser("screenshot")
    parser_screenshot.add_argument(
        "outfile", type=Path, help="Path for output screenshot image"
    )
    parser_screenshot.add_argument(
        "--bg", action="store_true", help="Include the background in the screenshot"
    )
    parser_screenshot.set_defaults(func=do_screenshot)

    parser_import_partial = subparsers.add_parser("import-partial")
    parser_import_partial.add_argument(
        "codefile", type=Path, help="Path to Kisekae code to import"
    )
    parser_import_partial.set_defaults(func=do_partial_import)

    parser_import_full = subparsers.add_parser("import")
    parser_import_full.add_argument(
        "codefile", type=Path, help="Path to Kisekae code to import"
    )
    parser_import_full.add_argument(
        "outfile", type=Path, help="Path for output screenshot image"
    )
    parser_import_full.set_defaults(func=do_full_import)

    parser_export_code = subparsers.add_parser("export")
    parser_export_code.add_argument(
        "--character",
        help="Character to export, either an index (0-8 inclusive) or 'current' (default: export ALL code)",
        default=None,
    )
    parser_export_code.set_defaults(func=do_export_code)

    parser_get_character_data = subparsers.add_parser("get-character-data")
    parser_get_character_data.add_argument(
        "character", type=int, help="Index of character to inspect (0-8)"
    )
    parser_get_character_data.add_argument(
        "tab_name", help="Save code prefix of character data to inspect"
    )
    parser_get_character_data.add_argument(
        "tab_parameter",
        type=int,
        help="Zero-based index into save code for character data to inspect",
    )
    parser_get_character_data.set_defaults(func=do_get_character_data)

    parser_get_graphics_data = subparsers.add_parser("get-graphics-data")
    parser_get_graphics_data.add_argument(
        "character", type=int, help="Index of character to inspect (0-8)"
    )
    parser_get_graphics_data.add_argument(
        "path", nargs="?", default=None, help="Sprite path to export"
    )
    parser_get_graphics_data.set_defaults(func=do_get_graphics_data)

    parser_set_character_data = subparsers.add_parser("set-character-data")
    parser_set_character_data.add_argument(
        "character", type=int, help="Index of character to modify (0-8)"
    )
    parser_set_character_data.add_argument(
        "tab_name", help="Save code prefix of character data to modify"
    )
    parser_set_character_data.add_argument(
        "tab_parameter",
        type=int,
        help="Zero-based index into save code for character data to modify",
    )
    parser_set_character_data.add_argument("value", help="Value to set")
    parser_set_character_data.set_defaults(func=do_set_character_data)

    parser_set_alpha = subparsers.add_parser("set-alpha")
    parser_set_alpha.add_argument(
        "character", type=int, help="Index of character to modify (0-8)"
    )
    parser_set_alpha.add_argument("part", help="Internal part name to modify")
    parser_set_alpha.add_argument("color_index", type=int, help="Color index to modify")
    parser_set_alpha.add_argument(
        "alpha_value", type=int, help="Alpha value to set (0-255)"
    )
    parser_set_alpha.set_defaults(func=do_set_alpha)

    parser_set_alpha_direct = subparsers.add_parser("set-alpha-direct")
    parser_set_alpha_direct.add_argument(
        "character", type=int, help="Index of character to modify (0-8)"
    )
    parser_set_alpha_direct.add_argument("path", help="Internal sprite path to modify")
    parser_set_alpha_direct.add_argument(
        "alpha_value", type=int, help="Alpha value to set (0-255)"
    )
    parser_set_alpha_direct.add_argument(
        "--multiplier", type=float, default=0, help="Alpha multiplier to set (0-1)"
    )
    parser_set_alpha_direct.set_defaults(func=do_set_alpha_direct)

    parser_get_alpha_direct = subparsers.add_parser("get-alpha-direct")
    parser_get_alpha_direct.add_argument(
        "character", type=int, help="Index of character to inspect (0-8)"
    )
    parser_get_alpha_direct.add_argument("path", help="Internal sprite path to inspect")
    parser_get_alpha_direct.set_defaults(func=do_get_alpha_direct)

    parser_reset_alpha_direct = subparsers.add_parser("reset-alpha-direct")
    parser_reset_alpha_direct.add_argument(
        "character", type=int, help="Index of character to modify (0-8)"
    )
    parser_reset_alpha_direct.add_argument("path", help="Internal sprite path to reset")
    parser_reset_alpha_direct.set_defaults(func=do_reset_alpha_direct)

    parser_reset_all_alpha_direct = subparsers.add_parser("reset-all-alpha-direct")
    parser_reset_all_alpha_direct.set_defaults(func=do_reset_all_alpha_direct)

    async def main(args: argparse.Namespace):
        async with connect(5) as client:
            resp: KisekaeServerResponse = await args.func(client, args)
            if resp.is_success():
                return 0
            else:
                print(
                    "Command returned error status: {}".format(resp.get_reason()),
                    file=sys.stderr,
                )
                return 1

    args = parser.parse_args()
    if args.command is None:
        parser.print_help()
        sys.exit(0)

    log_level = logging.WARNING
    if args.log_level == 1:
        log_level = logging.INFO
    elif args.log_level >= 2:
        log_level = logging.DEBUG
    logging.basicConfig(
        format="%(levelname):s [%(name)s]: %(message)s", level=log_level
    )

    retval = asyncio.run(main(args))
    sys.exit(retval)
