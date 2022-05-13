"""
Support library for working with KisekaeLocal.

This module contains functions for importing Kisekae codes using KisekaeLocal,
and functions for processing the generated image files.

It can also serve as a utility for automatically importing sets of codes from
CSV files, plain text files, or directly from the command line.
"""

import argparse
import csv
import os
import os.path as osp
from pathlib import Path
import re
import sys
import time

import numpy as np
from scipy import ndimage
from PIL import Image


SETUP_STRING_33 = (
    "33***bc185.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]ua1.0.0.0_ub_uc7.0.30_ud7.0"
)
SETUP_STRING_36 = "36***bc185.500.0.0.1_ga0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0_ub_u0_v0_uc7.0.30_ud7.0"
SETUP_STRING_40 = "40***bc185.500.0.0.1*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v0_uc7.2.24_ud7.8"
SETUP_STRING_68 = "68***ba50_bb6.0_bc410.500.8.0.1.0_bd6_be180_ad0.0.0.0.0.0.0.0.0.0_ae0.3.3.0.0*0*0*0*0*0*0*0*0#/]a00_b00_c00_d00_w00_x00_e00_y00_z00_ua1.0.0.0.100_uf0.3.0.0_ue_ub_u0_v00_ud7.8_uc7.2.24"
SEPARATOR = "#/]"

CODE_SPLIT_REGEX = r"(\d+?)\*\*\*?([^\#\/\]]+)(?:\#\/\](.+))?"


class KisekaeComponent(object):
    def __init__(self, data=None):
        """
        Represents a subcomponent of a Kisekae character or scene.
        
        Attributes:
            id (str): An ID identifying this subcomponent's type.
            prefix (str): A prefix identifying this subcomponent.
            attributes (list of str): The attributes associated with this component.
        """

        if isinstance(data, KisekaeComponent):
            self.id = data.id
            self.prefix = data.prefix
            self.attributes = data.attributes.copy()
        elif isinstance(data, str):
            if data[1].isalpha():
                self.id = data[0:2]  # code is 2 letters
                self.prefix = data[0:2]
            else:
                self.id = data[0]
                self.prefix = data[0:3]  # code is 1 letter + 2 digits
                self.index = int(data[1:3])

            self.attributes = data[len(self.prefix) :].split(".")
        else:
            raise ValueError(
                "`data` must be either str or KisekaeComponent, not "
                + type(data).__name__
            )

    def __eq__(self, other):
        if isinstance(other, KisekaeComponent):
            if (
                self.id != other.id
                or self.prefix != other.prefix
                or len(self.attributes) != len(other.attributes)
            ):
                return False

            for x, y in zip(self.attributes, other.attributes):
                if x != y:
                    return False

            return True
        elif isinstance(other, str):
            return str(self) == other
        else:
            raise NotImplementedError(
                "KisekaeComponents can only be compared to other Components or strings."
            )

    def __len__(self):
        return len(self.attributes)

    def __iter__(self):
        return self.attributes.__iter__()

    def __getitem__(self, key):
        return self.attributes[key]

    def __setitem__(self, key, val):
        self.attributes[key] = str(val)

    def __delitem__(self, key):
        del self.attributes[key]

    def __contains__(self, item):
        return self.attributes.__contains__(self, item)

    def __str__(self):
        return self.prefix + ".".join(self.attributes)


