#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import unicode_literals

import sys
if sys.version_info[0] < 3:
    reload(sys)
    sys.setdefaultencoding('UTF8')
    from io import open

import csv
from collections import OrderedDict
import functools
import os
import os.path as osp
import re
import time
from behaviour_parser import parse_file, parse_meta
from ordered_xml import OrderedXMLElement

VERSION = '0.14.2-alpha'  # will attempt to follow semver if possible
COMMENT_TIME_FORMAT = 'at %X %Z on %A, %B %d, %Y'  # strftime format
WARNING_COMMENT = 'This file was machine generated using csv2xml.py {:s} {:s}. Please do not edit it directly without preserving your improvements elsewhere or your changes may be lost the next time this file is generated.'

def generate_comment():
    return WARNING_COMMENT.format(VERSION, time.strftime(COMMENT_TIME_FORMAT))

def parse_interval(val):
    interval_tuple = val.split('-')

    if len(interval_tuple[0].strip()) == 0:  # handles intervals with a single negative number
        return int(val), int(val)

    low = int(interval_tuple[0].strip())

    if len(interval_tuple) > 1:
        hi = int(interval_tuple[1].strip())
    else:
        hi = int(low)

    return low, hi


def format_interval(interval):
    low, hi = interval
    if low == hi:
        return str(low)
    else:
        return str(low)+'-'+str(hi)


__xml_cache = {}
def get_target_xml(target, opponents_dir=None):
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
        __xml_cache[target] = parse_file(target_xml_file)

    return __xml_cache[target]


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
                target_elem = get_target_xml(target_id)
                gender = target_elem.find('gender').text.strip().lower()

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

__articles = {
    'male': {
        'nom': 'he',
        'pos': 'his',
        'obj': 'him',
    },
    'female': {
        'nom': 'he',
        'pos': 'his',
        'obj': 'him',
    },
}

def preprocess_state_text(text, case_tag, cond_set):
    if 'female' in case_tag:
        article_set = __articles['female']
    elif 'male' in case_tag:
        article_set = __articles['male']
    else:
        article_set = None

    def article_repl(match):
        if article_set is None:
            raise ValueError("Cannot determine gender from case tag "+case_tag)

        return article_set[match.group(1)]

    # replace ~art:<type>~ tags
    text = re.sub(r'\~\s*art(?:icle)?\s*\:\s*(\w+)\s*\~', article_repl, text)

    return text

def parse_stage_name(name, opponent):
    name = name.strip().lower()

    if name == 'full' or name == 'fully_clothed' or name == 'fully clothed':
        return 0
    elif name == 'naked' or name == 'nude' or name == '-3':
        return opponent.naked_stage()
    elif name == 'mast' or name == 'masturbate' or name == 'masturbating' or name == '-2':
        return opponent.masturbate_stage()
    elif name == 'finish' or name == 'finished' or name == '-1':
        return opponent.finished_stage()
    else:
        # attempt to match a lost-clothing selector:
        m = re.match(r'lost(?:\-|\_|\s+)(.+)', name, re.IGNORECASE)
        if m is not None:
            clothing_stage = opponent.lost_clothing_stage(m.group(1))
            if clothing_stage is not None:
                return clothing_stage

        try:
            # try to parse the name as a number directly
            return int(name)
        except ValueError:
            # if all else fails just assume the name is a stage in and of itself
            return name


def parse_stage_selector(selector, opponent):
    stages = []

    if isinstance(selector, str):
        for sub_selector in selector.split(','):
            sub_selector = sub_selector.strip().lower()

            if sub_selector == 'all':
                stages = range(opponent.len_stages())
                break

            interval_match = re.match(r'(.+?)\s*\-\s*(.+)', sub_selector, re.IGNORECASE)
            if interval_match is not None:
                low = interval_match.group(1)
                hi = interval_match.group(2)

                low = parse_stage_name(low, opponent)
                hi = parse_stage_name(hi, opponent)

                if not isinstance(low, int):
                    raise ValueError("Cannot use special stage in an interval: {}".format(low))

                if not isinstance(hi, int):
                    raise ValueError("Cannot use special stage in an interval: {}".format(hi))

                stages.extend(range(low, hi+1))
            else:
                stages.append(parse_stage_name(sub_selector, opponent))
    else:
        stages = selector

    return frozenset(stages)


