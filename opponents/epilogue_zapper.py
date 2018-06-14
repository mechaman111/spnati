import os
import os.path as osp
import sys

if sys.version_info[0] < 3:
    reload(sys)
    sys.setdefaultencoding('UTF8')
    
import behaviour_parser as bp
import io

if __name__ == '__main__':
    opponents_dir = os.getcwd()
    if len(sys.argv) > 1:
        opponents_dir = osp.abspath(sys.argv[1])
    
    for child in os.listdir(os.getcwd()):
        behaviour_file = osp.join(child, 'behaviour.xml')
        opp_id = osp.basename(child)
        if osp.exists(behaviour_file):
            try:
                print("Processing: "+opp_id)
                tree = bp.parse_file(behaviour_file)
                
                epilogues = list(tree.iter('epilogue'))
                epilogue_images = []
                
                for epilogue in epilogues:
                    for screen in epilogue.iter('screen'):
                        img_path = osp.join(child, screen.get('img'))
                        if osp.exists(img_path) and osp.isfile(img_path):
                            epilogue_images.append(img_path)
                    
                    tree.children.remove(epilogue)
                
                for fn in epilogue_images:
                    try:
                        os.remove(fn)
                        print("Removed file: "+str(fn))
                    except OSError as e:
                        print("Could not remove file "+str(fn)+": "+str(e))
                
                with io.open(behaviour_file, 'w', encoding='utf-8') as f:
                    f.write(tree.serialize().decode('utf-8'))
                    
            except bp.ParseError as e:
                print("Encountered parse error: "+str(e))