class KisekaeCharacter(object):
    def __init__(self, character_data=None):
        """
        Represents a collection of subcodes.
        
        Attributes:
            subcodes (list of KisekaeComponent): The subcodes contained within this object.
        """

        self.subcodes = []

        if isinstance(character_data, str):
            for subcode in character_data.split("_"):
                self.subcodes.append(KisekaeComponent(subcode))
        elif isinstance(character_data, KisekaeCharacter):
            for subcode in character_data.subcodes:
                self.subcodes.append(KisekaeComponent(subcode))
        else:
            raise ValueError(
                "`character_data` must be either str or KisekaeCharacter, not "
                + type(data).__name__
            )

    def __str__(self):
        return "_".join(str(sc) for sc in self.subcodes)

    def __len__(self):
        return len(self.subcodes)

    def __iter__(self):
        return self.subcodes.__iter__()

    def __getitem__(self, key):
        if isinstance(key, int):
            return self.subcodes[key]
        elif isinstance(key, str):
            v = self.find(key)
            if v is None:
                raise KeyError("No subcode with ID {:s} in this character".format(key))
            return v
        else:
            raise ValueError(
                "Index value must be either an int or a subcode ID string."
            )

    def __setitem__(self, key, val):
        if not isinstance(val, KisekaeComponent):
            raise ValueError("Assignment value must be a KisekaeComponent.")

        if isinstance(key, int):
            self.subcodes[key] = val
        elif isinstance(key, str):
            idx = None

            for i, sc in enumerate(self.subcodes):
                if sc.prefix == key:
                    idx = i
                    break
            else:
                raise KeyError("No subcode with ID {:s} in this character".format(key))

            self.subcodes[idx] = val

    def __delitem__(self, key):
        if isinstance(key, int):
            del self.subcodes[key]
        elif isinstance(key, str):
            idx = None

            for i, sc in enumerate(self.subcodes):
                if sc.prefix == key:
                    idx = i
                    break
            else:
                raise KeyError("No subcode with ID {:s} in this character".format(key))

            del self.subcodes[idx]

    def __contains__(self, item):
        if isinstance(item, KisekaeComponent):
            return self.subcodes.__contains__(self, item)
        elif isinstance(item, str):
            return self.find(item) is not None
        else:
            raise ValueError(
                "Item must be either a KisekaeComponent or a subcode ID string."
            )

    def find(self, subcode_prefix):
        """
        Find the first inner KisekaeComponent with the given `subcode_prefix`.
        """

        for sc in self.subcodes:
            if sc.prefix == subcode_prefix:
                return sc

    def iter(self, subcode_prefix):
        """
        Iterate over all inner KisekaeComponents with the given `subcode_prefix`
        """

        return filter(lambda sc: sc.prefix.startswith(subcode_prefix), self.subcodes)


class KisekaeCode(object):
    def __init__(self, code=None, version=97):
        """
        Represents an entire importable Kisekae code, possibly containing
        character data and scene data.
        
        Attributes:
            version (int): The version of Kisekae used to generate this code.
            scene (KisekaeCharacter): Container for scene data and attributes.
            characters (list of KisekaeCharacter): List of characters contained in the code.
        """

        self.version = version
        self.characters = []
        self.scene = None

        if isinstance(code, KisekaeCode):
            self.version = code.version

            for character in code:
                self.characters.append(KisekaeCharacter(character))

            if code.scene is not None:
                self.scene = KisekaeCharacter(code.scene)

            return
        elif isinstance(code, KisekaeCharacter):
            self.characters.append(KisekaeCharacter(code))
            return
        elif isinstance(code, str):
            m = re.match(CODE_SPLIT_REGEX, code.strip())
            if m is None:
                return

            version, character_data, scene_data = m.groups()

            self.version = int(version)
            self.characters = []

            if scene_data is not None:
                self.scene = KisekaeCharacter(scene_data)
            else:
                self.scene = None

            for character in character_data.split("*"):
                if character == "0":
                    continue

                self.characters.append(KisekaeCharacter(character))
        else:
            raise ValueError(
                "`code` must be either a KisekaeCode, KisekaeCharacter, or str, not "
                + type(data).__name__
            )

    def __str__(self):
        ret = str(self.version) + "**"

        if self.scene is not None:
            for i in range(9):
                if i >= len(self.characters):
                    ret += "*0"
                else:
                    ret += "*" + str(self.characters[i])

            ret += SEPARATOR + str(self.scene)
        else:
            ret += str(self.characters[0])

        return ret

    def __len__(self):
        return len(self.characters)

    def __iter__(self):
        return self.characters.__iter__()

    def __getitem__(self, key):
        return self.characters[key]

    def __setitem__(self, key, val):
        self.characters[key] = str(val)

    def __delitem__(self, key):
        del self.characters[key]

    def __contains__(self, item):
        return self.characters.__contains__(self, item)