def format_stage_set(stage_set):
    fragments = []
    stages = []

    for stage in stage_set:
        try:
            stages.append(int(stage))
        except ValueError:
            fragments.append(stage)

    stages = sorted(stages)

    while len(stages) > 0:
        lo = stages[0]
        hi_idx = 0

        for i in range(1, len(stages)):
            if stages[i] != lo+i:
                break
            hi_idx = i

        hi = lo+hi_idx
        fragments.append(format_interval((lo, hi)))

        del stages[0:hi_idx+1]

    return ','.join(fragments)

class Opponent(object):
    GENDER_MALE = 'male'
    GENDER_FEMALE = 'female'

    SIZE_SMALL = 'small'
    SIZE_MEDIUM = 'medium'
    SIZE_LARGE = 'large'

    INTEL_BAD = 'bad'
    INTEL_AVG = 'average'
    INTEL_GOOD = 'good'

    def __init__(self):
        self.enabled = True
        self.first = None
        self.last = None
        self.label = ''
        self.intelligence = []
        self.size = self.SIZE_MEDIUM
        self.gender = self.GENDER_FEMALE
        self.timer = 0
        self.wardrobe = []
        self.tags = []

        self.description = ''
        self.selection_pic = ''
        self.height = ''
        self.source = ''
        self.writer = ''
        self.artist = ''
        self.has_ending = False


    def len_stages(self):
        return len(self.wardrobe) + 3

    def lost_clothing_stage(self, clothing_name):
        if len(self.wardrobe) == 0:
            print("[Warning] Attempting to access wardrobe data that hasn't been loaded yet! Are you sure your metadata is at the top of your input file?")
            return

        clothing_name = clothing_name.lower()
        for stage, clothing in enumerate(self.wardrobe):
            if clothing_name == clothing['lowercase']:
                return len(self.wardrobe) - stage

    def naked_stage(self):
        return len(self.wardrobe)

    def masturbate_stage(self):
        return len(self.wardrobe) + 1

    def finished_stage(self):
        return len(self.wardrobe) + 2

    @classmethod
    def from_xml(cls, opponent_elem, meta_elem):
        opp = cls()

        opp.first = opponent_elem.find('first').text
        opp.last = opponent_elem.find('last').text
        opp.label = opponent_elem.find('label').text
        opp.timer = int(opponent_elem.find('timer').text)
        opp.gender = opponent_elem.find('gender').text.lower()
        opp.size = opponent_elem.find('size').text.lower()

        opp.tags = [tag.text for tag in opponent_elem.find('tags').iter('tag')]

        for clothing in opponent_elem.find('wardrobe').iter('clothing'):
            opp.wardrobe.append(OrderedDict([
                ('lowercase', clothing.attributes['lowercase']),
                ('position', clothing.attributes['position']),
                ('proper-name', clothing.attributes['proper-name']),
                ('type', clothing.attributes['type']),
            ]))

        for intel in opponent_elem.iter('intelligence'):
            stage = None
            if 'stage' in intel.attributes:
                stage = intel.attributes['stage']

            opp.intelligence.append((stage, intel.text))

        if meta_elem is not None:
            opp.enabled = (meta_elem.find('enabled').text.strip().lower() == 'true')
            opp.selection_pic = meta_elem.find('pic').text
            opp.height = meta_elem.find('height').text
            opp.source = meta_elem.find('from').text
            opp.writer = meta_elem.find('writer').text
            opp.artist = meta_elem.find('artist').text
            opp.description = meta_elem.find('description').text
            opp.has_ending = (meta_elem.find('has_ending').text.strip().lower() == 'true')

        return opp

    def to_meta_xml(self):
        meta_elem = OrderedXMLElement('opponent')

        if self.enabled:
            meta_elem.children.append(OrderedXMLElement('enabled', 'true'))
        else:
            meta_elem.children.append(OrderedXMLElement('enabled', 'false'))

        meta_elem.children.append(OrderedXMLElement('first', self.first))
        meta_elem.children.append(OrderedXMLElement('last', self.last))
        meta_elem.children.append(OrderedXMLElement('label', self.label))
        meta_elem.children.append(OrderedXMLElement('pic', self.selection_pic))
        meta_elem.children.append(OrderedXMLElement('gender', self.gender))
        meta_elem.children.append(OrderedXMLElement('height', self.height))
        meta_elem.children.append(OrderedXMLElement('from', self.source))
        meta_elem.children.append(OrderedXMLElement('writer', self.writer))
        meta_elem.children.append(OrderedXMLElement('artist', self.artist))
        meta_elem.children.append(OrderedXMLElement('description', self.description))

        if self.has_ending:
            meta_elem.children.append(OrderedXMLElement('has_ending', 'true'))
        else:
            meta_elem.children.append(OrderedXMLElement('has_ending', 'false'))

        meta_elem.children.append(OrderedXMLElement('layers', str(len(self.wardrobe))))

        tags = OrderedXMLElement('tags')
        for tag in self.tags:
            tags.children.append(OrderedXMLElement('tag', tag))

        meta_elem.children.append(tags)

        return meta_elem

    def to_xml(self):
        opponent_elem = OrderedXMLElement('opponent')

        opponent_elem.children.append(OrderedXMLElement('first', self.first))
        opponent_elem.children.append(OrderedXMLElement('last', self.last))
        opponent_elem.children.append(OrderedXMLElement('label', self.label))
        opponent_elem.children.append(OrderedXMLElement('size', self.size))
        opponent_elem.children.append(OrderedXMLElement('gender', self.gender))
        opponent_elem.children.append(OrderedXMLElement('timer', str(self.timer)))

        for stage, intelligence in self.intelligence:
            if stage is not None:
                attrs = [('stage', stage)]
            else:
                attrs = None

            opponent_elem.children.append(OrderedXMLElement('intelligence', intelligence, attrs))

        tags = OrderedXMLElement('tags')
        for tag in self.tags:
            tags.children.append(OrderedXMLElement('tag', tag))

        opponent_elem.children.append(tags)

        wardrobe = OrderedXMLElement('wardrobe')
        for clothing_attrs in self.wardrobe:
            wardrobe.children.append(OrderedXMLElement('clothing', None, clothing_attrs))

        opponent_elem.children.append(wardrobe)

        return opponent_elem


