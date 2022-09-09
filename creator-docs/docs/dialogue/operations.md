# Dialogue Operations

How to make your dialogue do more than just display text

---

"Dialogue Operations" are a catch-all term for actions that are associated with individual lines of dialogue.

For dialogue in regular (non-hidden) cases, these actions take place whenever the associated line is 'played' (i.e. displayed to the player). In general, operations associated with regular dialogue lines take effect immediately _after_ the associated text is displayed; for example, if you change your character's `gender` attribute in a Stripping line, the change will only take effect starting with the following After Stripping line. Changes to a character's `label` are an exception, however (see below for details).

If a dialogue case is _hidden,_ however, the actions associated with each line in the case take effect whenever the conditions for said case are met. For now, this happens at some point before dialogue is displayed, but exactly _when_ this happens is undefined (so it could happen before or after conditions for other cases are checked). This will change in the future, however.

In the `behaviour.xml` file, these operations are collected inside of an `<operations>` element within each `<state>`.

## Player Attribute Operations

Player attribute operations change the core attributes of a player. In XML, these are represented as `<player>` elements within the `<operations>` element.

| Attribute | Meaning |
|-----------|---------|
| `intelligence` | The difficulty setting of the character's AI. |
| `label` | The name that is displayed above the character's cards, and the name that other characters will use for this character by default. Unlike other operations, changes to a character's `label` are processed _before_ dialogue is displayed, so other characters will begin using the new `label` in their dialogue immediately (instead of only using the new name starting from their next lines). |
| `size` | The size of this character's breasts or penis. |
| `gender` | The in-game gender of this character. Characters such as Chihiro use this to implement their gender reveal mechanics. |

## Forfeit Operations

Forfeit operations change various aspects of a character's forfeit. In XML, these are represented as `<forfeit>` elements within the `<operations>` element.

For obvious reasons, none of these operations will have any effect after a character has finished.

### Stamina and Forfeit Timer

A character's `stamina` attribute controls how long the character's forfeit will last once they start. This attribute can only be changed _before_ a character's forfeit starts.

_During_ a character's forfeit, however, the `timer` attribute determines how long they have left before finishing; this attribute can only be changed while a character is forfeiting, beginning with the Start Masturbating (Self) case.

Both of these attributes can be either directly set to specific values, or can be changed with basic arithmetic operations. Note that the lowest possible value for both attributes is 1: setting the `timer` attribute to 1 will cause them to finish with the very next button click, and setting the `stamina` attribute to 1 will likewise cause their forfeit to finish immediately after it begins.

### Finishing Dialogue Redirection

The `redirect-finish` attribute can be used to 'redirect' the dialogue that plays when a character is finishing, so that another character's dialogue is displayed in place of the finishing character's own dialogue.

The `redirect-finish` attribute itself takes a reference to a character as its value: this can be a character ID, a placeholder such as `target` or `winner`, or a bound variable from a case condition. This works identically to how player variables are expanded in tests and within dialogue, but without the enclosing tildes (i.e. `target`, not `~target~`). `self` can also be used to undo any previously-set redirection, so that a character will play their own dialogue while finishing.

For the sake of clarity, in the this section the _initiator_ refers to the the character whose dialogue is being redirected; in other words, this is the character that actually has the `redirect-finish` operation in their dialogue.

On the other hand, the _receiver_ refers to whoever will be playing finishing dialogue in the place of the initiator; in other words, this is the character that is referred to within the `redirect-finish` operation.

The `redirect-finish` operation must be applied _before_ the initiating character finishes; once the initiator finishes, instead of them playing a line from the Finishing (Self) case as usual, the receiver will instead play a line from a special Opponent Finishing case, targeted towards the initiator. This is the only time dialogue from this case type will be used.

Afterwards, the initiator, receiver, and other characters at the table will play Finished cases as usual.

### Heavy Masturbation Manipulation

In addition to changing timers and finishing dialogue redirection, forfeit operations can also affect whether or not a character is heavily masturbating.

Operations affecting heavy masturbation are always applied after operations that change forfeit timers; this ensures that operations always use the correct timer values when resetting heavy masturbation status.

