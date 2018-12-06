import sys

def hexcode2rgb(code):
    code = code.lower()
    
    return [int(code[0:2], 16), int(code[2:4], 16), int(code[4:6], 16)]

def rgb2hexcode(rgb):
    return '{:02X}{:02X}{:02X}'.format(*rgb)

def rgb2hsv(rgb):
    r = rgb[0] / 255.0
    g = rgb[1] / 255.0
    b = rgb[2] / 255.0
    
    rgb_max = max(r,g,b)
    rgb_min = min(r,g,b)
    
    h = 0
    s = 0
    v = rgb_max
    
    if rgb_max > 0:
        s = (rgb_max - rgb_min) / rgb_max
    
    if rgb_max == rgb_min:
        h = 0
    elif rgb_max == r:
        h = (g - b) / (rgb_max - rgb_min)
    elif rgb_max == g:
        h = 2 + ((b - r) / (rgb_max - rgb_min))
    else:
        h = 4 + ((r - g) / (rgb_max - rgb_min))
    
    h *= 60.0
    
    if h < 0:
        h += 360.0
        
    return [h, s, v]
    
def hsv2rgb(hsv):
    c = hsv[2] * hsv[1]
    hprime = hsv[0] / 60.0
    
    rgb = [0,0,0]
    
    x = c * (1 - abs(((int(hprime) % 2) - 1)))
    
    if hprime >= 0 and hprime <= 1:
        rgb = [c,x,0]
    elif hprime > 1 and hprime <= 2:
        rgb = [x,c,0]
    elif hprime > 2 and hprime <= 3:
        rgb = [0,c,x]
    elif hprime > 3 and hprime <= 4:
        rgb = [0,x,c]
    elif hprime > 4 and hprime <= 5:
        rgb = [x,0,c]
    elif hprime > 5 and hprime <= 6:
        rgb = [c,0,x]
    
    m = hsv[2] - c
    
    rgb[0] = int((rgb[0] + m) * 255)
    rgb[1] = int((rgb[1] + m) * 255)
    rgb[2] = int((rgb[2] + m) * 255)
        
    return rgb
        
if __name__ == "__main__":
    rgb_avg = [0, 0, 0]
    hsv_avg = [0, 0, 0]
    
    for hexcode in sys.argv[1:]:
        rgb = hexcode2rgb(hexcode)
        hsv = rgb2hsv(rgb)
        
        print("{:s}: rgb({},{},{}) hsv({:.2f}, {:.2f}, {:.2f})".format(hexcode, *rgb, *hsv))
        
        rgb_avg[0] += rgb[0]
        rgb_avg[1] += rgb[1]
        rgb_avg[2] += rgb[2]
        
        hsv_avg[0] += hsv[0]
        hsv_avg[1] += hsv[1]
        hsv_avg[2] += hsv[2]
    
    rgb_avg[0] = int(rgb_avg[0] / (len(sys.argv)-1))
    rgb_avg[1] = int(rgb_avg[1] / (len(sys.argv)-1))
    rgb_avg[2] = int(rgb_avg[2] / (len(sys.argv)-1))
    
    hsv_avg[0] = hsv_avg[0] / (len(sys.argv)-1)
    hsv_avg[1] = hsv_avg[1] / (len(sys.argv)-1)
    hsv_avg[2] = hsv_avg[2] / (len(sys.argv)-1)
    
    print("Averages:")
    print("RGB - {:s}: rgb({},{},{}) hsv({:.2f}, {:.2f}, {:.2f})".format(
        rgb2hexcode(rgb_avg), *rgb_avg, *rgb2hsv(rgb_avg)
    ))
    
    hsv_avg_rgb = hsv2rgb(hsv_avg)
    
    print("HSV - {:s}: rgb({},{},{}) hsv({:.2f}, {:.2f}, {:.2f})".format(
        rgb2hexcode(hsv_avg_rgb), *hsv_avg_rgb, *hsv_avg
    ))