class State(object):
    def __init__(self, text, img, marker=None, silent=False):
        self.text = text
        self.image = img
        self.marker = marker
        self.silent = silent

    def __eq__(self, other):
        return (
            (self.text == other.text)
            and (self.image == other.image)
            and (self.marker == other.marker)
            and (self.silent == other.silent)
        )

    def to_tuple(self):
        return (self.text, self.image, self.marker, self.silent)

    @classmethod
    def from_tuple(cls, tup):
        return cls(*tup)

    @staticmethod
    def xml_to_tuple(elem):
        return (
            elem.text,
            elem.get('img'),
            elem.get('marker') if 'marker' in elem.attributes else None,
            bool(elem.get('silent')) if 'silent' in elem.attributes else False
        )

    @classmethod
    def from_xml(cls, elem):
        root, ext = osp.splitext(elem.get('img'))

        img_split = re.match(r'(\d+)\-(.+)', root)
        if img_split is not None:
            root = img_split.group(2)

        state = cls(elem.text, root)

        if 'marker' in elem.attributes:
            state.marker = elem.get('marker')

        if 'silent' in elem.attributes:
            state.silent = bool(elem.get('silent'))

        return state

    def to_xml(self, stage):
        elem = OrderedXMLElement('state')

        # assume all images are PNG files for now because:
        # - I am lazy
        # - this is meant for internal use only
        image = '{:d}-{:s}.png'.format(int(stage), self.image)

        if not osp.exists(osp.join(os.getcwd(), image)):
            print("Warning: image {} does not exist! (Stage {}, linetext={})".format(image, stage, self.text))

        elem.attributes['img'] = image

        if self.marker is not None:
            elem.attributes['marker'] = self.marker

        if self.silent:
            elem.attributes['silent'] = 'true'

        elem.text = self.text

        return elem


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


