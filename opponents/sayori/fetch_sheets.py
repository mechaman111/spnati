import csv
from pathlib import Path
from apiclient.discovery import build
from httplib2 import Http
from oauth2client import file, client, tools
import argparse
import logging

parser = argparse.ArgumentParser(parents=[tools.argparser])
flags = parser.parse_args()

# Setup the Sheets API
SCOPES = 'https://www.googleapis.com/auth/spreadsheets.readonly'
store = file.Storage('token.json')
creds = store.get()
if not creds or creds.invalid:
    flow = client.flow_from_clientsecrets('client_id.json', SCOPES)
    creds = tools.run_flow(flow, store, flags)
service = build('sheets', 'v4', http=creds.authorize(Http()))

SPREADSHEET_ID = "1kQ--XoQjA6yRqfpDgko7Q-vviu6rs6dE0E0wH-wGt1M"
def get_data(sheet_name, sheet_range='A1:Z900'):
    logging.info("Fetching sheet: {:s}...".format(sheet_name))
    
    full_range = "'{:s}'!{:s}".format(sheet_name, sheet_range)
    result = service.spreadsheets().values().get(spreadsheetId=SPREADSHEET_ID,
                                                 range=full_range).execute()
    return result.get('values', None)

OUT_DIR = Path('.')

# Maps sheets to filenames
ALL_SHEETS = {
    'Meta Info and Start Lines': OUT_DIR / 'csvs' / '0-meta.csv',
    'Stages 0-3': OUT_DIR / 'csvs' / '1-s0-s3.csv',
    'Stages 4-5': OUT_DIR / 'csvs' / '2-s4-s5.csv',
    'Stages 6-7': OUT_DIR / 'csvs' / '3-s6-s7.csv',
    'Stage 8': OUT_DIR / 'csvs' / '4-s8.csv',
    'Stage 9': OUT_DIR / 'csvs' / '5-s9.csv',
    'Poses': OUT_DIR / 'poses.csv'
}

for sheet_name, output_path in ALL_SHEETS.items():
    result = get_data(sheet_name)
    if result:
        with output_path.open('w', encoding='utf-8', newline='') as f:
            writer = csv.writer(f)
            writer.writerows(result)
    else:
        logging.error("Got no data for sheet {:s}".format(sheet_name))
