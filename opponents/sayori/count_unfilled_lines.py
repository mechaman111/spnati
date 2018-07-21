import csv
from pathlib import Path

CSVS_DIR = Path('./csvs')

block_target = 896

total_lines = 0
filled_lines = 0
unique_lines = set()

def count_lines(file):
    total = 0
    filled = 0
    unique_set = set()
    
    with file.open('r', encoding='utf-8', newline='') as f:
        reader = csv.DictReader(f, restval='')
        
        for i, row in enumerate(reader):
            if len(row['stage'].strip()) <= 0 or row['stage'].strip() == 'meta':
                continue
                
            total += 1
            
            if len(row['image']) > 0 and len(row['text']) > 0:
                filled += 1
                unique_set.add(row['text'])
            
    return total, filled, unique_set

for file in filter(lambda p: p.suffix == '.csv', CSVS_DIR.iterdir()):
    t, f, u = count_lines(file)
    
    total_lines += t
    filled_lines += f
    unique_lines.update(u)
    

unfilled_lines = total_lines - filled_lines
progress = block_target - unfilled_lines

print("==============================")
print("    Lines Total: {:d}".format(total_lines))
print("    Lines Finished: {:d}".format(filled_lines))
print("    Unique Lines: {:d}".format(len(unique_lines)))
print("")
print("    Left to Go: {:d}".format(unfilled_lines))
print("    Current Progress: {:d}".format(progress))
print("==============================")
