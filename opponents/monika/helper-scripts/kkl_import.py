"""
Support library for working with KisekaeLocal.

This module contains functions for importing Kisekae codes using KisekaeLocal,
and functions for processing the generated image files.

It can also serve as a utility for automatically importing sets of codes from
CSV files, plain text files, or directly from the command line.
"""

import argparse
import csv
import os
import os.path as osp
from pathlib import Path
import re
import sys
import time

from PIL import Image


SETUP_STRING_33   = "33***bc185.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]ua1.0.0.0_ub_uc7.0.30_ud7.0"
SETUP_STRING_36   = "36***bc185.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0_ub_u0_v0_uc7.0.30_ud7.0"
SETUP_STRING_40   = "40***bc185.500.0.0.1*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v0_uc7.2.24_ud7.8"
SETUP_STRING_68   = "68***ba50_bb6.0_bc410.500.8.0.1.0_bd6_be180_ad0.0.0.0.0.0.0.0.0.0_ae0.3.3.0.0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_e00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v00_ud7.8_uc7.2.24"
SEPARATOR = "#/]"

CODE_SPLIT_REGEX = r"(\d+?)\*\*\*?([^\#\/\]]+)(?:\#\/\](.+))?"


class KisekaeComponent(object):
    def __init__(self, data):
        """
        Represents a subcomponent of a Kisekae character or scene.
        
        Attributes:
            id (str): An ID identifying this subcomponent's type.
            prefix (str): A prefix identifying this subcomponent.
            attributes (list of str): The attributes associated with this component.
        """
        
        if data[1].isalpha():
            self.id = data[0:2]      # code is 2 letters
            self.prefix = data[0:2]
        else:
            self.id = data[0]
            self.prefix = data[0:3]  # code is 1 letter + 2 digits
        
        self.attributes = data[len(self.prefix):].split('.')

    def __str__(self):
        return self.prefix + '.'.join(self.attributes)


class KisekaeCharacter(object):
    def __init__(self, character_data):
        """
        Represents a collection of subcodes.
        
        Attributes:
            subcodes (list of KisekaeComponent): The subcodes contained within this object.
        """
        
        self.subcodes = []
        
        for subcode in character_data.split('_'):
            self.subcodes.append(KisekaeComponent(subcode))

    def __str__(self):
        return '_'.join(str(sc) for sc in self.subcodes)
        
    def find(self, subcode_id):
        """
        Find the first inner KisekaeComponent with the given `subcode_id`.
        """
        
        for sc in self.subcodes:
            if sc.id == subcode_id:
                return sc
                
    def iter(self, subcode_id):
        """
        Iterate over all inner KisekaeComponents with the given `subcode_id`
        """
        
        return filter(lambda sc: sc.id == subcode_id, self.subcodes)


class KisekaeCode(object):
    def __init__(self, code):
        """
        Represents an entire importable Kisekae code, possibly containing
        character data and scene data.
        
        Attributes:
            version (int): The version of Kisekae used to generate this code.
            scene (KisekaeCharacter): Container for scene data and attributes.
            characters (list of KisekaeCharacter): List of characters contained in the code.
        """
        
        m = re.match(CODE_SPLIT_REGEX, code.strip())
        if m is None:
            return None
            
        version, character_data, scene_data = m.groups()
        
        self.version = int(version)
        self.characters = []
        
        if scene_data is not None:
            self.scene = KisekaeCharacter(scene_data)
        else:
            self.scene = None
        
        for character in character_data.split('*'):
            if character == '0':
                continue
                
            self.characters.append(KisekaeCharacter(character))
            
    def __str__(self):
        ret = str(self.version) + '**'
        
        if self.scene is not None:
            for i in range(9):
                if i >= len(self.characters):
                    ret += '*0'
                else:
                    ret += '*' + str(self.characters[i])
                    
            ret += SEPARATOR + str(self.scene)
        else:
            ret += str(self.characters[0])
        
        return ret
            
    
def disable_character_motion(character):
    """
    Disables automatic motion for a character.
    
    Args:
        character (KisekaeCharacter): The character to modify.
    """
    
    ad = character.find('ad')
    ae = character.find('ae')
    
    if ad is not None:
        ad.attributes = ['0'] * 10
        
    if ae is not None:
        ae.attributes = ['0', '3', '3', '0', '0']
    
    return character
    

