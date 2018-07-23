import argparse
import csv
import itertools as it
from operator import itemgetter

import csv2xml as c2x

def find_targeting(from_char, to_char):
    lineset, opponent_meta = c2x.load_character(from_char)
    
    rows = []
    line_to_row = {}
    
    for stageset, case in c2x.iter_lineset(lineset):
        targeted_to_char = False
        stage_targeting = None
        
        if 'target' in case.conditions:
            if case.conditions['target'] == to_char:
                targeted_to_char = True
                stage_targeting = case.conditions.get('targetStage', None)
            else:
                continue
        
        if 'alsoPlaying' in case.conditions and case.conditions['alsoPlaying'] == to_char:
            targeted_to_char = True
            stage_targeting = case.conditions.get('alsoPlayingStage', None)
            
        if not targeted_to_char:
            continue
            
        tag = case.tag
        if tag.startswith('must_strip_'):
            tag = 'must_strip_self'
        
        for state in case.states:
            line_to_row[state.text.strip()] = {
                'from': from_char,
                'from-stage': stageset,
                'to': to_char,
                'to-stage': stage_targeting,
                'case': tag,
                'conditions': case.format_conditions(True),
                'image': state.image,
                'text': state.text,
                'marker': state.marker
            }
    
    return list(line_to_row.values())

SELF_STRIPPING_TAGS = [
    'must_strip_self',
    'must_strip_losing',
    'must_strip_normal',
    'must_strip_winning',
    'stripping',
    'stripped',
    'must_masturbate_first',
    'must_masturbate',
    'start_masturbating',
    'masturbating',
    'heavy_masturbating',
    'finished_masturbating',
]

STRIPPING_TAGS = [
    'must_strip_self',
    'must_strip_losing',
    'must_strip_normal',
    'must_strip_winning',
    'female_must_strip',
    'male_must_strip',
    'female_removing_accessory',
    'male_removing_accessory',
    'female_removing_minor',
    'male_removing_minor',
    'female_removing_major',
    'male_removing_major',
    'female_chest_will_be_visible',
    'male_chest_will_be_visible',
    'female_crotch_will_be_visible',
    'male_crotch_will_be_visible',
    'stripping',
    'female_removed_accessory',
    'male_removed_accessory',
    'female_removed_minor',
    'male_removed_minor',
    'female_removed_major',
    'male_removed_major',
    'male_chest_is_visible',
    'female_small_chest_is_visible',
    'female_medium_chest_is_visible',
    'female_large_chest_is_visible',
    'female_crotch_is_visible',
    'male_small_crotch_is_visible',
    'male_medium_crotch_is_visible',
    'male_large_crotch_is_visible',
    'stripped',
    'must_masturbate_first',
    'must_masturbate',
    'female_must_masturbate',
    'male_must_masturbate',
    'start_masturbating',
    'female_start_masturbating',
    'male_start_masturbating',
    'masturbating',
    'female_masturbating',
    'male_masturbating',
    'heavy_masturbating',
    'female_heavy_masturbating',
    'male_heavy_masturbating',
    'finished_masturbating',
    'female_finished_masturbating',
    'male_finished_masturbating',
]

def is_stripping_case(row):
    return row['case'] in STRIPPING_TAGS

def get_stripping_case_sort_key(row):
    if row['case'] in STRIPPING_TAGS:
        return STRIPPING_TAGS.index(row['case'])
    return 0


def stage_set_key(field):
    def _sorter(row):
        if row[field] is None:
            return 999
        else:
            return sum(row[field])
    return _sorter

def stages_to_strings(row):
    row = row.copy()
    row['from-stage'] = c2x.format_stage_set(row['from-stage'])
    if row['to-stage'] is not None:
        row['to-stage'] = c2x.format_interval(row['to-stage'])
    else:
        row['to-stage'] = ''
        
    return row
    
def get_stripping_rows(rows):
    stripping_rows = filter(is_stripping_case, rows)
    stripping_rows = sorted(stripping_rows, key=get_stripping_case_sort_key)
    
    for tag, case_group in it.groupby(stripping_rows, key=itemgetter('case')):
        if tag in SELF_STRIPPING_TAGS:
            case_group = sorted(case_group, key=itemgetter('from'))
            char_iter = it.groupby(case_group, key=itemgetter('from'))
        else:
            case_group = sorted(case_group, key=itemgetter('to'))
            char_iter = it.groupby(case_group, key=itemgetter('to'))
            
        for _, char_group in char_iter:
            if tag in SELF_STRIPPING_TAGS:
                char_group = sorted(char_group, key=stage_set_key('from-stage'))
                stage_iter = it.groupby(char_group, key=itemgetter('from-stage'))
            else:
                char_group = sorted(char_group, key=stage_set_key('to-stage'))
                stage_iter = it.groupby(char_group, key=itemgetter('to-stage'))
                
            for _, stage_group in stage_iter:
                yield from map(stages_to_strings, stage_group)
                yield {}
                
    #return stripping_rows

def get_other_rows(rows):
    other_rows = it.filterfalse(is_stripping_case, rows)
    other_rows = sorted(other_rows, key=itemgetter('from-stage'))
    other_rows = sorted(other_rows, key=itemgetter('to-stage'))
    other_rows = sorted(other_rows, key=itemgetter('from'))
    other_rows = sorted(other_rows, key=lambda r: c2x.Case.ALL_TAGS.index(r['case']))
    
    for tag, case_group in it.groupby(other_rows, key=itemgetter('case')):
        for char, char_group in it.groupby(case_group, key=itemgetter('from')):
            yield from char_group
            yield {}

def main(args):
    rows = find_targeting(args.char_1, args.char_2)
    rows.extend(find_targeting(args.char_2, args.char_1))
    
    fields = ['from', 'from-stage', 'to', 'to-stage', 'case', 'conditions', 'image', 'text', 'marker']
    
    with open(args.outfile, 'w', encoding='utf-8', newline='') as f:
        writer = csv.DictWriter(f, fields, dialect='unix')
        writer.writeheader()
        
        writer.writerows(get_stripping_rows(rows))
        
if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Finds all instances of targetting between two characters.')
    parser.add_argument('char_1', help='The first character to analyze.')
    parser.add_argument('char_2', help='The second character to analyze.')
    parser.add_argument('outfile', help='CSV file to write to.')
    args = parser.parse_args()
    
    main(args)
    
