#!/usr/bin/env python3
from __future__ import print_function, unicode_literals

import sys
if sys.version_info[0] < 3:
    reload(sys)
    sys.setdefaultencoding('UTF8')
    from io import open

import behaviour_parser as bp
import csv2xml as c2x
from argparse import ArgumentParser, FileType
import os
import os.path as osp

def extract_case(fname, case_tag):
    lineset = c2x.parse_xml_to_lineset(fname)
    
    ret_case_list = []
    
    for stage_set, cases in lineset.items():
        for case in cases:
            if case.tag == case_tag:
                ret_case_list.append((stage_set, case))
                
    return ret_case_list

if __name__ == '__main__':
    parser = ArgumentParser(description='Pulls all states for a given case tag from a behaviour.xml file.')
    parser.add_argument('-c', '--csv', help='Output extracted cases to a supplied CSV file.')
    parser.add_argument('--generic', action='store_true', help='Output generic cases only.')
    parser.add_argument('--targeted', help='Output cases targeted towards a specific opponent ID only.')
    parser.add_argument('--filtered', help='Output cases filtered against a specific tag only.')
    parser.add_argument('case', help='The case tag to extract.')
    parser.add_argument('files', metavar='file', nargs='+', help='Files to extract cases from. The opponent ID is automatically extracted from the name of the containing folder.')
    
    args = parser.parse_args()
    
    output = {}
    
    for fname in args.files:
        opp_id = osp.basename(osp.dirname(osp.abspath(fname)))
        cases = extract_case(fname, args.case)
        rows = []
        
        for stage_set, case in cases:
            if args.generic:
                if len(case.conditions) > 0 or len(case.counters) > 0:
                    continue
                    
            if args.targeted is not None:
                if 'target' not in case.conditions or case.conditions['target'] != args.targeted:
                    continue
            
            if args.filtered is not None:
                if 'filter' not in case.conditions or case.conditions['filter'] != args.filtered:
                    continue
                    
            for state in case.states:
                priority = ''
                if case.priority is not None:
                    priority = str(case.priority)
                                
                rows.append({
                    'id': opp_id,
                    'stages': c2x.format_stage_set(stage_set),
                    'case': args.case,
                    'conditions': case.format_conditions(),
                    'priority': priority,
                    'image': state.image,
                    'text': state.text,
                    'marker': state.marker,
                    'silent': state.silent
                })
        if len(rows) > 0:
            output[opp_id] = rows
    
    if args.csv is not None:
        with open(args.csv, 'w', encoding='utf-8', newline='') as outfile:
            writer = csv.DictWriter(outfile, ['id', 'stages', 'case', 'conditions', 'priority', 'image', 'text', 'marker', 'silent'])
            for opp_id, rows in output.items():
                for row in rows:
                    writer.writerow(row)
    else:
        for opp_id, rows in output.items():
            if len(output) > 1:
                print("=== Opponent {} =======".format(opp_id))
                
            for row in rows:
                print("\nStages {} - Case {} [{}]:".format(row['stages'], row['case'], row['conditions']))
                for state in case.states:
                    if state.marker is None:
                        print("    [{}] {}".format(row['image'], row['text']))
                    else:
                        print("    [{}] {} [marker={}]".format(row['image'], row['text'], row['marker']))
