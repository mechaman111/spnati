# Conditions

The following is a list of conditions that can be used for cases.

## Variable Tests

Variable Test conditions check to see if a variable (such as `~clothing~` or `~name~`) has a certain value.
Variable tests can be added by selecting _Game > Variable Test (+)_ in the conditions editor.

Many new variables have been added that are intended to be used with variable tests;
in certain cases, they can act as more flexible substitutes for existing simple conditions (below).

A case can have any number of variable tests attached.

Tests with the following variables have special shortcuts within the CE (as of version 3.5):

| Variable Tested         | CE Name                      | Description
|-------------------------|------------------------------|----------------------
| `~background~`          | Game > Background            | What background has been selected?
| `~background.location~` | Game > Inside/Outside        | Does the current background depict an indoors or an outdoors setting?
| `~clothing.position~`   | Clothing > Clothing Position | What type of clothing is currently being removed (e.g. `upper`, `lower`, `both`, etc.)?

## Table Filter / Count Conditions

Table Filter conditions count how many characters at the table fit certain criteria, such as tags, gender, and clothing status.

These conditions can be accessed by selecting _Table > Filter (+)_ in the conditions editor.

## Simple Conditions

### Target Character

| Condition | CE Name | Types Accepted    | Description
|-----------|---------|-------------------|------------------------
| `target`  | Target > Target | Character ID      | Is the given character the target for this situation?
| `filter`  | Target > Target Tag | Character Tag  | Does the target for this situation have the given tag?
| `targetStage` | Target > Target Stage | Interval      | Is the target's current stage within the given interval?
| `targetLayers` | Target > Target Layers | Interval     | Does the target's number of layers remaining fall within the given interval?
| `targetStatus` | Target > Target Status | Clothing Status | Does the target's clothing meet the given criteria? (for example: are they bottomless? Topless? Completely naked?)
| `targetStartingLayers` | Target > Target Starting Layers | Interval | Does the number of layers the target started with fall within the given interval?
| `targetSaidMarker` | Target > Target Said Marker | Marker | Tests if a given marker on the target has been _previously_ set.
| `targetNotSaidMarker` | Target > Target Not Said Marker | Marker | Tests if a marker on the target has not been set.
| `targetSayingMarker` | Target > Target Saying Marker | Marker | Tests to see if a marker on the target is _currently_ being set by their current line of dialogue.
| `targetTimeInStage`  | Target > Target Time in Stage | Interval | Does the target's current amount of time in stage fall within the given interval?
| `oppHand` | Target > Target Hand | Poker Hand | Does the target have the given poker hand?

### Also Playing

NOTE: all of the conditions beneath `alsoPlaying` in the following table require that `alsoPlaying` itself be used to designate an also-playing character.

| Condition | CE Name | Types Accepted    | Description
|-----------|---------|-------------------|------------------------
| `alsoPlaying` | Also Playing > Also Playing | Character ID | Is the given character at the table, **AND** are they **not** the current target character?
| `alsoPlayingStage` | Also Playing > Also Playing Stage | Interval  | Is the also-playing character's current stage within the given interval?
| `alsoPlayingSaidMarker` | Also Playing > Also Playing Said Marker | Marker | Tests if a given marker on the also-playing character has been _previously_ set.
| `alsoPlayingNotSaidMarker` | Also Playing > Also Playing Not Said Marker | Marker | Tests if a marker on the also-playing character has not been set.
| `alsoPlayingSayingMarker` | Also Playing > Also Playing Saying Marker | Marker | Tests to see if a marker on the also-playing character is _currently_ being set by their current line of dialogue.
| `alsoPlayingTimeInStage`  | Also Playing > Also Playing Time in Stage | Interval | Does the also-playing character's current amount of time in stage fall within the given interval?
| `alsoPlayingHand` | Also Playing > Also Playing Hand | Poker Hand | Does the also-playing character have the given poker hand?

### Self

| Condition | CE Name | Types Accepted    | Description
|-----------|---------|-------------------|------------------------
| `saidMarker` | Self > Said Marker | Marker | Tests if a given marker on the this character has been _previously_ set.
| `notSaidMarker` | Self > Not Said Marker | Marker | Tests if a marker on the this character has not been set.
| `timeInStage` | Self > Time in Stage | Interval | Does this character's current time in stage fall within the given interval?
| `hasHand` | Self > Has Hand | Poker Hand | Does this character have the given poker hand?

### Game

| Condition | CE Name | Types Accepted    | Description
|-----------|---------|-------------------|------------------------
| `totalRounds` | Game > Total Rounds | Interval | Does the total number of game rounds played so far fall within the given interval?
| `consecutiveLosses` | Game > Consecutive Losses | Interval | Does the number of consecutive losses by either the target character (if applicable) or this character (otherwise) fall within the given interval?

### Table

**NOTE:** All of these conditions include the current character, if applicable!

| Condition           | CE Name                    | Types Accepted   | Description
|---------------------|----------------------------|------------------|-------------------------------------------
| `totalMales`        | Table > Total Males        | Interval         | Tests the total number of male characters at the table.
| `totalFemales`      | Table > Total Females      | Interval         | Tests the total number of female characters at the table.
| `totalAlive`        | Table > Total Playing      | Interval         | Tests the total number of characters still playing at the table.
| `totalExposed`      | Table > Total Exposed      | Interval         | Tests the total number of `exposed` characters at the table.
| `totalNaked`        | Table > Total Naked        | Interval         | Tests the total number of naked (incl. masturbating and finished) characters at the table.
| `totalMasturbating` | Table > Total Masturbating | Interval         | Tests the total number of currently masturbating characters at the table.
| `totalFinished`     | Table > Total Finished     | Interval         | Tests the total number of finished characters at the table.
