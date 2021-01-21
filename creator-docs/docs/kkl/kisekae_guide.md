# Intro to Kisekae

A beginner's guide to SPNatI's character art creation program

---

Kisekae (or KKL) is the program used by every developer to create their characters' images in SPNatI. While some developers augment their sprites with external editors such as Photoshop or Paint.NET, they still use Kisekae as a base. And some of our best art was made *entirely* within Kisekae! It's a powerful tool, but one which requires practice to master.

A *complete* user guide to Kisekae is unfortunately too large an undertaking to attempt. The best method to learning the ins-and-outs of the program is to mess around with it yourself. This guide will instead lay out only the basics, in order to send you down the right path. Only the technical aspects of Kisekae will be covered here, not the artistic— this is *how* to do things, not *what* to do.

**Note:** This guide uses screenshots from KKL version `104.1`. Later versions may look slightly different.

---

## Tabs and Controls

Going from Row One to Row Four, from left to right.

### Sets

#### Default Sets

![KKL 01](../img/01_set01.png)

Choose from a set of default **K-On!** or **Love Live!** girls. These aren't particularly useful for our purposes.

---

#### Preset Characters

![KKL 02](../img/02_set02.png)

The default girls as individual characters. Not useful for SPNatI. Our versions of Maki and Shimakaze still needed adjustments beyond the defaults.

---

#### Preset Outfits

![KKL 03](../img/03_set03.png)

A variety of available outfits for default characters. For SPNatI it's better to more deliberate with costume choices, so this too is a tab you likely won't ever use.

---

#### Preset Attributes

