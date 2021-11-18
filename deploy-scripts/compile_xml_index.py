#!/usr/bin/env python3
from __future__ import print_function, annotations

import hashlib
from pathlib import PurePath, Path
from typing import List
import sys


def pad_bytes(data: bytes, alignment: int = 4):
    pad_len = alignment - (len(data) % alignment)
    pad_byte = pad_len.to_bytes(1, "big")
    return data + (pad_byte * pad_len)


class IndexElement:
    def __init__(self, data: bytes, rel_path: PurePath):
        self.data: bytes = data
        self.rel_path: PurePath = rel_path

    def encode(self) -> bytes:
        padded_path = pad_bytes(self.rel_path.as_posix().encode("utf-8"), alignment=4)
        padded_data = pad_bytes(self.data, alignment=4)

        return (
            len(padded_path).to_bytes(4, "big")
            + len(padded_data).to_bytes(4, "big")
            + padded_path
            + padded_data
        )


def compile_index():
    local_mode = sys.argv[1] == "dev"
    root_dir = Path(sys.argv[2])
    out_base_path = PurePath(sys.argv[3])
    index_elems: List[IndexElement] = []

    for glob_pattern in sys.argv[4:]:
        for file_path in filter(Path.is_file, root_dir.glob(glob_pattern)):
            with file_path.open("rb") as f:
                index_elems.append(IndexElement(f.read(), file_path.relative_to(root_dir)))

    encoded = b"".join(map(IndexElement.encode, index_elems))
    m = hashlib.sha1()
    m.update(encoded)

    if local_mode:
        out_rel_path = out_base_path
    else:
        # Compute the actual name of the output file by adding the hash of the file contents.
        hashed_name = out_base_path.stem + "." + m.hexdigest() + out_base_path.suffix
        out_rel_path = out_base_path.parent.joinpath(hashed_name)

    with root_dir.joinpath(out_rel_path).open("wb") as f:
        f.write(encoded)

    # Escape slashes to make the result acceptable for insertion into sed's arguments.
    out_str = out_rel_path.as_posix()
    if not local_mode:
        out_str = out_str.replace("\\", "\\/").replace("/", "\\/")
    print(out_str, end="")  # output without newline


if __name__ == "__main__":
    compile_index()
