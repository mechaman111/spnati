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
result = service.spreadsheets().get(spreadsheetId=SPREADSHEET_ID).execute()
for sheet in result['sheets']:
    title = sheet['properties']['title']
    index = sheet['properties']['index']
    gid = sheet['properties']['sheetId']
    if 'archived' in title.lower():
        continue
        
    print("{:d}: {:s}: {:d}".format(index, title, gid))
