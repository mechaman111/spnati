# Variables #

Variables are placeholders that you can use in your dialogue and let your characters say things that are relevant to the situation; as the most basic example, the name of the player that lost the current hand and has to strip. In addition to appearing in dialogue text, they can be used in generic variable tests that let you determine which lines are relevant to a given situation.

Variable names are enclosed in tildes (`~`). Example: `~name~`. Several variables are structured in multiple levels; you only put tildes at the beginning and end, e.g. `~target.marker.some_marker~. However, there exist functions, which take arguments that can contain other variable references enclosed in tildes.

Variable names are generally not case sensitive. However, if the first letter of a variable name is written in uppercase, the first letter of the resulting variable expansion will be converted to uppercase. Thus, if a sentence begins with a variable, the variable name should start with an uppercase letter, as in `~Player~ must be new to this!`.

## General ##

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~player~`   | The name entered by the human playing the game, regardless of situation. The same as `~human~`.  This is also a player variable (see below). |
| `~name~`     | The name (label) of the current target. The same as `~target~`. The target depends on the situation and not all situations have a target. | 
| `~background~` | The name of the current background. |
| `~rng(lo-hi)~` | Generates a random number between `lo` and `hi`, inclusive. |

Note that backgrounds have lots of information that can be accessed using variables;
see the page on backgrounds for more details.

## Cards ##

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~cards~`  | In the `swap_cards` situation, the number of cards swapped. |
| `~cards.text~` | The number of cards swapped in textual form, e.g. `three`. |
| `~cards.ifplural(text if plural\|text if singular)` | Expands to `text if singular` if the number of cards swapped is one, and `text if plural` otherwise, so you can actually include the word "card" or "cards" as appropriate. |

## Clothing ##

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~clothing~` | The name of the article of clothing the target is stripping/stripped. |
| `~clothing.ifplural~` | Lets you make your dialogue correct according to the grammatically number of the current article of clothing. See the _ifplural_ section below, for how to use this variable. |
| `~clothing.plural~` | Expands to `plural` or `single`, for use in variable tests if you want to write completely separate cases depending on the grammatical number. |
| `~clothing.generic~` | The generic designation of the current article. For example, sandals, boots, and shoes might all be generically referred to as shoes (TBD). If no generic designation is assigned in the character's behaviour file, this expands to the same as `~clothing~`. |
| `~clothing.type~` | The `type` of the current article – `extra`, `minor`, `major`, or `important` |
| `~clothing.position~` | The `position` of the current article – `upper`, `lower`, `both`, `feet`, `hands`, `arms`, `legs`, `waist`, `neck`, `head`, or `other`.

## Collectibles ##

These variables can also be used as player variables; see the section below for
more detail on how those work.

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~collectible.Collectible_ID~` | `true` if the collectible `Collectible_ID` has been unlocked, `false` otherwise. |
| `~collectible.Collectible_ID.counter~` | The current value of `Collectible_ID`s counter. |

## Date and Time ##

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~weekday~` | The current day of the week, from Monday to Sunday |
| `~weekday.number~` | The current day of the week, from 1 (Monday) to 7 (Sunday) |
| `~day~` | The current day of the month as an ordinal (1st through 31st) |
| `~day.number~` | The current day of the month (between 1 and 31) |
| `~month~` | The current month, from January to December |
| `~month.number~` | The current month as a number (1-12) |

## Markers ##

