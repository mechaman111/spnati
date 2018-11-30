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
var EPILOGUES_UNLOCKED = false;
var EPILOGUE_BADGES_ENABLED = true;
var ALT_COSTUMES_ENABLED = false;
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
$playerTagsModal = $('#player-tags-modal');

/* Screen State */
$previousScreen = null;

/* CSS rules for arrow offsets */
var bubbleArrowOffsetRules;

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
                playerData.currentLine    = players[i].chosenState.rawDialogue;
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
  * xml (jQuery object), the player's loaded behaviour.xml file.
  * metaXml (jQuery object), the player's loaded meta.xml file.
  ************************************************************/

function Player (id) {
    this.id = id;
    this.folder = 'opponents/'+id+'/';
    this.base_folder = 'opponents/'+id+'/';
    this.first = '';
    this.last = '';
    this.labels = undefined;
    this.folders = undefined;
    this.size = eSize.MEDIUM;
    this.intelligence = eIntelligence.AVERAGE;
    this.gender = eGender.MALE;
    this.timer = 20;
    this.scale = undefined;
    this.tags = [id];
    this.xml = null;
    this.metaXml = null;

    this.selected_costume = null;
    this.alt_costume = null;
    this.default_costume = null;

    this.resetState();
}

/*******************************************************************
 * (Re)Initialize the player properties that change during a game
 *******************************************************************/
Player.prototype.resetState = function () {
    this.out = this.finished = this.exposed = false;
	this.forfeit = "";
	this.stage = this.current = this.consecutiveLosses = 0;
	this.timeInStage = -1;
	this.markers = {};
	this.exposed = { upper: false, lower: false };

	if (this.xml !== null) {
        /* Load in the legacy "start" lines, and also
         * initialize player.chosenState to the first listed line.
         * This may be overridden by later updateBehaviour calls if
         * the player has (new-style) selected or game start case lines.
         */
		var allStates = [];
        
        /* Initialize reaction handling state. */
        this.volatileMatches = [];
        this.bestVolatileMatch = null;
        this.currentTarget = null;
        this.currentPriority = -1;
        this.stateLockCount = 0;
        
        this.xml.children('start').children('state').each(function () {
            allStates.push(new State($(this)));
        });
        
        this.allStates = allStates;
		this.chosenState = this.allStates[0];
        this.chosenState.expandDialogue(this, null);
        
        if (!this.chosenState) {
            /* If the opponent does not have legacy start lines then select
             * a new-style selected line immediately.
             * Prevents a crash triggered by selected, unselecting, and re-selecting
             * an opponent with no legacy starting lines.
             */
            this.updateBehaviour(SELECTED);
            updateAllVolatileBehaviours();
        }

        var appearance = this.default_costume;
        if (ALT_COSTUMES_ENABLED && this.alt_costume) {
            appearance = this.alt_costume;
        }

        this.labels = appearance.labels;
        this.folders = appearance.folders;

        if (appearance.tags) {
            var alt_tags = appearance.tags.find('tag').each(function (idx, elem) {
                var $elem = $(elem);
                var tag = $elem.text();
                var removed = $elem.attr('remove') || '';
                if (removed.toLowerCase() === 'true') {
                    if (this.tags.indexOf(tag) > 0) {
                        this.tags.splice(this.tags.indexOf(tag), 1);
                    }
                } else {
                    this.tags.push(tag);
                }
            }.bind(this));
        }

        if (appearance.id) {
            this.tags.push(appearance.id);
        }

		/* Load the player's wardrobe. */

    	/* Find and grab the wardrobe tag */
    	$wardrobe = appearance.wardrobe;

    	/* find and create all of their clothing */
        var clothingArr = [];
    	$wardrobe.find('clothing').each(function () {
    		var formalName = $(this).attr('formalName');
    		var genericName = $(this).attr('genericName') || $(this).attr('lowercase');
    		var type = $(this).attr('type');
    		var position = $(this).attr('position');
    		var plural = ['true', 'yes'].indexOf($(this).attr('plural')) >= 0;

    		var newClothing = createNewClothing(formalName, genericName, type, position, null, plural, 0);

    		clothingArr.push(newClothing);
    	});

        this.clothing = clothingArr;
		this.startingLayers = clothingArr.length;
		this.mostlyClothed = checkPlayerStatus(this, STATUS_DECENT);
	}

	this.updateLabel();
	this.updateFolder();
}

