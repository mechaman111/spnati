# Style Guide and Dialogue Formatting Syntax

When and How to Style Your Character's Text

---

## Style Guide

To maintain a consistent style across the game, all characters should adhere to some basic dialogue guidelines.

- _Use proper spelling and punctuation._ You might sometimes choose to misspell specific words intentionally if your character pronounces things oddly. Punctuation is less flexible and errors should be corrected where reported.
- _Pay attention to your character's grammar._ Not every character will have perfect grammar, but no grammar errors should be accidental.
- Use _italics_ for emphasis, not **bold** or CAPITALS.
- _Use "okay" or "OK" but not "ok"._
- _Characters should use American English spelling._ An exception can be made for British characters that use British English spelling. This depends on the identity of the character not the identity of her author.
- _Internal monologue should be in parentheses and italicized:_ _(Like this.)_ As of January 2021, the game's internal monologue lines were split with about half with italicized parentheses and half without. Italicizing all of these won't be enforced, but we recommend coming to this unified standard.
- Some authors have experimented making text smaller to fit more in for long-winded or very tall characters. We may have a formal standard in the future for which size and line spacing to choose, but, if you try this in the meantime, try to imitate how others have done it. Note that doing this may make your text too small to read on handheld devices.
- _Use of text styling in dialogue should be informative, not decorative._ With the exception of italics, you should use the methods details in the sections below sparingly. In epilogues or in non-dialogue situations like Komi-san's notebook or narrator panels, there are no limitations.

## Formatting

Two basic methods can be used to apply special formatting to dialogue within SPNATI:

 - HTML tags
 - Style specifiers

### HTML Tags

HTML tags are more straightforward and should be more familiar to most writers.
However, for technical reasons they are also fairly limited.

Examples:

```html
<i>italicized text</i>

One line<br>Two lines

This text has<hr>horizontal lines<hr>that separate it
```

| Tag    | Description
| ------ | -----------
| `<i>`  | Applies _italics_.
| `<br>` | Inserts a linebreak.
| `<hr>` | Starts a new line of text, and also divides those lines using a horizontal rule (line).

### Style Specifiers

To apply more complex and richer formatting, you can use _style specifiers_.

These can be used within dialogue as follows:
```
{mystyle} This text is styled.

{styleA} This text uses {styleB} two different styles

{styleC} This text starts off styled {!reset} then returns to normal

{styleE styleF} You can also apply multiple styles at once to text
```

The text within the `{curly braces}` indicates what styles to apply to the following text.
Multiple styles can be specified by separating them with spaces.

Custom styles can be created per-character using CSS, to produce unique text effects.
_(TODO: document this?)_

SPNATI includes several pre-packaged styles that can be used without any custom setup, however,
covering a few common text effects and formatting.

| Specifier     | Description
| ------------- | -----------
| `{!reset}`    | Resets all following text to the default style.
| `{i}`         | Applies _italics_, identically to `<i>` tags. Included for completeness.
| `{b}`         | Applies **boldface**.
| `{u}`         | Applies underlining.
| `{s}`         | Applies ~~strikethrough~~.
| `{highlight}` | Surrounds text with a yellow highlight.
| `{mono}`      | Displays text with a `monospaced font` (does not change text background).
| `{small}`     | Makes text small.
| `{big}`       | Makes text big. 