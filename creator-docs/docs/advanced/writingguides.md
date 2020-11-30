# Adding and Editing SPNatI Documentation

A Guide For Writing Guides

---

## Intro

Do you have an idea for a new guide or info sheet to be added to **Documentation Night at the Inventory**? (That's this.) We'd love to read it! 

Anyone can submit their documentation to the repository. The Docs Repo uses [MkDocs](https://www.mkdocs.org/) to build all the pages. Formatting is handled with [Markdown](https://daringfireball.net/projects/markdown/), similar to Reddit and Discord. This makes it easy for additional pages to be added.

The guides are hosted as part of the `spnati` Git repository (AKA the SPNatI Repo, the folder downloaded from Gitgud), in the [`creator-docs` folder](https://gitgud.io/spnati/spnati/-/tree/master/creator-docs).

In `creator-docs` should be three items:

 - A `docs` folder, where the documentation pages live.
 - `mkdocs.yml`, the configuration and settings file for the docs repo.
 - A `cinder` folder containing the custom CSS (you shouldn't need to touch this).

!!!note
	MkDocs only "builds" the `.md` files into `.html` files when it is deployed. This means that you cannot easily preview the appearance of your doc before it is added to the site. This is only possible by [downloading MkDocs](https://www.mkdocs.org/) and running the `mkdocs serve` command (recommended for advanced users only).

## Adding New Guides

Adding a new guide is easy. Simply write it up as a `.txt` document. MkDocs uses markdown to format its text, see below for a guide.

Once your guide is written, you'll want to change the guide from a `.txt` extension to `.md`.

Navigate to the `creator-docs/docs` folder in the `spnati` repository . Each folder there represents a different "directory". User-submitted guides should go in the `userdocs` folder. Copy your .md file into there.

Open the `mkdocs.yml` file in Notepad, TextEdit, or any word processor. Inside, under the `nav:` section, you should see something like:

        - 'User Submitted':
            - 'Pose Organization': 'userdocs/pose_organization.md'

Add the path to your doc under the last guide on the list, taking care to keep the formatting consistent.

        - 'User Submitted':
            - 'Pose Organization': 'userdocs/pose_organization.md'
            - 'My New Guide': 'userdocs/myguide.md'

!!! warning
	Only use legal characters in your doc file names, e.g. letters, numbers, underscores and dashes. Avoid spaces and special characters.

Save .mkdocs.yml. You can now submit your changes as a merge request to the SPNatI repo on Gitgud. [Follow the directions here to do so.](githubmerge.html)

If all that seems too complicated, there's always the tried and true method: give it to a mod and make them figure it out.

## Editing Guides

There are a few ways to edit guides. If you find a mistake or typo, don't be afraid to submit a fix.

### Offline Repo

Open up the `.md` file you want to edit in Notepad, TextEdit, or any word processor. Make your changes and save. [Submit as a merge request](githubmerge.html).

### Git Page

[You can edit documentation directly on Gitgud.](https://gitgud.io/spnati/spnati/-/tree/master/creator-docs) This will copy the doc to a fork and submit your changes as a merge request, so you'll need to have a Gitgud account to do so.

On the Git page, navigate to the `.md` file you wish to edit. It'll show the file with proper formatting for markdown. To edit, find the **Edit** button up top.

![Edit Button Location](../img/writingguides01.png)

Click it, and you'll be taken to a version of the page you can directly edit. Make your changes, then hit the green "Commit changes" button at the bottom. That's it!

![Commit Button Location](../img/writingguides02.png "Press me")

## Formatting

### How to use

MkDocs uses markdown for formatting, which you should be familiar with if you use Discord or Reddit.

|Style|Code|Result|
|-----|----|------|
|Italics|`*Example*`|*Example*|
|Bold|`**Example**`|**Example**|
|Code|`` `Example` ``|`Example`|
|Link|`[Example](https://spnati.net/)`|[Example](https://spnati.net/)|

**Tables** (like the one above) can be built like so:

```
|AAAAA|BBBBB|CCCCC|
|-----|-----|-----|
|One|Two|Three|
```

**Code blocks** (like the one above) can be built with:

 - Four spaces before each line
 - One tab before each line
 - Three back ticks (`` ``` ``) at the beginning and end.

**Lists** (like the one above!) can be built with:

```
 - One
 - Two
 - Three
```

OR

```
 * One
 * Two
 * Three
```

You can make numbered lists: 

```
 1. One
 2. Two
 3. Three
```

**Block quotes** are made as such:

```
> Paragraph One
> 
> Paragraph Two
```

**Images** are similar to links, but with an `!` in front. Note that the file path starts `../`, because MkDocs uses relative links, you need to use the two periods to tell it to go back a directory, then find the `img` folder.

```
![Alt Text](../img/writingguides01.png)
```

!!! note
    You can make note tabs like so.

        !!!note
            Note.

!!! warning
    More powerful than a note.

        !!!warning
            Warning!

!!! danger
    The most powerful Admonition. Please don't overuse.

        !!!danger
            Holy fuckin' shit.

**Line Breaks** are done with `---`.

**Headers** are made with the `#` sign. More `#` means a smaller header. Note that first through third level headers appear in the side bar to the left.

```
# First Level Header

## Second Level Header

### Third Level Header

#### Fourth Level Header

##### Fifth Level Header

###### Sixth Level Header
```

### First Header

By default, the pages here are set to center the first top-level header, as well as the first line of default text. This means your pages should always have a title, formatted like so:

```
# Guide Title

A short description of the guide.

---

Rest Of Doc Here
```

!!! Note
	If you don't format your beginning this way, your first paragraph of text will be centered. Be sure to format your docs properly!