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
        elif individual.get('status') == 'testing':
            opponents_list.append(individual.text)
    
    with open(sys.argv[2], 'w', encoding='utf-8', newline='') as f:
        writer = csv.writer(f)
        
        print("id  -  unique lines  -  unique targeted lines  -  cases  -  targeted cases")
        writer.writerow(['id', 'unique lines', 'unique targeted lines', 'cases', 'targeted cases'])
        
        for opponent in opponents_list:
            opp_path = opponents_dir.joinpath(opponent, 'behaviour.xml')
            if opp_path.exists() and opp_path.is_file():
                try:
                    lineset = c2x.xml_to_lineset(bp.parse_file(str(opp_path)))
                    unique_lines, unique_targeted_lines, num_cases, num_targeted_cases = c2x.get_unique_line_count(lineset)
                    print("{}  -  {}  -  {}  -  {}  -  {}".format(opponent, unique_lines, unique_targeted_lines, num_cases, num_targeted_cases))
                    writer.writerow([opponent, unique_lines, unique_targeted_lines, num_cases, num_targeted_cases])
                except bp.ParseError as e:
                    pass
