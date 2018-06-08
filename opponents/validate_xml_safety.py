#!/usr/bin/env python3
from __future__ import print_function    # in case we happen to be on python2...

import sys
import time
import bleach.sanitizer
import glob
from behaviour_parser import ParseError, parse_file

# A list of allowed HTML tags within dialogue.
#
# This is just a conservative guess.
# I'm pretty sure that the CE and make_xml.py also eat any tags that aren't <i>, so...
allowed_tags = ['b', 'i', 's', 'u', 'strong', 'em']

# Allowed attributes for HTML tags within dialogue.
#
# See:
# https://bleach.readthedocs.io/en/latest/clean.html
# for specifics on how this should be structured.
allowed_attributes = {}

# Right now, we don't allow the `style` attribute, but if we did,
# this list would contain the list of things allowed to be styled in case HTML
# using the style attribute.
allowed_styles = []

# By default, Bleach allows the 'http:', 'https:', and 'mailto:' protocols
# to be used in URIs (for example, links).
# I'm restricting that to purely HTTP and HTTPs-- there isn't any reason
# for us to be sending email in SPNATI, is there?
allowed_protocols = ['http', 'https']

def write_report_header(report_out):
    report_out.write("NOTE: Lines that failed validation are printed with the original state text first,\n")
    report_out.write("followed by a cleaned version of the text. Your files have not been modified.\n")

def write_report(summary_data, failed_lines, fname, report_out):
    total_lines, total_failed = summary_data
    
    report_out.write("==============\n")
    report_out.write("Checking {} at {}\n".format(fname, time.asctime()))
    
    for context, raw_text, cleaned_text in failed_lines:
        report_out.write("==============\n")
        report_out.write(context+":\n")
        report_out.write(raw_text.strip()+'\n')
        report_out.write(cleaned_text.strip()+'\n')
    
    report_out.write("==============\n")
    report_out.write("End of report.\n")
    report_out.write("==============\n")
    report_out.write("Checked {} lines in total.\n".format(total_lines))
    report_out.write("Passed: {}\n".format(total_lines - total_failed))
    report_out.write("Failed: {}\n".format(total_failed))

def check_file(fname):
    print("Checking: {}".format(fname))
    tree = parse_file(fname)
    cleaner = bleach.sanitizer.Cleaner(allowed_tags, allowed_attributes, allowed_styles, allowed_protocols)
    
    stats = {
        'total_lines': 0,
        'total_failed': 0
    }
    
    failed_lines = []
    
    def check_text(raw_text, context):
        if raw_text is None or len(raw_text) == 0:
            print("{:s}: No text found, skipping...".format(context))
        else:
            stats['total_lines'] = stats['total_lines'] + 1
            cleaned_text = cleaner.clean(raw_text)
            
            if raw_text != cleaned_text:
                print("{:s} failed validation...".format(context))
                stats['total_failed'] = stats['total_failed'] + 1
                failed_lines.append((context, raw_text, cleaned_text))
                
    check_text(tree.find('first').text, "First name")
    check_text(tree.find('last').text, "Last name")
    check_text(tree.find('label').text, "Label")
                
    # check start lines:
    for i, state in enumerate(tree.find('start').iter('state')):
        check_text(state.text, "Starting line {}".format(i+1))
        
    # check clothing names:
    for i, clothing in enumerate(tree.find('wardrobe').iter('clothing')):
        check_text(clothing.get('lowercase'), "Lowercase name for wardrobe item {}".format(i+1))
        check_text(clothing.get('proper-name'), "Proper name for wardrobe item {}".format(i+1))
        
    # check behaviour:
    for stage in tree.find('behaviour').iter('stage'):
        stage_no = int(stage.get('id'))
        
        for case_i, case in enumerate(stage.iter('case')):
            tag = case.get('tag')
            
            for i, state in enumerate(case.iter('state')):
                check_text(state.text, "Stage {}, case {} ({}), line {}".format(stage_no, case_i, tag, i+1))
                
    # check epilogues:
    for epilogue_i, epilogue in enumerate(tree.iter('epilogue')):
        check_text(epilogue.find('title').text, "Title for epilogue {}".format(epilogue_i+1))
        
        for screen_i, screen in enumerate(epilogue.iter('screen')):
            for i, text_elem in enumerate(screen.iter('text')):
                check_text(text_elem.find('content').text, "Epilogue {}, screen {}, text box {}".format(epilogue_i+1, screen_i+1, i+1))
                    
    print("Checked {} lines in total.".format(stats['total_lines']))
    print("Passed: {}".format(stats['total_lines'] - stats['total_failed']))
    print("Failed: {}".format(stats['total_failed']))
        
    return (stats['total_lines'], stats['total_failed']), failed_lines

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("USAGE: python validate_xml_safety.py [input filename globs...]")
        
    report_filename = "safety_validation.log"
    
    files = []
    
    for input_glob in sys.argv[1:]:
        files.extend(glob.glob(input_glob))

    if len(files) > 1:
        # multiple file mode
        
        report_filename = "safety_validation_multi.log"
        details_filename = "safety_validation_details.log"
        print("Checking {} files...".format(len(files)))
        
        with open(report_filename, 'w') as summary_out:
            with open(details_filename, 'w') as details_out:
                summary_out.write("<filename> : <validated / failed> : lines total / lines passed / lines failed\n")
                write_report_header(details_out)
                
                for filename in files:
                    try:
                        summary_data, failed_lines = check_file(filename)
                        total_lines, total_failed = summary_data
                        
                        if summary_data[1] > 0:
                            validated = 'failed'
                        else:
                            validated = 'validated'
                            
                        write_report(summary_data, failed_lines, filename, details_out)
                        summary_out.write("{} : {} : {} / {} / {}\n".format(
                            filename, validated, total_lines, total_lines-total_failed, total_failed
                        ))
                    except ParseError as e:
                        print("Warning: encountered error while checking {}: {}".format(filename, str(e)))
                        summary_out.write("{} : error : {}\n".format(filename, str(e)))    
    else:
        # single-file mode
        xml_filename = files[0]
        summary_data, failed_lines = check_file(xml_filename)
        with open(report_filename, 'w') as report_out:
            write_report_header(report_out)
            write_report(summary_data, failed_lines, xml_filename, report_out)
