#!/usr/bin/bash
# cd to repo root first
cd $(git rev-parse --show-toplevel)
git ls-files -m -o --exclude-standard -- opponents/mari opponents/reskins/mari_* | grep png | xargs ./tools/pngquant -f --ext .png --skip-if-larger --