# Character Editor Recipes

How to use them and how to share them


---

Per the [version 5.0 of the Character Editor](https://old.reddit.com/r/spnati/comments/couw2g/character_editor_version_50_and_video_tutorials/) change log:

> Get your dialogue cooking by using recipes!

> Use the flask in the Case Tree to quickly create a case for interesting game situations with the appropriate conditions already filled in.

> Example recipes include: Losing the first hand, first instance of nudity in the game, final hand of the game, and more.

> Create your own recipes using Tools > Manage Recipes. Recipes are stored in %appdata%/SPNATI/Recipes. You can share your own recipes with other creators who merely have to plug your recipe files into that folder for them to become available. Or, if you've cooked up something really special, consider submitting it to become a default recipe in the editor.

## How to use a Recipe:

In the Dialogue tab of the Character Editor, the small flask icon is the Use Recipe button. You will be shown a list of pre-made situations. These are all normal dialogue cases, but with some extra conditions already filled in to help your character comment on some specific things that are happening.

Recipes are a lot like the Situations used by the Writing Aid. Except instead of using them to react to a particular character, you can use them to react to a particular game state.

For example, select the "Lost 3 in a Row" recipe in the Poker category. This will create an Opponent Lost case, but with the extra condition that the target's Consecutive Loses = 3. Now your character can have something to say when she sees someone extra unlucky.

When a case has been created from a recipe, you can feel free to modify it however you like. (This won't change the base recipe, just your new case.) For example, you could change Consecutive Loses to 7, which would be unlikely to happen in a real game but is very likely if the human player is cheating. Or you can right-click the case in the dialogue tree and choose "Split This Case into Individual Stages" to make a different variant for every level of undress. Maybe your character is more sympathetic to her opponents' bad luck when she's fully dressed and more antagonistic when she's naked.

## How to create a Recipe:

You can create your own Recipe by clicking Tools in the menu bar and then "Manage Recipes..." Type a name for your recipe and click "CREATE NEW".

You can fill in the following:

* Name: This is the name of your recipe in the recipe list
* File name: You can optionally give your recipe a custom file name if you want to share it with other authors
* Group: This is the heading your recipe will be sorted under in the recipe list. The game's built-in recipes have the group names of: Stripping, Game Over, Poker, and Forfeit
* Label: You can learn about labels [in this video](https://youtu.be/Nq7kHhQ7yQI?t=55). These are a handy categorization tool, but you will probably want to leave this blank for most recipes.
* Description: This is how your recipe will be described in the recipe list. This line will also be used as the placeholder line of dialogue when someone uses this recipe.
* Case tag: This is important. Which case would you like used for this recipe?
* Conditions: This is what makes a recipe unique. What conditions would you like to pre-fill for anybody using the recipe?

A recipe will automatically save when you close its tab.

## How to share a Recipe:

Recipes are saved as individual files. You can find the folder where they're saved by either:

* Typing `%appdata%/SPNATI/Recipes` into the address bar of Windows Explorer
* Or editing a recipe you've created in the "Manage Recipes" window and clicking the "OPEN FOLDER" button in the top right

Recipe file names are random letters and numbers unless you chose a file name for them. To make sure you have the right one, you can open it in a text editor.

If you think of a good recipe, please share the file with us! Feel free to hand it off to the mods and make them take care of it.

To work, a recipe needs to apply to all characters. For example, a recipe couldn't be about one's own breasts, since not all characters have breasts. All recipes should be relevant to all characters.

## How to load a Recipe:

The game reads recipes from three locations:

- `spnati/tools/character_editor/recipes` is the central location of all recipes in the Git repository. 
- `%appdata%/SPNATI/Recipes` is where the CE exports them to by default. Recipes in this folder can be edited via "Manage Recipes".
- The `Resources/Recipes` folder in the Character Editor folder loads the recipes that originally came with the CE. Be careful of putting recipes here, as you'll need to move them if you download a new version of the CE.

Put your recipes files in one of the above folders, then load the Character Editor. Your recipes should now be available in the list.

Give this a shot! The repo's `recipes` folder already comes packaged with a slate of recipes from developer Arndress.

Most of the current recipes are Accessory/Minor Removed cases for males and females that look for the specific clothing names that can be worn by the player. You can use these to tailor some specific responses to the removal of these items. For example, if a character has removed a necktie, your character might make a bondage reference. If a character has removed headphones, your character might ask what he or she was listening to. I recommend that you make these responses **only play once**, just in case multiple opponents are wearing headphones.

## Additional Info

Here's what a recipe looks like when you open its text file. Remember, the Character Editor makes this file for you, so I just put this here in case you're curious. 

    {
    "case": {
      "lines": [],
      "tag": "game_start",
      "oneShotId": 0,
      "totalAlive": "3",
      "counters": [],
      "tests": []
    },
    "label": "",
    "name": "Three-Person Game",
    "key": "352366df-ef02-4874-a2eb-0fdc4c210fbd",
    "group": "Game Start",
    "description": "The game has just started, and only three people are here: the human player and two opponents."
    }