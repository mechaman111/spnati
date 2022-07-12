from __future__ import annotations

import subprocess as sp
import sys
import time
from argparse import ArgumentParser, FileType
from itertools import zip_longest
from pathlib import Path
from typing import Dict, Generator, Optional, Tuple, Union

from attrs import define, field
from bs4 import BeautifulSoup, Tag

from extract_sprites import ProgressLine
from kkl_import import KisekaeCharacter, KisekaeCode, KisekaeComponent


def _tag_string(tag: Tag) -> str:
    return "".join(map(str, tag.stripped_strings))


AttributeDict = Dict[str, KisekaeComponent]
AttributeConflicts = Dict[str, Tuple[Optional[KisekaeComponent], Optional[KisekaeComponent], Optional[KisekaeComponent]]]
ImageConflicts = Dict[int, Tuple[Optional[str], Optional[str], Optional[str]]]


@define
class AttributeDiff:
    a: AttributeDict
    b: AttributeDict
    added: AttributeDict = field(factory=dict)
    removed: AttributeDict = field(factory=dict)
    modified: Dict[str, Dict[int, Tuple[Optional[str], Optional[str]]]] = field(factory=dict)
    common: AttributeDict = field(factory=dict)

    @classmethod
    def from_characters(cls, a: AttributeDict, b: AttributeDict) -> AttributeDiff:
        a_copy = dict()
        for k, v in a.items():
            a_copy[k] = KisekaeComponent(v)

        b_copy = dict()
        for k, v in b.items():
            b_copy[k] = KisekaeComponent(v)

        ret = cls(a_copy, b_copy)
        a_prefixes = set(a.keys())
        b_prefixes = set(b.keys())

        for prefix in a_prefixes.symmetric_difference(b_prefixes):
            a_data = a.get(prefix, None)
            b_data = b.get(prefix, None)
            if a_data is not None:
                ret.removed[prefix] = KisekaeComponent(a_data)
            elif b_data is not None:
                ret.added[prefix] = KisekaeComponent(b_data)

        for prefix in a_prefixes.intersection(b_prefixes):
            a_data = a[prefix]
            b_data = b[prefix]
            diff = {}

            for i, (a_attr, b_attr) in enumerate(zip_longest(a_data.attributes, b_data.attributes, fillvalue=None)):
                if a_attr != b_attr:
                    diff[i] = (a_attr, b_attr)

            if len(diff) > 0:
                ret.modified[prefix] = diff
            else:
                ret.common[prefix] = KisekaeComponent(a_data)

        return ret

    def merge(self, common: AttributeDict) -> Tuple[bool, Union[AttributeDict, AttributeConflicts]]:
        ret = dict()
        conflicts: AttributeConflicts = dict()

        for prefix, ab_data in self.common.items():
            ret[prefix] = KisekaeComponent(ab_data)

        for prefix, b_data in self.added.items():
            common_data = common.get(prefix, None)
            if common_data is None:
                ret[prefix] = KisekaeComponent(b_data) # added in B only
            elif common_data == b_data:
                continue # deleted in A only
            else:
                # delete / modify conflict
                conflicts[prefix] = (None, KisekaeComponent(b_data), KisekaeComponent(common_data))
        
        for prefix, a_data in self.removed.items():
            common_data = common.get(prefix, None)
            if common_data is None:
                ret[prefix] = KisekaeComponent(a_data) # added in A only
            elif common_data == a_data:
                continue # deleted in B only
            else:
                # delete / modify conflict
                conflicts[prefix] = (KisekaeComponent(a_data), None, KisekaeComponent(common_data))

        for prefix in self.modified.keys():
            common_data = common.get(prefix, None)
            a_data = self.a[prefix]
            b_data = self.b[prefix]

            if common_data is None:
                # delete / modify conflict
                conflicts[prefix] = (KisekaeComponent(a_data), KisekaeComponent(b_data), None)
                continue

            conflicted = False
            new_data = []
            for a_attr, b_attr, c_attr in zip_longest(a_data.attributes, b_data.attributes, common_data.attributes, fillvalue=None):
                if a_attr == b_attr:
                    new_data.append(a_attr)
                elif a_attr == c_attr:
                    new_data.append(b_attr)
                elif b_attr == c_attr:
                    new_data.append(a_attr)
                else:
                    # modify / modify conflict (or delete/modify conflict)
                    conflicted = True
                    break
            
            if conflicted:
                conflicts[prefix] = (KisekaeComponent(a_data), KisekaeComponent(b_data), KisekaeComponent(common_data))
            else:
                merged_component = KisekaeComponent(a_data)
                merged_component.attributes = new_data
                ret[prefix] = merged_component

        if len(conflicts) > 0:
            return False, conflicts
        else:
            return True, ret


