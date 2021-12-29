#!/bin/bash -e

mkdir -p .public/opponents .public/img
cp -r css fonts js player index.html version-info.xml backgrounds.xml events.xml cards.xml .public

# Copy non-recursively to exclude the backgrounds folder.
cp img/* .public/img

# Copy card images recursively.
cp -r img/cards .public/img

# Set build timestamp in version-info.xml.
# `date +%s` returns timestamp in seconds; add %N and cut to include milliseconds.
sed "s/__BUILD_TIMESTAMP/$(date +%s%N | cut -b1-13)/g" version-info.xml > .public/version-info.xml

sed "s/__CI_COMMIT_SHA/${CI_COMMIT_SHA}/g; s/__VERSION/${VERSION}/g" prod-config.xml > .public/config.xml
cp opponents/listing.xml .public/opponents
cp opponents/general_collectibles.xml .public/opponents

# Copy online background images and set initial background to display during
# loading.
python3 deploy-scripts/copy_backgrounds.py .public/

# tar may be the easiest way to copy an arbitrary
# list of files, keeping the directory structure.
# Include *.js and *.css to accommodate Monika.
find `python opponents/list_opponents.py` -regextype egrep -iregex '.*\.(png|gif|jpe?g|xml|js|css|[ot]tf|woff2?)' | tar -cT - | tar -C .public -x

# Combine all collectibles.xml files for deployed opponents to speed up loading
# for the online version.
COLLECTIBLES_INDEX_PATH=$(python3 deploy-scripts/compile_xml_index.py .public 'opponents/*/collectibles.xml' opponents/collectibles_index.xml)
ESCAPED_COLLECTIBLES_INDEX_PATH=$(printf '%s\n' "$COLLECTIBLES_INDEX_PATH" | sed -e 's/[\/&]/\\&/g')
sed "s/__COLLECTIBLES_INDEX/${ESCAPED_COLLECTIBLES_INDEX_PATH}/g" js/spniCore.js > .public/js/spniCore.js

# Copy alternate costume files for deployment.
python3 deploy-scripts/copy_alternate_costumes.py .public/ ./ all

# Rename JS and core game CSS for cache-busting purposes.
python3 deploy-scripts/cache_bust.py .public/

python3 opponents/fill_linecount_metadata.py .public/opponents
python3 deploy-scripts/fill_update_timestamps.py .public/opponents opponents
python opponents/gzip_dialogue.py .public/opponents/*/behaviour.xml
python opponents/analyze_image_space.py .public/opponents