def close_character_vagina(character):
    """
    Closes / un-spreads a Kisekae character's vagina.
    
    Args:
        character (KisekaeCharacter): The character to modify.
    """
    
    dc = character.find('dc')
    
    if dc is not None:
        dc.attributes[5] = '0'
    
    return character
    
    
def get_kkl_directory():
    """
    Retrieve the path to the main KisekaeLocal application directory.
    """
    
    if sys.platform == 'darwin':
        return Path.home() / "Library" / "Application Support" / "kkl" / "Local Store"
    else :
        return Path(os.getenv('APPDATA')) / "kkl" / "Local Store"


def process_kkl_code(code, scene_name):
    """
    Import an image into KisekaeLocal.
    Returns a Path object to the output image once it has been generated.
    
    Args:
        code (str): The Kisekae code to import.
        scene_name (str): A pose name to use when importing.
    """
    
    input_path = get_kkl_directory().joinpath(scene_name + '.txt')
    output_path = get_kkl_directory().joinpath(scene_name + '.png')
    
    # remove the input and output files if they already exist
    if output_path.is_file():
        output_path.unlink()
        
    if input_path.is_file():
        input_path.unlink()
    
    sys.stdout.write("Importing: {:s}... ".format(scene_name))
    sys.stdout.flush()
    
    with input_path.open('w', encoding='utf-8') as f:
        f.write(code)
        
    # wait for KKL to process the file
    #print("Waiting for output file to be generated...")
    while input_path.is_file() or not output_path.is_file():
        time.sleep(0.1)
        
    return output_path


