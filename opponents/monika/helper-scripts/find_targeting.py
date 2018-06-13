import csv
import os.path as osp
from pathlib import Path
import behaviour_parser as bp
from csv2xml import xml_to_lineset, format_stage_set, format_interval, Opponent

def search_opponent(opponent_folder, target_id, target_tags):
    id = opponent_folder.name
    behaviour_file = opponent_folder.joinpath('behaviour.xml')
    
    try:
        lineset = xml_to_lineset(bp.parse_file(str(behaviour_file)))
    except bp.ParseError:
        print("Warning: could not parse behaviour.xml file for "+id)
        return [], []
    
    targeted_cases = []
    filtered_cases = []
    
    for stage_set, case_list in lineset.items():
        for case in case_list:
            filter_condition = None
            target_condition = None
            for condition in case.conditions:
                if condition[0] == 'target':
                    target_condition = condition[1] == target_id
                elif condition[0] == 'filter':
                    filter_condition = condition[1] in target_tags
            
            if filter_condition is not None and not filter_condition:
                continue
                
            if target_condition is not None and not target_condition:
                continue
            
            case_tuple = (id, stage_set, case)
            
            if filter_condition is not None and filter_condition:
                filtered_cases.append(case_tuple)
                    
            if target_condition is not None and target_condition:
                targeted_cases.append(case_tuple)

    return targeted_cases, filtered_cases

def csv_report(case_list, dict_writer):
    for case_tuple in case_list:
        from_id, stage_set, case = case_tuple
        
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
            })

def console_report(from_id, case_list):
    print("[{:s}]".format(from_id))
    for case_tuple in case_list:
        _, stage_set, case = case_tuple
        
        print("Stage(s) {:s}, Case {:s} [{:s}]:".format(
            format_stage_set(stage_set), case.tag, case.format_conditions()
        ))
        
        for state in case.states:
            print("    ({:s}) {:s}".format(state.image, state.text))
            
        print("")
            
if __name__ == '__main__':
    import sys
    
    if len(sys.argv) < 4:
        print("USAGE: find_targeting.py [behaviour.xml] [direct_targeted_lines.csv] [filtered_lines.csv]")
    
    main_file = Path(sys.argv[1]).resolve()
    
    if main_file.name != 'behaviour.xml':
        raise ValueError("The first argument to this script must be a behaviour.xml file!")
    
    cur_id = main_file.parent.name
    opponents_dir = main_file.parents[1]
    
    main_elem = bp.parse_file(str(main_file))
    main_opp = Opponent.from_xml(main_elem, None)
    
    opponents_list = []
    if not opponents_dir.joinpath('listing.xml').exists():
        raise FileNotFoundError("Could not find listing.xml!")
        
    listing_elem = bp.parse_listing(str(opponents_dir.joinpath('listing.xml')))
    for individual in listing_elem.find('individuals').iter('opponent'):
        if 'status' not in individual.attributes:
            opponents_list.append(individual.text)
        elif individual.get('status') == 'testing':
            opponents_list.append(individual.text)
            
    all_targeted_cases = []
    all_filtered_cases = []
    
    for opponent in opponents_list:
        if opponent == cur_id:
            continue
            
        opp_path = opponents_dir.joinpath(opponent)
        print("Processing character: "+opponent)
        
        targeted_cases, filtered_cases = search_opponent(opp_path, cur_id, main_opp.tags)
        
        if len(targeted_cases) > 0:
            all_targeted_cases.append(targeted_cases)
            
        if len(filtered_cases) > 0:
            all_filtered_cases.append(filtered_cases)
    
    print("=== Directly Targeted Cases ===")
    with open(sys.argv[2], 'w', newline='') as target_report:
        writer = csv.DictWriter(target_report, [
            'id', 'stages', 'case', 'conditions', 'image', 'text', 'marker',
            'silent'
        ])
        
        writer.writeheader()
        
        for case_list in all_targeted_cases:
            console_report(case_list[0][0], case_list)
            
            csv_report(case_list, writer)
            writer.writerow({})
            
    print("\n=== Tag-Filtered Cases ===")
    with open(sys.argv[3], 'w', newline='') as filter_report:
        writer = csv.DictWriter(filter_report, [
            'id', 'stages', 'case', 'conditions', 'image', 'text', 'marker',
            'silent'
        ])
        
        writer.writeheader()
        
        for case_list in all_filtered_cases:
            console_report(case_list[0][0], case_list)
            
            csv_report(case_list, writer)
            writer.writerow({})
