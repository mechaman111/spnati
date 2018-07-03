#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import unicode_literals

import sys
if sys.version_info[0] < 3:
    reload(sys)
    sys.setdefaultencoding('UTF8')
    from io import open

import csv
from collections import OrderedDict
import os
import os.path as osp
import time

from .behaviour_parser import parse_file, parse_meta
from .ordered_xml import OrderedXMLElement
from .opponent import Opponent
from .xml_format import xml_to_lineset, lineset_to_xml
from .csv_format import csv_to_lineset, lineset_to_csv
from .utils import get_unique_line_count

VERSION = '0.16.0-alpha'  # will attempt to follow semver if possible
COMMENT_TIME_FORMAT = 'at %X %Z on %A, %B %d, %Y'  # strftime format
WARNING_COMMENT = 'This file was machine generated using csv2xml.py {:s} {:s}. Please do not edit it directly without preserving your improvements elsewhere or your changes may be lost the next time this file is generated.'

def generate_comment():
    return WARNING_COMMENT.format(VERSION, time.strftime(COMMENT_TIME_FORMAT))

def parse_xml_to_lineset(fname):
    opponent_elem = parse_file(fname)
    return xml_to_lineset(opponent_elem)

if __name__ == '__main__':
    if len(sys.argv) < 3:
        print("USAGE: python csv2xml.py [infile(.csv|.xml)] [outfile(.csv|.xml)]")

    infile = sys.argv[1]
    outfile = sys.argv[2]

    inroot, inext = osp.splitext(infile)
    outroot, outext = osp.splitext(outfile)

    print("Reading input file...")
    if inext == '.xml':
        opponent_elem = parse_file(infile)
        meta_elem = parse_meta(osp.join(osp.dirname(infile), 'meta.xml'))

        opponent_meta = Opponent.from_xml(opponent_elem, meta_elem)
        lineset = xml_to_lineset(opponent_elem)
    elif inext == '.csv':
        with open(infile, newline='', encoding='utf-8') as f:
            reader = csv.DictReader(f)
            lineset, opponent_meta = csv_to_lineset(reader)

    unique_lines, unique_targeted_lines, num_cases, num_targeted_cases = get_unique_line_count(lineset)

    print("Statistics:")
    print("Unique Lines: {}".format(unique_lines))
    print("Unique Targeted Lines: {}".format(unique_targeted_lines))
    print("Total Cases: {}".format(num_cases))
    print("Total Targeted Cases: {}".format(num_targeted_cases))

    print("Writing output file...")
    if outext == '.xml':
        opponent_elem = opponent_meta.to_xml()
        meta_elem = opponent_meta.to_meta_xml()

        behaviour_elem, start_elem = lineset_to_xml(lineset)
        opponent_elem.children.insert(-1, start_elem)
        opponent_elem.children.append(behaviour_elem)

        with open(outfile, 'w', encoding='utf-8') as f:
            f.write("<?xml version='1.0' encoding='UTF-8'?>\n")
            f.write('<!-- '+generate_comment()+' -->\n\n')
            f.write('<!--\n')
            f.write('    File Statistics:\n')
            f.write('    Unique Lines: {}\n'.format(unique_lines))
            f.write('    Unique Targeted Lines: {}\n'.format(unique_targeted_lines))
            f.write('    Total Cases: {}\n'.format(num_cases))
            f.write('    Total Targeted Cases: {}\n'.format(num_targeted_cases))
            f.write('-->\n\n'.format(num_targeted_cases))
            f.write(opponent_elem.serialize())

        with open(osp.join(osp.dirname(outfile), 'meta.xml'), 'w', encoding='utf-8') as meta_f:
            meta_f.write("<?xml version='1.0' encoding='UTF-8'?>\n")
            meta_f.write('<!-- '+generate_comment()+' -->\n')
            meta_f.write(meta_elem.serialize())

    elif outext == '.csv':
        with open(outfile, 'w', newline='', encoding='utf-8') as f:
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
            writer.writerow({'stage': 'comment', 'text': generate_comment()})
            writer.writerow({'stage': 'comment', 'text': 'File Statistics:'})
            writer.writerow({'stage': 'comment', 'text': 'Unique Lines: {}'.format(unique_lines)})
            writer.writerow({'stage': 'comment', 'text': 'Unique Targeted Lines: {}'.format(unique_targeted_lines)})
            writer.writerow({'stage': 'comment', 'text': 'Total Cases: {}'.format(num_cases)})
            writer.writerow({'stage': 'comment', 'text': 'Total Targeted Cases: {}'.format(num_targeted_cases)})
            lineset_to_csv(lineset, opponent_meta, writer)
