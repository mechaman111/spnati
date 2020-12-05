import asyncio
from pathlib import Path
import struct
import json
import time
import re
from typing import Optional, Dict, Any, Union, List, Tuple, Iterable

PROTOCOL_HEADER = b"KKL "

MSG_TYPE_CONTROL = 0x01
MSG_TYPE_RESPONSE = 0x02
MSG_TYPE_IMAGE = 0x03
MSG_TYPE_HEARTBEAT = 0x04

MAX_REQUEST_ID: int = 2 ** 32


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
    def character_screenshot(
        cls,
        characters: Union[int, Iterable[int]],
        matrices: Union[None, Iterable[float], Iterable[Iterable[float]]] = None,
        base_scale: Optional[float] = None,
        fast_encode: bool = True,
    ):
        opts = []

        if isinstance(characters, int):
            opt = {"index": characters}
            if matrices is not None:
                opt["matrix"] = list(map(float, matrices))
            opts = [opt]
        elif matrices is None:
            for idx in characters:
                opts.append({"index": idx})
        else:
            for idx, matx in zip(characters, matrices):
                if len(matx) != 6:
                    raise ValueError("matrix must have six elements")

                opts.append({"index": int(idx), "matrix": list(map(float, matx))})

        return cls(
            "character-screenshot",
            {"characters": opts, "scale": base_scale, "fastEncode": fast_encode},
        )

    @classmethod
    def direct_screenshot(
        cls,
        include_bg: bool,
        size: Optional[Tuple[int, int]] = None,
        shift: Optional[Tuple[int, int]] = None,
        sf: Optional[float] = None,
        fast_encode: bool = True,
    ):
        return cls(
            "direct-screenshot",
            {
                "bg": include_bg,
                "shift": shift,
                "size": size,
                "sf": sf,
                "fastEncode": fast_encode,
            },
        )

    @classmethod
    def reset_full(cls):
        return cls("reset_full")

    @classmethod
    def reset_partial(cls):
        return cls("reset_partial")

    @classmethod
    def get_character_data(
        cls,
        character: int,
        tab_name: str,
        tab_parameter: int,
        internal_names: bool = False,
    ):
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
                "internalNames": internal_names,
            },
        )

    @classmethod
    def set_character_data(
        cls,
        character: int,
        tab_name: str,
        tab_parameter: Union[int, str],
        value: Union[int, bool, str],
        internalNames: bool = False,
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
                "internalNames": internalNames,
            },
        )

    @classmethod
    def fastload(
        cls,
        character: int,
        data: List[Tuple[str, int, Union[str, int, bool]]],
        attachments: Optional[Dict[int, str]] = None,
        version: Optional[int] = None,
        write_to_cache: bool = True,
        read_from_cache: bool = True,
    ):
        if character < 0 or character > 8:
            raise ValueError(
                "'character' must be between 0-8 (got {})".format(character)
            )

        return cls(
            "fastload",
            {
                "data": data,
                "attachments": attachments,
                "character": character,
                "version": version,
                "write_to_cache": write_to_cache,
                "read_from_cache": read_from_cache,
            },
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
            "alpha_direct", {"op": "get", "character": character, "path": path,},
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


class ConnectionError(Exception):
    pass


class KisekaeLocalClient(object):
    def __init__(
        self,
        reader: asyncio.StreamReader,
        writer: asyncio.StreamWriter,
        loop: Optional[asyncio.AbstractEventLoop] = None,
    ):
        self.next_request_id: int = 0
        self.reader: asyncio.StreamReader = reader
        self.writer: asyncio.StreamWriter = writer
        self.last_heartbeat: Optional[float] = None
        self.pending_requests: Dict[int, asyncio.Future] = {}

        self.loop: asyncio.AbstractEventLoop = loop
        if loop is None:
            self.loop = asyncio.get_event_loop()

    def get_request_id(self) -> int:
        self.next_request_id = (self.next_request_id + 1) % MAX_REQUEST_ID
        return self.next_request_id

    @classmethod
    async def connect(
        cls, max_tries: int = -1, loop: Optional[asyncio.AbstractEventLoop] = None
    ):
        cur_tries = 0

        while (max_tries == -1) or (cur_tries < max_tries):
            try:
                reader, writer = await asyncio.open_connection(
                    "127.0.0.1", 8008, loop=loop
                )
            except OSError:
                await asyncio.sleep(5)
                cur_tries += 1
                continue

            return cls(reader, writer)

        raise ConnectionError("could not connect to KKL")

    async def run(self):
        while True:
            msg = await read_msg(self.reader)

            if msg.get_type() == MSG_TYPE_HEARTBEAT:
                self.last_heartbeat = time.monotonic()
                continue

            request_id: int = msg.get_id()
            try:
                fut: asyncio.Future = self.pending_requests[request_id]
                if msg.is_complete():
                    fut.set_result(msg)
            except KeyError:
                sys.stderr.write(
                    "Received response for unknown request {}".format(request_id)
                )
                sys.stderr.flush()

    async def send_command(
        self, command: KisekaeServerRequest
    ) -> KisekaeServerResponse:
        req_id: int = self.get_request_id()
        fut: asyncio.Future = self.loop.create_future()
        self.pending_requests[req_id] = fut

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


async def send_msg(writer: asyncio.StreamWriter, payload: bytes):
    payload_len = struct.pack("!L", len(payload))
    writer.write(b"KKL \x01" + payload_len + payload)
    await writer.drain()


async def send_command(writer: asyncio.StreamWriter, cmd_obj: KisekaeServerRequest):
    return await send_msg(writer, cmd_obj.encode())


async def _read_msg_header(reader: asyncio.StreamReader) -> (int, int):
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
    msg_type, msg_len = await _read_msg_header(reader)

    recv_buf = bytearray()
    while len(recv_buf) < msg_len:
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


if __name__ == "__main__":
    import argparse
    import sys

    async def do_reset(client: KisekaeLocalClient, args: argparse.Namespace):
        if args.command == "reset-full":
            return await client.send_command(KisekaeServerRequest.reset_full())
        elif args.command == "reset-partial":
            return await client.send_command(KisekaeServerRequest.reset_partial())

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

    async def do_direct_screenshot(
        client: KisekaeLocalClient, args: argparse.Namespace
    ):
        resp = await client.send_command(
            KisekaeServerRequest.direct_screenshot(
                args.bg,
                size=args.size,
                shift=args.shift,
                sf=args.sf,
                fast_encode=args.fast_encode,
            )
        )

        if resp.is_success():
            outfile: Path = args.outfile
            with outfile.open("wb") as f:
                sys.stderr.write(
                    "Received {} bytes of image data.\n".format(len(resp.get_data()))
                )
                sys.stderr.flush()

                f.write(resp.get_data())

        return resp

    async def do_character_screenshot(
        client: KisekaeLocalClient, args: argparse.Namespace
    ):
        resp = await client.send_command(
            KisekaeServerRequest.character_screenshot(
                args.characters, None, base_scale=args.sf, fast_encode=args.fast_encode
            )
        )

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

    async def do_get_character_data(
        client: KisekaeLocalClient, args: argparse.Namespace
    ):
        request = KisekaeServerRequest.get_character_data(
            args.character,
            args.tab_name,
            args.tab_parameter,
            internal_names=args.internal_names,
        )

        resp = await client.send_command(request)
        if resp.is_success():
            print(repr(resp.get_data()))

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

        try:
            param: int = int(args.tab_parameter)
        except ValueError:
            param: str = args.tab_parameter.strip()

        request = KisekaeServerRequest.set_character_data(
            args.character, args.tab_name, param, value, args.internalNames
        )

        return await client.send_command(request)

    async def do_fastload(client: KisekaeLocalClient, args: argparse.Namespace):
        data = []
        for i in range(0, len(args.data), 3):
            data.append((args.data[i], args.data[i + 1], args.data[i + 2]))

        request = KisekaeServerRequest.fastload(
            args.character,
            data,
            write_to_cache=args.write_to_cache,
            read_from_cache=args.read_from_cache,
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
    subparsers = parser.add_subparsers(dest="command")

    parser_version = subparsers.add_parser("version")
    parser_version.set_defaults(func=do_version)

    parser_reset = subparsers.add_parser("reset-full", aliases=["reset-partial"])
    parser_reset.set_defaults(func=do_reset)

    parser_screenshot = subparsers.add_parser("screenshot")
    parser_screenshot.add_argument(
        "outfile", type=Path, help="Path for output screenshot image"
    )
    parser_screenshot.add_argument(
        "--bg", action="store_true", help="Include the background in the screenshot"
    )
    parser_screenshot.set_defaults(func=do_screenshot)

    def int_pair(arg: str) -> Tuple[int, int]:
        if arg is None or len(arg) == 0:
            return None
        m = re.match(r"(\d+)\s*\D\s*(\d+)", arg)
        if m is None:
            raise ValueError("Not an integer pair (must be e.g. 1234x5678 or similar)")
        return (int(m[1]), int(m[2]))

    parser_direct_screenshot = subparsers.add_parser("direct-screenshot")
    parser_direct_screenshot.add_argument(
        "outfile", type=Path, help="Path for output screenshot image"
    )
    parser_direct_screenshot.add_argument(
        "--bg", action="store_true", help="Include the background in the screenshot"
    )
    parser_direct_screenshot.add_argument(
        "--size", type=int_pair, default=None, help="Size of screenshot"
    )
    parser_direct_screenshot.add_argument(
        "--shift", type=int_pair, default=None, help="Shift for screenshot",
    )
    parser_direct_screenshot.add_argument(
        "--sf",
        type=float,
        default=None,
        help="Base scale factor for screenshot (establishes resolution)",
    )
    parser_direct_screenshot.add_argument(
        "--slow-encode",
        action="store_false",
        help="Use slower compression (results in smaller images)",
        dest="fast_encode",
    )
    parser_direct_screenshot.set_defaults(func=do_direct_screenshot)

    parser_character_screenshot = subparsers.add_parser("character-screenshot")
    parser_character_screenshot.add_argument(
        "outfile", type=Path, help="Path for output screenshot image"
    )
    parser_character_screenshot.add_argument(
        "characters", type=int, nargs="+", help="Characters to capture"
    )
    parser_character_screenshot.add_argument(
        "--sf",
        type=float,
        default=None,
        help="Base scale factor for screenshot (establishes resolution)",
    )
    parser_character_screenshot.add_argument(
        "--slow-encode",
        action="store_false",
        help="Use slower compression (results in smaller images)",
        dest="fast_encode",
    )
    parser_character_screenshot.set_defaults(func=do_character_screenshot)

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

    parser_get_character_data = subparsers.add_parser("get-character-data")
    parser_get_character_data.add_argument(
        "character", type=int, help="Index of character to inspect (0-8)"
    )
    parser_get_character_data.add_argument(
        "tab_name", help="Save code prefix of character data to inspect"
    )
    parser_get_character_data.add_argument(
        "tab_parameter",
        help="Zero-based index into save code or parameter name for character data to inspect",
    )
    parser_get_character_data.add_argument("--internal-names", action="store_true")
    parser_get_character_data.set_defaults(func=do_get_character_data)

    parser_set_character_data = subparsers.add_parser("set-character-data")
    parser_set_character_data.add_argument(
        "character", type=int, help="Index of character to modify (0-8)"
    )
    parser_set_character_data.add_argument(
        "tab_name", help="Save code prefix of character data to modify"
    )
    parser_set_character_data.add_argument(
        "tab_parameter",
        help="Zero-based index into save code for character data to modify",
    )
    parser_set_character_data.add_argument("value", help="Value to set")
    parser_set_character_data.add_argument(
        "--internal-names",
        action="store_true",
        help="Use internal Kisekae data path names",
        dest="internalNames",
    )
    parser_set_character_data.set_defaults(func=do_set_character_data)

    parser_fastload = subparsers.add_parser("mod-character-data")
    parser_fastload.add_argument(
        "character", type=int, help="Index of character to modify (0-8)"
    )
    parser_fastload.add_argument(
        "data",
        help="Tab names, indices, and values to inject into character data",
        nargs="+",
    )
    parser_fastload.add_argument(
        "--read-from-cache",
        "-r",
        action="store_true",
        help="Read from cache when loading (default: don't)",
    )
    parser_fastload.add_argument(
        "--no-write-cache",
        "-W",
        action="store_false",
        dest="write_to_cache",
        help="Don't write to cache when loading (default: do)",
    )
    parser_fastload.set_defaults(func=do_fastload)

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
        sys.stderr.write("Connecting to KKL...")
        sys.stderr.flush()

        client = await KisekaeLocalClient.connect(5)

        sys.stderr.write("connected.\n")
        sys.stderr.flush()

        asyncio.create_task(client.run())

        resp: KisekaeServerResponse = await args.func(client, args)
        if resp.is_success():
            return 0
        else:
            print("Command returned error status: {}".format(resp.get_reason()))
            return 1

    args = parser.parse_args()
    if args.command is None:
        parser.print_help()
        sys.exit(0)

    retval = asyncio.run(main(args))
    sys.exit(retval)
