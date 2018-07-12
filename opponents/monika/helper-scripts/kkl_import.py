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
    
    print("Sending code for {:s} to KKL...".format(scene_name))
    with input_path.open('w', encoding='utf-8') as f:
        f.write(code)
        
    # wait for KKL to process the file
    print("Waiting for output file to be generated...")
    while input_path.is_file() or not output_path.is_file():
        time.sleep(0.1)
        
    return output_path


def open_image_file(path):
    retry_time_limit = 10 #  try for at most 10 seconds before saying there's a problem
    retry_interval = 0.2  #  re-check for the existance of the image every 200 milliseconds
    retry_limit = int(retry_time_limit // retry_interval) # try 50 times
    
    for retry in range(retry_limit):
        try:
            image_file = Image.open(str(path))
            return image_file
        except IOError:
            if retry >= retry_limit-1:
                raise
            time.sleep(retry_interval)


def import_as_image(code, scene_name='imported'):
    output_path = process_kkl_code(code, scene_name)
    return open_image_file(output_path)
    
    
def get_crop_box(width=600, height=1400, center_x = 1000, margin_y=15):
    left = center_x - int(width // 2)
    right = center_x + int(width // 2)
    upper = margin_y
    lower = height + margin_y
    
    return (left, upper, right, lower)
    
def import_character(code, pose_name='imported', crop_box=None, remove_motion=True):
    if remove_motion:
        code = code + REMOVE_MOTION_CODE
        
    img = import_as_image(code, pose_name)
    
    if crop_box is None:
        crop_box = get_crop_box()
    
    cropped_image = img.crop(crop_box)
    cropped_image.load()
    
    img.close()
    
    return cropped_image
    

def setup_kkl_scene():
    """
    Send a scene reset code to KKL to prepare for image importing.
    Clear away unnecessary characters, backgrounds, objects, etc.
    """
    
    output_path = process_kkl_code(SETUP_STRING_68, 'scene_setup_file')
    output_path.unlink()
    
    
if __name__ == '__main__':
    dest_filename = Path(sys.argv[1]).resolve()
    code = sys.argv[2]
    
    if dest_filename.is_file():
        dest_filename.unlink()
    
    setup_kkl_scene()
    
    kkl_output = import_character(code, dest_filename.stem)
    kkl_output.save(str(dest_filename))
    kkl_output.close()
    
