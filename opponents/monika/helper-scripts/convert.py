import argparse
import os

from csv2xml.__main__ import convert

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Converts SPNATI behaviour files between .xml and .csv formats.')
    parser.add_argument('--opponent-dir', '-d', default=os.getcwd(), help="Path to SPNATI's opponents/ directory. Defaults to the current working directory.")
    parser.add_argument('infile', help='Input file to process.')
    parser.add_argument('outfile', help='Output file to write to.')
    args = parser.parse_args()
    
    convert(args.infile, args.outfile, opponent_dir=args.opponent_dir)
