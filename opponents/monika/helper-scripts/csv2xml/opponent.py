from collections import OrderedDict
from .ordered_xml import OrderedXMLElement


class Opponent(object):
    GENDER_MALE = 'male'
    GENDER_FEMALE = 'female'

    SIZE_SMALL = 'small'
    SIZE_MEDIUM = 'medium'
    SIZE_LARGE = 'large'

    INTEL_BAD = 'bad'
    INTEL_AVG = 'average'
    INTEL_GOOD = 'good'

    def __init__(self):
        self.enabled = True
        self.first = None
        self.last = None
        self.label = ''
        self.intelligence = []
        self.size = self.SIZE_MEDIUM
        self.gender = self.GENDER_FEMALE
        self.timer = 0
        self.wardrobe = []
        self.tags = []
        self.endings = []

        self.description = ''
        self.selection_pic = ''
        self.height = ''
        self.source = ''
        self.writer = ''
        self.artist = ''
        self.has_ending = False


    def len_stages(self):
        return len(self.wardrobe) + 3

    def lost_clothing_stage(self, clothing_name):
        if len(self.wardrobe) == 0:
            raise ValueError("Attempting to access wardrobe data that hasn't been loaded yet! Are you sure your metadata is at the top of your input file?")

        clothing_name = clothing_name.lower()
        for stage, clothing in enumerate(self.wardrobe):
            if clothing_name == clothing['lowercase']:
                return len(self.wardrobe) - stage

    def naked_stage(self):
        return len(self.wardrobe)

    def masturbate_stage(self):
        return len(self.wardrobe) + 1

    def finished_stage(self):
        return len(self.wardrobe) + 2

    @classmethod
    def from_meta_xml(cls, meta_elem):
        opp = cls()
        
        opp.first = meta_elem.find('first').text
        opp.last = meta_elem.find('last').text
        opp.label = meta_elem.find('label').text
        opp.gender = meta_elem.find('gender').text.lower()
        opp.size = meta_elem.find('size').text.lower()
        opp.selection_pic = meta_elem.find('pic').text
        opp.height = meta_elem.find('height').text
        opp.source = meta_elem.find('from').text
        opp.writer = meta_elem.find('writer').text
        opp.artist = meta_elem.find('artist').text
        opp.description = meta_elem.find('description').text
        
        opp.has_ending = (meta_elem.find('has_ending') is not None) and (meta_elem.find('has_ending').text.strip().lower() == 'true')
        for ending in opponent_elem.iter('epilogue'):
            opp.has_ending = True
            opp.endings.append(ending)
            
        opp.tags = [tag.text for tag in opponent_elem.find('tags').iter('tag')]
        
        return opp

    @classmethod
    def from_xml(cls, opponent_elem, meta_elem):
        opp = cls()

        opp.first = opponent_elem.find('first').text
        opp.last = opponent_elem.find('last').text
        opp.label = opponent_elem.find('label').text
        opp.timer = int(opponent_elem.find('timer').text)
        opp.gender = opponent_elem.find('gender').text.lower()
        opp.size = opponent_elem.find('size').text.lower()

        opp.tags = [tag.text for tag in opponent_elem.find('tags').iter('tag')]

        for clothing in opponent_elem.find('wardrobe').iter('clothing'):
            opp.wardrobe.append(OrderedDict([
                ('lowercase', clothing.attributes['lowercase']),
                ('position', clothing.attributes['position']),
                ('proper-name', clothing.attributes['proper-name']),
                ('type', clothing.attributes['type']),
            ]))

        for intel in opponent_elem.iter('intelligence'):
            stage = None
            if 'stage' in intel.attributes:
                stage = intel.attributes['stage']

            opp.intelligence.append((stage, intel.text))

        if meta_elem is not None:
            opp.enabled = (meta_elem.find('enabled').text.strip().lower() == 'true')
            opp.selection_pic = meta_elem.find('pic').text
            opp.height = meta_elem.find('height').text
            opp.source = meta_elem.find('from').text
            opp.writer = meta_elem.find('writer').text
            opp.artist = meta_elem.find('artist').text
            opp.description = meta_elem.find('description').text
            
            opp.has_ending = (meta_elem.find('has_ending') is not None) and (meta_elem.find('has_ending').text.strip().lower() == 'true')
            for ending in opponent_elem.iter('epilogue'):
                opp.has_ending = True
                opp.endings.append(ending)

        return opp

    def to_meta_xml(self):
        meta_elem = OrderedXMLElement('opponent')

        if self.enabled:
            meta_elem.children.append(OrderedXMLElement('enabled', 'true'))
        else:
            meta_elem.children.append(OrderedXMLElement('enabled', 'false'))

        meta_elem.children.append(OrderedXMLElement('first', self.first))
        meta_elem.children.append(OrderedXMLElement('last', self.last))
        meta_elem.children.append(OrderedXMLElement('label', self.label))
        meta_elem.children.append(OrderedXMLElement('pic', self.selection_pic))
        meta_elem.children.append(OrderedXMLElement('gender', self.gender))
        meta_elem.children.append(OrderedXMLElement('height', self.height))
        meta_elem.children.append(OrderedXMLElement('from', self.source))
        meta_elem.children.append(OrderedXMLElement('writer', self.writer))
        meta_elem.children.append(OrderedXMLElement('artist', self.artist))
        meta_elem.children.append(OrderedXMLElement('description', self.description))

        if self.has_ending:
            meta_elem.children.append(OrderedXMLElement('has_ending', 'true'))
        else:
            meta_elem.children.append(OrderedXMLElement('has_ending', 'false'))

        meta_elem.children.append(OrderedXMLElement('layers', str(len(self.wardrobe))))

        tags = OrderedXMLElement('tags')
        for tag in self.tags:
            tags.children.append(OrderedXMLElement('tag', tag))

        meta_elem.children.append(tags)

        return meta_elem

    def to_xml(self):
        opponent_elem = OrderedXMLElement('opponent')

        opponent_elem.children.append(OrderedXMLElement('first', self.first))
        opponent_elem.children.append(OrderedXMLElement('last', self.last))
        opponent_elem.children.append(OrderedXMLElement('label', self.label))
        opponent_elem.children.append(OrderedXMLElement('size', self.size))
        opponent_elem.children.append(OrderedXMLElement('gender', self.gender))
        opponent_elem.children.append(OrderedXMLElement('timer', str(self.timer)))

        for stage, intelligence in self.intelligence:
            if stage is not None:
                attrs = [('stage', stage)]
            else:
                attrs = None

            opponent_elem.children.append(OrderedXMLElement('intelligence', intelligence, attrs))

        tags = OrderedXMLElement('tags')
        for tag in self.tags:
            tags.children.append(OrderedXMLElement('tag', tag))

        opponent_elem.children.append(tags)

        wardrobe = OrderedXMLElement('wardrobe')
        for clothing_attrs in self.wardrobe:
            wardrobe.children.append(OrderedXMLElement('clothing', None, clothing_attrs))

        opponent_elem.children.append(wardrobe)

        return opponent_elem
