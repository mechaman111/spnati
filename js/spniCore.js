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
var COLLECTIBLES_ENABLED = true;
var COLLECTIBLES_UNLOCKED = false;
var EPILOGUE_BADGES_ENABLED = true;
var COSTUME_BADGES_ENABLED = true;
var ALT_COSTUMES_ENABLED = false;
var DEFAULT_COSTUME_SET = null;
var USAGE_TRACKING = undefined;
var SENTRY_INITIALIZED = false;
var RESORT_ACTIVE = false;
var BASE_FONT_SIZE = 14;
var BASE_SCREEN_WIDTH = 100;

var USAGE_TRACKING_ENDPOINT = 'https://spnati.faraway-vision.io/usage/report';
var BUG_REPORTING_ENDPOINT = 'https://spnati.faraway-vision.io/usage/bug_report';
var FEEDBACK_ROUTE = "https://spnati.faraway-vision.io/usage/feedback/";

var CURRENT_VERSION = undefined;
var VERSION_COMMIT = undefined;
var VERSION_TAG = undefined;

var DEFAULT_FILL = undefined;

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
var alternateCostumeSets = {};

var versionInfo = null;

/* game table */
var tableOpacity = 1;
$gameTable = $('#game-table');

/* useful variables */
var BLANK_PLAYER_IMAGE = "opponents/blank.png";

/* player array */
var players = Array(5);
Object.defineProperty(players, 'opponents', {
    get: function() {
        return this.slice(1);
    }
});
var humanPlayer;

/* Current timeout ID, so we can cancel it when restarting the game in order to avoid trouble. */
var timeoutID;

/**********************************************************************
 * Game Wide Global Variables
 **********************************************************************/

var table = new Table();
var jsErrors = [];
var sessionID = '';
var gameID = '';
var generalCollectibles = [];
var codeImportEnabled = false;

/**********************************************************************
 * Screens & Modals
 **********************************************************************/

/* Screens */
$titleScreen = $('#title-screen');
$selectScreen = $('#main-select-screen');
$individualSelectScreen = $('#individual-select-screen');
$groupSelectScreen = $('#group-select-screen');
$gameScreen = $('#game-screen');
$epilogueScreen = $('#epilogue-screen');
$galleryScreen = $('#gallery-screen');

var allScreens = [$titleScreen, $selectScreen, $individualSelectScreen, $groupSelectScreen, $gameScreen, $epilogueScreen, $galleryScreen];

/* Modals */
$helpModal = $('#help-modal');
$creditModal = $('#credit-modal');
$versionModal = $('#version-modal');
$bugReportModal = $('#bug-report-modal');
$feedbackReportModal = $('#feedback-report-modal');
$usageTrackingModal = $('#usage-reporting-modal');
$playerTagsModal = $('#player-tags-modal');
$collectibleInfoModal = $('#collectibles-info-modal');
$ioModal = $('#io-modal');
$resortModal = $('#resort-modal');

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
    var bugCharacter = null;
    if (bugType.startsWith('character')) {
        bugCharacter = bugType.split(':', 2)[1];
        bugType = 'character';
    }

    var circumstances = {
        'userAgent': navigator.userAgent,
        'origin': getReportedOrigin(),
        'visibleScreens': [],
    };

    var data = {
        'date': (new Date()).toISOString(),
        'commit': VERSION_COMMIT,
        'session': sessionID,
        'game': gameID,
        'type': bugType,
        'character': bugCharacter,
        'description': userDesc,
        'circumstances': circumstances,
        'player': {
            'gender': humanPlayer.gender,
            'size': humanPlayer.size,
        },
        'jsErrors': jsErrors,
    };

    if (epiloguePlayer) {
        data.epilogue = {
            epilogue: epiloguePlayer.epilogue.title,
            player: epiloguePlayer.epilogue.player.id,
            gender: epiloguePlayer.epilogue.gender,
            scene: epiloguePlayer.sceneIndex,
            sceneName: epiloguePlayer.activeScene.name,
            view: epiloguePlayer.viewIndex,
            directive: epiloguePlayer.directiveIndex,
        };
        for (let i = epiloguePlayer.directiveIndex; i >= 0; i--) {
            if (epiloguePlayer.activeScene.directives[i].type == "text") {
                data.epilogue.lastText = epiloguePlayer.activeScene.directives[i].text;
                break;
            }
        }
    } else {
        var gameState = {
            'currentRound': currentRound,
            'currentTurn': currentTurn,
            'previousLoser': previousLoser,
            'recentLoser': recentLoser,
            'gameOver': gameOver,
            'rollback': inRollback()
        };
        mergeObjects(circumstances, gameState);
        if (gamePhase) {
            if (inRollback()) {
                circumstances.gamePhase = rolledBackGamePhase[0];
            } else {
                circumstances.gamePhase = gamePhase[0];
            }
        }

        var tableReports = [];
        for (let i=1;i<players.length;i++) {
            if (players[i]) {
                playerData = {
                    'id': players[i].id,
                    'slot': i,
                    'stage': players[i].stage,
                    'timeInStage': players[i].timeInStage,
                    'markers': players[i].markers,
                    'oneShotCases': players[i].oneShotCases,
                    'oneShotStates': players[i].oneShotStates,
                }

                if (players[i].chosenState) {
                    playerData.currentLine    = players[i].chosenState.rawDialogue;
                    if (players[i].chosenState.image) {
                        playerData.currentImage   = players[i].folder + players[i].chosenState.image.replace('#', players[i].stage);
                    }
                }

                tableReports[i-1] = playerData;
            } else {
                tableReports[i-1] = null;
            }
        }
        data.table = tableReports;
    }

    for (let i=0;i<allScreens.length;i++) {
        if (allScreens[i].css('display') !== 'none') {
            circumstances.visibleScreens.push(allScreens[i].attr('id'));
        }
    }

    return data;
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
function fetchCompressedURL(baseUrl) {
    return $.ajax(baseUrl+'.gz', {
        xhrFields: { responseType: 'arraybuffer' },
    }).then(function(data) {
        return pako.inflate(new Uint8Array(data), { to: 'string' });
    }, function(jqXHR) {
        if (jqXHR.status == 404) {
            return $.ajax(baseUrl, {
                dataType: 'text',
            });
        }
    });
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
  * stamina (integer), time until forfeit is finished (initial timer value).
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
    this.stamina = 20;
    this.scale = undefined;
    this.tags = this.baseTags = [];
    this.xml = null;
    this.metaXml = null;
    this.persistentMarkers = {};

    this.resetState();
}

/*******************************************************************
 * Sets initial values of state variables used by targetStatus,
 * targetStartingLayers etc. adccording to wardrobe.
 *******************************************************************/
Player.prototype.initClothingStatus = function () {
	this.startingLayers = this.clothing.length;
	this.exposed = { upper: true, lower: true };
	for (var position in this.exposed) {
		if (this.clothing.some(function(c) {
			return (c.type == IMPORTANT_ARTICLE || c.type == MAJOR_ARTICLE)
				&& (c.position == position || c.position == FULL_ARTICLE);
		})) {
			this.exposed[position] = false;
		};
	}
	this.mostlyClothed = this.decent = !(this.exposed.upper || this.exposed.lower)
		&& this.clothing.some(function(c) {
			return c.type == MAJOR_ARTICLE
				&& [UPPER_ARTICLE, LOWER_ARTICLE, FULL_ARTICLE].indexOf(c.position) >= 0;
		});
}

/*******************************************************************
 * (Re)Initialize the player properties that change during a game
 *******************************************************************/
Player.prototype.resetState = function () {
    this.out = this.finished = false;
    this.outOrder = undefined;
    this.biggestLead = 0;
	this.forfeit = "";
	this.stage = this.current = this.consecutiveLosses = 0;
	this.timeInStage = -1;
	this.markers = {};
	this.oneShotCases = {};
	this.oneShotStates = {};

	if (this.xml !== null) {
        /* Initialize reaction handling state. */
        this.currentTarget = null;
        this.currentTags = [];
        this.stateCommitted = false;

        if (this.startStates.length > 0) this.updateChosenState(new State(this.startStates[0]));

        var appearance = this.default_costume;
        if (ALT_COSTUMES_ENABLED && this.alt_costume) {
            appearance = this.alt_costume;
        }

        this.labels = appearance.labels;
        this.folders = appearance.folders;
        this.baseTags = appearance.tags.slice();
        this.labelOverridden = false;

		/* Load the player's wardrobe. */

    	/* Find and grab the wardrobe tag */
    	$wardrobe = appearance.wardrobe;

    	/* find and create all of their clothing */
        var clothingArr = [];
    	$wardrobe.find('clothing').each(function () {
            var generic = $(this).attr('generic');
            var name = $(this).attr('name') || $(this).attr('lowercase');
            var type = $(this).attr('type');
            var position = $(this).attr('position');
            var plural = ['true', 'yes'].indexOf($(this).attr('plural')) >= 0;

            var newClothing = new Clothing(name, generic, type, position, null, plural, 0);

            clothingArr.push(newClothing);
    	});
        
        this.poses = appearance.poses;

        this.clothing = clothingArr;
		this.initClothingStatus();
	}

	this.stageChangeUpdate();
}

