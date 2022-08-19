import sys
import time
from argparse import ArgumentParser, FileType

from bs4.element import Tag

from kkl_import import PoseMatrix

if __name__ == "__main__":
    parser = ArgumentParser()
    parser.add_argument("infile", type=FileType("rb"))
    parser.add_argument("outfile", type=FileType("wb"))
    parser.add_argument("sheets", type=int, nargs="+")
    args = parser.parse_args()

    matrix = PoseMatrix.load_file(args.infile)
    sheet_idx: int

    total_cells_processed = 0
    for sheet_idx in args.sheets:
        sheet = matrix.sheets[sheet_idx]
        print(
            'Processing sheet {} ("{}")... '.format(sheet_idx, sheet.name),
            file=sys.stderr,
            end="",
        )

        n_seen = 0
        n_processed = 0
        rows_seen = 0
        rows_processed = 0
        sheet_modified = False

        for row in sheet.rows.values():
            row_modified = False
            rows_seen += 1
            for pose in row.poses.values():
                n_seen += 1
                expanded = pose.expand_code()
                if (expanded is None) or (pose.tag is None) or (pose.tag.code is None):
                    continue

                code_tag: Tag = pose.tag.code
                code_tag.string = str(expanded)
                n_processed += 1
                row_modified = True
                sheet_modified = True
                total_cells_processed += 1

                if pose.tag.lastupdate is not None:
                    pose.tag.lastupdate.string = str(int(round(time.time() * 1000)))

            if row_modified:
                rows_processed += 1
                if row.tag.clothing is not None:
                    row.tag.clothing.clear()
        if sheet_modified:
            sheet.tag.model.clear()
        print(
            "modified {}/{} rows and {}/{} cells.".format(
                rows_processed, rows_seen, n_processed, n_seen
            ),
            file=sys.stderr,
        )
    print("{} matrix cells modified.".format(total_cells_processed), file=sys.stderr)

    args.outfile.write(matrix.tag.find("posegrid").encode("utf-8", formatter="minimal"))