These operations use the `heavy` attribute in XML.

| Value | Meaning |
|-------|---------|
| `true` | Force this character into heavy masturbation, and have them stay in heavy masturbation until they finish (or until changed by another forfeit operation). |
| `false` | Force this character out of heavy masturbation, and lock them into regular masturbation until they finish or another forfeit operation changes their status. |
| `reset` | Reset this character's heavy masturbation status; the character's heavy masturbation status will be determined using the regular rules, and will change as usual once they get close to finishing. |

### Joint Forfeits

One of the main use-cases for forfeit operations is joint forfeits (though they're not the only possible use-case, of course).

With that in mind, though, how do you put this all together?

Generally speaking, this is the envisioned way to do things:

- One character begins a joint forfeit with another character, while one or both of them are masturbating.
- One of these characters clears their dialogue and displays a blank image (using Global cases), and the other character uses special poses and text to make it appear as if both characters are in the same position at the table.
- The character that is currently masturbating (or both characters, if applicable) should use forfeit operations to reset their timer(s) to a known value, as well as reset their heavy masturbation status. This isn't _strictly_ necessary, but it helps ensure that the joint forfeit doesn't end too quickly.
- In addition, the character that is out of their normal position (and using blank poses/text) should use a `redirect-finish` operation to mark the other character as handling their finishing dialogue.
- When the out-of-position character finishes, the other character should have a targeted Opponent Finishing case to display dialogue for them.

Note that although one character must be in their forfeit stage for this to work, the other character can be in any stage: a joint forfeit can start while both characters are masturbating, or even while one character is still in the game.

## Nickname Operations 

Nickname operations modify the list of possible nicknames a character will use to refer to another character when using dialogue variables (such as `~name~`). 

These operations take a reference to a player (a character ID, a placeholder such as `target` or `winner`, or a bound variable from a case condition) in the `character` XML attribute, a nickname value in the `name` XML attribute, and an operation in the `op` attribute. The referenced player can also be set to `*`, to set default nicknames for all characters that don't have more specific ones set.

If multiple nicknames are set for a given character, the game will randomly select one to use each time a variable is expanded. A nickname can appear multiple times in the nickname list as a form of weighting; for convenience, operations (except for `clear` and `=`) can take an optional `weight` attribute to help manipulate nickname weights (see below for details).

Nickname operations will clear any per-target `nickname` marker set for the referenced character; however, if the `nickname` marker is re-set subsequent to the operation, it will take priority over the nickname list derived from operations.

| Operation | Meaning |
|-----------|---------|
| `clear`   | Clear the nickname list for the referenced character, so that this character uses the referenced character's `label` attribute when naming them. |
| `=`       | Sets the nickname for the referenced character. This clears out previously added nicknames from the list, so that the nickname set here is the only one used for the referenced character. Using this operation with a `name` that resolves to an empty string is equivalent to a `clear` operation. |
| `>`       | Sets the weight of a given nickname. This is equivalent to removing all preexisting instances of the nickname from the list, then (optionally) re-adding the same nickname. |
| `+`       | Adds a new nickname for the referenced character, or increases a nickname's weight if already in use. This operation can add a nickname to the list multiple times to adjust how frequently it is used. |
| `-`       | Removes one or more instances of the given nickname that are currently in use for the referenced character. |

All operations except for `clear` and `=` can optionally specify a `weight` XML attribute to manipulate nickname weights:
- `+` operations will increase the weight of a nickname by the given amount by adding `weight` instances of a nickname to the list.
- Conversely, `-` operations will remove `weight` instances of a nickname from the list, thereby decreasing a that nickname's weight (if not removing it entirely from the list of possible nicknames).
- `>` operations will reset a nickname's weight by removing all instances of the given nickname from the list, then re-adding `weight` new instances of it. This can be used to remove a nickname from use entirely by specifying a weight of `0`.

Nickname operations are applied in the order they're listed in the above table, starting with `clear` operations.
Among other things, this ensures that dialogue can use `clear` or `=` operations to clear out the nickname list _and_ repopulate it with multiple nicknames in a single line, without relying on operations to be ordered correctly in XML.
