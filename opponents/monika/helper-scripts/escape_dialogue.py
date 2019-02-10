from csv2xml import behaviour_parser as bp
from csv2xml.ordered_xml import OrderedXMLElement
from pathlib import Path
import sys
import shutil
import re

TEXT_ESCAPES = {
    '<': 'lt',
    '>': 'gt',
    '&': 'amp'
}
TEXT_ESCAPE_RE = re.compile('|'.join(TEXT_ESCAPES.keys()))

def main():
    shutil.copyfile('./behaviour.xml', './behaviour.xml.escape.bak')
    root = bp.parse_file('./behaviour.xml')

    for stage_elem in root.find('behaviour').iter('stage'):
        for case_elem in stage_elem.iter('case'):
            for state_elem in case_elem.iter('state'):
                state_elem.text = TEXT_ESCAPE_RE.sub(
                    lambda m: '&' + TEXT_ESCAPES[m.group(0)] + ';',
                    state_elem.text
                )
    
    with open('behaviour.xml', 'w', encoding='utf-8') as f:
        f.write(root.serialize())

if __name__ == '__main__':
    main()
