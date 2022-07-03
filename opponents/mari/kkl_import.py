from __future__ import annotations

"""
Support library for parsing Kisekae codes.
"""

import collections.abc
from itertools import zip_longest
from pathlib import Path
from typing import (IO, Dict, Generator, Iterable, Iterator, List, Optional,
                    Tuple, Union)

from attrs import define, field, validators
from bs4 import BeautifulSoup
from bs4.element import Tag

SETUP_STRING_33 = (
    "33***bc185.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]ua1.0.0.0_ub_uc7.0.30_ud7.0"
)
SETUP_STRING_36 = "36***bc185.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0_ub_u0_v0_uc7.0.30_ud7.0"
SETUP_STRING_40 = "40***bc185.500.0.0.1*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v0_uc7.2.24_ud7.8"
SETUP_STRING_68 = "68***ba50_bb6.0_bc410.500.8.0.1.0_bd6_be180_ad0.0.0.0.0.0.0.0.0.0_ae0.3.3.0.0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_e00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v00_ud7.8_uc7.2.24"
SEPARATOR = "#/]"
IMG_SEPARATOR = "/#]"

CODE_SPLIT_REGEX = r"(\d+?)\*\*\*?([^\#\/\]]+)(?:\#\/\](.+))?"


class KisekaeComponent:
    id: str
    prefix: str
    attributes: List[str]
    index: Optional[int]

    def __init__(self, data: Union[KisekaeComponent, str]):
        """
        Represents a subcomponent of a Kisekae character or scene.
        
        Attributes:
            id (str): An ID identifying this subcomponent's type.
            prefix (str): A prefix identifying this subcomponent.
            attributes (list of str): The attributes associated with this component.
        """

        self.index = None
        if isinstance(data, KisekaeComponent):
            self.id = data.id
            self.prefix = data.prefix
            self.attributes = data.attributes.copy()
            self.index = data.index
        elif isinstance(data, str):
            if data[1].isalpha():
                self.id = data[0:2]  # code is 2 letters
                self.prefix = data[0:2]
            else:
                self.id = data[0]
                self.prefix = data[0:3]  # code is 1 letter + 2 digits
                self.index = int(data[1:3])

            self.attributes = data[len(self.prefix) :].split(".")
        else:
            raise ValueError(
                "`data` must be either str or KisekaeComponent, not "
                + type(data).__name__
            )

    def __eq__(self, other: Union[KisekaeComponent, str]) -> bool:
        if isinstance(other, KisekaeComponent):
            if (
                self.id != other.id
                or self.prefix != other.prefix
                or len(self.attributes) != len(other.attributes)
            ):
                return False

            for x, y in zip(self.attributes, other.attributes):
                if x != y:
                    return False

            return True
        elif isinstance(other, str):
            return str(self) == other
        else:
            raise NotImplementedError(
                "KisekaeComponents can only be compared to other Components or strings."
            )

    def __len__(self) -> int:
        return len(self.attributes)

    def __iter__(self) :
        return self.attributes.__iter__()

    def __getitem__(self, key: int):
        return self.attributes[key]

    def __setitem__(self, key: int, val: str):
        self.attributes[key] = str(val)

    def __delitem__(self, key: int):
        del self.attributes[key]

    def __contains__(self, item: str):
        return self.attributes.__contains__(self, item)

    def __str__(self) -> str:
        return self.prefix + ".".join(self.attributes)


