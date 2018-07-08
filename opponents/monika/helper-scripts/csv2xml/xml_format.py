from .behaviour_parser import parse_file
from .ordered_xml import OrderedXMLElement
from .case import Case
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
