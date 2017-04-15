#!/usr/bin/env python

# To use: python xml_unique_line_counter.py --file C:\file_path_here\GitHub\spni.github.io\opponents\character_name\behaviour.xml
# Verbose: python xml_unique_line_counter.py --file C:\file_path_here\GitHub\spni.github.io\opponents\character_name\behaviour.xml --verbose
# To specify a different output directory:
#   python xml_unique_line_counter.py -f path\to\character\behaviour.xml -o path\to\save\output\file
# To have the lines appear by most frequent first:
#   python xml_unique_line_counter.py -f path\to\character\behaviour.xml -s


# Parser:
# pip install html5lib
# pip install beautifulsoup4

from bs4 import BeautifulSoup
import os
import sys
import getopt
import logging
from collections import Counter
from glob import glob

logger = logging.getLogger(os.path.basename(__file__))
sort_most_frequent = False

def parse(f):

    l_ = []
    with open(f, 'r') as hlr:
        f_ = hlr.read()

    logger.debug("Read file: ******")
    logger.debug(f_)

    logger.debug("Parsing now: ******")
    soup = BeautifulSoup(f_, 'html5lib')
    for c, s in enumerate(soup.find_all('state')):
        text_ = s.text.strip()
        logger.debug('Found text: {}. Line number: {}'.format(text_.encode('utf-8'), c))
        l_.append(text_)
    logger.debug('****  Count *****')
    
    d = Counter(l_)
    ctr = 1
    
    line_iter_items = d.iteritems()
    if sort_most_frequent: # if the "-s" flag is enabled, order by most frequent lines first
        line_iter_items = d.most_common()
    
    logger.info('***** Dialogue count: {} *****'.format(f))
    
    for k, v in line_iter_items:
        logger.info('{} --> Frequency: {}, Line count: {}'.format(k.encode('utf-8'), v, ctr))
        ctr += 1
        
    logger.info('Unique dialogue count: {}\n'.format(len(d)))


if __name__ == '__main__':
    verbose = None
    output_dir = os.path.dirname(__file__)

    argv = sys.argv[1:]
    opts, args = getopt.getopt(argv, "d:vf:o:s", ["download=", "verbose", "file=", "output=", "sortfreq"])
    for opt, arg in opts:
        if opt in ("-v", "--verbose"):
            verbose = True
        elif opt in ("-f", "--file"):
            file_ = glob(arg)
        elif opt in ("-o", "--output"):
            output_dir = arg
        elif opt in ("-s", "--sortfreq"):
            sort_most_frequent = True
            
    log_file = os.path.join(output_dir, "line_count.log")
    file_hndlr = logging.FileHandler(log_file, mode='w')
    logger.addHandler(file_hndlr)
    console = logging.StreamHandler(stream=sys.stdout)
    logger.addHandler(console)
    ch = logging.Formatter('[%(levelname)s] %(message)s')
    console.setFormatter(ch)
    file_hndlr.setFormatter(ch)

    if verbose:
        logger.setLevel(logging.getLevelName('DEBUG'))
    else:
        logger.setLevel(logging.getLevelName('INFO'))
    logger.debug('CLI args: {}'.format(opts))
    
    for f in file_:
        parse(f)
