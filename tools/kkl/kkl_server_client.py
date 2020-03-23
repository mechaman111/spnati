import asyncio
import json
import sys
import struct

PROTOCOL_HEADER = b"KKL "

# returns (type, length)
async def wait_for_msg(reader: asyncio.StreamReader) -> (int, int):
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


async def send_msg(writer: asyncio.StreamWriter, payload: bytes):
    payload_len = struct.pack("!L", len(payload))
    writer.write(b"KKL \x01" + payload_len + payload)
    await writer.drain()


async def kkl_client():
    reader, writer = await asyncio.open_connection("127.0.0.1", 8008)

    payload = None
    mode = sys.argv[1]
    outfile = None

    if mode == "import":
        with open(sys.argv[2], "r", encoding="utf-8") as f:
            code = f.read()

        if len(sys.argv) > 3:
            outfile = sys.argv[3]
            payload = json.dumps({"type": "import", "code": code, "id": 1})
        else:
            payload = json.dumps({"type": "import_partial", "code": code, "id": 1})
    elif mode == "reset_full" or mode == "reset_partial" or mode == "version":
        payload = json.dumps({"type": sys.argv[1], "id": 1})
    elif mode == "screenshot" or mode == "screenshot_bg":
        outfile = sys.argv[2]

        o = {"type": "screenshot", "id": 1}
        if mode == "screenshot_bg":
            o["bg"] = True

        payload = json.dumps(o)
    elif mode == "alpha":
        o = {"type": "alpha", "id": 1}

        for arg in sys.argv[2:]:
            try:
                k, v = arg.split("=", 2)

                k = k.lower()
                if k == "colorindex":
                    o["colorIndex"] = int(v)
                elif k == "character":
                    o["character"] = min(9, max(0, int(v)))
                elif k == "part":
                    o["part"] = v
                elif k == "colortab":
                    o["colorTab"] = v
                elif k == "alpha":
                    o["alpha"] = min(255, max(0, int(v)))

            except ValueError:
                pass

        if "colorIndex" in o and "character" in o and "part" in o and "alpha" in o:
            payload = json.dumps(o)
        else:
            print("Required parameters: colorIndex, character, part")
            sys.exit(1)
    elif mode == "peek" or mode == "poke" or mode == "get" or mode == "set":
        o = {"type": "character_data", "id": 1}

        if mode == "poke":
            o["op"] = "set"
        elif mode == "peek":
            o["op"] = "get"
        else:
            o["op"] = mode

        for arg in sys.argv[2:]:
            try:
                k, v = arg.split("=", 2)

                k = k.lower()
                if k == "character":
                    o["character"] = min(9, max(0, int(v)))
                elif k == "value":
                    o["value"] = v
                elif k == "tabname":
                    o["tabName"] = v
                elif k == "tabparameter":
                    o["tabParameter"] = v
                elif k == "internalnames":
                    o["internalNames"] = v.lower() == "true"

            except ValueError:
                pass

        if "character" in o and "tabName" in o and "tabParameter" in o:
            if mode == "poke" and "value" not in o:
                print("Missing required parameter 'value'")
                sys.exit(1)

            payload = json.dumps(o)
        else:
            print("Required parameters: character, tabName, tabParameter")
            sys.exit(1)
    elif mode == "dump_character":
        payload = json.dumps(
            {"type": mode, "id": 1, "character": min(9, max(0, int(sys.argv[2])))}
        )

    payload_bytes = payload.encode("utf-8")
    await send_msg(writer, payload_bytes)

    while True:
        msg_type, msg_len = await wait_for_msg(reader)

        recv_buf = bytearray()
        while len(recv_buf) < msg_len:
            chunk = await reader.read(msg_len - len(recv_buf))
            recv_buf.extend(chunk)

        payload = bytes(recv_buf)

        if msg_type == 0x02:
            payload = payload.decode("utf-8")
            print(payload)

            payload_obj = json.loads(payload)
            status = payload_obj["status"]
            if status == "done" or status == "error":
                break
        elif msg_type == 0x04:
            print("Received heartbeat...")
        elif msg_type == 0x03:
            identifier = struct.unpack("!L", payload[:4])[0]

            img_len = msg_len - 4

            print(
                "received image for identifier {} ({} bytes)".format(
                    identifier, img_len
                )
            )

            with open(outfile, "wb") as f:
                bytes_received = len(payload[4:])
                f.write(payload[4:])

                while not reader.at_eof() and bytes_received < img_len:
                    chunk = await reader.read(128)
                    f.write(chunk)
                    bytes_received += len(chunk)

            break

    writer.close()
    await writer.wait_closed()


if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("USAGE: " + sys.argv[0] + " [mode] [args]")
        print("Modes:")
        print(
            "    "
            + sys.argv[0]
            + " import [code file] [screenshot file] (import a code, export a screenshot)"
        )
        print(
            "    "
            + sys.argv[0]
            + " import_partial [code file]           (import a code into the KKL workspace)"
        )
        print(
            "    "
            + sys.argv[0]
            + " reset_full                           (clear all characters and reset camera/BG)"
        )
        print(
            "    "
            + sys.argv[0]
            + " reset_partial                        (reset camera, background, and prop settings)"
        )
        print(
            "    "
            + sys.argv[0]
            + " screenshot [file]                    (take a screenshot of the KKL workspace)"
        )
        print(
            "    "
            + sys.argv[0]
            + " screenshot_bg [file]                 (take a screenshot of the KKL workspace, with the background)"
        )
        sys.exit(0)

    asyncio.run(kkl_client())
