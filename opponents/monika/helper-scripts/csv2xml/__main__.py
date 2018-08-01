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
from pathlib import Path
import argparse
import logging

import csv2xml as c2x
import csv2xml.behaviour_parser as bp

def many_csv_reader(indir):
    for file in filter(lambda f: f.suffix == '.csv', indir.iterdir()):
        logging.info("Reading input file: {:s}".format(file.name))
        with file.open('r', newline='', encoding='utf-8') as f:
            yield from csv.DictReader(f)


def convert(infile, outfile, **kwargs):  
    opp_dir = Path(kwargs.get('opponent_dir', os.getcwd()))
    infile = Path(infile)
    outfile = Path(outfile)
      
    c2x.config_default_opponents_dir(opp_dir)
    c2x.config_image_directory(infile.parent)

    if infile.is_file():
        logging.info("Reading input file...")
        if infile.suffix == '.xml':
            opponent_elem = bp.parse_file(str(infile))
            meta_elem = bp.parse_meta(str(infile.parent.joinpath('meta.xml'))) # osp.join(osp.dirname(infile), 'meta.xml')

            opponent_meta = c2x.Opponent.from_xml(opponent_elem, meta_elem)
            lineset = c2x.xml_to_lineset(opponent_elem)
            
            epilogues = None # TODO: deserialize epilogues later
        elif infile.suffix == '.csv':
            with infile.open('r', newline='', encoding='utf-8') as f:
                reader = csv.DictReader(f)
                lineset, opponent_meta, epilogues = c2x.csv_to_lineset(reader)
    elif infile.is_dir():
        lineset, opponent_meta, epilogues = c2x.csv_to_lineset(many_csv_reader(infile))
            

    unique_lines, unique_targeted_lines, num_cases, num_targeted_cases = c2x.get_unique_line_count(lineset)

    logging.info("Statistics:")
    logging.info("Unique Lines: {}".format(unique_lines))
    logging.info("Unique Targeted Lines: {}".format(unique_targeted_lines))
    logging.info("Total Cases: {}".format(num_cases))
    logging.info("Total Targeted Cases: {}".format(num_targeted_cases))

    logging.info("Writing output file...")
    if outfile.suffix == '.xml':
        opponent_elem = opponent_meta.to_xml()
        meta_elem = opponent_meta.to_meta_xml()

        behaviour_elem, start_elem = c2x.lineset_to_xml(lineset)
        opponent_elem.children.insert(-1, start_elem)
        opponent_elem.children.append(behaviour_elem)
        
        if epilogues is not None:
            for epilogue in epilogues:
                opponent_elem.children.append(epilogue.to_xml())

        with outfile.open('w', encoding='utf-8') as f:
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

        with infile.parent.joinpath('meta.xml').open('w', encoding='utf-8') as meta_f:
            meta_f.write("<?xml version='1.0' encoding='UTF-8'?>\n")
            meta_f.write('<!-- '+c2x.generate_comment()+' -->\n')
            meta_f.write(meta_elem.serialize())

    elif outfile.suffix == '.csv':
        with outfile.open('w', newline='', encoding='utf-8') as f:
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
    parser = argparse.ArgumentParser(description='Converts SPNATI behaviour files between .xml and .csv formats.')
    parser.add_argument('--opponent-dir', '-d', default=os.getcwd(), help="Path to SPNATI's opponents/ directory. Defaults to the current working directory.")
    parser.add_argument('infile', help='Input file to process.')
    parser.add_argument('outfile', help='Output file to write to.')
    args = parser.parse_args()
    
    convert(args.infile, args.outfile, opponent_dir=args.opponent_dir)
