1. Make sure you've downloaded the SPNATI source from the GitLab repository.
2. Follow the guide at https://www.reddit.com/r/spnati/comments/4zbo34/character_creation_scripts_and_help/ to create and export images for your character. Place these images in a new folder under the opponents folder where you downloaded the SPNATI source.
   a. Make sure you follow a consistent naming scheme with your images (0-happy, 1-happy, 2-happy, etc.), since this is what the editor will you when figuring out which images are available for each stage.
3. Run SPNATI Character Editor.exe
4. If this is your first time, you will be asked to browse to the SPNATI Repository. Navigate to where you downloaded the files and select the root folder (the one that contains css, fonts, js, opponents, etc.)
5. You should now be in the main editor. Go to File > New and locate your character in the list. This will be the name of the folder where you placed your images, assuming you followed step #2 correctly.
6. You are now ready to edit your character!

Basic Steps to Get Your Character Into the Game (offline only)
----------
1. Fill out the metadata tab. The vital important part is the label (what other characters call yours in game), and the default portrait (what is used in the game for character selection)
2. Fill out the wardrobe tab. This describes what clothes the character is wearing and the order they will be removed. The last layer to remove in the game should be the listed first, with the first item to remove listed last. You can reorder layers with the up and down arrows.
3. Go to the Dialogue tab. It will fill in defaults for every required line in the game. Don't touch anything else for now. Save your character using File > Save
4. Back on the Metadata tab, click Add to Listing to make your character available for selection in game.
5. At this point, your character is ready to use in game (though with placeholder dialogue). Verify that they appear by opening index.html form your spnati directory.
6. Back in the editor, fill out the dialogue to your heart's content.

Basic Dialogue 
-----------
Your character's dialogue is split into different stages (states of undress) and cases (phases within the game: exchanging cards, stripping, reacting to another player stripping, etc.) Each stage has one or more case for every phase of the game.
Every case contains one or more lines of dialogue and their corresponding images.

=Dialogue Tree=
On the left of the dialogue tab is a dialogue "tree" containing each state of undress and its associated cases. Expand a stage and click a case to edit its dialogue. This enables a whole plethora of options pertaining to the case.
Add Case - this will add another case of the same tag as the currently selected case. This new case will by default only apply to the current stage. You will use this to add stage-specific cases, or targeted cases.

=Edit screen=
* Tag: The game phase the case applies to. This is the same as what's in the tree, but if you want to move dialogue to a different phase, you can change the tag here.
* Applies to stages: This is a series of checkboxes per clothing stage that the case belongs to. You can check multiple boxes to have the dialogue appear in multiple stages, avoiding the need to copy and paste dialogue everywhere. The case will appear in the tree under every stage checked, and editing dialogue or conditions in one stage will automatically update all the other linked stages.
* Conditions: This allows you to target dialogue. By default, lines for a phase are chosen randomly from all cases meeting the character's current stage. You can narrow down specific lines of dialogue to play only under certain conditions (ex. Blake is another player in the game, the character who lost is Rosalina, etc.)
** Target tab: This controls targeting dialogue to the player who is the target of the current phase (ex. the player who is stripping, the player who won the game, etc.)
** NOTE: This tab is only available for certain cases (i.e. those that have a target)
*** Player: The folder name of the target player. For example, filling in Rosalina will limit the line to only when Rosalina is the target
*** At stage: The line will only play if the target player is at the chosen state of undress (ex. Rosalina has just removed her dress)
*** Hand: The line will only play if the target player has a specific poker hand
*** Tag: The line will only play if the target player has the given "tag." Tags are free text fields filled in on the metadata tag, and are useful for targeting general characteristics (ex. the target player is shaved)

==NOTES ABOUT CONDITIONS==
Targeted dialogue takes priority over non-targeted dialogue. If a targeted line meets its conditions, it will ALWAYS play in favor of a non-targeted line. Different conditions have different priorities, and one type of target will ALWAYS play in favor of another type. Cases are sorted by this priority in the tree, so whatever is on top is the case that will take priority if its targets are met.

** Other Player tab: This controls targeting dialogue when a certain player is also playing (but is not the target, if there is one)
** The options are the same as the Target tab, but apply to the character in question rather than the target

** Misc tab: This controls miscellaneous targets (the character's own poker hand, how many females are playing, etc.)



Testing Tool
------------
The editor provides a game simulator of sorts via the Test button in the toolbar. You can use this to quickly test dialogue without having to go through the game. You can choose which 4 characters are playing, their current state of undress, their hand, the current game phase, and who is being targeted for that phase. This allows you to test specific targeted dialogue without relying on luck that you will run into that situation inside the game.
