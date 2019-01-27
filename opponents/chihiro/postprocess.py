import behaviour_parser as bp
from ordered_xml import OrderedXMLElement
from pathlib import Path
from PIL import Image
import sys
import shutil

AE_BASE_Y_OFFSET = 275
with Image.open('vfx/AE-overlay.png') as overlay:
    AE_OVERLAY_SIZE = overlay.size

runaway_anim_def = {
    'id': 'base',
    'interpolation': 'spline',
    'keyframes': [
        {'time': 0,   'x':  0},
        {'time': 0.2, 'x': -30},
        {'time': 0.9, 'x': -35},
        {'time': 1.1, 'x': -75},
        {'time': 1.8, 'x': -80},
        {'time': 2.8, 'x': -10080},
    ]
}

def dict_to_directive(in_def):
    animationElem = OrderedXMLElement('directive', init_attrs={
        'id': in_def['id'],
        'type': 'animation',
        'interpolation': in_def['interpolation']
    })
    
    for keyframe in in_def['keyframes']:
        animationElem.subElement('keyframe', init_attrs=keyframe)
        
    return animationElem

def runaway_anim():
    elem = OrderedXMLElement('pose', init_attrs={'id': '4-runaway', 'baseHeight': 1400})
    
    with Image.open('./4-runaway.png') as img:
        baseSprite = elem.subElement('sprite', init_attrs={
            'id': 'base',
            'src': 'chihiro/4-runaway.png',
            'width': img.width, 'height': img.height
        })
        
        elem.append(dict_to_directive(runaway_anim_def))
    
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
            'src': 'chihiro/vfx/AE-overlay.png',
            'width': AE_OVERLAY_SIZE[0], 'height': AE_OVERLAY_SIZE[1],
            'z': 2,
            'y': AE_BASE_Y_OFFSET,
            'scaley': 1.40,
            'scalex': 0.80
        })
        
        laptopSprite = elem.subElement('sprite', init_attrs={
            'id': 'laptop',
            'src': 'chihiro/vfx/laptop.png',
            'z': 1,
            'y': 1050,
            'scaley': 1,
            'scalex': 1
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
                        state_elem.set('img', 'custom:4-runaway')
        elif stage == '5':
            for case_elem in stage_elem.iter('case'):
                tag = case_elem.get('tag')
                if tag in ['must_strip', 'must_strip_normal', 'must_strip_losing', 'must_strip_winning']:
                    for state_elem in case_elem.iter('state'):
                        state_elem.set('set-label', 'Chihiro')
                elif tag == 'stripping':
                    for state_elem in case_elem.iter('state'):
                        state_elem.set('set-gender', 'male')
                elif tag == 'stripped':
                    for state_elem in case_elem.iter('state'):
                        state_elem.set('set-label', 'Alter Ego')
                        
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
