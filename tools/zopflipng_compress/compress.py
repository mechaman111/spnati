#!/usr/bin/env python3

from __future__ import annotations

import argparse
import asyncio
import math
import os
import sys
import time
from pathlib import Path
from typing import List, Optional, Tuple

processed: List[Tuple[Path, int, int, float, float, bool]] = []
all_files: List[Path] = []


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


def format_size(size: int) -> str:
    if size < 1024:
        return "{}B".format(size)
    elif size < (1024 ** 2):
        kb = size / 1024
        return "{:.2f}K".format(kb)
    else:
        mb = size / (1024 ** 2)
        return "{:.2f}M".format(mb)


def format_time(t: float) -> str:
    mins = int(math.floor(t / 60))
    if mins >= 1:
        seconds = t - (mins * 60)
        return "{:d} minute{}, {:.1f} second{}".format(
            mins, ("s" if (mins != 1) else ""), seconds, ("s" if (seconds != 1) else "")
        )
    else:
        return "{:.1f} second{}".format(t, ("s" if (t != 1) else ""))


async def compress_file_loop(
    process_queue: asyncio.Queue,
    out_dir: Path,
    all_filters: bool,
    extra_iterations: bool,
    force_all: bool,
):
    while process_queue.qsize() > 0:
        infile: Path
        rel_infile: Path

        try:
            infile, rel_infile = process_queue.get_nowait()
        except asyncio.QueueEmpty:
            return

        in_st = infile.stat()
        in_size = in_st.st_size
        start_time = time.perf_counter()

        outfile = out_dir.joinpath(rel_infile)
        outfile.parent.mkdir(parents=True, exist_ok=True)
        do_compress = (not outfile.is_file()) or force_all
        if do_compress:
            # NOTE: could also use --filters=01234mepb here to squeeze out a few hundred more bytes per image, at the cost of slowing compression down a bit
            # -m shaves off about 100-200 more, but slows compression by 2x, so it probably isn't worth it.

            args = ["zopflipng.exe"]

            if all_filters:
                args.append("--filters=01234mepb")
            else:
                args.append("--filters=01234mep")

            if extra_iterations:
                args.append("-m")

            args.append(infile.as_posix())
            args.append(outfile.as_posix())

            proc = await asyncio.create_subprocess_exec(
                *args,
                stdout=asyncio.subprocess.DEVNULL,
                stderr=asyncio.subprocess.DEVNULL,
            )
            await proc.communicate()

        out_size = outfile.stat().st_size
        processed.append(
            (
                rel_infile,
                in_size,
                out_size,
                start_time,
                time.perf_counter(),
                do_compress,
            )
        )

        if out_size >= in_size:
            outfile.unlink()

        process_queue.task_done()


async def term_loop(progress: ProgressLine):
    justify_file = max(len(path.as_posix()) for path in all_files)

    while True:
        total_new_sz = 0
        total_old_sz = 0
        times = []

        for tup in processed:
            total_old_sz += tup[1]
            total_new_sz += tup[2]

            if tup[5]:
                times.append(tup[4] - tup[3])

        if total_old_sz > 0:
            total_reduction = total_new_sz / total_old_sz
        else:
            total_reduction = 1.0

        if len(processed) > 0:
            last_file = processed[-1][0].as_posix().ljust(justify_file)
        else:
            last_file = "<none>".ljust(justify_file)

        if len(times) > 0:
            avg_time = ", avg. {} per file".format(format_time(sum(times) / len(times)))
        else:
            avg_time = ""

        progress.update(
            "{:>3d} / {:<3d} ({:5.1%}) : {} : total size {} => {} ({:.1%} of original{})",
            len(processed),
            len(all_files),
            len(processed) / len(all_files),
            last_file,
            format_size(total_old_sz),
            format_size(total_new_sz),
            total_reduction,
            avg_time,
        )
        await asyncio.sleep(0.05)


