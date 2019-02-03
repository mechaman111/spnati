from pathlib import Path
from PIL import Image

X_WIDTH = 600
Y_MARGIN_SPACE = 45

def do_generate(base_path, overlay):
    stage, emotion = base_path.stem.split('-', 1)
    with Image.open(base_path) as base_img:
        l, t, r, b = base_img.getbbox()
        
        center_x = int((r+l)/2)
        crop_l = int(center_x - (X_WIDTH // 2))
        crop_r = int(center_x + (X_WIDTH // 2))
        
        head_crop = base_img.crop((crop_l, t-Y_MARGIN_SPACE, crop_r, t+350-Y_MARGIN_SPACE))
        head_crop.save('./5-{}.png'.format(emotion))
        
        #out_img = Image.new('RGBA', overlay.size)
        
        #out_img.paste(head_crop, (17, 12))
        #out_img.alpha_composite(overlay)
        
        #out_img.save('./5-{}.png'.format(emotion))

if __name__ == '__main__':
    with Image.open('./AE-overlay.png') as overlay:
        for path in filter(lambda p: p.suffix.lower() == '.png', Path('./').iterdir()):
            stage, emotion = path.stem.split('-', 1)
            
            try:
                stage = int(stage)
            except ValueError:
                continue

            if stage == 0:
                do_generate(path, overlay)
                print("Generated: 5-{}.png".format(emotion))