class KisekaeCharacter(object):
    subcodes: List[KisekaeComponent]
    images: List[str]

    def __init__(self, character_data: Union[str, KisekaeCharacter, None]=None):
        """
        Represents a collection of subcodes.
        
        Attributes:
            subcodes (list of KisekaeComponent): The subcodes contained within this object.
            images (list of str): the images contained within this object.
        """

        self.subcodes = []
        self.images = []

        if isinstance(character_data, str):
            parts = character_data.split(IMG_SEPARATOR)
            for subcode in parts[0].split("_"):
                self.subcodes.append(KisekaeComponent(subcode))
            self.images = list(parts[1:])
        elif isinstance(character_data, KisekaeCharacter):
            for subcode in character_data.subcodes:
                self.subcodes.append(KisekaeComponent(subcode))
            self.images = character_data.images.copy()
        elif character_data is not None:
            raise ValueError(
                "`character_data` must be either str or KisekaeCharacter, not "
                + type(character_data).__name__
            )

    def __str__(self) -> str:
        ret = "_".join(str(sc) for sc in self.subcodes)
        if len(self.images) > 0:
            ret += IMG_SEPARATOR
            ret += IMG_SEPARATOR.join(self.images)
        return ret

    def __len__(self) -> int:
        return len(self.subcodes)

    def __iter__(self):
        return self.subcodes.__iter__()

    def __getitem__(self, key: Union[int, str]):
        if isinstance(key, int):
            return self.subcodes[key]
        elif isinstance(key, str):
            v = self.find(key)
            if v is None:
                raise KeyError("No subcode with ID {:s} in this character".format(key))
            return v
        else:
            raise ValueError(
                "Index value must be either an int or a subcode ID string."
            )

    def __setitem__(self, key: Union[int, str], val: KisekaeComponent):
        if not isinstance(val, KisekaeComponent):
            raise ValueError("Assignment value must be a KisekaeComponent.")

        if isinstance(key, int):
            self.subcodes[key] = val
        elif isinstance(key, str):
            idx = None

            for i, sc in enumerate(self.subcodes):
                if sc.prefix == key:
                    idx = i
                    break
            else:
                raise KeyError("No subcode with ID {:s} in this character".format(key))

            self.subcodes[idx] = val

    def __delitem__(self, key: Union[int, str]):
        if isinstance(key, int):
            del self.subcodes[key]
        elif isinstance(key, str):
            idx = None

            for i, sc in enumerate(self.subcodes):
                if sc.prefix == key:
                    idx = i
                    break
            else:
                raise KeyError("No subcode with ID {:s} in this character".format(key))

            del self.subcodes[idx]

    def __contains__(self, item: Union[KisekaeComponent, str]):
        if isinstance(item, KisekaeComponent):
            return self.subcodes.__contains__(self, item)
        elif isinstance(item, str):
            return self.find(item) is not None
        else:
            raise ValueError(
                "Item must be either a KisekaeComponent or a subcode ID string."
            )

    def find(self, subcode_prefix: str):
        """
        Find the first inner KisekaeComponent with the given `subcode_prefix`.
        """

        for sc in self.subcodes:
            if sc.prefix == subcode_prefix:
                return sc

    def iter(self, subcode_prefix: str):
        """
        Iterate over all inner KisekaeComponents with the given `subcode_prefix`
        """

        return filter(lambda sc: sc.prefix.startswith(subcode_prefix), self.subcodes)

    def _subcode_map(self) -> Tuple[Dict[str, KisekaeComponent], Dict[int, str]]:
        img_map: Dict[int, str] = {}
        subcode_map: Dict[str, KisekaeComponent] = {}
        img_numbers: List[int] = []

        for subcode in self.subcodes:
            subcode_map[subcode.prefix] = KisekaeComponent(subcode)
            if subcode.id == "f":
                img_numbers.append(subcode.index)

        for idx, img in zip(sorted(img_numbers), self.images):
            img_map[idx] = img
        
        return subcode_map, img_map

    def overlay(self, other: KisekaeCharacter) -> KisekaeCharacter:
        self_subcodes, self_images = self._subcode_map()
        other_subcodes, other_images = other._subcode_map()

        self_subcodes.update(other_subcodes)
        self_images.update(other_images)

        ret = KisekaeCharacter()
        ret.subcodes = sorted(self_subcodes.values(), key=lambda sc: sc.prefix)
        ret.images = [img for _, img in sorted(self_images.items(), key=lambda pair: pair[0])]
        return ret


