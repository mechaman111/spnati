
# Disassembler Documentation

## Scripts

This system consists of two scripts:
- `extract_sprites.py` performs sprite disassembly.
- `autoalign_sprites.py` can reassemble sprites using data output by `extract_sprites`.

`kkl_client.py` and `kkl_import.py` are Python modules that provide functionality common to both scripts. They need to be co-located with the main scripts.

This document only describes `extract_sprites.py`.

## Folder Setup

Inputs to the disassembler (Kisekae codes and options) and outputs from the same (images and some other data) are organized within a "base directory". 

The base directory itself contains two subdirectories:
- The script will read codes containing models to disassemble from the `codes` subdirectory.
- The corresponding disassembled sprites from each code will be organized within the `images` subdirectory.

Output sprites will be placed into image folders based on the code file name. For ALL codes, the disassembler will further subdivide output sprites based on which character they come from.

Additionally, images will always be put into an `exported/` folder within the target output folder; this is useful for cases where you need to reference individual sprite images (e.g. in the Pose Maker or within epilogues).

I highly recommend adding a line to your character folder's `.gitignore` file (or creating one as necessary) that reads:
```
**/images/**/exported
```
to avoid potentially committing massive amounts of unused images.

Here's an example folder setup, using `base/` as the base directory:
```
base/
    - codes/
        - single-model.txt
        - two-characters.txt
        - two-named-characters.txt (see below for how this works)
    - images/
        - single-model/
            - exported/
                - <disassembled sprites: arm_left.png, arm_right.png, ...>
        - two-characters/
            - 1/
                - exported/
                    - <disassembled sprite images for slot 1>
            - 2/
                - exported/
                    - <disassembled sprite images for slot 2>
        - two-named-characters/
            - character_a/
                - exported/
                    - <disassembled sprite images...>
            - character_b/
                - exported/
                    - <disassembled images...>
```

## Options and Invocation

The disassembler accepts a number of optional command line flags. Most of these can be specified in two ways: a long form (prefixed by `--`), and a short form (prefixed by `-`). Some flags also take values, which will be listed below.

| Flag(s) | Possible Values | Description |
| - | - | - |
| `--align` | `default`, `static`, or `none` | Controls horizontal alignment of characters prior to disassembly. Set this to `none` to force the disassembler to use the horizontal position values from the input code. |
| `--align-z` | same as `align` | Controls depth alignment of characters prior to disassembly. As with `--align`, setting this to `none` will prevent the disassembler from attempting to center characters along the depth axis. |
| `--show-shadow` / `-s` | (none) | By default, the disassembler hides shadows during disassembly. Passing `-s` will show them instead. |
| `--zoom`/ `-z` | integer from 1-100 | Specifies the zoom value to use while exporting sprites. By default, sprites are exported at zoom 7 to match regular pose sprites. |
| `--camera-x`, `-x` | integer, possibly prefixed by `+/-` | Specifies the X position of the camera while importing. By default, this is calculated based on the `zoom` setting, but can be either fixed to a value directly by specifying a raw number, or offset from the default calculated position by using a number prefixed by `+` or `-`. |
| `--camera-y`, `-y` | same as `camera-x` | Specifies the Y position of the camera while importing. See `--camera-x`. |
| `--juice`, `-j` | integer from 0-100 | Pass `-j` to override the "juice" value of imported models. |
| `--names`, `-n` | 1 or more folder names | If passed when disassembling an ALL code, each exported character's images will be placed in subfolders folders named by this option. Each listed name corresponds to a character slot in the code: `--names a b c` will assign folder `a` to slot 1, `b` to slot 2, and `c` to slot 3, for example. |
| `--character`, `-c` | slot number from 1-9 | If passed when disassembling an ALL code, only the character in the given slot will be processed. |

Most of these options can also be specified within code files instead of being passed via command line; see below for details.

