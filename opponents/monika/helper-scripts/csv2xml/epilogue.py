from collections import OrderedDict

from .ordered_xml import OrderedXMLElement


class TextBox(object):
    def __init__(self, text, x, y, width='20%', arrow=None):
        """
        A text box within an epilogue screen.
        
        Attributes:
            text (str): The content of the text box.
            x (str): The position of the text box's left side. Technically any CSS value, but usually either a percentage, or the word 'centered'.
            y (str): The position of the text box's top.
            width (str): The width of the text box. Can be None, or a percentage.
            arrow (str): The direction of the arrow associated with the text box. One of 'left', 'right', 'up', 'down', or None.
        """
        self.text = text
        self.x = x
        self.y = y
        self.width = width
        self.arrow = arrow
        
    def to_xml(self):
        """
        Create a <text> OrderedXMLElement from this TextBox.
        """
        elem = OrderedXMLElement('text')
        elem.children = [
            OrderedXMLElement('x', self.x),
            OrderedXMLElement('y', self.y)
        ]
        
        if self.width is not None:
            elem.children.append(OrderedXMLElement('width', self.width.strip()))
        
        if self.arrow is not None:
            elem.children.append(OrderedXMLElement('arrow', self.arrow.strip().lower()))
            
        elem.children.append(OrderedXMLElement('content', self.text))
        
        return elem
        
    @classmethod
    def from_xml(cls, elem):
        """
        Create a TextBox instance from a <text> OrderedXMLElement.
        
        Args:
            elem (OrderedXMLElement): the OrderedXMLElement to create the instance from.
        """
        ret = cls(elem.find('content').text, elem.find('x').text, elem.find('y').text)
        
        if elem.find('width') is not None:
            ret.width = elem.find('width').text
        else:
            ret.width = '20%'
            
        if elem.find('arrow') is not None:
            ret.arrow = elem.find('arrow').text.strip().lower()
        else:
            ret.arrow = None
            
        return ret


class Screen(object):
    def __init__(self, image):
        """
        A screen within an epilogue.
        
        Attributes:
            image (str): The image filename to use for this screen's background.
            boxes (list): A list of TextBoxes displayed within this screen.
        """
        
        self.image = image
        self.boxes = []
        
    def __len__(self):
        return len(self.boxes)
        
    def __iter__(self):
        return self.boxes.__iter__()
    
    def to_xml(self):
        """
        Create a <screen> OrderedXMLElement from this Screen.
        """
        elem = OrderedXMLElement('screen')
        elem.attributes['img'] = self.image
        
        for box in self.boxes:
            elem.children.append(box.to_xml())
        
        return elem
            
    @classmethod
    def from_xml(cls, elem):
        """
        Create a Screen from a <screen> OrderedXMLElement.
        """
        ret = cls(elem.get('img'))
        
        for box_elem in elem.iter('text'):
            ret.boxes.append(TextBox.from_xml(box_elem))
        
        return ret
        
        
class Epilogue(object):
    def __init__(self, title):
        """
        A character epilogue.
        
        Attributes:
            title (str): This epilogue's title.
            screens (list): A list of Screen objects to display within this Epilogue.
            conditions (dict): A dict containing all condition attributes for this Epilogue.
        """
        
        self.title = title
        self.screens = []
        self.conditions = OrderedDict()
        
    def __len__(self):
        return len(self.screens)
        
    def __iter__(self):
        return self.screens.__iter__()
        
    def to_xml(self):
        """
        Create an <epilogue> OrderedXMLElement from this Epilogue.
        """
        elem = OrderedXMLElement('epilogue', None, self.conditions)
        elem.children.append(OrderedXMLElement('title', self.title))
        
        for screen in self.screens:
            elem.children.append(screen.to_xml())
            
        return elem
        
    @classmethod
    def from_xml(cls, elem):
        """
        Create an Epilogue from an <epilogue> OrderedXMLElement.
        """
        ret = cls(elem.find('title').text)
        ret.conditions = OrderedDict(elem.attributes)
        
        for screen_elem in elem.iter('screen'):
            ret.screens.append(Screen.from_xml(screen_elem))
            
        return ret
