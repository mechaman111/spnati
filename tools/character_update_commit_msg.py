import requests
import sys
from pathlib import Path


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

def format_diff_info(info: dict, include_modified: bool=True) -> str:
    ret = "{:d} -> {:d}".format(info["old"], info["new"])

    if not include_modified:
        return ret + " ({:+d})".format(info["net"])

    info_parts = []

    if info["added"] > 0:
        info_parts.append("added {:d}".format(info["added"]))
    
    if info["removed"] > 0:
        info_parts.append("removed {:d}".format(info["removed"]))
    
    if info["modified"] > 0:
        info_parts.append("modified {:d}".format(info["modified"]))

    if info["added"] > 0 and info["removed"] > 0:
        info_parts.append("net {:+d}".format(info["net"]))

    return "{:d} -> {:d} ({:s})".format(
        info["old"], info["new"], ", ".join(info_parts)
    )

def format_linecount_updates(diff):
    commit_msg = ""

    for key, info in diff.items():
        try:
            header = diff_set_names[key]
        except KeyError:
            continue
            
        if key == "poses" and info["net"] == 0:
            continue

        commit_msg += "\n**{:s}: ** {:s}".format(header, format_diff_info(info, key != "poses"))

    if len(diff["character_targets"]) > 0:
        commit_msg += "\n\nTargeting Updates:"

        for target, info in diff["character_targets"].items():
            commit_msg += "\n    - `{:s}`: {:s}".format(
                target, format_diff_info(info)
            )

    if len(diff["tag_targets"]) > 0:
        commit_msg += "\n\nFilter Updates:"

        for target, info in diff["tag_targets"].items():
            commit_msg += "\n    - `{:s}`: {:s}".format(
                target, format_diff_info(info)
            )

    return commit_msg


def find_base_dir() -> Path:
    cur = Path.cwd().resolve()
    
    while not cur.joinpath("opponents").is_dir():
        if len(cur.parts) == 1:
            raise ValueError("Could not find SPNATI repository")
        cur = cur.parent
        
    return cur


def compute_commit_msg(character_id: str, summary_msg: str) -> str:
    base_dir = find_base_dir()

    with base_dir.joinpath("opponents", character_id, "behaviour.xml").open("r", encoding="utf-8") as f:
        behavior_data = f.read()
        
    with base_dir.joinpath("opponents", character_id, "tags.xml").open("r", encoding="utf-8") as f:
        tags_data = f.read()

    print("Computing lineset diff for " + character_id + " against master...", file=sys.stderr, flush=True)
    
    r = requests.post(
        "https://spnati.faraway-vision.io/stats-api/diff/" + character_id + "/master",
        json={
            "behaviour.xml": behavior_data,
            "tags.xml": tags_data
        },
        headers={"Content-Type": "application/json"},
    )

    if r.status_code < 200 or r.status_code > 299:
        print("Linecountd diff request failed with status " + str(r.status_code), file=sys.stderr)
        sys.exit(1)
    
    diff = r.json()
    commit_msg = character_id.capitalize() + ": " + summary_msg + "\n"

    for key, info in diff.items():
        try:
            header = diff_set_names[key]
        except KeyError:
            continue
            
        if key == "poses" and info["net"] == 0:
            continue

        commit_msg += "\n**{:s}: ** {:s}".format(header, format_diff_info(info, key != "poses"))

    if len(diff["character_targets"]) > 0:
        commit_msg += "\n\nTargeting Updates:"

        for target, info in diff["character_targets"].items():
            commit_msg += "\n    - `{:s}`: {:s}".format(
                target, format_diff_info(info)
            )

    if len(diff["tag_targets"]) > 0:
        commit_msg += "\n\nFilter Updates:"

        for target, info in diff["tag_targets"].items():
            commit_msg += "\n    - `{:s}`: {:s}".format(
                target, format_diff_info(info)
            )

    return commit_msg


def main():
    try:
        update_character = sys.argv[1]
    except IndexError:
        print("USAGE: " + sys.argv[0] + " <character ID> [summary message]", file=sys.stderr)
        sys.exit(1)
        
    try:
        summary_msg = sys.argv[2].strip()
        if summary_msg == "-":
            summary_msg = None
    except IndexError:
        summary_msg = None

    if summary_msg == None or len(summary_msg) == 0:
        summary_msg = sys.stdin.read().strip()  
    
    if len(summary_msg) == 0:
        summary_msg = "updates"
        
    print(compute_commit_msg(update_character, summary_msg))
    

if __name__ == "__main__":
    main()
