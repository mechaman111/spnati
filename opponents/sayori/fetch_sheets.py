import csv
from pathlib import Path
from apiclient.discovery import build
from httplib2 import Http
from oauth2client import file, client, tools
import argparse
import logging
import functools

parser = argparse.ArgumentParser(parents=[tools.argparser])
parser.add_argument('--secrets-file', '-s', default='client_id.json', help='Path to the secrets file.')
parser.add_argument('--store-file', '-t', default='token.json', help='Path to store the client token under.')
parser.add_argument('--out-dir', '-o', default='.', help='Relative output path to store fetched files under.')
flags = parser.parse_args()

# Setup the Sheets API
SCOPES = 'https://www.googleapis.com/auth/spreadsheets.readonly'
store = file.Storage(flags.store_file)
creds = store.get()
if not creds or creds.invalid:
    flow = client.flow_from_clientsecrets(flags.secrets_file, SCOPES)
    creds = tools.run_flow(flow, store, flags)
service = build('sheets', 'v4', http=creds.authorize(Http()))

SPREADSHEET_ID = "1kQ--XoQjA6yRqfpDgko7Q-vviu6rs6dE0E0wH-wGt1M"

@functools.lru_cache()
def get_all_sheet_info():
    sheet_info = service.spreadsheets().get(spreadsheetId=SPREADSHEET_ID).execute()
    return sheet_info.get('sheets', [])

def get_data(sheet_name, sheet_range='A1:Z900'):
    sheet_info = get_all_sheet_info()
    full_range = None
    
    for sheet in sheet_info:
        title = sheet['properties']['title']
        n_rows = sheet['properties']['gridProperties']['rowCount']
        
        if title == sheet_name:
            full_range = "'{:s}'!A1:Z{:d}".format(sheet_name, n_rows)
            logging.info("Grabbing {:d} rows from {:s}...".format(n_rows, sheet_name))
            break
    else:
        raise FileNotFoundError("Could not find sheet {:s} in spreadsheet!".format(sheet_name))
    
    full_range = "'{:s}'!{:s}".format(sheet_name, sheet_range)
    result = service.spreadsheets().values().get(spreadsheetId=SPREADSHEET_ID,
                                                 range=full_range).execute()
    return result.get('values', None)

OUT_DIR = Path(flags.out_dir)

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