![KKL 04](../img/04_set04.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Character Slot Selector|Pose Sets|Skin Color Sets|Height Adjuster|Preset Hairstyles (and hair color picker)|
|Preset Facial Features (and eye color picker)|Preset Outfits|Preset Underwear|Preset BGs|

Most of this isn't worth using, but you'll need to use the **Character Slot Selector** pretty often to hide, show, and select different characters. The button with the eye toggles a character slot on or off. When off, the eye will turn into a red slash. This on/off toggle will be available for almost every other item we encounter in this guide.

A new artist could get posing ideas from the **Pose Sets** selector as well, since those show off techniques like body and head rotation.

---

### Posing

#### Arm Movement

![KKL 05](../img/05_posing01.png)

|-|-|-|-|
|-|-|-|-|
|Right Shoulder Movement|Right Elbow Movement (with Layer Button)|Right Hand Selector|Right Wrist Movement|
|Left Shoulder Movement|Left Elbow Movement (with Layer Button)|Left Hand Selector|Left Wrist Movement|
|Right Hand Held Item Selector (plus Color Picker)|Left Hand Held Item Selector|

This tab is used to pose the arms of your character when it comes time to make their sprites. Use the grey link toggle button on the right hand side of the first row of this tab to de-link the left and right arms when posing.

The **Color Picker** chooses color of held objects. Most objects in Kisekae allow you to pick a color. The **Layer Button** is the up arrow, in this case next to the **Right Elbow Movement** slider, which adjusts the arm behind or in front of the body. This is also a fairly common button, and different items layer over different things.

---

#### Leg and Body Movement

![KKL 06](../img/06_posing02.png)

|-|-|-|-|-|-|
|-|-|-|-|-|-|
|Right Leg Position Selector (with Flip Button and link toggle)|Left Leg Position Selector|Head Rotation|Body Rotation|
|Swaps Characters in Slots|Horizontal Movement|Depth|Vertical Movement (with shadow)|Vertical Movement (shadow adjusts for height)|Shadow Visibility Toggle|

This is where you do the bulk of your posing. For the CE, it's usually best to keep your character at `500` on the **Depth** (forward and back movement) slider. For some sitting poses, you may need to adjust them back. Please don't ask why legs are a series of set positions and not a slider.

The **Flip Button**, the sideways facing arrow next to the **Right Leg Position Selector**, swaps right and left leg positions, while the link toggle button unlinks left and right legs. Both these buttons are common in other tabs as well.

---

#### Blurs, Shadows, and Silhouettes

![KKL 07](../img/07_posing03.png)

|-|-|-|-|-|-|
|-|-|-|-|-|-|
|Overlay Selector|Overlay Opacity|Character Opacity|
|Drop Shadow (with Color Picker)|Invert Drop Shadow|Shadow Mask|Shadow Only|Shadow Opacity|Shadow Size|
|Shadow Blur Horizontal|Shadow Blur Vertical|Shadow Direction|Shadow Distance|
|Blur Toggle and Type Selector|Blur Level Horizontal|Blur Level Vertical|Emote Bubble Blur|

This is an advanced tab not often used in posing. It adds effects over your character sprite. Poses using partial transparency or a blur effect likely use this tool.

---

### Proportions

#### Width and Height

![KKL 08](../img/08_proportions01.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Character Size|Body Height|Leg Length|
|Lower Leg and Foot Thickness|Arm Thickness|Chest Width|Shoulder Width|Hip and Thigh Width|
|Belly Button Selector|Waist Selector|Body Detail Opacity|Torso Height|

One of the most important tabs in Kisekae. Because the proportions for the default characters are awful, you'll want to adjust them when making your own models. The **Character Size** slider adjusts the overall model size, while the **Body Height** slider adjusts everything besides the head. It is recommended to unlink the **Body Height** and **Leg Length** sliders for finer control. 

Unfortunately, there is no way to uncouple hip width and thigh width.

---

#### Skin Color and Head Proportions

![KKL 09](../img/09_proportions02.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Skin Color|Tanlines (plus toggle and Color Picker)|Blush Stickers|Head Size|Neck Length|
|Chin + Cheeks Shape|Chin + Cheeks Width|Chin + Cheeks Length|
|Breast Size|Nipple Selector (plus toggle and Color Picker)|Nipple Size|Nipple Position  Horizontal|Nipple Position Vertical|

Set the **Blush Sticker** selector to `10` for no default blush (the recommended choice). For **Breast Size**, use size `1` or `2` for a male character. **Tanlines** aren't necessary if your character wouldn't have them.

I apologize on behalf of Pochi for the available chin shapes.

---

#### The Saucy Bits

![KKL 10](../img/10_proportions03.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Penis Selector (plus toggle and Color Picker, use the up arrow to adjust layering)|Erection Size Increase|Penis Head Selector|Penis Size|
|Erection Controls|Erection Toggle|Penis Rotation|Testicles|Testicle Size|
|Vagina Selector (plus toggle and Color Picker)|Pubic Hair (plus toggle and Color Picker)|Pubic Hair Opacity|

If you give your model a penis, be sure to set the **Erection Controls** to **Manual**. Use the **Erection Toggle** to give your model an erection. The **Erection Size Increase** slider affects the size of the erection *relative to* the size of the penis, so no change will be visible if the model is flaccid.

With the model selected, click and hold on the labia to spread them. Click and hold again to close. Clicking repeatedly on the vagina will create **Love Juice**, hold down **Spacebar** and click on the juice to remove it.

Make sure to turn off the vagina if making a male character.

---

### Features

#### Hair

![KKL 11](../img/11_features01.png)

|-|-|-|-|
|-|-|-|-|
|Hair Color (All)|Hairstyle (with Flip Button)|Bangs (with Flip Button and Layer Button)|Bang Length|
|Back Hair (with gravity toggle)|Back Hair Length|Back Hair Width|Back Hair Position|
|Right Side Lock (plus Layer Button and Gravity Toggle)|Right Side Lock Length|Right Side Lock Position|(button to toggle link with Left Side Lock)|
|Left Side Lock (plus Layer Button and Gravity Toggle)| Left Side Lock Length| Left Side Lock Position|

Changing the **Hair Color (All)** selector will adjust the color for *all* hair *and* hairpieces, so be careful. You can make gradient hair by selecting two different colors with the first two spots in the **Color Picker**. The third slot is for the outline color.

The rightmost button on the **Back Hair** and **Side Locks** selectors is the **Gravity Toggle**. This either looks like a circular arrow or a dash, depending on whether it is **active** or **inactive** respectively. When active, it makes the affected hair obey gravity when the model's head is tilted (though not when the entire body is).

The **Layer Button**, the button with the up arrows, adjusts the layering of the **Bangs** to be above or below the sides of the **Hairstyle**. For the **Side Locks**, this is either behind the back, behind the ear, or in front of the ear. If you've been following this guide in order, you're already familiar with this button (penises also have it). 

---

#### Hair Pieces

![KKL 12](../img/12_features02.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Hair Piece Sets|Hair Piece Slots|Hair Piece Selector (plus Color Picker, Mirroring Button, Layer Button, Gravity Toggle, and Shading Toggle)|
|Hair Piece Width (with height Link Toggle)|Hair Piece Height (with outline Link Toggle)|Hair Piece Outline Thickness|
|Hair Piece Skew|Hair Piece Rotation|Hair Piece Skewed Rotation|Hair Piece Horizontal Position|Hair Piece Vertical Position|

For the four buttons on the Hair Piece Selector: the **Mirroring Button**, which looks like two triangles pointed in opposite directions, allows you to choose the right-hand version of a hairpiece, the left-hand version, or both. The **Layer Button** and **Gravity Toggle** were explained in previous sections. Finally the **Shading Toggle** allows you to turn off the shadows on any hairpieces which have them.

The slider for **Skewed Rotation** affects a hairpiece after it has been skewed based on how much it has been skewed. A Hairpiece at the default skew of `500` won't see any difference compared to the normal **Rotation** slider.

Note: It is recommended to not use the first five **Hair Piece Slots**, as use of those can cause lagging in Kisekae.

!!! warning
	Pressing the **Hair Piece Sets** button (AKA the Set button) will replace *all* your hairpieces with a pre-set group of hairpieces. **Be very careful around this tab** as to not undo your hard work. Thankfully, KKL keeps backups in case of this button being accidentally pressed.

---

#### Eyes

![KKL 13](../img/13_features03.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Eye Shape (plus outline Color Picker and Layer Button)|Eye Position Horizontal|Eye Position Vertical|Eye Width|Eye Height|
|Eye Rotation|Eyelids|Eyelashes|Under-Eye Lines|
|Right Iris and Pupil Style (with link toggle)|Left Iris and Pupil Style|Iris Width|Iris Height|
|Iris Position Horizontal |Iris Position Vertical |Eye Shine (with Flip Button)|Eye Shine Rotation|

The **Layer Button** next to the **Eye Shape** selector layers eyes above or below the bangs. The **Eyelids** selector are just lines above the eye, but below the eyebrow.

The **Flip Button** on the **Eye Shine** selector (the one that looks like an arrow pointed either left or right) flips it horizontally. This is the same button on the hair and bangs selectors above.

When adjusting a characters eyes during posing, we recommend to not use the **Iris Position** sliders for that. Instead, use the tools in the **Facial Posing Tab** (covered later in this guide).

---

#### Facial Features

![KKL 14](../img/14_features04.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Eyebrows (with Color Picker and Layer Button)|Eyebrow Rotation|Eyebrow Position Vertical|
|Mouth Size|Mouth Height|Facial Markings (can be toggled individually)|
|Nose|Nose Width (with link toggle)|Nose Height|Nose Position Vertical|
|Nose Shadow|Nose Shadow Opacity|Nose Shadow Width (with link toggle)|Nose Shadow Height|Nose Shadow Position Vertical|
|Ears|Ear Width|Ear Rotation|Ear Position Horizontal|Ear Position Vertical|

Like with the eyes and irises before, it is better to adjust **Eyebrows** and **Mouth Size** using the **Facial Posing Tab** when it comes time for facial posing, not this tab. Instead, use these controls to get a good "neutral" benchmark to work off of.

Not all **Ears** allow you to pick colors. Those that don't will just be the model's skin color.

---

#### Face Marks

![KKL 15](../img/15_features05.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Face Marks Slots|Face Mark Selector (plus Color Picker, Mirroring Button, and Layer Button)|Face Mark Opacity|Face Mark Width (with link toggle)|
|Face Mark Height|Face Mark Skew|Face Mark Rotation|Face Mark Position Horizontal|Face Mark Position Vertical|

**Face Marks** can layer above or below the eyes. These can be used to create custom facial shading.

---

#### Ears, Horns, and Tails

![KKL 16](../img/16_features06.png)

|-|-|-|
|-|-|-|
|Animal Ears (with Color Picker and Clothing Toggle)|Tails (with Color Picker and Flip Button)|Tail Size|
|Wings (with Flip Button)|Wing Size|Wing Vertical Position|
|Horns (with Layer Button)|Horn Size|Horn Position| 

The **Clothing Toggle** on the **Animal Ears** control is the button that looks like hair or a hat, depending on what it's set to. As hair, the ears are treated as part of the body, but as a hat they're treated as clothing, and can be clicked on to remove. That seems to be the only difference.

Click on a **Wing** to adjust its position. **Horn Positions** adjust the horn either vertically or around the head, depending on the horn.

Note: If you don't see the exact ear or tail you like, it's probably best to make your own with **Ribbons** than settle for one here that's not quite as good.

---

### Expressions

#### Emotions & Emotes

![KKL 17](../img/17_expressions01.png)

|-|-|-|-|-|-|
|-|-|-|-|-|-|
|Auto vs. Manual Switch|Personality Type (Auto Only)|Ahegao Toggle (Auto Only)|Horniness Slider (Auto Only)|Crying Slider (Auto Only)|Heavy Breaths Slider (Auto Only)|
|Emote Eyes (with sclera Color Picker and Flip Button)|Emote Eye Size|Emote Eye Position Horizontal|Emote Eye Position Vertical|
|Blush|Unblush|Eye effects (can be toggled individually)|Emotion Effects (with Flip Button, can be toggled individually)|Emote Bubble|

!!! warning
	KKL is configured to use **Manual** Expressions by default. This allows you to carefully create the posing you want in your sprites. Switching to **Auto** mode will mess up your chosen emotions and make it difficult for you to reset back.

These are the "cartoony" emotion effects, which may or may not be useful depending on the character and pose. None of us could come up with a better name than "Emote Eyes".

---

#### Facial Posing

![KKL 18](../img/18_expressions02.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Right Eye Openness|Left Eye Openness|Right Iris Size|Left Iris Size|
|Right Iris Position Horizontal (with Mirroring Toggle and link toggle)|Left Iris Position Horizontal|Right Iris Position Vertical (with link toggle)|Left Iris Position Vertical|
|Right Eyebrow Mode|Right Eyebrow Rotation|Right Eyebrow Position Vertical (and link toggle selector)|
|Left Eyebrow Mode|Left Eyebrow Rotation|Left Eyebrow Position Vertical|Mouth (and Flip Button)|Mouth Width (with link toggle)|
|Mouth Height|Mouth Outline Width|Mouth Rotation|Mouth Position Horizontal|Mouth Position Vertical|

Use the **Eye Openness** slider to make a character close their eyes by dragging it to the left end. Use the **Mirroring Toggle** to change how the **Iris Position Horizontal** movement works, either both in same direction or both in opposite directions.

Only a handful of mouths are actually any good. Past the fifties be dragons.

---

#### Breast Movement

![KKL 19](../img/19_expressions03.png)

|-|-|-|-|-|-|-|-|-|-|
|-|-|-|-|-|-|-|-|-|-|
|Right Breast Bounce (Auto Only)|Left Breast Bounce (Auto Only)|Right Breast Side Bounce (Auto Only)|Left Breast Side Bounce (Auto Only)|Right Nipple Tweak Vertical (Auto Only)|Left Nipple Tweak Vertical (Auto Only)|Right Nipple Tweak Horizontal (Auto Only)|Left Nipple Tweak Horizontal (Auto Only)|Auto Vaginal Touch|Auto Lip Touch|
|Auto vs. Manual Switch|Right Breast Positions|Left Breast Positions|Right Nipple Positions|Left Nipple Positions|

Unlike the **Emotions & Emotes** tab, a character can work perfectly well with Breast Movement set to **Auto**. Set to **Manual** only if you need finer control.

The **Auto Vaginal Touch** switch causes the model to gain **Love Juice** quickly, similar to clicking the vagina directly. The **Auto Lip Touch** gives them a kissy face and only works with characters that have their emotions set to Auto in the **Emotions & Emotes** tab above.

---

### Shirts

#### Jackets and Sweaters

![KKL 20](../img/20_shirts01.png)

|-|-|-|-|
|-|-|-|-|
|Jackets|Jacket Right Sleeves (with link toggle)|Jacket Left Sleeves|
|Jacket Breast Pockets (plus Color Picker and Mirroring Button)|Jacket Side Pockets (With Color Picker and Position Number)|
|Sweaters|Sweater Right Sleeves (with link toggle)|Sweater Left Sleeves|
|Sweater Top Style|Sweater Hem Style|Sweater Breast Pockets (plus Color Picker and Mirroring Button)|Sweater Side Pockets (With Color Picker and Position Number)|

For the **Breast Pocket** selectors, `001` is "no pocket (or decoration)". Same with the **Side Pocket** selector. The **Position Number** on the side pockets serves the same function as a selector, the number refers to one of 15 different positions for the pockets.

Only some **Jacket** and **Sweater Sleeves** have options in the Color Picker, otherwise they default to the Jacket or Sweater color respectively.

---

#### Collared and T-Shirts

![KKL 21](../img/21_shirts02.png)

|-|-|-|-|
|-|-|-|-|
|Collared Shirts|Collars (with Collar Style button)|Collared Shirt Right Sleeves (with link toggle)|Collared Shirt Left Sleeves|
|Collared Shirt Top Style|Collared Shirt Hem Style|Collared Shirt Breast Pockets (plus Color Picker and Mirroring Button)|Collared Shirt Side Pockets (With Color Picker and Position Number)|
|T-Shirts|T-Shirt Right Sleeves (with link toggle)|T-Shirt Left Sleeves|
|T-Shirt Top Style|T-Shirt Hem Style|T-Shirt Breast Pockets (plus Color Picker and Mirroring Button)|T-Shirt Side Pockets (With Color Picker and Position Number)|

The **Collar Style** button allows you to choose one of six different color configurations for the collar. 

The Side Pockets and Breast Pockets function the same as the ones in the **Jackets and Sweaters** tab above. Same with sleeves.

---

### Leggings

#### Skirts, Pants, and Tights

![KKL 22](../img/22_leggings01.png)

|-|-|-|
|-|-|-|
|Skirts (plus Color Picker and Layer Button)|Tights and Pantyhose (plus Color Picker and Layer Button)|
|Pants|Right Pant Cuff Style (and link toggle)|Left Pant Cuff Style|

Fairly self-explanatory. The **Pants** controls can be used to mix and match styles.

---

#### Socks and Anklets

![KKL 23](../img/23_leggings02.png)

|-|-|
|-|-|
|Right Sock (and link toggle)|Left Sock|
|Right Anklet (and link toggle)|Left Anklet|

You can use the **Socks** selector to make thigh-highs too.

---

#### Shoes and Boots

![KKL 24](../img/24_leggings03.png)

|-|-|
|-|-|
|Right Shoe|Right Boot Cuff|
|Left Shoe|Left Boot Cuff|

Only some **Shoe** styles have **Boot Cuffs** available.

---

### Undies and Other Lewd Things

#### Underwear and Swimsuits, Shibari, Pasties, and Piercings

![KKL 25](../img/25_undies01.png)

|-|-|-|-|
|-|-|-|-|
|Bra (with Top Toggle and link toggle)|Underwear (with Layer Button)|
|Shibari|Maebari|Maebari Size|
|Right Breast Pasties and Piercings|Right Breast Pasties and Piercings Size (with link toggle)|Left Breast Pasties and Piercings|Left Breast Pasties and Piercings Size|

The **Top Toggle** button next to the **Bra** color picker is only available for some bras. It allows you to have only the top portion of the item, only the bottom, or both. The Layer Button on the **Underwear** controls adjusts whether they appear above or below a one-piece top (e.g. the swimsuits).

**Shibari** is bondage rope, while **Maebari** are similar to pasties, but for the crotch.

---

#### Dildos

![KKL 26](../img/26_undies02.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Dildo|Dildo Size|Toggle to Allow Insertion Through Clothing|Animation Type (With Loop Toggle and Character Animation Toggle)|Animation Speed (with Loop Toggle)|

This tab might not ever be used, depending on whether or not your character uses a toy during their masturbation sequence.

The **Loop Toggle** autoplays all stages of animation speed and type in a continuous loop. `01` for **Animation Speed** is perfectly still (in case you need to take a picture of one specific animation frame). The **Character Animation Toggle** determines whether or not the character moves with the Dildo.

---

### Headwear

#### Hats, Glasses, Masks, etc.

![KKL 27](../img/27_headwear01.png)

|-|-|-|
|-|-|-|
|Hats (with Flip Button, Bangs Toggle, Back Hair Toggle, Side Locks Toggle, Hair Piece Toggle, and Ribbon Toggle)|Headband (and Layer Button)|
|Glasses (with Flip Toggle and Layer Button)|Glasses Vertical Position|Masks|
|Headphones and Earmuffs|Right Earring (with link toggle)|Left Earring|
|Collars and Scarves (with Layer Button)|Necklaces (with Layer Button)|Neckties|

The various toggles next in the **Hat** controls (Bangs Toggle, Back Hair Toggle, Side Locks Toggle, Hair Piece Toggle, and Ribbon Toggle) are just quick ways to toggle whether or not those pieces show up when a hat is worn.

You can click **Headphones** and some **Neckties** to adjust their position.

---

#### Ribbons

![KKL 28](../img/28_headwear02.png)

|-|-|-|-|
|-|-|-|-|
|Ribbon Slots|Ribbon Placement Location|Ribbon Selector (plus Color Picker, Mirroring Button, Layer Button, and Shading Toggle)|
|Ribbon Width (and link toggle)|Ribbon Height (and link toggle)|Ribbon Outline Width|
|Ribbon Skew|Ribbon Rotation|Ribbon Position Horizontal|Ribbon Position Vertical|

Possibly the most important Tab in Kisekae. Use the **Ribbon Placement Location** to pin ribbons to the torso or abdomen, and you can build almost any item of clothing. The options to pin ribbons are: Head, Front Hair, Torso, Abdomen, Back Hair, Side Locks, or the first five **Hairpiece** slots. Ribbons attached to part of the hair with **Gravity** toggled on will move with the hair.

Much of these controls are similar to **Hairpieces** above, and better explained there.

Holding down **shift** when using the controls will adjust all ribbons at once.

---

### Belts and Bracelets

#### Belts

![KKL 29](../img/29_beltsbrace01.png)

|-|-|-|-|
|-|-|-|-|
|Belt Slots|Belt Selector (plus Color Picker, Mirroring Button, Layer Button, and Mask Toggle)|Belt Position|
|Belt Width (and link toggle)|Belt Height (and link toggle)|Belt Outline Width|
|Belt Skew|Belt Rotation|Belt Position Horizontal|Belt Position Vertical|

Another critically important tab, though a first timer might not realize it. You can build a wide variety of clothing items out of belts.

The **Mask Toggle** affects whether a belt will only be as wide at the outline of a character, or whether the whole belt is visible at once. The **Belt Position** control allows you to choose what configuration you want the the belt in, the amount varies by bely. The curved rope belt configuration of Belt `08` allows you to make curved outlines.

Like ribbons, holding down **shift** when using the controls will adjust all belts.

---

#### Bracelets and Sleeves

![KKL 30](../img/30_beltsbrace02.png)

|-|-|
|-|-|
|Right Bracelet (with link toggle)|Left Bracelet|
|Right Upper Arm Cuff (with link toggle)|Left Upper Arm Cuff|
|Right Forearm Band (with link toggle)|Left Forearm Band|

The **Bracelet** selector also allows you to choose detached sleeves as well, perfect for making Vocaloids and armpit shrine maidens. 

---

#### Armbands, Elbow Bads, and Gloves

![KKL 31](../img/31_beltsbrace03.png)

|-|-|-|-|
|-|-|-|-|
|Right Armband (with link toggle)|Left Armband|
|Right Elbow Pad (with link toggle)|Left Elbow Pad|
|Right Glove|Right Glove Length (with link toggle)|Left Glove|Left Glove Length|

No special clarifications needed.

---

### Image Attachments

#### Character Image Attachments

![KKL 32](../img/32_attachments.png)

|-|-|-|-|-|-|-|
|-|-|-|-|-|-|-|
|URL vs. Local Toggle|Load Button|Image Path Input Field|Image URL List|Pochi DLC Images|
|Attachment Slots|Attachment Placement Location|Paint Bucket (Pochi DLC Images only)|Flip Button and Layer Button|Attachment Opacity|Attachment Width (with link toggle)|Attachment Height|
|Attachment Skew|Attachment Rotation|Attachment Position Horizontal|Attachment Position Vertical|

Image attachments allow you to take a `.png`, `.svg`, or `.swf` image from outside of Kisekae, and import as part of your model. This gives you much more freedom when making art for your character. You can even make art in Kisekae, export as a `.png` file, and re-import to save ribbon or hair piece slots. Because ribbons cannot be attached legs, re-importing as an image attachment means you need to only deal with one piece instead of many.

The **URL vs. Local Toggle** determines whether you will use an image from online or your local hard drive. The **Load Button** either loads an image from online, or allows you to navigate to the location of an image on your HDD. When loading an image from your drive, the **Image Path Input Field** will be filled automatically. You can use the **Attachment Placement Location** to pin attachments to the body, head, left and right upper arms, and left and right forearms.

Image attachments can only be layered completely in front of or completely behind the body part they're pinned to. This means that body attachments will layer in front of everything, including arms.

!!! note
	You can auto-load image attachments onto a saved model by putting them in an `images` folder in the same directory as `kkl.exe` (`.svg` and `.swf` files can *only* be loaded in this method). Make sure your image file names do not use spaces or other illegal characters.

---

### Global Items

These are items which do not anchor to an individual **Character**, but exist as part of the **Scene**. These can only be saved as part of an **All** code, not a **Select** code.

#### Global Arms

![KKL 33](../img/33_globalarms.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Global Arm Slot|Global Arm Style (with Color Picker and Flip Button|Global Arm Opacity|Global Arm Size|Global Arm Thickness|
|Global Arm Rotation|Global Arm Position Horizontal|Global Arm Position Vertical|Global Arm Depth|
|Hand Selector|Wrist Rotation|Held Item Selector|
|Sleeves|Bracelets|

Note that not every option for normal arms are available for global arms. These can be useful if you need an arm to wrap around another character or go over an image attachment, and can't use an outside image editor.

#### Global Ribbons

![KKL 34](../img/34_globalribbons.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Global Ribbon Slots|Global Ribbon Selector (plus Color Picker, Flip Button, and Shading Toggle)|
|Global Ribbon Width (and link toggle)|Global Ribbon Height (and link toggle)|Global Ribbon Outline Width|
|Global Ribbon Skew|Global Ribbon Rotation|Global Ribbon Position Horizontal|Global Ribbon Position Vertical|Global Ribbon Depth|

Mostly the same as regular **Ribbons**. If you want to build something to re-import as an attachment, you can use these and export an image without the model. Otherwise, you'll want to use the first kind of Ribbons.

#### Global Belts

![KKL 35](../img/35_globalbelts.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Global Belt Slots|Global Belt Selector (plus Color Picker and Flip Button)|Global Belt Position|
|Global Belt Width (and link toggle)|Global Belt Height (and link toggle)|Global Belt Outline Width|
|Global Belt Skew|Global Belt Rotation|Global Belt Position Horizontal|Global Belt Position Vertical|Global Belt Depth|

Basically everything above, just replace "ribbons" with "belts".

#### Props

![KKL 36](../img/36_props.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Prop Slots|Prop (with Color Picker and Flip Button|Prop Orientation|Prop Size (with link toggle)|
|Prop Line Width|Prop Rotation|Prop Position Horizontal|Prop Position Vertical|Prop Depth|

This may be useful to you and your character, or you might never touch this tab. Props are rarely needed for character sprites.

#### Flags

![KKL 37](../img/37_flags.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Flag Slots|Flag Selector|Flag Style|Flag Width (and link toggle)|Flag Height|
|Flad Skew|Flag Rotation|Flag Position Horizontal|Flag Postion Vertical| Flag Depth|

The most important tab in ahahahaha I'm just kidding. No one ever uses this tab.

#### Speech Bubbles

![KKL 38](../img/38_speechbubbles.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Speech Bubble Slot|Speech Bubble Selector (with Color Picker and Flip Button)|Speech Bubble Opacity|Speech Bubble Blend Mode|
|Speech Bubble Width (and link toggle)|Speech Bubble Height|Speech Bubble Outline Width|Speech Bubble Skew|Speech Bubble Rotation|
|Speech Bubble Position Horizontal|Speech Bubble Postion Vertical|Speech Bubble Depth|Speech Bubble Tail Selector (and Flip Button)|Tail Outline|
|Tail Width (and link toggle)|Tail Height|Tail Rotation|Tail Position Horizontal|Tail Position Vertical|

**Speech Bubbles** can be used for their intended purpose (being speech bubbles), but one can also use them for light effects or shading. Keep in mind however, like all Global assets Speech Bubbles don't move with the character.

Speech Bubbles aren't significantly used in SPNatI spritework.

#### Backgrounds

![KKL 39](../img/39_bg.png)

|-|-|-|
|-|-|-|
|Background|Horizon Point|Floor|
|Stage Cutout|Foreground Audience|

SPNatI Sprites are exported without a background. However, you might want to make a more neutral background for character creation, or create a background for an epilogue.

#### Text

![KKL 40](../img/40_text.png)

|-|-|-|-|-|-|
|-|-|-|-|-|-|
|Text Field|Font (and Color Picker)|Text Direction|Paragraph|Text Placement Location|Layer Button|
|Text Opacity|Font Size|Text Width (and link toggle)|Text Height|Text Field Size|
|Text Skew|Text Rotation|Text Position Horizontal|Text Position Vertical|Leading|Kerning|

Since the character's text is written in the CE, and their speech bubbles are part of the game, you'll rarely need to make text within Kisekae. You may find a need for it outside of SPNatI work, however.

#### Global Image Attachments

![KKL 41](../img/41_globalattachments.png)

|-|-|-|-|-|-|-|-|
|-|-|-|-|-|-|-|-|
|URL vs. Local Toggle|Load Button|Image Path Input Field|Image URL List|Pochi DLC Images|
|Attachment Slots|Attachment Placement Location|Paint Bucket (Pochi DLC Images only)|Flip Button and Layer Button|Attachment Opacity|Attachment Width (with link toggle)|Attachment Height|Auto-Scale to Full Size|
|Attachment Skew|Attachment Rotation|Attachment Position Horizontal|Attachment Position Vertical|

Like image attachments, but global.

You can attach global attachments to word balloons using the **Attachment Placement Location**.

### Settings

#### Settings

![KKL 42](../img/42_settings.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Hide/Show Tab Bar|Arrow Marker Position|Numerical Display|Volume|Stereo/Mono Output|
|Image Quality|Censorship|Easy/Expert Toggle|Shortcut Keys|Breakout|

!!! danger
	By default, KKL is set to `Expert Mode`. **Do not switch out of Expert Mode**. You will have far fewer options available for character customization. Additionally, **Easy Mode** changes the locations of a number of important controls, which this guide does not take into account. This includes the toggle for switching *back* to Expert Mode, which will be located in the **Image Export Tab** (with the magnifying glass icon).

**Hide/Show Tab Bar**: Toggles whether or not the rows at the bottom will be visible when all tabs are closed and the mouse is moved away. 

**Arrow Marker Position**: Toggles whether or not the yellow marker appears above or behind a selected character. [Also activated using the `V` key.]

**Numerical Display**: Toggles showing the exact number selected on all sliders. [Also activated using the `M` key.]

**Volume**: Not remotely useful for SPNatI work. I can't get it to function anyway.

**Stereo/Mono Output**: By default, set to stereo (checked).

**Image Quality**: The quality of the Kisekae screen.

** Censorship**: Setting `8` is "no censorship", which KKL is set to by default. Do not change this setting.

**Easy/Expert Toggle**: Eeh? Easy Mode? How lame! Only kids play in easy mode!! Kya ha ha ha ha ha ha!!

**Shortcut Keys**: Assign shortcut keys to various tabs, or change the default keyboard shortcuts.

**Breakout**: Play the game *Breakout*, except with clothes as the blocks. 

### Unused

#### Gif Maker

![KKL 43](../img/43_gifmaker.png)

|-|-|-|-|-|
|-|-|-|-|-|
|Preview|Save and Load (as .txt)|Internal Save|Output as Gif|Gif Animation Speed|
|Frame List|Add/Remove Frames|Frame Selector|

The Gif Maker is completely unused in SPNatI's workflow. All in-game animation is built in the Character Editor, using the Pose Maker.

### Main Tabs

#### Save and Load

![KKL 44](../img/44_saveload.png)

|-|-|-|-|
|-|-|-|-|
|Save and Load (as .txt)|Import and Export (as code)|Internal Save|Save and Load All Internal Saves|

!!! note
	**Save and Load (as .txt)** is the most secure way to save your data, as you can back up these files. **Internal Saves** stores data as cookies, which can get wiped due to glitches or user error.

**Save and Load (as .txt)**: The Save button saves the entirety of your Kisekae project as a .txt file wherever you choose on your hard drive. The Load button opens a saved .txt file and loads it in Kisekae.

**Import and Export**: Opens a window where you can copy and paste character and scene codes. These are the same codes generated by the Save and Load options. Unlike the above option, with Import and Export you can only import or export partial codes (e.g. copy another character's shirt onto your own). Choose **Select** to only export the selected character's code, or select **All** to export everything (including Global Objects).

!!! note
	The first three numbers in a Kisekae code denote the version it was saved in. We don't recommend trying to import newer codes into an older version of KKL.

	Additionally, codes from KKL will likely not work in online Kisekae, as KKL has been modified to have additional Hairpiece, Belt, and Ribbon slots.

**Internal Save**: Saves the code on your PC using cookies. Unlike .txt files, these cannot be backed up or shared with others and are lost if you clear your cookies. However, this method also saves characters that have been toggled off in the **Preset Attributes** tab.

**Save and Load All Internal Saves**: Bulk exports all the above Internal Saves as .txt files. Note that importing using **Load-All** will overwrite all internal saves.

#### Image Export

![KKL 45](../img/45_export.png)

|-|-|-|
|-|-|-|
|Zoom|Fullscreen Mode|Image Size|
|PNG (Transparency)|JPG (JPG Quality)|Cropping (X Coordinate, Y Coordinate, Width, Height) (Mouse Selection)

**Zoom** `7` is the default for SPNatI-sized models. Whenever exporting, always use **Image Size** `5/5` for best quality. Export as **PNG** with **Transparency** toggled on (made light grey instead of dark grey).

For **Cropping**, enable it by checking the crop tool. You can either manually enter a range using the **Coordinate** and **Width and Height** controls, or toggle on the **Mouse Selection** and choose an area by clicking and dragging.

The **Character Editor** automatically handles cropping and exporting images at SPNatI size and resolution. It's better in almost all instances to export your poses through there.

#### Emergency Exit

![KKL 46](../img/46_exit.png)

|-|
|-|
|Emergency Exit|

Hides the screen, in case your mom comes in the room.

---

## Important Tricks and Tips

### Space Bar

Holding down the **Space Bar** when using most sliders allows you to go beyond the normal `0` to `100` range. This allows you far more freedom in model customization, especially when it comes to making custom clothing.

!!! note
	Holding down the **Space Bar** when using most sliders allows you to go beyond the normal `0` to `100` range.

	That's right, it was so important I copied it to a note too.

---

### Keyboard Shortcuts and Modifiers

#### General

|Key|Action|
|-|-|
|Space + Click|Hide items that can't be removed by normal clicking (Love Juice, Hair, Emote Bubbles, etc.)|
|`A`|Right Breast Bounce|
|`S`|Left Breast Bounce|
|`Z`|Right Breast Jiggle|
|`X`|Left Breast Jiggle|
|`D`|Crotch Stimulation|
|`F`|Kiss|
|Arrow Keys|Move Screen|
|Space + Arrow Keys|Move Screen Beyond Normal Constraints|
|`Q`|Zoom In|
|`W`|Zoom Out|
|`E`|Set Zoom to `0`|
|`M`|Show Slider Value|
|`V`|Layer Selected Marker|
|`<`|Previous Row|
|`>`|Next Row|
|`7`|Increase Body Height|
|`7` + Shift|Decrease Body Height|
|`7` + Control|Reset Body Height|
|`8`|Increase Body Width|
|`8` + Shift|Decrease Body Width|
|`8` + Control|Reset Body Width|
|`9`|Increase Underwear Transparency|
|`9` + Shift|Decrease Underwear Transparency|
|`9` +  Control|Reset Underwear Transparency|

#### Import/Export (Save and Load Tab)

|Key|Action|
|-|-|
|Space + Click|Turn Off/On All Tabs|

#### Hair Tab

|Key|Action|
|-|-|
|`1`|Match All Hair Colors With Base Hair Color|
|`2`|Match All Outline Colors with Base Outline Color|
|`3`|Toggle All Hair Outlines (On, 50%, and Off)|

#### Ears, Horns, and Tails Tab

|Key|Action|
|-|-|
|`1`|Match Ear and Tail Color to Hair Color|
|`2`|Match Ear and Tail Outline Color to Hair Outline Color|
|`3`|Toggle Ear and Tail Outlines (On, 50%, and Off)|

---

### Auto-Saves

In the event that you do press the dreaded Set button in the Hairpieces Tab, KKL automatically saves backup codes. These are stored in the same directory as `kkl.exe`, in an `autosaves` folder with the current date. Each file is labelled with the date and time it autosaved.

To use one of these backups to restore your model, copy it to another folder and change the file extension to `.txt`. Then load it into Kisekae the way you would any other .txt save.