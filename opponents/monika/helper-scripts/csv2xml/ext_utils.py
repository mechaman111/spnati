from . import behaviour_parser as bp
from .opponent import Opponent
from .opponent_utils import find_opponents_directory
from .xml_format import xml_to_lineset

def load_character(char_id, opponents_path=None):
    """
    Loads a character by their ID (folder name).
    """
    opponents_path = find_opponents_directory(opponents_path)
    
    if opponents_path is None:
        raise FileNotFoundError("Cannot find SPNATI opponents directory!")
    
    behaviour_path = opponents_path / char_id / 'behaviour.xml'
    meta_path = opponents_path / char_id / 'meta.xml'
    
    opponent_elem = bp.parse_file(str(behaviour_path))
    meta_elem = bp.parse_meta(str(meta_path))
    
    opponent_meta = Opponent.from_xml(opponent_elem, meta_elem)
    lineset = xml_to_lineset(opponent_elem)
    
    return lineset, opponent_meta


def list_opponents(opponents_path=None, online=True, testing=True, offline=False, incomplete=False):
    """
    Iterate over all opponent IDs in listing.xml.
    
    Args:
        opponents_path (pathlib.Path or str): if specified, search for the opponents/ directory
            from here instead of from the default directory.
    
    Kwargs:
        online (bool, default True): whether to yield online opponents during iteration.
        testing (bool, default True): whether to yield testing opponents during iteration.
        offline (bool, default False): whether to yield offline opponents during iteration.
        incomplete (bool, default False): whether to yield incomplete opponents during iteration.
    """

    opponents_path = find_opponents_directory(opponents_path)
    
    if opponents_path is None:
        raise FileNotFoundError("Cannot find SPNATI opponents directory!")
        
    listing_path = opponents_path / 'listing.xml'
    listing_elem = bp.parse_listing(str(listing_path))
    
    for individual in listing_elem.find('individuals').iter('opponent'):
        if 'status' not in individual.attributes:
            if online:
                yield individual.text
        elif individual.attributes['status'] == 'testing' and testing:
            yield individual.text
        elif individual.attributes['status'] == 'offline' and offline:
            yield individual.text
        elif individual.attributes['status'] == 'incomplete' and incomplete:
            yield individual.text
