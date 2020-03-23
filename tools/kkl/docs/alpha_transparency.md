When loading a code from a file (**not** from the Import/Export tab), KKL will first look for `key=value` pairs, separated by '\n' (one per line).
These key-value pairs can be used to specify alpha transparency for different parts of a character's body.

The last non-whitespace line of the file should contain the actual code to import.
Specifying alpha transparency is only supported for single character ('**') imports.

Note that the keys used are the property / attribute(?) names used internally by Kisekae,
so many of them are in a mix of Japanese and English.

Alpha transparency values are specified as integers ranging from 0 (invisible) to 255 (fully transparent).

### Core Body

 Key                                   | Translated Part
---------------------------------------|----------------
`head`                                 | Head
`head.mouth`                           | Mouth
`head.nose`                            | Nose
`head.face`                            | Face (setting this to 0 leaves behind the facial features and an outline of the head)
`head.ear0`                            | Left Ear
`head.ear1`                            | Right Ear
`head.eye.eye0`                        | Left Eye   _(Note: I'm aware of the inconsistency in the property names. I can't really do anything about it.)_
`head.eye.eye1`                        | Right Eye
`mune`                                 | Upper Body
`dou`                                  | Lower Body
`peni`                                 | Penis
`vibrator`                             | Vibrator

### Arms
 Key                                   | Translated Part
---------------------------------------|----------------
`handm0_0`                             | Left Upper Arm + Shoulder
`handm0_1`                             | Right Upper Arm + Shoulder
`handm0_X.hand.arm2`                   | Upper Arm (armpit down to elbow)
`handm1_0`                             | Left Lower Arm and Hand
`handm1_1`                             | Right Lower Arm and Hand
`handm1_X.hand.arm1`                   | Lower Arm (between elbow and wrist)
`handm1_X.hand.arm0`                   | Hand
`handm1_X.hand.item`                   | Item in Hand

### Legs
 Key                                          | Translated Part
----------------------------------------------|----------------
`ashi0`                                       | Left Leg (whole)
`ashi1`                                       | Right Leg (whole)
`ashiX.thigh.thigh` _and_ `ashiX.shiri.shiri` | Thigh (knee upwards to hip)
`ashiX.leg.leg`                               | Lower Leg (in between knee and ankle)
`ashiX.foot.foot`                             | Foot

### Clothing

 Key                                   | Translated Part
---------------------------------------|----------------
`mune.SeihukuMune`                     | Jacket (Upper Body)
`dou.SeihukuDou`                       | Jacket (Lower Body)
`mune.VestMune` _and_ `mune.VestMune2` | Vest (Upper Body)
`dou.vestDou`                          | Vest (Lower Body)
`mune.YsyatuMune` / `mune.YsyatuMune2` | Shirt (Upper Body)
`dou.YsyatuDou`                        | Shirt (Lower Body)
`mune.Necktie0` / `mune.Necktie1`      | Necktie

### Hair

Notes:
 - I haven't been able to determine what most of these mean yet, sorry.
 - This is probably an incomplete list.
 
 Key                                        | Translated Part
--------------------------------------------|----------------
`head.hair`                                 | 
`head.Bangs`                                |
`head.hairUnder`                            |
`head.HairBaseSen`                          |
`hairUshiro`                                |
`hairBack`                                  |
`hane`                                      |
`head.SideBurnLeft`                         |
`head.SideBurnRight`                        |
`SideBurnMiddle`                            |
`HatBack`                                   |
`head.Headphone0`                           |
`head.Headphone1`                           |
`HairExN` (where N is an integer from 0-98) | Placeable hair pieces 
`RibonN` (where N is as above)              | Placeable hair ribbons


### Example import file

The following example will hide the legs and lower body:

```
ashi0=0
ashi1=0
dou=0
86**<code...>
```
