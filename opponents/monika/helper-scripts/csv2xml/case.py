from collections import OrderedDict
import logging
import re

from .utils import parse_interval, format_interval
from .opponent_utils import get_target_gender, get_target_stripping_case, get_target_stripped_case
from .state import State
from .ordered_xml import OrderedXMLElement

class Case(object):
    # Maps case tags to the stage intervals in which they can appear.
    # These are slice objects, so they have start / stop / step attributes.
    # -1 is the 'finished' stage
    # -2 is the 'masturbating' stage
    # -3 is the 'naked' stage
    TAG_INTERVALS = OrderedDict([
        ("game_start", slice(0, 1)),
        ("selected", slice(0, 1)),
        ("swap_cards", slice(None, -2)),
        ("good_hand", slice(None, -2)),
        ("okay_hand", slice(None, -2)),
        ("bad_hand", slice(None, -2)),
        ("hand", slice(None, -2)),
        ("global", slice(None, None)),
        ("opponent_lost", slice(None, None)),
        ("male_human_must_strip", slice(None, None)),
        ("male_must_strip", slice(None, None)),
        ("male_removing_accessory", slice(None, None)),
        ("male_removing_minor", slice(None, None)),
        ("male_removing_major", slice(None, None)),
        ("male_chest_will_be_visible", slice(None, None)),
        ("male_crotch_will_be_visible", slice(None, None)),
        ("male_removed_accessory", slice(None, None)),
        ("male_removed_minor", slice(None, None)),
        ("male_removed_major", slice(None, None)),
        ("male_chest_is_visible", slice(None, None)),
        ("male_small_crotch_is_visible", slice(None, None)),
        ("male_medium_crotch_is_visible", slice(None, None)),
        ("male_large_crotch_is_visible", slice(None, None)),
        ("male_must_masturbate", slice(None, None)),
        ("male_start_masturbating", slice(None, None)),
        ("male_masturbating", slice(None, None)),
        ("male_heavy_masturbating", slice(None, None)),
        ("male_finished_masturbating", slice(None, None)),
        ("female_human_must_strip", slice(None, None)),
        ("female_must_strip", slice(None, None)),
        ("female_removing_accessory", slice(None, None)),
        ("female_removing_minor", slice(None, None)),
        ("female_removing_major", slice(None, None)),
        ("female_chest_will_be_visible", slice(None, None)),
        ("female_crotch_will_be_visible", slice(None, None)),
        ("female_removed_accessory", slice(None, None)),
        ("female_removed_minor", slice(None, None)),
        ("female_removed_major", slice(None, None)),
        ("female_small_chest_is_visible", slice(None, None)),
        ("female_medium_chest_is_visible", slice(None, None)),
        ("female_large_chest_is_visible", slice(None, None)),
        ("female_crotch_is_visible", slice(None, None)),
        ("female_must_masturbate", slice(None, None)),
        ("female_start_masturbating", slice(None, None)),
        ("female_masturbating", slice(None, None)),
        ("female_heavy_masturbating", slice(None, None)),
        ("female_finished_masturbating", slice(None, None)),
        ("must_strip", slice(None, -3)),
        ("must_strip_winning", slice(None, -3)),
        ("must_strip_normal", slice(None, -3)),
        ("must_strip_losing", slice(None, -3)),
        ("stripping", slice(None, -3)),
        ("stripped", slice(1, -2)),
        ("must_masturbate", slice(-3, -2)),
        ("must_masturbate_first", slice(-3, -2)),
        ("start_masturbating", slice(-3, -2)),
        ("masturbating", slice(-2, -1)),
        ("heavy_masturbating", slice(-2, -1)),
        ("finishing_masturbating", slice(-2, -1)),
        ("finished_masturbating", slice(-1, None)),
        ("game_over_victory", slice(None, -2)),
        ("game_over_defeat", slice(-1, None)),
    ])
    
    ALL_TAGS = list(TAG_INTERVALS.keys())
    
    INTERVAL_CONDITIONS = [
        'targetStage',
        'targetTimeInStage',
        'alsoPlayingStage',
        'alsoPlayingTimeInStage',
        'totalMales',
        'totalFemales',
        'timeInStage',
        'consecutiveLosses',
        'totalAlive',
        'totalExposed',
        'totalNaked',
        'totalMasturbating',
        'totalFinished',
        'totalRounds',
        'targetLayers',
    ]

    ID_CONDITIONS = [
        'target',
        'filter',
        'targetSaidMarker',
        'targetNotSaidMarker',
        'oppHand',
        'hasHand',
        'alsoPlaying',
        'alsoPlayingHand',
        'alsoPlayingSaidMarker',
        'alsoPlayingNotSaidMarker',
        'saidMarker',
        'notSaidMarker',
        'targetStatus',
    ]

    POSSIBLE_ATTRIBUTES = INTERVAL_CONDITIONS + ID_CONDITIONS + ['priority']

    def __init__(self, tag, conditions=None, custom_priority=None):
        """
        Represents a list of States as well as the set of conditions under which they play.
        Corresponds to a <case> XML element.
        """
        
        self.tag = tag
        self.priority = custom_priority
        self.conditions = OrderedDict()
        self.counters = OrderedDict()
        self.states = []

        if conditions is None:
            return

        self.conditions, self.counters, cond_tag, cond_priority = self.parse_conditions(conditions)

        if self.tag is None:
            self.tag = cond_tag

        if self.priority is None:
            self.priority = cond_priority
            
        if self.tag not in self.ALL_TAGS:
            logging.error("Case tag not recognized: %s", self.tag)
            
    def copy(self):
        """
        Clone this set of conditions.
        """
        
        c = Case(self.tag, None, self.priority)
        c.conditions = self.conditions.copy()
        c.counters = self.counters.copy()
        c.states = self.states.copy()
        
        return c

    @classmethod
    def parse_conditions(cls, conditions):
        """
        Parse a condition set expressed as either a string or as an iterable of tuples.
        
        Returns:
            * attr_conditions (OrderedDict): The parsed conditions, indexed by their XML attribute names.
            * counters (OrderedDict): Tag count condition intervals, indexed by their corresponding tag.
            * tag (str or None): any case tag found within the condition set. May be None if none were found.
            * priority (int or None): any custom priority found within the condition set. May be None if none were found.
        """
        
        priority = None
        tag = None
        attr_conditions = OrderedDict()
        counters = OrderedDict()

        if isinstance(conditions, str) and len(conditions) > 0:
            conditions = [cond.split('=', 1) for cond in conditions.split(',')]

        if len(conditions) > 0:
            for cond_tuple in conditions:
                attr = cond_tuple[0].strip()
                val = cond_tuple[1]

                if len(cond_tuple) == 3:
                    low = cond_tuple[1]
                    hi = cond_tuple[2]

                # normalize attributes if possible
                if attr not in cls.POSSIBLE_ATTRIBUTES:
                    for possible_attr in cls.POSSIBLE_ATTRIBUTES:
                        if attr.lower() == possible_attr.lower():
                            logging.info("Normalized attribute %s to %s", attr, possible_attr)
                            attr = possible_attr
                            break

                tag_match = re.match(r'tag\s*\:\s*([^\=]+)', attr, re.IGNORECASE)
                if tag_match is not None:
                    matched_tag = tag_match.group(1)
                    if len(cond_tuple) == 2:
                        low, hi = parse_interval(val)
                        
                    counters[matched_tag] = (low, hi)
                elif attr == 'priority':
                    priority = int(val)
                elif attr == 'tag':
                    tag = val
                elif attr in cls.INTERVAL_CONDITIONS:
                    # attribute condition taking an integer interval
                    # split condition interval if necessary
                    try:
                        if len(cond_tuple) == 2:
                            low, hi = parse_interval(val)
                            
                        attr_conditions[attr] = (low, hi)
                    except ValueError as e:
                        logging.error("Caught ValueError: {:s}".format(str(e)))
                else:
                    # attribute condition taking an identifier
                    attr_conditions[attr] = val.strip()

                    if attr not in cls.ID_CONDITIONS:
                        logging.warning("Case condition type not recognized: %s", attr)

        return attr_conditions, counters, tag, priority

    @classmethod
    def parse_conditions_set(cls, conditions, tag, priority):
        """
        Create a condition set from a string or an iterable of tuples.
        
        See also:
            * :meth:`parse_conditions`
            * :meth:`condition_set`
        """
        
        attr_conditions, counters, cond_tag, cond_priority = cls.parse_conditions(conditions)

        if tag is None:
            tag = cond_tag

        if priority is None:
            priority = cond_priority

        return cls._make_conditions_set(attr_conditions, counters, tag, priority)

    @classmethod
    def _make_conditions_set(cls, attr_conds, counters, tag, priority):
        condition_tuples = [('tag', tag)]
        
        for tag_name, counter in counters.items():
            condition_tuples.append(('tag:'+tag_name, counter[0], counter[1]))

        if priority is not None:
            condition_tuples.append(('priority', priority))

        for attr, val in attr_conds.items():
            if isinstance(val, tuple):
                condition_tuples.append((attr, *val))
            else:
                condition_tuples.append((attr, val))

        return frozenset(condition_tuples)

    def conditions_set(self):
        """
        Get all conditions describing this Case (incl. case tag and any custom priority) in a hashable container.
        
        Returns:
            :obj:`frozenset`: a 'conditions set' containing all conditions describing this Case.
        """
        
        return self._make_conditions_set(self.conditions, self.counters, self.tag, self.priority)

    def states_set(self):
        """
        Get all States within this Case in a hashable container.
        
        Returns:
            :obj:`frozenset`: a 'states set' containing States within this Case.
        """
        return frozenset(state.to_tuple() for state in self.states)

    def format_conditions(self, sort=False):
        """
        Format this Case's conditions in a machine-parsable and human-readable format.
        
        Args:
            sort (bool, optional): If true, then the attributes will be sorted in ascending
                order in the returned string.
                
        Returns:
            str: The formatted conditions.
        """
        
        attrs = []
        for attr, cond in self.conditions.items():
            if isinstance(cond, tuple):
                attrs.append("{:s}={:s}".format(attr, format_interval(cond)))
            else:
                attrs.append("{:s}={:s}".format(attr, cond))

        for tag, counter in self.counters.items():
            attrs.append("tag:{:s}={:s}".format(tag, format_interval(counter)))

        if self.priority is not None:
            attrs.append("priority={:d}".format(self.priority))

        if not sort:
            return ','.join(attrs)
        else:
            return ','.join(sorted(attrs))
        
    def __eq__(self, other):
        if isinstance(other, Case):
            return self.conditions_set() == other.conditions_set()
        elif isinstance(other, frozenset):
            return self.conditions_set() == other
        else:
            raise NotImplementedError()
            
    def is_conditional(self):
        """
        Check whether this Case has any conditions attached to it.
        """
        
        return (len(self.conditions) > 0) or (len(self.counters) > 0)
        
    def is_generic(self):
        """
        Check whether this Case is generic; i.e. has no conditions attached to it.
        """
        
        return not self.is_conditional()

    def is_targeted(self):
        """
        Check whether this Case is 'targeted'; i.e. is specifically conditional w.r.t. a filter tag or an opponent.
        Specifically, this function checks for 'filter', 'alsoPlaying', 'target', or tag-count conditions.
        """
        
        return ('filter' in self.conditions) or ('alsoPlaying' in self.conditions) or ('target' in self.conditions) or (len(self.counters) > 0)

    @classmethod
    def from_condition_set(cls, cond_set, states=None):
        """
        Create a Case from a condition set.
        """
        
        case = cls(None, cond_set)
        if states is not None:
            case.states.extend(states)
        return case

    @classmethod
    def from_xml(cls, elem):
        """
        Create a Case from a <case> OrderedXMLElement.
        """
        
        conditions_list = []

        # including 'priority' and 'tag' attributes in conditions_list will work
        # to set priority and tag, though it wouldn't really be expected from the name.
        # this is just to make things easier.
        for attr, val in elem.attributes.items():
            conditions_list.append((attr, val))

        for child in elem.iter('condition'):
            conditions_list.append(('tag:'+child.attributes['filter'], child.attributes['count']))

        case = cls(elem.attributes['tag'], conditions_list)

        for state in elem.iter('state'):
            case.states.append(State.from_xml(state))

        return case

    def to_xml(self, stage):
        """
        Create a <case> OrderedXMLElement from this Case.
        """
        
        elem = OrderedXMLElement('case')
        elem.attributes['tag'] = self.tag

        for attr, cond in self.conditions.items():
            if isinstance(cond, tuple):
                val = format_interval(cond)
            else:
                val = cond

            elem.attributes[attr] = val

        if self.priority is not None:
            elem.attributes['priority'] = str(self.priority)

        for tag, counter in self.counters.items():
            child = OrderedXMLElement('condition')
            child.attributes['count'] = format_interval(counter)
            child.attributes['filter'] = tag
            elem.children.append(child)

        for state in self.states:
            elem.children.append(state.to_xml(stage))

        return elem
        
        
