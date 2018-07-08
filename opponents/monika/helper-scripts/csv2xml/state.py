import os
import os.path as osp
import re

from .ordered_xml import OrderedXMLElement
from . import utils

class State(object):
    def __init__(self, text, img, marker=None, silent=False):
        self.text = text
        self.image = img
        self.marker = marker
        self.silent = silent

    def __eq__(self, other):
        return (
            (self.text == other.text)
            and (self.image == other.image)
            and (self.marker == other.marker)
            and (self.silent == other.silent)
        )

    def to_tuple(self):
        return (self.text, self.image, self.marker, self.silent)

    @classmethod
    def from_tuple(cls, tup):
        return cls(*tup)

    @staticmethod
    def xml_to_tuple(elem):
        return (
            elem.text,
            elem.get('img'),
            elem.get('marker') if 'marker' in elem.attributes else None,
            bool(elem.get('silent')) if 'silent' in elem.attributes else False
        )

    @classmethod
    def from_xml(cls, elem):
        root, ext = osp.splitext(elem.get('img'))

        img_split = re.match(r'(\d+)\-(.+)', root)
        if img_split is not None:
            root = img_split.group(2)

        state = cls(elem.text, root)

        if 'marker' in elem.attributes:
            state.marker = elem.get('marker')

        if 'silent' in elem.attributes:
            state.silent = bool(elem.get('silent'))

        return state

    def to_xml(self, stage):
        elem = OrderedXMLElement('state')

        # assume all images are PNG files for now because:
        # - I am lazy
        # - this is meant for internal use only
        image = utils.find_image(self.image, int(stage))

        if not osp.exists(utils.get_image_path(image)):
            print("Warning: image {} does not exist! (Stage {}, linetext={})".format(image, stage, self.text))

        elem.attributes['img'] = image

        if self.marker is not None:
            elem.attributes['marker'] = self.marker

        if self.silent:
            elem.attributes['silent'] = 'true'

        elem.text = self.text

        return elem