class KisekaeCode(object):
    version: int
    characters: List[KisekaeCharacter]
    scene: Optional[KisekaeCharacter]


    def __init__(self, code: Union[KisekaeCode, KisekaeCharacter, str, None] = None, version: int = 105):
        """
        Represents an entire importable Kisekae code, possibly containing
        character data and scene data.
        
        Attributes:
            version (int): The version of Kisekae used to generate this code.
            scene (KisekaeCharacter): Container for scene data and attributes.
            characters (list of KisekaeCharacter): List of characters contained in the code.
            images (list of PurePath): List of image paths contained in the code.
        """

        self.version = version
        self.characters = []
        self.scene = None

        if isinstance(code, KisekaeCode):
            self.version = code.version

            for character in code:
                self.characters.append(KisekaeCharacter(character))

            if code.scene is not None:
                self.scene = KisekaeCharacter(code.scene)

            return
        elif isinstance(code, KisekaeCharacter):
            self.characters.append(KisekaeCharacter(code))
            return
        elif isinstance(code, str):
            version, code = code.split("**", 1)
            self.version = int(version)

            try:
                code, scene_code = code.split(SEPARATOR, 1)
                self.scene = KisekaeCharacter(scene_code)
            except ValueError:
                self.scene = None

            self.characters = []

            if len(code) > 0:
                for character in code.split("*"):
                    if (len(character) == 0) or (character == "0"):
                        continue
                    self.characters.append(KisekaeCharacter(character))
        elif code is not None:
            raise ValueError(
                "`code` must be either a KisekaeCode, KisekaeCharacter, or str, not "
                + type(code).__name__
            )

    def __str__(self):
        ret = str(self.version) + "**"

        if self.scene is not None:
            for i in range(9):
                if i >= len(self.characters):
                    ret += "*0"
                else:
                    ret += "*" + str(self.characters[i])

            ret += SEPARATOR + str(self.scene)
        else:
            ret += str(self.characters[0])

        return ret

    def __len__(self):
        return len(self.characters)

    def __iter__(self):
        return self.characters.__iter__()

    def __getitem__(self, key):
        return self.characters[key]

    def __setitem__(self, key, val):
        self.characters[key] = str(val)

    def __delitem__(self, key):
        del self.characters[key]

    def __contains__(self, item):
        return self.characters.__contains__(self, item)

    @property
    def is_single_character(self) -> bool:
        return (self.scene is None) and (len(self.characters) == 1)

    def overlay(self, other: KisekaeCode) -> KisekaeCode:
        ret = KisekaeCode(None, version=max(self.version, other.version))

        for self_character, other_character in zip_longest(self.characters, other.characters):
            if (self_character is not None) and (other_character is not None):
                ret.characters.append(self_character.overlay(other_character))
            elif self_character is not None:
                ret.characters.append(KisekaeCharacter(self_character))
            elif other_character is not None:
                ret.characters.append(KisekaeCharacter(other_character))

        if (self.scene is not None) and (other.scene is not None):
            ret.scene = self.scene.overlay(other.scene)
        elif self.scene is not None:
            ret.scene = KisekaeCharacter(self.scene)
        elif other.scene is not None:
            ret.scene = KisekaeCharacter(other.scene)

        return ret


def _tag_string(tag: Tag) -> str:
    return "".join(map(str, tag.stripped_strings))


@define()
class MatrixPose:
    key: str
    row: MatrixRow
    cell_code: Optional[KisekaeCode] = None
    crop: Optional[Tuple[int, int, int, int]] = (0 ,0, 600, 1400)
    transparency: Dict[str, float] = field(factory=dict)

    @property
    def sheet(self) -> MatrixSheet:
        return self.row.sheet

    @property
    def matrix(self) -> PoseMatrix:
        return self.row.sheet.matrix

    @property
    def filled(self) -> bool:
        return self.cell_code is not None

    def expand_code(self) -> Optional[KisekaeCode]:
        if self.cell_code is None:
            return None
        
        base = self.row.template_base()
        if base is not None:
            return base.overlay(self.cell_code)
        else:
            return KisekaeCode(self.cell_code)

    def import_str(self) -> str:
        ret = str(self.expand_code())
        if len(self.transparency) > 0:
            extra = "\n".join("{}={}".format(*kv) for kv in self.transparency.items())
            ret = extra + "\n" + ret
        return ret

    @classmethod
    def from_xml(cls, tag: Tag, row: MatrixRow) -> MatrixPose:
        key = tag.attrs["key"]

        if tag.code is not None:
            code = KisekaeCode(_tag_string(tag.code))
        else:
            code = None

        if tag.crop is not None:
            crop_tag = tag.crop
            # Crop bounds are in PIL box order
            crop = (int(crop_tag.attrs["left"]), int(crop_tag.attrs["top"]), int(crop_tag.attrs["right"]), int(crop_tag.attrs["bottom"]))
        else:
            crop = None

        transparency = {}
        if tag.extra is not None:
            extras_str = _tag_string(tag.extra)
            for pair in extras_str.split(","):
                try:
                    transparency_key, alpha = pair.rsplit("=", 1)
                    transparency[transparency_key] = float(alpha)
                except ValueError:
                    continue

        return cls(key, row, code, crop, transparency)


