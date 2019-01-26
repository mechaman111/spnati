import behaviour_parser as bp
from ordered_xml import OrderedXMLElement
from pathlib import Path
from PIL import Image
import sys
import shutil

AE_BASE_Y_OFFSET = 275
with Image.open('AE-overlay.png') as overlay:
    AE_OVERLAY_SIZE = overlay.size

def runaway_anim():
    elem = OrderedXMLElement('pose', init_attrs={'id': '4-runaway', 'baseHeight': 1400})
    
    with Image.open('./4-runaway.png') as img:
        baseSprite = elem.subElement('sprite', init_attrs={
            'id': 'base',
            'src': 'chihiro/4-runaway.png',
            'width': img.width, 'height': img.height
        })
        
        animationElem = elem.subElement('directive', init_attrs={
            'id': 'base',
            'type': 'animation',
            'interpolation': 'linear'
        })
        
        animationElem.subElement('keyframe', init_attrs={
            'time': 0,
            'x': 0
        })
        
        animationElem.subElement('keyframe', init_attrs={
            'time': 0.75,
            'x': -300
        })
        
        animationElem.subElement('keyframe', init_attrs={
            'time': 1.25,
            'x': -300
        })
        
        animationElem.subElement('keyframe', init_attrs={
            'time': 1.25+1.0,
            'x': -10300
        })
    
    return elem

def ae_pose(emotion, img_path):
    elem = OrderedXMLElement('pose', init_attrs={'id': '5-'+emotion, 'baseHeight': 1400})
    
    with Image.open(img_path) as img:
        baseSprite = elem.subElement('sprite', init_attrs={
            'id': 'base',
            'src': 'chihiro/'+img_path.name,
            'width': img.width, 'height': img.height,
            'z': 1,
            'y': AE_BASE_Y_OFFSET+12,
            'scale': 1.40
        })
        
        overlaySprite = elem.subElement('sprite', init_attrs={
            'id': 'overlay',
            'src': 'chihiro/AE-overlay.png',
            'width': AE_OVERLAY_SIZE[0], 'height': AE_OVERLAY_SIZE[1],
            'z': 2,
            'y': AE_BASE_Y_OFFSET,
            'scaley': 1.40,
            'scalex': 0.80
        })
    
    return elem

def main():
    shutil.copyfile('./behaviour.xml', './behaviour.xml.bak')
    root = bp.parse_file('./behaviour.xml')
    
    poses_elem = root.find('poses')
    poses_elem.children = []
    
    for path in filter(lambda p: p.suffix.lower() == '.png', Path('./').iterdir()):
        stage, emotion = path.stem.split('-', 1)
        
        try:
            stage = int(stage)
        except ValueError:
            continue
            
        if stage == 5:
            poses_elem.append(ae_pose(emotion, path))
            
    poses_elem.append(runaway_anim())
    
    for stage_elem in root.find('behaviour').iter('stage'):
        stage = stage_elem.get('id')
        
        if stage == '4':
            for case_elem in stage_elem.iter('case'):
                tag = case_elem.get('tag')
                if tag == 'stripping':
                    for state_elem in case_elem.iter('state'):
                        state_elem.set('set-intelligence', 'good')
                        state_elem.set('set-label', 'Alter Ego')
                        state_elem.set('img', 'custom:4-runaway')
        elif stage == '5':
            for case_elem in stage_elem.iter('case'):
                tag = case_elem.get('tag')
                if tag in ['must_strip', 'must_strip_normal', 'must_strip_losing', 'must_strip_winning']:
                    for state_elem in case_elem.iter('state'):
                        state_elem.set('set-gender', 'male')
                        state_elem.set('set-label', 'Chihiro')
                        
                for state_elem in case_elem.iter('state'):
                    try:
                        img = state_elem.get('img')
                        base, suffix = img.split('.', 1)
                        state_elem.set('img', 'custom:{}'.format(base))
                    except ValueError:
                        print("Already adjusted pose name: {}".format(img))
    
    with open('behaviour.xml', 'w', encoding='utf-8') as f:
        f.write(root.serialize())

if __name__ == '__main__':
    main()
