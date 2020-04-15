# Dialogue Formatting Syntax

Two basic methods can be used to apply special formatting to dialogue within SPNATI:
 - HTML tags, and
 - Style specifiers

## HTML Tags

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

## Style Specifiers

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