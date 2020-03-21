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

        if len(sys.argv) >= 3:
            outfile = sys.argv[3]
            payload = json.dumps({"type": "import", "code": code, "id": 1})
        else:
            payload = json.dumps({"type": "import_partial", "code": code, "id": 1})
    elif mode == "reset_full" or mode == "reset_partial":
        payload = json.dumps({"type": sys.argv[1], "id": 1})
    elif mode == "screenshot" or mode == "screenshot_bg":
        outfile = sys.argv[2]

        o = {"type": "screenshot", "id": 1}
        if mode == "screenshot_bg":
            o["bg"] = True

        payload = json.dumps(o)

    payload_bytes = payload.encode("utf-8")
    await send_msg(writer, payload_bytes)

    while True:
        msg_type, msg_len = await wait_for_msg(reader)
        payload = await reader.read(msg_len)

        if msg_type == 0x02:
            payload = payload.decode("utf-8")
            print(payload)

            payload_obj = json.loads(payload)
            if payload_obj["status"] == "done":
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
