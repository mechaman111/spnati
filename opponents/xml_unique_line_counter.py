#!/usr/bin/env python

# To generate a unique line count for a single character (use -f or --file): 
#   python xml_unique_line_counter.py -f path\to\character\behaviour.xml
# Note that you can use wildcards to process multiple related characters at once:
#   python xml_unique_line_counter.py -f path\to\a*\behaviour.xml
#   (This will process all character directories which begin with the letter 'a')
# To generate a unique line count for multiple unrelated characters (use -F or --files):
#   python xml_unique_line_counter.py -F "path\to\character1\behaviour.xml path\to\character2\behaviour.xml"
#   (IMPORTANT: The paths must be enclosed in quotes AND the paths must NOT contain spaces)
#   (You can also use wildcards with this flag as well)
# To turn on verbose (debugging) output (use -v or --verbose):
#   python xml_unique_line_counter.py -f path\to\character\behaviour.xml -v
# To specify a different output directory (use -o or --output):
#   python xml_unique_line_counter.py -f path\to\character\behaviour.xml -o path\to\save\output\file
# To have the lines appear by most frequent first (use -s or --sortfreq):
#   python xml_unique_line_counter.py -f path\to\character\behaviour.xml -s
# To output only the total number of unique lines (use -t or --totalonly):
#   python xml_unique_line_counter.py -f path\to\character\behaviour.xml -t

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
total_count_only = False

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
    
    if total_count_only: # only display unique line count totals
        logger.info('{} : {}'.format(f, len(d)))
    else:
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
    opts, args = getopt.getopt(argv, "d:vf:F:o:st", ["download=", "verbose", "file=", "files=", "output=", "sortfreq", "totalonly"])
    for opt, arg in opts:
        if opt in ("-v", "--verbose"):
            verbose = True
        elif opt in ("-f", "--file"):
            file_ = glob(arg)
        elif opt in ("-F", "--files"):
            file_ = []
            file_args = arg.split(" ")
            for fa in file_args:
                file_.extend(glob(fa))
        elif opt in ("-o", "--output"):
            output_dir = arg
        elif opt in ("-s", "--sortfreq"):
            sort_most_frequent = True
        elif opt in ("-t", "--totalonly"):
            total_count_only = True
            
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
