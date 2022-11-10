# Variables

How to let your characters talk about the game with dynamic text

---

Variables are placeholders that you can use in your dialogue and let your characters say things that are relevant to the situation; as the most basic example, the name of the player that lost the current hand and has to strip. In addition to appearing in dialogue text, they can be used in generic variable tests that let you determine which lines are relevant to a given situation.

Variable names are enclosed in tildes (`~`). Example: `~name~`. Several variables are structured in multiple levels; you only put tildes at the beginning and end, e.g. `~target.marker.some_marker~. However, there exist functions, which take arguments that can contain other variable references enclosed in tildes. Note, however, that the parsing can't handle nested functions.

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
| `~cards~`  | In the `swap_cards` situation, the number of cards swapped. (It can also be used after swapping.) |
| `~cards.text~` | The number of cards swapped in textual form, e.g. `three`. |
| `~cards.ifplural(text if plural|text if singular)~` | Expands to `text if singular` if the number of cards swapped is one, and `text if plural` otherwise, so you can actually include the word "card" or "cards" as appropriate. |

## Clothing ##

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~clothing~` | The name of the article of clothing the target is stripping/stripped. |
| `~clothing.a~` | The proper indefinite article for `~clothing~`, if it's not plural or uncountable. Includes a following space as needed. For example, `~clothing.a~~clothing~` can expand to "an apron", "a shirt", or "pants". |
| `~clothing.ifplural(|)~` | Lets you make your dialogue correct according to the grammatically number of the current article of clothing. See the _ifplural_ section below, for how to use this variable. |
| `~clothing.plural~` | Expands to `plural` or `single`, for use in variable tests if you want to write completely separate cases depending on the grammatical number. |
| `~clothing.toplural~` | Converts `~clothing~` to plural form if it isn't already, and isn't uncountable. Example: "dress" becomes "dresses". This can let your character talk more naturally about clothing of the same kind as that in question. |
| `~clothing.generic~` | The generic designation of the current article. For example, sandals, boots, and shoes might all be generically referred to as shoes (TBD). If no generic designation is assigned in the character's behaviour file, this expands to the same as `~clothing~`. |
| `~clothing.type~` | The `type` of the current article – `extra`, `minor`, `major`, or `important` |
| `~clothing.position~` | The `position` of the current article – `upper`, `lower`, `both`, `feet`, `hands`, `arms`, `legs`, `waist`, `neck`, `head`, or `other`.
| `~clothing.id~` | Expands to the ID of the collectible associated with this piece of clothing, if any. For player clothing options that are available by default, this will instead expand to something starting with `_default.`. Note that this is only defined for clothing worn by the human player: this variable will always expand to an empty string for clothing worn by NPCs. |

## Collectibles ##

These variables can also be used as player variables; see the section below for
more detail on how those work.

| Variable     | Description                                    |
| ------------ | ---------------------------------------------- |
| `~collectible.Collectible_ID~` | `true` if the collectible `Collectible_ID` has been unlocked, `false` otherwise. |
| `~collectible.Collectible_ID.counter~` | The current value of `Collectible_ID`s counter. |
| `~collectible.Collectible_ID.wearing~` | `true` if the clothing associated with `Collectible_ID` is currently being worn by the human player, and `false` otherwise. |

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

A condition can also define a custom player variable. If the condition matches at least one player, that custom variable will be bound to one of them randomly.
If multiple conditions define the same variable, and they all require at least one player fulfilling it, it is guaranteed that at least one player satisfies all those
conditions. You can also add extra restrictions using variable tests on the custom variable. Furthermore, any additional matches will (randomly) be assigned variables
with the same names but with `2`, `3`, and so on, appended. These can also be used with variable tests, but note that not all possible combinations of such additional
numbered variables will be tried.

Note that you _don't_ have to define a custom variable to reference a specific character whose presence you've ensured with an Opponent or Also Playing condition.

| Subvariable  | Description                                    |
| ------------ | ---------------------------------------------- |
| (none)       | Using only `~character_id~`, `~target~`, or `~self~` returns the name of the referenced player, or a nickname if one has been specified for that character. |
| `.id`        | The ID (folder name) of the referenced player. Can be useful to store in a marker.   |
| `.position(other)`  | The position, `left` or `right` (from the perspective of the human player, not the characters) of the player relative to `other`, defaulting to the subject character. The human player is considered to be to `across` from all characters. `~self.position~` resolves to simply `self`. |
| `.distance(other)` | How many slots away this player is from `other`, defaulting to the subject character. `1` indicates the characters are adjacent. |
| `.slot`      | The slot number of the player, from 0 (the human player) to 5. |
| `.attracted(other)`, `.compatible(other)` | Helps checking all relevant sexual orientation tags in one go. `.attracted()` only tests for predominant sexual attraction to `other`'s gender (defaulting to the subject character), whereas `compatible()` returns `true` also if the player is curious. If the player has no sexual orientation tag at all, `undefined` is returned. So, for example, if the subject character is female, `~target.compatible~ != false` yields the same result as checking that the target is male and not gay, OR female and not straight.
| `.collectible`, `.marker`, `.targetmarker` | Lets you access collectible and marker data of a different character. See the corresponding general variable descriptions above for details. |
| `.tag.tag_name` | `true` if the player has the tag `tag_name`, `false` otherwise. |
| `.costume`      | The ID of the player's alternate costume/skin, or `default` if no alternate costume is worn. |
| `.size`         | The player's breast or penis size depending on the gender (`small`, `medium`, or `large`). |
| `.intelligence` | The player's AI intelligence. For the human player, this always expands to `average`. |
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
| `.cards`        | Works the same as `~cards~`, but for a specific player. Be sure not to try to use it on a player that hasn't swapped yet. |
| `.timeinstage`  | How many rounds the player has spent in the same stage; see the Time in Stage condition for more details about how this works.
| `.timer`        | How many ticks the player has left before they finish masturbating. This is equal to their stamina if they're still in the game, as well as when their Start Masturbating dialogue is playing. On the other hand, this is (naturally) equal to 0 when and after they finish.
| `.ticksinstage` | Counts how many ticks has spent in the same stage; this is similar to Time in Stage, but is incremented whenever forfeit timers are ticked (or whenever they would otherwise be ticked, for players that are not masturbating). The loser of a round's Ticks in Stage is 0 when their Stripped or Start Masturbating cases are played; in general, when a player is masturbating, the sum of their current forfeit timer and ticks in stage values should equal their stamina. Like Time in Stage, when a player finishes, this is not reset to 0 until immediately _after_ the Finished cases have played.
| `.heavy` | Expands to `true` if the player is currently heavily masturbating, and `false` otherwise. Note that this expands to `false` when a character is finishing. |

### `.ifplural` 

The `.ifplural` variables, available as `~cards.ifplural(|)~` and `~clothing.ifplural(|)~`
allow you to make your dialogue gramatically correct, according to whether or not
the referenced item is plural or not.

The syntax for these variables are: `~variable.ifplural(dialogue if plural|dialogue if singular)~`.

For example, the dialogue `I have ~cards.ifplural(several cards|one card)~.` will expand to:
 - "I have one card." if the character has exactly one card.
 - "I have several cards." if the character has more than one card.

However, if you're trying to do something like `~clothing~` followed by `~clothing.ifplural(s|)~`,
you should use `~clothing.toplural~` instead.

### `.ifmale` 

`~player.ifmale(|)~` works similarly to `.ifplural` (described above), but works
based on the referenced player's gender.

The syntax for this variable is: `~player.ifmale(dialogue if male|dialogue if female)~`.
