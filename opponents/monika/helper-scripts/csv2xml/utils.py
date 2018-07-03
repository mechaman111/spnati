import time

VERSION = '0.16.0-alpha'  # will attempt to follow semver if possible
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