Player.prototype.getIntelligence = function () {
    return this.intelligence; // Opponent uses getByStage()
};

/* These shouldn't do anything for the human player, but exist as empty functions
   to make it easier to iterate over the entire players[] array. */
Player.prototype.updateLabel = function () { }
Player.prototype.updateFolder = function () { }
Player.prototype.updateBehaviour = function() { }

/*****************************************************************************
 * Subclass of Player for AI-controlled players.
 ****************************************************************************/
function Opponent (id, $metaXml, status, releaseNumber) {
    this.id = id;
    this.folder = 'opponents/'+id+'/';
    this.base_folder = 'opponents/'+id+'/';
    this.metaXml = $metaXml;

    this.enabled = $metaXml.find('enabled').text();
    this.status = status;
    this.first = $metaXml.find('first').text();
    this.last = $metaXml.find('last').text();
    this.label = $metaXml.find('label').text();
    this.image = $metaXml.find('pic').text();
    this.gender = $metaXml.find('gender').text();
    this.height = $metaXml.find('height').text();
    this.source = $metaXml.find('from').text();
    this.artist = $metaXml.find('artist').text();
    this.writer = $metaXml.find('writer').text();
    this.description = fixupDialogue($metaXml.find('description').html());
    this.endings = $metaXml.find('epilogue');
    this.ending = this.endings.length > 0 || $metaXml.find('has_ending').text() === "true";
    this.layers = parseInt($metaXml.find('layers').text(), 10);
    this.scale = Number($metaXml.find('scale').text()) || 100.0;
    this.tags = $metaXml.find('tags').children().map(function() { return $(this).text(); }).get();
    this.release = parseInt(releaseNumber, 10) || Number.POSITIVE_INFINITY;

    /* Attempt to preload this opponent's picture for selection. */
    new Image().src = 'opponents/'+id+'/'+this.image;

    this.alternate_costumes = $metaXml.find('alternates').find('costume').map(function () {
        return {
            'folder': $(this).attr('folder'),
            'label': $(this).text(),
            'image': $(this).attr('img')
        };
    }).get();
}

Opponent.prototype = Object.create(Player.prototype);
Opponent.prototype.constructor = Opponent;

Opponent.prototype.clone = function() {
	var clone = Object.create(Opponent.prototype);
	/* This should be deep enough for our purposes. */
	for (var prop in this) {
		if (this[prop] instanceof Array) {
			clone[prop] = this[prop].slice();
		} else {
			clone[prop] = this[prop];
		}
	}
	return clone;
}

Opponent.prototype.isLoaded = function() {
	return this.xml != undefined;
}

Opponent.prototype.onSelected = function() {
    this.resetState();
	console.log(this.slot+": "+this);
    this.preloadStageImages(-1);
	this.updateBehaviour(SELECTED);
	updateSelectionVisuals();
}

Opponent.prototype.updateLabel = function () {
    if (this.labels) this.label = this.getByStage(this.labels);
}

Opponent.prototype.updateFolder = function () {
    if (this.folders) this.folder = this.getByStage(this.folders);
}

Opponent.prototype.getByStage = function (arr, stage) {
    if (typeof(arr) === "string") {
        return arr;
    }
    if (stage === undefined) stage = this.stage;
    var bestFitStage = -1;
    var bestFit = null;
    for (var i = 0; i < arr.length; i++) {
        var startStage = arr[i].getAttribute('stage');
        startStage = parseInt(startStage, 10) || 0;
        if (startStage > bestFitStage && startStage <= stage) {
            bestFit = $(arr[i]).text();
            bestFitStage = startStage;
        }
    }
    return bestFit;
};

Opponent.prototype.selectAlternateCostume = function (folder) {
    this.selected_costume = folder;
};

Opponent.prototype.getIntelligence = function () {
    return this.getByStage(this.intelligence) || eIntelligence.AVERAGE;
};

Opponent.prototype.loadAlternateCostume = function () {
    $.ajax({
        type: "GET",
        url: this.selected_costume+'costume.xml',
        dataType: "text",
        success: function (xml) {
            var $xml = $(xml);

            this.alt_costume = {
                id: $xml.find('id').text(),
                labels: $xml.find('label'),
                tags: $xml.find('tags'),
                folders: $xml.find('folder'),
                wardrobe: $xml.find('wardrobe')
            };

            this.onSelected();
        }.bind(this),
        error: function () {
            console.error("Failed to load alternate costume: "+this.selected_costume);
        },
    })
}

