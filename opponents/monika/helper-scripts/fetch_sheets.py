import csv
from pathlib import Path
from apiclient.discovery import build
from httplib2 import Http
from oauth2client import file, client, tools
import argparse
import logging
import functools

parser = argparse.ArgumentParser(parents=[tools.argparser])
parser.add_argument('--secrets-file', '-s', default='~/Programming/spnati-client-secrets.json', help='Path to the secrets file.')
parser.add_argument('--store-file', '-t', default='~/Programming/spnati-fetch-token.json', help='Path to store the client token under.')
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

POSE_SPREADSHEET = "1O_gI55dVlY5ge047NHXbN6QVM_Pdw8QlI4Vkzams3B0"
DIALOGUE_SPREADSHEET = "1Z-1Q88bV1-OpbhkVOe7Dk9LmZYwY8sPaAR04mQ6BPLA"

@functools.lru_cache()
def get_all_sheet_info(spreadsheet_id):
    sheet_info = service.spreadsheets().get(spreadsheetId=spreadsheet_id).execute()
    return sheet_info.get('sheets', [])

def get_data(spreadsheet_id, sheet_name):
    sheet_info = get_all_sheet_info(spreadsheet_id)
    full_range = None
    
    for sheet in sheet_info:
        title = sheet['properties']['title']
        n_rows = sheet['properties']['gridProperties']['rowCount']
        
        if title == sheet_name:
            full_range = "'{:s}'!A1:Z{:d}".format(sheet_name, n_rows)
            print("Grabbing {:d} rows from {:s}...".format(n_rows, sheet_name))
            break
    else:
        raise FileNotFoundError("Could not find sheet {:s} in spreadsheet!".format(sheet_name))
    
    result = service.spreadsheets().values().get(spreadsheetId=spreadsheet_id,
                                                 range=full_range).execute()
    return result.get('values', None)

OUT_DIR = Path(flags.out_dir)

# Maps sheets to filenames
ALL_SHEETS = {
    (POSE_SPREADSHEET, 'poses.csv',): OUT_DIR / 'poses.csv',
    (DIALOGUE_SPREADSHEET, 'behaviour.csv',): OUT_DIR / 'behaviour.csv',
}

for sheet_tup, output_path in ALL_SHEETS.items():
    sheet_id, sheet_name = sheet_tup
    result = get_data(sheet_id, sheet_name)
    if result:
        with output_path.open('w', encoding='utf-8', newline='') as f:
            writer = csv.writer(f)
            writer.writerows(result)
    else:
        logging.error("Got no data for sheet {:s}".format(sheet_name))
