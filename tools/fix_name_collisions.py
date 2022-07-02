import subprocess as sp
import sys
from argparse import ArgumentParser
from pathlib import Path
from typing import Dict, Generator, Set


def get_repository_root() -> Path:
    proc = sp.run(["git", "rev-parse", "--show-toplevel"], capture_output=True)
    return Path(proc.stdout.decode("utf-8").strip())


def get_working_tree_files(repository_root: Path) -> Generator[Path, None, None]:
    proc = sp.run(["git", "ls-files", "-z", "--full-name"], capture_output=True, cwd=repository_root)
    for line in proc.stdout.split(b"\0"):
        yield Path(line.decode("utf-8").strip())


def get_ref_files(repository_root: Path, treeish: str) -> Generator[Path, None, None]:
    proc = sp.run(["git", "ls-tree", "-r", "-z", "--name-only", "--full-tree", treeish], capture_output=True, cwd=repository_root)
    for line in proc.stdout.split(b"\0"):
        yield Path(line.decode("utf-8").strip())


# Returns dict mapping from WD file => set of colliding paths in referenced tree
def get_colliding_paths(repository_root: Path, treeish: str) -> Dict[Path, Set[Path]]:
    filenames: Dict[str, Dict[Path, Path]] = {}

    for wd_file in get_working_tree_files(repository_root):
        name_set = filenames.setdefault(wd_file.name.casefold(), dict())
        name_set[wd_file.parent] = wd_file

    collisions: Dict[Path, Set[Path]] = {}

    for ref_file in get_ref_files(repository_root, treeish):
        try:
            name_set = filenames[ref_file.name.casefold()]
        except KeyError:
            continue

        try:
            collision = name_set[ref_file.parent]
            if ref_file.as_posix() == collision.as_posix():
                continue

            collisions.setdefault(collision, set()).add(ref_file)
        except KeyError:
            pass

    return collisions

def do_git_mv(repository_root: Path, from_file: Path, to_file: Path):
    proc = sp.run(["git", "mv", "-f", from_file.as_posix(), to_file.as_posix()], cwd=repository_root)
    if proc.returncode != 0:
        print("Git move of {} to {} failed with return code {}".format(from_file.as_posix(), to_file.as_posix(), proc.returncode), file=sys.stderr)
    else:
        print("Moved {} => {}".format(wd_file.as_posix(), to_file.as_posix()), file=sys.stderr)


if __name__ == "__main__":
    parser = ArgumentParser()
    parser.add_argument("--rename", "-r", action="store_true", help="Automatically rename colliding files with 'git mv'")
    parser.add_argument("ref", help="Git ref to compare against")
    args = parser.parse_args()

    repo_root = get_repository_root()
    collisions = get_colliding_paths(repo_root, args.ref)

    for wd_file, ref_files in collisions.items():
        if args.rename:
            if len(ref_files) == 1:
                do_git_mv(repo_root, wd_file, ref_files.pop())
            elif len(ref_files) > 1:
                print("{} collides with {} different files in {}".format(wd_file.as_posix(), len(ref_files), args.ref), file=sys.stderr)
        else:
            for ref_file in ref_files:
                print(wd_file.as_posix(), ref_file.as_posix())

