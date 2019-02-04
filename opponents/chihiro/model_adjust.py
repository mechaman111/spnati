import kkl_import as kkl
import csv
import sys

#       0 1 2 3   4  5  6  7 8  9 10 11
#85**ca68.1.0.105.0.-20.25.0.30.0.60.57
modifications = {
    'ca': {
        11: 57
    },
}

def modify_code(in_code):
    char = kkl.KisekaeCode(in_code)[0]
    for subcode in char.subcodes:
        if subcode.prefix not in modifications:
            continue
        
        for idx, value in modifications[subcode.prefix].items():
            if idx < len(subcode.attributes):
                for i in range(len(subcode.attributes), idx+1):
                    value.append('0')
                    
            subcode[idx] = value
    
    return kkl.KisekaeCode(char)
    
if __name__ == "__main__":
    infile = sys.argv[1]
    outfile = sys.argv[2]
    
    with open(infile, 'r', encoding='utf-8', newline='') as inf:
        with open(outfile, 'w', encoding='utf-8', newline='') as outf:
            reader = csv.DictReader(inf)
            writer = None
            
            for row in reader:
                if writer is None:
                    writer = csv.DictWriter(outf, reader.fieldnames)
                    writer.writeheader()
                
                if 'code' in row and row['code'] is not None and len(row['code']) > 0:
                    incode = row['code']
                    outcode = str(modify_code(incode))
                    
                    row['code'] = outcode
                
                writer.writerow(row)
            
            
    