Player.prototype.getIntelligence = function () {
    return this.intelligence; // Opponent uses getByStage()
};

/* These shouldn't do anything for the human player, but exist as empty functions
   to make it easier to iterate over the entire players[] array. */
Player.prototype.updateLabel = function () { }
Player.prototype.updateFolder = function () { }
Player.prototype.updateBehaviour = function() { }

/* Compute the Player's tags list from their baseTags list. */
Player.prototype.updateTags = function () {
    var tags = [this.id];
    var stage = this.stage || 0;
    
    if (this.alt_costume && this.alt_costume.id) {
        tags.push(this.alt_costume.id);
    }
    
    this.baseTags.forEach(function (tag_desc) {
        if (typeof(tag_desc) === 'string') {
            tags.push(tag_desc);
            return;
        }
        
        if (!tag_desc.tag) return;
        
        var tag = tag_desc.tag;
        var from = parseInt(tag_desc.from, 10);
        var to = parseInt(tag_desc.to, 10);
        
        if (isNaN(to))   to = Number.POSITIVE_INFINITY;
        if (isNaN(from)) from = 0;
        
        if (stage >= from && stage <= to) {
            tags.push(tag);
        }
    });
    
    this.tags = expandTagsList(tags);
}

Player.prototype.stageChangeUpdate = function () {
    this.updateLabel();
    this.updateFolder();
    this.updateTags();
}

Player.prototype.addTag = function(tag) {
    this.baseTags.push(canonicalizeTag(tag));
}

Player.prototype.removeTag = function(tag) {
    tag = canonicalizeTag(tag);
    
    this.baseTags = this.baseTags.filter(function (t) {
        if (typeof(t) === 'string') { return t !== tag };
        if (!t.tag) return false;
        return t.tag !== tag;
    });
}

Player.prototype.hasTag = function(tag) {
    return tag && this.tags && this.tags.indexOf(canonicalizeTag(tag)) >= 0;
};

Player.prototype.countLayers = function() {
	return this.clothing.length;
};

Player.prototype.checkStatus = function(status) {
	if (status.substr(0, 4) == "not_") {
		return !this.checkStatus(status.substr(4));
	}
	switch (status.trim()) {
	case STATUS_LOST_SOME:
		return this.stage > 0;
	case STATUS_MOSTLY_CLOTHED:
		return this.mostlyClothed;
	case STATUS_DECENT:
		return this.decent;
	case STATUS_EXPOSED_TOP:
		return this.exposed.upper;
	case STATUS_EXPOSED_BOTTOM:
		return this.exposed.lower;
	case STATUS_EXPOSED:
		return this.exposed.upper || this.exposed.lower;
	case STATUS_EXPOSED_TOP_ONLY:
		return this.exposed.upper && !this.exposed.lower;
	case STATUS_EXPOSED_BOTTOM_ONLY:
		return !this.exposed.upper && this.exposed.lower;
	case STATUS_NAKED:
		return this.exposed.upper && this.exposed.lower;
	case STATUS_ALIVE:
		return !this.out;
	case STATUS_LOST_ALL:
		return this.clothing.length == 0;
	case STATUS_MASTURBATING:
		return this.out && !this.finished;
	case STATUS_FINISHED:
		return this.finished;
	}
}

/**
 * Get the value of a marker set on this Player.
 * 
 * This method always attempts to parse stored marker values as integers,
 * but if this isn't possible then the raw string will be returned instead
 * (unless `numeric` is set to `true`).
 * 
 * If a `target` is passed, the marker value will be read from a per-target
 * marker first, if possible. By default (if `targeted_only` is not `true`),
 * if the per-target marker is not found, the base marker's value will be
 * used as a default.
 * 
 * @param {string} baseName The name of the marker to look up.
 * @param {Player} target If passed, the value will be loaded on a per-target
 * basis.
 * @param {boolean} numeric If `true`, then stored marker values that cannot
 * be converted to number values will be returned as 0 instead of as strings.
 * @param {boolean} targeted_only If `true`, then per-target markers will
 * _not_ default to using their base names if not found.
 * @returns {number | string}
 */
Player.prototype.getMarker = function (baseName, target, numeric, targeted_only) {
    var val = 0;

    var name = baseName;
    if (target && target.id) {
        name = getTargetMarker(baseName, target);
    }

    if (!this.persistentMarkers[baseName]) {
        val = this.markers[name];

        if (!val && target && !targeted_only) {
            /* If the per-target marker wasn't found, attempt to default
             * to the nonspecific marker.
             */
            val = this.markers[baseName];
        }
    } else {
        val = save.getPersistentMarker(this, name);

        if (!val && target && !targeted_only) {
            val = save.getPersistentMarker(this, baseName);
        }
    }

    var cast = parseInt(val, 10);
    
    if (!isNaN(cast)) {
        return cast;
    } else if (numeric) {
        return 0;
    } else {
        return val;
    }
}

/**
 * Set the value of a marker on this Player.
 * 
 * @param {string} baseName The name of the marker to set.
 * @param {Player} target If passed, the value will be set on a per-target
 * basis.
 * @param {string | number} value The value to set for the marker.
 */
Player.prototype.setMarker = function (baseName, target, value) {
    var name = baseName;
    if (target && target.id) {
        name = getTargetMarker(baseName, target);
    }

    if (!this.persistentMarkers[baseName]) {
        this.markers[name] = value;
    } else {
        save.setPersistentMarker(this, name, value);
    }
}


/**
 * Subclass of Player for AI-controlled players.
 * 
 * @constructor
 * 
 * @param {string} id 
 * @param {jQuery} $metaXml 
 * @param {string} status 
 * @param {number} [releaseNumber] 
 * @param {string} [highlightStatus]
 */
function Opponent (id, $metaXml, status, releaseNumber, highlightStatus) {
    Player.call(this, id);

    this.id = id;
    this.folder = 'opponents/'+id+'/';
    this.base_folder = 'opponents/'+id+'/';
    this.metaXml = $metaXml;

    this.status = status;
    this.highlightStatus = highlightStatus || status || '';
    this.first = $metaXml.find('first').text();
    this.last = $metaXml.find('last').text();
    
    /* selectLabel shouldn't change due to e.g. alt costumes selected on
     * the main select screen.
     */
    this.selectLabel = $metaXml.find('label').text();
    this.label = this.selectLabel;

    this.image = $metaXml.find('pic').text();
    this.gender = $metaXml.find('gender').text();
    this.height = $metaXml.find('height').text();
    this.source = $metaXml.find('from').text();
    this.artist = $metaXml.find('artist').text();
    this.writer = $metaXml.find('writer').text();
    this.description = fixupDialogue($metaXml.find('description').html());
    this.has_collectibles = $metaXml.find('has_collectibles').text() === "true";
    this.collectibles = null;
    this.layers = parseInt($metaXml.find('layers').text(), 10);
    this.scale = Number($metaXml.find('scale').text()) || 100.0;
    this.release = parseInt(releaseNumber, 10) || Number.POSITIVE_INFINITY;
    this.uniqueLineCount = parseInt($metaXml.find('lines').text(), 10) || undefined;
    this.posesImageCount = parseInt($metaXml.find('poses').text(), 10) || undefined;
    this.z_index = parseInt($metaXml.find('z-index').text(), 10) || 0;
    this.dialogue_layering = $metaXml.find('dialogue-layer').text();
    
    this.endings = $metaXml.find('epilogue');
    this.ending = $metaXml.find('has_ending').text() === "true";

    if (this.endings.length > 0) {
        this.endings.each(function (idx, elem) {
            var status = $(elem).attr('status');
            if (!status || includedOpponentStatuses[status]) {
                this.ending = true;
            }
        }.bind(this));
    }

    if (['over', 'under'].indexOf(this.dialogue_layering) < 0) {
        this.dialogue_layering = 'under';
    }
    
    this.selected_costume = null;
    this.alt_costume = null;
    this.default_costume = null;
    this.poses = {};
    this.labelOverridden = false;
    this.pendingCollectiblePopup = null;

    this.loaded = false;
    this.loadProgress = undefined;
    
    /* baseTags stores tags that will be later used in resetState to build the
     * opponent's true tags list. It does not store implied tags.
     *
     * The tags list stores the fully-expanded list of tags for the opponent,
     * including implied tags.
     */
    this.baseTags = $metaXml.find('tags').children().map(function() { return canonicalizeTag($(this).text()); }).get();
    this.removeTag(this.id);
    this.updateTags();
    this.searchTags = expandTagsList(this.baseTags);
    
    this.cases = new Map();

    /* Attempt to preload this opponent's picture for selection. */
    new Image().src = 'opponents/'+id+'/'+this.image;

    this.alternate_costumes = [];
    this.selection_image = this.folder + this.image;
    
    $metaXml.find('alternates').find('costume').each(function (i, elem) {
        var set = $(elem).attr('set') || 'offline';
        var status = $(elem).attr('status') || 'offline';
        
        if (alternateCostumeSets['all'] || alternateCostumeSets[set]) {
            if (!includedOpponentStatuses[status]) {
                return;
            }

            var costume_descriptor = {
                'folder': $(elem).attr('folder'),
                'label': $(elem).text(),
                'image': $(elem).attr('img'),
                'set': set,
                'status': status,
            };
            
            if (set === DEFAULT_COSTUME_SET) {
                this.selection_image = costume_descriptor['folder'] + costume_descriptor['image'];
                this.selectAlternateCostume(costume_descriptor);
            }
            
            this.alternate_costumes.push(costume_descriptor);
        }
    }.bind(this)).get();
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
    return this.loaded;
}