def open_image_file(path):
    """
    Attempt to open an image file generated by KKL.
    """
    
    retry_time_limit = 10 #  try for at most 10 seconds before saying there's a problem
    retry_interval = 0.2  #  re-check for the existance of the image every 200 milliseconds
    retry_limit = int(retry_time_limit // retry_interval) # try 50 times
    
    for retry in range(retry_limit):
        try:
            image_file = Image.open(path)
            return image_file
        except IOError:
            if retry >= retry_limit-1:
                raise
            time.sleep(retry_interval)

    
def get_crop_box(width=600, height=1400, center_x=1000, margin_y=15):
    """
    Calculate a cropping rectangle.
    
    Args:
        width (int): The width of the output image.
        height (int): The height of the output image.
        center_x (int): Position along the X-axis for the center of the rectangle.
        margin_y (int): How far down along the Y-axis to position the rectangle.
        
    Returns:
        A cropping box as a 4-tuple, suitable to be passed to Image.crop().
    """
    
    left = center_x - int(width // 2)
    right = center_x + int(width // 2)
    upper = margin_y
    lower = height + margin_y
    
    return (left, upper, right, lower)
    
    
def auto_crop_box(image, margin_y=15):
    """
    Automatically calculate a centered cropping rectangle from an image's bounding box.
    
    Args:
        margin_y (int): How much empty margin space to leave at the bottom of the image, in px.
        
    Returns:
        A cropping box as a 4-tuple, suitable to be passed to Image.crop().    
    """
    
    left, top, right, bottom = image.getbbox()
    
    out_width = (right - left) + 2
    if out_width < 600:
        out_width = 600
    
    out_height = bottom - top
    if out_height < 1400:
        out_height = 1400
        
    center_x = int((right + left) / 2)
    
    crop_left = int(center_x - (out_width // 2))
    crop_right = int(center_x + (out_width // 2))
    crop_top = (bottom + margin_y) - out_height
    crop_bottom = bottom + margin_y
    
    return (crop_left, crop_top, crop_right, crop_bottom)
    
    
def import_character(import_code, pose_name='imported', remove_motion=True, close_vagina=True):
    """
    Import a code into KisekaeLocal and open the generated image file.
    
    Args:
        import_code (str): The Kisekae code to import.
        pose_name (str): A pose name to use when importing.
        remove_motion (bool): If True (default), then a code fragment will be automatically appended to the imported code to disable unwanted breast motion.
            This may potentially override settings from the imported code itself, so be wary.
    """
    
    code = KisekaeCode(import_code)
    
    if remove_motion:
        disable_character_motion(code.characters[0])
    
    if close_vagina:    
        close_character_vagina(code.characters[0])
        
    output_path = process_kkl_code(str(code), pose_name)
    
    img = open_image_file(output_path)
    img.load()
    
    output_path.unlink()
    
    return img

def setup_kkl_scene():
    """
    Send a scene reset code to KKL to prepare for image importing.
    This will clear away unnecessary characters, backgrounds, objects, etc.
    """
    
    output_path = process_kkl_code(SETUP_STRING_68, 'scene_setup')
    output_path.unlink()
    
    sys.stdout.write("done.\n")
    sys.stdout.flush()
    
    
def crop_and_save(img, crop_box, dest_filename):
    """
    Crop an image and save it.
    This is a convenience function.
    
    Args:
        img (PIL.Image): The image object to crop and save.
        crop_box (4-tuple): The cropping rectangle.
        dest_filename (pathlib.Path or str): The destination filename to save to.
    """
    
    cropped_img = img.crop(crop_box)
    cropped_img.save(dest_filename)
    cropped_img.close()
    img.close()
    
    
def process_csv(infile, dest_dir, **kwargs):
    """
    Import and process a set of Kisekae codes listed in a CSV file.
    
    Args:
        infile (pathlib.Path): The CSV file to read.
        dest_dir (pathlib.Path): The destination folder to save to.
    
    Kwargs:
        stage (list of int, optional): If set, then only codes for these stages will be imported.
        pose (list of str, optional): If set, then only codes for these poses will be imported.
    """
    
    setup_kkl_scene()
    with infile.open('r', encoding='utf-8', newline='') as f:
        reader = csv.DictReader(f, restval='')
        for row in reader:
            if len(row['stage']) <= 0:
                continue
                
            stage = int(row['stage'])
            
            if kwargs['stage'] is not None and len(kwargs['stage']) > 0 and stage not in kwargs['stage']:
                continue
            
            if kwargs['pose'] is not None and len(kwargs['stage']) > 0 and row['pose'] not in kwargs['pose']:
                continue
                
            pose_name = '{:d}-{:s}'.format(stage, row['pose'])
                
            dest_filename = dest_dir.joinpath(pose_name+'.png')
            
            remove_motion = (row.get('remove_motion', 'true').strip().lower() != 'false')
            close_vagina = (row.get('close_vagina', 'true').strip().lower() != 'false')
            
            width = row['width'].strip().lower()
            height = row['height'].strip().lower()
            center_x = row['center_x'].strip().lower()
            margin_y = int(row['margin_y'].strip().lower())
            
            kkl_output = import_character(row['code'], pose_name, remove_motion=remove_motion, close_vagina=close_vagina)
            
            if width == 'auto' or height == 'auto' or center_x == 'auto':
                crop_box = auto_crop_box(kkl_output, margin_y)
            else:
                crop_box = get_crop_box(int(width), int(height), int(center_x), margin_y)
            
            if dest_filename.is_file():
                dest_filename.unlink()

            crop_and_save(kkl_output, crop_box, dest_filename)
            
            sys.stdout.write("done.\n")
            sys.stdout.flush()
    

def process_file(infile, dest_dir, **kwargs):
    """
    Import and process a set of Kisekae codes listed in a plain-text file.
    
    Args:
        infile (pathlib.Path): The text file to read.
        dest_dir (pathlib.Path): The destination folder to save to.
    
    Kwargs:
        remove_motion (bool, optional): If True (default), then a code fragment will be automatically appended to the imported codes to disable unwanted breast motion.
            This may potentially override settings from the imported codes themselves, so be wary.
        auto_crop (bool, optional): If True, then cropping boxes will be automatically calculated based on the generated image bounding boxes.
            The `width`, `height`, and `center_x` kwargs will be ignored.
        width (int): The width of the generated images (when using manual cropping).
        height (int): The height of the generated images (when using manual cropping).
        center_x (int): The position along the X-axis around which to center the generated images (when using manual cropping).
        margin_y (int): When using manual cropping, this is how far down to position the cropping box.
            When using automatic cropping, this controls how much empty space to add at the bottom of the generated images.
    """
        
    setup_kkl_scene()
    with infile.open('r', encoding='utf-8') as f:
        for line in f:
            pose_name, code = line.split('=', 1)
            pose_name = name.strip()
            code = code.strip()
            
            dest_filename = dest_dir.joinpath(pose_name+'.png')
            
            kkl_output = import_character(code, pose_name, **kwargs)
            
            if dest_filename.is_file():
                dest_filename.unlink()
                
            width = int(kwargs.get('width', 600))
            height = int(kwargs.get('height', 1400))
            center_x = int(kwargs.get('center_x', 1000))
            margin_y = int(kwargs.get('margin_y', 15))
                
            if kwargs.get('auto_crop', True):
                crop_box = auto_crop_box(kkl_output, margin_y)
            else:
                crop_box = get_crop_box(width, height, center_x, margin_y)
                
            crop_and_save(kkl_output, crop_box, dest_filename)
            
            sys.stdout.write("done.\n")
            sys.stdout.flush()
            
            
def process_single(code, dest, **kwargs):
    """
    Import and process a Kisekae code.
    
    Args:
        code (str): The Kisekae code to import.
        dest (pathlib.Path): The destination to save to. If the destination exists and is a directory, the image will be saved here as `out.png`.
            Otherwise, it will be treated as a filename to save to.
    
    Kwargs:
        remove_motion (bool, optional): If True (default), then a code fragment will be automatically appended to the imported codes to disable unwanted breast motion.
            This may potentially override settings from the imported codes themselves, so be wary.
        auto_crop (bool, optional): If True, then cropping boxes will be automatically calculated based on the generated image bounding boxes.
            The `width`, `height`, and `center_x` kwargs will be ignored.
        width (int): The width of the generated images (when using manual cropping).
        height (int): The height of the generated images (when using manual cropping).
        center_x (int): The position along the X-axis around which to center the generated images (when using manual cropping).
        margin_y (int): When using manual cropping, this is how far down to position the cropping box.
            When using automatic cropping, this controls how much empty space to add at the bottom of the generated images.
    """
        
    setup_kkl_scene()
    
    if dest.is_dir():
        dest_filename = dest.joinpath('out.png')
    else:
        dest_filename = dest
    
    kkl_output = import_character(code, 'out', **kwargs)
    
    if dest_filename.is_file():
        dest_filename.unlink()
        
    width = int(kwargs.get('width', 600))
    height = int(kwargs.get('height', 1400))
    center_x = int(kwargs.get('center_x', 1000))
    margin_y = int(kwargs.get('margin_y', 15))
        
    if kwargs.get('auto_crop', True):
        crop_box = auto_crop_box(kkl_output, margin_y)
    else:
        crop_box = get_crop_box(width, height, center_x, margin_y)
        
    crop_and_save(kkl_output, crop_box, dest_filename)
    
    sys.stdout.write("done.\n")
    sys.stdout.flush()
    
    
if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Imports a pose into KisekaeLocal, and saves an automatically cropped image.')
    parser.add_argument('--width', '-w', default=600, help='Output image width.')
    parser.add_argument('--height', '-l', default=1400, help='Output image height.')
    parser.add_argument('--center-x', '-x', default=1000, help='X position to center the output image around.')
    parser.add_argument('--margin-y', '-y', default=15, help='Number of margin pixels from top when cropping output image.')
    parser.add_argument('--manual-crop', '--manual', '-m', action='store_false', dest='auto_crop', help='Do not automatically calculate crop with bounding box.')
    parser.add_argument('--stage', nargs='*', type=int, help='(CSV-only) Import codes for a particular stage or set of stages only.')
    parser.add_argument('--pose', nargs='*', help='(CSV-only) Import codes for a particular pose or set of poses only.')
    parser.add_argument('--no-remove-motion', action='store_false', dest='remove_motion', help="Do not automatically set motion parameters to 'Manual' on importing.")
    parser.add_argument('--no-close-vagina', action='store_false', dest='close_vagina', help="Do not automatically close the flaps of a female character's vagina on importing.")
    parser.add_argument('--file', '-f', help='Load codes from text file.')
    parser.add_argument('--csv', '-c', help='Load codes from a CSV file.')
    parser.add_argument('--code', '-i', help='Load code as command line argument. [dest_dir] is the destination image path in this case.')
    parser.add_argument('dest_dir', help='Destination directory to output to.')
    args = parser.parse_args()
    
    kwargs = vars(args)
    
    dest_dir = Path(args.dest_dir).resolve()
    del kwargs['dest_dir']
    
    if args.csv is not None:
        process_csv(Path(args.csv), dest_dir, **kwargs)
    elif args.file is not None:
        process_file(Path(args.file), dest_dir, **kwargs)
    elif args.code is not None:
        code = args['code']
        del kwargs['code']
        process_single(code, dest_dir, **kwargs)
    else:
        raise ValueError("Must provide at least one of --csv, --file, or --code.")
    