class Stage(object):
    def __init__(self, stage_num):
        self.stage_num = int(stage_num)
        self.cases = []

    @classmethod
    def from_line_set(cls, lineset, stage_num):
        """
        Create a stage object from cases within a dict keyed by stage sets.
        """

        stage = cls(stage_num)

        for stage_set, cases in lineset.items():
            if stage_num in stage_set:
                stage.cases.extend(cases)

        return stage

    @classmethod
    def from_xml(cls, elem):
        stage = cls(elem.get('id'))

        for case in elem.iter('case'):
            stage.cases.append(Case.from_xml(case))

        return stage

    def to_xml(self):
        elem = OrderedXMLElement('stage')
        elem.attributes['id'] = str(self.stage_num)

        for case in self.cases:
            elem.children.append(case.to_xml(self.stage_num))

        return elem


def xml_to_lineset(opponent_elem):
    # cases maps case condition sets to other dictionaries.
    # the 2nd level maps state sets to (mutable) stage sets.
    # this allows us to automatically de-duplicate sets of lines that are identical
    # (both dialogue- and condition-wise) across stages.
    cases = {}

    behaviour_elem = opponent_elem.find('behaviour')

    for stage_elem in behaviour_elem.iter('stage'):
        stage = Stage.from_xml(stage_elem)

        for case in stage.cases:
            cond_set = case.conditions_set()

            if cond_set not in cases:
                cases[cond_set] = {}

            # this is a dictionary containing cases with the same conditions but possibly different states.
            level_2 = cases[cond_set]

            for state in case.states:
                state_tuple = state.to_tuple()
                if state_tuple not in level_2:
                    level_2[state_tuple] = set([stage.stage_num])
                else:
                    stage_set = level_2[state_tuple]
                    stage_set.add(stage.stage_num)

    # Now that we have a unique list of all conditions and line sets across stages,
    # construct the line set.
    lineset = {}

    for cond_set, level_2 in cases.items():
        states_by_stageset = {}

        for state_tuple, stage_set in level_2.items():
            stage_set = frozenset(stage_set)

            if stage_set not in states_by_stageset:
                states_by_stageset[stage_set] = []

            states_by_stageset[stage_set].append(state_tuple)

        for stage_set, state_tuples in states_by_stageset.items():
            states = [State.from_tuple(tup) for tup in state_tuples]
            case = Case.from_condition_set(cond_set, states)

            if stage_set not in lineset:
                lineset[stage_set] = []

            lineset[stage_set].append(case)

    start_elem = opponent_elem.find('start')
    start_stageset = frozenset(['start'])

    select_case = Case('select')
    select_case.states.append(State.from_xml(start_elem.children[0]))

    start_case = Case('start')
    for state in start_elem.children[1:]:
        start_case.states.append(State.from_xml(state))

    lineset[start_stageset] = [select_case, start_case]

    return lineset