Opponent.prototype.onSelected = function(individual) {
    this.resetState();
    console.log(this.slot+": ");
    console.log(this);
    
    // check for duplicate <link> elements:
    if (this.stylesheet) {
        if ($('link[href=\"'+this.stylesheet+'\"]').length === 0) {
            console.log("Loading stylesheet: "+this.stylesheet);
            
            var link_elem = $('<link />', {
                'rel': 'stylesheet',
                'type': 'text/css',
                'href': this.stylesheet
            });
            
            $('head').append(link_elem);
        }
    }

    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            category: 'select',
            message: 'Load completed for ' + this.id,
            level: 'info'
        });
    }
    
    this.preloadStageImages(-1);
    if (individual) {
        updateAllBehaviours(this.slot, SELECTED, [[OPPONENT_SELECTED]]);
    } else {
        this.updateBehaviour(SELECTED);
        this.commitBehaviourUpdate();
    }

    updateSelectionVisuals();
}

Opponent.prototype.updateLabel = function () {
    if (this.labels && !this.labelOverridden) this.label = this.getByStage(this.labels);
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

Opponent.prototype.selectAlternateCostume = function (costumeDesc) {
    if (!costumeDesc) {
        this.selected_costume = null;
        this.selection_image = this.base_folder + this.image;
    } else {
        this.selected_costume = costumeDesc.folder;
        this.selection_image = costumeDesc.folder + costumeDesc.image;
    }

    if (this.selectionCard)
        this.selectionCard.update();
};

Opponent.prototype.getIntelligence = function () {
    return this.getByStage(this.intelligence) || eIntelligence.AVERAGE;
};

Opponent.prototype.loadAlternateCostume = function (individual) {
    if (this.alt_costume) {
        if (this.alt_costume.folder != this.selected_costume) {
            this.unloadAlternateCostume();
        } else {
            setTimeout(this.onSelected.bind(this), 1, individual);
            return;
        }
    }
    console.log("Loading alternate costume: "+this.selected_costume);
    $.ajax({
        type: "GET",
        url: this.selected_costume+'costume.xml',
        dataType: "text",
        success: function (xml) {
            var $xml = $(xml);

            if (SENTRY_INITIALIZED) {
                Sentry.addBreadcrumb({
                    category: 'select',
                    message: 'Initializing alternate costume for ' + this.id + ': ' + this.selected_costume,
                    level: 'info'
                });
            }

            this.alt_costume = {
                id: $xml.find('id').text(),
                labels: $xml.find('label'),
                tags: [],
                folder: this.selected_costume,
                folders: $xml.find('folder'),
                wardrobe: $xml.find('wardrobe')
            };
            
            var poses = $xml.find('poses');
            var poseDefs = {};
            $(poses).find('pose').each(function (i, elem) {
                var def = new PoseDefinition($(elem), this);
                poseDefs[def.id] = def;
            }.bind(this));
            
            this.alt_costume.poses = poseDefs;

            var costumeTags = this.default_costume.tags.slice();
            var tagMods = $xml.find('tags');
            if (tagMods) {
                var newTags = [];
                tagMods.find('tag').each(function (idx, elem) {
                    var $elem = $(elem);
                    var tag = canonicalizeTag($elem.text());
                    var removed = $elem.attr('remove') || '';
                    var fromStage = $elem.attr('from');
                    var toStage = $elem.attr('to');

                    // Remove previous declarations for this tag
                    costumeTags = costumeTags.filter(function (t) { return t.tag !== tag; });

                    if (removed.toLowerCase() !== 'true') {
                        newTags.push({'tag': tag, 'from': fromStage, 'to': toStage});
                    }
                });
                
                Array.prototype.push.apply(costumeTags, newTags);
            }

            this.alt_costume.tags = costumeTags;

            this.onSelected(individual);
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
    
    this.alt_costume = null;
    this.selectAlternateCostume(null);
    this.resetState();
}

Opponent.prototype.loadCollectibles = function (onLoaded, onError) {
    if (!this.has_collectibles) return;
    if (this.collectibles !== null) return;
    
    $.ajax({
		type: "GET",
		url: this.folder + 'collectibles.xml',
		dataType: "text",
		success: function(xml) {
			var collectiblesArray = [];
			$(xml).find('collectible').each(function (idx, elem) {
				collectiblesArray.push(new Collectible($(elem), this));
            }.bind(this));
            
            this.collectibles = collectiblesArray;
            
            this.has_collectibles = this.collectibles.some(function (c) {
                return !c.status || includedOpponentStatuses[c.status];
            });
            
            if (onLoaded) onLoaded(this);
		}.bind(this),
        error: function (jqXHR, status, err) {
            console.error("Error loading collectibles for "+this.id+": "+status+" - "+err);
            if (onError) onError(this, status, err);
        }.bind(this)
	});
}

/* Called prior to removing a character from the table. */
Opponent.prototype.unloadOpponent = function () {
    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            category: 'select',
            message: 'Unloading opponent ' + this.id,
            level: 'info'
        });
    }

    if (this.stylesheet) {
        /* Remove the <link> to this opponent's stylesheet. */
        $('link[href=\"'+this.stylesheet+'\"]').remove();
    }
}

Opponent.prototype.fetchBehavior = function() {
    // Optionally, replace with fetchCompressedURL(this.folder + "behaviour.xml")
    return $.ajax({
        type: "GET",
        url: this.folder + "behaviour.xml",
        dataType: "text",
    });
}

/************************************************************
 * Loads and parses the start of the behaviour XML file of the
 * given opponent.
 *
 * The onLoadFinished parameter must be a function capable of
 * receiving a new player object and a slot number.
 ************************************************************/
Opponent.prototype.loadBehaviour = function (slot, individual) {
    this.slot = slot;
    if (this.isLoaded()) {
        if (this.selected_costume) {
            this.loadAlternateCostume();
        } else {
            this.unloadAlternateCostume();
            setTimeout(this.onSelected.bind(this), 1, individual);
        }
        return;
    }

		/* Success callback.
         * 'this' is bound to the Opponent object.
         */
    this.fetchBehavior()
        .then(function(xml) {
            var $xml = $(xml);

            if (SENTRY_INITIALIZED) {
                Sentry.addBreadcrumb({
                    category: 'select',
                    message: 'Fetched and parsed opponent ' + this.id + ', initializing...',
                    level: 'info'
                });
            }

            if (this.has_collectibles) {
                this.loadCollectibles();
            }
            
            this.xml = $xml;
            this.size = $xml.find('size').text();
            this.stamina = Number($xml.find('timer').text());
            this.intelligence = $xml.find('intelligence');

            /* Load in the legacy "start" lines, and also
             * initialize player.chosenState to the first listed line.
             * This may be overridden by later updateBehaviour calls if
             * the player has (new-style) selected or game start case lines.
             */
            this.startStates = $xml.children('start').children('state').get().map(function (el) {
                return new State($(el));
            });

            this.stylesheet = null;
            
            var stylesheet = $xml.find('stylesheet').text();
            if (stylesheet) {
                var m = stylesheet.match(/[a-zA-Z0-9()~!*:@,;\-.\/]+\.css/i);
                if (m) {
                    this.stylesheet = 'opponents/'+this.id+'/'+m[0];
                }
            }

            /* The gender listed in meta.xml and behaviour.xml might differ
             * (for example with gender-revealing characters)
             * So assume behaviour.xml holds the 'definitive' starting gender
             * for the character.
             */
            var startGender = $xml.find('gender').text();
            if (startGender) {
                this.gender = startGender;    
            }

            this.default_costume = {
                id: null,
                labels: $xml.find('label'),
                tags: null,
                folders: this.folder,
                wardrobe: $xml.find('wardrobe')
            };
            
            var poses = $xml.find('poses');
            var poseDefs = {};
            $(poses).find('pose').each(function (i, elem) {
                var def = new PoseDefinition($(elem), this);
                poseDefs[def.id] = def;
            }.bind(this));
            
            this.default_costume.poses = poseDefs;

            var tags = $xml.find('tags');
            var tagsArray = [];
            if (typeof tags !== typeof undefined && tags !== false) {
                tagsArray = $(tags).find('tag').map(function () {
                    return {
                        'tag': canonicalizeTag($(this).text()),
                        'from': $(this).attr('from'),
                        'to': $(this).attr('to'),
                    }
                }).get();
            }

            this.default_costume.tags = tagsArray;

            /* Load forward-declarations for persistent markers. */
            var persistentMarkers = $xml.find('persistent-markers');
            if (typeof persistentMarkers !== typeof undefined && persistentMarkers) {
                $(persistentMarkers).find('marker').each(function (i, elem) {
                    var markerName = $(elem).text();
                    this.persistentMarkers[markerName] = true;
                }.bind(this));
            }

            this.targetedLines = {};

            /* Clone cases with alternative conditions/test, keeping
             * one alternative set of conditions and tests on the case
             * level of each case clone. This may create multiple
             * cases with the same oneShotId, which is what we want,
             * because the case clones should still be seen as the
             * same case.
             *
             * This means that the conditions on the case element as
             * well as any condition and test elements outside of
             * alternatives must always be fulfilled, along with all
             * the conditions of tests inside any of the alternative
             * elements. */
            $xml.find('>behaviour case:has(>alternative)').each(function() {
                var $case = $(this);
                $case.children('alternative').each(function() {
                    // Make clone and insert after original case
                    var $clone = $case.clone().insertAfter($case);
                    // Remove all <alternative> elements from clone, leaving base conditions
                    $clone.children('alternative').remove();
                    // Append conditions from this alternative to cloned case
                    $clone.append($(this).children());
                    for (var i = 0; i < this.attributes.length; i++) {
                        $clone.attr(this.attributes[i].name, this.attributes[i].value);
                    }
                });
                $case.remove();
            });

            var nicknames = {};
            $xml.find('nicknames>nickname').each(function() {
                if ($(this).attr('for') in nicknames) {
                    nicknames[$(this).attr('for')].push($(this).text());
                } else {
                    nicknames[$(this).attr('for')] = [ $(this).text() ];
                }
            });
            this.nicknames = nicknames;

            if (this.xml.find('behaviour>trigger').length > 0) {
                var cachePromise = this.loadXMLTriggers();
            } else {
                var cachePromise = this.loadXMLStages();
            }

            cachePromise.progress(function (completed, total) {
                this.loadProgress = completed / total;
                mainSelectDisplays[this.slot - 1].updateLoadPercentage(this);
            });

            cachePromise.then(function () {
                this.loaded = true;

                if (this.selected_costume) {
                    return this.loadAlternateCostume();
                }

                return this.onSelected(individual);
            });
		}.bind(this))
		/* Error callback. */
        .fail(function(err) {
            console.log("Failed reading \""+this.id+"\" behaviour.xml");
            delete players[this.slot];
        }.bind(this));
}

Opponent.prototype.recordTargetedCase = function (caseObj) {
    var entities = new Set();

    if (caseObj.target) entities.add(caseObj.target);
    if (caseObj.alsoPlaying) entities.add(caseObj.alsoPlaying);
    if (caseObj.filter) entities.add(caseObj.filter);

    caseObj.counters.forEach(function (ctr) {
        if (ctr.id) entities.add(ctr.id);
        if (ctr.tag) entities.add(ctr.tag);
    });

    var lines = new Set();
    caseObj.states.forEach(function (s) {
        lines.add(s.rawDialogue);

        /* Handle the old persist-marker flag by adding all markers set with
         * persist-marker="true" to the persistentMarkers list.
         *
         * TODO: Remove this once all characters using persistent markers
         * have migrated over to the system in #74.
         */
        if (s.legacyPersistentFlag && s.marker && s.marker.name) {
            this.persistentMarkers[s.marker.name] = true;
        }
    }.bind(this));

    entities.forEach(function (ent) {
        if (!(ent in this.targetedLines)) {
            this.targetedLines[ent] = { count: 0, seen: new Set() };
        }

        lines.forEach(Set.prototype.add, this.targetedLines[ent].seen);
    }, this);
}

/**
 * Traverses a new-format opponent's behaviour <trigger> elements
 * and pre-emptively adds their Cases to the opponent's cases structure.
 * This is done in 50ms chunks to avoid blocking the UI.
 * 
 * @returns {$.Promise} A Promise. Progress callbacks are fired after each
 * chunk of work, and the promise resolves once all cases have been processed.
 * All callbacks are fired with the Opponent as `this`.
 */
Opponent.prototype.loadXMLTriggers = function () {
    var deferred = $.Deferred();

    var triggerQueue = this.xml.find('behaviour>trigger').get();
    if (triggerQueue.length <= 0) {
        deferred.resolveWith(this, [0]);
        return deferred.promise();
    }

    var loadItemsTotal = this.xml.find('behaviour>trigger>case').length;
    var loadItemsCompleted = 0;

    function process(tag, elemQueue) {
        var startTS = performance.now();

        /* break tasks into roughly 50ms chunks */
        while (performance.now() - startTS < 50) {
            while (elemQueue.length <= 0) {
                /* If triggerQueue is empty, then we are done. */
                if (triggerQueue.length <= 0) {
                    return deferred.resolveWith(this, [loadItemsCompleted]);
                }

                let $trigger = $(triggerQueue.shift());
                tag = $trigger.attr('id');
                elemQueue = $trigger.children('case').get();
            }

            let c = new Case($(elemQueue.shift()));
            this.recordTargetedCase(c);            

            c.getStages().forEach(function (stage) {
                var key = tag+':'+stage;
                if (!this.cases.has(key)) {
                    this.cases.set(key, []);
                }

                this.cases.get(key).push(c);
            }, this);

            loadItemsCompleted++;
        }

        deferred.notifyWith(this, [loadItemsCompleted, loadItemsTotal]);
        setTimeout(process.bind(this, tag, elemQueue), 50);
    }

    let $trigger = $(triggerQueue.shift());
    let tag = $trigger.attr('id');
    let cases = $trigger.children('case').get();

    setTimeout(process.bind(this, tag, cases), 0);
    return deferred.promise();
}

/**
 * Traverses an old-format opponent's behaviour <stage> elements
 * and pre-emptively adds their Cases to the opponent's cases structure.
 * This is done in 50ms chunks to avoid blocking the UI, similarly to
 * loadXMLTriggers.
 * 
 * @returns {$.Promise} A Promise. Progress callbacks are fired after each
 * chunk of work, and the promise resolves once all cases have been processed.
 * All callbacks are fired with the Opponent as `this`.
 */
Opponent.prototype.loadXMLStages = function (onComplete) {
    var deferred = $.Deferred();

    var stageQueue = this.xml.find('behaviour>stage').get();
    if (stageQueue.length <= 0) {
        deferred.resolveWith(this, [0]);
        return deferred.promise();
    }

    var loadItemsTotal = this.xml.find('behaviour>stage>case').length;
    var loadItemsCompleted = 0;

    function process(stage, elemQueue) {
        var startTS = performance.now();

        while (performance.now() - startTS < 50) {
            while (elemQueue.length <= 0) {
                if (stageQueue.length <= 0) {
                    return deferred.resolveWith(this, [loadItemsCompleted]);
                }

                let $stage = $(stageQueue.shift());
                stage = parseInt($stage.attr('id'), 10);
                elemQueue = $stage.children('case').get();
            }

            let c = new Case($(elemQueue.shift()));
            this.recordTargetedCase(c);

            var key = c.tag + ':' + stage;
            if (!this.cases.has(key)) {
                this.cases.set(key, []);
            }

            this.cases.get(key).push(c);
            
            loadItemsCompleted++;
        }

        deferred.notifyWith(this, [loadItemsCompleted, loadItemsTotal])
        setTimeout(process.bind(this, stage, elemQueue), 50);
    }

    let $stage = $(stageQueue.shift());
    let stage = parseInt($stage.attr('id'), 10);
    let cases = $stage.children('case').get();

    setTimeout(process.bind(this, stage, cases), 0);
    return deferred.promise();
}

Player.prototype.getImagesForStage = function (stage) {
    if(!this.xml) return [];

    var poseSet = {};
    var imageSet = {};
    var folder = this.folders ? this.getByStage(this.folders, stage) : this.folder;
    var advPoses = this.poses;

    function processCase (c) {
        /* Skip cases requiring characters that aren't present. */
        if (c.target && !players.some(function (p) { return p.id === c.target; })) return; 
        if (c.alsoPlaying && !players.some(function (p) { return p.id === c.alsoPlaying; })) return;
        if (c.filter && !players.some(function (p) { return p.hasTag(c.filter); })) return;

        if (!c.counters.every(function (ctr) {
            var count = players.countTrue(function(p) {
                if (ctr.id && p.id !== ctr.id) return false;
                if (ctr.tag && !p.hasTag(ctr.tag)) return false;

                return true;
            });

            return inInterval(count, ctr.count);
        })) return;

        /* Collate pose names into poseSet. */
        c.getPossibleImages(stage === -1 ? 0 : stage).forEach(function (poseName) {
            poseSet[poseName] = true;
        });
    }

    if (stage > -1) {
        /* Find all cases that can play within this stage, then process
         * them.
         */

        var keySuffix = ':'+stage;
        this.cases.forEach(function (caseList, key) {
            if (!key.endsWith(keySuffix)) return;
            caseList.forEach(processCase);
        });
    } else {
        /* Get all poses within the game start states. */
        this.startStates.forEach(function (state) {
            state.getPossibleImages(0).forEach(function (poseName) {
                poseSet[poseName] = true;
            });
        });

        if (this.cases.has(GAME_START + ':0')) {
            this.cases.get(GAME_START + ':0').forEach(processCase);
        }
    }

    /* Finally, transform the set of collected pose names into a
     * set of image file paths.
     */
    Object.keys(poseSet).forEach(function (poseName) {
        if (poseName.startsWith('custom:')) {
            var key = poseName.split(':', 2)[1];
            var pose = advPoses[key];
            if (pose) pose.getUsedImages().forEach(function (img) {
                imageSet[img.replace('#', stage)] = true;
            });
        } else {
            imageSet[folder + poseName] = true;
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
    players[HUMAN_PLAYER] = humanPlayer = new Player('human'); //createNewPlayer("human", "", "", "", eGender.MALE, eSize.MEDIUM, eIntelligence.AVERAGE, 20, undefined, [], null);
    humanPlayer.slot = HUMAN_PLAYER;

    /* Generate a random session ID. */
    sessionID = generateRandomID();

	/* enable table opacity */
	tableOpacity = 1;
	$gameTable.css({opacity:1});

    /* Load title screen info first, since that's fast and synchronous */
    loadTitleScreen();
    selectTitleCandy();

    /* Attempt to detect broken images as caused by running SPNATI from an invalid archive. */
    detectBrokenOffline();
    
    /* Make sure that the config file is loaded before processing the
     *  opponent list, so that includedOpponentStatuses is populated.
     *
     * Also ensure that the config file is loaded before initializing Sentry,
     * which requires the commit SHA.
     * 
     * Also: .done() and .always() do not chain like .then() does.
     */
    loadConfigFile().then(loadBackgrounds, loadBackgrounds).always(
        loadVersionInfo,
        loadGeneralCollectibles,
        loadSelectScreen,
        function () {
            /* Make sure that save data is loaded before updateTitleGender(),
             * since the latter uses selectedClothing.
             */
            save.load();
            updateTitleGender();
        },
        function () {
            if (USAGE_TRACKING && !SENTRY_INITIALIZED) sentryInit();
        }
    );

    if (SENTRY_INITIALIZED) Sentry.setTag("screen", "warning");

	/* show the title screen */
	$titleScreen.show();
	$('#warning-start-button').focus();
    autoResizeFont();
	/* set up future resizing */
	window.onresize = autoResizeFont;

    /* Construct a CSS rule for every combination of arrow direction, screen, and pseudo-element */
    bubbleArrowOffsetRules = [];
    var targetCssSheet = document.getElementById("spniStyleSheet").sheet;

    for (var i = 1; i <= 4; i++) {
        var pair = [];
        [["up", "down"], ["left", "right"]].forEach(function(p) {
            var index = targetCssSheet.cssRules.length;
            var rule = p.map(function(d) {
                return ["select", "game"].map(function(s) {
                    return '#'+s+'-bubble-'+i+'.arrow-'+d+'::before';
                }).join(', ');
            }).join(', ') + ' {}';
            targetCssSheet.insertRule(rule, index);
            pair.push(targetCssSheet.cssRules[index]);
        });
        bubbleArrowOffsetRules.push(pair);
    }
    $(document).keydown(function(ev) {
        if (ev.keyCode == 9) {  // Tab
            $("body").addClass('focus-indicators-enabled');
        }
    });
    $(document).keyup(function(ev) {
        if ((ev.key == 'f' || (ev.key == 'F' && !ev.shiftKey))
            && !$(document.activeElement).is('input, select, textarea')) {
            toggleFullscreen();
        } else if (ev.keyCode == 112) { // F1
            showHelpModal();
        }
    });
    $(document).mousedown(function(ev) {
        $("body").removeClass('focus-indicators-enabled');
    });
    $(window).on('beforeunload', function() {
        if (inGame) {
            event.preventDefault();
            event.returnValue = '';
        }
    });

    $('[data-toggle="tooltip"]').tooltip({ delay: { show: 200 } });
}

function loadVersionInfo () {
    $('.substitute-version').text('Unknown Version');
    
    return $.ajax({
        type: "GET",
		url: "version-info.xml",
		dataType: "text",
		success: function(xml) {
            versionInfo = $(xml);
            CURRENT_VERSION = versionInfo.find('current').attr('version');

            if (SENTRY_INITIALIZED) Sentry.setTag("game_version", CURRENT_VERSION);
            
            $('.substitute-version').text('v'+CURRENT_VERSION);
            console.log("Running SPNATI version "+CURRENT_VERSION);
            
            version_ts = versionInfo.find('changelog > version[number=\"'+CURRENT_VERSION+'\"]').attr('timestamp');        
            
            version_ts = parseInt(version_ts, 10);
            now = Date.now();
            
            elapsed_time = now - version_ts;
            
            /* Format last update time */
            last_update_string = '';
            if (elapsed_time < 5 * 60 * 1000) {
                // <5 minutes ago - display 'just now'
                last_update_string = 'just now';
            } else if (elapsed_time < 60 * 60 * 1000) {
                // < 1 hour ago - display minutes since last update
                last_update_string = Math.floor(elapsed_time / (60 * 1000))+' minutes ago';
            } else if (elapsed_time < 24 * 60 * 60 * 1000) {
                // < 1 day ago - display hours since last update
                var n_hours = Math.floor(elapsed_time / (60 * 60 * 1000));
                last_update_string = n_hours + (n_hours === 1 ? ' hour ago' : ' hours ago');
            } else {
                // otherwise just display days since last update
                var n_days = Math.floor(elapsed_time / (24 * 60 * 60 * 1000));
                last_update_string =  n_days + (n_days === 1 ? ' day ago' : ' days ago');
            }
            
            $('.substitute-version-time').text('(updated '+last_update_string+')');

            $('.version-button').click(showVersionModal);
        }
    });
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

            var _default_fill_mode = $(xml).find('default-fill').text();
            if (!_default_fill_mode || _default_fill_mode === 'none') {
                DEFAULT_FILL = undefined;
                console.log("Startup table filling disabled");
            } else {
                DEFAULT_FILL = _default_fill_mode;
                console.log("Using startup table fill mode " + DEFAULT_FILL + '.');
            }

            var _game_commit = $(xml).find('commit').text();
            if (_game_commit) {
                VERSION_COMMIT = _game_commit;
                console.log("Running SPNATI commit "+VERSION_COMMIT+'.');
            } else {
                console.log("Could not find currently deployed Git commit!");
            }

            var _version_tag = $(xml).find('version-tag').text();
            if (_version_tag) {
                VERSION_TAG = _version_tag;
                console.log("Running SPNATI production version " + VERSION_TAG + '.');
            } else {
                console.log("Could not find currently deployed production version tag!");
            }

            var _default_bg = $(xml).find('default-background').text();
            if (_default_bg) {
                defaultBackgroundID = _default_bg;
                console.log("Using default background: "+defaultBackgroundID);
            } else {
                defaultBackgroundID = 'inventory';
                console.log("No default background ID set, defaulting to 'inventory'...");
            }

            var _alts = $(xml).find('alternate-costumes').text();

            if(_alts === "true") {
                ALT_COSTUMES_ENABLED = true;
                console.log("Alternate costumes enabled");

                var _costume_badges = $(xml).find('costume_badges').text();
                if (_costume_badges.toLowerCase() === 'false') {
                    COSTUME_BADGES_ENABLED = false;
                    console.log("Alternate costume badges are disabled.");
                } else {
                    console.log("Alternate costume badges are enabled.");
                    COSTUME_BADGES_ENABLED = true;
                }
                
                DEFAULT_COSTUME_SET = $(xml).find('default-costume-set').text();
                if (DEFAULT_COSTUME_SET) {
                    console.log("Defaulting to alternate costume set: "+DEFAULT_COSTUME_SET);
                    alternateCostumeSets[DEFAULT_COSTUME_SET] = true;
                }

                $(xml).find('alternate-costume-sets').each(function () {
                    var set = $(this).text();
                    alternateCostumeSets[set] = true;
                    if (set === 'all') {
                        console.log("Including all alternate costume sets");
                    } else {
                        console.log("Including alternate costume set: "+set);
                    }
                });
            } else {
                ALT_COSTUMES_ENABLED = false;
                console.log("Alternate costumes disabled");
            }
            
            COLLECTIBLES_ENABLED = false;
            COLLECTIBLES_UNLOCKED = false;
            
            if ($(xml).find('collectibles').text() === 'true') {
                COLLECTIBLES_ENABLED = true;
                console.log("Collectibles enabled");
                
                if ($(xml).find('collectibles-unlocked').text() === 'true') {
                    COLLECTIBLES_UNLOCKED = true;
                    console.log("All collectibles force-unlocked");
                }
            } else {
                console.log("Collectibles disabled");
            }
            
            var _resort_mode = $(xml).find('resort').text();
            if (_resort_mode.toLowerCase() === 'true') {
                console.log("Resort mode active!");
                RESORT_ACTIVE = true;
            } else {
                RESORT_ACTIVE = false;
                console.log("Resort mode disabled.");
            }

            includedOpponentStatuses.online = true;
			$(xml).find('include-status').each(function() {
				includedOpponentStatuses[$(this).text()] = true;
				console.log("Including", $(this).text(), "opponents");
			});
        }
	});
}

function loadGeneralCollectibles () {
    return $.ajax({
		type: "GET",
		url: 'opponents/general_collectibles.xml',
		dataType: "text",
		success: function(xml) {
			var collectiblesArray = [];
			$(xml).find('collectible').each(function (idx, elem) {
				generalCollectibles.push(new Collectible($(elem), undefined));
			});
		},
        error: function (jqXHR, status, err) {
            console.error("Error loading general collectibles: "+status+" - "+err);
        }
	});
}

/**
 * Attempt to detect common ways of incorrectly running the offline version.
 * Specifically, we check the following:
 * - Can we load resources using XHR?
 * - Have image LFS pointers been properly replaced with actual content?
 * If either of these checks fail, the broken offline modal is shown.
 */
function detectBrokenOffline() {
    $("#broken-offline-modal .section").hide();

    $.ajax({
        type: "GET",
        url: "img/enter.png",
        dataType: "text",
        success: function (data) {
            if (data.startsWith("version ")) {
                /* Returned data is indicative of an LFS pointer file.
                 * PNG files start with a different 8-byte signature.
                 */
                $("#broken-images-section").show();
                $("#broken-offline-modal").modal('show');
            }
        },
        error: function (jqXHR, status, err) {
            $("#broken-xhr-section").show();
            $("#broken-offline-modal").modal('show');
        }
    });

    var img = new Image();
    img.onerror = function () {
        $("#broken-images-section").show();
    }

    img.src = "img/enter.png";
}

function enterTitleScreen() {
    $warningContainer.hide();
    $titleContainer.show();
    $('.title-candy').show();
    $('#title-start-button').focus();
    if (SENTRY_INITIALIZED) Sentry.setTag("screen", "title");
}

/************************************************************
 * Transitions between two screens.
 ************************************************************/
function screenTransition (first, second) {
    if (first.data('keyhandler')) {
        $(document).off('keyup', first.data('keyhandler'));
    }
    first.hide();
    second.show();
    if (second.data('keyhandler')) {
        $(document).on('keyup', second.data('keyhandler'));
    }
    autoResizeFont();
}

/************************************************************
 * Switches to the next screen based on the screen provided.
 ************************************************************/
function advanceToNextScreen (screen) {
    if (screen == $titleScreen) {
        /* advance to the select screen */
        screenTransition(screen, $selectScreen);
    } else if (screen == $selectScreen) {
        /* advance to the main game screen */
        loadGameScreen();
        screenTransition(screen, $gameScreen);
        $mainButton.focus();
    }
}

/************************************************************
 * Switches to the last screen based on the screen provided.
 ************************************************************/
function returnToPreviousScreen (screen) {
    if (screen == $selectScreen) {
        /* return to the title screen */
        screenTransition($selectScreen, $titleScreen);
    }
}

/************************************************************
 * Resets the game state so that the game can be restarted.
 ************************************************************/
function resetPlayers () {
    players.forEach(function(p) {
        p.resetState();
    });
    updateAllBehaviours(null, null, SELECTED);
}

/************************************************************
 * Restarts the game.
 ************************************************************/
function restartGame () {
    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            category: 'ui',
            message: 'Returning to title screen.',
            level: 'info'
        });

        Sentry.setTag("screen", "title");
        Sentry.setTag("epilogue_player", undefined);
        Sentry.setTag("epilogue", undefined);
        Sentry.setTag("epilogue_gallery", undefined);
    }

	clearTimeout(timeoutID); // No error if undefined or no longer valid
	timeoutID = autoForfeitTimeoutID = undefined;
	stopCardAnimations();
	resetPlayers();

	/* enable table opacity */
	tableOpacity = 1;
	$gameTable.css({opacity:1});
    $gamePlayerCardArea.show();
    $gamePlayerClothingArea.css('display', '');  /* Reset to default so as not to interfere with 
                                                    switching between classic and minimal UI. */
    inGame = false;

    if (SENTRY_INITIALIZED) {
        Sentry.setTag("in_game", false);
    }

	/* trigger screen refreshes */
	updateSelectionVisuals();
	updateAllGameVisuals();
    selectTitleCandy();

    forceTableVisibility(true);

    /* there is only one call to this right now */
    $epilogueSelectionModal.hide();
    clearEpilogue();
    screenTransition($epilogueScreen, $titleScreen);
    screenTransition($gameScreen, $titleScreen);
    autoResizeFont();
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
            $('#bug-report-type').empty();
            closeBugReportModal();
        }
    });
}

