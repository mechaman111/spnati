from __future__ import print_function

import os
import os.path as osp
from bs4 import BeautifulSoup
import shutil
import stat
import sys

def process(opponent_folder_path):
    opponent = osp.basename(opponent_folder_path)
    behaviour_path = osp.join(opponent_folder_path, 'behaviour.xml')
    meta_path = osp.join(opponent_folder_path, 'meta.xml')
    
    if not osp.exists(behaviour_path) or not osp.exists(meta_path):
        return
    
    with open(meta_path, 'r', encoding='utf-8') as f:
        meta_soup = BeautifulSoup(f.read())
    
    if meta_soup.opponent.lines is not None or meta_soup.opponent.poses is not None:
        return
    
    with open(behaviour_path, 'r', encoding='utf-8') as f:
        soup = BeautifulSoup(f.read())
    
    all_lines = set()
    all_poses = set()
    
    for state in soup.find_all('state', recursive=True):
        pose = state.get('img', '')
        all_poses.add(pose)
        
        dialogue = ''.join(str(child) for child in state.stripped_strings).strip()
        all_lines.add(dialogue)
        
    n_unique_lines = len(all_lines)
    n_poses = len(all_poses)
        
    print(opponent+":")
    print("    - "+str(n_unique_lines)+" lines")
    print("    - "+str(n_poses)+" poses")
    
    linecount_tag = soup.new_tag('lines')
    linecount_tag.string = str(n_unique_lines)
    
    posecount_tag = soup.new_tag('poses')
    posecount_tag.string = str(n_poses)
    
    meta_soup.opponent.append(linecount_tag)
    meta_soup.opponent.append(posecount_tag)
    
    with open(meta_path, 'w', encoding='utf-8') as f:
        f.write(str(meta_soup))

for arg in sys.argv[1:]:
    for name in os.listdir(arg):
        path = osp.join(arg, name)
        if osp.isdir(path):
            process(path)
