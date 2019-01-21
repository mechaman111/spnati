import behaviour_parser as bp
from ordered_xml import OrderedXMLElement
import sys
import shutil

def main():
    shutil.copyfile('./behaviour.xml', './behaviour.xml.bak')
    root = bp.parse_file('./behaviour.xml')
    
    for stage_elem in root.find('behaviour').iter('stage'):
        if stage_elem.get('id') != '5':
            continue
        
        for case_elem in stage_elem.iter('case'):
            if case_elem.get('tag') not in ['must_strip', 'must_strip_normal', 'must_strip_losing', 'must_strip_winning']:
                continue
            
            for state_elem in case_elem.iter('state'):
                state_elem.set('set-gender', 'male')
    
    with open('behaviour.xml', 'w', encoding='utf-8') as f:
        f.write(root.serialize())

if __name__ == '__main__':
    main()
