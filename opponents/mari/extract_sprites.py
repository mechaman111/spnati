import argparse
import asyncio
import sys
import time
from io import BytesIO
from pathlib import Path
from typing import Iterable, Optional, Set, Tuple

from PIL import Image

import kkl_client as kkl
from kkl_client import (KisekaeImageResponse, KisekaeLocalClient,
                        KisekaeServerRequest, KisekaeServerResponse)
from kkl_import import KisekaeCode, PoseMatrix

BASE_PARTS = {
    "body_lower": ("dou",),
    "vibrator": ("vibrator",),
    "upper_arm_left": ("handm0_0",),
    "upper_arm_right": ("handm0_1",),
    "forearm_left": ("handm1_0",),
    "forearm_right": ("handm1_1",),
    "arm_left": ("handm0_0", "handm1_0"),
    "arm_right": ("handm0_1", "handm1_1"),
    "lower_forearm_left": ("handm1_0.hand.arm1",),
    "lower_forearm_right": ("handm1_1.hand.arm1",),
    "hand_left": ("handm1_0.hand.arm0", "handm1_0.hand.item"),
    "hand_right": ("handm1_1.hand.arm0", "handm1_1.hand.item"),
    "leg_left": (
        "ashi0.thigh.thigh",
        "ashi0.shiri.shiri",
        "ashi0.leg.leg",
        "ashi0.leg_huku.leg.LegBand",
        "ashi0.foot.foot",
    ),
    "leg_right": (
        "ashi1.thigh.thigh",
        "ashi1.shiri.shiri",
        "ashi1.leg.leg",
        "ashi1.leg_huku.leg.LegBand",
        "ashi1.foot.foot",
    ),
    "thigh_left": ("ashi0.thigh.thigh", "ashi0.shiri.shiri"),
    "thigh_right": ("ashi1.thigh.thigh", "ashi1.shiri.shiri"),
    "lower_leg_left": ("ashi0.leg.leg", "ashi0.leg_huku.leg.LegBand"),
    "lower_leg_right": ("ashi1.leg.leg", "ashi1.leg_huku.leg.LegBand"),
    "foot_left": ("ashi0.foot.foot",),
    "foot_right": ("ashi1.foot.foot",),
    "head_base": ("head",),
    "hair_base": (
        "HairUshiro",
        "HairBack",
        "hane",
        "HatBack",
        "SideBurnMiddle",
        "HatBack",
    ),
}

BASE_HEAD_PARTS = [
    "head",
    "HairUshiro",
    "HairBack",
    "hane",
    "SideBurnMiddle",
    "HatBack",
]


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

    def notice(self, status: str, fmt: str, *args, **kwargs):
        last_line = self.last_line
        self._print(status, "\n", fmt, args, kwargs)
        self.last_line = last_line
        print(last_line, end="\r", file=sys.stderr, flush=True)

    def warn(self, fmt: str, *args, **kwargs):
        return self.notice("!", fmt, *args, **kwargs)

    def info(self, fmt: str, *args, **kwargs):
        return self.notice("i", fmt, *args, **kwargs)

    def update(self, fmt: str, *args, **kwargs):
        self._print(self.spinner_frames[self.spinner_idx], "\r", fmt, args, kwargs)
        self.spinner_idx = (self.spinner_idx + 1) % len(self.spinner_frames)

    def finish(self, fmt: str, *args, **kwargs):
        elapsed = time.perf_counter() - self.start_time
        self._print("✓", "\n", fmt + " in {:.2f} seconds".format(elapsed), args, kwargs)


class AsyncProgress:
    def __init__(self, total: int, start_status: str = ""):
        self.progress = ProgressLine()
        self.cur_progress = 0
        self.progress_total = total
        self.cur_status = start_status
        self.task = None

    def update(self, cur: int, cur_status: str):
        self.cur_progress = cur
        self.cur_status = cur_status

    async def loop(self):
        while True:
            self.progress.update(
                "{:d} / {:d} ({:.0%}): {}...",
                self.cur_progress,
                self.progress_total,
                self.cur_progress / self.progress_total,
                self.cur_status,
            )
            await asyncio.sleep(0.1)

    def run(self):
        self.task = asyncio.create_task(self.loop())

    async def finish(self, finish_text: str):
        self.task.cancel()
        try:
            await self.task
        except asyncio.CancelledError:
            pass
        self.progress.finish(finish_text)


