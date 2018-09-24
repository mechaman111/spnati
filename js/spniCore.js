/********************************************************************************
 This file contains the variables and functions that forms the core of the game.
 Anything that is needed game-wide is kept here.
 ********************************************************************************/

/**********************************************************************
 * Game Wide Constants
 **********************************************************************/

/* General Constants */
var DEBUG = false;
var EPILOGUES_ENABLED = true;
var EPILOGUE_BADGES_ENABLED = true;
var USAGE_TRACKING = false;
var BASE_FONT_SIZE = 14;
var BASE_SCREEN_WIDTH = 100;

var USAGE_TRACKING_ENDPOINT = 'https://spnati.faraway-vision.io/usage/report';
var BUG_REPORTING_ENDPOINT = 'https://spnati.faraway-vision.io/usage/bug_report';

/* Game Wide Constants */
var HUMAN_PLAYER = 0;

/* Directory Constants */
var IMG = 'img/';

var backgroundImage;

/*var OPP = 'opponents/';
#The "OPP" folder abbreviation was used to slightly shorten a few lines in spniSelect that looked for opponents in the opponents folder.
#Now that opponents can be specified in any folder, this is no longer required.*/

/* Gender Images */
var MALE_SYMBOL = IMG + 'male.png';
var FEMALE_SYMBOL = IMG + 'female.png';

var includedOpponentStatuses = {};


/* game table */
var tableOpacity = 1;
$gameTable = $('#game-table');

/* useful variables */
var BLANK_PLAYER_IMAGE = "opponents/blank.png";

/* player array */
var players = Array(5);

/* Current timeout ID, so we can cancel it when restarting the game in order to avoid trouble. */
var timeoutID;

/**********************************************************************
 * Game Wide Global Variables
 **********************************************************************/

var table = new Table();
var jsErrors = [];
var sessionID = '';
var gameID = '';

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

var allScreens = [$warningScreen, $titleScreen, $selectScreen, $individualSelectScreen, $groupSelectScreen, $gameScreen, $epilogueScreen, $galleryScreen];

/* Modals */
$searchModal = $('#search-modal');
$groupSearchModal = $('#group-search-modal');
$creditModal = $('#credit-modal');
$versionModal = $('#version-modal');
$gameSettingsModal = $('#game-settings-modal');
$bugReportModal = $('#bug-report-modal');
$usageTrackingModal = $('#usage-reporting-modal');

/* Screen State */
$previousScreen = null;


/********************************************************************************
 * Game Wide Utility Functions
 ********************************************************************************/

function getReportedOrigin () {
    var origin = window.location.origin;
    
    if (origin.toLowerCase().startsWith('file:')) {
        return '<local filesystem origin>';
    } else {
        return origin;
    }
}

/* Gathers most of the generic information for an error report. */
function compileBaseErrorReport(userDesc, bugType) {
    var tableReports = [];
    for (let i=1;i<players.length;i++) {
        if (players[i]) {
            playerData = {
                'id': players[i].id,
                'slot': i,
                'stage': players[i].stage,
                'timeInStage': players[i].timeInStage,
                'markers': players[i].markers
            }
            
            if (players[i].chosenState) {
                playerData.currentLine    = players[i].chosenState.dialogue;
                playerData.currentImage   = players[i].chosenState.image;
            }
            
            tableReports[i-1] = playerData;
        } else {
            tableReports[i-1] = null;
        }
    }
    
    var circumstances = {
        'userAgent': navigator.userAgent,
        'origin': getReportedOrigin(),
        'currentRound': currentRound,
        'currentTurn': currentTurn,
        'visibleScreens': []
    }
    
    if (gamePhase) {
        circumstances.gamePhase = gamePhase[0];
    }
    
    for (let i=0;i<allScreens.length;i++) {
        if (allScreens[i].css('display') !== 'none') {
            circumstances.visibleScreens.push(allScreens[i].attr('id'));
        }
    }
    
    var bugCharacter = null;
    if (bugType.startsWith('character')) {
        bugCharacter = bugType.split('-', 2)[1];
        bugType = 'character';
    }
    
    return {
        'date': (new Date()).toISOString(),
        'session': sessionID,
        'game': gameID,
        'type': bugType,
        'character': bugCharacter,
        'description': userDesc, 
        'circumstances': circumstances,
        'table': tableReports,
        'player': {
            'gender': players[HUMAN_PLAYER].gender,
            'size': players[HUMAN_PLAYER].size,
        },
        'jsErrors': jsErrors,
    };
}

