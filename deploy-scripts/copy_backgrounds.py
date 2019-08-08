#!/usr/bin/env python3

# Copies backgrounds according to their status attributes in backgrounds.xml.

import os
import os.path as osp
from bs4 import BeautifulSoup
import shutil
import sys

if len(sys.argv) > 2:
    base_path = sys.argv[1]
    dest_path = sys.argv[2]
else:
    base_path = os.getcwd()
    dest_path = sys.argv[1]


backgrounds_file = osp.join(base_path, "backgrounds.xml")

with open(backgrounds_file, 'r', encoding='utf-8') as f:
    soup = BeautifulSoup(f.read(), 'html.parser')
    
for background in soup.find_all(name='background', recursive=True):
    if background.status is not None and str(background.status.string) == 'offline':
        continue
    
    bg_rel_path = str(background.src.string)
    
    bg_src_path = osp.join(base_path, bg_rel_path)
    bg_dest_path = osp.join(dest_path, bg_rel_path)
    bg_dest_dir = osp.dirname(bg_dest_path)
    
    os.makedirs(bg_dest_dir, exist_ok=True)
    shutil.copyfile(bg_src_path, bg_dest_path)