def lineset_to_xml(lineset):
    behaviour_elem = OrderedXMLElement('behaviour')
    start_elem = OrderedXMLElement('start')

    stage_superset = set()
    for stage_set in lineset.keys():
        stage_superset.update(stage_set)

    for stage_id in stage_superset:
        if stage_id == 'start':
            for stage_set, cases in lineset.items():
                if stage_id in stage_set:
                    for case in cases:
                        if case.tag == 'select':
                            start_elem.children.insert(0, case.states[0].to_xml(0))
                            if len(case.states) > 1:
                                print("[Warning] Multiple 'select' lines found! Selecting only the first: {}".format(case.states[0].text))
                        elif case.tag == 'start':
                            for state in case.states:
                                start_elem.children.append(state.to_xml(0))
        else:
            stage_elem = OrderedXMLElement('stage')
            stage_elem.attributes['id'] = str(stage_id)

            # attempt to merge together identical cases with different stage sets:
            cases_by_cond_set = {}

            for stage_set, cases in lineset.items():
                if stage_id in stage_set:
                    for case in cases:
                        cond_set = case.conditions_set()
                        if cond_set not in cases_by_cond_set:
                            cases_by_cond_set[cond_set] = case
                        else:
                            cases_by_cond_set[cond_set].states.extend(case.states)

            for case in cases_by_cond_set.values():
                stage_elem.children.append(case.to_xml(stage_id))

            behaviour_elem.children.append(stage_elem)

    return behaviour_elem, start_elem


def csv_to_lineset(dict_reader):
    opponent_meta = Opponent()
    stage_map = {} # maps stage sets to case condition sets to line lists.

    for line_no, row in enumerate(dict_reader):
        # strip leading/trailing whitespace from all keys and values:
        row_replace = {}
        for key, value in row.items():
            row_replace[key] = value.strip()
        row = row_replace

        case_tag = row['case'].lower()
        row['stage'] = row['stage'].lower()

        if row['stage'].startswith('comment') or row['stage'].startswith('#') or len(row['stage']) == 0:
            # treat this row as a comment
            continue

        if row['stage'].startswith('note'):
            print("note [line {}]: {}".format(line_no, row['text']))
            continue

        if row['stage'].startswith('todo') or row['stage'].startswith('to-do'):
            print("todo [line {}]: {}".format(line_no, row['text']))
            continue

        if row['stage'].startswith('meta'):
            if case_tag == 'timer':
                opponent_meta.timer = int(row['text'])
            elif case_tag == 'has_ending' or case_tag == 'enabled':
                setattr(opponent_meta, case_tag, (row['text'].lower() == 'true'))
            elif case_tag == 'tags':
                opponent_meta.tags.extend([tag.strip() for tag in row['text'].split(',')])
            elif case_tag == 'clothing':
                proper_name, lowercase, clothing_type, position = row['text'].split(',')
                opponent_meta.wardrobe.append(OrderedDict([
                    ('lowercase', lowercase),
                    ('position', position),
                    ('proper-name', proper_name),
                    ('type', clothing_type),
                ]))
            elif case_tag == 'intelligence':
                stage = None
                if row['conditions'].startswith('stage='):
                    stage = int(row['conditions'][6:])

                opponent_meta.intelligence.append((stage, row['text']))
            else:
                setattr(opponent_meta, case_tag, row['text'])

            continue

        stage_set = parse_stage_selector(row['stage'], opponent_meta)

        priority = None
        if ('priority' in row) and (len(row['priority']) > 0):
            priority = int(row['priority'])

        marker = None
        if ('marker' in row) and (len(row['marker']) > 0):
            marker = row['marker']

        silent = False
        if ('silent' in row) and (len(row['silent']) > 0):
            silent = (row['silent'].lower() == 'true')
            
        if len(row['image']) == 0 and len(row['text']) == 0:
            continue

        for actual_case_tag in parse_case_name(row['case'], row['conditions']):
            cond_set = Case.parse_conditions_set(row['conditions'], actual_case_tag, priority)

            if stage_set not in stage_map:
                stage_map[stage_set] = {}

            cond_map = stage_map[stage_set]
            if cond_set not in cond_map:
                cond_map[cond_set] = ([], actual_case_tag, row['conditions'], priority)

            state_text = preprocess_state_text(row['text'], actual_case_tag, cond_set)

            if len(state_text.strip()) == 0:
                print("Warning: empty state found (stages {}, case {}, row {})".format(row['stage'], row['case'], line_no))

            cond_map[cond_set][0].append(State(state_text, row['image'], marker, silent))

    lineset = {}
    for stage_set, cond_map in stage_map.items():
        for cond_set, case_tuple in cond_map.items():
            states, case_tag, conditions, priority = case_tuple

            case = Case(case_tag, conditions, priority)
            case.states = states

            if stage_set not in lineset:
                lineset[stage_set] = []

            lineset[stage_set].append(case)

    return lineset, opponent_meta