@define
class ImageDiff:
    a: Dict[int, str]
    b: Dict[int, str]
    added: Dict[int, str] = field(factory=dict)
    removed: Dict[int, str] = field(factory=dict)
    modified: Dict[int, Tuple[str, str]] = field(factory=dict)
    common: Dict[int, str] = field(factory=dict)

    @classmethod
    def from_images(cls, a: Dict[int, str], b: Dict[int, str]) -> ImageDiff:
        ret = cls(dict(a), dict(b))

        a_idxs = set(a.keys())
        b_idxs = set(b.keys())

        for idx in a_idxs.symmetric_difference(b_idxs):
            a_data = a.get(idx, None)
            b_data = b.get(idx, None)
            if a_data is not None:
                ret.removed[idx] = a_data
            elif b_data is not None:
                ret.added[idx] = b_data

        for idx in a_idxs.intersection(b_idxs):
            a_data = a[idx]
            b_data = b[idx]
            if a_data != b_data:
                ret.modified[idx] = (a_data, b_data)
            else:
                ret.common[idx] = a_data

        return ret

    def merge(self, common: Dict[str, str]) -> Tuple[bool, Union[Dict[int, str], ImageConflicts]]:
        ret = dict()
        conflicts = dict()

        for prefix, ab_data in self.common.items():
            ret[prefix] = ab_data

        for prefix, b_data in self.added.items():
            common_data = common.get(prefix, None)
            if common_data is None:
                ret[prefix] = b_data # added in B only
            elif common_data == b_data:
                continue # deleted in A only
            else:
                # delete / modify conflict
                conflicts[prefix] = (None, b_data, common_data)
        
        for prefix, a_data in self.removed.items():
            common_data = common.get(prefix, None)
            if common_data is None:
                ret[prefix] = a_data # added in A only
            elif common_data == a_data:
                continue # deleted in B only
            else:
                # delete / modify conflict
                conflicts[prefix] = (a_data, None, common_data)

        for prefix in self.modified.keys():
            common_data = common.get(prefix, None)
            a_data = self.a[prefix]
            b_data = self.b[prefix]

            if a_data == common_data:
                ret[prefix] = b_data
            elif b_data == common_data:
                ret[prefix] = a_data
            else:
                conflicts[prefix] = (a_data, b_data, common_data) # modify/modify or delete/modify conflict

        if len(conflicts) > 0:
            return False, conflicts
        else:
            return True, ret


def merge_characters(common: KisekaeCharacter, a: KisekaeCharacter, b: KisekaeCharacter) -> Tuple[Optional[KisekaeCharacter], Optional[AttributeConflicts], Optional[ImageConflicts]]:
    common_subcodes, common_images = common.subcode_map()
    a_subcodes, a_images = a.subcode_map()
    b_subcodes, b_images = b.subcode_map()

    ab_attr_diff = AttributeDiff.from_characters(a_subcodes, b_subcodes)
    ab_image_diff = ImageDiff.from_images(a_images, b_images)

    attrs_merged, merge_attrs = ab_attr_diff.merge(common_subcodes)
    imgs_merged, merge_imgs = ab_image_diff.merge(common_images)

    if (not attrs_merged) and (not imgs_merged):
        return None, merge_attrs, merge_imgs
    elif (not attrs_merged):
        return None, merge_attrs, None
    elif (not imgs_merged):
        return None, None, merge_imgs

    def _prefix_index(prefix: str, character: KisekaeCharacter) -> int:
        for i, component in enumerate(character):
            if component.prefix == prefix:
                return i
        raise ValueError(prefix)

    def _component_key(component: KisekaeComponent) -> int:
        try:
            return _prefix_index(component.prefix, b)
        except ValueError:
            pass

        try:
            return _prefix_index(component.prefix, a)
        except ValueError:
            pass

        try:
            return _prefix_index(component.prefix, common)
        except ValueError:
            pass

        return -1

    ret_character = KisekaeCharacter()
    ret_character.subcodes = sorted(merge_attrs.values(), key=_component_key)
    for _, img in sorted(merge_imgs.items(), key=lambda kv: int(kv[0])):
        ret_character.images.append(img)
    return ret_character, None, None


