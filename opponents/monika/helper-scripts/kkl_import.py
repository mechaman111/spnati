import argparse
import csv
import os
import os.path as osp
from pathlib import Path
import sys
import time

from PIL import Image


REMOVE_MOTION_CODE = "_ad0.0.0.0.0.0.0.0.0.0_ae0.3.3.0.0"

SETUP_STRING_33   = "33***bc185.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]ua1.0.0.0_ub_uc7.0.30_ud7.0"
SETUP_STRING_36   = "36***bc185.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0_ub_u0_v0_uc7.0.30_ud7.0"
SETUP_STRING_40   = "40***bc185.500.0.0.1*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v0_uc7.2.24_ud7.8"
SETUP_STRING_68   = "68***ba50_bb6.0_bc410.500.8.0.1.0_bd6_be180_ad0.0.0.0.0.0.0.0.0.0_ae0.3.3.0.0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_e00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v00_ud7.8_uc7.2.24"
# SETUP_STRING_68 = "68***ba50_bb6.0_bc410.500.28.0.1.0_bd6_be180*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_e00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v00_ud7.8_uc7.2.24"
# SETUP_STRING_68 = "68***f00*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_e00_y00_z00_ua1.57.57.59.100_uf0.3.59.0_ue_ub_u0_v00_ud7.8_uc7.2.24"
SEPARATOR = "#/]"


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
    left = center_x - int(width // 2)
    right = center_x + int(width // 2)
    upper = margin_y
    lower = height + margin_y
    
    return (left, upper, right, lower)
    
    
def auto_crop_box(image, margin_y=15):
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
    
    
def import_character(code, pose_name='imported',remove_motion=True):
    if remove_motion:
        code = code + REMOVE_MOTION_CODE
        
    output_path = process_kkl_code(code, pose_name)
    
    img = open_image_file(output_path)
    img.load()
    
    output_path.unlink()
    
    return img

def setup_kkl_scene():
    """
    Send a scene reset code to KKL to prepare for image importing.
    Clear away unnecessary characters, backgrounds, objects, etc.
    """
    
    output_path = process_kkl_code(SETUP_STRING_68, 'scene_setup_file')
    output_path.unlink()
    
    sys.stdout.write("done.\n")
    sys.stdout.flush()
    
    
def crop_and_save(img, crop_box, dest_filename):
    cropped_img = img.crop(crop_box)
    cropped_img.save(str(dest_filename))
    cropped_img.close()
    img.close()
    
    
def process_csv(infile, dest_dir, **kwargs):
    setup_kkl_scene()
    with infile.open('r', encoding='utf-8', newline='') as f:
        reader = csv.DictReader(f, restval='')
        for row in reader:
            if len(row['stage']) <= 0:
                continue
                
            stage = int(row['stage'])
            
            if kwargs['stage'] is not None and kwargs['stage'] != stage:
                continue
            
            if kwargs['pose'] is not None and kwargs['pose'] != row['pose']:
                continue
                
            pose_name = '{:d}-{:s}'.format(stage, row['pose'])
                
            dest_filename = dest_dir.joinpath(pose_name+'.png')
            
            remove_motion = (row['remove_motion'].strip().lower() != 'false')
            
            width = row['width'].strip().lower()
            height = row['height'].strip().lower()
            center_x = row['center_x'].strip().lower()
            margin_y = int(row['margin_y'].strip().lower())
            
            kkl_output = import_character(row['code'], pose_name, remove_motion)
            
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
    setup_kkl_scene()
    with infile.open('r', encoding='utf-8') as f:
        for line in f:
            pose_name, code = line.split('=', 1)
            pose_name = name.strip()
            code = code.strip()
            
            dest_filename = dest_dir.joinpath(pose_name+'.png')
            
            kkl_output = import_character(code, pose_name, kwargs.get('remove_motion', True))
            
            if dest_filename.is_file():
                dest_filename.unlink()
                
            width = int(kwargs.get('width', 600))
            height = int(kwargs.get('height', 1400))
            center_x = int(kwargs.get('center_x', 1000))
            margin_y = int(kwargs.get('margin_y', 15))
                
            if kwargs.get('auto_crop', False):
                crop_box = auto_crop_box(kkl_output, margin_y)
            else:
                crop_box = get_crop_box(width, height, center_x, margin_y)
                
            crop_and_save(kkl_output, crop_box, dest_filename)
            
            print("Successfully exported: {:s}".format(pose_name))
            
            
def process_single(code, dest_dir, **kwargs):
    setup_kkl_scene()
    
    if dest_dir.is_dir():
        dest_filename = dest_dir.joinpath('out.png')
    else:
        dest_filename = dest_dir
    
    kkl_output = import_character(code, 'out', kwargs.get('remove_motion', True))
    
    if dest_filename.is_file():
        dest_filename.unlink()
        
    width = int(kwargs.get('width', 600))
    height = int(kwargs.get('height', 1400))
    center_x = int(kwargs.get('center_x', 1000))
    margin_y = int(kwargs.get('margin_y', 15))
        
    if kwargs.get('auto_crop', False):
        crop_box = auto_crop_box(kkl_output, margin_y)
    else:
        crop_box = get_crop_box(width, height, center_x, margin_y)
        
    crop_and_save(kkl_output, crop_box, dest_filename)
    
    print("Successfully exported: out.png")
    
    
if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Imports a pose into KisekaeLocal, and saves an automatically cropped image.')
    parser.add_argument('--width', '-w', default=600, help='Output image width.')
    parser.add_argument('--height', '-l', default=1400, help='Output image height.')
    parser.add_argument('--center-x', '-x', default=1000, help='X position to center the output image around.')
    parser.add_argument('--margin-y', '-y', default=15, help='Number of margin pixels from top when cropping output image.')
    parser.add_argument('--auto-crop', '--auto', '-a', action='store_true', help='Automatically calculate crop with bounding box.')
    parser.add_argument('--stage', type=int, help='(CSV-only) Import codes for a particular stage only')
    parser.add_argument('--pose', help='(CSV-only) Import codes for a particular pose only')
    parser.add_argument('--no-remove-motion', action='store_false', dest='remove_motion', help="Do not automatically set motion parameters to 'Manual' on importing.")
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
    
