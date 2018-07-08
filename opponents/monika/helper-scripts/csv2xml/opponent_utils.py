import os
import os.path as osp

from . import behaviour_parser as bp


__xml_cache = {}
__default_opponents_dir = None


def config_default_opponents_dir(d):
    global __default_opponents_dir
    __default_opponents_dir = d


def get_target_xml(target, opponents_dir=None):
    global __default_opponents_dir, __xml_cache
    
    if __default_opponents_dir is not None and opponents_dir is None:
        if osp.basename(osp.abspath(__default_opponents_dir)) == 'opponents':
            opponents_dir = osp.abspath(__default_opponents_dir)
        
    if opponents_dir is None:
        # try to find the main opponents directory
        if osp.basename(os.getcwd()) == 'opponents':
            opponents_dir = os.getcwd()
        elif osp.basename(osp.abspath('..')) == 'opponents':
            opponents_dir = osp.abspath('..')
        else:
            raise ValueError("Cannot find SPNATI opponents directory!")

    if target not in __xml_cache:
        target_xml_file = osp.join(opponents_dir, target+'/behaviour.xml')
        __xml_cache[target] = bp.parse_file(target_xml_file)

    return __xml_cache[target]
    

def get_target_gender(target_id):
    target_elem = get_target_xml(target_id)
    return target_elem.find('gender').text.strip().lower()


def get_target_stripping_case(target, stage):
    target_elem = get_target_xml(target)
    clothing_elems = [e for e in target_elem.find('wardrobe').iter('clothing')]
    clothing_elems.reverse()

    if stage < 0 or stage >= len(clothing_elems):
        raise IndexError("Invalid stage for target stripping case: '{}' (target={})".format(stage, target))

    gender = target_elem.find('gender').text.strip().lower()
    target_clothing_type = clothing_elems[stage].attributes['type'].strip().lower()
    target_clothing_position = clothing_elems[stage].attributes['position'].strip().lower()

    if target_clothing_type == 'extra':
        return gender+'_removing_accessory'
    elif target_clothing_type == 'minor' or target_clothing_type == 'major':
        return gender+'_removing_'+target_clothing_type
    elif target_clothing_type == 'important':
        if target_clothing_position == 'upper':
            return gender+'_chest_will_be_visible'
        elif target_clothing_position == 'lower':
            return gender+'_crotch_will_be_visible'
        else:
            raise ValueError("Unknown clothing position for target '{}' stage {}: {}".format(target, stage, target_clothing_position))
    else:
        raise ValueError("Unknown clothing type for target '{}' stage {}: {}".format(target, stage, target_clothing_type))


def get_target_stripped_case(target, stage):
    target_elem = get_target_xml(target)
    clothing_elems = [e for e in target_elem.find('wardrobe').iter('clothing')]
    clothing_elems.reverse()

    if stage < 1 or stage > len(clothing_elems):
        raise IndexError("Invalid stage for target stripped case: '{}' (target={})".format(stage, target))

    gender = target_elem.find('gender').text.strip().lower()
    size = target_elem.find('size').text.strip().lower()
    target_clothing_type = clothing_elems[stage-1].attributes['type'].strip().lower()
    target_clothing_position = clothing_elems[stage-1].attributes['position'].strip().lower()

    if target_clothing_type == 'extra':
        return gender+'_removed_accessory'
    elif target_clothing_type == 'minor' or target_clothing_type == 'major':
        return gender+'_removed_'+target_clothing_type
    elif target_clothing_type == 'important':
        if target_clothing_position == 'upper':
            if gender == 'female':
                return 'female_'+size+'_chest_is_visible'
            elif gender == 'male':
                return 'male_chest_is_visible'
            else:
                raise ValueError("Unknown gender for target '{}' stage {}: {}".format(target, stage, gender))
        elif target_clothing_position == 'lower':
            if gender == 'male':
                return 'male_'+size+'_crotch_is_visible'
            elif gender == 'female':
                return 'female_crotch_is_visible'
            else:
                raise ValueError("Unknown gender for target '{}' stage {}: {}".format(target, stage, gender))
        else:
            raise ValueError("Unknown clothing position for target '{}' stage {}: {}".format(target, stage, target_clothing_position))
    else:
        raise ValueError("Unknown clothing type for target '{}' stage {}: {}".format(target, stage, target_clothing_type))
