# Variables #

Variables are placeholders that you can use in your dialogue and let your characters say things that are relevant to the situation; as the most basic example, the name of the player that lost the current hand and has to strip. In addition to appearing in dialogue text, they can be used in generic variable tests that let you determine which lines are relevant to a given situation.

Variable names are enclosed in tildes (`~`). Example: `~name~`. Several variables are structured in multiple levels; you only put tildes at the beginning and end, e.g. `~target.marker.some_marker~. However, there exist functions, which take arguments that can contain other variable references enclosed in tildes.

Variable names are generally not case sensitive. However, if the first letter of a variable name is written in uppercase, the first letter of the resulting variable expansion will be converted to uppercase. Thus, if a sentence begins with a variable, the variable name should start with an uppercase letter, as in `~Player~ must be new to this!`.
Additionally, if a variable is written in all caps (e.g. `~PLAYER~`), then the expansion will also be all-uppercase.


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
| `.position`  | The position, `left` or `right` (from the perspective of the human player, not the characters) of the player relative to the subject character. The human player is considered to be to `across` from all characters. `~self.position~` resolves to simply `self`. |
| `.distance` | How many slots away this character is from the subject character. `1` indicates the characters are adjacent. |
| `.slot`      | The slot number of the player, from 0 (the human player) to 5. |
| `.collectible`, `.marker`, `.targetmarker` | Lets you access collectible and marker data of a different character. See the corresponding general variable descriptions above for details. |
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
