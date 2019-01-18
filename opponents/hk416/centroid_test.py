import numpy as np
from PIL import Image
from scipy import ndimage
import sys

if __name__ == "__main__":
    with Image.open(sys.argv[1]) as img:
        arr = np.array(img)
        intensity = np.sum(arr, axis=2)
        nonzero = np.greater(intensity, 0).T
        
        centroid = ndimage.measurements.center_of_mass(nonzero)
        
        print("Image CoM: "+str(centroid))
        
        center_x = int(img.width / 2)
        com_x = centroid[0]
        
        shift_x = int(com_x-center_x)
        print("Offset: {}".format(shift_x))
        
        new_sz = (img.width-shift_x, img.height)
        im2 = img.crop((0, 0, new_sz[0], new_sz[1]))
        im2 = im2.transform(img.size, Image.AFFINE, (1, 0, shift_x, 0, 1, 0))
        
        im2.save(sys.argv[2])
        
        