window.addEventListener('error', function (ev) {
    jsErrors.push({
        'date': (new Date()).toISOString(),
        'type': ev.error.name,
        'message': ev.message,
        'filename': ev.filename,
        'lineno': ev.lineno,
        'stack': ev.error.stack
    });
    
    if (USAGE_TRACKING) {
        var report = compileBaseErrorReport('Automatically generated after Javascript error.', 'auto');
        
        $.ajax({
            url: BUG_REPORTING_ENDPOINT,
            method: 'POST',
            data: JSON.stringify(report),
            contentType: 'application/json',
            error: function (jqXHR, status, err) {
                console.error("Could not send bug report - error "+status+": "+err);
            },
        });
    }
});

/* Fetch a possibly compressed file.
 * Attempts to fetch a compressed version of the file first,
 * then fetches the uncompressed version of the file if that isn't found.
 */
function fetchCompressedURL(baseUrl, successCb, errorCb) {
    /*
     * The usual Jquery AJAX request function doesn't play nice with
     * the binary-encoded data we'll get here, so we do the XHR manually.
     * (I would use fetch() were it not for compatibility issues.)
     */
    var req = new XMLHttpRequest();
    req.open('GET', baseUrl+'.gz', true);
    req.responseType = 'arraybuffer';
    
    req.onload = function(ev) {
        if (req.status < 400 && req.response) {
            var data = new Uint8Array(req.response);
            var decompressed = pako.inflate(data, { to: 'string' });
            successCb(decompressed);
        } else if (req.status === 404) {
            $.ajax({
                type: "GET",
        		url: baseUrl,
        		dataType: "text",
                success: successCb,
                error: errorCb,
            });
        } else {
            errorCb();
        }
    }
    
    req.onerror = function(err) {
        $.ajax({
            type: "GET",
            url: baseUrl,
            dataType: "text",
            success: successCb,
            error: errorCb,
        });
    }
    
    req.send(null);
}


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
 * labels (string or XML element), what's shown on screen and what other players refer to them as.
 *   Can vary by stage.
 * size (string): Their level of endowment
 * intelligence (string or XML element), the name of their AI algorithm.
 *   Can vary by stage.
 * gender (constant), their gender.
 * clothing (array of Clothing objects), their clothing.
 * timer (integer), time until forfeit is finished.
 * state (array of PlayerState objects), their sequential states.
 * xml (jQuery object), the player's loaded XML file.
 ************************************************************/
