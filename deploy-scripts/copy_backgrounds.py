#!/usr/bin/env python3

# Copies backgrounds according to their status attributes in backgrounds.xml.

import os
import os.path as osp
import shutil
import sys

from bs4 import BeautifulSoup

if len(sys.argv) > 2:
    base_path = sys.argv[1]
    dest_path = sys.argv[2]
else:
    base_path = os.getcwd()
    dest_path = sys.argv[1]


backgrounds_file = osp.join(base_path, "backgrounds.xml")

with open(backgrounds_file, "r", encoding="utf-8") as f:
    soup = BeautifulSoup(f.read(), "html.parser")

background_images = {}
for background in soup.find_all(name="background", recursive=True):
    if background.status is not None and str(background.status.string) == "offline":
        continue

    for src_tag in background.find_all("src", recursive=True):
        bg_rel_path = str(src_tag.string)

        bg_src_path = osp.join(base_path, bg_rel_path)
        bg_dest_path = osp.join(dest_path, bg_rel_path)
        bg_dest_dir = osp.dirname(bg_dest_path)

        os.makedirs(bg_dest_dir, exist_ok=True)
        shutil.copyfile(bg_src_path, bg_dest_path)

        try:
            background_images.setdefault(background["id"], []).insert(0, bg_rel_path)
        except KeyError:
            pass

# Adjust spni.css to change the default background during load to the
# specified default-background.
# This optimizes page load slightly by directly loading any specified event
# background, instead of loading the default inventory background image first,
# then loading the event background.

FALLBACK_INVENTORY_BACKGROUND = "img/backgrounds/inventory.png"
CURRENT_DEFAULT_BACKGROUND = [FALLBACK_INVENTORY_BACKGROUND]

with open(osp.join(dest_path, "config.xml"), "r", encoding="utf-8") as config:
    conf_soup = BeautifulSoup(config.read(), "html.parser")
    default_bg = conf_soup.find("default-background")

    if default_bg is not None:
        default_bg = str(default_bg.string)

        # If default_bg is not in background_images, this will throw a
        # KeyError.
        # This is good, because this only happens if there's an error in
        # config.xml itself.
        CURRENT_DEFAULT_BACKGROUND = background_images[default_bg]


def css_url(p):
    return 'url("../{}")'.format(p)


search_str = css_url(FALLBACK_INVENTORY_BACKGROUND)
replace_str = ", ".join(map(css_url, CURRENT_DEFAULT_BACKGROUND))

if search_str != replace_str:
    css_path = osp.join(dest_path, "css/spni.css")

    with open(css_path, "r", encoding="utf-8") as f:
        contents = f.read().replace(search_str, replace_str)

    with open(css_path, "w", encoding="utf-8") as f:
        f.write(contents)
