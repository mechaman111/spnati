import argparse
import csv
import os.path as osp
from pathlib import Path

import csv2xml.behaviour_parser as bp
import csv2xml as c2x

case_sort_order = {
    'stripped': 0,
    'must_strip_losing': 1,
    'must_strip_normal': 2,
    'must_strip_winning': 3,
    'must_masturbate_first': 4,
    'must_masturbate': 5,
    'stripping': 6,
    'start_masturbating': 7,
    'masturbating': 8,
    'heavy_masturbating': 9,
    'finishing_masturbating': 10,
    'finished_masturbating': 11
}

def get_case_sort_key(case):
    return case_sort_order[case.tag]
    

def get_all_stripping_cases(lineset):
    case_sets = {}
    
    for stage_set, cases in lineset.items():
        for case in filter(lambda c: c.tag in case_sort_order, cases):
            for stage in filter(lambda s: isinstance(s, int), stage_set):
                if stage not in case_sets:
                    case_sets[stage] = c2x.CaseSet()
                
                case_sets[stage].add(case)
    
    return case_sets
    
    
def generate_report_rows(lineset, opponent_id):
    case_sets = get_all_stripping_cases(lineset)
    
    for stage, case_set in sorted(case_sets.items(), key=lambda kv: kv[0]):
        for case in sorted(case_set, key=get_case_sort_key):
            formatted_conditions = case.format_conditions(True)

            for state in case.states:
                yield {
                    'id': opponent_id,
                    'stage': str(stage),
                    'case': case.tag,
                    'conditions': formatted_conditions,
                    'image': state.image,
                    'text': state.text,
                    'marker': state.marker
                }
                
        yield {}


def main(args=None):
    if args is None:
        parser = argparse.ArgumentParser(description="Extracts a character's stripping sequence into a CSV file.")
        parser.add_argument("--opponents-dir", "-d", help="Path to the SPNATI opponents directory.")
        parser.add_argument("character", help="The character ID to extract from.")
        parser.add_argument("out_file", help="The path of the CSV file to write to.")
        
        args = parser.parse_args()
        
    lineset, opponent_meta = c2x.load_character(args.character, args.opponents_dir)

    with open(args.out_file, 'w', encoding='utf-8', newline='') as f:
        writer = csv.DictWriter(f, ['id', 'stage', 'case', 'conditions', 'image', 'text', 'marker'])
        
        writer.writeheader()
        writer.writerows(generate_report_rows(lineset, args.character))
        
        
if __name__ == '__main__':
    main()
