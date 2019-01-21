import behaviour_parser as bp
from ordered_xml import OrderedXMLElement
import sys

def main():
    
    root = bp.parse_file('./behaviour.xml')

if __name__ == '__main__':
    main()
