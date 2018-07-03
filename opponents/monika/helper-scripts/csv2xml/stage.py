import re
from .utils import *
from .case import Case


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
