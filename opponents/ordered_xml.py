from __future__ import unicode_literals
    
from collections import OrderedDict

INDENT = '    '

class OrderedXMLElement(object):
    """Represents an XML element with an ordered set of attributes and either child elements or raw text within."""
    def __init__(self, type, init_text=None, init_attrs=None):
        super(OrderedXMLElement, self).__init__()
        
        self.type = type
        self.children = []
        self.text = init_text
        
        if init_attrs is not None:
            self.attributes = OrderedDict(init_attrs)
        else:
            self.attributes = OrderedDict()
        
    def find(self, tag):
        for child in self.children:
            if child.type == tag:
                return child
                
    def get(self, attr):
        return self.attributes[attr]
        
    def iter(self, find_tag):
        for child in self.children:
            if child.type == find_tag:
                yield child
    
    def __serialize(self):
        attr_string = " ".join(["{:s}=\"{:s}\"".format(k, v) for k,v in self.attributes.items()])
        
        if len(attr_string) > 0:
            attr_string = ' '+attr_string
        
        if len(self.children) == 0 and self.text is None:
            # <[type] [attributes...] />
            return ["<{:s}{:s} />".format(self.type, attr_string)]
        elif len(self.children) == 0 and self.text is not None:
            # <[type] [attributes...]>[text]</[type]>
            return ["<{:s}{:s}>{:s}</{:s}>".format(self.type, attr_string, self.text, self.type)]
        elif len(self.children) > 0 and self.text is None:
            l = ["<{:s}{:s}>".format(self.type, attr_string)]
            
            for child in self.children:
                for line in child.__serialize():
                    l.append(INDENT+line)
            
            l.append("</{:s}>".format(self.type))
            return l
        else:
            raise NotImplementedError("OrderedXMLElements do not support having both child elements and text.")
            
    def serialize(self):
        return '\n'.join(self.__serialize())
