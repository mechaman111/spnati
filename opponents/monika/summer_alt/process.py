from PIL import Image
import kkl_import as kkl
from pathlib import Path
import sys
import csv

POSE_COMPONENTS = [
    'aa',
    'ab',
    'ac',
    'ad',
    'ae',
    'ba',
    'bb',
    'bc',
    'bd',
    'be',
    'bf',
    'bg',
    'bh',
    'ca',
    'db',
    'dc',
    'dd',
    'dh',
    'di',
    't01',
    'ga',
    'gb',
    'gc',
    'gd',
    'ge',
    'gh',
    'gf',
    'gg',
    'ha',
    'hb',
    'hc',
    'hd'
]

BASE_BLUSH = [0, 0, 0, 0, 25, 35, 45, 55]

COMPONENT_BASE_DIR = Path('./components')
OUTPUT_DIR = Path('./out')

with open('./base_code.txt', 'r', encoding='utf-8') as f:
    BASE_CODE = f.read()
    BASE_MODEL = kkl.KisekaeCode(BASE_CODE)

hair_down_subcodes = {}
with open('./hair_down_code.txt', 'r', encoding='utf-8') as f:
    HAIR_DOWN_CODE = f.read()
    HAIR_DOWN_MODEL = kkl.KisekaeCode(HAIR_DOWN_CODE)

    for subcode in HAIR_DOWN_MODEL[0].subcodes:
        hair_down_subcodes[subcode.prefix] = subcode

def remove_subcodes(char, prefixes):
    tgt_char = kkl.KisekaeCharacter(char)

    subcodes = []
    for subcode in char:
        if subcode.prefix not in prefixes:
            continue

        subcodes.append(subcode)

    for subcode in subcodes:
        tgt_char.subcodes.remove(subcode)

    return tgt_char

def apply_hair_down(dest_char):
    tgt_char = kkl.KisekaeCharacter(dest_char)

    for prefix, component in hair_down_subcodes.items():
        if prefix in tgt_char:
            tgt_char[prefix] = kkl.KisekaeComponent(component)
        else:
            tgt_char.subcodes.append(kkl.KisekaeComponent(component))
    
    del tgt_char['r03']
    del tgt_char['m00']
    del tgt_char['m01']

    tgt_char.subcodes.append(kkl.KisekaeComponent('m00'))

    return tgt_char

def apply_pose_components(dest_char, pose_char):
    tgt_char = kkl.KisekaeCharacter(dest_char)

    found_t01 = False

    for subcode in pose_char:
        if subcode.prefix not in POSE_COMPONENTS:
            continue

        if subcode.prefix == 't01':
            found_t01 = True

        # comp = kkl.KisekaeComponent(subcode)
        # tgt_char.subcodes.append(comp)
        for tgt_subcode in tgt_char.subcodes:
            if tgt_subcode.prefix == subcode.prefix:
                dest_list = tgt_subcode.attributes.copy()

                if len(dest_list) < len(subcode.attributes):
                    for i in range(len(dest_list), len(subcode.attributes)):
                        dest_list.append('0')

                for i, a in enumerate(subcode.attributes):
                    dest_list[i] = a

                tgt_subcode.attributes = dest_list
                break
        else:
            comp = kkl.KisekaeComponent(subcode)
            tgt_char.subcodes.append(comp)

    if not found_t01:
        tgt_char = remove_subcodes(tgt_char, ['t01'])
        tgt_char.subcodes.append(kkl.KisekaeComponent('t01'))

    return tgt_char

def wipe_subcodes(char, prefixes):
    tgt_char = kkl.KisekaeCharacter(char)

    subcodes = []
    for subcode in tgt_char:
        if subcode.prefix not in prefixes:
            continue

        subcode.attributes = []

    return tgt_char

