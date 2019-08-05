from PIL import Image
from pathlib import Path
import sys

COMPOSITE_ORDER = [3, 0, 1, 2]
COMPONENT_BASE_DIR = Path('./components')
OUTPUT_DIR = Path('./out')

def clip_by_y(img, clip_start, clip_end):
    dest = Image.new('RGBA', img.size)

    cropped = img.crop((0, clip_start, img.width, clip_end))
    dest.paste(cropped, (0, clip_start))

    return dest

def composite_pose(pose_name, div_y):
    dest = Image.new('RGBA', (800, 1400))
    
    for idx in COMPOSITE_ORDER:
        comp = COMPONENT_BASE_DIR.joinpath(str(idx), pose_name+'.png')
        with Image.open(comp) as img:
            component_img = img
            if idx == 1:
                component_img = clip_by_y(img, 0, div_y)
            elif idx == 2:
                component_img = clip_by_y(img, div_y, img.height)
            
            dest.alpha_composite(component_img)

    dest.save(OUTPUT_DIR.joinpath(pose_name+'.png'))

if __name__ == "__main__":
    composite_pose(sys.argv[1], int(sys.argv[2]))