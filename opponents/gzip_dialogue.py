from __future__ import print_function

import os
import glob
import gzip
import shutil
import stat
import sys

def do_gzip(path):
    original_stat = os.stat(path)
    
    with open(path, 'rb') as f_in:
        with gzip.open(path+'.gz', 'wb') as f_out:
            shutil.copyfileobj(f_in, f_out)
            
    new_stat = os.stat(path+'.gz')
    
    in_size = original_stat.st_size
    out_size = new_stat.st_size
    
    os.remove(path)
    
    return in_size, out_size

in_sizes = []
out_sizes = []

for arg in sys.argv[1:]:
    for path in glob.iglob(arg):
        in_size, out_size = do_gzip(path)
        print("Compressed {:s}: {:d} bytes --> {:d} bytes".format(path, in_size, out_size))
        
        in_sizes.append(in_size)
        out_sizes.append(out_size)
        
total_in_size = sum(in_sizes)
total_out_size = sum(out_sizes)

print("="*80)
print("Size before compression: {:d} bytes".format(total_in_size))
print("Size after compression: {:d} bytes".format(total_out_size))
print("Difference: {:d} bytes".format(total_in_size - total_out_size))
print("="*80)
