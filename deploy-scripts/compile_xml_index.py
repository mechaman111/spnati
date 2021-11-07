#!/usr/bin/env python3
from __future__ import print_function

from bs4 import BeautifulSoup
from bs4.element import Tag
import hashlib
from pathlib import PurePath, Path
import sys


def compile_index():
    index_key = sys.argv[1]
    root_dir = Path(sys.argv[2])
    index_soup = BeautifulSoup(
        "<?xml version='1.0' encoding='UTF-8'?><index></index>", features="html.parser"
    )
    index_elem = index_soup.find("index")

    # This should be applicable for collectibles, costumes, and (hopefully) anything else
    # of that nature that we add in the future.
    for xml_path in filter(Path.is_file, root_dir.glob(sys.argv[3])):
        with xml_path.open("rb") as f:
            soup = BeautifulSoup(f.read(), features="html.parser")

        for elem in filter(
            lambda e: isinstance(e, Tag) and len(e.contents) > 0, soup.children
        ):
            if index_key == "subfolder":
                elem["id"] = xml_path.parts[-2]
            elif index_key == "path":
                elem["id"] = str(xml_path.relative_to(root_dir).parent.as_posix())
            index_elem.append(elem)

    index_contents = index_soup.encode(encoding="utf-8")

    m = hashlib.sha1()
    m.update(index_contents)

    # Compute the actual name of the output file by adding the hash of the file contents.
    out_base_path = PurePath(sys.argv[4])
    hashed_name = out_base_path.stem + "." + m.hexdigest() + out_base_path.suffix
    out_rel_path = out_base_path.parent.joinpath(hashed_name)

    with root_dir.joinpath(out_rel_path).open("wb") as f:
        f.write(index_contents)

    # Escape slashes to make the result acceptable for insertion into sed's arguments.
    out_str = out_rel_path.as_posix()
    out_str.replace("\\", "\\/").replace("/", "\\/")

    print(out_str, end="")  # output without newline


if __name__ == "__main__":
    compile_index()
