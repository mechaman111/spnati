#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import unicode_literals

import sys
if sys.version_info[0] < 3:
    reload(sys)
    sys.setdefaultencoding('UTF8')
    from io import open

import csv
import os
import os.path as osp
import argparse

import csv2xml as c2x
import csv2xml.behaviour_parser as bp

def main():
    parser = argparse.ArgumentParser(description='Converts SPNATI behaviour files between .xml and .csv formats.')
    parser.add_argument('--opponent-dir', '-d', default=os.getcwd(), help="Path to SPNATI's opponents/ directory. Defaults to the current working directory.")
    parser.add_argument('infile', help='Input file to process.')
    parser.add_argument('outfile', help='Output file to write to.')
    args = parser.parse_args()

    c2x.config_default_opponents_dir(args.opponent_dir)

    inroot, inext = osp.splitext(args.infile)
    outroot, outext = osp.splitext(args.outfile)

    print("Reading input file...")
    if inext == '.xml':
        opponent_elem = bp.parse_file(args.infile)
        meta_elem = bp.parse_meta(osp.join(osp.dirname(args.infile), 'meta.xml'))

        opponent_meta = c2x.Opponent.from_xml(opponent_elem, meta_elem)
        lineset = c2x.xml_to_lineset(opponent_elem)
    elif inext == '.csv':
        with open(args.infile, newline='', encoding='utf-8') as f:
            reader = csv.DictReader(f)
            lineset, opponent_meta = c2x.csv_to_lineset(reader)

    unique_lines, unique_targeted_lines, num_cases, num_targeted_cases = c2x.get_unique_line_count(lineset)

    print("Statistics:")
    print("Unique Lines: {}".format(unique_lines))
    print("Unique Targeted Lines: {}".format(unique_targeted_lines))
    print("Total Cases: {}".format(num_cases))
    print("Total Targeted Cases: {}".format(num_targeted_cases))

    print("Writing output file...")
    if outext == '.xml':
        opponent_elem = opponent_meta.to_xml()
        meta_elem = opponent_meta.to_meta_xml()

        behaviour_elem, start_elem = c2x.lineset_to_xml(lineset)
        opponent_elem.children.insert(-1, start_elem)
        opponent_elem.children.append(behaviour_elem)

        with open(args.outfile, 'w', encoding='utf-8') as f:
            f.write("<?xml version='1.0' encoding='UTF-8'?>\n")
            f.write('<!-- '+c2x.generate_comment()+' -->\n\n')
            f.write('<!--\n')
            f.write('    File Statistics:\n')
            f.write('    Unique Lines: {}\n'.format(unique_lines))
            f.write('    Unique Targeted Lines: {}\n'.format(unique_targeted_lines))
            f.write('    Total Cases: {}\n'.format(num_cases))
            f.write('    Total Targeted Cases: {}\n'.format(num_targeted_cases))
            f.write('-->\n\n'.format(num_targeted_cases))
            f.write(opponent_elem.serialize())

        with open(osp.join(osp.dirname(args.outfile), 'meta.xml'), 'w', encoding='utf-8') as meta_f:
            meta_f.write("<?xml version='1.0' encoding='UTF-8'?>\n")
            meta_f.write('<!-- '+c2x.generate_comment()+' -->\n')
            meta_f.write(meta_elem.serialize())

    elif outext == '.csv':
        with open(args.outfile, 'w', newline='', encoding='utf-8') as f:
            fieldnames = [
                'stage',
                'case',
                'conditions',
                'image',
                'text',
                'marker',
                'priority',
                'silent',
            ]

            writer = csv.DictWriter(f, fieldnames, restval='')
            writer.writeheader()
            writer.writerow({'stage': 'comment', 'text': c2x.generate_comment()})
            writer.writerow({'stage': 'comment', 'text': 'File Statistics:'})
            writer.writerow({'stage': 'comment', 'text': 'Unique Lines: {}'.format(unique_lines)})
            writer.writerow({'stage': 'comment', 'text': 'Unique Targeted Lines: {}'.format(unique_targeted_lines)})
            writer.writerow({'stage': 'comment', 'text': 'Total Cases: {}'.format(num_cases)})
            writer.writerow({'stage': 'comment', 'text': 'Total Targeted Cases: {}'.format(num_targeted_cases)})
            c2x.lineset_to_csv(lineset, opponent_meta, writer)
            
if __name__ == '__main__':
    main()