def lineset_to_csv(lineset, opponent_meta, dict_writer):
    # Write out opponent metadata:
    dict_writer.writerow({'stage': 'meta', 'case': 'first', 'text': opponent_meta.first})
    dict_writer.writerow({'stage': 'meta', 'case': 'last', 'text': opponent_meta.last})
    dict_writer.writerow({'stage': 'meta', 'case': 'label', 'text': opponent_meta.label})
    dict_writer.writerow({'stage': 'meta', 'case': 'gender', 'text': opponent_meta.gender})
    dict_writer.writerow({'stage': 'meta', 'case': 'size', 'text': opponent_meta.size})
    dict_writer.writerow({'stage': 'meta', 'case': 'timer', 'text': str(opponent_meta.timer)})

    for stage, intel in opponent_meta.intelligence:
        cond = ''
        if stage is not None:
            cond = 'stage={}'.format(stage)

        dict_writer.writerow({'stage': 'meta', 'conditions': cond, 'case': 'intelligence', 'text': intel})

    for tag in opponent_meta.tags:
        dict_writer.writerow({'stage': 'meta', 'case': 'tags', 'text': tag})

    if opponent_meta.enabled:
        dict_writer.writerow({'stage': 'meta', 'case': 'enabled', 'text': 'true'})
    else:
        dict_writer.writerow({'stage': 'meta', 'case': 'enabled', 'text': 'false'})

    dict_writer.writerow({'stage': 'meta', 'case': 'description', 'text': opponent_meta.description})
    dict_writer.writerow({'stage': 'meta', 'case': 'selection_pic', 'text': opponent_meta.selection_pic})
    dict_writer.writerow({'stage': 'meta', 'case': 'height', 'text': opponent_meta.height})
    dict_writer.writerow({'stage': 'meta', 'case': 'source', 'text': opponent_meta.source})
    dict_writer.writerow({'stage': 'meta', 'case': 'writer', 'text': opponent_meta.writer})
    dict_writer.writerow({'stage': 'meta', 'case': 'artist', 'text': opponent_meta.artist})

    if opponent_meta.has_ending:
        dict_writer.writerow({'stage': 'meta', 'case': 'has_ending', 'text': 'true'})
    else:
        dict_writer.writerow({'stage': 'meta', 'case': 'has_ending', 'text': 'false'})

    for clothing in opponent_meta.wardrobe:
        text = '{:s},{:s},{:s},{:s}'.format(clothing['proper-name'], clothing['lowercase'], clothing['type'], clothing['position'])
        dict_writer.writerow({'stage': 'meta', 'case': 'clothing', 'text': text})

    # Write out the dialogue now:
    for stage_set, cases in lineset.items():
        formatted_stage_set = format_stage_set(stage_set)

        for case in cases:
            formatted_conditions = case.format_conditions()

            priority = ''
            if case.priority is not None:
                priority = str(case.priority)

            for state in case.states:
                row = {
                    'stage': formatted_stage_set,
                    'case': case.tag,
                    'conditions': formatted_conditions,
                    'priority': case.priority,
                    'image': state.image,
                    'text': state.text,
                    'marker': state.marker,
                    'silent': state.silent
                }

                dict_writer.writerow(row)


def get_unique_line_count(lineset):
    unique_lines = set()
    unique_targeted_lines = set()
    n_cases = 0
    n_targeted_cases = 0

    for stage_set, cases in lineset.items():
        for case in cases:
            case_targeted = (len(case.conditions) > 0) or (len(case.counters) > 0)

            for state in case.states:
                unique_lines.add(state.to_tuple())
                if case_targeted:
                    unique_targeted_lines.add(state.to_tuple())

            n_cases += 1
            if case_targeted:
                n_targeted_cases += 1

    return len(unique_lines), len(unique_targeted_lines), n_cases, n_targeted_cases