def read_gitignores(folder: Path) -> List[Path]:
    ignored = []
    for gitignore in folder.glob("**/.gitignore"):
        with gitignore.open("r", encoding="utf-8") as f:
            for line in filter(lambda s: len(s) > 0, map(str.strip, f)):
                ignored.extend(gitignore.parent.glob(line))
    return ignored


async def main(
    target_dir: Path,
    out_dir: Path,
    n_parallel: Optional[int],
    all_filters: bool,
    extra_iterations: bool,
    force_all: bool,
    ignore: List[Path],
):
    if not out_dir.is_dir():
        out_dir.mkdir()

    process_queue = asyncio.Queue()
    for path in filter(Path.is_file, target_dir.glob("**/*.png")):
        if (
            any(path.is_relative_to(ignored_path) for ignored_path in ignore)
            or path.is_relative_to(out_dir)
            or path in ignore
        ):
            continue

        print(path.as_posix())

        process_queue.put_nowait((path, path.relative_to(target_dir)))
        all_files.append(path)

    progress = ProgressLine()
    tasks: List[asyncio.Task] = [asyncio.create_task(term_loop(progress))]
    if n_parallel is None or n_parallel <= 0:
        n_parallel = os.cpu_count()
        if n_parallel is None:
            n_parallel = 1

    for _ in range(n_parallel):
        tasks.append(
            asyncio.create_task(
                compress_file_loop(
                    process_queue, out_dir, all_filters, extra_iterations, force_all
                )
            )
        )

    await process_queue.join()
    for task in tasks:
        task.cancel()
    await asyncio.gather(*tasks, return_exceptions=True)

    total_new_sz = 0
    total_old_sz = 0
    times = []

    for tup in processed:
        total_old_sz += tup[1]
        total_new_sz += tup[2]

        if not tup[5]:
            times.append(tup[4] - tup[3])

    if total_old_sz > 0:
        total_reduction = total_new_sz / total_old_sz
    else:
        total_reduction = 1.0

    progress.finish(
        "Compressed {} images from {} to {} total size ({:.1%} of original)",
        len(all_files),
        format_size(total_old_sz),
        format_size(total_new_sz),
        total_reduction,
    )


if __name__ == "__main__":
    parser = argparse.ArgumentParser(
        "compress.py",
        description="Compress images in parallel using zopflipng.",
        epilog="""
    If no out_dir is passed, images will be written to a 'compressed/' subdirectory in the target directory by default.
    Images that already exist in the destination directory will be skipped by default (unless -f/--force is passed).
    """,
    )
    parser.add_argument(
        "-b",
        "--all-filters",
        action="store_true",
        help="Try brute-force filter for images (i.e. pass --filters=01234mepb to zopflipng). Typically not necessary.",
    )
    parser.add_argument(
        "-m",
        "--compress-more",
        action="store_true",
        help="Compress more: use more iterations (i.e. pass -m to zopflipng). Typically not necessary.",
    )
    parser.add_argument(
        "-p",
        "--parallelism",
        type=int,
        help="Number of zopflipng processes to run in parallel (default: # of CPU cores available)",
    )
    parser.add_argument(
        "-i",
        "--ignore",
        type=Path,
        nargs="*",
        help="Ignore images under path (can be specified multiple times)",
    )
    parser.add_argument(
        "-f",
        "--force",
        action="store_true",
        help="Attempt to compress images even if they already exist in the destination directory",
    )
    parser.add_argument(
        "in_dir", type=Path, help="Source directory with images to compress."
    )
    parser.add_argument(
        "out_dir",
        nargs="?",
        type=Path,
        default=None,
        help="Destination directory for writing compressed images (will be created if necessary).",
    )
    args = parser.parse_args()

    if args.out_dir is None:
        args.out_dir = args.in_dir.joinpath("compressed")

    if args.ignore is None:
        args.ignore = []
    args.ignore.extend(read_gitignores(args.in_dir))

    asyncio.run(
        main(
            args.in_dir,
            args.out_dir,
            args.parallelism,
            args.all_filters,
            args.compress_more,
            args.force,
            args.ignore,
        )
    )