class CaseSet(object):
    def __init__(self):
        """
        Contains multiple distinct cases and attempts to merge together newly-added cases.
        """
        self._case_dict = {}
        
    def add(self, case):
        cond_set = case.conditions_set()
        if cond_set not in self._case_dict:
            self._case_dict[cond_set] = case.copy()
        else:
            self._case_dict[cond_set].states.extend(case.states)
                
    def remove(self, case):
        cond_set = case.conditions_set()
        if cond_set not in self._case_dict:
            raise KeyError("Case [tag={:s}] not contained within CaseSet.".format(case.tag))
        
        del self._case_dict[cond_set]
    
    def __len__(self):
        return len(self._case_dict)
    
    def __iter__(self):
        return self._case_dict.values().__iter__()
        
    def __contains__(self, case):
        cond_set = case.conditions_set()
        return cond_set in self._case_dict


simple_pseudo_cases = {
    'opponent_removing_accessory':      ['male_removing_accessory', 'female_removing_accessory'],
    'opponent_removing_minor':          ['male_removing_minor', 'female_removing_minor'],
    'opponent_removing_major':          ['male_removing_major', 'female_removing_major'],
    'opponent_chest_will_be_visible':   ['male_chest_will_be_visible', 'female_chest_will_be_visible'],
    'opponent_crotch_will_be_visible':  ['male_crotch_will_be_visible', 'female_crotch_will_be_visible'],
    'opponent_must_masturbate':         ['male_must_masturbate', 'female_must_masturbate'],
    
    'opponent_removed_accessory':       ['male_removed_accessory', 'female_removed_accessory'],
    'opponent_removed_minor':           ['male_removed_minor', 'female_removed_minor'],
    'opponent_removed_major':           ['male_removed_major', 'female_removed_major'],
    'opponent_chest_is_visible':        ['male_chest_is_visible', 'female_small_chest_is_visible', 'female_medium_chest_is_visible', 'female_large_chest_is_visible'],
    'female_chest_is_visible':          ['female_small_chest_is_visible', 'female_medium_chest_is_visible', 'female_large_chest_is_visible'],
    'opponent_crotch_is_visible':       ['female_crotch_is_visible', 'male_small_crotch_is_visible', 'male_medium_crotch_is_visible', 'male_large_crotch_is_visible'],
    'male_crotch_is_visible':           ['male_small_crotch_is_visible', 'male_medium_crotch_is_visible', 'male_large_crotch_is_visible'],
    'opponent_start_masturbating':      ['male_start_masturbating', 'female_start_masturbating'],
    
    'opponent_must_strip':              ['female_must_strip', 'male_must_strip'],
    'opponent_masturbating':            ['male_masturbating', 'female_masturbating'],
    'opponent_heavy_masturbating':      ['male_heavy_masturbating', 'female_heavy_masturbating'],
    'opponent_finished_masturbating':   ['male_finished_masturbating', 'female_finished_masturbating'],

    'male_removing_any': [
        'male_removing_accessory',
        'male_removing_minor',
        'male_removing_major',
        'male_crotch_will_be_visible',
        'male_chest_will_be_visible',
        'male_must_masturbate',
    ],
    
    'female_removing_any': [
        'female_removing_accessory',
        'female_removing_minor',
        'female_removing_major',
        'female_chest_will_be_visible',
        'female_crotch_will_be_visible',
        'female_must_masturbate',
    ],

    'opponent_removing_any': [
        'male_removing_accessory',
        'female_removing_accessory',
        'male_removing_minor',
        'female_removing_minor',
        'male_removing_major',
        'female_removing_major',
        'male_chest_will_be_visible',
        'female_chest_will_be_visible',
        'male_crotch_will_be_visible',
        'female_crotch_will_be_visible',
        'male_must_masturbate',
        'female_must_masturbate',
    ],

    'opponent_removed_any': [
        'male_removed_accessory',
        'female_removed_accessory',
        'male_removed_minor',
        'female_removed_minor',
        'male_removed_major',
        'female_removed_major',
        'female_small_chest_is_visible',
        'female_medium_chest_is_visible',
        'female_large_chest_is_visible',
        'male_small_crotch_is_visible',
        'male_medium_crotch_is_visible',
        'male_large_crotch_is_visible',
        'male_chest_is_visible',
        'female_crotch_is_visible',
        'male_start_masturbating',
        'female_start_masturbating',
    ],
    
    'male_removed_any': [
        'male_removed_accessory',
        'male_removed_minor',
        'male_removed_major',
        'male_small_crotch_is_visible',
        'male_medium_crotch_is_visible',
        'male_large_crotch_is_visible',
        'male_chest_is_visible',
        'male_start_masturbating',
    ],
    
    'female_removed_any': [
        'female_removed_accessory',
        'female_removed_minor',
        'female_removed_major',
        'female_small_chest_is_visible',
        'female_medium_chest_is_visible',
        'female_large_chest_is_visible',
        'female_crotch_is_visible',
        'female_start_masturbating',
    ],

    'must_strip_self':                  ['must_strip_winning', 'must_strip_normal', 'must_strip_losing'],
    'self_must_strip':                  ['must_strip_winning', 'must_strip_normal', 'must_strip_losing'],

    'hand_quality':                     ['good_hand', 'okay_hand', 'bad_hand'],
    'hand':                             ['good_hand', 'okay_hand', 'bad_hand'],
    'any_hand':                         ['good_hand', 'okay_hand', 'bad_hand'],
    'hand_chatter':                     ['good_hand', 'okay_hand', 'bad_hand'],

    'player_must_strip':                ['female_human_must_strip', 'male_human_must_strip'],
    'human_must_strip':                 ['female_human_must_strip', 'male_human_must_strip'],
    
    'start': ['game_start'],
    'select': ['selected'],
    'start': ['game_start']
}

