from __future__ import print_function

import behaviour_parser as bp
import os
import os.path as osp
import stat
import sys

def iter_listing(opponents_base):
    listing_elem = bp.parse_listing(osp.join(opponents_base, 'listing.xml'))

    for individual in listing_elem.find('individuals').iter('opponent'):
        if 'status' not in individual.attributes:
            yield individual.text
        elif individual.attributes['status'] == 'testing':
            yield individual.text

def calc_image_space(opponents_base, opponent):
    opp_dir = osp.join(opponents_base, opponent)
    files = os.listdir(opp_dir)

    total = 0
    for filename in files:
        path = osp.join(opp_dir, filename)
        ext = osp.splitext(path)[1].lower()

        if not osp.isfile(path) or (ext not in ['.gif', '.png', '.jpg']):
            continue

        total += osp.getsize(path)

    return total


if __name__ == '__main__':
    opp_base = sys.argv[1]

    total_image_size = 0
    per_opponent = {}
    for opponent in iter_listing(opp_base):
        sz = calc_image_space(opp_base, opponent)
        per_opponent[opponent] = sz
        total_image_size += sz

    print("== Image Space Usage by Character ==")

    for character in sorted(per_opponent.keys(), key=lambda k: per_opponent[k], reverse=True):
        print("{}: {:.2f} MB ({} bytes) -> {:.3%} of total".format(
            character, float(per_opponent[character]) / 1000000, per_opponent[character], float(per_opponent[character]) / float(total_image_size)
        ))

    print("Total image size: {:.2f} MB ({} bytes)".format(
        float(total_image_size) / 1000000, total_image_size
    ))