@define()
class MatrixRow(collections.abc.Mapping):
    id: int
    custom_name: Optional[str]
    poses: Dict[str, MatrixPose]
    sheet: MatrixSheet
    clothing: Optional[KisekaeCode] = None

    @property
    def matrix(self) -> PoseMatrix:
        return self.sheet.matrix

    @property
    def name(self) -> str:
        if self.custom_name is not None:
            return self.custom_name
        elif self.sheet.global_sheet:
            if len(self.sheet.rows) == 1:
                return "Global"
            else:
                return str(self.id)
        else:
            try:
                stage_name = self.matrix.stage_names[self.id]
                return str(self.id) + " - " + stage_name
            except IndexError:
                return str(self.id)
        
    def template_base(self) -> Optional[KisekaeCode]:
        if (self.clothing is not None) and (self.sheet.model is not None):
            return self.sheet.model.overlay(self.clothing)
        elif self.clothing is not None:
            return KisekaeCode(self.clothing)
        elif self.sheet.model is not None:
            return KisekaeCode(self.sheet.model)
        else:
            return None

    def __getitem__(self, key: str) -> MatrixPose:
        return self.poses.__getitem__(key)

    def __contains__(self, key: str) -> bool:
        return self.poses.__contains__(key)

    def __iter__(self) -> Iterator[str]:
        return self.poses.__iter__()
    
    def __len__(self) -> int:
        return self.poses.__len__()

    @classmethod
    def from_xml(cls, tag: Tag, sheet: MatrixSheet) -> MatrixRow:
        id = int(tag.attrs["id"])
        name = tag.attrs.get("name")

        if tag.clothing:
            clothing = KisekaeCode(_tag_string(tag.clothing))
        else:
            clothing = None

        ret = cls(id, name, {}, sheet, clothing)
        if tag.poses:
            for pose_tag in tag.poses.find_all("pose", recursive=False):
                pose = MatrixPose.from_xml(pose_tag, ret)
                ret.poses[pose.key] = pose
        return ret


@define()
class MatrixSheet:
    name: str
    rows: Dict[int, MatrixRow]
    global_sheet: bool
    matrix: PoseMatrix
    model: Optional[KisekaeCode] = None

    def pose_keys(self) -> Generator[str, None, None]:
        seen = set()
        for row in self.rows.values():
            for key in row:
                if key not in seen:
                    seen.add(key)
                    yield key

    def iter_columns(self) -> Generator[Tuple[str, List[MatrixPose]], None, None]:
        for key in self.pose_keys():
            col = []
            for row in self.rows.values():
                try:
                    col.append(row[key])
                except KeyError:
                    col.append(MatrixPose(key, row))
            yield (key, col)

    def iter_pose(self, key: str) -> Generator[MatrixPose, None, None]:
        for row in self.rows.values():
            try:
                yield row[key]
            except KeyError:
                pass

    def __contains__(self, key: str) -> bool:
        return self.rows.__contains__(key)

    def __iter__(self) -> Iterator[str]:
        return self.rows.__iter__()
    
    def __len__(self) -> int:
        return self.rows.__len__()

    def __getitem__(self, key: Union[Tuple[int, str], int, str]) -> Union[MatrixRow, List[MatrixPose], MatrixPose]:
        try:
            row_key, pose_key = key
            return self.rows[row_key][pose_key]
        except ValueError:
            pass

        try:
            return self.rows[key]
        except KeyError:
            pass

        ret = list(self.iter_pose(key))
        if len(ret) == 0:
            raise KeyError(key)
        return ret

    @classmethod
    def from_xml(cls, tag: Tag, matrix: PoseMatrix) -> MatrixSheet:
        name = _tag_string(tag.find("name"))
        global_sheet = tag.attrs.get("global", "false").casefold() == "true"

        if tag.model:
            model = KisekaeCode(_tag_string(tag.model))
        else:
            model = None

        ret = cls(name, {}, global_sheet, matrix, model)
        if tag.stages:
            for row_tag in tag.stages.find_all("stage", recursive=False):
                row = MatrixRow.from_xml(row_tag, ret)
                ret.rows[row.id] = row
        return ret