def disable_character_motion(character):
    """
    Disables automatic motion for a character.
    
    Args:
        character (KisekaeCharacter): The character to modify.
    """

    try:
        character["ad"].attributes = ["0"] * 10
    except KeyError:
        pass

    try:
        character["ae"].attributes = ["0", "3", "3", "0", "0"]
    except KeyError:
        pass

    return character


def close_character_vagina(character):
    """
    Closes / un-spreads a Kisekae character's vagina.
    
    Args:
        character (KisekaeCharacter): The character to modify.
    """

    try:
        character["dc"][5] = "0"
    except KeyError:
        pass

    return character


def preprocess_character_code(
    in_code,
    blush=-1,
    anger=-1,
    juice=-1,
    remove_motion=True,
    close_vagina=True,
    **kwargs
):
    code = KisekaeCode(in_code)

    if remove_motion:
        disable_character_motion(code.characters[0])

    if close_vagina:
        close_character_vagina(code.characters[0])

    blush = int(blush)
    anger = int(anger)
    juice = int(juice)

    try:
        code[0]["bc"][1] = int(kwargs.get("z_depth", 500))
    except KeyError:
        pass

    if blush >= 0:
        try:
            code[0]["gc"][0] = blush
        except KeyError:
            pass

    if anger >= 0:
        try:
            code[0]["gc"][1] = anger
        except KeyError:
            pass

    if juice >= 0:
        try:
            code[0]["dc"][0] = juice
        except KeyError:
            pass

    return code


def _get_wine_kkl_directory():
    # look for kkl path under Wine:
    username = Path.home().stem
    wine_path = (
        Path.home()
        / ".wine"
        / "drive_c"
        / "users"
        / username
        / "Application Data"
        / "kkl"
        / "Local Store"
    )

    return wine_path


def get_kkl_directory():
    """
    Retrieve the path to the main KisekaeLocal application directory.
    """

    if sys.platform == "darwin":
        native_path = (
            Path.home() / "Library" / "Application Support" / "kkl" / "Local Store"
        )
        if native_path.is_dir():
            return native_path

        p = _get_wine_kkl_directory()
        if p.is_dir():
            return p
        else:
            raise FileNotFoundError("Could not find path to KKL.")
    elif sys.platform.startswith("linux"):
        p = _get_wine_kkl_directory()
        if p.is_dir():
            return p
        else:
            raise FileNotFoundError("Could not find path to KKL.")
    else:  # We're on windows, presumably
        return Path(os.getenv("APPDATA")) / "kkl" / "Local Store"


def process_kkl_code(code, scene_name):
    """
    Import an image into KisekaeLocal.
    Returns a Path object to the output image once it has been generated.
    
    Args:
        code (str or KisekaeCode): The Kisekae code to import.
        scene_name (str): A pose name to use when importing.
    """

    input_path = get_kkl_directory().joinpath(scene_name + ".txt")
    output_path = get_kkl_directory().joinpath(scene_name + ".png")

    # remove the input and output files if they already exist
    if output_path.is_file():
        output_path.unlink()

    if input_path.is_file():
        input_path.unlink()

    sys.stdout.write("Importing: {:s}... ".format(scene_name))
    sys.stdout.flush()

    with input_path.open("w", encoding="utf-8") as f:
        f.write(str(code))

    # wait for KKL to process the file
    # print("Waiting for output file to be generated...")
    while input_path.is_file() or not output_path.is_file():
        time.sleep(0.1)

    return output_path