The general form to run the script looks like this:
```
$ python extract_scripts.py [options] <base path> <code file name without extension> <'all', or individual parts to disassemble>
```

For example: `python extract_scripts.py -z 40 base/ two-characters all`

## Code Files

Model codes to be processed by the disassembler are stored within plain-text code files, stored within the `codes/` subdirectory of the base directory.

Aside from a line containing the actual code to import during disassembly, code files can contain comments (prefixed by `#`) and lines listing settings to use when importing the code.

If provided, each setting must be contained on its own line with the format: `[setting_name value]`. In general, setting names correspond to command line flag names above, but with dashes replaced by underscores (`camera_y` instead of `camera-y`, etc.).

See the above table for info on what can be configured and valid values for each setting.

Here's an example of a code file used for a joint forfeit pose:
```
# OMari / Mari S. joint forfeit, Mari S. focus, regular pose 3
[names mari_setogaya, mari_omori]
[zoom 7]
[camera_y 80]
105***aa20.274.1.25.71 ... <rest of code>
```
(The last line should obviously list a full code, but it's been cut here for the sake of readability.)

In this case, slot 1 has been assigned a folder name of `mari_setogaya`, while slot 2 has been assigned the folder name `mari_omori`. Therefore, the disassembled sprite images for slot 1 would be placed under `images/<codefile-name>/mari_setogaya/exported`, and the images for slot 2 would be placed under `images/<codefile-name>/mari_omori/exported`.

Additionally, the camera will be forced to Y position 80 when importing this code; this particular code has both characters sitting down, so the camera needs to be moved to keep them from getting cut off at the bottom. The `[zoom 7]` line is technically redundant, but I like to include it for documentation purposes, essentially.

Specifying options within the code file instead of from the CLI makes it easy to ensure that sprites are always exported with consistent settings; this is particularly important when changing `[zoom]` settings. I also particularly recommend listing `[names]` for the characters within ALL code within the code file itself, instead of passing them via CLI.

## A Fully-Worked Example

For this example, let's assume we have an ALL code containing two characters posed together for a sitting dual forfeit: Mari (OMORI) in slot 1, and Heris in slot 2. We're also going to assume that our base directory, relative to the SPNATI repository root, is `opponents/mari/promari-forfeit`.

Within Mari's character folder, we create a new file at `promari-forfeit/codes/heris-normal-1.txt`, with contents:
```
[character_names mari, heris]
[zoom 7]
[camera_y +50]
105***aa23.249.1.4.21.12.299.1.45.98_ab_ac ... <rest of code>
```

To start the disassembly process, we can then run (from the top-level repository directory) the command:
```
$ python extract_sprites.py opponents/mari/promari-forfeit/ heris-normal-1 all
```

Each character within the code will be imported and disassembled one-by-one, at zoom level 7 and with the camera moved down slightly to compensate for both characters being seated.

Mari's model in slot 1 will be processed first, with the resulting sprites being placed in `promari-forfeit/images/heris-normal-1/mari/exported`. Likewise, the resulting sprites for Heris in slot 2 will be placed in `promari-forfeit/images/heris-normal-1/heris/exported`.

Once everything's been exported, you can then put things together, either in the image editor of your choice or within the Pose Maker.

If you're working with the Pose Maker (or with epilogues, for that matter), you're most likely only going to use a handful of disassembled parts from each character; in this case, I'd recommend moving the sprites you *do* use out of the `exported/` folders and into the per-character image folders. Note that exported "parts" use the same names across characters (e.g. `leg_left.png`), hence the need for separate per-character image folders.

The individual sprites used within the Pose Maker, then, might have the following source paths:
```
- promari-forfeit/images/heris-normal-1/mari/leg_left.png
- promari-forfeit/images/heris-normal-1/mari/body_arms_upper.png
- promari-forfeit/images/heris-normal-1/heris/body_arms_upper.png
- promari-forfeit/images/heris-normal-1/heris/forearm_left.png
...
```