@define()
class PoseMatrix:
    sheets: List[MatrixSheet]
    stage_names: List[str] = field(factory=list, converter=list, validator=validators.deep_iterable(validators.instance_of(str)))

    @classmethod
    def from_xml(cls, tag: Union[Tag, BeautifulSoup], stage_names: Union[Iterable[str], None] = None) -> PoseMatrix:
        if stage_names is None:
            stage_names = []

        ret = cls([], stage_names)
        if tag.sheets is not None:
            for sheet_tag in tag.sheets.find_all("sheet", recursive=False):
                ret.sheets.append(MatrixSheet.from_xml(sheet_tag, ret))
        return ret

    @classmethod
    def load_file(cls, file_or_path: Union[Path, str, IO], stage_names: Union[Iterable[str], None] = None) -> PoseMatrix:
        if isinstance(file_or_path, str):
            file_or_path = Path(file_or_path)

        if isinstance(file_or_path, Path):
            with file_or_path.open("rb") as f:
                soup = BeautifulSoup(f)
        else:
            soup = BeautifulSoup(file_or_path)

        if soup.sheets is None:
            raise ValueError("File does not contain a pose matrix")

        return cls.from_xml(soup, stage_names)

    @staticmethod
    def _load_stage_names(base_path: Path) -> Optional[List[str]]:
        with base_path.open("rb") as f:
            soup = BeautifulSoup(f)
        
        if not soup.wardrobe:
            return None

        clothing_names = []
        for clothing_tag in soup.wardrobe.find_all("clothing", recursive=False):
            name: str = clothing_tag.attrs["name"].strip()
            prettified = " ".join(
                part[0].upper() + part[1:]
                for part in filter(lambda s: len(s) > 0, map(str.strip, name.split()))
            ).strip()

            if len(prettified) == 0:
                prettified = "Clothing " + str(len(clothing_names) + 1)
            clothing_names.append(prettified)

        ret = ["Fully Clothed"]
        for clothing_name in reversed(clothing_names[1:]):
            ret.append("Lost " + clothing_name)
        ret.extend(["Naked", "Masturbating", "Finished"])
        return ret

    @classmethod
    def load_character(cls, char_path: Union[Path, str]) -> PoseMatrix:
        char_path = Path(char_path)

        if char_path.is_file():
            return cls.load_file(char_path)

        if not char_path.joinpath("poses.xml").is_file():
            raise ValueError("{} does not contain a character or costume".format(char_path.as_posix()))

        stage_names = None
        if char_path.joinpath("costume.xml").is_file():
            stage_names = cls._load_stage_names(char_path.joinpath("costume.xml"))
        elif char_path.joinpath("behaviour.xml").is_file():
            stage_names = cls._load_stage_names(char_path.joinpath("behaviour.xml"))
        return cls.load_file(char_path.joinpath("poses.xml"), stage_names)


def _fmt_sheet(sheet: MatrixSheet) -> str:
    cols = []

    stage_col = []
    max_stage_col_len = max(len(name) for name in sheet.matrix.stage_names) + 1
    stage_col.extend([" " * max_stage_col_len, "-" * max_stage_col_len])
    stage_col.extend(name.ljust(max_stage_col_len) for name in sheet.matrix.stage_names)
    cols.append(stage_col)

    for key, col in sheet.iter_columns():
        col_len = len(key) + 2
        filled_str = "!".ljust(col_len // 2).rjust(col_len)
        empty_str = " " * col_len
        text_col = [" " + key + " ", "-" * col_len]

        for cell in col:
            if cell.filled:
                text_col.append(filled_str)
            else:
                text_col.append(empty_str)
        cols.append(text_col)

    rows = [list() for _ in range(max(len(col) for col in cols))]
    for col in cols:
        for row, text in zip(rows, col):
            row.append(text)

    row_texts = ["|".join(row) for row in rows]
    return "\n".join(row_texts)


if __name__ == "__main__":
    import sys

    sheet, stage, pose = sys.argv[2].split(":", 2)
    sheet = int(sheet)
    stage = int(stage)
    matx = PoseMatrix.load_character(sys.argv[1])

    cell = matx.sheets[sheet][stage, pose]
    print(str(cell.expand_code()))

    # with open(sys.argv[1], "w", encoding="utf-8") as f:
    #     f.write(_fmt_sheet(matx.sheets[0]))
