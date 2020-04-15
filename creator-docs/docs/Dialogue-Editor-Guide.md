# The Dialogue Editor

![annotated dialogue editor guide v3.5](images/dialogue-editor-annotated-3.5.png)

## Overview

The following guide will attempt to explain the different parts of the dialogue
editor at a high level. Each section below corresponds to a numbered box in the
reference image above.

## How Dialogue Works

Every character's dialogue is divided into different _cases_.
Each case is associated with:
 - a basic _situation_ tag ("Game Start", "Stripping", etc.) indicating when its associated dialogue can be displayed,
 - optional _conditions_ that must be met before it can be used, and
 - a _priority_ value that allows certain cases to supersede others.

Whenever the game triggers a dialogue update for a given situation, it
internally goes through the following steps:

1. Look through the character for all cases that match the given situation AND have all conditions met.
2. Filter out all cases except for those with the highest priority.
3. Select a line at random from the remaining cases.

## 1. Case Selector

The Case Selector (box 1, at left) displays every case within your character's dialogue,
along with any conditions and the priority associated with each case.
Clicking on any case here will bring it up for editing in the editor view to the right.

#### 2. Case Tools

Above the Case Selector (box 2, top left), you'll find buttons for adding, removing, or modifying dialogue cases
within the Selector. You can also switch how cases are organized in the Selector
using the `View:` dropdown.

#### 3. Case Filtering

Below the Case Selector (box 3, bottom left), you can filter the Selector so that
it displays only certain types of cases (the _Filter_ dropdown),
or only cases targeted towards a specific character (the _Target_ field).

## 4-7: Case Overview

These parts of the Dialogue Editor mainly deal with the more general aspects of individual
cases.

#### 4. Case Situation

This area (box 4, at top) simply displays what situation the current case is associated with,
along with a brief description of the situation.

The case shown in the reference image is associated with the _Opponent Stripping_ case,
which means it'll be considered whenever any opponent strips. Obviously, this is a very
general case, so conditions are used to limit when it plays, which is explained below.

#### 5. "Respond to This" Button

This button (box 5 at top, labeled _"Respond to This..."_) allows you to create a response to this case on another character;
in other words, it'll create a new case on another character, with conditions
set up so that both cases will play together at the same time.

#### 6. "Call Out" Button

This button (box 6 at top, labeled _"Call Out..."_) creates an entry in the "Situations" tab for this case, which
will cause it to show up in the "Writing Aid" tab for every other character.

#### 7. Stage Selector

This area (box 7, at top) allows you to select what stages this case falls under.

Note that the selected stages don't necessarily need to be contiguous;
for example, you can set a case to be available first in stages 0 and 1,
then 3 and 4 (skipping stage 2).

## 8-10: Case Conditions

These parts of the Dialogue Editor allow you to add and change the conditions
associated with your cases.
This part of the Dialogue Editor has perhaps changed the most between different versions
of the CE as a whole.

#### 8. Condition Tabs

Each of the labels here (box 8, center) correspond to particular types of condition.
Hovering your mouse over any of these categories will reveal a dropdown list,
from which you can select conditions to add to the current case.

Some condition types cannot be used in certain situations. For example,
Target conditions don't make sense when used with self-stripping situations
or with the dealt hand cases.

[A full list of possible conditions can be found here.](Conditions)

#### 9. Conditions View

At the center of the conditions editor (box 9, center) is a list of all conditions associated
with the current case.
From here, you can edit the particular values associated with each condition.

The two buttons at the far right of each condition (the star and the red box)
allow you to favorite or remove conditions, respectively. Favorited conditions will
be automatically added to new cases by default.

The particular case shown in the reference image has two conditions, specifying that:

 - This case will only play if **Chiaki** is at the table (the _Target_ condition), and
 - This case will only play if Chiaki is **fully clothed** (the _Target Stage_ condition).
 
#### 10. Priority Value
 
Entering an number into this field (box 10, center-right) allows you to set a
custom priority for the current case.

By default, the priority for each case is calculated based on the conditions
attached to it; setting a custom priority overrides this, however.

In particular, smart use of this feature can allow you to either ensure that a
case will always be used if it its conditions are met, or conversely ensure that
lines that are dependent on conditions or targeting are mixed in with completely generic lines.

## 11-13: Writing

These parts of the dialogue editor deal with the actual lines of dialogue within each case.

#### 11. Line Editor

This part of the dialogue editor (box 11, at center) is where you actually write your character's dialogue.

Individual "lines" of dialogue consist of text and a character image at minimum.
Lines can also have more complex settings or effects, many of which are hidden behind the _Show Advanced_ checkbox:

![dialogue editor advanced settings](images/dialogue-editor-advanced-settings.png)

 - **Markers** allow your character to remember things for use later in other dialogue. They can act as flags, counters, and can also store other information (such as what clothing a given character has taken off).
   - For the programming-savvy: markers are in essence variables.
   - Individual lines can specify a given marker to modify when and if that line is selected to be displayed, indicated by the **Marker** column.
   - The value to assign to the marker can be specified in the **Value** column:
     - If the Value field is left blank, the marker will be assigned a value of 1 by default.
     - Entering **any number** into the Value field will cause the marker to be set to that value.
     - Entering **'+' or '-'** into the Value field will cause the marker to increment or decrement, respectively.
     - Entering **text** into the Value field will cause that text to be stored in the marker.
     - Entering **variables** into the Value field (for example, `~clothing~`) will store the value of that variable into the marker.
 - The **Gender**, **Label**, **AI**, and **Size** columns allow you to change the corresponding attributes of your characters whenever the listed dialogue plays.
 - The **Arrow Direction** and **Arrow Location** columns allow you to customize the look of the in-game dialogue bubbles for each line. Note that the Arrow Location column takes a percentage value.
 
#### 12. Unique Lines Counter

This counter (box 12, top-right) displays how many _unique_ lines of dialogue have been written for your character so far.

#### 13. Character Image

This area (box 13, at right) displays the selected image for the currently-selected line of dialogue.
 
