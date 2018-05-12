/********************************************************************************
 This file contains the variables and functions that forms the core of the game.
 Anything that is needed game-wide is kept here.
 ********************************************************************************/

/**********************************************************************
 * Game Wide Constants
 **********************************************************************/

/* General Constants */
var DEBUG = true;
var BASE_FONT_SIZE = 14;
var BASE_SCREEN_WIDTH = 100;

/* Game Wide Constants */
var HUMAN_PLAYER = 0;

/* Directory Constants */
var IMG = 'img/';
/*var OPP = 'opponents/';
#The "OPP" folder abbreviation was used to slightly shorten a few lines in spniSelect that looked for opponents in the opponents folder.
#Now that opponents can be specified in any folder, this is no longer required.*/

/* Gender Images */
var MALE_SYMBOL = IMG + 'male.png';
var FEMALE_SYMBOL = IMG + 'female.png';




/* game table */
var tableOpacity = 1;
$gameTable = $('#game-table');

/* useful variables */
var BLANK_PLAYER_IMAGE = "opponents/blank.png";

/* player array */
var players = [null, null, null, null, null];




/**********************************************************************
 * Game Wide Global Variables
 **********************************************************************/

var table = new Table();


/**********************************************************************
 * Screens & Modals
 **********************************************************************/

/* Screens */
$warningScreen = $('#warning-screen');
$titleScreen = $('#title-screen');
$selectScreen = $('#main-select-screen');
$individualSelectScreen = $('#individual-select-screen');
$groupSelectScreen = $('#group-select-screen');
$gameScreen = $('#game-screen');
$epilogueScreen = $('#epilogue-screen');
$galleryScreen = $('#gallery-screen');

/* Modals */
$searchModal = $('#search-modal');
$creditModal = $('#credit-modal');
$versionModal = $('#version-modal');
$gameSettingsModal = $('#game-settings-modal');

/* Screen State */
$previousScreen = null;


/********************************************************************************
 * Game Wide Utility Functions
 ********************************************************************************/



/**********************************************************************
 *****                Player Object Specification                 *****
 **********************************************************************/

/************************************************************
 * Creates and returns a new player object based on the
 * supplied information.
 *
 * folder (string), the path to their folder
 * first (string), their first name.
 * last (string), their last name.
 * gender (constant), their gender.
 * clothing (array of Clothing objects), their clothing.
 * out (boolean), is the player still in the game?
 * forfeit (string), state of their forfeit.
 * timer (integer), time until forfeit is finished.
 * current (integer), their current state.
 * state (array of PlayerState objects), their sequential states.
 * xml (jQuery object), the player's loaded XML file.
 ************************************************************/
function createNewPlayer (folder, first, last, label, gender, size, intelligence, timer, tags, xml) {
    var newPlayerObject = {folder:folder,
                           first:first,
                           last:last,
                           label:label,
						   size:size,
						   intelligence:intelligence,
                           gender:gender,
                           timer:timer,
                           tags:tags,
                           xml:xml,

                           getIntelligence: function getIntelligence() {

                               if (typeof(this.intelligence) === "string") {

                                   return this.intelligence;
                               }

                               var bestFitStage = -1;
                               var bestFit = null;
                               for (var i = 0; i < this.intelligence.length; i++) {

                                   var startStage = this.intelligence[i].getAttribute('stage');
                                   startStage = parseInt(startStage, 10) || 0;
                                   if (startStage > bestFitStage && startStage <= this.stage)
 {

                                       bestFit = $(this.intelligence[i]).text();

                                       bestFitStage = startStage;

                                   }

                               }

                               return bestFit || eIntelligence.AVERAGE;

                           },
                       };
                           
	initPlayerState(newPlayerObject);
    return newPlayerObject;
}

/*******************************************************************
 * (Re)Initialize the player properties that change during a game 
 *******************************************************************/
function initPlayerState(player) {
	player.out = player.finished = player.exposed = false;
	player.forfeit = "";
	player.stage = player.current = player.consecutiveLosses = 0;
	player.timeInStage = -1;
	player.markers = {};
	if (player.xml !== null) {
		player.state = parseDialogue($(player.xml).find('start'), [PLAYER_NAME], [players[HUMAN_PLAYER].label]);
		loadOpponentWardrobe(player);
	}
}

/**********************************************************************
 *****              Overarching Game Flow Functions               *****
 **********************************************************************/

/************************************************************
 * Loads the initial content of the game.
 ************************************************************/
