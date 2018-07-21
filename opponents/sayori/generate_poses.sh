#!/usr/bin/env bash
winpty python.exe ./fetch_sheets.py \
    -s ~/Programming/spnati-client-secrets.json \
    -t ~/Programming/spnati-fetch-token.json \
    -o ./
winpty python.exe ../monika/helper-scripts/kkl_import.py -c ./poses.csv ./
