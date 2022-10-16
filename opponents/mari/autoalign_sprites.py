from __future__ import annotations

import argparse
import json
import re
import shutil
from pathlib import Path, PurePath
from typing import Dict, Iterator, List, Optional, TextIO, Tuple, Union

from attrs import define, field
from PIL import Image

BBoxMap = Dict[PurePath, Optional[Tuple[int, int, int, int]]]


def load_bboxes(base_dir: PurePath) -> BBoxMap:
    with Path.cwd().joinpath(base_dir, "bboxes.json").open("r", encoding="utf-8") as f:
        data = json.load(f)

    ret = {}
    for key, val in data.items():
        key = PurePath(key)
        # key = PurePath(key).relative_to(base_dir)
        if val is None:
            ret[key] = None
        else:
            ret[key] = (val["left"], val["top"], val["right"], val["bottom"])
    return ret


class LayerParseError(Exception):
    def __init__(self, parser: LayerParser, msg: str, *args: object):
        self.msg = msg
        self.line = parser.cur_line
        super().__init__(*args)

    def __str__(self) -> str:
        return self.msg + " at line " + str(self.line + 1)


@define()
class LayerParser:
    lines: List[Tuple[int, str]]
    cur_line: int

    @classmethod
    def parse_file(cls, file: TextIO, bboxes: BBoxMap) -> LayerBlock:
        lines = []
        for line in file:
            if len(line.strip()) == 0:
                continue
            line = line.replace("\t", "    ")
            m = re.match(r"(\s*)\-\s*(\S.*?)\s*$", line)
            if m is None:
                continue
            lines.append((len(m[1]), m[2].strip()))
        parser = cls(lines, 0)
        return parser._read_block(bboxes)

    def _peek(self) -> Optional[Tuple[int, str]]:
        try:
            return self.lines[self.cur_line]
        except IndexError:
            return None

    def _advance(self):
        if self.cur_line < len(self.lines):
            self.cur_line += 1

    def _read_block(self, bboxes: BBoxMap) -> LayerBlock:
        block_contents = []
        start_line = self._peek()

        if start_line is None:
            raise LayerParseError(self, "Expected block")

        block_arg = start_line[1]
        assert block_arg.endswith(":")
        block_name = block_arg[:-1]

        self._advance()

        cur_line = self._peek()
        if cur_line is None:
            raise LayerParseError(self, "Expected block contents")

        block_indent = cur_line[0]
        while cur_line is not None:
            indent, cur_arg = cur_line
            cur_arg = cur_arg.strip()

            if indent < block_indent:
                break
            elif indent == block_indent:
                if cur_arg.endswith(":"):
                    child = self._read_block(bboxes)
                else:
                    child = cur_arg
                    self._advance()
                block_contents.append(child)
                cur_line = self._peek()
            else:
                raise LayerParseError(self, "Unexpected indent")
        return LayerBlock(block_name, block_contents, bboxes)


