# New Developer FAQs

Common questions from fresh devs

---

## General

### Q: Do I need to know how to program to add characters?

A: No, not at all! All of the character file formatting is handled by the Character Editor. That said, having experience with algorithms, if/then statements, and general decision trees will help with many advanced CE features.

### Q: Do I need to install software to make this work?

A: Yes, though the softwares themselves are quite light, there is a bit of finesse involved to make life easy for yourself. See the [Character Development Guide](/docs/basics/chardevguide.html) for more info.

### Q: What determines the criteria for adding a character to the Testing Roster? The core game?

A: [Official Testing Requirements are available here.](/docs/policy/testing.html) Once you feel you've been on Testing long enough, [you can apply for sponsorship and admittance into the Main Roster.](/docs/policy/sponsorship.html)

### Q: I want to do [something] with my character, but I have no idea how to go about it!

A: Any time you're stumped as to what to do, check out a character you like and see how they do things. View their files in their `opponents` folder or crack them open in the CE. Many characters include their Kisekae codes in the repository, letting you break them apart and see how they work. Remember the old adage: "Good artists steal, great artists steal from Meia."

### Q: I've made my character. How do I know everything's gonna work like it should?

A: Playtest, playtest, playtest! Find all the obvious bugs yourself before the players do, and get a sense of how the character performs in-game.

### Q: There's a character on the roster that isn't getting updated, and I'd like to write for them. Can I adopt them?

A: Maybe! In order to adopt a character, you need the permission of the original author. If you cannot contact the original author, or if two months pass without a response from the original author, then you are free to take over that character. Note that Original Characters cannot be adopted.

### Q: Is it possible to make a transgender or non-binary character?

A: Unfortunately not. When this game was first created it only supported male/female binary options, and attempting to alter that would break too many existing characters. While many of the developers would like to be able to cater to our trans and non-binary players, it simply isn't feasible now or in the foreseeable future.

### Q: Can I make an OC for my first character?

A: While there are no rules currently prohibiting it, we don't reccomend starting out with an OC as your very first character. This is because it effectively doubles the workload for a new dev, needing to learn both proper SPNatI development *and* design an appealing character. It's also more difficult to get people excited for a character they don't know over a familiar face. We're not opposed to OCs, but new creators giving themselves uphill battles at the outset has too often ended in unhappiness.

### Q: Do I need to join the Discord?

A: At some point you'll want to join the SPNatI Development Discord server. It's the best place to troubleshoot technical issues or get feedback on dialogue or art. You'll also be able to submit updates directly using the **SPNATI_Utilties bot**, so you won't need to bother with any tricky Git operations. Plus, you get to meet fellow smut-smiths. What's not to like?

### Q: What if I have a question that isn't answered here?

A: The answer might be in one of the other docs! If not, then your best bet is to ask other writers or artists on the Development Discord. We're always ready to help new creators!


## Writing

### Q: How do I make my character interact with the other characters?

A: Creators are only allowed to write lines for their own characters, or characters they have permission for. If you want interactions with your character, your best bet is to talk with the current writer of the character. Note that not every character is under active development, but it never hurts to ask! You can also write lines on your own and upload them, letting the other author respond at his or her leisure.

### Q: I want to target X character, but I don't know the first thing about them.

A: Every character has their own channel on the SPNatI Development Discord server, and many have a post with targetable qualities pinned. Give those a read if you're stumped. Otherwise, you could try asking a creator directly on the server or through DMs. Lastly, if all else fails, you can always target based on what you see before you when playing the character.

### Q: If I want to write conversations with a character, what's the best way to go about it mechanically?

A: The most popular way is to write **targeted dialogue** ("targets") to another character in your character's "Opponent Must Strip", "Opponent Stripping", and "Opponent Stripped" cases. This is practical, as that character will be the center of attention in-game, and you can easily target that character and their status in the Character Editor. Simply make a new case in the CE (it's best to start with Opponent Must Strip), choose that character as a `target`, and choose what `stage` the target is in (to target a single stage, you only need to select a stage in the `From:` section). This also makes it easy for the other character's author to know how to direct their responses.

### Q: There's what appears to be a blank marker at the top of my markers list?

