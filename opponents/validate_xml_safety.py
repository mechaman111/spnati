#!/usr/bin/env python3
from __future__ import print_function    # in case we happen to be on python2...

import sys
import time
import bleach.sanitizer
from xml.etree.ElementTree import ParseError
import defusedxml.ElementTree as ET

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
    
    for stage_no, tag, line, raw_text, cleaned_text in failed_lines:
        report_out.write("==============\n")
        report_out.write("Stage {}, case {}, line {}:\n".format(stage_no, tag, line))
        report_out.write(raw_text.strip()+'\n')
        report_out.write(cleaned_text.strip()+'\n')
    
    report_out.write("==============\n")
    report_out.write("End of report.\n")
    report_out.write("==============\n")
    report_out.write("Checked {} lines in total.\n".format(total_lines))
    report_out.write("Passed: {}\n".format(total_lines - total_failed))
    report_out.write("Failed: {}\n".format(total_failed))

def get_state_text(state):
    # Get the complete inner text of the dialogue,
    # along with all sub-tags
    raw_text = (state.text or '')
    for subelem in state:
        raw_text += ET.tostring(subelem, encoding='unicode')
    raw_text += (state.tail or '')
    return raw_text

def check_file(fname):
    print("Checking: {}".format(fname))
    
    tree = ET.parse(fname)
    cleaner = bleach.sanitizer.Cleaner(allowed_tags, allowed_attributes, allowed_styles, allowed_protocols)
    
    total_lines = 0
    total_failed = 0
    
    failed_lines = []
    
    def check_state(stage_no, tag, i, state):
        nonlocal total_lines, total_failed
        
        raw_text = get_state_text(state)
        
        if len(raw_text) == 0:
            print("Stage {}, case {}: No text found for line {}, skipping...".format(stage_no, tag, i))
        else:
            total_lines += 1
            cleaned_text = cleaner.clean(raw_text)
            
            if raw_text != cleaned_text:
                print("Stage {}, case {}: Line {} failed validation".format(stage_no, tag, i))
                total_failed += 1
                failed_lines.append((stage_no, tag, i+1, raw_text, cleaned_text))
                
    # check start lines:
    for i, state in enumerate(tree.find('start').iter('state')):
        check_state(0, 'start', i, state)
        
    for stage in tree.iter('stage'):
        stage_no = int(stage.get('id'))
        
        for case in stage.iter('case'):
            tag = case.get('tag')
            
            for i, state in enumerate(case.iter('state')): 
                check_state(stage_no, tag, i, state)
                    
    print("Checked {} lines in total.".format(total_lines))
    print("Passed: {}".format(total_lines - total_failed))
    print("Failed: {}".format(total_failed))
        
    return (total_lines, total_failed), failed_lines

if __name__ == "__main__":
    xml_filename = "behaviour.xml"
    report_filename = "safety_validation.log"
    
    if len(sys.argv) == 2:
        xml_filename = sys.argv[1]
    elif len(sys.argv) > 2:
        # multiple file mode
        report_filename = "safety_validation_multi.log"
        details_filename = "safety_validation_details.log"
        print("Checking {} files...".format(len(sys.argv[1:])))
        with open(report_filename, 'w') as summary_out:
            with open(details_filename, 'w') as details_out:
                summary_out.write("<filename> : <validated / failed> : lines total / lines passed / lines failed\n")
                write_report_header(details_out)
                
                for filename in sys.argv[1:]:
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
                    except Exception as e:
                        print("Warning: encountered error while checking {}: {}".format(filename, str(e)))
                        summary_out.write("{} : error : {}\n".format(filename, str(e)))
            
        sys.exit()

    # single-file mode
    summary_data, failed_lines = check_file(xml_filename)
    with open(report_filename, 'w') as report_out:
        write_report_header(report_out)
        write_report(summary_data, failed_lines, xml_filename, report_out)
