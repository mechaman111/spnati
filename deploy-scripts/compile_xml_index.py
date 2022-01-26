#!/usr/bin/env python3
from __future__ import print_function, annotations

from argparse import ArgumentParser
import hashlib
from pathlib import PurePath, Path
from typing import List


def pad_bytes(data: bytes, alignment: int = 4):
    pad_len = alignment - (len(data) % alignment)
    pad_byte = pad_len.to_bytes(1, "big")
    return data + (pad_byte * pad_len)


class IndexElement:
    def __init__(self, data: bytes, rel_path: PurePath):
        self.data: bytes = data
        self.rel_path: PurePath = rel_path

    def encode(self) -> bytes:
        padded_path = pad_bytes(self.rel_path.as_posix().casefold().encode("utf-8"), alignment=4)
        padded_data = pad_bytes(self.data, alignment=4)

        return (
            len(padded_path).to_bytes(4, "big")
            + len(padded_data).to_bytes(4, "big")
            + padded_path
            + padded_data
        )


def compile_index(production: bool, root_dir: Path, out_base_path: PurePath, glob_patterns: List[str]):
    root_dir = Path(root_dir)
    out_base_path = PurePath(out_base_path)
    index_elems: List[IndexElement] = []

    for glob_pattern in glob_patterns:
        for file_path in filter(Path.is_file, root_dir.glob(glob_pattern)):
            with file_path.open("rb") as f:
                index_elems.append(IndexElement(f.read(), file_path.relative_to(root_dir)))

    encoded = b"".join(map(IndexElement.encode, index_elems))
    m = hashlib.sha1()
    m.update(encoded)

    if production:
        # Compute the actual name of the output file by adding the hash of the file contents.
        hashed_name = out_base_path.stem + "." + m.hexdigest() + out_base_path.suffix
        out_rel_path = out_base_path.parent.joinpath(hashed_name)
    else:
        out_rel_path = out_base_path

    with root_dir.joinpath(out_rel_path).open("wb") as f:
        f.write(encoded)

    # Escape slashes to make the result acceptable for insertion into sed's arguments.
    out_str = out_rel_path.as_posix()
    if production:
        out_str = out_str.replace("\\", "\\/").replace("/", "\\/")
    print(out_str, end="")  # output without newline


if __name__ == "__main__":
    parser = ArgumentParser(description="Combines data files into a packed index file. The path to the generated file will be written to stdout.")
    parser.add_argument("--production", action="store_true", help="Activates production mode. In this mode, the generated file will have an SHA-1 hash appended to ease caching, and the path output by this program will be escaped for easy use with sed patterns.")
    parser.add_argument("root", type=Path, help="The root directory to search for files to pack.")
    parser.add_argument("out", type=PurePath, help="Path to the output index file. In production mode, the SHA-1 hash of the generated file will be attached before the extension suffix.")
    parser.add_argument("globs", nargs="+", help="Glob patterns for files to include in the output index file. These are treated as relative to the root directory.")
    args = parser.parse_args()

    compile_index(args.production, args.root, args.out, args.globs)
