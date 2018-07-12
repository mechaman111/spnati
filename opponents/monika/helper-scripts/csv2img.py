import argparse
import csv
from pathlib import Path
import time

import kkl_import as kkl


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Converts a set of poses from a CSV file into cropped images via KisekaeLocal.')
    parser.add_argument('--no-remove-motion', action='store_false', dest='remove_motion', help="Do not automatically set motion parameters to 'Manual' on importing.")
    parser.add_argument('pose_list', help='Path to CSV file to import.')
    parser.add_argument('dest_dir', help='Path to image destination directory.')
    args = parser.parse_args()
    
    dest_dir = Path(args.dest_dir).resolve()
    
    kkl.setup_kkl_scene()
    
    with open(args.pose_list, 'r', encoding='utf-8', newline='') as f:
        reader = csv.DictReader(f)
        for row in reader:
            pose_name = row['name']
            dest_filename = dest_dir.joinpath(pose_name+'.png')
            
            crop_box = kkl.get_crop_box(row['width'], row['height'], row['center_x'], row['margin_y'])
            kkl_output = kkl.import_character(row['code'], pose_name, crop_box, args.remove_motion)
            
            if dest_filename.is_file():
                dest_filename.unlink()

            kkl_output.save(str(dest_filename))
            kkl_output.close()
            
            print("Successfully exported: {:s}".format(pose_name))