$('#bug-report-type').change(updateBugReportOutput);
$('#bug-report-desc').change(updateBugReportOutput);
$('#bug-report-copy-btn').click(copyBugReportOutput);

if (!document.fullscreenEnabled) {
    $('.title-fullscreen-button, .game-menu-dropup li:has(#game-fullscreen-button), #epilogue-fullscreen-button').hide();
}

 /************************************************************
  * The player clicked a bug-report button. Shows the bug reports modal.
  ************************************************************/
function showBugReportModal () {
    var prevVal = $('#bug-report-type').val();
    /* Set up possible bug report types. */
    var bugReportTypes = [
        ['freeze', 'Game Freeze or Crash'],
        ['display', 'Game Graphical Problem'],
        ['other', 'Other Game Issue'],
    ].concat((epiloguePlayer ? [ epiloguePlayer.epilogue.player ] : players.opponents).map(function(p) {
        return [ 'character:'+p.id, (epiloguePlayer ? 'Epilogue' : 'Character') + ' Defect ('+p.id.initCap()+')'];
    }));

    $('#bug-report-type').empty().append(bugReportTypes.map(function(item) {
        return $('<option>', { value: item[0], text: item[1] });
    }));
    if (prevVal && bugReportTypes.some(function(t) { return t[0] === prevVal; })) {
        $('#bug-report-type').val(prevVal);
    } else if (epiloguePlayer) {
        $('#bug-report-type').val('character:'+epiloguePlayer.epilogue.player.id);
    }

    updateBugReportOutput();

    $bugReportModal.modal('show');
}