Opponent.prototype.unloadAlternateCostume = function () {
    if (!this.alt_costume) {
        return;
    }

    if (this.alt_costume.tags) {
        this.alt_costume.tags.find('tag').each(function (idx, elem) {
            var $elem = $(elem);
            var tag = $elem.text();
            var removed = $elem.attr('remove') || '';
            if (removed.toLowerCase() === 'true') {
                this.tags.push(tag);    // tag was previously removed, readd it
            } else {
                if (this.tags.indexOf(tag) > 0) {
                    // remove added tag
                    this.tags.splice(this.tags.indexOf(tag), 1);
                }
            }
        }.bind(this));
    }

    this.tags.splice(this.tags.indexOf(this.alt_costume.id), 1);

    this.alt_costume = null;
    this.selectAlternateCostume(null);
    this.resetState();
}

/************************************************************
 * Loads and parses the start of the behaviour XML file of the
 * given opponent.
 *
 * The onLoadFinished parameter must be a function capable of
 * receiving a new player object and a slot number.
 ************************************************************/
Opponent.prototype.loadBehaviour = function (slot) {
    this.slot = slot;
    if (this.isLoaded()) {
        if (this.selected_costume) {
            this.loadAlternateCostume();
        } else {
            this.onSelected();
        }
        return;
    }

    fetchCompressedURL(
		'opponents/' + this.id + "/behaviour.xml",
		/* Success callback.
         * 'this' is bound to the Opponent object.
         */
		function(xml) {
            console.time("Behaviour Load");
            console.time("XML Parsing");
            
            var $xml = $(xml);
            
            console.timeEnd("XML Parsing");
            
            this.xml = $xml;
            this.size = $xml.find('size').text();
            this.timer = Number($xml.find('timer').text());
            this.intelligence = $xml.find('intelligence');

            this.default_costume = {
                id: null,
                labels: $xml.find('label'),
                tags: null,
                folders: this.folder,
                wardrobe: $xml.find('wardrobe')
            };

            var tags = $xml.find('tags');
            var tagsArray = [this.id];
            if (typeof tags !== typeof undefined && tags !== false) {
                $(tags).find('tag').each(function () {
                    tagsArray.push($(this).text());
                });
            }

            this.tags = tagsArray;

            var targetedLines = {};

            $xml.find('case[target]>state, case[alsoPlaying]>state').each(function() {
                var $case = $(this.parentNode);
                ['target', 'alsoPlaying'].forEach(function(attr) {
                    var id = $case.attr(attr);
                    if (id) {
                        if (!(id in targetedLines)) { targetedLines[id] = { count: 0, seen: {} }; }
                        if (!(this.textContent in targetedLines[id].seen)) {
                            targetedLines[id].seen[this.textContent] = true;
                            targetedLines[id].count++;
                        }
                    }
                }, this);
            });

            this.targetedLines = targetedLines;

            if (ALT_COSTUMES_ENABLED && this.selected_costume) {
                this.loadAlternateCostume();
            } else {
                this.onSelected();
            }
            
            console.timeEnd("onSelected");
            console.timeEnd("Behaviour Load");
		}.bind(this),
		/* Error callback. */
        function(err) {
            console.log("Failed reading \""+this.id+"\" behaviour.xml");
            delete players[this.slot];
        }.bind(this)
	);
}

Player.prototype.getImagesForStage = function (stage) {
    if(!this.xml) return [];

    var imageSet = {};
    var folder = this.folders ? this.getByStage(this.folders, stage) : this.folder;
    var selector = (stage == -1 ? 'start, stage[id=1] case[tag=game_start]'
                    : 'stage[id='+stage+'] case');
    this.xml.find(selector).each(function () {
        var target = $(this).attr('target'), alsoPlaying = $(this).attr('alsoPlaying'),
            filter = $(this).attr('filter');
        // Skip cases requiring a character that isn't present
        if ((target === undefined || players.some(function(p) { return p.id === target; }))
            && (alsoPlaying === undefined || players.some(function(p) { return p.id === alsoPlaying; }))
            && (filter === undefined || players.some(function(p) { return p.tags.indexOf(filter) >= 0; })))
        {
            $(this).children('state').each(function () {
                imageSet[folder+$(this).attr('img')] = true;
            })
        }
    });
    return Object.keys(imageSet);
};

Player.prototype.preloadStageImages = function (stage) {
    this.getImagesForStage(stage)
        .forEach(function(fn) { new Image().src = fn; }, this );
};

