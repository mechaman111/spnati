import hashlib
import re
import sys
from pathlib import Path

# Filename globs to apply cache busting to.
PATHS = [
    "css/spni*.css",
    "js/fileIndex.js",
    "js/player.js",
    "js/save.js",
    "js/table.js",
    "js/feedback.js",
    "js/event.js",
    "js/selectGroups.js",
    "js/spni*.js",
    "opponents/monika/js/behaviour_callbacks.js",
    "opponents/monika/js/effects.js",
    "opponents/monika/js/extended_dialogue.js",
    "opponents/monika/js/utils.js",
    "opponents/monika/css/*.css",
]

# Files in which references to cache-busted files should be changed.
MODIFY_FILES = [
    "index.html",
    "opponents/monika/js/monika.js",
]


def replace_references(transformed, infile):
    with infile.open("r", encoding="utf-8") as f:
        contents = f.read()

    for sub_from, repl in transformed.items():
        contents = re.sub(re.escape(sub_from), repl.replace("\\", r"\\"), contents)

    with infile.open("w", encoding="utf-8") as f:
        f.write(contents)

def rename_file(base_path, path):
    parent = path.parent

    m = hashlib.sha1()
    with path.open("rb") as f:
        m.update(f.read())

    out_name = path.stem + "." + m.hexdigest() + path.suffix
    out_path = parent.joinpath(out_name)

    path.replace(out_path)

    repl_from = str(path.relative_to(base_path).as_posix())
    repl_to = str(out_path.relative_to(base_path).as_posix())
    print("{} => {}".format(repl_from, repl_to))

    return (repl_from, repl_to)


def main():
    transformed = {}
    base_path = Path(sys.argv[1]).resolve()

    for pattern in PATHS:
        for path in base_path.glob(pattern):
            repl_from, repl_to = rename_file(base_path, path)
            transformed[repl_from] = repl_to

    for pattern in MODIFY_FILES:
        for path in base_path.glob(pattern):
            replace_references(transformed, path)

    # Handle monika.js and Monika's behaviour.xml specially:
    monika_js = base_path.joinpath('opponents', 'monika', 'js', 'monika.js')
    monika_xml = base_path.joinpath('opponents', 'monika', 'behaviour.xml')

    repl_from, repl_to = rename_file(base_path, monika_js)
    transformed[repl_from] = repl_to
    replace_references(transformed, monika_xml)


if __name__ == "__main__":
    main()
