from __future__ import unicode_literals
    
import re
import sys
from .ordered_xml import OrderedXMLElement

if sys.version_info[0] < 3:
    from io import open

class ParseError(Exception):
    """Represents an error encountered during parsing."""
    def __init__(self, msg, parsed_seq, trace=None):
        super(ParseError, self).__init__(msg, parsed_seq, trace)
        
    def __str__(self):
        return self.args[0]
        
_opening_tag_start = re.compile(r'.*?\<([a-zA-Z0-9\-\_]+)\s*?', re.DOTALL)
_opening_tag_end = re.compile(r'(\/?)\>', re.DOTALL)

_attribute = re.compile(r"([a-zA-Z0-9\-\_\:]+)(?:\s*\=\s*(\"(.*?)\"))?")
_comment = re.compile(r'\s*(?:\<\!\-\-(.*?)\-\-\>)?', re.DOTALL)
_decl_tag = re.compile(r'\<\?(.*?)\?\>', re.DOTALL)  # ignored for now

base_tag_spec = {
    'opponent': {
        'first': None,
        'last': None,
        'label': None,
        'gender': None,
        'size': None,
        'timer': None,
        'intelligence': None,
        'tags': { 'tag': None },
        'start': { 'state': None },
        'wardrobe': { 'clothing': None },
        'behaviour': {
            'stage': {
                'case': { 'priority': None, 'condition': None, 'state': None }
            }
        },
        'epilogue': {
            'title': None,
            'background': {
                'scene': {
                    'sprite': {
                        'x': None,
                        'y': None,
                        'width': None,
                        'src': None
                    },
                    'text': {
                        'x': None,
                        'y': None,
                        'width': None,
                        'arrow': None,
                        'content': None,
                    }
                }
            },
            'screen': {
                'start': None,
                'text': {
                    'x': None,
                    'y': None,
                    'width': None,
                    'arrow': None,
                    'content': None,
                }
            }
        }
    }
} 

meta_tag_spec = {
    'opponent': {
        'enabled': None,
        'first': None,
        'last': None,
        'label': None,
        'pic': None,
        'gender': None,
        'height': None,
        'from': None,
        'writer': None,
        'artist': None,
        'description': None,
        'has_ending': None,
        'layers': None,
        'release': None,
        'tags': { 'tag': None },
        'epilogue': None,
        'alternates': { 'costume': None },
        'scale': None,
    }
}

listing_tag_spec = {
    'catalog': {
        'individuals': {
            'opponent': None
        },
        'groups': {
            'group': None
        }
    }
}

# skip whitespace and comments
def _skip_chars(seq, index):
    match = _comment.match(seq, index)
    while match is not None:
        if(len(match.group(0)) == 0):
            return index
            
        index += len(match.group(0))
        match = _comment.match(seq, index)
        
    return index

def _consume_char(seq, char, index, suppress_eof_error=False):
    index = _skip_chars(seq, index)
    
    if index >= len(seq)-1 and not suppress_eof_error:
        raise ParseError("Unexpected end of input", index)
    
    if seq.startswith(char, index):
        return True, index + len(char)
    else:
        return None, index

def _consume_re(seq, regex, index, suppress_eof_error=False):
    index = _skip_chars(seq, index)
    
    if index >= len(seq)-1 and not suppress_eof_error:
        raise ParseError("Unexpected end of input", index)
        
    match = regex.match(seq, index)
    if match is not None:
        index = match.end()
    
    return match, index

def parse_attribute_list(seq, elem, index):
    attr_match, index = _consume_re(seq, _attribute, index)
    
    while attr_match is not None:
        attr_name = attr_match.group(1).strip()
        try:
            elem.attributes[attr_name] = attr_match.group(3)
        except IndexError:
            elem.attributes[attr_name] = True
            
        attr_match, index = _consume_re(seq, _attribute, index)
        
    return index

