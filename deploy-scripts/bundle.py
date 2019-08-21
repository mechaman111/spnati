from bs4 import BeautifulSoup
import json
import os.path as osp
from pathlib import Path
import sys
import zipfile

CORE_DIRS = [
    'attachments',
    'css',
    'fonts',
    'img',
    'js',
    'player',
]

MAX_BUNDLE_OPP_SIZE = 1048576 * 450 # 450 MiB of opponents per bundle

SPNATI_BASE_DIR = Path(sys.argv[1])
OUTPUT_DIR = Path(sys.argv[2])
MODE = sys.argv[3]

def read_listing():
    """
    Read listing.xml and extract a list of opponents, organized by status.
    """

    with SPNATI_BASE_DIR.joinpath('opponents', 'listing.xml').open('r', encoding='utf-8') as f:
        soup = BeautifulSoup(f.read(), "html.parser")
    
    roster = {}

    for opp in soup.find_all('opponent', recursive=True):
        if 'status' in opp.attrs:
            status = opp.attrs['status']
        else:
            status = 'online'
        
        if status not in roster:
            roster[status] = []

        roster[status].append(str(opp.string))
    
    return roster


def recursive_list(path):
    for child in path.iterdir():
        if child.is_file():
            yield child
        elif child.is_dir():
            yield from recursive_list(child)


def recursive_folder_size(path):
    sz = 0
    
    for child in recursive_list(path):
        stat = child.stat()
        sz += stat.st_size
    
    return sz


def tally_opponent_filesizes(opponents):
    """
    Calculate the size of a given set of opponent folders.
    """

    sizes = {}
    for opp in opponents:
        opp_path = SPNATI_BASE_DIR.joinpath('opponents', opp)
        sizes[opp] = recursive_folder_size(opp_path)
    
    return sizes


def organize_bundles_from_opponents(opponents):
    """
    Divide a list of opponents into discrete bundles for download.
    """

    sizes = tally_opponent_filesizes(opponents)
    bundles = []

    current_bundle_sz = 0
    current_bundle = []

    for opp in opponents:
        new_bundle_sz = current_bundle_sz + sizes[opp]

        if new_bundle_sz >= MAX_BUNDLE_OPP_SIZE:
            # start a new bundle
            bundles.append((current_bundle, current_bundle_sz))
            current_bundle = []
            current_bundle_sz = 0

        current_bundle.append(opp)
        current_bundle_sz += sizes[opp]

    bundles.append((current_bundle, current_bundle_sz))

    return bundles


def dry_run():
    """
    List bundles that would be generated plus their sizes.
    """

    bundle_core_size = 0
    for d in CORE_DIRS:
        p = SPNATI_BASE_DIR.joinpath(d)
        bundle_core_size += recursive_folder_size(p)
    
    roster = read_listing()
    for status, opponents in roster.items():
        bundles = organize_bundles_from_opponents(opponents)

        for i, bundle_data in enumerate(bundles):
            bundle, sz = bundle_data

            print("{} - bundle {}: {} opponent bytes + {} core bytes = {} total bytes".format(
                status, i, sz, bundle_core_size, sz+bundle_core_size
            ))
            print(bundle)
            print("")


def add_folder_to_zipfile(path, zf):
    for child in recursive_list(path):
        rel_path = str(child.relative_to(SPNATI_BASE_DIR))
        zf.write(str(child), rel_path)


def add_bundle_core_files(zf):
    for core_dir in CORE_DIRS:
        p = SPNATI_BASE_DIR.joinpath(core_dir)
        add_folder_to_zipfile(p, zf)
        
    for d in [SPNATI_BASE_DIR.joinpath('opponents'), SPNATI_BASE_DIR]:
        for child in d.iterdir():
            if not child.is_file():
                continue
                
            zf.write(str(child), str(child.relative_to(SPNATI_BASE_DIR)))


def generate_single_bundle(category, bundle_idx, bundle_folders, include_core=True, description=None):
    bundle_name = 'spnati-{}-{}.zip'.format(
        category, bundle_idx
    )

    out_path = OUTPUT_DIR.joinpath(bundle_name)

    print("Generating bundle: "+bundle_name)
    print("Folders: "+(', '.join(
        str(p.relative_to(SPNATI_BASE_DIR)) for p in bundle_folders
    )))

    with zipfile.ZipFile(str(out_path), mode='w', compression=zipfile.ZIP_DEFLATED) as zf:
        if include_core:
            add_bundle_core_files(zf)

        for folder in bundle_folders:
            add_folder_to_zipfile(folder, zf)

    bundle_stat = out_path.stat()

    print("Bundle size: "+str(bundle_stat.st_size)+" bytes")    
    print("")


    return {
        'name': bundle_name,
        'category': category,
        'index': bundle_idx,
        'size': bundle_stat.st_size,
        'description': description,
        'folders': [str(p.relative_to(SPNATI_BASE_DIR)) for p in bundle_folders]
    }

def generate_bundles():
    print("Reading listing...")
    roster = read_listing()

    bundle_manifests = []

    for status, opponents in roster.items():
        print("Generating bundles for "+status+"...")
        bundles = organize_bundles_from_opponents(opponents)

        for bundle_idx, bundle_data in enumerate(bundles):
            bundle_opponents, sz = bundle_data
            bundle_folders = [SPNATI_BASE_DIR.joinpath('opponents', opponent) for opponent in bundle_opponents]

            manifest = generate_single_bundle(status, bundle_idx+1, bundle_folders, include_core=(status != 'incomplete'))
            manifest['opponents'] = bundle_opponents

            bundle_manifests.append(manifest)

    print("Generating reskins bundle...")
    reskins_base = SPNATI_BASE_DIR.joinpath('opponents', 'reskins')
    reskin_dirs = []

    for child in reskins_base.iterdir():
        if not child.is_dir():
            continue
        reskin_dirs.append(child)
    
    reskin_manifest = generate_single_bundle('reskins', 1, reskin_dirs, include_core=False, description="Alternate costumes for certain opponents.")
    bundle_manifests.append(reskin_manifest)

    print("Generating tools bundle...")
    tools_manifest = generate_single_bundle('tools', 1, [SPNATI_BASE_DIR.joinpath('tools')], include_core=False, description="Tools for developers. Includes the Character Editor and Kisekae.")
    bundle_manifests.append(tools_manifest)


    with OUTPUT_DIR.joinpath('manifest.json').open('w', encoding='utf-8') as f:
        json.dump(bundle_manifests, f)


if __name__ == "__main__":
    if MODE == 'dry-run':
        dry_run()
    elif MODE == "generate":
        generate_bundles()