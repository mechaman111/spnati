#!/usr/bin/env python3
from __future__ import annotations

from pathlib import Path
from typing import Optional, Tuple
import png
import multiprocessing as mp
import time
import sys
import os
import traceback


KIB = 1024
MIB = 1024 * 1024
GIB = 1024 * 1024 * 1024


class ProgressLine:
    spinner_frames = ["⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏"]

    def __init__(self):
        self.spinner_idx = 0
        self.last_line: str = ""
        self.start_time = time.perf_counter()

    def _print(self, status: str, end: str, fmt: str, args, kwargs):
        s = " [" + status + "] " + fmt.format(*args, **kwargs)
        if len(s) < len(self.last_line):
            s += " " * (len(self.last_line) - len(s))
        self.last_line = s
        print(s, file=sys.stderr, end=end, flush=True)

    def warn(self, fmt: str, *args, **kwargs):
        last_line = self.last_line
        self._print("!", "\n", fmt, args, kwargs)
        self.last_line = last_line
        print(last_line, end="\r", file=sys.stderr, flush=True)

    def update(self, fmt: str, *args, **kwargs):
        self._print(self.spinner_frames[self.spinner_idx], "\r", fmt, args, kwargs)
        self.spinner_idx = (self.spinner_idx + 1) % len(self.spinner_frames)

    def finish(self, fmt: str, *args, **kwargs):
        elapsed = time.perf_counter() - self.start_time
        self._print("✓", "\n", fmt + " in {:.2f} seconds".format(elapsed), args, kwargs)


def _map_fn(path: Path) -> Optional[Tuple[Path, int]]:
    if path.stat().st_size <= (10 * KIB):
        return None

    try:
        reader = png.Reader(filename=path.as_posix())
        reader.preamble()
        if reader.color_type == 3:
            return None
    except png.Error as e:
        return None
    except Exception as e:
        traceback.print_exc(file=sys.stderr)
        return None
    return (path, path.stat().st_size)


def format_size(size: int) -> str:
    if size >= GIB:
        return format(size / GIB, ".2f") + " GiB"
    elif size >= MIB:
        return format(size / MIB, ".2f") + " MiB"
    elif size >= KIB:
        return format(size / KIB, ".2f") + " KiB"
    else:
        return str(size) + " bytes"


if __name__ == "__main__":
    uncompressed = []
    with mp.Pool(processes=os.cpu_count()) as pool:
        progress = ProgressLine()
        for i, result in enumerate(
            pool.imap_unordered(
                _map_fn, list(Path("").glob("**/*.png")), chunksize=500
            )
        ):
            if result is not None:
                uncompressed.append(result)
            progress.update(
                "Scanned {} images ({} uncompressed images found)...",
                i + 1,
                len(uncompressed),
            )
        progress.finish("Scanned {} images", i + 1)

    by_character = {}
    for path, size in uncompressed:
        by_character.setdefault(Path(*path.parts[:-1]), list()).append((path, size))

    grand_total = 0
    for k, files in sorted(
        by_character.items(), key=lambda kv: sum(f[1] for f in kv[1]), reverse=True
    ):
        total_sz = sum(f[1] for f in files)
        grand_total += total_sz
        print("[ {} ({}) ]".format(k.as_posix(), format_size(total_sz)))

        max_len = max(len(file.relative_to(k).as_posix()) for file, _ in files)
        for file, sz in sorted(files, key=lambda t: t[1], reverse=True):
            print(file.relative_to(k).as_posix().ljust(max_len) + " " + format_size(sz))
        print("")

    print("Total size of uncompressed images: " + format_size(grand_total))
