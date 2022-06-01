# Conditions

How to have your character see the state of the game and opponents

---

Each case can have an arbitrary number of **conditions**. In the behaviour.xml file, these are combined in `<condition>` elements, which are represented in the CE as colored boxes.

A condition element specifies how many among all players (including the character themselves, the subject), the opponents, the opponents except the target (the "Also Playing" players), etc. (called the **role**)
must satisfy the combination of conditions. For example, you can specify that there have to be between 2 and 3 female opponents, each having between 1 and 2 layers of clothing left.

If the same type of condition needs to be applied more than once, for example the subject themselves having said two different markers, you have to use multiple condition elements.

Conditions will be grouped below according to which of the dropdown menus inside the condition boxes they appear in. There are also shortcuts for most common combinations of roles and conditions, as well as variable tests, in the menus at the top of the condition tabs.

In addition to conditions, there are variable tests, which do not get colored boxes in the CE, and which are mainly found in the main Game, Player, and Clothing menus. See the [variables](variables.md) page for
information on those.

## Roles

| Value    | CE Name      | CE Color   | Meaning
|----------|--------------|------------|--------------------------------
| (none)   | Anyone       | Purple     | Counts all players at the table, including the subject character.
| `target` | Target       | Brown      | Counts only the current target. Since there can at most be one target, the CE does not show any Count boxes in this case, but instead a Not checkbox, which generates a 0 count.
| `opp`    | Opponent     | Blue       | Counts all players except the subject character.
| `other`  | Also Playing | Blue       | Counts all players except the target (if any) _and_ the subject character.
| `self`   | Self         | Green      | Counts only the subject themselves. Like with Target, the CE only displays a Not checkbox.
| `winner` | Winner       | Grey       | Counts the winner of the most recent hand. Note that unlike `target`, this is always valid after the first hand has been revealed (unless a tie).

## Non-player conditions

There is one special condition that is not associated with any player and is found in the Game menu despite not being a variable test.

| XML Attribute | CE Name             | Types Accepted    | Description
|---------------|---------------------|-------------------|------------------------
| `totalRounds` | Game > Total Rounds | Interval          | Checks the total number of game rounds completed so far. The value is incremented when the cards are dealt (including after a tie), just after the Dealing Cards situation, and is -1 before the first hand is dealt.

## Matching a specific character

The `character` attribute of the `<condition>` element specifies a particular character that has to be the target, or just present. The character ID is the same as the folder name. The human player also has an ID: `human`.

It is possible for multiple clones of the same character to be present at the same time: Certain preset tables, e.g. the Full Moon table, feature 

## Dialogue Conditions

These conditions check markers, dialogue text, and poses. Usually, because markers and poses are character specific, they are combined with a `character`, but this is not necessary.

| XML Attribute   | CE Name          | Types Accepted         | Description
|-----------------|------------------|------------------------|------------------------
| `saidMarker`    | Said Marker      | Marker[-operator-value]| Checks the value a given marker has _previously_ been set to. It is possible to specify a comparison operator and a target value. Otherwise, the condition is fulfilled if the marker has been set to any non-zero value.
| `notSaidMarker` | Not Said Marker  | Marker                 | Checks that a given marker has not been set.
| `sayingMarker`  | Saying Marker    | Marker[-operator-value]| Checks that a marker is being set by a character's _current_ line of dialogue.
| `saying`        | Saying Text      | String                 | Checks that a character's _current_ line of dialogue contains specific text. The comparison is done before variable substitutions, is case insensitive and ignores formatting.
| `said`          | (TBD)            | String[-operator-value]| Checks how many times a character's _previously_ said dialogue contains specific text. The comparison is done before variable substitutions, is case insensitive and ignores formatting.
| `pose`          | Pose             | Pose name              | Checks that a character's _current_ line of dialogue uses a pose with a specific name. The pose name excludes the stage prefix and filename extension.

### Volatile Conditions

The conditions above that check the _current_ line are called **volatile**. They have the special property that although character dialogue is mainly updated from left to right, they allow reacting to dialoge from characters to the _right_. The way this works is that after the initial round of updates, there are additional rounds of updates that only considers cases with such volatile conditions. If, due to the markers, poses, and/or text of the characters to the right, a higher priority case is found, it replaces the original one, recording such markers, poses, and/or text, or lack thereof, as volatile dependencies. Up to three such rounds can happen, with the restriction that a line that another character depends on in this way cannot be replaced.

### Saying and Said Markers

Generally, marker values set in a previous phase are "said" and can be tested with `saidMarker`/`notSaidMarker`, whereas markers being set/changed in the current phase are not yet "said", and can be tested only with `sayingMarker`. The AI card swap phase is an exception, however, because it happens sequentially (even if you set the AI turn time to instant). There, each character's Swapping Cards and Hand (as well as Masturbating, for eliminated players) lines are committed in turn, but the Hand lines are still considered current until the end of the phase. This means that markers set by Swapping Cards and Hand lines of players to the left can be tested with both `saidMarker` and `sayingMarker`. Only after all characters have swapped cards does the above volatile update happen. If at that point a character to the left reacts to a character to the right, they will get an extra line of dialogue played. Keep in mind that this can yield too little time to read the first line of dialogue.

