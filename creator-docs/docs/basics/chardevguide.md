# Character Development Guide for SPNatI

Do you want to make a character? Then start here!

---

**Game link:** **[https://spnati.net/](https://spnati.net/)**

This guide will be considered a "getting started" launchpad you can use to move from fantasy to finalized character. 

---

## Glossary

**KKL/Kisekae**: This is our modeling software.

**Repo**: The Git repository, found [here](https://gitgud.io/spnati/spnati). This often refers to the master branch, which can be considered the live game.

**Intelligence**: This is the code that determines how statistically savvy a character is. In other words, how often they’re likely to lose. *Please* bear in mind that the gulf between bad and average, and average and good, is immense. Good is a lot stronger than you think it is, and best is very punishing. Throw implies the character has given up and is ready to forfeit.  

**Tags**: Tags are values that describe your character in various ways. These can change as the game progresses.

**Markers**: Unlike tags, markers are values that flag your character as having said certain dialogue throughout the game. A guide to markers can be found later. For the novice, consider these highlightable events for either your own character to avoid repetition, or to signal to other characters to respond.

**XML**: For practical purposes, this refers to `behaviour.xml`, a file containing your character’s dialogue, soul, and general playability. Consider this the brain behind your model. 

---

## Getting Started

### **1.** Play the game a lot.

No, seriously. If you’re interested in adding content, that’s always great! But having a good working knowledge of what various effects and interactions look like in practice will help you drill down into the guts and develop your own characters.

### **2.** Download the Git Repository.

If you’re planning on developing content for the game, it is enormously helpful to download a copy of the [Git repository](https://gitgud.io/spnati/spnati), even if you yourself don’t want to use Git.

We recommend using [Github Desktop](https://desktop.github.com/) to keep your local copy of the game up-to-date at all times, without having to re-download the full game. [Here's a simple guide to set up Github Desktop.](/docs/basics/githubdesktop.html) Trust me: spending the 10 minutes to set it up (plus an hour or so to let it download) is very much worth it.

If you already have Github Desktop installed, you can just add a new project and include the master branch of the Gitgud repo to resume updating normally.

!!! note
	Git has advanced features beyond what's available through Github Desktop. These features are not necessary for regular SPNatI character development work, however they might prove useful should errors or mistakes arise. You can run these commands through a **Command Line Interface (CLI)** such as cmd.exe or Terminal.app. A simple guide to Git commands [is available here](http://rogerdudler.github.io/git-guide/). A far more in-depth guide [can be seen here](https://git-scm.com/book/en/v2).

### **3.** Open the following from the “Tools” folder:

 * **KKL**: This is our image generating software, and helps us keep a consistent style. Please note this is different than Online Kisekae, which is not currently supported. The most recent offline version of Kisekae can be found in the "tools" folder within the Git Repository download. 

 * **Character Editor**: Thanks to the hard work of /u/spnati_edit and ReformCopyright, we have a supplemental software that *greatly* simplifies the character creation and editing process.

It's best to unzip these tools to a folder outside the `spnati` repository. That way you won't fill up the "Changes" list on GitHub Desktop, and you won't lose valuable work if Git gets accidentally reset.

!!! note
	These tools are only designed to work "out of the box" on Windows sytems. MacOS users can use [this guide](/docs/advanced/macguide.html) to make use of SPNatI's developing tools, like the Character Editor. Linux users can follow [this user-submitted guide](/docs/userdocs/linuxguide.html).

### **4.** Software installed! What comes next?

For this guide, we’ll be assuming you'll be tackling your dialogue first, as it’s the larger hurdle to entering the game. Many creators handle art first, however, so feel free to start with whatever aspect of character creations works best for you.

 * When using the Character Editor, you can enter dialogue and cases directly. A breakdown of the Editor's many features can be found in its help files, accessible through the **Help** menu on the top bar. Additionally, [spnati_edit has set up a Youtube channel](https://www.youtube.com/channel/UC3lqu7IuyolqxJAcTLAFpuw) that serves as a one stop shop for information. 

 * While deciding which character you want to develop, it might be a good idea to check out [the Contributor Reference Sheet](https://docs.google.com/spreadsheets/d/e/2PACX-1vRejQBHSnenImBLUEX5qlmxdopG0c_O9uWklerYr6v5yJ00_UUcQiOvqC6t6CUeKsRAkd2769YzA67P/pubhtml#), which lists some of the characters that are currently either finished/semi-finished, or are being worked on, under the "currently being developed" page. It also lists the developer in question, which open up the possibility to contact that developer for potential collaboration. Not all characters in development are listed there, so your most up-to-date source will always be the SPNatI Development Discord server.

 * You have to assign tags to your character based on their appearance and personality traits. Other characters can react to these tags using filtered lines. To check which tags you're supposed to give you character, check out [this guide](/docs/dialogue/tags.html).

 * Game developer Faraway Vision has built [a number of helpful tools on his site](https://spnati.faraway-vision.io/tools/image-import.html). This includes an online Kisekae importer, a Kisekae model disassembler, a code splitter, and more.

### **5.** Tips for Dialogue:

 * The guide to [advanced syntax for game state situational awareness can be found here](/docs/dialogue/conditions.html). Similarly, you can target [backgrounds in depth using the background metadata](/docs/dialogue/backgrounds.html).

 * You can use **[Variables](/docs/dialogue/variables.html)** to create dynamic text that adjusts itself to match the game state.

 * [The guide to markers (and other advanced dialogue features) can be found here.](https://www.youtube.com/watch?v=Yg97qiTGKLc)

 * [You can format your text by following this guide.](/docs/dialogue/formatting.html) You can even make custom text formatting using the **Advanced** tab in the CE. Please don't overuse text effects, however.

 * Once you’ve read all these, consider checking the **“Writing Aid”** tab in the character editor! This includes a lot of scenarios that are “must targets,” or interesting for characters to react to. You can call out your own cases with the "Call Out" button, and view your Call Outs in the **Situations** tab.

 * Use [**Recipes**](/docs/advanced/recipes.html), which are pre-made cases with advanced conditions for targeting particular game states. These are available in the CE's **Tools** menu. You can even make your own and share them.

 * A few other great tips worth mentioning in general: Use minor removal lines to describe stuff about your character’s world, explore other characters in the CE to see how they work, and always reach out for help! 

 * And of course, we highly recommend checking out the "Dialogue" section of these docs!
        
### **6.** Tips for Kisekae and Character Art:

 * [A basic user's guide to Kisekae can be found here](/docs/kkl/kisekae_guide.html).

- A helpful video tutorial by Rinkahbestgirl [is available on Youtube](https://www.youtube.com/watch?v=TMITpq2dfaU).

 - If you can't directly connect KKL with the Character Editor (e.g., you're running on a non-Windows system) [you can follow this guide for creating a local Kisekae server the CE can see](/docs/kkl/kkl_local_protocol.html).

 - There are lots of talented people on the Discord servers who are willing to help you out with anything you're having trouble with. Consider sharing your model there before you work on poses.

 - And of course, the "Art" section of these docs!

### **7.** I filled out my dialogue, and I made a cool model, now what? 

 - [Give your character a collectible!](/docs/advanced/collectibles.html)

 - [You could try your hand at an epilogue](https://www.youtube.com/watch?v=ww-y0VH5z40), though they aren't required and are better served being done *after* submission to the Testing Roster (in case changes to the character model are needed).

 - Apply to the Testing Roster! [The full guide and all requirements are available here.](/docs/policy/testing.html)

 - Once you’ve had your character on test for a while, and have smoothed out the errors, apply for sponsorship. [The guide to the sponsorship process can be found here.](/docs/policy/sponsorship.html)

 - Join us on the [SPNatI.net Development Discord server](https://discord.gg/UVUpQmY) for help, feedback, and camaraderie!