A: Yeah IDK how that happens either. Feel free to ignore it or delete it.

### Q: How many lines should I have before tossing my character onto Testing?

A: The minimum is 300, as seen on the [testing policy guide](/docs/policy/testing.html). You'll want to avoid having too many more than that, as it can cause potential re-writes to be too daunting of an undertaking.

### Q: How many hand/masturbating (self)/masturbating (other)/finished/ etc. lines do I need?

A: Always, more than you have. You can never have too many of these lines, so don't be afraid to pile them on. Test your character for yourself, and see if you notice a ton of repetition anywhere.

### Q: What is the "weight" of a line?

A: **Weight** is odds of a line playing, by default `1`. Higher than `1` means more likely to play, lower means less likely. So if you have one line at weight `1`, and one at weight `2`, the second line will play twice as often as the first line. All lines of the same **priority** are weighted against one another.

### Q: This character had a great line just now! How do I respond to it?

A: It's better to avoid responding to generic dialogue, as it likely won't always play when you want it to, and it'll make it harder for the other person's character to respond. If you want to target characters, check the CE's Writing Aid for possible prompts. Otherwise, coordinate back and forth targets with the character's author.

### Q: Can I/should I write dialogue in the "Dealing Cards" case?

A: No. The "Dealing Cards" case is used for certain advanced situations. If you're using the "New Dev FAQ", odds are you shouldn't be writing in it.

### Q: Is there a list of things worth targeting?

A: The **Writing Aid** serves this function. This is part of the Character Editor, and you can find it among the tabs on the left-hand side. This contains a list of interesting or important situations, "called out" by the characters' authors.

### Q: What constitutes a "good" line?

A: A proper guide to generic and targeted dialogue is beyond the scope of this FAQ, and that's not taking into account people's differing opinions. However, as a general rule of thumb, you can consider any line that reveals character, is funny, is sexy, or all of the above to be a good line. If no one but your character could have said it, it's a good line.


## Art

### Q: Do I need to have any artistic skills to make a character?

A: You don't need to be an artist! All our models are created in Kisekae, a "paper doll" program. [A basic guide is available here](/docs/kkl/kisekae_guide.html). Kisekae has a lot of little tricks that can make your models stand out, so if you'd like direct advice from experienced members, feel free to ask on the Development Discord server!

### Q: What's the difference between Kisekae and KKL?