## Game Conditions

These conditions check certain aspects of the state of the poker game.

| XML Attribute       | CE Name            | Types Accepted         | Description
|---------------------|--------------------|------------------------|------------------------
| `consecutiveLosses` | Consecutive Losses | Interval               | Checks the number of times a player has lost in a row, including the current round (updated in the Reveal Phase, so the current loser has one or more consecutive losses, the others zero. In other words, this is only meaningful with Target and Self. In case of a tie, consecutive losses are _not_ reset.
| `hand`              | Has Hand           | Name of type of hand   | Checks the type of hand a player has, from High Card to Royal Flush. It is possible to check the hands of other players before the Reveal phase (but, without tricks with volatile conditions, only those to the left, there should be a good excuse for doing so, and it can't be used to change your character's `intelligence` for the current round).

## Metadata Conditions

| XML Attribute       | CE Name            | Types Accepted         | Description
|---------------------|--------------------|------------------------|------------------------
| `gender`            | Gender             | "male" or "female"     | Checks a player's gender.
| `tag`               | Tag                | Tag name               | Checks that a player has a given tag.
| `startingLayers`    | Starting Layers    | Interval               | Checks that a player started with a number of layers within a given range.

Using tags to target the human player and/or their gender is deprecated.

## Status Conditions

| XML Attribute       | CE Name            | Types Accepted         | Description
|---------------------|--------------------|------------------------|------------------------
| `status`            | Status             | Status (see below)     | Checks that a player's state of undress matches, or doesn't match, a predefined criterion.
| `stage`             | Stage              | Interval               | Checks a player's current stage. The stage number is incremented each time a player strips, when they start masturbating, and when they finish.
| `layers`            | Remaining Layers   | Interval               | Checks a player's remaining number of layers. Unlike the stage, this is not decremented when the player starts masturbating (it does not become negative).
| `timeInStage`       | Time in Stage      | Interval               | Checks the number of rounds a player has been in the same stage, i.e. not lost a hand. This value is incremented at the end of each round, after the loser of the round is done stripping or has started masturbating, at which point said loser's Time in Stage is 0. When the game is over, it's incremented after every four clicks of the button (after the dialogue update when it goes back from "Wait..." to "Wait"). To make it a bit more useful when a player finishes masturbating, it's not reset to 0 until just _after_ the Finished cases have played.

### Definitions of Statuses

| Value            | CE Name                         | Meaning
|------------------|---------------------------------|---------------------------
| `lost_some`      | Lost something                  | Has lost at least one layer, i.e. `stage` > 0.
| `mostly_clothed` | Lost only accessories           | Has only lost accessories, _and_ is `decent` (see next item).
| `decent`         | Still covered by major articles | Started with `major` articles on both the `upper` and `lower` part of the body, and has not lost a `major` article.
| `exposed`        | Chest and/or crotch visible     | Either `chest_visible` or `crotch_visible`, or both.
| `chest_visible`  | Chest visible                   | Has removed an `important` article from the `upper` part of the body, or, if none exists, has removed all `major` articles.
| `crotch_visible` | Crotch visible                  | Has removed an `important` article from the `lower` part of the body, or, if none exists, has removed all `major` articles.
| `topless`        | Topless (not naked)             | `chest_visible` but _not_ `crotch_visible`.
| `bottomless`     | Bottomless (not naked)          | `crotch_visible` but _not_ `chest_visible`.
| `naked`          | Naked (fully exposed)           | Both `chest_visible` and `crotch_visible`, but may still have layers left.
| `lost_all`       | Lost all layers                 | Has 0 layers left.
| `alive`          | Still in the game               | Not out; has not started masturbating. The negation of this is the same as Masturbating or Finished Masturbating.
| `masturbating`   | Masturbating                    | Is about to start masturbating or is in the masturbating stage (number of layers + 1).
| `finished`       | Finished masturbating           | Is in the finished stage (number of layers + 2).

Obviously, statuses are not generally mutually exclusive. Note in particular that `exposed` through `lost_all` are all still true when a player is out.

### Stage and Status transitions

When a player strips, the stage change and all the associated updates happen between the Stripping and After Stripping phases. As the default text for the After Stripping situation says, "this is the start of a new stage".
Note that this means that you'll need to use different status conditions in "Will Be Visible" cases vs. "Is Visible" ones.

Time in Stage is actually set to -1 at this point, so that it becomes 0 after the end of the round.

When a player starts masturbating, things are not quite as consistent, but as with Must Strip, nothing has happened yet in the Must Masturbate situation. In particular, the number of players Still in the game is still the same.
The stage transition happens _after_ Start Masturbating (which mostly corresponds to Stripping), but the player's `out` flag is set _before_ that. This means that `status` does not match `stage` in the Start Masturbating situation.

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

