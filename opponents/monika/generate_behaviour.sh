#!/usr/bin/env bash
python.exe ./helper-scripts/fetch_sheets.py \
    -s ~/Programming/spnati-client-secrets.json \
    -t ~/Programming/spnati-fetch-token.json \
    -o ./ |& tee convert.log
    
python.exe ./helper-scripts/convert.py ./csvs ./behaviour.xml |& tee convert.log
