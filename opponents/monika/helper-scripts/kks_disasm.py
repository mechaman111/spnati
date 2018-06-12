def get_component_type(data):
    if data[1].isalpha():
        return data[0:2], data[0:2] # code is 2 letters
    else:
        return data[0], data[0:3]  # code is 1 letter + 2 digits

class KisekaeComponent(object):
    def __init__(self, data):
        self.id, self.prefix = get_component_type(data)
        self.raw_data = data[len(self.prefix):]
        self.pieces = self.raw_data.split('.')
        
    def __str__(self):
        return self.prefix + '.'.join(self.pieces)
        
class KisekaeCharacter(object):
    def __init__(self, code):
        version, data = code.split('**')
        
        if data[0] == '*':
            raise ValueError("Improperly-formatted Kisekae code! Did you export ALL items by accident?")
            
        self.version = int(version)
        self.components = {}
        
        for i, subcode in enumerate(data.split('_')):
            comp = KisekaeComponent(subcode)
            
            if comp.id not in self.components:
                self.components[comp.id] = []
                
            self.components[comp.id].append(comp)
            
    def get_pose_subcomponents(self):
        pass
                        
if __name__ == '__main__':
    import sys
    
    char = KisekaeCharacter(sys.argv[1])
    
    for key, comp_list in char.components.items():
        for comp in comp_list:
            print("{}: {}".format(key, comp))
