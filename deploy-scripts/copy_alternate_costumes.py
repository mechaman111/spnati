#!/usr/bin/env python3
from __future__ import print_function

import xml.etree.ElementTree as ET
import shutil as sh
import sys
import os
import os.path as osp


def main():
    base_dir = sys.argv[1]
    copyfrom_src = sys.argv[2]
    event_set = sys.argv[3]
    
    listing_xml = ET.parse(osp.join(base_dir, 'opponents', 'listing.xml'))
    
    # contains tuples of (character ID, costume folder)
    costumes = []
    opp_stage_counts = {}
    delete_stages = {}
    
    for opp in listing_xml.find('individuals').iter('opponent'):
        if 'status' not in opp.attrib or opp.attrib['status'] == "testing":
            meta_xml = ET.parse(osp.join(base_dir, 'opponents',  opp.text, 'meta.xml'))
            for alts in meta_xml.iter('alternates'):               
                n_stages = int(meta_xml.find('layers').text) + 2
                
                # find any costumes in the specified event set
                for costume in alts.iter('costume'):
                    if event_set == 'all' or costume.get('set', None) == event_set:
                        costumes.append((opp.text, costume.get('folder')))
                        delete_stages[opp.text] = list(range(n_stages))
                        opp_stage_counts[opp.text] = n_stages
    
    #for opp_id, costume_folder in costumes:
    #    opp_base_folder = 'opponents/'+opp_id+'/'
    #    costume_xml = ET.parse(osp.join(costume_folder, 'costume.xml'))
    #    
    #    # Read all folders referenced by the costume (sorted by stage)
    #    folders = []
    #    for folder in costume_xml.iter('folder'):
    #        folder_stage = int(folder.get('stage', 0))
    #        folders.append((folder_stage, folder.text))
    #    folders = sorted(folders, key=lambda t: t[0])
        
    #    # Figure out which costume stages reference the base folder.
    #    # Remove those from delete_stages.
    #    remove_set = set()
    #    for stage in delete_stages[opp_id]:
    #        best_path = folders[0][1]
    #        for folder_stage, folder_path in folders:
    #            if folder_stage > stage:
    #                break
                    
    #            best_path = folder_path
    #        
    #        if best_path == opp_base_folder:
    #            remove_set.add(stage)
        
    #    for stage in remove_set:
    #        delete_stages[opp_id].remove(stage)
            
    # Make the destination path for reskins.
    os.mkdir(osp.join(base_dir, 'opponents', 'reskins'))
    
    # Copy over all event costumes.
    for opp_id, costume_folder in costumes:
        try:
            src = osp.join(copyfrom_src, costume_folder)
            dst = osp.join(base_dir, costume_folder)
            print("Copying: {} --> {}".format(src, dst))
            
            sh.copytree(src, dst)
        except FileNotFoundError:
            print("Could not copy: {}".format(src))

    # Delete previously-determined stages in base folders:
    #for opp_id, stages in delete_stages.items():
    #    if opp_id == 'chiaki':
    #        continue
        
    #    print("Deleting {} stages {}...".format(opp_id, str(stages)))
        
    #    n_deleted = 0
    #    for fname in os.listdir(osp.join(base_dir, 'opponents', opp_id)):
    #        posename = osp.splitext(fname)[0]
            
    #        try:
    #            stage = int(posename.split('-')[0])
    #        except ValueError:
    #            continue
                
    #        if stage in stages:
    #            os.unlink(osp.join(base_dir, 'opponents', opp_id, fname))
    #            n_deleted += 1
        
    #    print("Deleted {} images from {}.".format(n_deleted, opp_id))

if __name__ == '__main__':
    main()
