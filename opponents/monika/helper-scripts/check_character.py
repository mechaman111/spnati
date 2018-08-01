import csv2xml as c2x
import csv2xml.behaviour_parser as bp
import argparse
import logging
import functools

def get_cases_by_stage(lineset, opponent_meta, mode=None):
    cases_by_stage = [set() for _ in range(opponent_meta.len_stages())]
    
    for stage_set, cases in lineset.items():
        for case in cases:
            if case.is_conditional() and mode == 'generic':
                continue
                
            if not case.is_conditional() and mode == 'conditional':
                continue
                
            if not case.is_targeted() and mode == 'target':
                continue
                
            for stage in filter(lambda x: isinstance(x, int), stage_set):
                cases_by_stage[stage].add(case.tag)
    
    return cases_by_stage


def check_character_generics(lineset, opponent_meta):
    """
    Ensure a character has generics for all cases.
    """
    cases_by_stage = get_cases_by_stage(lineset, opponent_meta, 'generic')

    stages = range(opponent_meta.len_stages())
    for tag, interval in c2x.Case.TAG_INTERVALS.items():
        for stage in filter(lambda s: tag not in cases_by_stage[s], stages[interval]):
            logging.error("Stage {:d} is missing generic lines for case {:s}".format(stage, tag))


def check_impossible_cases(lineset, opponent_meta):
    """
    Check for impossible case and stage combinations.
    """
    
    cases_by_stage = get_cases_by_stage(lineset, opponent_meta)
    stages = range(opponent_meta.len_stages())
    for stage, tag_set in enumerate(cases_by_stage):
        for tag in tag_set:
            if stage not in stages[c2x.Case.TAG_INTERVALS[tag]]:
                logging.error("Stage {:d} contains case {:s} that will never be used".format(stage, tag))


def check_undefined_cases(lineset, opponent_meta):
    """
    Check for undefined cases.
    """
    
    cases_by_stage = get_cases_by_stage(lineset, opponent_meta)
    for stage, case_set in enumerate(cases_by_stage):
        for case_tag in filter(lambda x: x not in c2x.Case.ALL_TAGS, case_set):
            logging.error("Stage {:d} contains unknown case {:s}".format(stage, case_tag))


def main(args):
    lineset, opponent_meta = c2x.load_character(args.opponent_id, args.opponents_dir)
    
    check_character_generics(lineset, opponent_meta)
    check_impossible_cases(lineset, opponent_meta)
    check_undefined_cases(lineset, opponent_meta)
    
    
if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Does basic checks for an opponent's behaviour.xml file.")
    parser.add_argument('--opponents-dir', '-d', help="Path to SPNATI's opponents/ directory. If not set it will be searched for starting from the current working directory.")
    parser.add_argument('opponent_id', help='ID of the character to check.')
    main(parser.parse_args())
