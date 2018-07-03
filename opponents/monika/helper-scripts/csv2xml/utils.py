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