These variables can also be used as player subvariables; see the section below for
more detail on how those work.

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~marker.marker_name~` | If there is a target, and your character has set a target-specific marker `marker_name` for that character, expands to its value. Otherwise it expands to the value of the regular marker `marker_name`. |
| `~targetmarker.marker_name~` | If there is a target, and your character has set a target-specific marker `marker_name` for that character, expands to its value. |
| `~marker.marker_name.subvariable~` | If your character has set a marker `marker_name` whose value corresponds to a character's ID, you can additionally use the marker to access information about that character as if it were a player variable. See the section below on Indirection for more details. |
| `~targetmarker.marker_name.subvariable` | You can also indirect through target-specific markers in the same way as regular markers. |

## Player Variables ##

For each player, a set of variables exist, which can be accessed through their ID with any non-word character (word characters are a-z, 0-9, and _) removed. For instance, D.Va is referred to as `~dva~`.

Four special player IDs exist: 
 - `target`, which refers to the current target,
 - `winner`, which refers to the player who had the best hand most recently (if two players tied for best hand, it's indeterminate who is `winner`), and 
 - `self`, which refers to the subject (your) character.
 - `player`, which refers to the human player, though you can also refer to them using `human`.

| Subvariable  | Description                                    |
| ------------ | ---------------------------------------------- |
| (none)       | Using only `~character_id~`, `~target~`, or `~self~` returns the name of the referenced player, or a nickname if one has been specified for that character. |
| `.name`     | Alternately, you can use `.name` to explicitly get the name of the referenced player. This is useful if you're indirectly referencing a player using a variable (see below). |
| `.id` | The internal ID (folder name) of the referenced player. This is useful in conjunction with indirect referencing (see below). |
| `.position`  | The position, `left` or `right` (from the perspective of the human player, not the characters) of the player relative to the subject character. The human player is considered to be to `across` from all characters. `~self.position~` resolves to simply `self`. |
| `.distance` | How many slots away this character is from the subject character. `1` indicates the characters are adjacent. |
| `.slot`      | The slot number of the player, from 0 (the human player) to 5. |
| `.collectible`, `.marker`, `.targetmarker` | Lets you access collectible and marker data of a different character, as well as perform indirect references through another player's markers. See the sections above and below for details. |
| `.tag.tag_name` | `true` if the player has the tag `tag_name`, `false` otherwise. |
| `.costume`      | The ID of the player's alternate costume/skin, or `default` if no alternate costume is worn. |
| `.size`         | The player's breast or penis size depending on the gender (`small`, `medium`, or `large`). |
| `.gender`       | The player's (current) gender, `male` or `female`. |
| `.ifmale` | Outputs "text if male" if the player is male and "text if female" otherwise. Typically uses with `he\|she`, `him\|her` etc. in cases that don't depend on the particular player's gender except for the choice of pronouns. See the section below for how to use this variable. |
| `.place`        | The player's current rank in terms of layers left or the reverse order they are eliminated, between 1 and 5 (if there are that many players). Two (or more) players can be in the same place if they have the same number of layers. In other words, the number of other players that have more layers left or were eliminated after this player, plus one. |
| `.revplace`     | Like `.place`, but counting from the bottom. The number of other players that were eliminated before this player or have fewer layers left, plus one. This mainly exists because the number of players isn't always five. |
| `.lead`         | The difference in number of layers between this player and the best of the rest. If this player shares first place, this is 0. If this player isn't in first place, this is negative. If and only if this is > 1 when the player loses a hand, they are considered "winning". |
| `.trail`        | The difference in number of layers between the worst performing of the other *remaining* players and this one. That is, this variable ignores eliminated players, since it wouldn't be very useful once the first player is out otherwise. If this player isn't in last place (of the remaining players), this is negative. |
| `.biggestlead`  | The largest lead in layers this player has had over all the others at some point. If a player with a considerable `biggestlead` is eliminated, that may be noteworthy. Note: This just remembers the largest `.lead` value, it doesn't return the largest lead held over the remaining players. |
| `.diff`         | The difference in layers left between this player and the subject player. Can be negative. For instance, use `~target.diff~` to get a grasp of whether the player stripping is doing better or worse than your character. |
| `.diff(other)`  | The difference in layers left between this player and the player given by `other`, which can be `target` or `self`.        |
| `.stage`        | The player's current stage.     |
| `.hand`         | A not too formal or exact description of the current hand, for example "a pair of queens", "three sixes", "a straight". Use like `I had ~self.hand~!` |
| `.hand.noart`   | Like above, but with no indeterminate article. Use like `My ~self.hand.noart~ was better than your ~target.hand.noart~!` |
| `.hand.score`   | A numerical value of the hand. The hundreds digit specifies the type of hand (0 = High card, 1 = One pair, 2 = Two pair, 3 = Trips, 4 = Straight, 5 = Flush, 6 = Full house, 7 = Quads, 8 = Straight flush, and 9 = Royal Flush). The rest of the digits specify the rank of the (top) pair, triplet and so on. So 14 = ace high, 107 = a pair of sevens, 413 = King-high straight. It's not complete information about the hand, but better than just "a pair"; the difference between a pair of aces and a pair of deuces is *huge*.


## `.ifplural` ##

The `.ifplural` variables, available as `~cards.ifplural~` and `~clothing.ifplural~`
allow you to make your dialogue gramatically correct, according to whether or not
the referenced item is plural or not.

The syntax for these variables are: `~variable.ifplural(dialogue if plural|dialogue if singular)~`.

For example, the dialogue `I have ~cards.ifplural(several cards|one card)~.` will expand to:
 - "I have one card." if the character has exactly one card.
 - "I have several cards." if the character has more than one card.

However, if you're trying to do something like `~clothing~` followed by `~clothing.ifplural(s|)~`,
you should use `~clothing.toplural~` instead.


## `~player.ifmale~` ##

`~player.ifmale(|)~` works similarly to `.ifplural` (described above), but works
based on the referenced player's gender.

The syntax for this variable is: `~player.ifmale(dialogue if male|dialogue if female)~`.


## Indirection ##

The `marker.` and `targetmarker.` variables (and their player subvariable counterparts) can
be used to _indirectly_ reference a player at the table, using their ID.

This lets you access information about a player whose ID has been stored in a marker,
as if you were using a player variable.

An indirect reference needs two components in order to work properly:
 - A marker that has been set to a character's ID, for example using `~target.id~`, or to one of the special player IDs, such as `winner`.
   - Indirect references using a marker that doesn't exist, or that references a character not at the table, will expand to an empty string.
   - If a marker is set to one of the special player IDs such as `winner`, the indirect reference will access the appropriate character based on the current context.
 - The actual full variable being accessed needs to have additional subvariables listed after the marker name.
   - If you don't have an additional subvariable after the marker name, `~marker.[name]~` will of course just expand directly to the value of the marker.

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~marker.name.subvariable~` | Performs indirect access of a character whose ID is specified by a marker. |
| `~targetmarker.name.subvariable~` | Like the above, but works with target-specific markers. |
| `~player.marker.name.subvariable~` | Performs indirect access of a character whose ID is specified by a marker set on another player. |
| `~player.targetmarker.name.subvariable~` | Performs indirect access of a character whose ID is specified by a target-specific marker set on another player. |

