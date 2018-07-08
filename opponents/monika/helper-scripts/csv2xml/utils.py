import os
import os.path as osp
from pathlib import Path
import time

from . import behaviour_parser as bp
from .xml_format import xml_to_lineset
from .opponent import Opponent

VERSION = '0.18.0-alpha'  # will attempt to follow semver if possible
COMMENT_TIME_FORMAT = 'at %X %Z on %A, %B %d, %Y'  # strftime format
WARNING_COMMENT = 'This file was machine generated using csv2xml.py {:s} {:s}. Please do not edit it directly without preserving your improvements elsewhere or your changes may be lost the next time this file is generated.'
IMAGE_FORMATS = ["png", "jpg", "jpeg", "gif", "gifv"]

__image_directory = Path(os.getcwd())
__default_opponents_dir = Path(os.getcwd())
__xml_cache = {}


def config_default_opponents_dir(d):
    global __default_opponents_dir
    __default_opponents_dir = Path(d)


def find_opponents_directory(search_from = None):
    """
    Attempt to find SPNATI's opponents/ directory.
    """
    global __default_opponents_dir
    
    def is_valid_opponent_directory(p):
        if p is None:
            return False
        elif isinstance(p, str):
            p = Path(p)
            
        listing_path = p.joinpath('listing.xml')
        return listing_path.exists() and listing_path.is_file()
    
    if is_valid_opponent_directory(__default_opponents_dir):
        return __default_opponents_dir
    
    if search_from is not None:
        search_dir = Path(search_from).resolve()
    else:
        search_dir = Path(os.getcwd())
    
    if is_valid_opponent_directory(search_dir):
        return search_dir
    else:
        for p in search_dir.parents:
            if is_valid_opponent_directory(p):
                return p
    
    return None


def get_target_xml(target, opponents_dir=None):
    global __xml_cache
    
    opponents_dir = find_opponents_directory(opponents_dir)
    if opponents_dir is None:
        raise ValueError("Cannot find SPNATI opponents directory!")

    if target not in __xml_cache:
        target_xml_path = opponents_dir / target / 'behaviour.xml'
        __xml_cache[target] = bp.parse_file(str(target_xml_path))

    return __xml_cache[target]
    

def get_target_gender(target_id, opponents_dir=None):
    target_elem = get_target_xml(target_id, opponents_dir)
    return target_elem.find('gender').text.strip().lower()


def get_target_stripping_case(target, stage, opponents_dir=None):
    target_elem = get_target_xml(target, opponents_dir)
    clothing_elems = [e for e in target_elem.find('wardrobe').iter('clothing')]
    clothing_elems.reverse()

    if stage < 0 or stage >= len(clothing_elems):
        raise IndexError("Invalid stage for target stripping case: '{}' (target={})".format(stage, target))

    gender = target_elem.find('gender').text.strip().lower()
    target_clothing_type = clothing_elems[stage].attributes['type'].strip().lower()
    target_clothing_position = clothing_elems[stage].attributes['position'].strip().lower()

    if target_clothing_type == 'extra':
        return gender+'_removing_accessory'
    elif target_clothing_type == 'minor' or target_clothing_type == 'major':
        return gender+'_removing_'+target_clothing_type
    elif target_clothing_type == 'important':
        if target_clothing_position == 'upper':
            return gender+'_chest_will_be_visible'
        elif target_clothing_position == 'lower':
            return gender+'_crotch_will_be_visible'
        else:
            raise ValueError("Unknown clothing position for target '{}' stage {}: {}".format(target, stage, target_clothing_position))
    else:
        raise ValueError("Unknown clothing type for target '{}' stage {}: {}".format(target, stage, target_clothing_type))


