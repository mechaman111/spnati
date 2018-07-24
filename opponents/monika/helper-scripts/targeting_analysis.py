import csv
import csv2xml as c2x
from pathlib import Path
import itertools as it
from operator import itemgetter
import sys

def find_all_targeting(char_id):
    lineset, opponent_meta = c2x.load_character(char_id)
    
    for stageset, case in filter(lambda t: t[1].is_targeted(), c2x.iter_lineset(lineset)):
        for state in case.states:
            m = ''
            if state.marker is not None:
                m = state.marker
                
            yield {
                'stage': c2x.format_stage_set(stageset),
                'case': case.tag,
                'conditions': case.format_conditions(sort=True),
                'image': state.image,
                'text': state.text,
                'marker': m
            }

def sort_rows(row_iter):
    s = sorted(row_iter, key=itemgetter('case'))
    s = sorted(s, key=itemgetter('conditions'))
    s = sorted(s, key=itemgetter('stage'))
    
    return s

def group_rows(row_iter):
    for stages, group in it.groupby(row_iter, key=itemgetter('conditions')):
        yield from group
        yield {}

if __name__ == '__main__':
    p = Path(sys.argv[2])
    
    with p.open('w', encoding='utf-8', newline='') as f:
        writer = csv.DictWriter(f, ['stage', 'case', 'conditions', 'image', 'text', 'marker'])
        writer.writeheader()
        
        rows = group_rows(sort_rows(find_all_targeting(sys.argv[1])))
        writer.writerows(rows)