def parse_tag(seq, index, tag_spec, progress_cb=None):
    if progress_cb is not None:
        progress_cb(index)
    
    match, index = _consume_re(seq, _opening_tag_start, index)
    if match is None:
        raise ParseError("Expected opening tag", index)
    
    _tag_start_index = index - len(match.group(0))
        
    tag_type = match.group(1)
    if tag_type not in tag_spec:
        raise ParseError("Unexpected tag type '{}'".format(tag_type), index)
    
    elem = OrderedXMLElement(tag_type)
    index = parse_attribute_list(seq, elem, index)
    
    match, index = _consume_re(seq, _opening_tag_end, index)
    if match is None:
        raise ParseError("Expected close for opening tag", index)
    
    simple_tag_match = (len(match.group(1)) > 0)
    
    try:
        # For simple tags (for example: <br />) just return the empty element
        if not simple_tag_match:
            # This tag contains either child text or child elements.
            child_tag_spec = tag_spec[tag_type]
            if child_tag_spec is None:
                # For text-only nodes, just grab everything up to the closing tag as the node's contents.
                tag_close_regex = re.compile(r'(.*?)\<\s*?\/{}\s*?\>'.format(re.escape(tag_type)), re.DOTALL)
                
                match, index = _consume_re(seq, tag_close_regex, index)
                if match is None:
                    raise ParseError("Could not find closing tag for <{:s}> element".format(tag_type), index)
                
                elem.text = match.group(1)
            else:
                # Otherwise, parse this node's child elements.
                # The tag-close regex here is slightly different from the one above.
                # we only want to know if the start of the string has a tag close.
                tag_close_regex = re.compile(r'\<\s*?\/{}\s*?\>'.format(re.escape(tag_type)))
            
                closing_tag_match, index = _consume_re(seq, tag_close_regex, index)
                while closing_tag_match is None:
                    child, index = parse_tag(seq, index, child_tag_spec, progress_cb)
                    elem.children.append(child)
                    closing_tag_match, index = _consume_re(seq, tag_close_regex, index)
    except ParseError as e:
        context = seq[_tag_start_index:_tag_start_index+50].strip()
        trace = "\n    in {:s} (pos. {:d}): {:s} ...".format(tag_type, _tag_start_index, context)
        if e.args[2] is not None:
            trace = e.args[2] + trace
        
        raise ParseError(e.args[0], e.args[1], trace)
                
    return elem, index

def parse(seq, tag_spec=base_tag_spec, progress_cb=None):
    _, index = _consume_re(seq, _decl_tag, 0)
    
    try:
        base_elem, _ = parse_tag(seq, index, tag_spec, progress_cb)
        return base_elem
    except ParseError as e:
        error_index = e.args[1]
            
        # find line number and position of error:
        error_line = 0
        error_pos = 0
        
        cur_idx = 0
        for i, line in enumerate(seq.split('\n')):
            if error_index < cur_idx + len(line) + 1:
                error_line = i
                error_pos = error_index - cur_idx
                break
            cur_idx += len(line)+1
            
        raise ParseError("{:s} at line {:d}, position {:d} (abs. position {:d})".format(e.args[0], error_line, error_pos, error_index), None)

def parse_file(fname, progress_cb=None):
    with open(fname, encoding='utf-8') as infile:
        if progress_cb is not None:
            seq = infile.read()
            
            def wrapped_progress_cb(cur_index):
                return progress_cb(len(seq), cur_index)
                
            return parse(seq, base_tag_spec, wrapped_progress_cb)
        else:
            return parse(infile.read())

def parse_meta(fname, progress_cb=None):
    with open(fname, encoding='utf-8') as infile:
        if progress_cb is not None:
            seq = infile.read()
            
            def wrapped_progress_cb(cur_index):
                return progress_cb(len(seq), cur_index)
                
            return parse(seq, meta_tag_spec, wrapped_progress_cb)
        else:
            return parse(infile.read(), meta_tag_spec)
            
def parse_listing(fname, progress_cb=None):
    with open(fname, encoding='utf-8') as infile:
        if progress_cb is not None:
            seq = infile.read()
            
            def wrapped_progress_cb(cur_index):
                return progress_cb(len(seq), cur_index)
                
            return parse(seq, listing_tag_spec, wrapped_progress_cb)
        else:
            return parse(infile.read(), listing_tag_spec)
