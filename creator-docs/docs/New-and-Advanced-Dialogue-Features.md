## Contents

- [Condition: `targetStatus`](#condition-targetstatus)
- [Condition: `targetSayingMarker` / `alsoPlayingSayingMarker`](#condition-targetsayingmarker-alsoplayingsayingmarker)
- [Condition: `targetLayers`](#condition-targetlayers)
- [Gender and Status Counting Conditions (`<condition>`)](#gender-and-status-counting-conditions-condition)
- [Variable Test Expressions (`<test>`)](#variable-test-expressions-test)
- [Targeted Markers (`marker*`)](#targeted-markers-marker)
- [Marker Value Storage (`+marker`, `-marker`, `marker=~variable~`)](#marker-value-storage-marker-marker-markervariable)
- [Variable: `~marker.markerName~`](#variable-markermarkername)
- [Variables: `~background~` / `~background.location~`](#variables-background-backgroundlocation)
- [Variable: `~weekday~`](#variable-weekday)

## Condition: `targetStatus`
 - **Introduced:** Merge Request 867 (merged November 7, 2018)
 - **CE Support:** Yes (3.0.1)
 - **make_xml.py Support:** Yes

#### Description
This condition allows you to check how clothed a targeted character currently is.
For example, lines can be conditioned on whether a target character is currently topless or bottomless.

More than one can be true at the same time and all the following statuses can be negated by prepending “not_”, or checking the box in the Not column in the CE.

Some are more useful with `<condition>` (see below).

Note that as with the legacy counting conditions (`totalExposed` etc.), the status changes between stages, i.e. from e.g. `female_crotch_will_be_visible` to `female_crotch_is_visible`)

 - `lost_some`: has stripped at least once (`not_lost_some` is the same as `targetStage="0"`).
 - `mostly_clothed`: has not removed anything except accessories, and is decent.
 - `decent`: has at least one major article with `position=upper` and one with `position=lower`, or one with `position=both`.
   - (This is subject to change and several characters need changes to the positions of their major articles for this to work as intended.)
 - `chest_visible`: has exposed their chest
 - `crotch_visible`: has exposed their crotch
 - `topless`: has exposed their chest but not their crotch
 - `bottomless`: has exposed their crotch but not their chest
 - `exposed`: has exposed their chest or their crotch, or both
 - `naked`: has exposed both their chest and their crotch
 - `lost_all`: has no layers left (same as `targetLayers="0"`); will have to masturbate at the next loss
 - `alive`: still in the game (`not_alive` means either masturbating or finished)
 - `masturbating`
 - `finished`

#### Use Cases
 - Check for `not_chest_visible` or `bottomless` to detect bottom exposed before top.
 - Check for `not_decent`, `exposed`, and/or `naked` to respond more properly when a player strips an accessory after major or even important articles.
 - Check for `mostly_clothed` together with, say, `targetStage="3-"` to detect that a player has removed many accessories.
 - Reassure an opponent who is at least still `decent`.


## Condition: `targetSayingMarker` / `alsoPlayingSayingMarker`
 - **Introduced:** Merge Request 1164 (merged December 3, 2018)
 - **CE Support:** Yes (3.0.1)
 - **make_xml.py Support:** Yes
 
#### Description
This condition works the same as the basic `targetSaidMarker` and `alsoPlayingSaidMarker` conditions, but specifically checks the lines the other characters at the table are currently saying. Markers from lines that were said in the past are ignored.

## Condition: `targetLayers`
 - **Introduced:** Merge Request 867 (merged November 7, 2018)
 - **CE Support:** Yes (3.0.1)
 - **make_xml.py Support:** Yes
 
#### Description
This condition allows you to check how many articles of clothing a targeted opponent is currently wearing.


## Gender and Status Counting Conditions (`<condition>`)
 - **Introduced:** Merge Request 867 (merged November 7, 2018)
 - **CE Support:** Yes (3.0.1)
 - **make_xml.py Support:** Yes

#### Description
In addition to counting the number of characters with a certain tag at the table, you can also check how many characters at the table share a given clothing status, gender, or both. For example, you can write lines targeting a specific number of topless men or bottomless women at the table.

See `targetStatus` above for possible status codes.

Within the CE, conditions using this feature can be found under the `Table` tab of the Dialogue Editor.


## Variable Test Expressions (`<test>`)
 - **Introduced:** Merge Request 1003 (merged November 10, 2018)
 - **CE Support:** Yes (3.1)
 - **make_xml.py Support:** Yes
 
#### Description
Variable tests can be used to target lines based on the values of dialogue variables.
For example, lines can be targeted towards a specific `~clothing~` type or `~player~` name.

This condition can be found under `Game > Variable Test (+)` within the CE dialogue editor.

#### Use Cases
 - Use with `~background~` to target a specific choice of game background.
 - Use with `~player~` to target specific player names.
 - Use with `~weekday~` to target specific days of the week.
 - Use with `~marker.markerName~` to replicate `saidMarker` conditions.
 
 
## Targeted Markers (`marker*`)
 - **Introduced:** Merge Request 1160 (merged November 29, 2018)
 - **CE Support:** Yes (3.1)
 - **make_xml.py Support:** Yes
 
#### Description
Adding an asterisk to the end of a marker’s name (for example, `marker*`) will make that marker target-specific.

Target-specific markers can have different values for each opponent at the table, and lines that use them will ‘see’ the value that corresponds to the currently-targeted opponent.


## Marker Value Storage (`+marker`, `-marker`, `marker=~variable~`)
- **Introduced:** Merge Requests 978 and 1160 (merged September 5, 2018 and November 29, 2018)
- **CE Support:** Yes
- **make_xml.py Support:** Yes

#### Description
In addition to simply serving as flags, markers can be used to store information such as counters or variable values.

For example, to increment or decrement a marker called `myMarker`:
 - With `make_xml.py`, set `marker:+myMarker` or `marker:-myMarker`
 - With the CE, set the line _Marker_ field to `myMarker` and the _Value_ field to either `+` or `-`.

To set a marker to store a specific value:
 - With `make_xml.py`, set `marker:myMarker=value`
 - With the CE, set the line _Marker_ field to `myMarker` (as before) and the _Value_ field to any value.
   - If the _Value_ field is left blank, the marker value will be set to 1 by default.
 
If the value is a variable (`~name~`, `~cards~`, `~clothing~`, etc.) then the expanded value of the variable will be stored
(for example, `Mister`, `5`, or `pants`).

## Variable: `~marker.markerName~`
- **Introduced:** Merge Request 1160 (merged November 29, 2018)
- **CE Support:** Yes (3.1)
- **make_xml.py Support:** Yes

#### Description
You can insert the value of a marker into dialogue text by using `~marker.markerName~`, replacing `markerName` with any marker name.

As with other variables, this syntax can also be used in `<test>` expressions (see above).

## Variables: `~background~` / `~background.location~`
- **Introduced:** Merge Request 1179 (merged December 9, 2018)
- **CE Support:** Yes
- **make_xml.py Support:** Yes

#### Description
Information about the currently selected background can be used in dialogue and in conditions through the `~background~` and `~background.location~` variables.

The `~background~` variable will be substituted with a lowercase value corresponding to the current game background (for example, `inventory`, `bedroom`, `street`)

The `~background.location~` variable will be substituted with either `indoors` or `outdoors` depending on whether the depicted background location is indoors or outdoors.

For more details, see [Backgrounds](backgrounds).

## Variable: `~weekday~`
 - **Introduced:** Merge Request 1174 (merged December 7, 2018)
 - **CE Support:** Yes
 - **make_xml.py Support:** Yes

#### Description
The `~weekday~` variable will be substituted with the current day of the week (‘Monday’, ‘Tuesday’, etc), both in dialogue and in `<test>` expressions.
