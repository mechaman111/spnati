import csv2xml as c2x
import csv2xml.behaviour_parser as bp
import argparse

def get_cases_by_stage(lineset, opponent_meta):
    cases_by_stage = [set() for _ in range(opponent_meta.len_stages())]
    
    for stage_set, cases in lineset.items():
        for case in filter(lambda case: not case.is_conditional(), cases):
            for stage in filter(lambda x: isinstance(x, int), stage_set):
                cases_by_stage[stage].add(case.tag)
    
    return cases_by_stage


def check_character_generics(cases_by_stage):
    """
    Ensure a character has generics for all cases.
    """
    
    def report(stage, missing_cases):
        for case_tag in missing_cases:
            print("Stage {:d} is missing generic lines for case {:s}".format(stage, case_tag))
    
    def find_missing_tags_single(tag_list, stage):
        tag_set = frozenset(tag_list)
        report(start, tag_set.difference(cases_by_stage[stage]))
    
    def find_missing_tags(tag_list, start, end):
        tag_set = frozenset(tag_list)
        for stage, case_set in enumerate(cases_by_stage[start:end]):
            report(stage+start, tag_set.difference(case_set))
    
    find_missing_tags(c2x.Case.ALWAYS_TAGS, 0, None)
    find_missing_tags(c2x.Case.PLAYING_TAGS, 0, -2)
    find_missing_tags(c2x.Case.CLOTHED_STAGE_TAGS, 0, -3)
    find_missing_tags_single(c2x.Case.START_TAGS, 0)
    find_missing_tags_single(c2x.Case.NAKED_STAGE_TAGS, -3)
    find_missing_tags_single(c2x.Case.MASTURBATION_STAGE_TAGS, -2)
    find_missing_tags_single(c2x.Case.FINISHED_STAGE_TAGS, -1)


def check_impossible_cases(cases_by_stage):
    """
    Check for impossible case and stage combinations.
    """
    
    for stage, case_set in enumerate(cases_by_stage):
        impossible_tags = []
        
        if stage != 0:
            impossible_tags += c2x.Case.START_TAGS
        
        if stage != opponent_meta.masturbate_stage():
            impossible_tags += c2x.Case.MASTURBATION_STAGE_TAGS
            
        if stage != opponent_meta.finished_stage():
            impossible_tags += c2x.Case.FINISHED_STAGE_TAGS
            
        if stage != opponent_meta.naked_stage():
            impossible_tags += c2x.Case.NAKED_STAGE_TAGS 
        
        if stage >= opponent_meta.masturbate_stage():
            impossible_tags += c2x.Case.PLAYING_STAGE_TAGS
        
        if stage >= opponent_meta.naked_stage():
            impossible_tags += c2x.Case.CLOTHED_STAGE_TAGS
            
        impossible_tags = frozenset(impossible_tags)
        for case_tag in impossible_tags.intersection(case_set)
            print("Stage {:d} contains case {:s} that will never be used".format(stage, case_tag))


def check_undefined_cases(cases_by_stage):
    """
    Check for undefined cases.
    """
    
    for stage, case_set in enumerate(cases_by_stage):
        for case_tag in filter(lambda x: x not in c2x.Case.ALL_TAGS, case_set):
            print("Stage {:d} contains unknown case {:s}".format(stage, case_tag))


def main(args):
    lineset, opponent_meta = c2x.load_character(args.opponent_id, args.opponents_dir)
    cases_by_stage = get_cases_by_stage(lineset, opponent_meta)
    
    check_character_generics(cases_by_stage)
    check_impossible_cases(cases_by_stage)
    check_undefined_cases(cases_by_stage)
    
    
if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Does basic checks for an opponent's behaviour.xml file.")
    parser.add_argument('--opponents-dir', '-d', help="Path to SPNATI's opponents/ directory. If not set it will be searched for starting from the current working directory.")
    parser.add_argument('opponent_id', help='ID of the character to check.')
    main(parser.parse_args())