function createNewPlayer (id, first, last, labels, gender, size, intelligence, timer, scale, tags, xml) {
    var newPlayerObject = {id:id,
                           folder:'opponents/'+id+'/',
						   first:first,
                           last:last,
                           labels:labels,
						   size:size,
						   intelligence:intelligence,
                           gender:gender,
                           timer:timer,
						   scale:scale,
                           tags:tags,
                           xml:xml,
                           
                           getImagesForStage: function(stage) {
                               if(!this.xml) return [];
                               
                               var imageSet = {};
                               var folder = this.folder;
                               this.xml.find('stage[id="'+stage+'"] state').each(function () {
                                   imageSet[folder+$(this).attr('img')] = true;
                               });
                               return Object.keys(imageSet);
                           },
                           getByStage: function (arr) {
                               if (typeof(arr) === "string") {
                                   return arr;
                               }
                               var bestFitStage = -1;
                               var bestFit = null;
                               for (var i = 0; i < arr.length; i++) {
                                   var startStage = arr[i].getAttribute('stage');
                                   startStage = parseInt(startStage, 10) || 0;
                                   if (startStage > bestFitStage && startStage <= this.stage) {
                                       bestFit = $(arr[i]).text();
                                       bestFitStage = startStage;
                                   }
                               }
                               return bestFit;
                           },
                           getIntelligence: function () {
                               return this.getByStage(this.intelligence) || eIntelligence.AVERAGE;
                           },
                           updateLabel: function () {
                               if (this.labels) this.label = this.getByStage(this.labels);
                           }
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
        /* Load in the legacy "start" lines, and also
         * initialize player.chosenState to the first listed line.
         * This may be overridden by later updateBehaviour calls if
         * the player has (new-style) selected or game start case lines.
         */
		player.allStates = parseDialogue(player.xml.find('start'), player);
		player.chosenState = player.allStates[0];
        
		loadOpponentWardrobe(player);
	}
	player.updateLabel();
}

/**********************************************************************
 *****              Overarching Game Flow Functions               *****
 **********************************************************************/

/************************************************************
 * Loads the initial content of the game.
 ************************************************************/
function initialSetup () {
    /* start by creating the human player object */
    var humanPlayer = createNewPlayer("human", "", "", "", eGender.MALE, eSize.MEDIUM, eIntelligence.AVERAGE, 20, undefined, [], null);
    players[HUMAN_PLAYER] = humanPlayer;

	/* enable table opacity */
	tableOpacity = 1;
	$gameTable.css({opacity:1});

    /* load the all content */
    loadTitleScreen();
    selectTitleCandy();
	/* Make sure that the config file is loaded before processing the
	   opponent list, so that includedOpponentStatuses is populated. */
    loadConfigFile().always(loadSelectScreen);
	save.loadCookie();

	/* show the title screen */
	$warningScreen.show();
    autoResizeFont();
    
    /* Generate a random session ID. */
    sessionID = generateRandomID();
}