$bugReportModal.on('shown.bs.modal', function() {
	$('#bug-report-type').focus();
});

function closeBugReportModal() {
    $bugReportModal.modal('hide');
}

 /************************************************************
  * Functions for the feedback reporting modal.
  ************************************************************/

function sendFeedbackReport() {
    if ($('#feedback-report-desc').val().trim().length == 0) {
        $('#feedback-report-status').text("Please enter a description first!");
        return false;
    }

    var desc = $('#feedback-report-desc').val();
    var character = $('#feedback-report-character').val();
    var report = compileBaseErrorReport(desc, "feedback");

    $.ajax({
        url: FEEDBACK_ROUTE + (character || ""),
        method: 'POST',
        data: JSON.stringify(report),
        contentType: 'application/json',
        error: function (jqXHR, status, err) {
            console.error("Could not send feedback report - error " + status + ": " + err);
            $('#feedback-report-status').text("Failed to send feedback report (error " + err + ")");
        },
        success: function () {
            $('#feedback-report-status').text("Feedback sent!");
            $('#feedback-report-desc').val("");
            $('#feedback-report-character').empty()
            closeFeedbackReportModal();
        }
    });
}

function updateFeedbackSendButton() {
    if (
        !!$("#feedback-report-character").val() &&
        $('#feedback-report-desc').val().trim().length > 0
    ) {
        $("#feedback-report-modal-send-button").removeAttr('disabled');
    } else {
        $("#feedback-report-modal-send-button").attr('disabled', 'true');
    }
}