class KisekaeServerError(Exception):
    pass


async def do_command(
    client: KisekaeLocalClient, request: KisekaeServerRequest
) -> KisekaeServerResponse:
    resp = await client.send_command(request)
    if not resp.is_success():
        raise KisekaeServerError(request, resp)

    return resp


async def hide_children(client: KisekaeLocalClient, path: str):
    try:
        await do_command(
            client, KisekaeServerRequest.set_children_alpha_direct(0, path, 0, 0)
        )
    except KisekaeServerError:
        pass
        # sys.stderr.write("failed to set alpha for parts under " + path + " ... ")
        # sys.stderr.flush()


async def show_part(client: KisekaeLocalClient, path: str):
    try:
        await do_command(client, KisekaeServerRequest.reset_alpha_direct(0, path))
    except KisekaeServerError:
        pass
        # sys.stderr.write("failed to set alpha for " + path + " ... ")
        # sys.stderr.flush()


async def export_part(
    client: KisekaeLocalClient, ids: Tuple[str], all_parts: Set[str]
) -> bytes:
    prefixes = set(ids)
    for part in ids:
        path = part.split(".")
        for i in range(1, len(path)):
            prefix = ".".join(path[:i])
            prefixes.add(prefix)

    for part in filter(
        lambda part: not any(map(lambda i: part.startswith(i), ids))
        and part not in prefixes,
        all_parts,
    ):
        try:
            await do_command(
                client, KisekaeServerRequest.set_alpha_direct(0, part, 0, 0)
            )
        except KisekaeServerError:
            pass
            # sys.stdout.write("Failed to set alpha for " + part + " ... ")
            # sys.stdout.flush()

    img_data: KisekaeImageResponse = await do_command(
        client, KisekaeServerRequest.screenshot(False)
    )

    await do_command(client, KisekaeServerRequest.reset_all_alpha_direct())
    return img_data.get_data()


def compute_parts_map(code: KisekaeCode):
    parts_map = dict(BASE_PARTS)
    all_parts = set()

    ribbon_parts = []
    ribbon_depths = {}

    belt_parts = []
    belt_depths = {}

    head_parts = list(BASE_HEAD_PARTS)

    hair_parts = []
    hair_depths = {}

    for part in code[0]:
        if len(part.id) != 1:
            continue

        if len(part) < 2:
            continue

        part_type = None
        if part.id == "r":
            part_type = "HairEx{}".format(part.index)
            add_to = [head_parts, hair_parts]

            if len(part) >= 6:
                depth = part[5]
                if depth not in hair_depths:
                    hair_depths[depth] = []
                add_to.append(hair_depths[depth])

        elif part.id == "s":
            part_type = "belt{}".format(part.index)
            add_to = [belt_parts]

            if len(part) >= 10:
                depth = part[9]
                if depth not in belt_depths:
                    belt_depths[depth] = []
                add_to.append(belt_depths[depth])

        elif part.id == "m":
            if len(part) >= 16:
                if part[15] != "0":
                    continue

            part_type = "Ribon{}".format(part.index)
            add_to = [head_parts, ribbon_parts]

            if len(part) >= 6:
                depth = part[5]
                if depth not in ribbon_depths:
                    ribbon_depths[depth] = []
                add_to.append(ribbon_depths[depth])

        if part_type is not None:
            try:
                mirroring = part[4]
            except IndexError:
                mirroring = "0"

            if mirroring == "0" or mirroring == "1":
                for l in add_to:
                    l.append(part_type + "_1")

            if mirroring == "0" or mirroring == "2":
                for l in add_to:
                    l.append(part_type + "_0")

    if len(ribbon_parts) > 0:
        parts_map["ribbons"] = tuple(ribbon_parts)

    if len(belt_parts) > 0:
        parts_map["belts"] = tuple(belt_parts)

    if len(hair_parts) > 0:
        parts_map["hair_parts"] = tuple(hair_parts) + tuple(ribbon_parts)

    for depth, part_list in ribbon_depths.items():
        parts_map["ribbons_" + depth] = tuple(part_list)

    for depth, part_list in hair_depths.items():
        parts_map["hair_" + depth] = tuple(part_list)

    for depth, part_list in belt_depths.items():
        parts_map["belts_" + depth] = tuple(part_list)

    try:
        parts_map["hair_2"] = parts_map["hair_2"] + tuple(ribbon_parts)
    except KeyError:
        pass

    parts_map["chest"] = tuple(["mune"])
    parts_map["body_upper"] = tuple(head_parts + ["mune"])
    parts_map["body"] = tuple(head_parts + ["mune", "dou"])
    parts_map["head"] = tuple(head_parts)
    parts_map["hair"] = (
        tuple(BASE_PARTS["hair_base"]) + tuple(hair_parts) + tuple(ribbon_parts)
    )

    parts_map["body_upper_larm_upper"] = tuple(head_parts + ["mune"]) + BASE_PARTS["upper_arm_left"]
    parts_map["body_upper_rarm_upper"] = tuple(head_parts + ["mune"]) + BASE_PARTS["upper_arm_right"]
    parts_map["body_upper_arms_upper"] = tuple(head_parts + ["mune"]) + BASE_PARTS["upper_arm_right"] + BASE_PARTS["upper_arm_left"]
    parts_map["body_upper_larm_full"] = tuple(head_parts + ["mune"]) + BASE_PARTS["arm_left"]
    parts_map["body_upper_rarm_full"] = tuple(head_parts + ["mune"]) + BASE_PARTS["arm_right"]
    parts_map["body_upper_arms_full"] = tuple(head_parts + ["mune"]) + BASE_PARTS["arm_right"] + BASE_PARTS["arm_left"]

    for t in parts_map.values():
        for part in t:
            all_parts.add(part)

    return parts_map, all_parts