def get_target_stripped_case(target, stage, opponents_dir=None):
    target_elem = get_target_xml(target, opponents_dir)
    clothing_elems = [e for e in target_elem.find('wardrobe').iter('clothing')]
    clothing_elems.reverse()

    if stage < 1 or stage > len(clothing_elems):
        raise IndexError("Invalid stage for target stripped case: '{}' (target={})".format(stage, target))

    gender = target_elem.find('gender').text.strip().lower()
    size = target_elem.find('size').text.strip().lower()
    target_clothing_type = clothing_elems[stage-1].attributes['type'].strip().lower()
    target_clothing_position = clothing_elems[stage-1].attributes['position'].strip().lower()

    if target_clothing_type == 'extra':
        return gender+'_removed_accessory'
    elif target_clothing_type == 'minor' or target_clothing_type == 'major':
        return gender+'_removed_'+target_clothing_type
    elif target_clothing_type == 'important':
        if target_clothing_position == 'upper':
            if gender == 'female':
                return 'female_'+size+'_chest_is_visible'
            elif gender == 'male':
                return 'male_chest_is_visible'
            else:
                raise ValueError("Unknown gender for target '{}' stage {}: {}".format(target, stage, gender))
        elif target_clothing_position == 'lower':
            if gender == 'male':
                return 'male_'+size+'_crotch_is_visible'
            elif gender == 'female':
                return 'female_crotch_is_visible'
            else:
                raise ValueError("Unknown gender for target '{}' stage {}: {}".format(target, stage, gender))
        else:
            raise ValueError("Unknown clothing position for target '{}' stage {}: {}".format(target, stage, target_clothing_position))
    else:
        raise ValueError("Unknown clothing type for target '{}' stage {}: {}".format(target, stage, target_clothing_type))


def load_character(char_id, opponents_path=None):
    opponents_path = find_opponents_directory(opponents_path)
    
    if opponents_path is None:
        raise ValueError("Cannot find SPNATI opponents directory!")
    
    behaviour_path = opponents_path / char_id / 'behaviour.xml'
    meta_path = opponents_path / char_id / 'meta.xml'
    
    opponent_elem = bp.parse_file(str(behaviour_path))
    meta_elem = bp.parse_meta(str(meta_path))
    
    opponent_meta = Opponent.from_xml(opponent_elem, meta_elem)
    lineset = xml_to_lineset(opponent_elem)
    
    return lineset, opponent_meta


def config_image_directory(d):
    global __image_directory
    __image_directory = d


def find_image(pose_file, stage, directory=None):
    global __image_directory
    if directory is None:
        directory = __image_directory
        
    # If the pose_file already exists then just assume that's what they're looking for
    if osp.exists(osp.join(directory, pose_file)):
        return pose_file

    # If the pose_file isn't prefixed with the stage then do that
    if not pose_file[0].isdigit():
        pose_file = "{:d}-{:s}".format(stage, pose_file)
    
    root, ext = osp.splitext(pose_file)
    
    # Do what make_xml.py does: if the image doesn't have an extension, or if
    # the extension doesn't match any known filetype, then assume the image
    # should have a .png extension:
    if ext not in IMAGE_FORMATS:
        pose_file += '.png'
        
    return pose_file
    
    
def get_image_path(image):
    global __image_directory
    return osp.join(__image_directory, image)
    
    
def generate_comment():
    return WARNING_COMMENT.format(VERSION, time.strftime(COMMENT_TIME_FORMAT))


def parse_interval(val):
    interval_tuple = val.split('-')

    if len(interval_tuple[0].strip()) == 0:  # handles intervals with a single negative number
        return int(val), int(val)

    low = int(interval_tuple[0].strip())

    if len(interval_tuple) > 1:
        hi = int(interval_tuple[1].strip())
    else:
        hi = int(low)

    return low, hi


def format_interval(interval):
    low, hi = interval
    if low == hi:
        return str(low)
    else:
        return str(low)+'-'+str(hi)


def all_cases(lineset):
    for case_list in lineset.values():
        for case in case_list:
            yield case
            

def get_unique_line_count(lineset):
    unique_lines = set()
    unique_targeted_lines = set()
    n_cases = 0
    n_targeted_cases = 0

    for case in all_cases(lineset):
        for state in case.states:
            unique_lines.add(state.to_tuple())
            if case.is_targeted():
                unique_targeted_lines.add(state.to_tuple())

        n_cases += 1
        if case.is_targeted():
            n_targeted_cases += 1

    return len(unique_lines), len(unique_targeted_lines), n_cases, n_targeted_cases