def find_pose_tags(soup: BeautifulSoup) -> Generator[Tuple[str, str, Tag, Tag], None, None]:
    sheet: Tag
    for sheet in soup.find_all("sheet"):
        sheet_name = _tag_string(sheet.find("name")).strip()

        stage_tag: Tag
        for stage_tag in sheet.find_all("stage"):
            stage_id = str(stage_tag.attrs["id"]).strip()

            pose_tag: Tag
            for pose_tag in stage_tag.find_all("pose"):
                code_tag: Tag = pose_tag.find("code")
                if code_tag is None:
                    continue

                yield (sheet_name, stage_id, pose_tag, code_tag)

def normalize_character(in_char: KisekaeCharacter) -> KisekaeCharacter:
    for subcode in in_char.subcodes:
        if subcode.prefix == "bc": # 410.500.8.0.1.0
            subcode.attributes = ["400", "500", "8", "0", "1", "0"]
    return in_char

def read_pose_codes(fpath: Path, pose_key: str, from_rev: Optional[str] = None) -> Dict[Tuple[str, str], KisekaeCharacter]:
    progress = ProgressLine()
    progress.update("Loading pose data from {}...", fpath.as_posix())

    if from_rev is not None:
        src = from_rev + ":" + fpath.as_posix()
        proc = sp.run(["git", "cat-file", "blob", src], capture_output=True)
        proc.check_returncode()
        soup = BeautifulSoup(proc.stdout, features="lxml")
    else:
        src = fpath.as_posix()
        with fpath.open("rb") as f:
            soup = BeautifulSoup(f, features="lxml")

    ret = {}
    for sheet_name, stage_id, pose_tag, code_tag in find_pose_tags(soup):
        progress.update("Reading pose data...")
        if pose_tag.attrs["key"] != pose_key:
            continue

        code = _tag_string(code_tag).strip()
        if len(code) == 0:
            continue

        parsed_code = KisekaeCode(code)
        if (parsed_code.scene is not None) or len(parsed_code.characters) != 1:
            progress.warn("Skipped [{}]:{}/{}/{} : appears to be an ALL code", src, sheet_name, stage_id, pose_key)
            continue

        ret[(sheet_name, stage_id)] = normalize_character(parsed_code.characters[0])
        progress.info("Loaded [{}]:{}/{}/{}", src, sheet_name, stage_id, pose_key)

    progress.finish("Read {} poses", len(ret))

    return ret

def find_or_make_pose_tag(soup: BeautifulSoup, sheet_name: str, stage_id: str, pose_key: str) -> Optional[Tuple[Tag, Tag, Tag]]:
    sheet: Tag
    for sheet in soup.find_all("sheet"):
        tag_sheet_name = _tag_string(sheet.find("name")).strip()
        if tag_sheet_name != sheet_name:
            continue

        stage_tag: Tag
        for stage_tag in sheet.find_all("stage"):
            tag_stage_id = str(stage_tag.attrs["id"]).strip()
            if tag_stage_id != stage_id:
                continue

            pose_tag: Tag
            for pose_tag in stage_tag.find_all("pose"):
                if pose_tag.attrs["key"] == pose_key:
                    break
            else:
                pose_tag = soup.new_tag("pose")
                stage_tag.append(pose_tag)
            
            code_tag: Tag = pose_tag.find("code")
            if code_tag is None:
                code_tag = soup.new_tag("code")
                pose_tag.append(code_tag)

            crop_tag: Tag = pose_tag.find("crop")
            if crop_tag is None:
                crop_tag = soup.new_tag("crop")
                crop_tag.attrs["top"] = "0"
                crop_tag.attrs["right"] = "600"
                crop_tag.attrs["bottom"] = "1400"
                crop_tag.attrs["left"] = "0"
                pose_tag.append(crop_tag)

            lastupdate_tag: Tag = pose_tag.find("lastupdate")
            if lastupdate_tag is None:
                lastupdate_tag = soup.new_tag("lastupdate")
                lastupdate_tag.string = str(int(round(time.time() * 1000)))
                pose_tag.append(lastupdate_tag)

            return (pose_tag, code_tag, lastupdate_tag)
    return None