def open_image_file(path):
    """
    Attempt to open an image file generated by KKL.
    """

    retry_time_limit = 10  #  try for at most 10 seconds before saying there's a problem
    retry_interval = (
        0.2  #  re-check for the existance of the image every 200 milliseconds
    )
    retry_limit = int(retry_time_limit // retry_interval)  # try 50 times

    for retry in range(retry_limit):
        try:
            image_file = Image.open(path)
            return image_file
        except IOError:
            if retry >= retry_limit - 1:
                raise
            time.sleep(retry_interval)


def get_crop_box(width=600, height=1400, center_x=1000, margin_y=15):
    """
    Calculate a cropping rectangle.
    
    Args:
        width (int): The width of the output image.
        height (int): The height of the output image.
        center_x (int): Position along the X-axis for the center of the rectangle.
        margin_y (int): How far down along the Y-axis to position the rectangle.
        
    Returns:
        A cropping box as a 4-tuple, suitable to be passed to Image.crop().
    """

    left = center_x - int(width // 2)
    right = center_x + int(width // 2)
    upper = margin_y
    lower = height + margin_y

    return (left, upper, right, lower)


def auto_crop_box(image, margin_y=15):
    """
    Automatically calculate a centered cropping rectangle from an image's bounding box.
    
    Args:
        margin_y (int): How much empty margin space to leave at the bottom of the image, in px.
        
    Returns:
        A cropping box as a 4-tuple, suitable to be passed to Image.crop().    
    """

    arr = np.array(image)
    intensity = np.sum(arr, axis=2)
    nonzero = np.greater(intensity, 0).T

    centroid = ndimage.measurements.center_of_mass(nonzero)

    left, top, right, bottom = image.getbbox()

    out_height = bottom - top
    if out_height < 1400:
        out_height = 1400

    com_x = centroid[0]
    center_x = int((right + left) / 2)
    shift_x = int(center_x - com_x)  # negative:right, positive:left

    out_width = (right - left) + 2
    if out_width < 600:
        out_width = 600

    crop_left = int(com_x - (out_width // 2))
    crop_right = int(com_x + (out_width // 2))

    if shift_x < 0:
        # Add extra space to the left side
        crop_left -= shift_x
    elif shift_x > 0:
        crop_right += shift_x

    if crop_left > left:
        crop_left = left - 1
        crop_right = crop_left + out_width

    if crop_right < right:
        crop_right = right + 1
        crop_left = crop_right - out_width

    crop_top = (bottom + margin_y) - out_height
    crop_bottom = bottom + margin_y

    return (crop_left, crop_top, crop_right, crop_bottom)


def import_character(import_code, pose_name="imported"):
    """
    Import a code into KisekaeLocal and open the generated image file.
    
    Args:
        import_code (str or KisekaeCode): The Kisekae code to import.
        pose_name (str): A pose name to use when importing.
    """

    output_path = process_kkl_code(import_code, pose_name)

    img = open_image_file(output_path)
    img.load()

    output_path.unlink()

    return img


def import_and_unlink(import_code, scene_name="temp"):
    """
    Import the specified `import_code` but immediately delete the generated image.
    This is useful for scene resets, quickly opening characters for editing in KKL, etc.
    
    Args:
        import_code (str or KisekaeCode): The Kisekae code to import.
        pose_name (str): A pose name to use when importing.
    """

    output_path = process_kkl_code(import_code, scene_name)

    retry_time_limit = 10  #  try for at most 10 seconds before saying there's a problem
    retry_interval = (
        0.2  #  re-check for the existance of the image every 200 milliseconds
    )
    retry_limit = int(retry_time_limit // retry_interval)  # try 50 times

    for retry in range(retry_limit):
        try:
            output_path.unlink()
            break
        except PermissionError:
            if retry >= retry_limit - 1:
                raise
            time.sleep(retry_interval)

    sys.stdout.write("done.\n")
    sys.stdout.flush()


def setup_kkl_scene(z_depth=500):
    """
    Send a scene reset code to KKL to prepare for image importing.
    This will clear away unnecessary characters, backgrounds, objects, etc.
    """

    reset_code = KisekaeCode(SETUP_STRING_68)
    reset_code[0]["bc"][1] = z_depth

    import_and_unlink(reset_code, "scene_setup")


def crop_and_save(img, crop_box, dest_filename):
    """
    Crop an image and save it.
    This is a convenience function.
    
    Args:
        img (PIL.Image): The image object to crop and save.
        crop_box (4-tuple): The cropping rectangle.
        dest_filename (pathlib.Path or str): The destination filename to save to.
    """

    cropped_img = img.crop(crop_box)
    cropped_img.save(dest_filename)
    cropped_img.close()
    img.close()


def read_ce_pose_file(path):
    """
    Read a pose file in the format used by the CE, discarding cropping information.
    """

    poses = {}

    with open(path, "r", encoding="utf-8") as f:
        for line in f:
            try:
                pose_name, code = line.split("=", 1)
            except ValueError:
                continue

            poses[pose_name.strip()] = code.strip()

    return poses


def process_single(code, dest, **kwargs):
    """
    Import and process a Kisekae code.
    
    Args:
        code (str or KisekaeCode): The Kisekae code to import.
        dest (str or pathlib.Path): The destination to save to. If the destination exists and is a directory, the image will be saved here as `out.png`.
            Otherwise, it will be treated as a filename to save to.
    
    Kwargs:
        remove_motion (bool, optional): If True (default), then a code fragment will be automatically appended to the imported codes to disable unwanted breast motion.
            This may potentially override settings from the imported codes themselves, so be wary.
        auto_crop (bool, optional): If True, then cropping boxes will be automatically calculated based on the generated image bounding boxes.
            The `width`, `height`, and `center_x` kwargs will be ignored.
        width (int): The width of the generated images (when using manual cropping).
        height (int): The height of the generated images (when using manual cropping).
        center_x (int): The position along the X-axis around which to center the generated images (when using manual cropping).
        margin_y (int): When using manual cropping, this is how far down to position the cropping box.
            When using automatic cropping, this controls how much empty space to add at the bottom of the generated images.
    """

    dest = Path(dest)

    if dest.is_dir():
        dest_filename = dest.joinpath("out.png")
    else:
        dest_filename = dest

    if kwargs.get("do_setup", True):
        setup_kkl_scene(z_depth=kwargs.get("z_depth", 500))

    process_code = preprocess_character_code(code, **kwargs)
    kkl_output = import_character(process_code, dest_filename.stem)

    # kkl_output.save(str(dest_filename.parent.joinpath(dest_filename.stem + '.debug.png')))

    if dest_filename.is_file():
        dest_filename.unlink()

    margin_y = int(kwargs.get("margin_y", 15))

    if kwargs.get("auto_crop", True):
        crop_box = auto_crop_box(kkl_output, margin_y)
    else:
        width = int(kwargs.get("width", 600))
        height = int(kwargs.get("height", 1400))
        center_x = int(kwargs.get("center_x", 1000))
        crop_box = get_crop_box(width, height, center_x, margin_y)

    crop_and_save(kkl_output, crop_box, dest_filename)

    sys.stdout.write("done.\n")
    sys.stdout.flush()


def process_csv(infile, dest_dir, **kwargs):
    """
    Import and process a set of Kisekae codes listed in a CSV file.
    
    Args:
        infile (pathlib.Path): The CSV file to read.
        dest_dir (pathlib.Path): The destination folder to save to.
    
    Kwargs:
        stage (list of int, optional): If set, then only codes for these stages will be imported.
        pose (list of str, optional): If set, then only codes for these poses will be imported.
    """

    setup_kkl_scene(z_depth=kwargs.get("z_depth", 500))
    with infile.open("r", encoding="utf-8", newline="") as f:
        reader = csv.DictReader(f, restval="")
        for row in reader:
            if "enabled" in row and row["enabled"].strip().lower() == "false":
                continue

            if "stage" not in row or "code" not in row or "pose" not in row:
                continue

            if len(row["stage"]) <= 0 or len(row["code"]) <= 0 or len(row["pose"]) <= 0:
                continue

            # normalize pose name:
            row["pose"] = re.sub(r"[^\w\-\_]", "", row["pose"]).lower().strip()

            stage = int(row["stage"])

            if (
                kwargs["stage"] is not None
                and len(kwargs["stage"]) > 0
                and stage not in kwargs["stage"]
            ):
                continue

            if (
                kwargs["pose"] is not None
                and len(kwargs["pose"]) > 0
                and row["pose"] not in kwargs["pose"]
            ):
                continue

            pose_name = "{:d}-{:s}".format(stage, row["pose"])
            dest_filename = dest_dir.joinpath(pose_name + ".png")

            row["remove_motion"] = (
                row.get("remove_motion", "").strip().lower() != "false"
            )
            row["close_vagina"] = row.get("close_vagina", "").strip().lower() != "false"

            process_single(dest=dest_filename, do_setup=False, **row)


def process_file(infile, dest_dir, **kwargs):
    """
    Import and process a set of Kisekae codes listed in a plain-text file.
    
    Args:
        infile (pathlib.Path): The text file to read.
        dest_dir (pathlib.Path): The destination folder to save to.
    
    Kwargs:
        remove_motion (bool, optional): If True (default), then a code fragment will be automatically appended to the imported codes to disable unwanted breast motion.
            This may potentially override settings from the imported codes themselves, so be wary.
        auto_crop (bool, optional): If True, then cropping boxes will be automatically calculated based on the generated image bounding boxes.
            The `width`, `height`, and `center_x` kwargs will be ignored.
        width (int): The width of the generated images (when using manual cropping).
        height (int): The height of the generated images (when using manual cropping).
        center_x (int): The position along the X-axis around which to center the generated images (when using manual cropping).
        margin_y (int): When using manual cropping, this is how far down to position the cropping box.
            When using automatic cropping, this controls how much empty space to add at the bottom of the generated images.
    """

    dest_dir = Path(dest_dir)

    setup_kkl_scene(z_depth=kwargs.get("z_depth", 500))
    with open(infile, "r", encoding="utf-8") as f:
        for line in f:
            try:
                pose_name, code = line.split("=", 1)
            except ValueError:
                continue

            pose_name = pose_name.strip()
            code = code.strip()

            dest_filename = dest_dir.joinpath(pose_name + ".png")

            process_single(code, dest=dest_filename, do_setup=False, **kwargs)


if __name__ == "__main__":
    parser = argparse.ArgumentParser(
        description="Imports a pose into KisekaeLocal, and saves an automatically cropped image."
    )
    parser.add_argument("--width", "-w", default=600, help="Output image width.")
    parser.add_argument("--height", "-l", default=1400, help="Output image height.")
    parser.add_argument(
        "--center-x",
        "-x",
        default=1000,
        help="X position to center the output image around.",
    )
    parser.add_argument(
        "--margin-y",
        "-y",
        default=15,
        help="Number of margin pixels from top when cropping output image.",
    )
    parser.add_argument(
        "--z-depth", "-z", default=500, help="Depth at which to import characters."
    )
    parser.add_argument(
        "--manual-crop",
        "--manual",
        "-m",
        action="store_false",
        dest="auto_crop",
        help="Do not automatically calculate crop with bounding box.",
    )
    parser.add_argument(
        "--stage",
        nargs="*",
        type=int,
        help="(CSV-only) Import codes for a particular stage or set of stages only.",
    )
    parser.add_argument(
        "--pose",
        nargs="*",
        help="(CSV-only) Import codes for a particular pose or set of poses only.",
    )
    parser.add_argument(
        "--no-remove-motion",
        action="store_false",
        dest="remove_motion",
        help="Do not automatically set motion parameters to 'Manual' on importing.",
    )
    parser.add_argument(
        "--no-close-vagina",
        action="store_false",
        dest="close_vagina",
        help="Do not automatically close the flaps of a female character's vagina on importing.",
    )
    parser.add_argument("--file", "-f", help="Load codes from text file.")
    parser.add_argument("--csv", "-c", help="Load codes from a CSV file.")
    parser.add_argument(
        "--code",
        "-i",
        help="Load code as command line argument. [dest_dir] is the destination image path in this case.",
    )
    parser.add_argument("dest_dir", help="Destination directory to output to.")
    args = parser.parse_args()

    kwargs = vars(args)

    dest_dir = Path(args.dest_dir).resolve()
    del kwargs["dest_dir"]

    if args.csv is not None:
        process_csv(Path(args.csv), dest_dir, **kwargs)
    elif args.file is not None:
        process_file(Path(args.file), dest_dir, **kwargs)
    elif args.code is not None:
        code = args["code"]
        del kwargs["code"]
        process_single(code, dest_dir, **kwargs)
    else:
        raise ValueError("Must provide at least one of --csv, --file, or --code.")
