import csv
from pathlib import Path

CSVS_DIR = Path('./csvs')

start_linecount = 1172
block_target = 174
last_progress = 14

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
progress = filled_lines - start_linecount
new_progress = progress - last_progress

print("="*79)
print("    Lines Total:         {:d}".format(total_lines))
print("    Lines Finished:      {:d}".format(filled_lines))
print("    Unique Lines:        {:d}".format(len(unique_lines)))
print("")
print("    Lines Unfilled:      {:d}".format(unfilled_lines))
print("    Current Progress:    {:d}".format(progress))
print("    Progress from Last: {:+d}".format(new_progress))

if block_target is not None and block_target > 0:
    print("    Progress Percentage: {:.1%}".format(progress / block_target))

print("="*79)