function initialSetup () {
    /* start by creating the human player object */
    var humanPlayer = createNewPlayer("", "", "", "", eGender.MALE, eSize.MEDIUM, eIntelligence.AVERAGE, 20, [], null);
    players[HUMAN_PLAYER] = humanPlayer;
    
	/* enable table opacity */
	tableOpacity = 1;
	$gameTable.css({opacity:1});
	
    /* load the all content */
    loadTitleScreen();
    selectTitleCandy();
	loadSelectScreen();
    loadConfigFile();
	save.loadCookie();

	/* show the title screen */
	$warningScreen.show();
    autoResizeFont();
}


function loadConfigFile () {
	$.ajax({
        type: "GET",
		url: "config.xml",
		dataType: "text",
		success: function(xml) {           
			var _debug = $(xml).find('debug').text();

            if (_debug === "true") {
                DEBUG = true;
                console.log("Debugging is enabled");
            }
            else {
                DEBUG = false;
                console.log("Debugging is disabled");
            }
		}
	});
}



function enterTitleScreen() {
    $warningScreen.hide();
    $titleScreen.show();
}

/************************************************************
 * Transitions between two screens.
 ************************************************************/
function screenTransition (first, second) {
	first.hide();
	second.show();
}
 
/************************************************************
 * Switches to the next screen based on the screen provided.
 ************************************************************/
function advanceToNextScreen (screen) {
    if (screen == $titleScreen) {
        /* advance to the select screen */
		screenTransition($titleScreen, $selectScreen);

    } else if (screen == $selectScreen) {
        /* advance to the main game screen */
        $selectScreen.hide();
		loadGameScreen();
        $gameScreen.show();
    }
}

/************************************************************
 * Switches to the last screen based on the screen provided.
 ************************************************************/
function returnToPreviousScreen (screen) {
    if (screen == $selectScreen) {
        /* return to the title screen */
        $selectScreen.hide();
        $titleScreen.show();
    }
}

/************************************************************
 * Resets the game state so that the game can be restarted.
 ************************************************************/
function resetPlayers () {
	for (var i = 0; i < players.length; i++) {
		if (players[i] != null) {
			collectPlayerHand(i);
			$gameLabels[i].css({"background-color" : clearColour});
			initPlayerState(players[i]);
		}
		timers[i] = 0;
	}
}

/************************************************************
 * Restarts the game.
 ************************************************************/
function restartGame () {
    KEYBINDINGS_ENABLED = false;
	
	resetPlayers();
	
	/* enable table opacity */
	tableOpacity = 1;
	$gameTable.css({opacity:1});
    $gamePlayerClothingArea.show();
    $gamePlayerCardArea.show();
	
	/* trigger screen refreshes */
	updateSelectionVisuals();
	updateAllGameVisuals();
    selectTitleCandy();
    
    forceTableVisibility(true);
	
	/* there is only one call to this right now */
	$epilogueSelectionModal.hide();
	$gameScreen.hide();
	$epilogueScreen.hide();
	loadClothing();
	$titleScreen.show();
}

/**********************************************************************
 *****                    Interaction Functions                   *****
 **********************************************************************/
 
/************************************************************
 * The player clicked the credits button. Shows the credits modal.
 ************************************************************/
function showCreditModal () {
    $creditModal.modal('show');
}

/************************************************************
 * The player clicked the version button. Shows the version modal.
 ************************************************************/
function showVersionModal () {
    $versionModal.modal('show');
}

/************************************************************
 * The player clicked on a table opacity button.
 ************************************************************/
function toggleTableVisibility () {
	if (tableOpacity > 0) {
		$gameTable.fadeOut();
		tableOpacity = 0;
	} else {
		$gameTable.fadeIn();
		tableOpacity = 1;
	}
}

function forceTableVisibility(state) {
    if (!state) {
		$gameTable.fadeOut();
		tableOpacity = 0;
	} else {
		$gameTable.fadeIn();
		tableOpacity = 1;
	}
}

/**********************************************************************
 *****                     Utility Functions                      *****
 **********************************************************************/

/************************************************************
 * Returns a random number in a range.
 ************************************************************/
function getRandomNumber (min, max) {
	return Math.floor(Math.random() * (max - min) + min);
}


/**********************************************************************
 * Returns the width of the visible screen in pixels.
 **/
function getScreenWidth () 
{
	/* fetch all game screens */
	var screens = document.getElementsByClassName('screen');
	
	/* figure out which screen is visible */
	for (var i = 0; i < screens.length; i++) 
    {
		if (screens[i].offsetWidth > 0) 
        {
			/* this screen is currently visible */
			return screens[i].offsetWidth;
		}
	}
}

/**********************************************************************
 * Automatically adjusts the size of all font based on screen width.
 **/
function autoResizeFont () 
{
	/* resize font */
	var screenWidth = getScreenWidth();
	document.body.style.fontSize = (14*(screenWidth/1000))+'px';
	
	/* set up future resizing */
	window.onresize = autoResizeFont;
}
