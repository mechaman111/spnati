from __future__ import print_function

import os
import os.path as osp
import subprocess as sp
import sys
from bs4 import BeautifulSoup

def process(opponent_folder_path: str, original_path: str):
    meta_path = osp.join(opponent_folder_path, 'meta.xml')
    
    if not osp.exists(meta_path):
        return
    
    with open(meta_path, 'r', encoding='utf-8') as f:
        meta_soup = BeautifulSoup(f.read(), features="html.parser")

    # Passed options:
    # "-n 1"           : limit to 1 commit
    # "--pretty=..."   : get UNIX timestamp only
    # "--first-parent" : Don't follow branches merged _into_ this branch
    # "--show-pulls"   : Treat merges as pulling changes from other branches
    #                    (see man git-rev-list for more details)
    proc = sp.run(
        ["git", "log", "-n", "1", "--pretty=format:%ct", "--first-parent", "--", "--show-pulls", original_path],
        stdout=sp.PIPE, encoding="utf-8", check=True, timeout=15
    )

    # Technically, we don't _have_ to do this, and could just append "000" to
    # the end of the stdout string. But _just to be sure_, this validates the
    # output from git log.
    try:
        ts = int(proc.stdout) * 1000
    except ValueError:
        sys.stderr.write("Could not find last-update timestamp for " + opponent_folder_path + "\n")
        sys.stderr.flush()
        return
    
    # Always pull times from Git when deploying to production, to keep things
    # consistent.
    cur_tag = meta_soup.opponent.lastupdate
    if cur_tag is None:
        update_tag = meta_soup.new_tag('lastupdate')
        update_tag.string = str(ts)
        meta_soup.opponent.append(update_tag)
    else:
        cur_tag.string = str(ts)
    
    with open(meta_path, 'w', encoding='utf-8') as f:
        f.write(str(meta_soup))

if __name__ == "__main__":
    target_path = sys.argv[1]
    timestamp_src = sys.argv[2]

    for name in os.listdir(target_path):
        opponent_path = osp.join(target_path, name)
        orig_path = osp.join(timestamp_src, name)

        if osp.isdir(opponent_path) and osp.isdir(orig_path):
            process(opponent_path, orig_path)