@define()
class EngineSprite:
    base_dir: PurePath
    src: PurePath
    bbox: Tuple[int, int, int, int]
    raw_bbox: Tuple[int, int, int, int]
    block_bbox: Tuple[int, int, int, int]
    container_bbox: Tuple[int, int, int, int]
    bboxes: BBoxMap
    z: int = 0
    parent: Optional[EngineSprite] = None

    id: str = ""
    width: int = 0
    height: int = 0
    x: int = 0
    y: int = 0
    raw_x: int = 0
    raw_y: int = 0

    def __attrs_post_init__(self):
        self.width = self.bbox[2] - self.bbox[0]
        self.height = self.bbox[3] - self.bbox[1]

        self_width = self.raw_bbox[2] - self.raw_bbox[0]
        self_height = self.raw_bbox[3] - self.raw_bbox[1]

        container_width = self.container_bbox[2] - self.container_bbox[0]
        container_height = self.container_bbox[3] - self.container_bbox[1]

        block_cx = (self.block_bbox[2] + self.block_bbox[0]) // 2
        container_cy = (self.container_bbox[3] + self.container_bbox[1]) // 2

        self_cx = (self.raw_bbox[2] + self.raw_bbox[0]) // 2
        self_cy = (self.raw_bbox[3] + self.raw_bbox[1]) // 2

        margin_y = (container_height - self_height) // 2

        # if self.parent is not None:
        #     x_offset = self.raw_bbox[0] - self.block_bbox[0]
        #     margin_x = (block_width - self_width) // 2
        # else:

        # x_offset = self_cx - block_cx
        # margin_x = (block_width - self_width) // 2

        x_offset = self.raw_bbox[0] - self.container_bbox[0]
        margin_x = (container_width - self_width) // 2

        # print(self.src.as_posix(), self.raw_bbox, self.bbox, self.block_bbox, self.container_bbox, (block_cx, block_cy), (self_cx, self_cy), (margin_x, margin_y), (self_width, self_height), (block_width, block_height), (container_width, container_height))

        # offset_bottom = 1400 - 15 - (block_cy - self_cy)
        # offset_top = offset_bottom - self_height

        # self.x = -margin_x + (block_cx - self_cx)
        # self.y = offset_top - margin_y

        # offset_bottom = 1400 - 15 - (self.container_bbox[3] - self.raw_bbox[3])
        offset_bottom = 1400 - 15 - (container_cy - self_cy)
        offset_top = offset_bottom - self_height

        # print(self.src.as_posix(), offset_bottom, offset_top, x_offset, margin_x, container_cx, block_cx, self_cx)
        # print(self.src.as_posix(), container_width, block_width, self_width)

        # self.x = (self.container_bbox[0] - self.raw_bbox[0]) + margin_x
        self.x = -margin_x + x_offset
        self.y = offset_top - margin_y

        self.raw_x = self.x
        self.raw_y = self.y

        if self.parent is not None:
            # print(self.src.as_posix(), self.x, self.parent.x, self.parent.raw_x)
            offset_x2 = (self_cx - block_cx) // 2
            if self.x >= 0:
                self.x += abs(offset_x2)
            self.x -= self.parent.raw_x
            self.y -= self.parent.raw_y

        # if self.parent is not None:
        #     self.x -= self.parent.x
        #     self.y -= self.parent.y

        # self.x = self.bbox[0] - (container_width - block_width)   # (self.bbox[0] + ((block_width - self.width) // 2))
        # self.y = (self.bbox[1] - (block_height - container_height)) // 2 # (self.bbox[1] + ((block_height - self.height) // 2))

        if (self.id != "") and (self.id is not None):
            return

        parts = []
        for part in self.src.parts[:-1]:
            if part.startswith("exported"):
                continue
            parts.append(part)
        parts.append(self.src.stem.replace("_", "-"))
        self.id = "-".join(parts)

    def as_pose_sprite(self, id_prefix: str = "") -> str:
        xml_attrs = [("id", id_prefix + self.id)]

        if self.parent is not None:
            xml_attrs.append(("parent", self.parent.id))

        if self.z != 0:
            xml_attrs.append(("z", self.z))

        xml_attrs.append(("src", self.base_dir.joinpath(self.src).as_posix()))
        xml_attrs.append(("x", self.x))
        xml_attrs.append(("y", self.y))

        ret = "<sprite "
        ret += " ".join(['{}="{}"'.format(k, v) for (k, v) in xml_attrs])
        ret += " />"
        return ret

    def as_epilogue_sprite(self, id_prefix: str = "") -> str:
        xml_attrs = [("type", "sprite"), ("id", id_prefix + self.id)]

        if self.parent is not None:
            xml_attrs.append(("parent", self.parent.id))

        xml_attrs.extend([("width", self.width), ("height", self.height)])

        if self.z != 0:
            xml_attrs.append(("layer", self.z))

        xml_attrs.append(("src", self.base_dir.joinpath(self.src).as_posix()))
        xml_attrs.append(("x", self.x))
        xml_attrs.append(("y", self.y))

        ret = "<directive "
        ret += " ".join(['{}="{}"'.format(k, v) for (k, v) in xml_attrs])
        ret += " />"
        return ret


@define()
class Layer:
    path: PurePath
    raw_bbox: Tuple[int, int, int, int]
    offset: Tuple[int, int, Optional[int]]
    id: Optional[str]

    @classmethod
    def from_spec(cls, spec: str, bboxes: BBoxMap) -> Layer:
        m = re.match(r"\s*(.+?)\s*(?:\[\s*(.+?)\s*\]\s*)?\:?$", spec)
        if m is None:
            name = spec
            offset = (0, 0, None)
            id = None
            bbox_of = None
        else:
            name = m[1]
            offset = [0, 0, None]
            id = None
            bbox_of = None
            if m.group(2) is not None:
                for assign in m.group(2).split(","):
                    key, val = assign.strip().split("=", 1)
                    key = key.casefold().strip()
                    val = val.strip()

                    if key in ("x", "y", "z"):
                        if val[0] == "+":
                            val = val[1:]
                        val = int(val)

                    if key == "x":
                        offset[0] = val
                    elif key == "y":
                        offset[1] = val
                    elif key == "z":
                        offset[2] = val
                    elif key in ("id", "name"):
                        id = val
                    elif key == "bbox":
                        bbox_of = PurePath(val)
            offset = tuple(offset)

        if name.endswith(":"):
            name = name[:-1]

        if bbox_of is None:
            bbox_of = PurePath(name)
        assert bboxes[bbox_of] is not None

        return cls(PurePath(name), bboxes[bbox_of], offset, id)


