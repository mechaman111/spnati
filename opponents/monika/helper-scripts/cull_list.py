import csv
from pathlib import Path
import os
import os.path as osp
import sys

import csv2xml.behaviour_parser as bp
import csv2xml as c2x
#from csv2xml import xml_to_lineset, get_unique_line_count

if __name__ == '__main__':
    if len(sys.argv) < 3:
        print("USAGE: linecount.py [opponents_path] [outfile]")
        sys.exit()
        
    opponents_dir = Path(sys.argv[1]).resolve()
    listing_file = opponents_dir.joinpath('listing.xml')
    
    opponents_list = []
    if not listing_file.exists():
        raise FileNotFoundError("Could not find listing.xml!")
        
    listing_elem = bp.parse_listing(str(listing_file))
    for individual in listing_elem.find('individuals').iter('opponent'):
        if 'status' not in individual.attributes:
            opponents_list.append(individual.text)
    
    with open(sys.argv[2], 'w', encoding='utf-8', newline='') as f:
        writer = csv.writer(f)
        
        line_counts = {}
        
        for opponent in opponents_list:
            opp_path = opponents_dir.joinpath(opponent, 'behaviour.xml')
            if opp_path.exists() and opp_path.is_file():
                print("Processing: "+opponent)
                root = bp.parse_file(str(opp_path))
                
                all_lines = set()
                targeted_lines = set()
                filtered_lines = set()
                
                for stage_elem in root.find('behaviour').iter('stage'):
                    for case_elem in stage_elem.iter('case'):
                        is_targeted = ('target' in case_elem.attributes)
                        is_filtered = ('filter' in case_elem.attributes)
                        
                        for cond_elem in case_elem.iter('condition'):
                            if 'filter' in cond_elem.attributes:
                                is_filtered = True
                        
                        for state_elem in case_elem.iter('state'):
                            all_lines.add(state_elem.text)
                            
                            if is_targeted:
                                targeted_lines.add(state_elem.text)
                                
                            if is_filtered:
                                filtered_lines.add(state_elem.text)
                
                n_lines = len(all_lines)
                n_targeted = len(targeted_lines)
                n_filtered = len(filtered_lines)
                
                line_counts[opponent] = (n_lines, n_targeted, n_filtered)
        
        opponents_list = sorted(opponents_list, key=lambda o: line_counts[o][2])
        opponents_list = sorted(opponents_list, key=lambda o: line_counts[o][1])
        opponents_list = sorted(opponents_list, key=lambda o: line_counts[o][0])
        
        total_line_cull = []
        target_cull = []
        filter_cull = []
        
        for opponent in opponents_list:
            n_lines, n_targeted, n_filtered = line_counts[opponent]
            
            if n_lines < 500:
                total_line_cull.append((opponent, n_lines))
            elif n_targeted < 25:
                target_cull.append((opponent, n_targeted))
            elif n_filtered < 25:
                filter_cull.append((opponent, n_filtered))
                
        cull_set = set()
        cull_list = []
        
        for opponent, _ in filter(lambda t: t[0] not in cull_set, sorted(total_line_cull, key=lambda t: t[1])):
            cull_set.add(opponent)
            cull_list.append((opponent, *line_counts[opponent], 'Fails unique line requirements (< 500 unique lines)'))
        
        for opponent, _ in filter(lambda t: t[0] not in cull_set, sorted(target_cull, key=lambda t: t[1])):
            cull_set.add(opponent)
            cull_list.append((opponent, *line_counts[opponent], 'Fails targeted line requirements (< 25 targeted lines)'))

        for opponent, _ in filter(lambda t: t[0] not in cull_set, sorted(filter_cull, key=lambda t: t[1])):
            cull_set.add(opponent)
            cull_list.append((opponent, *line_counts[opponent], 'Fails filtered line requirements (< 25 filtered lines)'))
        
        print("\norder - id -  unique lines  -  targeted lines - filtered lines - reason")
        writer.writerow(['cull order', 'id', 'unique lines', 'unique targeted lines', 'unique filtered lines', 'reason'])
        for i, data in enumerate(cull_list):
            print("{:2d} - {:15s} - {:4d} - {:4d} - {:4d} - {:s}".format(
                i+1, *data
            ))
            
            writer.writerow([i+1, *data])
