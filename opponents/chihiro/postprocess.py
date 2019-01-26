import behaviour_parser as bp
from ordered_xml import OrderedXMLElement
from pathlib import Path
from PIL import Image
import sys
import shutil

with Image.open('AE-overlay.png') as overlay:
    AE_OVERLAY_SIZE = overlay.size

def ae_pose(emotion, img_path):
    elem = OrderedXMLElement('pose', id='5-'+emotion, baseHeight=1400)
    
    with Image.open(img_path) as img:
        baseSprite = elem.subElement('sprite', src=img_path.name, width=img.width, height=img.height)
        overlaySprite = elem.subElement('sprite', src='AE-overlay.png', width=AE_OVERLAY_SIZE[0], height=AE_OVERLAY_SIZE[1])
    
    return elem

def main():
    shutil.copyfile('./behaviour.xml', './behaviour.xml.bak')
    root = bp.parse_file('./behaviour.xml')
    
    poses_elem = root.find('poses')
    for path in filter(lambda p: p.suffix.lower() == '.png', Path('./').iterdir()):
        stage, emotion = path.stem.split('-', 1)
        
        try:
            stage = int(stage)
        except ValueError:
            continue
            
        if stage == 5:
            poses_elem.append(ae_pose(emotion, p))
    
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
