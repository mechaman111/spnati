import re
import shutil

STATE_MATCH_RE = r"(<state[^>]+>)(.+)(?=</state>)"

ESCAPED_CHARACTERS = {
    '&amp;': '&',
    '&lt;': '<',
    '&gt;': '>'
}
TEXT_UNESCAPE_RE = re.compile('|'.join(map(re.escape, ESCAPED_CHARACTERS.keys())))

def escape_xml(match):
    print(match[0])
    
    t = TEXT_UNESCAPE_RE.sub(
        lambda m: ESCAPED_CHARACTERS[m.group(0)],
        match[2]
    )
    
    for sub_to, sub_from in ESCAPED_CHARACTERS.items():
        t = t.replace(sub_from, sub_to)
        
    print(t)
    
    return match[1] + t

shutil.copyfile('./behaviour.xml', './behaviour.xml.escape.bak')

with open("./behaviour.xml", "r") as f:
    escaped = re.sub(STATE_MATCH_RE, escape_xml, f.read())
    
with open("./behaviour.xml", "w") as f:
    f.write(escaped)