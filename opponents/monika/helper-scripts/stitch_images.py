from PIL import Image
import sys

images = []

total_width = 0
max_height = 0

for filename in sys.argv[1:-1]:
    img = Image.open(filename)
    
    images.append(img)
    max_height = max(max_height, img.height)
    total_width += img.width
    
out_img = Image.new('RGBA', (total_width, max_height), '#fff')

current_x = 0
for img in images:
    out_img.alpha_composite(img, (current_x, 0))
    current_x += img.width
    
out_img.save(sys.argv[-1])
