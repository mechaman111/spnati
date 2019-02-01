import behaviour_parser as bp
from ordered_xml import OrderedXMLElement
from pathlib import Path
from PIL import Image
import sys
import shutil

AE_BASE_Y_OFFSET = 275
with Image.open('vfx/AE-overlay.png') as overlay:
    AE_OVERLAY_SIZE = overlay.size
    
LAPTOP_X_SCALE = 0.70
LAPTOP_Y_SCALE = 1.0

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

runaway_laptop_anim_1 = {
    'id': 'laptop',
    'interpolation': 'none',
    'keyframes': [
        {'time': 0,   'alpha':  0},
        {'time': 1.8, 'alpha':  100},
    ]
}

runaway_laptop_anim_2 = {
    'id': 'laptop',
    'interpolation': 'spline',
    'delay': 1.8,
    'keyframes': [
        {'time': 0,   'x':  -80, 'y': 600,  'rotation': -30},
        {'time': 0.15/2, 'x':  -40, 'y': 450,  'rotation': 10},
        {'time': 0.15, 'x':  0,   'y': 1050, 'rotation': 0},
    ]
}

overlay_flicker_anim = {
    'id': 'overlay',
    'interpolation': 'none',
    'keyframes': [
        {'time': 0,   'alpha':  0},
        {'time': 2.5, 'alpha':  100},
        {'time': 2.75, 'alpha':  0},
        {'time': 3.25, 'alpha':  100},
    ]
}

ae_flicker_anim = {
    'id': 'alter_ego',
    'interpolation': 'none',
    'keyframes': [
        {'time': 0,   'alpha':  0},
        {'time': 3.50, 'alpha':  100},
    ]
}

# Chihiro pose, then AE pose
AE_TANDEM_POSES = {
    'angry': ['angry', 'angry'],
    'base': ['base', 'base'],
    'happy': ['happy', 'relieved'],
    'embarassed': ['embarassed', 'nervous'],
    'nervous': ['nervous', 'embarassed'],
    'excited': ['excited', 'happy'],
    'relieved': ['relieved', 'relieved'],
    'sad': ['sad', 'thinking-2'],
    'shocked': ['shocked', 'angry'],
    'shy': ['shy', 'thinking-1'],
    'thinking-1': ['thinking-1', 'happy'],
    'thinking-2': ['thinking-2', 'angry'],
    'start': ['start', 'embarassed'],
    'heavy-1': ['heavy-1', 'embarassed'],
    'heavy-2': ['heavy-2', 'embarassed'],
    'finish': ['finish', 'shocked'],
}

def dict_to_directive(in_def):
    animationElem = OrderedXMLElement('directive', init_attrs={
        'id': in_def['id'],
        'type': 'animation',
        'interpolation': in_def['interpolation'],
        'delay': in_def.get('delay', 0)
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
            'z': 2,
            'width': img.width, 'height': img.height
        })
        
        laptopSprite = elem.subElement('sprite', init_attrs={
            'id': 'laptop',
            'src': 'chihiro/vfx/laptop.png',
            'z': 1,
            'x': -80,
            'y': 600,
            'rotation': -30,
            'scalex': LAPTOP_X_SCALE,
            'scaley': LAPTOP_Y_SCALE
        })
        
    with Image.open('./5-base.png') as img:
        aeSprite = elem.subElement('sprite', init_attrs={
            'id': 'alter_ego',
            'src': 'chihiro/5-base.png',
            'width': img.width, 'height': img.height,
            'alpha': 0,
            'z': 1,
            'x': 0,
            'y': AE_BASE_Y_OFFSET+12,
            'scale': 1.40
        })
        
        overlaySprite = elem.subElement('sprite', init_attrs={
            'id': 'overlay',
            'src': 'chihiro/vfx/AE-overlay.png',
            'width': AE_OVERLAY_SIZE[0], 'height': AE_OVERLAY_SIZE[1],
            'alpha': 0,
            'z': 2,
            'x': 0,
            'y': AE_BASE_Y_OFFSET,
            'scaley': 1.40,
            'scalex': 0.90
        })
        
    elem.append(dict_to_directive(runaway_anim_def))
    elem.append(dict_to_directive(runaway_laptop_anim_1))
    elem.append(dict_to_directive(runaway_laptop_anim_2))
    elem.append(dict_to_directive(overlay_flicker_anim))
    elem.append(dict_to_directive(ae_flicker_anim))
    
    return elem

def ae_pose(emotion, img_path, x=0):
    elem = OrderedXMLElement('pose', init_attrs={'id': emotion, 'baseHeight': 1400})
    
    with Image.open(img_path) as img:
        baseSprite = elem.subElement('sprite', init_attrs={
            'id': 'alter_ego',
            'src': 'chihiro/'+img_path.name,
            'width': img.width, 'height': img.height,
            'z': 1,
            'x': x,
            'y': AE_BASE_Y_OFFSET+12,
            'scale': 1.40
        })
        
        overlaySprite = elem.subElement('sprite', init_attrs={
            'id': 'overlay',
            'src': 'chihiro/vfx/AE-overlay.png',
            'width': AE_OVERLAY_SIZE[0], 'height': AE_OVERLAY_SIZE[1],
            'z': 2,
            'x': x,
            'y': AE_BASE_Y_OFFSET,
            'scaley': 1.40,
            'scalex': 0.90
        })
        
        laptopSprite = elem.subElement('sprite', init_attrs={
            'id': 'laptop',
            'src': 'chihiro/vfx/laptop.png',
            'z': 1,
            'x': x,
            'y': 1050,
            'scalex': LAPTOP_X_SCALE,
            'scaley': LAPTOP_Y_SCALE
        })
    
    return elem

def ae_tandem_pose(name, pose_img_path, ae_img_path):
    pose = ae_pose(name, ae_img_path, x=150)
    
    with Image.open(pose_img_path) as img:
        chihiroSprite = pose.subElement('sprite', init_attrs={
            'id': 'chihiro',
            'src': 'chihiro/'+pose_img_path.name,
            'width': img.width, 'height': img.height,
            'z': 3,
            'x': -100
        })
        
    return pose

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
            
        if stage == 5 and emotion != 'strip':
            poses_elem.append(ae_pose('5-'+emotion, path))
        elif stage in [6, 7, 8]:
            chi_img, ae_img = AE_TANDEM_POSES[emotion]
            
            chi_img = Path('./{}-{}.png'.format(stage, chi_img))
            ae_img = Path('./5-{}.png'.format(ae_img))

            poses_elem.append(ae_tandem_pose('{}-{}'.format(stage, emotion), chi_img, ae_img))
    
    poses_elem.append(ae_tandem_pose('5-return', Path('./5-return.png'), Path('./5-shocked.png')))        
    poses_elem.append(ae_tandem_pose('5-strip', Path('./5-strip.png'), Path('./5-embarassed.png')))
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
                        state_elem.set('img', 'custom:5-return')
                elif tag == 'stripping':
                    for state_elem in case_elem.iter('state'):
                        state_elem.set('set-gender', 'male')
                        state_elem.set('img', 'custom:5-strip')
                elif tag == 'stripped':
                    for state_elem in case_elem.iter('state'):
                        state_elem.set('set-label', 'Alter Ego')
        
        if stage in ['5', '6', '7', '8']:            
            for case_elem in stage_elem.iter('case'):
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
