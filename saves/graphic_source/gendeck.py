#!/usr/bin/python3

import lxml.etree as ET
import copy
import sys

#ET.register_namespace('', 'http://www.w3.org/2000/svg')
svg = ET.parse(sys.stdin)

suits = ('spade', 'heart', 'diamo', 'clubs')
ranks = ('A', '2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K')

for suit in suits:
    for rank in range(1, 14):
        card = copy.deepcopy(svg)
        layer = card.find('{*}g')
        for path in layer.findall('{*}path'):
            if path.get('id') != suit:
                layer.remove(path)
        text = layer.find('.{*}text')
        rankspan = text.find('{*}tspan')
        rankspan.text = str(ranks[rank - 1])
        if suit == 'clubs' and rank == 10:
            text.attrib['y'] = rankspan.attrib['y'] = '67'

        for el in card.findall('.//*'):
            if 'id' in el.attrib:
                del el.attrib['id']
        card.write('%s%i.svg' % (suit, rank))

        if suit == 'clubs' or suit == 'diamo':
            path = card.find('.//{*}path')
            path.attrib['stroke'] = path.attrib['fill'] = "#187800" if suit == 'clubs' else "#1020ff"
            card.write('%s%s%i.svg' % ('green' if suit == 'clubs' else 'blue', suit, rank))
