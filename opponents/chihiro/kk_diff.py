import kkl_import as kkl
import csv
import sys

def character_diff(old_char, new_char):
    components_old = set([subcode.prefix for subcode in old_char.subcodes])
    components_new = set([subcode.prefix for subcode in new_char.subcodes])
    
    diff = []
    
    added_components = components_new - components_old
    common_components = components_old & components_new
    
    for subcode in new_char.subcodes:
        if subcode.prefix in added_components:
            for idx, attribute in enumerate(subcode.attributes):
                diff.append(['+', subcode.prefix, idx, attribute])
        else: # must be a common component
            old_subcode = old_char.find(subcode.prefix)
            for idx, attribute in enumerate(subcode.attributes):
                if len(old_subcode) <= idx:
                    diff.append(['+', subcode.prefix, idx, attribute])
                elif old_subcode[idx] != attribute:
                    diff.append(['=', subcode.prefix, idx, old_subcode[idx], attribute])
                    
            if len(subcode) < len(old_subcode):
                for idx in range(len(subcode), len(old_subcode)):
                    diff.append(['-', subcode.prefix, idx, old_subcode[idx]])
                    
    deleted_components = components_old - components_new
    for prefix in deleted_components:
        diff.append(['-', prefix, -1])
        
    return diff
    
if __name__ == "__main__":
    oldfile = sys.argv[1]
    newfile = sys.argv[2]
    outfile = sys.argv[3]
    
    diff = []
    
    with open(oldfile, 'r', encoding='utf-8') as oldf:
        with open(newfile, 'r', encoding='utf-8') as newf:
            old_code = kkl.KisekaeCode(oldf.read())
            new_code = kkl.KisekaeCode(newf.read())
            idx = 0
            
            for char1, char2 in zip(old_code.characters, new_code.characters):
                char_diff = character_diff(char1, char2)
                
                for entry in char_diff:
                    entry.insert(0, idx)
                    diff.append(entry)
                
                idx += 1
                
    if outfile == '-':
        outfile = sys.stdout
    else:
        outfile = open(outfile, 'w', encoding='utf-8')
        
    for entry in diff:
        char_idx = entry[0]
        op = entry[1]
        prefix = entry[2]
        idx = entry[3]
        data = ','.join(entry[4:])
        
        outfile.write('{}: {} {} {} {}\n'.format(
            char_idx, op, prefix, idx, data
        ))
