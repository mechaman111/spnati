import multiprocessing as mp
import subprocess as sp
import os
import os.path as osp
import sys
from PIL import Image
import math
import glob

resize_images = False
resize_rgba = False
target_height = 800  # will maintain aspect ratio

use_pngquant = True

# Test results with images in 'uravity/':
# Zopfli-only:
# >    Successfully compressed 149 files
# >    Total bytes: 8445380 --> 8212601 (saved 2.76% overall)
# >    Average space saved per image: 4.21%
# PNGQuant + ZopfliPNG:
# >    Successfully compressed 149 files
# >    Total bytes: 8445328 --> 8212615 (saved 2.76% overall)
# >    Average space saved per image: 4.11%
# Resizing (RGBA) + PNGQuant + ZopfliPNG:
# >    Successfully compressed 149 files
# >    Total bytes: 8445380 --> 7574349 (saved 10.31% overall)
# >    Average space saved per image: 17.05%

# Test results with images in 'vriska/':
# Zopfli-only:
# >    Successfully compressed 171 files
# >    Total bytes: 12562245 --> 12134445 (saved 3.41% overall)
# >    Average space saved per image: 3.04%
# PNGQuant + ZopfliPNG:
# >    Successfully compressed 171 files
# >    Total bytes: 12562245 --> 10369929 (saved 17.45% overall)
# >    Average space saved per image: 10.33%
# Resizing (RGBA) + PNGQuant + ZopfliPNG:
# >    Successfully compressed 171 files
# >    Total bytes: 12562245 --> 7673884 (saved 38.91% overall)
# >    Average space saved per image: 33.34%
# Resizing (Palette) + PNGQuant + ZopfliPNG:
# >    Successfully compressed 171 files
# >    Total bytes: 12562245 --> 5303937 (saved 57.78% overall)
# >    Average space saved per image: 55.72%

# Observations with Vriska's images:
#  Some of Vriska's images compressed really well (>= 60% space saved),
#  most likely because they were saved with greater than 8-bit palette depth, which PNGQuant is really, really good at compressing.
#
#  Most of Vriska's images, however, weren't as compressible, and averaged
#  around 25-30% space saved. This is still really significant, however, and quickly
#  adds up for 100s of images.
#
#  Also: PNGQuant + Zopfli seems to work better than tinypng.com when compressing
#  those "somewhat-compressible" images. On the one image I tested (vriska/0-ACalm.png),
#  tinypng achieved a 1% reduction in filesize, while PNGQuant+ZopfliPNG
#  managed to reduce it by 3%. Add resizing on top of that and you have a winning combination.

# About Resizing:
#  As it turns out, most of the opponent images we have are far too large for
#  the browser viewport; the browser thus resizes them internally before displaying them.
#  So, this script can preemptively resize them to significantly cut down on filesizes.
#
#  There are actually two separate ways we can do resizing:
#  - We can resize after converting the image into the RGBA mode. This produces images
#    with good interpolation and antialiasing that are pretty much indistinguishable from the originals in the browser.
#    On the other hand, we lose out on a lot of potential space savings by doing this.
#  - Alternatively, we can resize without converting the image from the palette mode.
#    This produces images with noticeably jagged edges and lines (little to no antialiasing).
#    On the other hand, we get the absolute highest amount of space savings by doing this.
#
#  You can see the results with Vriska's images above.
#  Resizing with the palette results in a total file size 1.5x smaller than resizing with RGBA;
#  however, due to the noticeable drop in image quality that palette-resizing creates,
#  this script always converts to RGBA before doing image resizing.

def compress_file(f):
    original_size = os.stat(f).st_size

    # Resizing:
    if resize_images:
        try:
            im = Image.open(f)
            if im.height == 1400: # all (most?) character images are 1400 pixels tall; ignore everything else when resizing
                if resize_rgba:
                    im = im.convert('RGBA')
                target_width = math.ceil((target_height / im.height) * im.width)
                im.thumbnail((target_width, target_height), Image.LANCZOS)
                im.save(f, 'PNG')
            else:
                print("{} doesn't seem to be a character image-- not resizing".format(f))
        except IOError:
            print("Could not resize {}".format(f))

    # pngquant pass:
    if use_pngquant:
        sp.run(
            ['pngquant', '--strip', '--speed', '1', '--skip-if-larger', '--force', '--output', f, '--', f],
            stdout=sp.DEVNULL
        )

    # zopfli-fication:
    ret = sp.run(
        ['zopflipng', '-y', '--lossy_transparent', '--lossy_8bit', f, f],
        stdout=sp.DEVNULL
    )

    if ret.returncode == 0:
        new_size = os.stat(f).st_size
        print("Compressed {} ({:n} --> {:n} bytes, saved {:.2%})".format(f, original_size, new_size, 1 - (new_size / original_size)))

        return new_size, original_size

    return None, None

if __name__ == '__main__':
    if len(sys.argv) < 2:
        print("USAGE: compress_images.py [file globs...]")
        sys.exit()
    
    
    target_files = []
    for arg in sys.argv[1:]:
        target_files.extend(glob.iglob(arg))

    with mp.Pool(processes=os.cpu_count()*2) as p:
        print("Compressing {:n} PNG files".format(len(target_files)))

        results = p.map(compress_file, target_files)

        original_total_size = 0
        new_total_size = 0
        n_files_successful = 0
        avg_space_savings = 0

        for new_size, original_size in results:
            if new_size is not None and original_size is not None:
                n_files_successful += 1

                original_total_size += original_size
                new_total_size += new_size

                space_saved = 1 - (new_size / original_size)
                avg_space_savings += space_saved

        avg_space_savings /= n_files_successful

        print("Successfully compressed {:n} files".format(n_files_successful))
        print("Total bytes: {:n} --> {:n} (saved {:.2%} overall)".format(original_total_size, new_total_size, 1 - (new_total_size / original_total_size)))
        print("Average space saved per image: {:.2%}".format(avg_space_savings))