A: Kisekae was made by Pochi, as a paperdoll dress-up program in the style of K-On. It is available online at [Pochi's site](http://pochi.lix.jp/k_kisekae2.html). KKL, or "Kisekae Local", is the version of Kisekae specifically modded for SPNatI work. It has way more slots for ribbons, belts, and hairpieces. Because of this, KKL codes are not compatible with Kisekae Online.

### Q: Every time I load my code, my image attachments are gone!

A: In order for Kisekae to automatically load image attachments, they need to be in a folder labelled `images` in the same directory as kkl.exe. The file names of the attachments also need to not contain spaces; only letters, numbers, and underscores are allowed.

### Q: I can't export poses using the Character Editor. What do I do?

A: Using the Pose Matrix in the Character Editor is the best method of creating sprites for your character, but it's possible that process isn't available to you. Instead, you can export directly out of Kisekae. Set your export quality to `5/5`, and zoom to `7`. Make sure background transparency is turned on and export as a .png. You'll have to then center and crop your image in an image editor.

### Q: What are the default sizes for character sprites? What the largest I can make my sprites?

A: 600x1400 is the default for character sprites. About 1200 px is the max width. Anything smaller than 1400 px will be stretched to fit, while sprites larger than 1400 px will squished.

### Q: I made my character too big in their sprites. What do I do (besides remake every image)?

A: Open the character up in the Character Editor and open the `Advanced` tab. In there should be a setting labelled **Scale Factor**. Adjust it down (or up) from 100 to manipulate the size of all your sprites at once.

### Q: How do I turn around a character in Kisekae? I want to see their butt.

A: Unfortunately, Kisekae does not have an option to turn the characters around. All of the "butt poses" in the game were made by the characters' artists from scratch. You can give your characters a butt pose as well, either in Kisekae with the creative use of belts and ribbons or in an image editor such as Photoshop, Gimp, or Paint.NET.


## Technical

### Q: I can't make new sprites or save my character! Question mark!?

A: Most commonly, this is because your character folder is "locked". To unlock in Mac OSX, right click and select "get info" (or hit CMD-i). In the screen that pops up, uncheck the "locked" setting. In Windows, Right click your character folder and ensure that the "Attributes: Read-only" setting is unchecked. If you are unable to switch off "Read-only", this is most likely due to your antivirus settings. Try adding "SPNatI Character Editor" as an exception. It's possible this only happens with Norton Antivirus.

### Q: I made changes to my character, or downloaded a new version, but when I launch SPNatI via offline_start.exe they don't show up.

A: Offline_start.exe has issues with cacheing, specifically the cache is not cleared automatically. To fix this, manually clear your browser's cache. Or use the no_buggers workaround to allow Firefox to access local files directly.

### Q: Why isn't my WIP character showing up in my offline version of the game, despite clearing my cache?

A: To allow a character to be selectable in the game, they need to be on `listing.xml`. This file is found in the `spnati/opponents` folder. Simply copy a line from another character and copy it in, replacing the other character's name with your character's folder name.

### Q: I downloaded the .zip file from GitGud, but none of the images, .exes, or .zip files work!

A: Long story short, SPNatI uses something called `LFS` to keep file sizes down. That unfortunately means the zip file download from GitGud doesn't work. Instead [use Github Desktop to keep your repo up to date](/docs/basics/githubdesktop.html).

### Q: I can't pull updates from Gitgud! I'm seeing merge conflicts with `config.xml` and/or `listing.xml`!

A: Those files often get changed upstream (meaning, in the main repository). When git sees you've made changes, it needs you to tell it whether to keep your version or pull the changed version. Since they're easy to edit, it's usually safest to simply discard your changes to listing.xml and then pull. Afterwards you’ll want to re-add your character to listing.xml. 

### Q: I'm trying to download new updates through GitHub Desktop. It's saying there are "Conflicts" and asking me if I want to Stash Changes? Should I stash changes?

A: Nope. Do not. Nooooo. That is just going to create headaches fo you down the road. Odds are your own character's files haven't been touched, since only someone with your character's role could update them. Most likely, it's just `config.xml` and `listing.xml`, like above. Those aren't worth stashing, so follow the steps above to discard your changes.

### Q: How do I revert a changed file back to how it should be?

A: To restore a file changed on your local repository to match how it should be on the main SPNatI repo, you'll need to discard your changes. In GitHub Desktop, right click the offending file and select "discard changes".

### Q: Does my character have bugs? How can I check?

A: The validator tab in the CE will list out any issues that arise with your character. Solving these will be necessary to move your character onto Testing and the Main Game. Double click on an issue in the validator to be taken directly to the offending case or line.

### Q: Is there a way to download the Offline version or run the Character Editor on mobile?

A: Unfortunately, not at this time.

### Q: How can I give my character a custom in-game feedback message?

A: Characters can receive anonymous feedback from players in-game, this is enabled by default but can be turned off. To change the feedback message, a user with update permissions must type in the character's channel on the Developer Discord server `b!feedback message [Message Here]`.

### Q: How do I update my character using the bot?

A: Once your character is on the Testing Roster, a channel on the Development Discord Server will be made for them, along with a role for that character. If you have a character's role, you can use the **SPNATI_Utilites bot** to submit updates as Merge Requests. Post (or DM the bot) `b!update [character name] [update description]` and attach a .zip file containing **only the files that have been changed**. Usually, this will be your `behaviour.xml` file, your `meta.xml` file, and your `editor.xml` file. The bot will tell you if your update request has been successfully submitted, or if there was an error.

### Q: Can I push my changes directly to the main SPNatI repository?

A: Only SPNatI Game Moderators can push changes directly to the repo, in order to prevent abuse. Otherwise, all updates will be submitted as "Merge Requests". The game mods will look over your update to make sure everything is A-OK, and add it to the game if so. Don't worry, so long as you're not trying to delete all of Remilia's files, your update is likely to pass muster.
