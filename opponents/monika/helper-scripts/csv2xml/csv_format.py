import logging
from collections import OrderedDict
import re

from .case import Case, parse_case_name
from .state import State
from .opponent import Opponent
from .stage import parse_stage_selector, format_stage_set
from .epilogue import Epilogue, Screen, TextBox
from . import utils


# Called from csv_to_lineset below.
def _handle_epilogue_row(row, epilogue_no, epilogues):
    if epilogue_no not in epilogues:
        epilogues[epilogue_no] = Epilogue(None)
        
    epilogue = epilogues[epilogue_no]
    case_tag = row['case'].lower()
    cond_tuples = [t.split('=', 1) for t in row['conditions'].split(',')]
    conditions = OrderedDict()
    
    for attr, val in cond_tuples:
        attr = attr.strip().lower()
        conditions[attr] = val.strip()
        
    if case_tag == '' or case_tag == 'title':
        epilogue.title = row['text']
        epilogue.conditions = conditions
    else:
        screen_match = re.match(r'screen\s*(?:\:|\-)?\s*(\d+)', case_tag)
        if screen_match is None:
            raise ValueError("Invalid screen case syntax: {:s} in epilogue {:d}".format(case_tag, epilogue_no))
        
        screen_num = int(screen_match[1])
        
        if len(epilogue.screens) > screen_num:
            raise KeyError("Invalid screen number {:d} in epilogue {:d}".format(screen_num, epilogue_no))
        
        if len(row['image']) > 0:
            img = utils.find_image(row['image'])
        else:
            img = ''
        
        if screen_num == len(epilogue.screens):
            # add a new screen:
            screen = Screen(img)
            epilogue.screens.append(screen)
        else:
            screen = epilogue.screens[screen_num]
            if screen.image != img:
                logging.warning("Ignoring redundant image definition for screen {:d} in epilogue {:d}".format(screen_num, epilogue_no))
            
        box = TextBox(row['text'], conditions['x'], conditions['y'], conditions.get('width', '20%'), conditions.get('arrow', None))
        screen.boxes.append(box)


def csv_to_lineset(dict_reader):
    opponent_meta = Opponent()
    stage_map = {} # maps stage sets to case condition sets to line lists.

    epilogues = OrderedDict()

    for line_no, row in enumerate(dict_reader):
        # strip leading/trailing whitespace from all keys and values:
        row_replace = {}
        for key, value in row.items():
            row_replace[key] = value.strip()
        row = row_replace

        case_tag = row['case'].lower()
        row['stage'] = row['stage'].lower()
        
        if len(row['stage']) <= 0 or len(case_tag) <= 0:
            continue
        
        if row['stage'] != 'meta' and len(row['image']) <= 0 and len(row['text']) <= 0:
            logging.warning("Skipping line for case {:s} in stage(s) {:s}!".format(case_tag, row['stage']))
            continue

        if row['stage'].startswith('comment') or row['stage'].startswith('#') or len(row['stage']) == 0:
            # treat this row as a comment
            continue

        if row['stage'].startswith('note'):
            logging.info("note [line {}]: {}".format(line_no, row['text']))
            continue

        if row['stage'].startswith('todo') or row['stage'].startswith('to-do'):
            logging.info("todo [line {}]: {}".format(line_no, row['text']))
            continue
            
        if row['stage'].startswith('epilogue'):
            epilogue_match = re.match(r'epilogue\s*(?:\:|\-)?\s*(\d+)', row['stage'])
            if epilogue_match is None:
                logging.warning("Invalid epilogue stage syntax: {:s}".format(row['stage']))
                continue
            
            _handle_epilogue_row(row, int(epilogue_match[1]), epilogues)
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

            if len(row['text'].strip()) == 0:
                logging.error("Warning: empty state found (stages {}, case {}, row {})".format(row['stage'], row['case'], line_no))

            cond_map[cond_set][0].append(State(row['text'], row['image'], marker, silent))

    lineset = {}
    for stage_set, cond_map in stage_map.items():
        for cond_set, case_tuple in cond_map.items():
            states, case_tag, conditions, priority = case_tuple

            case = Case(case_tag, conditions, priority)
            case.states = states

            if stage_set not in lineset:
                lineset[stage_set] = []

            lineset[stage_set].append(case)
            
    return lineset, opponent_meta, list(epilogues.values())


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
                    'priority': priority,
                    'image': state.image,
                    'text': state.text,
                    'marker': state.marker,
                    'silent': state.silent
                }

                dict_writer.writerow(row)
