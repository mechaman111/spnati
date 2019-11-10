import os
import os.path
import sys
import requests

cached_extensions = [
    "bmp",
    "ejs",
    "jpeg",
    "pdf",
    "ps",
    "ttf",
    "class",
    "eot",
    "jpg",
    "pict",
    "svg",
    "webp",
    "css",
    "eps",
    "js",
    "pls",
    "svgz",
    "woff",
    "csv",
    "gif",
    "mid",
    "png",
    "swf",
    "woff2",
    "doc",
    "ico",
    "midi",
    "ppt",
    "tif",
    "xls",
    "docx",
    "jar",
    "otf",
    "pptx",
    "tiff",
    "xlsx",
]

headers = {
    "Authorization": "Bearer " + os.environ["CLOUDFLARE_API_KEY"],
    "Content-Type": "application/json",
}

endpoint = (
    "https://api.cloudflare.com/client/v4/zones/"
    + os.environ["CLOUDFLARE_ZONE_ID"]
    + "/purge_cache"
)

payloads = []
current_file_list = []
total_files = 0

for line in sys.stdin:
    line = line.strip()
    sys.stdout.write("Updated file: " + line)

    if os.path.splitext(line)[1][1:] not in cached_extensions:
        sys.stdout.write("\n")
        continue

    sys.stdout.write(" (will purge)\n")

    line = line.replace(".public/", "")
    if line[0] == "/":
        line = line[1:]

    current_file_list.append("https://spnati.net/" + line)
    if len(current_file_list) == 30:
        payloads.append({"files": current_file_list.copy()})
        current_file_list = []

    total_files += 1

if len(current_file_list) > 0:
    payloads.append({"files": current_file_list.copy()})

request_success = 0
request_total = 0
files_purged = 0
for i, payload in enumerate(payloads):
    r = requests.post(endpoint, json=payload, headers=headers)
    resp = r.json()

    request_total += 1
    if r.status_code == 200:
        request_success += 1
        files_purged += len(payload["files"])

    for err in resp["errors"]:
        print(
            "! Error during request #{}: Code {} - {}".format(
                i + 1, err["code"], err["message"]
            )
        )

if request_total > 0:
    print("{:d} / {:d} requests succeeded.".format(request_success, request_total))
    print(
        "Successfully purged {:d} / {:d} ({:.0%}) files from Cloudflare cache.".format(
            files_purged, total_files, files_purged / total_files
        )
    )