async def do_disassembly(
    client: KisekaeLocalClient,
    code: KisekaeCode,
    export_parts: Iterable[str],
    out_folder: Path,
    disable_hair: bool,
    trim: bool,
    align: bool,
    zoom: int,
    camera_x: int,
    camera_y: int,
    juice: Optional[int],
):
    parts_map, all_parts = compute_parts_map(code)
    export_parts = list(export_parts)

    if "all" in export_parts:
        export_parts = list(parts_map.keys())

    progress_total = len(export_parts) + 1
    progress = AsyncProgress(progress_total, "Importing code")
    progress.run()

    if disable_hair:
        code[0]["ec"].attributes = []

    if align:
        code[0]["bc"][0] = "400"
        code[0]["bc"][1] = "500"

    if juice is not None:
        code[0]["dc"][0] = str(juice)

    reset_code = "68***ba50_bb6.0_bc410.500.8.0.1.0_bd6_be180_ad0.0.0.0.0.0.0.0.0.0_ae0.3.3.0.0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_e00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v00_ud7.8_uc{}.{}.{}".format(
        zoom, camera_x, camera_y
    )

    await do_command(client, KisekaeServerRequest.import_partial(reset_code))
    await do_command(client, KisekaeServerRequest.import_partial(str(code)))
    await do_command(client, KisekaeServerRequest.reset_all_alpha_direct())

    # Set the character shadow mode
    await do_command(client, KisekaeServerRequest.set_character_data(0, "bc", 4, False))

    if not out_folder.is_dir():
        out_folder.mkdir()
    cur_progress = 0

    for part_name in export_parts:
        cur_progress += 1
        if len(part_name) == 0:
            continue

        progress.update(cur_progress, "Exporting " + part_name)
        ids = parts_map[part_name]
        data = await export_part(client, ids, all_parts)

        if disable_hair:
            outfile = out_folder.joinpath(part_name + "-nohair.png")
        else:
            outfile = out_folder.joinpath(part_name + ".png")

        if trim:
            with BytesIO(data) as bio:
                with Image.open(bio, formats=["png"]) as im:
                    bbox = im.getbbox()
                    im_crop = im.crop(bbox)
                    with outfile.open("wb") as f:
                        im_crop.save(f, format="png")
        else:
            with outfile.open("wb") as f:
                f.write(data)

    progress.update(cur_progress, "Cleaning up")
    await do_command(client, KisekaeServerRequest.reset_all_alpha_direct())

    await progress.finish("Exported {} parts".format(len(export_parts)))


async def main(
    code: KisekaeCode,
    parts: Iterable[str],
    outdir: Path,
    args: argparse.Namespace
):
    async with kkl.connect(5) as client:
        await do_disassembly(
            client, code, parts, outdir, args.disable_hair, args.trim, args.align, args.zoom, args.camera_x, args.camera_y, args.juice
        )


