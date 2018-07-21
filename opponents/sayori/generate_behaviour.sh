#!/usr/bin/env bash
python.exe ./fetch_sheets.py \
    -s ~/Programming/spnati-client-secrets.json \
    -t ~/Programming/spnati-fetch-token.json \
    -o ./ |& tee convert.log
python.exe ../monika/helper-scripts/convert.py ./csvs ./behaviour.xml |& tee -a convert.log
python.exe ../monika/helper-scripts/check_character.py sayori |& tee -a convert.log
