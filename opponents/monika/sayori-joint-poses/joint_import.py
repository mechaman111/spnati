import kkl_import as kkl
from pathlib import Path
import sys
import os

def split_pose(code):
    orig = kkl.KisekaeCode(code)
    return kkl.KisekaeCode(orig[0]), kkl.KisekaeCode(orig[1])
    
with open('./face_juices.txt', 'r', encoding='utf-8') as f:
    juice_code = kkl.KisekaeCode(f.read())
    juice_subcodes = {}
    
    for subcode in juice_code[0]:
        juice_subcodes[subcode.prefix] = subcode
    
    
def apply_juice(dest_char):
    tgt_char = kkl.KisekaeCharacter(dest_char[0])

    for prefix, component in juice_subcodes.items():
        if prefix in tgt_char:
            tgt_char[prefix] = kkl.KisekaeComponent(component)
        else:
            tgt_char.subcodes.append(kkl.KisekaeComponent(component))

    return tgt_char
    
    
if __name__ == "__main__":
    p = Path(sys.argv[1])
    
    if not p.is_file():
        raise FileNotFoundError("{:s} is not a file!".format(str(p)))
    
    main_code = kkl.KisekaeCode(p.read_text(encoding='utf-8'))
    c1, c2 = split_pose(main_code)
    
    outdir = Path(os.getcwd())
    
    kkl.process_single(main_code, outdir.joinpath('{:s}-main.png'.format(p.stem)))
    kkl.process_single(c1, outdir.joinpath('{:s}-c1.png'.format(p.stem)), juice=100)
    kkl.process_single(apply_juice(c2), outdir.joinpath('{:s}-c2.png'.format(p.stem)), juice=100)