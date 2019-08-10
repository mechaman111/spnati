import kkl_import as kkl
from pathlib import Path
import sys
import os

def split_pose(code):
    orig = kkl.KisekaeCode(code)
    return kkl.KisekaeCode(orig[0]), kkl.KisekaeCode(orig[1])
    
if __name__ == "__main__":
    p = Path(sys.argv[1])
    
    if not p.is_file():
        raise FileNotFoundError("{:s} is not a file!".format(str(p)))
    
    main_code = kkl.KisekaeCode(p.read_text(encoding='utf-8'))
    c1, c2 = split_pose(main_code)
    
    outdir = Path(os.getcwd())
    
    kkl.process_single(main_code, outdir.joinpath('{:s}-main.png'.format(p.stem)))
    kkl.process_single(c1, outdir.joinpath('{:s}-c1.png'.format(p.stem)), juice=100)
    kkl.process_single(c2, outdir.joinpath('{:s}-c2.png'.format(p.stem)), juice=100)