$('#feedback-report-desc').keyup(updateFeedbackSendButton).change(updateFeedbackSendButton);

function updateFeedbackMessage() {
    var player = $('#feedback-report-character option:selected').data('character');

    $(".feedback-message-container").hide();
    $("#feedback-disabled-warning").hide();

    if (player && player.feedbackData) {
        if (player.feedbackData.enabled && player.feedbackData.message) {
            $(".feedback-message-container").show();
            $(".feedback-character-name").text(player.label);
            $(".feedback-message").text(player.feedbackData.message);
        } else if (!player.feedbackData.enabled) {
            $("#feedback-disabled-warning").show();
        }
    }
}

$("#feedback-report-character").change(updateFeedbackMessage);

function showFeedbackReportModal($fromModal) {
    var prevVal = $('#feedback-report-character').val();
    $('#feedback-report-character').empty().append(
        $('<option disabled data-load-indicator="">Loading...</option>'),
        $('<option value="">General Game Feedback</option>')
    );

    var feedbackCharacters = epiloguePlayer && !inGame ? [ epiloguePlayer.epilogue.player ] : players.opponents;

    $.when.apply($, feedbackCharacters.map(function(p) {
        $("#feedback-report-character").append($('<option>', { text: p.id.initCap(), value: p.id }).data('character', p));
        if (p.feedbackData) {
            return true;
        } else {
            return $.ajax({
                url: FEEDBACK_ROUTE + p.id,
                type: "GET",
                dataType: "json",
            }).then(function(data) {
                p.feedbackData = data;
            }, function() {
                console.error("Failed to get feedback message data for " + p.id);
                return $.Deferred().resolve().promise(); /* This is meant to avoid hiding the "Loading..." 
                                                            entry right away if one GET fails. */
            });
        }
    })).then(function() {
        $("#feedback-report-character option[data-load-indicator]").remove();
        if (prevVal && feedbackCharacters.indexOf(prevVal) >= 0) {
            $('#feedback-report-character').val(prevVal);
        } else if (epiloguePlayer) {
            $('#feedback-report-character').val(epiloguePlayer.epilogue.player.id);
        }
        updateFeedbackMessage();
    });

    if ($fromModal) {
        $fromModal.modal('hide');
        $feedbackReportModal.one('hide.bs.modal', function() {
            $fromModal.modal('show');
        });
    }
    $feedbackReportModal.modal('show');
}

