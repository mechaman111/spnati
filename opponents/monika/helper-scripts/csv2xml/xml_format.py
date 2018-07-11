from .behaviour_parser import parse_file
from .ordered_xml import OrderedXMLElement
from .case import Case, CaseSet
from .state import State
from .stage import Stage


def parse_xml_to_lineset(fname):
    opponent_elem = parse_file(fname)
    return xml_to_lineset(opponent_elem)
    

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

    start_cases = CaseSet()
    select_cases = CaseSet()

    for stage_set, cases in filter(lambda kv: ('start' in kv[0]) or (0 in kv[0]), lineset.items()):
        for case in cases:
            if case.tag == 'select' or case.tag == 'selected':
                case.tag = 'selected'
                select_cases.add(case)
            elif case.tag == 'start':
                start_cases.add(case)

    stage_superset = set()
    for stage_set in lineset.keys():
        for k in stage_set:
            if isinstance(k, int):
                stage_superset.add(k)
            elif k != 'start':
                print("[Warning] invalid stage ID found: {:s}".format(k))

    for stage_id in sorted(stage_superset):
        if stage_id != 'start':
            stage_elem = OrderedXMLElement('stage')
            stage_elem.attributes['id'] = str(stage_id)
            
            stage_cases = CaseSet()

            for stage_set, cases in filter(lambda kv: stage_id in kv[0], lineset.items()):
                for case in cases:
                    stage_cases.add(case)

            if stage_id == 0:
                for case in select_cases:
                    stage_elem.children.append(case.to_xml(stage_id))
                    
                for case in start_cases:
                    stage_elem.children.append(case.to_xml(stage_id))

            for case in stage_cases:
                stage_elem.children.append(case.to_xml(stage_id))

            behaviour_elem.children.append(stage_elem)
            
    for case in filter(lambda c: c.is_generic(), select_cases):
        for state in case.states:
            start_elem.children.insert(0, state.to_xml(0))
    
    for case in filter(lambda c: c.is_generic(), start_cases):
        for state in case.states:
            start_elem.children.append(state.to_xml(0))

    return behaviour_elem, start_elem
