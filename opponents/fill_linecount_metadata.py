from __future__ import print_function

import os
import os.path as osp
from bs4 import BeautifulSoup
import xml.etree.ElementTree as ET
import shutil
import stat
import sys
import re

def getRelevantStagesForTrigger(tag, layers):
    if tag in ('selected', 'game_start'):
        return (0, 0)
    if tag in ('swap_cards', 'good_hand', 'okay_hand', 'bad_hand', 'hand', 'game_over_victory'):
        return (0, layers)
    if tag in ('must_strip_winning', 'must_strip_normal', 'must_strip_losing', 'must_strip', 'stripping'):
        return (0, layers - 1)
    if tag == 'stripped':
        return (1, layers)
    if tag in ('must_masturbate_first', 'must_masturbate', 'start_masturbating'):
        return (layers, layers)
    if tag in ('masturbating', 'heavy_masturbating', 'finishing_masturbating'):
        return (layers + 1, layers + 1)
    if tag in ('finished_masturbating', 'after_masturbating', 'game_over_defeat'):
        return (layers + 2, layers + 2)
    return (0, layers + 2)

def parseInterval(string):
    pieces = string.split("-")
    min = None
    max = None
    if pieces[0].strip() != "":
        try:
            min = int(pieces[0])
        except ValueError:
            return None
    if len(pieces) == 1:
        max = min
    else:
        max = int(pieces[1])

    return (min, max)

def inInterval(value, interval):
    return interval and (interval[0] is None or interval[0] <= value) and (interval[1] is None or value <= interval[1])

def checkStage(curStage, stageStr):
    if stageStr is None:
        return True
    for stageInt in re.split('\\s+', stageStr):
        if inInterval(curStage, parseInterval(stageInt)):
            return True
    return False

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

    num_layers = len(soup.findChild('wardrobe').findChildren('clothing'))
    print(num_layers)
    
    for state in soup.find_all('state', recursive=True):
        case = state.findParent()
        trigger = case.findParent('trigger')
        stage = case.findParent('stage')
        if trigger is not None:
            stageInterval = getRelevantStagesForTrigger(trigger.get('id'), num_layers)
        elif stage is not None:
            stageInterval = (int(stage.get('id')), int(stage.get('id')))
        else: # probably legacy <start>
            stageInterval = (0, 0)

        for stage in range(stageInterval[0], stageInterval[1] + 1):
            images = [ x.get_text() for x in filter(lambda t: checkStage(stage, t.get('stage')), state.findChildren('image')) ]

            if len(images) == 0:
                images = [ state.get('img', '') ]
            for pose in images:
                all_poses.add(pose.replace('#', str(stage)))
        
        dialogue = ''.join(str(child) for child in state.stripped_strings).strip()
        all_lines.add(dialogue)

    n_unique_lines = len(all_lines)
    n_poses = len(all_poses)
        
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
