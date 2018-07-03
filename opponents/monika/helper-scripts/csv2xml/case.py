import re
from .utils import parse_interval, format_interval
from .opponent_utils import get_target_gender, get_target_stripping_case, get_target_stripped_case
from .state import State
from .ordered_xml import OrderedXMLElement


class Case(object):
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
    ]

    POSSIBLE_ATTRIBUTES = INTERVAL_CONDITIONS + ID_CONDITIONS + ['priority']

    def __init__(self, tag, conditions=None, custom_priority=None):
        self.tag = tag
        self.priority = custom_priority
        self.conditions = {}
        self.counters = {}
        self.states = []

        if conditions is None:
            return

        self.conditions, self.counters, cond_tag, cond_priority = self.parse_conditions(conditions)

        if self.tag is None:
            self.tag = cond_tag

        if self.priority is None:
            self.priority = cond_priority

    @classmethod
    def parse_conditions(cls, conditions):
        priority = None
        tag = None
        attr_conditions = {}
        counters = {}

        if isinstance(conditions, str) and len(conditions) > 0:
            conditions = [cond.split('=') for cond in conditions.split(',')]

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
                            print("[Info] Normalized attribute {} to {}".format(attr, possible_attr))
                            attr = possible_attr
                            break

                tag_match = re.match(r'tag\s*\:\s*([^\=]+)', attr, re.IGNORECASE)
                if tag_match is not None:
                    tag = tag_match.group(1)
                    if len(cond_tuple) == 2:
                        low, hi = parse_interval(val)
                        
                    counters[tag] = (low, hi)
                elif attr == 'priority':
                    priority = int(val)
                elif attr == 'tag':
                    tag = val
                elif attr in cls.INTERVAL_CONDITIONS:
                    # attribute condition taking an integer interval
                    # split condition interval if necessary
                    if len(cond_tuple) == 2:
                        low, hi = parse_interval(val)
                        
                    attr_conditions[attr] = (low, hi)
                else:
                    # attribute condition taking an identifier
                    attr_conditions[attr] = val.strip()

                    if attr not in cls.ID_CONDITIONS:
                        print("[Warning] case condition type not recognized: {}".format(attr))

        return attr_conditions, counters, tag, priority

    @classmethod
    def parse_conditions_set(cls, conditions, tag, priority):
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
        return self._make_conditions_set(self.conditions, self.counters, self.tag, self.priority)

    def states_set(self):
        return frozenset(state.to_tuple() for state in self.states)

    def format_conditions(self):
        attrs = []
        for attr, cond in self.conditions.items():
            if isinstance(cond, tuple):
                attrs.append("{:s}={:s}".format(attr, format_interval(cond)))
            else:
                attrs.append("{:s}={:s}".format(attr, cond))

        for tag, counter in self.counters.items():
            attrs.append("tag:{:s}={:s}".format(tag, format_interval(counter)))

        return ','.join(attrs)

    def __eq__(self, other):
        if isinstance(other, Case):
            return self.conditions_set() == other.conditions_set()
        elif isinstance(other, frozenset):
            return self.conditions_set() == other
        else:
            raise NotImplementedError()


    @classmethod
    def from_condition_set(cls, cond_set, states=None):
        case = cls(None, cond_set)
        if states is not None:
            case.states.extend(states)
        return case

    @classmethod
    def from_xml(cls, elem):
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
        'female_small_chest_visible',
        'female_medium_chest_visible',
        'female_large_chest_visible',
        'male_small_crotch_visible',
        'male_medium_crotch_visible',
        'male_large_crotch_visible',
        'male_chest_visible',
        'female_crotch_visible',
        'male_start_masturbating',
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
        elif name == 'npc_must_strip' or name == 'opponent_must_strip':
            if target_id is not None:
                gender = get_target_gender(target_id)

                if gender == 'female' or gender == 'male':
                    tag_list.append(gender+'_must_strip')
                else:
                    raise ValueError("Invalid gender found for target '{}': {}".format(target_id, gender))
            else:
                tag_list.append('female_must_strip')
                tag_list.append('male_must_strip')
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

                #print("[debug] Mapped pseudo-case {} for targetID {} stage {} to {}".format(name, target_id, target_stage, ret_case[0]))
        else:
            tag_list.append(name)
    return tag_list