def _apply_offset(
    bbox: Tuple[int, int, int, int], offset: Tuple[int, int, int]
) -> Tuple[int, int, int, int]:
    ret = list(bbox)
    ret[0] += offset[0]
    ret[2] += offset[0]
    ret[1] += offset[1]
    ret[3] += offset[1]
    return tuple(ret)


@define()
class LayerBlock:
    name: str
    layers: List[Union[Layer, LayerBlock]]
    bboxes: BBoxMap

    offset: Tuple[int, int, Optional[int]] = field(factory=tuple)
    block_bbox: Tuple[int, int, int, int] = field(factory=tuple)

    @property
    def width(self) -> int:
        return self.block_bbox[2] - self.block_bbox[0]

    @property
    def height(self) -> int:
        return self.block_bbox[3] - self.block_bbox[1]

    def __attrs_post_init__(self):
        m = re.match(r"\s*(.+?)\s*(?:\[\s*(.+?)\s*\]\s*)?$", self.name)
        if m is None:
            self.offset = (0, 0, None)
        else:
            self.name = m[1]
            self.offset = [0, 0, None]
            if m.group(2) is not None:
                for m2 in re.finditer(r"([a-zA-Z]+)\s*=\s*([+\-]?\d+)", m[2], re.I):
                    key = m2[1].casefold()
                    val = m2[2]
                    if val[0] == "+":
                        val = val[1:]
                    val = int(val)
                    if key == "x":
                        self.offset[0] = val
                    elif key == "y":
                        self.offset[1] = val
                    elif key == "z":
                        self.offset[2] = val

        cur_bbox = [None, None, None, None]
        layer_z_levels = []

        last_z = -1
        for layer in self.layers:
            if isinstance(layer, str):
                layer = Layer.from_spec(layer, self.bboxes)
            layer_offset = layer.offset

            if layer_offset[2] is not None:
                z = layer_offset[2]
            else:
                z = last_z + 1
            layer_z_levels.append((z, layer))
            last_z = z

        self.layers = sorted(layer_z_levels, key=lambda iv: iv[0])

        for z, layer in self.layers:
            if isinstance(layer, Layer):
                layer_bbox = layer.raw_bbox
            elif isinstance(layer, LayerBlock):
                layer_bbox = layer.block_bbox

            layer_offset = layer.offset
            layer_bbox = _apply_offset(layer_bbox, layer_offset)
            for i in range(4):
                if cur_bbox[i] is None:
                    cur_bbox[i] = layer_bbox[i]

            cur_bbox[0] = min(cur_bbox[0], layer_bbox[0])
            cur_bbox[1] = min(cur_bbox[1], layer_bbox[1])
            cur_bbox[2] = max(cur_bbox[2], layer_bbox[2])
            cur_bbox[3] = max(cur_bbox[3], layer_bbox[3])

        self.block_bbox = tuple(cur_bbox)

    def resolve_layer_offsets(
        self, parent_offset_x: int = 0, parent_offset_y: int = 0
    ) -> Iterator[Tuple[Union[Layer, LayerBlock], int, Tuple[int, int, int, int]]]:
        for z, layer in self.layers:
            if isinstance(layer, Layer):
                layer_bbox = layer.raw_bbox
            elif isinstance(layer, LayerBlock):
                layer_bbox = layer.block_bbox

            layer_bbox = _apply_offset(layer_bbox, layer.offset)
            layer_offset_x = parent_offset_x + (layer_bbox[0] - self.block_bbox[0])
            layer_offset_y = parent_offset_y + (layer_bbox[1] - self.block_bbox[1])
            yield (
                layer,
                z,
                (
                    layer_offset_x,
                    layer_offset_y,
                    layer_offset_x + self.width,
                    layer_offset_y + self.height,
                ),
            )

    def _resolve_flat_engine_sprites(
        self,
        base_dir: PurePath,
        container_bbox: Tuple[int, int, int, int],
        parent_x: int = 0,
        parent_y: int = 0,
        parent_z: int = 0,
    ) -> Iterator[EngineSprite]:
        for layer, offset_z, layer_bbox in self.resolve_layer_offsets(
            parent_x, parent_y
        ):
            layer_z = parent_z + offset_z

            if isinstance(layer, Layer):
                yield EngineSprite(
                    base_dir,
                    layer.path,
                    layer_bbox,
                    layer.raw_bbox,
                    self.block_bbox,
                    container_bbox,
                    self.bboxes,
                    layer_z,
                    None,
                    id=layer.id,
                )
            elif isinstance(layer, LayerBlock):
                yield from layer._resolve_flat_engine_sprites(
                    base_dir, container_bbox, layer_bbox[0], layer_bbox[1], layer_z
                )

    def _resolve_engine_sprites(
        self,
        base_dir: PurePath,
        container_bbox: Tuple[int, int, int, int],
        offset_x: int = 0,
        offset_y: int = 0,
        root_z: int = 0,
        parent: Optional[EngineSprite] = None,
    ) -> Iterator[EngineSprite]:
        layer_info = list(enumerate(self.resolve_layer_offsets(offset_x, offset_y)))

        root_index: int = None
        for i, (layer, z, _) in layer_info:
            if isinstance(layer, Layer) and (z >= 0) and (root_index is None):
                root_index = i

        if root_index is None:
            raise ValueError("No root sprite for group " + self.name)

        root_src, _, root_bbox = layer_info[root_index][1]
        root_sprite = EngineSprite(
            base_dir,
            root_src.path,
            root_bbox,
            _apply_offset(root_src.raw_bbox, root_src.offset),
            self.block_bbox,
            container_bbox,
            self.bboxes,
            root_z,
            parent,
            id=root_src.id,
        )
        yield root_sprite

        for i, (layer, z, layer_bbox) in layer_info:
            if i == root_index:
                continue

            if isinstance(layer, Layer):
                yield EngineSprite(
                    base_dir,
                    layer.path,
                    layer_bbox,
                    _apply_offset(layer.raw_bbox, layer.offset),
                    self.block_bbox,
                    container_bbox,
                    self.bboxes,
                    z,
                    root_sprite,
                    id=layer.id,
                )
            elif isinstance(layer, LayerBlock):
                yield from layer._resolve_engine_sprites(
                    base_dir,
                    container_bbox,
                    layer_bbox[0],
                    layer_bbox[1],
                    z,
                    root_sprite,
                )

    def resolve_engine_sprites(
        self,
        base_dir: PurePath,
        offset_x: int = 0,
        offset_y: int = 0,
        offset_z: int = 0,
        flatten: bool = False,
        single_root: bool = False,
    ) -> Iterator[EngineSprite]:
        if flatten:
            yield from self._resolve_flat_engine_sprites(
                base_dir, self.block_bbox, offset_x, offset_y, offset_z
            )
        elif single_root:
            yield from self._resolve_engine_sprites(
                base_dir, self.block_bbox, offset_x, offset_y, offset_z
            )
        else:
            for layer, layer_z, layer_bbox in self.resolve_layer_offsets(
                offset_x, offset_y
            ):
                layer_z += offset_z

                if isinstance(layer, Layer):
                    yield EngineSprite(
                        base_dir,
                        layer.path,
                        layer_bbox,
                        _apply_offset(layer.raw_bbox, layer.offset),
                        self.block_bbox,
                        self.block_bbox,
                        self.bboxes,
                        layer_z,
                        None,
                        id=layer.id,
                    )
                elif isinstance(layer, LayerBlock):
                    yield from layer._resolve_engine_sprites(
                        base_dir, self.block_bbox, layer_bbox[0], layer_bbox[1], layer_z
                    )

    def resolve_compositing_offsets(
        self, base_dir: Path, offset_x: int = 0, offset_y: int = 0
    ) -> Iterator[Tuple[Path, Tuple[int, int]]]:
        for (
            layer,
            _,
            (layer_x, layer_y, layer_r, layer_b),
        ) in self.resolve_layer_offsets(offset_x, offset_y):
            if isinstance(layer, Layer):
                # print(layer.as_posix(), layer_x, layer_y, layer_r, layer_b, "|", (layer_r + layer_x) // 2, (layer_b + layer_y) // 2)
                yield (base_dir.joinpath(layer.path), (layer_x, layer_y))
            elif isinstance(layer, LayerBlock):
                yield from layer.resolve_compositing_offsets(base_dir, layer_x, layer_y)

    def composite_image(
        self, base_dir: Path, align: bool, margin_y: int
    ) -> Image.Image:
        ret = Image.new("RGBA", (self.width, self.height), (0, 0, 0, 0))

        for path, (x, y) in self.resolve_compositing_offsets(base_dir):
            layer_img = Image.new("RGBA", (self.width, self.height), (0, 0, 0, 0))
            with Image.open(path) as im:
                layer_img.paste(im.convert("RGBA"), (x, y))
            ret = Image.alpha_composite(ret, layer_img)

        if align and (self.height + margin_y) <= 1400:
            aligned = Image.new("RGBA", (self.width + 30, 1400), (0, 0, 0, 0))
            dest_y = 1400 - margin_y - self.height
            aligned.paste(ret, (15, dest_y))
            return aligned
        else:
            return ret


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument(
        "base_dir",
        type=PurePath,
        help="Base path for images, relative to working directory (should contain bboxes.json)",
    )
    parser.add_argument(
        "--layering-spec",
        "-s",
        default="layering.txt",
        help="Filename for layering spec (default: layering.txt)",
    )
    parser.add_argument(
        "--composite-image",
        "-c",
        type=PurePath,
        help="Create composite image from layers (image generated relative to base_dir)",
    )
    parser.add_argument(
        "--no-expand",
        action="store_false",
        dest="expand",
        help="Do not expand poses and composite images to 1400 pixels",
    )
    parser.add_argument(
        "--margin-y",
        type=int,
        default=30,
        help="Margin space to add at bottom of expanded output",
    )
    parser.add_argument(
        "--offset-x",
        "-x",
        type=int,
        default=0,
        help="Offset all sprite positions horizontally",
    )
    parser.add_argument(
        "--offset-y",
        "-y",
        type=int,
        default=0,
        help="Offset all sprite positions vertically",
    )
    parser.add_argument(
        "--offset-z",
        "-z",
        type=int,
        default=0,
        help="Offset all sprite positions along Z-axis (layering)",
    )
    parser.add_argument(
        "--epilogue-xml",
        "-e",
        type=argparse.FileType("w", encoding="utf-8"),
        help="Emit sprite directives for an epilogue to a file (- for stdout)",
    )
    parser.add_argument(
        "--pose-xml",
        "-p",
        type=argparse.FileType("w", encoding="utf-8"),
        help="Emit sprite definitions for a pose (- for stdout)",
    )
    parser.add_argument(
        "--stage-images",
        "-t",
        action="store_true",
        help="Copy referenced images into the base directory",
    )
    parser.add_argument(
        "--single-root",
        action="store_true",
        help="Root the parent hierarchy into a single sprite in emitted XML",
    )
    parser.add_argument(
        "--flatten",
        action="store_true",
        help="Flatten the parent hierarchy in emitted XML",
    )
    parser.add_argument(
        "--id-prefix", default="", help="Apply prefix to all sprite IDs in emitted XML"
    )
    args = parser.parse_args()

    concrete_base = Path.cwd().joinpath(args.base_dir)
    bboxes = load_bboxes(args.base_dir)

    with concrete_base.joinpath(args.layering_spec).open("r", encoding="utf-8") as f:
        layers = LayerParser.parse_file(f, bboxes)

    engine_sprites = list(
        layers.resolve_engine_sprites(
            args.base_dir,
            args.offset_x,
            args.offset_y,
            args.offset_z,
            args.flatten,
            args.single_root,
        )
    )

    if args.stage_images and (
        (args.epilogue_xml is not None) or (args.pose_xml is not None)
    ):
        for sprite in engine_sprites:
            if (sprite.src.parent is not None) and sprite.src.parent.name.startswith(
                "exported"
            ):
                staging_dir = sprite.src.parents[1]
                if staging_dir is not None:
                    dest = staging_dir.joinpath(sprite.src.name)
                    shutil.copyfile(
                        sprite.base_dir.joinpath(sprite.src),
                        sprite.base_dir.joinpath(dest),
                    )
                    sprite.src = dest

    if args.epilogue_xml is not None:
        for sprite in engine_sprites:
            args.epilogue_xml.write(sprite.as_epilogue_sprite(args.id_prefix) + "\n")
        args.epilogue_xml.close()

    if args.pose_xml is not None:
        # pose_align_y = 0
        # if args.expand and ((layers.height + args.margin_y) <= 1400):
        #     pose_align_y = 1400 - args.margin_y - layers.height

        for sprite in engine_sprites:
            # sprite.bbox = _apply_offset(sprite.bbox, (0, pose_align_y))
            args.pose_xml.write(sprite.as_pose_sprite(args.id_prefix) + "\n")
        args.pose_xml.close()

    if args.composite_image is not None:
        out = layers.composite_image(concrete_base, args.expand, args.margin_y)
        out.save(concrete_base.joinpath(args.composite_image), format="png")