def parse_case_name(case_tags, cond_str):
    # we don't need the case tag or priority for this purpose
    conditions, _, _, _ = Case.parse_conditions(cond_str)
    
    target_id = conditions.get('target')
    
    if 'targetStage' in conditions:
        target_stage_low = conditions['targetStage'][0]
        target_stage_high = conditions['targetStage'][1]
    else:
        target_stage_low = None
        target_stage_high = None
        
    tag_list = []

    for name in case_tags.split(','):
        name = name.strip().lower()

        if name in simple_pseudo_cases:
            tag_list.extend(simple_pseudo_cases[name])
        elif name == 'target_stripping' or  name == 'target_stripped':
            if target_stage_low != target_stage_high:
                raise ValueError("The 'target_stripping' and 'target_stripped' pseudo-cases do not currently work with interval target stages.")

            if target_id is None or target_stage_low is None:
                raise ValueError("Lines must have targets and target stages set in order to use the 'target_stripping' and 'target_stripped' pseudo-cases!")
            else:
                if name == 'target_stripping':
                    tag_list.append(get_target_stripping_case(target_id, target_stage_low))
                else:
                    tag_list.append(get_target_stripped_case(target_id, target_stage_low))

                #logging.debug("Mapped pseudo-case {} for targetID {} stage {} to {}".format(name, target_id, target_stage, ret_case[0]))
        else:
            tag_list.append(name)
    
    # if we have a target, remove any tags that don't match the target gender
    if target_id is not None and target_id != 'human':
        gender = get_target_gender(target_id)
        
        def case_matches_target_gender(tag):
            male_tag = tag.startswith('male_')
            female_tag = tag.startswith('female_')
            
            if not (male_tag or female_tag):
                return True
                
            if (male_tag and gender == 'male') or (female_tag and gender == 'female'):
                return True
            
            return False
        
        tag_list = filter(case_matches_target_gender, tag_list)
    
    return tag_list
