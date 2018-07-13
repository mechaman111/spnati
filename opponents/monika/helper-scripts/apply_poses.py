import sys

import kkl_import as kkl

POSE_COMPONENTS = ['aa','ab','ac','ba','bc','bb','bd','be']

def apply_posing(pose_src, body_src):
    out = kkl.KisekaeCharacter(body_src)
    
    for subcode in filter(lambda sc: sc.id in POSE_COMPONENTS, pose_src.subcodes):
        out.find(subcode.id).attributes = subcode.attributes.copy()
        
    return out
    
if __name__ == '__main__':
    body_src = kkl.KisekaeCode(sys.argv[1]).characters[0]
    
    with open(sys.argv[2], 'r') as infile:
        with open(sys.argv[3], 'w') as outfile:
            for line in infile:
                current = kkl.KisekaeCode(line)
                current.scene = None
                current.version = 68
                
                out_char = apply_posing(current.characters[0], body_src)
                current.characters[0] = out_char
                
                outfile.write(str(current)+'\n')