### Examples ###

For clarity, here are some examples for how to use indirect references.

Suppose we're playing a game with the following setup:
- D.Va is at the table.
    - She has marker `refA` set to `chihiro`.
    - She just had the best hand in the current round.
- Chihiro is also at the table:
    - He has a marker, `refB` set to `d.va`.
    - He also has a target-specific marker, `refC`, whose value for Sayori is set to `winner`.
    - He also has a persistent marker, `refD`, whose value is set to `monika` (but she's not present in this game).
- Sayori is also at the table, but has no markers of interest set.
    - However, she has just lost a hand, and is stripping.

To summarize:
    - The current `winner` is D.Va.
    - The current `target` is Sayori.
    - For Chihiro:
        - `marker.refB` would expand to `d.va`.
        - `marker.refC` and `targetmarker.refC` would both expand to `winner`.

#### Example 1 ####

- **Character:** Chihiro
- **Variable:** `~marker.refB.name~`
- **Expansion:** "D.Va"
- **Explanation:**
    - The value of marker `refB` for Chihiro is `d.va`.
    - This matches D.Va's character ID, and she is at the table, so we can reference her indirectly.
        - After cleaning up capitalization and non-alphanumeric characters in the marker value, we get `dva`.
    - `dva.name` expands to "D.Va", and we're done.


#### Example 2 ####

- **Character:** Chihiro
- **Variable:** `~dva.marker.refA.name~`
- **Expansion:** "Chihiro"
- **Explanation:**
    - First, we access D.Va as a player variable directly as `dva`.
    - She has a marker `refA`, and it's set to `chihiro`.
        - Since it matches Chihiro's character ID, and he's of course at the table, we can reference him indirectly.
        - Cleaning up capitalization and non-alphanumeric characters doesn't do anything in this case.
    - `chihiro.name` expands to "Chihiro", and we're done.


#### Example 3 ####

- **Character:** Chihiro
- **Variable:** `~marker.refB.marker.refA.name~`
- **Expansion:** "Chihiro"
- **Explanation:**
    - This proceeds like in Example 1 at first, where `marker.refB` indirectly references D.Va as a player variable.
    - However, after that, we then use her marker `refA` as _another_ indirect reference.
        - This resolves to Chihiro, as in Example 2.
    - Then, `chihiro.name` expands to "Chihiro".
    - To summarize:
        - `marker.refB` resolves to D.Va.
        - `dva.marker.refA` resolves back to Chihiro.
        - `chihiro.name` expands to "Chihiro", and we're done.
- **Note:**
    - Indirect references can be nested like this to arbitrary levels.
    - However, be aware that each layer of indirection you use adds a point of failure to the variable expansion.


#### Example 4 ####

- **Character:** Chihiro
- **Variable:** `~targetmarker.refC.name~`
- **Expansion:** "D.Va"
- **Explanation:**
    - First, we get the value of target-specific marker `refC` on Chihiro.
        - Since Sayori is the current target, we get the value of `refC` that is specific to her, which in this case is `winner`.
    - `winner` doesn't directly name a character by ID, but it is one of the special character IDs.
    - In this case, `winner` resolves to D.Va, so we ultimately reference her, as with Example 1.
    - Finally, `dva.name` expands to "D.Va", and we're done.


#### Example 5 ####

- **Character:** Chihiro
- **Variable:** `~marker.nonexistent.name~`
- **Expansion:** "" (an empty string)
- **Explanation:**
    - First, we try to get the value of marker `nonexistent` on Chihiro.
        - Since it hasn't been set (presumably), its value defaults to an empty string.
    - We then try to access a character by that ID:
        - Since no character can have an ID equal to the empty string, this will always fail.
    - Since we couldn't find the referenced character, we give up and expand the entire variable to an empty string.


#### Example 6 ####

- **Character:** Chihiro
- **Variable:** `~marker.refD.name~`
- **Expansion:** "" (an empty string)
- **Explanation:**
    - First, we get the value of marker `refD` on Chihiro, which is `monika`.
    - We then try to access a character by that ID:
        - `monika` is, of course, a valid character ID.
        - _However_, since she's not at the table for this game, the reference to her fails to resolve.
    - Since we couldn't find the referenced character, we give up and expand the entire variable to an empty string.