function loadConfigFile () {
	return $.ajax({
        type: "GET",
		url: "config.xml",
		dataType: "text",
		success: function(xml) {           
			var _epilogues = $(xml).find('epilogues').text();
            if(_epilogues.toLowerCase() === 'false') {
                EPILOGUES_ENABLED = false;
                console.log("Epilogues are disabled.");
                $("#title-gallery-edge").hide();
            } else {
                console.log("Epilogues are enabled.");
                EPILOGUES_ENABLED = true;
            }
            
            var _epilogue_badges = $(xml).find('epilogue_badges').text();
            if(_epilogue_badges.toLowerCase() === 'false') {
                EPILOGUE_BADGES_ENABLED = false;
                console.log("Epilogue badges are disabled.");
            } else {
                console.log("Epilogue badges are enabled.");
                EPILOGUE_BADGES_ENABLED = true;
            }
            
			var _debug = $(xml).find('debug').text();

            if (_debug === "true") {
                DEBUG = true;
                console.log("Debugging is enabled");
            }
            else {
                DEBUG = false;
                console.log("Debugging is disabled");
            }
			$(xml).find('include-status').each(function() {
				includedOpponentStatuses[$(this).text()] = true;
				console.log("Including", $(this).text(), "opponents");
			});
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
			initPlayerState(players[i]);
		}
		timers[i] = 0;
	}
	updateAllBehaviours(null, SELECTED);
}

/************************************************************
 * Restarts the game.
 ************************************************************/
function restartGame () {
    KEYBINDINGS_ENABLED = false;

	clearTimeout(timeoutID); // No error if undefined or no longer valid
	timeoutID = autoForfeitTimeoutID = undefined;
	stopCardAnimations();
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

/*
 * Bug Report Modal functions
 */

function getBugReportJSON() {
    var desc = $('#bug-report-desc').val();
    var type = $('#bug-report-type').val();
    var character = undefined;

    var report = compileBaseErrorReport(desc, type);
    return JSON.stringify(report);
}

/* Update the bug report text dump. */
function updateBugReportOutput() {
    $('#bug-report-output').val(getBugReportJSON());
}

function copyBugReportOutput() {
    var elem = $('#bug-report-output')[0];
    elem.select();
    document.execCommand("copy");
}

function sendBugReport() {
    $.ajax({
        url: BUG_REPORTING_ENDPOINT,
        method: 'POST',
        data: getBugReportJSON(),
        contentType: 'application/json',
        error: function (jqXHR, status, err) {
            console.error("Could not send bug report - error "+status+": "+err);
        },
    });
    
    closeBugReportModal();
}

$('#bug-report-type').change(updateBugReportOutput);
$('#bug-report-desc').change(updateBugReportOutput);
$('#bug-report-copy-btn').click(copyBugReportOutput);

 /************************************************************
  * The player clicked a bug-report button. Shows the bug reports modal.
  ************************************************************/
function showBugReportModal () {
    /* Set up possible bug report types. */    
    var bugReportTypes = [
        ['freeze', 'Game Freeze or Crash'],
        ['display', 'Game Graphical Problem'],
        ['other', 'Other Game Issue'],
    ]
    
    for (var i=1;i<5;i++) {
        if (players[i]) {
            var mixedCaseID = players[i].id.charAt(0).toUpperCase()+players[i].id.substring(1);
            bugReportTypes.push(['character-'+players[i].id, 'Character Defect ('+mixedCaseID+')']);
        }
    }
    
    $('#bug-report-type').empty().append(bugReportTypes.map(function (t) {
        return $('<option value="'+t[0]+'">'+t[1]+'</option>');
    }));
    
    $('#bug-report-modal span[data-toggle="tooltip"]').tooltip();
    updateBugReportOutput();
    
    KEYBINDINGS_ENABLED = false;
    
    $bugReportModal.modal('show');
}

function closeBugReportModal() {
    KEYBINDINGS_ENABLED = true;
    $bugReportModal.modal('hide');
}

/*
 * Show the usage tracking consent modal.
 */
 
function showUsageTrackingModal() {
    $usageTrackingModal.modal('show');
}

function enableUsageTracking() {
    save.data.askedUsageTracking = true;
    USAGE_TRACKING = true;
    
    save.saveOptions();
}

function disableUsageTracking() {
    save.data.askedUsageTracking = true;
    USAGE_TRACKING = false;
    
    save.saveOptions();
}

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

/************************************************************
 * Changes the first letter in a string to upper case.
 ************************************************************/
String.prototype.initCap = function() {
	return this.substr(0, 1).toUpperCase() + this.substr(1);
}

/************************************************************
 * Generate a random alphanumeric ID.
 ************************************************************/
function generateRandomID() {
    var ret = ''
    for (let i=0;i<10;i++) {
        ret += 'abcdefghijklmnopqrstuvwxyz1234567890'[getRandomNumber(0,36)]
    }
    
    return ret;
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

	if (backgroundImage && backgroundImage.height && backgroundImage.width) {
		var w = window.innerWidth, h = window.innerHeight;
		if (h > (3/4) * w) {
			h = (3/4) * w;
		} else {
			w = 4 * h / 3;
		}
		var ar = backgroundImage.width / backgroundImage.height;
		if (ar > 4/3) {
			var scale = Math.sqrt(16/9 / ar);
			$("body").css("background-size", "auto " + Math.round(scale * h) + "px");
		} else {
			var scale = Math.sqrt(ar);
			$("body").css("background-size", Math.round(scale * w) + "px auto");
		}
	}
	/* set up future resizing */
	window.onresize = autoResizeFont;
}

/* Get the number of players loaded, including the human player.*/
function countLoadedOpponents() {
    return players.reduce(function (a, v) { return a + (v ? 1 : 0); }, 0);
}