def parse_xml_to_lineset(fname):
    opponent_elem = parse_file(fname)
    return xml_to_lineset(opponent_elem)

if __name__ == '__main__':
    if len(sys.argv) < 3:
        print("USAGE: python csv2xml.py [infile(.csv|.xml)] [outfile(.csv|.xml)]")

    infile = sys.argv[1]
    outfile = sys.argv[2]

    inroot, inext = osp.splitext(infile)
    outroot, outext = osp.splitext(outfile)

    print("Reading input file...")
    if inext == '.xml':
        opponent_elem = parse_file(infile)
        meta_elem = parse_meta(osp.join(osp.dirname(infile), 'meta.xml'))

        opponent_meta = Opponent.from_xml(opponent_elem, meta_elem)
        lineset = xml_to_lineset(opponent_elem)
    elif inext == '.csv':
        with open(infile, newline='', encoding='utf-8') as f:
            reader = csv.DictReader(f)
            lineset, opponent_meta = csv_to_lineset(reader)

    unique_lines, unique_targeted_lines, num_cases, num_targeted_cases = get_unique_line_count(lineset)

    print("Statistics:")
    print("Unique Lines: {}".format(unique_lines))
    print("Unique Targeted Lines: {}".format(unique_targeted_lines))
    print("Total Cases: {}".format(num_cases))
    print("Total Targeted Cases: {}".format(num_targeted_cases))

    print("Writing output file...")
    if outext == '.xml':
        opponent_elem = opponent_meta.to_xml()
        meta_elem = opponent_meta.to_meta_xml()

        behaviour_elem, start_elem = lineset_to_xml(lineset)
        opponent_elem.children.insert(-1, start_elem)
        opponent_elem.children.append(behaviour_elem)

        with open(outfile, 'w', encoding='utf-8') as f:
            f.write("<?xml version='1.0' encoding='UTF-8'?>\n")
            f.write('<!-- '+generate_comment()+' -->\n\n')
            f.write('<!--\n')
            f.write('    File Statistics:\n')
            f.write('    Unique Lines: {}\n'.format(unique_lines))
            f.write('    Unique Targeted Lines: {}\n'.format(unique_targeted_lines))
            f.write('    Total Cases: {}\n'.format(num_cases))
            f.write('    Total Targeted Cases: {}\n'.format(num_targeted_cases))
            f.write('-->\n\n'.format(num_targeted_cases))
            f.write(opponent_elem.serialize())

        with open(osp.join(osp.dirname(outfile), 'meta.xml'), 'w', encoding='utf-8') as meta_f:
            meta_f.write("<?xml version='1.0' encoding='UTF-8'?>\n")
            meta_f.write('<!-- '+generate_comment()+' -->\n')
            meta_f.write(meta_elem.serialize())

    elif outext == '.csv':
        with open(outfile, 'w', newline='', encoding='utf-8') as f:
            fieldnames = [
                'stage',
                'case',
                'conditions',
                'image',
                'text',
                'marker',
                'priority',
                'silent',
            ]

            writer = csv.DictWriter(f, fieldnames, restval='')
            writer.writeheader()
            writer.writerow({'stage': 'comment', 'text': generate_comment()})
            writer.writerow({'stage': 'comment', 'text': 'File Statistics:'})
            writer.writerow({'stage': 'comment', 'text': 'Unique Lines: {}'.format(unique_lines)})
            writer.writerow({'stage': 'comment', 'text': 'Unique Targeted Lines: {}'.format(unique_targeted_lines)})
            writer.writerow({'stage': 'comment', 'text': 'Total Cases: {}'.format(num_cases)})
            writer.writerow({'stage': 'comment', 'text': 'Total Targeted Cases: {}'.format(num_targeted_cases)})
            lineset_to_csv(lineset, opponent_meta, writer)
