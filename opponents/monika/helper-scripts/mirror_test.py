import sys
from behaviour_parser import parse

if __name__ == '__main__':
    infn = sys.argv[1]
    outfn = sys.argv[2]

    in_text = ''
    with open(infn) as in_file:
        in_text = in_file.read()
        
    opponent = parse(in_text)
        
    with open(outfn, 'w') as out_file:
        out_file.write(opponent.serialize())
