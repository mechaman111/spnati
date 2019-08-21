#!/bin/bash
git add opponents/$1
python tools/character_update_commit_msg.py $1 | git commit -eF -