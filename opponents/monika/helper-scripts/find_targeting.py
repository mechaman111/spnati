import csv
import os.path as osp
from pathlib import Path
import csv2xml.behaviour_parser as bp
from csv2xml.stage import format_stage_set
from csv2xml.xml_format import xml_to_lineset
from csv2xml.utils import format_interval
from csv2xml.opponent import Opponent
import csv2xml as c2x

naked_stage_tags = [
    'male_must_masturbate',
    'female_must_masturbate',
    'male_start_masturbating',
    'female_start_masturbating',
]

mast_stage_tags = [
    'female_masturbating',
    'male_masturbating',
    'female_heavy_masturbating',
    'male_heavy_masturbating',
]

finished_stage_tags = [
    'male_finished_masturbating',
    'female_finished_masturbating',
]

upper_exposed_tags = [
    'male_chest_is_visible',
    'female_small_chest_is_visible',
    'female_medium_chest_is_visible',
    'female_large_chest_is_visible',
]

lower_exposed_tags = [
    'female_crotch_is_visible',
    'male_small_crotch_is_visible',
    'male_medium_crotch_is_visible',
    'male_large_crotch_is_visible',
]

upper_preexposed_tags = [
    'male_chest_will_be_visible',
    'female_chest_will_be_visible',
]

lower_preexposed_tags = [
    'female_crotch_will_be_visible',
    'male_crotch_will_be_visible',
]

def search_opponent(opponent_id, target_id, cur_meta, upper_exposed_stage, lower_exposed_stage):
    try:
        lineset, _ = c2x.load_character(opponent_id)
    except bp.ParseError as e:
        print("Warning: could not parse behaviour.xml file for "+opponent_id)
        print(str(e))
        return []
    
    targeted_cases = []
    
    for stage_set, case_list in lineset.items():
        for case in case_list:
            target_condition = None
            target_stage = None
            
            if 'target' in case.conditions:
                target_condition = case.conditions['target'] == target_id
                if target_condition:
                    if 'targetStage' in case.conditions:
                        target_stage = case.conditions['targetStage']
                    elif case.tag in naked_stage_tags:
                        target_stage = cur_meta.naked_stage()
                    elif case.tag in mast_stage_tags:
                        target_stage = cur_meta.masturbate_stage()
                    elif case.tag in finished_stage_tags:
                        target_stage = cur_meta.finished_stage()
                    elif case.tag in upper_exposed_tags:
                        target_stage = upper_exposed_stage
                    elif case.tag in lower_exposed_tags:
                        target_stage = lower_exposed_stage
                    elif case.tag in upper_preexposed_tags and upper_exposed_stage is not None:
                        target_stage = upper_exposed_stage-1
                    elif case.tag in lower_preexposed_tags and lower_exposed_stage is not None:
                        target_stage = lower_exposed_stage-1
            
            if not target_condition and 'alsoPlaying' in case.conditions:
                target_condition = case.conditions['alsoPlaying'] == target_id
                if target_condition and 'alsoPlayingStage' in case.conditions:
                    target_stage = case.conditions['alsoPlayingStage']

            if not target_condition:
                continue
                
            if isinstance(target_stage, int):
                target_stage = (target_stage, target_stage)
            
            targeted_cases.append((opponent_id, stage_set, case, target_stage))

    return targeted_cases

def csv_report(case_list, dict_writer):
    for case_tuple in case_list:
        from_id, stage_set, case, target_stage = case_tuple
        
        if isinstance(target_stage, tuple):
            target_stage = c2x.format_interval(target_stage)
        
        for state in case.states:
            dict_writer.writerow({
                'id': from_id,
                'stages': format_stage_set(stage_set),
                'case': case.tag,
                'conditions': case.format_conditions(),
                'image': state.image,
                'text': state.text,
                'marker': state.marker,
                'silent': state.silent,
                'targeted stage': target_stage
            })
            
if __name__ == '__main__':
    import sys
    
    if len(sys.argv) < 3:
        print("USAGE: find_targeting.py [character ID] [direct_targeted_lines.csv]")
        
    cur_id = sys.argv[1]
    
    lineset, opponent_meta = c2x.load_character(cur_id)

    all_targeted_cases = []
    
    upper_exposed_stage = None
    lower_exposed_stage = None
    
    for idx, article in enumerate(opponent_meta.wardrobe):
        if article['type'] != 'important':
            continue
        
        if article['position'] == 'upper':
            upper_exposed_stage = len(opponent_meta.wardrobe) - idx
        elif article['position'] == 'lower':
            lower_exposed_stage = len(opponent_meta.wardrobe) - idx
        
    
    for opponent in c2x.list_opponents():
        if opponent == cur_id:
            continue
            
        print("Processing character: "+opponent)
        
        targeted_cases = search_opponent(opponent, cur_id, opponent_meta, upper_exposed_stage, lower_exposed_stage)
        
        if len(targeted_cases) > 0:
            all_targeted_cases.append(targeted_cases)
    
    with open(sys.argv[2], 'w', newline='', encoding='utf-8') as target_report:
        writer = csv.DictWriter(target_report, [
            'id', 'stages', 'case', 'conditions', 'image', 'text', 'marker',
            'silent', 'targeted stage'
        ])
        
        writer.writeheader()
        
        for case_list in all_targeted_cases:
            csv_report(case_list, writer)
            writer.writerow({})