if __name__ == "__main__":
    VALID_PARTS = list(BASE_PARTS.keys()) + [
        "body_upper",
        "body",
        "ribbons",
        "hair_parts",
        "head",
        "all",
        "body_upper_larm_upper",
        "body_upper_rarm_upper",
        "body_upper_arms_upper",
        "body_upper_larm_full",
        "body_upper_rarm_full",
        "body_upper_arms_full",
        "chest",
        "hair",
    ]
    BASE_IN_PATH = Path.cwd().joinpath("epilogue", "codes")
    BASE_OUT_PATH = Path.cwd().joinpath("epilogue", "images")

    parser = argparse.ArgumentParser()
    parser.add_argument("--no-align", "-A", action="store_false", dest="align")
    parser.add_argument("--from-matrix", "-m", action="store_true")
    parser.add_argument("--disable-hair", "-d", action="store_true")
    parser.add_argument("--trim", "-t", action="store_true")
    parser.add_argument("--out-name", "-o")
    parser.add_argument("--zoom", "-z", type=int, default=7)
    parser.add_argument("--camera-x", "-x")
    parser.add_argument("--camera-y", "-y")
    parser.add_argument("--juice", "-j", type=int)
    parser.add_argument("codefile")
    parser.add_argument("parts", nargs="+", choices=VALID_PARTS)
    args = parser.parse_args()

    # x=2, y=24 are default with zoom 7
    # x=300, y=400 work for zoom 30
    # x=500, y=485 work for zoom 45
    # paizuri uses x=740, y=630 @ zoom 65
    # cunnilingus uses x=400, y=680 @ zoom 40
    # alignment note: ensure sprites are at x=410, depth=500

    mx = (300 - 2) / (30 - 7)
    my = (400 - 24) / (30 - 7)
    baseline_cam_x = round((mx * (args.zoom - 7)) + 2)
    baseline_cam_y = round((my * (args.zoom - 7)) + 24)

    if args.camera_x is not None:
        if args.camera_x.startswith("+"):
            args.camera_x = baseline_cam_x + int(args.camera_x[1:])
        elif args.camera_x.startswith("-"):
            args.camera_x = baseline_cam_x - int(args.camera_x[1:])
        else:
            args.camera_x = int(args.camera_x)
    else:
        args.camera_x = baseline_cam_x

    if args.camera_y is not None:
        if args.camera_y.startswith("+"):
            args.camera_y = baseline_cam_y + int(args.camera_y[1:])
        elif args.camera_y.startswith("-"):
            args.camera_y = baseline_cam_y - int(args.camera_y[1:])
        else:
            args.camera_y = int(args.camera_y)
    else:
        args.camera_y = baseline_cam_y

    if args.from_matrix:
        tgt_parts: Tuple[str, ...] = args.codefile.split(":", 2)

        pose_key = tgt_parts[-1]
        if len(tgt_parts) == 3:
            stage = int(tgt_parts[1])
            sheet = tgt_parts[0]
            try:
                sheet = int(sheet)
            except ValueError:
                pass
        elif len(tgt_parts) == 2:
            stage = int(tgt_parts[0])
            sheet = 0
        else:
            stage = 0

        matrix = PoseMatrix.load_character(Path.cwd())
        if isinstance(sheet, int):
            pose_sheet = matrix.sheets[sheet]
        else:
            for pose_sheet in matrix.sheets:
                if pose_sheet.name == sheet:
                    break
            else:
                print("Could not find sheet {} in pose matrix", file=sys.stderr)
                sys.exit(1)
        code = pose_sheet[stage, pose_key].expand_code()
        if code is None:
            print("Specified pose matrix cell is blank", file=sys.stderr)
            sys.exit(1)
    else:
        with BASE_IN_PATH.joinpath(args.codefile + ".txt").open("r", encoding="utf-8") as f:
            code = KisekaeCode(f.read())

    if args.out_name is not None:
        outpath = BASE_OUT_PATH.joinpath(args.out_name)
    else:
        out_name = args.codefile
        outpath = BASE_OUT_PATH.joinpath(out_name)
    if args.disable_hair:
        outpath = outpath.joinpath("exported-nohair")
    else:
        outpath = outpath.joinpath("exported") 

    outpath.mkdir(exist_ok=True, parents=True)
    for child in outpath.iterdir():
        if child.is_file() and child.suffix == ".png":
            child.unlink()

    asyncio.run(
        main(
            code,
            args.parts,
            outpath,
            args
        )
    )
