from PIL import Image
import kkl_import as kkl
from pathlib import Path
import sys
import csv

POSE_COMPONENTS = [
    "aa",
    "ab",
    "ac",
    "ba",
    "bb",
    "bc",
    "bd",
    "be",
    "t01",
    "ga",
    "gb",
    "gc",
    "gd",
    "gh",
    "gf",
    "gg",
    "ha",
    "hb",
    "hc",
    "hd",
    "ca",
    "da",
    "db",
    "dd",
    "dh",
    "di",
]


def remove_subcodes(char, prefixes):
    tgt_char = kkl.KisekaeCharacter(char)

    subcodes = []
    for subcode in char:
        if subcode.prefix not in prefixes:
            continue

        subcodes.append(subcode)

    for subcode in subcodes:
        tgt_char.subcodes.remove(subcode)

    return tgt_char


def apply_pose_components(dest_char, pose_char):
    tgt_char = kkl.KisekaeCharacter(dest_char)

    found_t01 = False

    for subcode in pose_char:
        if subcode.prefix not in POSE_COMPONENTS and not subcode.prefix.startswith("m"):
            continue

        if subcode.prefix == "t01":
            found_t01 = True

        # comp = kkl.KisekaeComponent(subcode)
        # tgt_char.subcodes.append(comp)

        for tgt_subcode in tgt_char.subcodes:
            if tgt_subcode.prefix == subcode.prefix:
                dest_list = tgt_subcode.attributes.copy()

                if len(dest_list) < len(subcode.attributes):
                    for i in range(len(dest_list), len(subcode.attributes)):
                        dest_list.append("0")

                if subcode.prefix == "m01":
                    subcode.attributes[12] = "0"

                for i, a in enumerate(subcode.attributes):
                    dest_list[i] = a

                tgt_subcode.attributes = dest_list
                break
        else:
            comp = kkl.KisekaeComponent(subcode)

            if subcode.prefix == "m01":
                subcode.attributes[12] = "0"

            tgt_char.subcodes.append(comp)

    if not found_t01:
        tgt_char = remove_subcodes(tgt_char, ["t01"])
        tgt_char.subcodes.append(kkl.KisekaeComponent("t01"))

    return tgt_char


if __name__ == "__main__":
    model_file = sys.argv[1]
    pose_file = sys.argv[2]

    outfile = sys.argv[3]

    with open(model_file, "r", encoding="utf-8", newline="") as model_in:
        with open(pose_file, "r", encoding="utf-8", newline="") as pose_in:
            with open(outfile, "w", encoding="utf-8", newline="") as outf:
                writer = None

                model_reader = csv.DictReader(model_in)
                pose_reader = csv.DictReader(pose_in)

                models = {}
                poses = {}

                for model_row in model_reader:
                    try:
                        if model_row["code"] is None or len(model_row["code"]) <= 0:
                            continue

                        key = (model_row["stage"], model_row["pose"])
                        models[key] = model_row
                    except KeyError:
                        continue

                for pose_row in pose_reader:
                    try:
                        if pose_row["code"] is None or len(pose_row["code"]) <= 0:
                            continue

                        key = (pose_row["stage"], pose_row["pose"])
                        poses[key] = pose_row
                    except KeyError:
                        continue

                intersect = set(models.keys()).intersection(set(poses.keys()))

                writer = csv.DictWriter(outf, model_reader.fieldnames)
                writer.writeheader()

                out_rows = []

                for key in intersect:
                    model_row = models[key]
                    pose_row = poses[key]

                    model_char = kkl.KisekaeCode(model_row["code"])[0]
                    pose_char = kkl.KisekaeCode(pose_row["code"])[0]

                    out_row = dict(model_row)
                    out_row["code"] = str(
                        kkl.KisekaeCode(apply_pose_components(model_char, pose_char))
                    )

                    out_rows.append((key[0], key[1], out_row))

                out_rows = sorted(out_rows, key=lambda k: int(k[0]))
                out_rows = sorted(out_rows, key=lambda k: k[1])

                for _, _, row in out_rows:
                    writer.writerow(row)

