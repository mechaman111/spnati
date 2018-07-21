#!/usr/bin/env bash
winpty python.exe ./fetch_sheets.py 
winpty python.exe ../monika/helper-scripts/kkl_import.py -c ./poses.csv ./