def process_pose(pose, pose_name, out_path, component_order, with_flower, with_shoes, with_glasses, with_bra, with_panties, hair_down, **kwargs):
    if not isinstance(pose, kkl.KisekaeCharacter):
        code = kkl.KisekaeCode(pose)
        pose = code[0]

    kkl.setup_kkl_scene()
    out_path = Path(out_path).resolve()

    for idx, submodel in enumerate(BASE_MODEL.characters):
        if idx not in component_order:
            continue

        dest_path = out_path.joinpath(str(idx), pose_name+'.png')

        if not dest_path.parent.is_dir():
            dest_path.parent.mkdir(parents=True)

        adjusted_char = apply_pose_components(submodel, pose)

        if not with_flower:
            adjusted_char = remove_subcodes(adjusted_char, ['m02', 'm03'])

        if not with_shoes:
            adjusted_char = wipe_subcodes(adjusted_char, ['jd', 'je'])

        if not with_glasses:
            if idx == 1:
                c = []
                for i in range(59, 72):
                    c.append('s'+str(i))
                adjusted_char = remove_subcodes(adjusted_char, c)

        if not with_bra:
            adjusted_char = wipe_subcodes(adjusted_char, ['ka'])
        
        if not with_panties:
            adjusted_char = wipe_subcodes(adjusted_char, ['kb'])

        if hair_down:
            adjusted_char = apply_hair_down(adjusted_char)

        adjusted_code = kkl.KisekaeCode(adjusted_char)

        kkl.process_single(
            adjusted_code,
            dest_path,
            do_setup=False,
            auto_crop=False,
            width=900,
            height=1400,
            center_x=1000,
            margin_y=50,
            **kwargs
        )

def load_csv(fname):
    with open(fname, 'r', encoding='utf-8', newline='') as f:
        reader = csv.reader(f)
        next(f)

        codes = {}

        for row in reader:
            try:
                stage = int(row[0])
                clip_y = int(row[1])
            except ValueError:
                continue
            
            if stage not in codes:
                codes[stage] = {}

            pose_name = row[2]
            codes[stage][pose_name] = (650, row[3])

        return codes

def clip_by_y(img, clip_start, clip_end):
    dest = Image.new('RGBA', img.size)

    cropped = img.crop((0, clip_start, img.width, clip_end))
    dest.paste(cropped, (0, clip_start))

    return dest

def composite_pose(pose_name, div_y, composite_order):
    dest = Image.new('RGBA', (800, 1400))
    
    for idx in composite_order:
        comp = COMPONENT_BASE_DIR.joinpath(str(idx), pose_name+'.png')
        with Image.open(comp) as img:
            component_img = img
            if idx == 1:
                component_img = clip_by_y(img, 0, div_y)
            elif idx == 2:
                component_img = clip_by_y(img, div_y, img.height)
            
            dest.alpha_composite(component_img)

    crop_box = kkl.auto_crop_box(dest)
    cropped_dest = dest.crop(crop_box)

    cropped_dest.save(OUTPUT_DIR.joinpath(pose_name+'.png'))

if __name__ == '__main__':
    codes = load_csv(sys.argv[1])
    
    for arg in sys.argv[2:]:
        poses = []
        if arg.isdigit():
            # get all poses in stage
            arg = int(arg)
            for pose_name, data in codes[arg].items():
                poses.append((str(arg)+'-'+pose_name, arg, *data))
        elif '-' in arg:
            stage, pose_name = arg.split('-', maxsplit=1)
            stage = int(stage)
            poses.append((arg, stage, *codes[stage][pose_name]))
        else:
            for stage, stage_codes in codes.items():
                for pose_name, data in stage_codes.items():
                    if pose_name == arg:
                        poses.append((str(stage)+'-'+pose_name, stage, *data))
        
        for out_name, stage, clip_y, code in poses:
            with_flower  = (stage == 0)
            with_glasses = (stage < 3)
            with_shoes   = (stage < 2)
            hair_down    = (stage >= 6)
            with_bra     = (stage < 7)
            with_panties = (stage < 8)
            
            if stage < 4:
                composite_order = [3, 0, 1, 2]
            elif stage == 4:
                composite_order = [3, 0, 2]
            elif stage >= 5:
                composite_order = [0]

            blush = BASE_BLUSH[stage]

            if 'shy' in out_name:
                blush += 10
            elif 'horny' in out_name:
                blush += 15

            if out_name == '2-removed-glasses':
                with_glasses = False

            print("Processing: "+out_name+'...')

            process_pose(
                code,
                out_name,
                COMPONENT_BASE_DIR,
                composite_order,
                with_flower,
                with_shoes,
                with_glasses,
                with_bra,
                with_panties,
                hair_down,
                blush=blush
            )

            composite_pose(out_name, clip_y, composite_order)