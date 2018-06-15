import lxml.etree as ET
import sys
import shutil

if __name__ == '__main__':
    shutil.copy(sys.argv[1], sys.argv[1]+'.generated')
    
    tree = ET.parse(sys.argv[1])
    tree.write_c14n(sys.argv[2])