def apply_pose_codes(fpath: Path, pose_key: str, codes: Dict[Tuple[str, str], KisekaeCharacter], common_codes: Dict[Tuple[str, str], KisekaeCharacter]):
    progress = ProgressLine()
    progress.update("Loading pose data from {}...", fpath.as_posix())

    with fpath.open("rb") as f:
        soup = BeautifulSoup(f, features="lxml")
    
    n_merged = 0
    for pose_idx, ((sheet_name, stage_id), new_char) in enumerate(codes.items()):
        progress.update("Merging pose {} / {}...", pose_idx, len(codes))

        tags = find_or_make_pose_tag(soup, sheet_name, stage_id, pose_key)
        if tags is None:
            progress.warn("Skipped [{}]:{}/{}/{} : could not find sheet and/or stage", fpath.as_posix(), sheet_name, stage_id, pose_key)
            continue
        
        _, code_tag, lastupdate_tag = tags

        try:
            common_char = common_codes[(sheet_name, stage_id)]
        except KeyError:
            common_char = None

        prev_code = KisekaeCode()
        prev_code.characters.append(KisekaeCharacter(new_char))

        cur_code = _tag_string(code_tag)
        if len(cur_code) == 0:
            if common_char is not None:
                progress.warn("Skipped [{}]:{}/{}/{} : not deleting code", fpath.as_posix(), sheet_name, stage_id, pose_key)
                continue

            code_tag.string = str(prev_code)
            progress.notice("✓", "Added [{}]:{}/{}/{}", fpath.as_posix(), sheet_name, stage_id, pose_key)
        else:
            if common_char is None:
                if str(prev_code) != cur_code:
                    progress.warn("Skipped [{}]:{}/{}/{} : add/add conflict", fpath.as_posix(), sheet_name, stage_id, pose_key)
                else:
                    progress.warn("Skipped [{}]:{}/{}/{} : no change necessary", fpath.as_posix(), sheet_name, stage_id, pose_key)
                continue
            
            cur_code = KisekaeCode(cur_code)
            if (cur_code.scene is not None) or len(cur_code.characters) != 1:
                progress.warn("Skipped [{}]:{}/{}/{} : appears to be an ALL code", fpath.as_posix(), sheet_name, stage_id, pose_key)
                continue

            merged_code = KisekaeCode()
            cur_char = normalize_character(cur_code.characters[0])
            merged_char, attr_conflicts, img_conflicts = merge_characters(common_char, cur_char, new_char)

            if merged_char is not None:
                merged_code.characters.append(merged_char)
            else:
                s = ""
                if attr_conflicts is not None:
                    s = "attributes " + ", ".join(attr_conflicts.keys())

                if img_conflicts is not None:
                    if len(s) > 0:
                        s += ", and "
                    s += "images " + ", ".join(map(str, img_conflicts.keys()))

                progress.warn("Skipped [{}]:{}/{}/{} : merge conflicts in {}", fpath.as_posix(), sheet_name, stage_id, pose_key, s)
                continue

            code_tag.string = str(merged_code)

            progress.notice("✓", "Merged [{}]:{}/{}/{}", fpath.as_posix(), sheet_name, stage_id, pose_key)

        lastupdate_tag.string = str(int(round(time.time() * 1000)))
        n_merged += 1

    if n_merged > 0:
        progress.update("Writing new pose data...")
        with fpath.open("wb") as f:
            f.write(soup.find("posegrid").encode("utf-8", formatter="minimal"))
    
    progress.finish("Merged {} poses", n_merged)


if __name__ == "__main__":
    parser = ArgumentParser()
    parser.add_argument("--base-rev", type=str, default="HEAD")
    parser.add_argument("pose_key", type=str)
    parser.add_argument("source_path", type=Path)
    parser.add_argument("target_paths", nargs="+")
    args = parser.parse_args()

    source_codes = read_pose_codes(args.source_path, args.pose_key)
    if len(source_codes) == 0:
        print("ERROR: could not read any pose codes to merge", file=sys.stderr)
        sys.exit(1)

    common_codes = read_pose_codes(args.source_path, args.pose_key, from_rev=args.base_rev)
    if len(common_codes) == 0:
        print("ERROR: could not read any common base codes", file=sys.stderr)
        sys.exit(1)

    for target_path in map(Path, args.target_paths):
        apply_pose_codes(target_path, args.pose_key, source_codes, common_codes)