function closeFeedbackReportModal() {
    $feedbackReportModal.modal('hide');
}

$feedbackReportModal.on('shown.bs.modal', function () {
    $('#feedback-report-character').focus();
});

/*
 * Show the usage tracking consent modal.
 */

function showUsageTrackingModal() {
    $usageTrackingModal.modal('show');
}

function enableUsageTracking() {
    USAGE_TRACKING = true;
    save.saveUsageTracking();
    sentryInit();
}

function disableUsageTracking() {
    USAGE_TRACKING = false;
    save.saveUsageTracking();
}

function sentryInit() {
    if (USAGE_TRACKING && !SENTRY_INITIALIZED) {
        console.log("Initializing Sentry...");

        var sentry_opts = {
            dsn: 'https://df511167a4fa4a35956a8653ff154960@sentry.io/1508488',
            release: VERSION_TAG,
            maxBreadcrumbs: 100,
            integrations: [new Sentry.Integrations.Breadcrumbs({
                console: false,
                dom: false
            })],
            beforeSend: function (event, hint) {
                /* Inject additional game state data into event tags: */
                if (!event.extra) event.extra = {};

                event.tags.commit = VERSION_COMMIT;

                if (inGame && !epiloguePlayer) {
                    event.extra.recentLoser = recentLoser;
                    event.extra.previousLoser = previousLoser;
                    event.extra.gameOver = gameOver;
                    event.extra.currentTurn = currentTurn;
                    event.extra.currentRound = currentRound;

                    event.tags.rollback = inRollback();
                    event.tags.gamePhase = getGamePhaseString(gamePhase);
                }

                if (epiloguePlayer) {
                    event.tags.epilogue = epiloguePlayer.epilogue.title;
                    event.tags.epilogue_player = epiloguePlayer.epilogue.player.id;
                    event.tags.epilogue_gender = epiloguePlayer.epilogue.gender;

                    event.extra.loaded = epiloguePlayer.loaded;
                    event.extra.directiveIndex = epiloguePlayer.directiveIndex;
                    event.extra.sceneIndex = epiloguePlayer.sceneIndex;
                    event.extra.viewIndex = epiloguePlayer.viewIndex;
                }

                var n_players = 0;
                for (var i=1;i<players.length;i++) {
                    if (players[i]) {
                        n_players += 1;
                        event.tags["character:" + players[i].id] = true;
                        event.tags["slot-" + i] = players[i].id;

                        if (players[i].alt_costume) {
                            event.tags[players[i].id+":alt-costume"] = players[i].alt_costume.id;
                        }
                    } else {
                        event.tags["slot-" + i] = undefined;
                    }
                }

                event.tags.n_players = n_players;

                return event;
            }
        };

        if (window.location.origin.indexOf('spnati.net') >= 0) {
            sentry_opts.environment = 'production';
        }

        Sentry.init(sentry_opts);

        Sentry.setUser({
            'id': sessionID,
        });

        Sentry.setTag("game_version", CURRENT_VERSION);

        SENTRY_INITIALIZED = true;
    }
}

var SEMVER_RE = /[vV]?(\d+)\.(\d+)(?:\.(\d+))?(?:\-([a-zA-Z0-9\-]+(?:\.[a-zA-Z0-9\-])*))?(?:\+([a-zA-Z0-9\-]+(?:\.[a-zA-Z0-9\-]+)*))?/;
function parseSemVer (versionString) {
    var m = versionString.match(SEMVER_RE);
    if (!m) return null;
    
    var ver = {
        'major': parseInt(m[1], 10) || 0,
        'minor': parseInt(m[2], 10) || 0,
        'patch': parseInt(m[3], 10) || 0,
    }
    
    if (m[4]) {
        ver.prerelease = m[4].split('.');
    }
    
    return ver;
}

/* Implements semver precedence rules, as specified in the Semantic Versioning 2.0.0 spec. */
function compareVersions (v1, v2) {
    var m1 = parseSemVer(v1);
    var m2 = parseSemVer(v2);
    
    // compare major - minor - patch first, in that order
    if (m1.major !== m2.major) return (m1.major > m2.major) ? 1 : -1;
    if (m1.minor !== m2.minor) return (m1.minor > m2.minor) ? 1 : -1;
    if (m1.patch !== m2.patch) return (m1.patch > m2.patch) ? 1 : -1;
    
    // prerelease versions always have less precedence than release versions
    if (!m1.prerelease && m2.prerelease) return 1;
    if (!m2.prerelease && m1.prerelease) return -1;
    
    // Compare pre-release identifiers from left to right
    for (var i=0;i<Math.min(m1.prerelease.length, m2.prerelease.length);i++) {
        var pr1 = parseInt(m1.prerelease[i], 10);
        var pr2 = parseInt(m2.prerelease[i], 10);
        
        if (m1.prerelease[i] !== m2.prerelease[i]) {
            // if both are numerical or both are non-numeric
            if (isNaN(pr1) === isNaN(pr2)) return (pr1 > pr2) ? 1 : -1;
            
            // otherwise, if we're comparing numeric to non-numeric,
            // numeric PR identifiers always have less precedence than non-numeric
            return isNaN(pr1) ? 1 : -1;
        }
    }
    
    // All pre-release identifiers to now compared equal
    // in this case, the version with a larger set of pre-release fields has
    // higher precedence
    if (m1.prerelease.length !== m2.prerelease.length) {
        return (m1.prerelease.length > m2.prerelease.length) ? 1 : -1;
    }
    
    // If all else fails, both versions compare equal
    return 0;
}

$creditModal.on('shown.bs.modal', function() {
	$('#credit-modal-button').focus();
});

/************************************************************
 * The player clicked the version button. Shows the version modal.
 ************************************************************/
