import os
import os.path as osp
from pathlib import Path
import time


VERSION = '0.19.0-alpha'  # will attempt to follow semver if possible
COMMENT_TIME_FORMAT = 'at %X %Z on %A, %B %d, %Y'  # strftime format
WARNING_COMMENT = 'This file was machine generated using csv2xml.py {:s} {:s}. Please do not edit it directly without preserving your improvements elsewhere or your changes may be lost the next time this file is generated.'
IMAGE_FORMATS = ["png", "jpg", "jpeg", "gif", "gifv"]

__image_directory = Path(os.getcwd())


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
            t = ''
            if state.text is not None:
                t = state.text.strip()
                
            unique_lines.add(t)
            if case.is_targeted():
                unique_targeted_lines.add(t)

        n_cases += 1
        if case.is_targeted():
            n_targeted_cases += 1

    return len(unique_lines), len(unique_targeted_lines), n_cases, n_targeted_cases
