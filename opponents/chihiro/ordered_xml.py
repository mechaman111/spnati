from __future__ import unicode_literals
import re
from collections import OrderedDict

INDENT = '    '

ATTR_ESCAPES = {
    '<': 'lt',
    '>': 'gt',
    '&': 'amp',
    '"': 'quot',
    "\n": '#10'
}
ATTR_ESCAPE_RE = re.compile('|'.join(ATTR_ESCAPES.keys()))

TEXT_ESCAPES = {
    '<': 'lt',
    '>': 'gt',
    '&': 'amp'
}
TEXT_ESCAPE_RE = re.compile('|'.join(TEXT_ESCAPES.keys()))

ENTITY_RE = re.compile('&(?:(\w+)|#(?:(\d+)|x([\da-f]+)));')

TEXT_UNESCAPES = {
    '&amp;': '&',
    '&lt;': '<',
    '&gt;': '>'
}
TEXT_UNESCAPE_RE = re.compile('|'.join(map(re.escape, TEXT_UNESCAPES.keys())))

def XMLEscape(string, table, regex):
    return regex.sub(lambda m: '&' + table[m.group(0)] + ';', string)

def XMLUnescape(string, table, regex):
    return regex.sub(lambda m: table[m.group(0)], string)

class OrderedXMLElement(object):
    """Represents an XML element with an ordered set of attributes and either child elements or raw text within."""
    def __init__(self, type, init_text=None, init_attrs=None, **kwargs):
        super(OrderedXMLElement, self).__init__()
        
        self.type = type
        self.children = []
        self.text = init_text
        self.blank_before = kwargs['blank_before'] if 'blank_before' in kwargs else False
        self.blank_after = kwargs['blank_after'] if 'blank_after' in kwargs else False
        
        if init_attrs is not None:
            self.attributes = OrderedDict(init_attrs)
        else:
            self.attributes = OrderedDict()

    def append(self, el):
        self.children.append(el)

    def subElement(self, type, init_text=None, init_attrs=None, **kwargs):
        newEl = OrderedXMLElement(type, init_text, init_attrs, **kwargs)
        self.append(newEl)
        return newEl
        
    def find(self, tag):
        for child in self.children:
            if child.type == tag:
                return child
                
    def get(self, attr):
        return self.attributes[attr]

    def set(self, attr, value):
        self.attributes[attr] = value
        
    def iter(self, find_tag):
        for child in self.children:
            if child.type == find_tag:
                yield child

    def __serialize(self, level=0):
        attr_string = " ".join(["{:s}=\"{:s}\"".format(k, XMLEscape(str(v), ATTR_ESCAPES, ATTR_ESCAPE_RE)) for k,v in self.attributes.items()])
        
        if len(attr_string) > 0:
            attr_string = ' '+attr_string

        if self.type is None: # Comment
            return ["<!--{:s}-->".format(self.text)]
        elif len(self.children) == 0 and (self.text is None or self.text == ""):
            # <[type] [attributes...] />
            return ["<{:s}{:s} />".format(self.type, attr_string)]
        elif len(self.children) == 0 and self.text is not None:
            # <[type] [attributes...]>[text]</[type]>
            self.text = XMLUnescape(self.text, TEXT_UNESCAPES, TEXT_UNESCAPE_RE)
            return ["<{:s}{:s}>{:s}</{:s}>".format(self.type, attr_string, XMLEscape(self.text, TEXT_ESCAPES, TEXT_ESCAPE_RE), self.type)]
        elif len(self.children) > 0 and self.text is None:
            l = ["<{:s}{:s}>".format(self.type, attr_string)]
            for child in self.children:
                if child.blank_before and (len(l) == 0 or l[-1] != ''):
                    l.append('')
                for line in child.__serialize(level + 1):
                    l.append((INDENT+line).rstrip())
                if child.blank_after:
                    l.append('')

            l.append("</{:s}>".format(self.type))
            return l
        else:
            raise NotImplementedError("OrderedXMLElements do not support having both child elements and text.")
            
    def serialize(self):
        return '\n'.join(["<?xml version='1.0' encoding='UTF-8'?>"] + self.__serialize(0)) + '\n'

# This is similar to how ElementTree does it, except we just set the tag to None to signify a comment.
def Comment(text):
    return OrderedXMLElement(None, text)
