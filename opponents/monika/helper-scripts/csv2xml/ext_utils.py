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
        raise ValueError("Cannot find SPNATI opponents directory!")
    
    behaviour_path = opponents_path / char_id / 'behaviour.xml'
    meta_path = opponents_path / char_id / 'meta.xml'
    
    opponent_elem = bp.parse_file(str(behaviour_path))
    meta_elem = bp.parse_meta(str(meta_path))
    
    opponent_meta = Opponent.from_xml(opponent_elem, meta_elem)
    lineset = xml_to_lineset(opponent_elem)
    
    return lineset, opponent_meta