/**********************************************************************
 *****              Overarching Game Flow Functions               *****
 **********************************************************************/

/************************************************************
 * Loads the initial content of the game.
 ************************************************************/
function initialSetup () {
    /* start by creating the human player object */
    var humanPlayer = new Player('human'); //createNewPlayer("human", "", "", "", eGender.MALE, eSize.MEDIUM, eIntelligence.AVERAGE, 20, undefined, [], null);
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

    /* Construct a CSS rule for every combination of arrow direction, screen, and pseudo-element */
    bubbleArrowOffsetRules = [];
    for (var i = 1; i <= 4; i++) {
        var pair = [];
        [["up", "down"], ["left", "right"]].forEach(function(p) {
            var index = document.styleSheets[2].cssRules.length;
            var rule = p.map(function(d) {
                return ["select", "game"].map(function(s) {
                    return ["before", "after"].map(function(r) {
                        return '#'+s+'-bubble-'+i+'>.dialogue-bubble.arrow-'+d+'::'+r;
                    }).join(', ');
                }).join(', ');
            }).join(', ') + ' {}';
            document.styleSheets[2].insertRule(rule, index);
            pair.push(document.styleSheets[2].cssRules[index]);
        });
        bubbleArrowOffsetRules.push(pair);
    }
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

            var _epilogues_unlocked = $(xml).find('epilogues-unlocked').text().trim();
            if (_epilogues_unlocked.toLowerCase() === 'true') {
                EPILOGUES_UNLOCKED = true;
                console.error('All epilogues unlocked in config file. You better be using this for development only and not cheating!');
            } else {
                EPILOGUES_UNLOCKED = false;
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

            var _alts = $(xml).find('alternate-costumes').text();

            if(_alts === "true") {
                ALT_COSTUMES_ENABLED = true;
                console.log("Alternate costumes enabled");
            } else {
                ALT_COSTUMES_ENABLED = false;
                console.log("Alternate costumes disabled");
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
            players[i].resetState();
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

function updateBugReportSendButton() {
    if($('#bug-report-desc').val().trim().length > 0) {
        $("#bug-report-modal-send-button").removeAttr('disabled');
    } else {
        $("#bug-report-modal-send-button").attr('disabled', 'true');
    }
}

$('#bug-report-desc').keyup(updateBugReportSendButton);

/* Update the bug report text dump. */
function updateBugReportOutput() {
    $('#bug-report-output').val(getBugReportJSON());
    $('#bug-report-status').text("");

    updateBugReportSendButton();
}

function copyBugReportOutput() {
    var elem = $('#bug-report-output')[0];
    elem.select();
    document.execCommand("copy");
}

function sendBugReport() {
    if($('#bug-report-desc').val().trim().length == 0) {
        $('#bug-report-status').text("Please enter a description first!");
        return false;
    }

    $.ajax({
        url: BUG_REPORTING_ENDPOINT,
        method: 'POST',
        data: getBugReportJSON(),
        contentType: 'application/json',
        error: function (jqXHR, status, err) {
            console.error("Could not send bug report - error "+status+": "+err);
            $('#bug-report-status').text("Failed to send bug report (error "+status+")");
        },
        success: function () {
            $('#bug-report-status').text("Bug report sent!");
            $('#bug-report-desc').val("");
            closeBugReportModal();
        }
    });


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
 * The player clicked the player tags button. Shows the player tags modal.
 ************************************************************/
function showPlayerTagsModal () {
    if (document.forms['player-tags'].elements.length <= 6) {
        // Safari doesn't support color inputs properly!
        var hairColorPicker = document.getElementById('hair_color_picker');
        var selectionType;
        try {
            selectionType = typeof hairColorPicker.selectionStart;
        } catch(e) {
            selectionType = null;
        }
        for (var choiceName in playerTagOptions) {
            var replace = (choiceName != 'skin_color' || selectionType === 'number');
            var $existing = $('form#player-tags [name="'+choiceName+'"]');
            if (!replace && $existing.length) continue;
            var $select = $('<select>', { name: choiceName });
            $select.append(new Option(), playerTagOptions[choiceName].values.map(function(opt) {
                return new Option(opt.text || opt.value.replace(/_/g, ' ').initCap(), opt.value);
            }));
            if ($existing.length) {
                $existing.parent().replaceWith($select);
            } else {
                var $label = $('<label class="player-tag-select">');
                if (playerTagOptions[choiceName].gender) {
                    $select.addClass(playerTagOptions[choiceName].gender);
                    $label.addClass(playerTagOptions[choiceName].gender);
                }
                $label.append('Choose your ' + choiceName.replace(/_/g, ' ') + ':', $select);
                $('form#player-tags').append($label);
            }
        }

        var rgb2hsv = function(rgb) {
          var r = parseInt(rgb.slice(1,3), 16)/255;
          var g = parseInt(rgb.slice(3,5), 16)/255;
          var b = parseInt(rgb.slice(5), 16)/255;

          var min = Math.min(r, Math.min(g,b));
          var max = Math.max(r, Math.max(g,b));

          if (max === 0) {
            return [0,0,0];
          }

          var maxOffset = max === r ? 0 : (max === g ? 2 : 4);
          var delta = max === r ? g-b : (max === g ? b-r : r-g);

          var h = 60 * (maxOffset + delta / (max - min));
          if (h < 0) {
            h += 360;
          }

          return [h, (max - min) / max * 100, max * 100];
        }

        /* convert the raw colors to corresponding tags and display next to selector */
        $('input[type=color]').on('input', function() {
            var h, s, v;
            [h,s,v] = rgb2hsv(this.value);

            var tag;
            color2tag:
            if (this.id === 'hair_color_picker') {
              if (v < 10) {
                tag = 'black_hair';
                break color2tag;
              }

              if (s < 25) {
                if (v < 30) {
                  tag = 'black_hair';
                } else {
                  tag = 'white_hair';
                }
                break color2tag;
              }

              if (s < 50 && h > 20 && h < 50) {
                tag = 'brunette';
                break color2tag;
              }

              if (h < 50) {
                tag = 'ginger';
              } else if (h < 65) {
                tag = 'blonde';
              } else if (h < 325) {
                if (h < 145) {
                  tag = 'green_hair';
                } else if (h < 255) {
                  tag = 'blue_hair';
                } else if (h < 290) {
                  tag = 'purple_hair';
                } else {
                  tag = 'pink_hair';
                }
              } else {
                tag = 'ginger';
              }
            } else if (this.id === 'eye_color_picker') {

              if (v < 25) {
                tag = 'dark_eyes';
                break color2tag;
              }

              if (s < 20) {
                tag = 'pale_eyes';
                break color2tag;
              }

              if (h < 15) {
                tag = 'red_eyes';
              } else if (h < 65) {
                tag = 'amber_eyes';
              } else if (h < 145) {
                tag = 'green_eyes';
              } else if (h < 255) {
                tag = 'blue_eyes';
              } else if (h < 325) {
                tag = 'violet_eyes';
              } else {
                tag = 'red_eyes';
              }
            }

            this.previousElementSibling.value = tag || '';
        });

        $('input[name=skin_color]').on('input', function() {
            for (var i = 0; i < playerTagOptions['skin_color'].values.length; i++) {
                if (this.value <= playerTagOptions['skin_color'].values[i].to) {
                    tag = playerTagOptions['skin_color'].values[i].value;
                    break;
                }
            }

            this.previousElementSibling.value = tag || '';
        });

        $('.modal-button.clearSelections').click(function() {
            var formElements = document.forms['player-tags'].elements;
            for (var i = 0; i < formElements.length; i++) {
                if (formElements[i].type !== 'color') {
                    formElements[i].value = '';
                }
            }
        });
    }

    for (var choiceName in playerTagOptions) {
        $('form#player-tags [name="'+choiceName+'"]').val(playerTagSelections[choiceName]).trigger('input');
    }
    $('#player-tags-confirm').one('click', function() {
        playerTagSelections = {};
        for (var choiceName in playerTagOptions) {
            if (!('gender' in playerTagOptions[choiceName]) || playerTagOptions[choiceName].gender == players[HUMAN_PLAYER].gender) {
                var val = $('form#player-tags [name="'+choiceName+'"]').val();
                if (val) {
                    playerTagSelections[choiceName] = val;
                }
            }
        }
    });
    $playerTagsModal.modal('show');
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
 * Counts the number of elements that evaluate as true, or,
 * if a function is provided, passes the test implemented by it.
 ************************************************************/
Array.prototype.countTrue = function(func) {
    var count = 0;
    for (var i = 0; i < this.length; i++) {
        if (i in this
            && (func ? func(this[i], i, this) : this[i])) {
            count++;
        }
    }
    return count;
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