function showVersionModal () {
    var $changelog = $('#changelog-container');
    var entries = [];
    
    /* Get changelog info: */
    versionInfo.find('changelog > version').each(function (idx, elem) {
        entries.push({
            version: $(elem).attr('number'),
            timestamp: parseInt($(elem).attr('timestamp'), 10) || undefined,
            text: $(elem).text()
        });
    });
    
    /* Construct the version modal DOM: */
    $changelog.empty().append(entries.sort(function (e1, e2) {
        // Sort in reverse-precedence order
        return compareVersions(e1.version, e2.version) * -1;
    }).map(function (ent) {
        var row = document.createElement('tr');
        var versionCell = document.createElement('td');
        var dateCell = document.createElement('td');
        var logTextCell = document.createElement('td');
        
        versionCell.className = 'changelog-version-label';
        dateCell.className = 'changelog-date-label';
        logTextCell.className = 'changelog-entry-text';
        
        if (ent.timestamp) {
            var date = new Date(ent.timestamp);
            var locale = window.navigator.userLanguage || window.navigator.language
            dateCell.innerText = date.toLocaleString(locale, {'dateStyle': 'medium', 'timeStyle': 'short'});
        }
        
        versionCell.innerText = ent.version;
        logTextCell.innerText = ent.text;
        
        row.appendChild(versionCell);
        row.appendChild(dateCell);
        row.appendChild(logTextCell);
        
        return row;
    }));
    
    $versionModal.modal('show');
}

$versionModal.on('shown.bs.modal', function() {
	$('#version-modal-button').focus();
});

/************************************************************
 * The player clicked the help / FAQ button. Shows the help modal.
 ************************************************************/
function showHelpModal () {
    $helpModal.modal('show');
}

function gotoHelpPage (toPage) {
    var curPage = $helpModal.attr('data-current-page');
    curPage = parseInt(curPage, 10) || 1;
    
    if (toPage === 'prev') {
        curPage = (curPage > 1) ? curPage-1 : 1;
    } else if (toPage === 'next') {
        curPage = (curPage < 8) ? curPage+1 : 8;
    } else {
        curPage = toPage;
    }
    
    $helpModal.attr('data-current-page', curPage);
    $('.help-page').hide();
    $('.help-page[data-page="'+curPage+'"]').show();
    $('.help-page-select').removeClass('active');
    $('.help-page-select[data-page="'+curPage+'"]').addClass('active');
}

$('.help-page-select').click(function (ev) {
    gotoHelpPage($(ev.target).attr('data-select-page'));
})

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
            var $select = $('<select>', { name: choiceName, id: 'player-tag-choice-'+choiceName });
            $select.append('<option>', playerTagOptions[choiceName].values.map(function(opt) {
                return $('<option>').val(opt.value).addClass(opt.gender).append(opt.text || opt.value.replace(/_/g, ' ').initCap());
            }));
            if ($existing.length) {
                $existing.parent().replaceWith($select);
            } else {
                var $label = $('<div class="player-tag-select">');
                $label.append($('<label>', { 'for': 'player-tag-choice-' + choiceName,
                                             'text': 'Choose your ' + choiceName.replace(/_/g, ' ') + ':'}));
                if (playerTagOptions[choiceName].gender) {
                    $select.addClass(playerTagOptions[choiceName].gender);
                    $label.addClass(playerTagOptions[choiceName].gender);
                }
                $label.append($select);
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
            if (!('gender' in playerTagOptions[choiceName]) || playerTagOptions[choiceName].gender == humanPlayer.gender) {
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

function toggleFullscreen() {
    if (document.fullscreenElement) {
        document.exitFullscreen();
    } else {
        /* handle vendor prefixes for out of date browsers
         * (probably don't need -moz- prefix though, according to Sentry data)
         */
        var d = document.documentElement;
        if (d.requestFullscreen) {
            d.requestFullscreen();
        } else if (d.webkitRequestFullScreen) {
            d.webkitRequestFullScreen();
        } else if (d.msRequestFullscreen) {
            d.msRequestFullscreen();
        }
    }
}

$(':root').on('dblclick', ':input, .dialogue-bubble, .modal-dialog, .selection-card, .bordered, #epilogue-screen', function(ev) {
    ev.stopPropagation();
});
$(':root').on('dblclick', toggleFullscreen);

/************************************************************
 * The player clicked on a Load/Save button.
 ************************************************************/
function showImportModal() {
    $("#export-code").val(save.serializeStorage());
    
    $('#import-invalid-code').hide();

    if (codeImportEnabled) {
        $('#import-progress').prop('disabled', false);
        $('#import-restriction-warning').hide();
    } else {
        $('#import-progress').prop('disabled', true);
        $('#import-restriction-warning').show();
    }
    
    $ioModal.modal('show');

    $('#import-progress').click(function() {
        var code = $("#export-code").val();

        if (SENTRY_INITIALIZED) {
            Sentry.addBreadcrumb({
                category: 'ui',
                message: 'Loading save code...',
                level: 'info'
            });
        }

        if (save.deserializeStorage(code)) {
            $ioModal.modal('hide');
        } else {
            $('#import-invalid-code').show();
        }
    });
}

function showResortModal() {
    var playedCharacters = save.getPlayedCharacterSet();
    
    /* NOTE: Vis is a slepy boi */
    if (RESORT_ACTIVE && playedCharacters.length >= 30) {
        if (!save.hasShownResortModal()) {
            $resortModal.modal('show');
        }
        save.setResortModalFlag(true);
    } else {
        save.setResortModalFlag(false);
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

function mergeObjects(a, b){
	if(b === undefined || b === null){
		return a;
	}
	else if(a === undefined || a === null){
		return b;
	}
	for(var v in b){
		if (typeof a[v] === 'object') {
			a[v] = mergeObjects(a[v], b[v])
		} else {
			a[v] = b[v];
		}
	}
	return a;
}

/************************************************************
 * Changes the first letter in a string to upper case.
 ************************************************************/
String.prototype.initCap = function() {
	return this.substr(0, 1).toUpperCase() + this.substr(1);
}

// Polyfills for IE
if (!String.prototype.startsWith) {
    Object.defineProperty(String.prototype, 'startsWith', {
        value: function(search, pos) {
            pos = !pos || pos < 0 ? 0 : +pos;
            return this.substring(pos, pos + search.length) === search;
        }
    });
}

if (!String.prototype.endsWith) {
    String.prototype.endsWith = function (search, this_len) {
        if (this_len === undefined || this_len > this.length) {
            this_len = this.length;
        }
        return this.substring(this_len - search.length, this_len) === search;
    };
}

if (!Object.entries) {
    Object.entries = function (obj) {
        var ownProps = Object.keys(obj),
            i = ownProps.length,
            resArray = new Array(i); // preallocate the Array
        while (i--)
            resArray[i] = [ownProps[i], obj[ownProps[i]]];

        return resArray;
    };
}

/************************************************************
 * Counts the number of elements that evaluate as true, or,
 * if a function is provided, passes the test implemented by it.
 ************************************************************/
Object.defineProperty(Array.prototype, 'countTrue', {
    value: function(func) {
        var count = 0;
        for (var i = 0; i < this.length; i++) {
            if (i in this
                && (func ? func(this[i], i, this) : this[i])) {
                count++;
            }
        }
        return count;
    }
});

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
 * Automatically adjusts the size of all font based on screen width.
 **/
function autoResizeFont ()
{
    var w = window.innerWidth, h = window.innerHeight;
    /* resize font. Note: the 3/2 threshold must match the @media CSS block */
    if ($('.wide-screen-container .screen:visible').width() && w / h >= 3/2) {
        // Calculate 4/3 of the height a normal screen would have ((16/9) / (4/3) = 4/3) 
        $(':root').css('font-size', ($('.screen:visible').width() / 4 * 3 / 100)+'px');
    } else if ($('.screen:visible').width()) {
        $(':root').css('font-size', ($('.screen:visible').width() / 100)+'px');
    } else {
        return;
    }

	if (backgroundImage && backgroundImage.height && backgroundImage.width) {
		if (h > (3/4) * w) {
			h = (3/4) * w;
		} else {
			w = 4 * h / 3;
		}
        if (activeBackground.viewport) {
            var scale = backgroundImage.height / (activeBackground.viewport.bottom - activeBackground.viewport.top);
            var offset = ((backgroundImage.height - activeBackground.viewport.bottom) - activeBackground.viewport.top) / 2;
            $("body").css("background-size", "auto " + Math.round(scale * h) + "px");
            $("body").css("background-position-y", "calc(50% + " + h * offset / backgroundImage.height + "px)");
        } else {
            var ar = backgroundImage.width / backgroundImage.height;
            if (ar > 4/3) {
                var scale = Math.sqrt(16/9 / ar);
                $("body").css("background-size", "auto " + Math.round(scale * h) + "px");
            } else {
                var scale = Math.sqrt(ar);
                $("body").css("background-size", Math.round(scale * w) + "px auto");
            }
            $("body").css("background-position-y", '');
        }
	}
}

$('.modal').on('show.bs.modal', function() {
	$('.screen:visible').find('button, input').attr('tabIndex', -1);
});

$('.modal').on('hidden.bs.modal', function() {
	$('.screen:visible').find('button, input').removeAttr('tabIndex');
});
