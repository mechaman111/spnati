import requests
import sys

try:
    CHARACTER = sys.argv[1]
except IndexError:
    sys.exit(0)
    
try:
    SUMMARY_MSG = sys.argv[2]
except IndexError:
    SUMMARY_MSG = "updates"


diff_set_names = {
    "all_lines": "Total Linecount",
    "generic_lines": "Generic Linecount",
    "special_lines": "Special-Conditions Linecount",
    "targeted_lines": "Targeted Linecount",
    "filtered_lines": "Filtered Linecount",
    "poses": "Referenced Poses",
    "targeted_characters": "Total Targeted Characters",
    "targeted_tags": "Total Targeted Tags",
}


with open("./opponents/" + CHARACTER + "/behaviour.xml", "rb") as f:
    payload = f.read()

r = requests.post(
    "https://spnati.faraway-vision.io/stats-api/diff/" + CHARACTER + "/master",
    data=payload,
    headers={"Content-Type": "text/xml"},
)

if r.status_code < 200 or r.status_code > 299:
    sys.exit(1)

diff = r.json()

commit_msg = ""

for key, info in diff.items():
    try:
        header = diff_set_names[key]
    except KeyError:
        continue

    commit_msg += "\n**{:s}: ** {:d} -> {:d} ({:+d})".format(
        header, info["old"], info["new"], info["net"]
    )

if len(diff["character_targets"]) > 0:
    commit_msg += "\n\nTargeting Updates:"

    for target, info in diff["character_targets"].items():
        commit_msg += "\n    - `{:s}`: {:d} -> {:d} ({:+d})".format(
            target, info["old"], info["new"], info["net"]
        )

if len(diff["tag_targets"]) > 0:
    commit_msg += "\n\nFilter Updates:"

    for target, info in diff["tag_targets"].items():
        commit_msg += "\n    - `{:s}`: {:d} -> {:d} ({:+d})".format(
            target, info["old"], info["new"], info["net"]
        )

sys.stdout.write(CHARACTER.capitalize() + ": " + SUMMARY_MSG + "\n")
sys.stdout.write(commit_msg)
sys.stdout.write("\n")
sys.stdout.flush()
