/********************************************************************************
 This file contains all of the variables and functions for the Player object, as
 well as definitions for opponent and group lisitings.
 ********************************************************************************/

/**********************************************************************
 * Enumerations
 **********************************************************************/

/**
 * Content statuses. Used all over the place to restrict content to offline-only
 * where needed.
 * 
 * @global
 * @typedef {(null|undefined|"online"|"testing"|"offline"|"incomplete")} ContentStatus
 */

/**
 * @global
 * @readonly
 * @enum {("male" | "female")}
 */
var eGender = {
    MALE: "male",
    FEMALE: "female"
};

/**
 * Possible player penis / breast sizes.
 * 
 * @global
 * @readonly
 * @enum {string}
 */
var eSize = {
    SMALL: "small",
    MEDIUM: "medium",
    LARGE: "large"
};

/**
 * Possible AI player intelligence levels (difficulty settings).
 * 
 * @global
 * @readonly
 * @enum {string}
 */
var eIntelligence = {
    /** 'Throw' intelligence: this player actively tries to lose. */
    THROW: "throw",

    /** 'Bad' intelligence: this player swaps cards at random. */
    BAD: "bad",

    /**
     * 'Average' intelligence: this player makes some attempt at winning, but
     * may make slightly suboptimal moves.
     */
    AVERAGE: "average",

    /** 'Good' intelligence: only attempts to get or improve on pairs. */
    GOOD: "good",

    /**
     * 'Best' intelligence: same as 'Good', but also keeps the highest-value
     * card when receiving bad hands.
     */
    BEST: "best"
};


/**
 * Represents any player in-game, including the player character.
 * 
 * @constructor
 * @global
 * 
 * @param {string} id The unique ID for this player. Corresponds to a folder
 * name for NPCs.
 * @param {jQuery} [$metaXml] The parsed `meta.xml` object for this character.
 * @param {ContentStatus} [status] The opponent status for this character.
 * @param {number} [releaseNumber] This character's release number.
 * @param {string} [highlightStatus] This character's highlight status.
 */
function Player (id, $metaXml, status, releaseNumber) {
    /** 
     * This player's unique ID.
     * @type {string}
     */
    this.id = id;

    /**
     * The current folder from which data for this character should be loaded.
     * May change during the course of a game, if an alternate costume is
     * in use for this character.
     * @type {string}
     */
    this.folder = 'opponents/'+id+'/';

    /**
     * The core folder from which data for this character can be loaded.
     * This will not change during the course of a game.
     * @type {string}
     */
    this.base_folder = 'opponents/'+id+'/';

    /** 
     * This opponent's first name, if any.
     * @type {string}
     */
    this.first = '';

    /** 
     * This opponent's last name, if any. 
     * @type {string} 
     */
    this.last = '';

    /** 
     * A jQuery selector object matching the <label> elements for this character.
     * @type {jQuery}
     */
    this.labels = undefined;

    /**
     * A jQuery selector object matching the <folder> elements for this character.
     * @type {jQuery}
     */
    this.folders = undefined;

    /** 
     * This player's current gender. 
     * @type {eGender}
     */
    this.gender = 'male';

    /**
     * This player's current penis or breast size.
     * @type {eSize} 
     */
    this.size = eSize.MEDIUM;

    /** 
     * This player's current AI difficulty level.
     * @type {eIntelligence}  
     */
    this.intelligence = eIntelligence.AVERAGE;

    /** 
     * This player's initial 'stamina' (forfeit stage duration). 
     * @type {number} 
     */
    this.stamina = 20;

    /**
     * This player's current image scaling factor, as a percentage
     * (i.e. 100 = full-scale). This scaling factor is applied to all player
     * images when rendered on-screen. 
     * @type {number}
     */
    this.scale = 100.0;

    /**
     * The current list of tags for this player, after considering
     * canonicalization, tag implications, and per-stage tag changes.
     * @type {string[]}
     */
    this.tags = [];

    /**
     * The 'base' list of tags that have been specified for this player /
     * alternate costume.
     * 
     * Objects in this array contain both a tag name and an interval of stages 
     * that they apply to. Both ends of the stage interval ('from' and 'to') 
     * are optional.
     * 
     * Objects in this array can also just be plain old tag names.
     * @type {(string|{tag: string, from: string, to: string})[]}
     */
    this.baseTags = [];

    /**
     * The parsed `behaviour.xml` data for this character as a jQuery object.
     * If this is `null`, this character either has not been loaded, or is
     * the human player.
     * @type {?jQuery}
     */
    this.xml = null;

    /**
     * The parsed `meta.xml` data for this character as a jQuery object.
     * If this is `null`, this character must be the human player.
     * @type {?jQuery}
     */
    this.metaXml = null;

    /**
     * This opponent's release status.
     * @type {ContentStatus}
     */
    this.status = status;

    /**
     * This opponent's select screen highlight status.
     * 
     * @type {string}
     */
    this.highlightStatus = highlightStatus || status || '';

    /**
     * The number of clothing layers this player starts the game with.
     * @type {number}
     */
    this.startingLayers = 0;

    /**
     * Flags for whether or not this player's upper and/or lower body are
     * considered 'exposed'.
     * @type {{upper: boolean, lower: boolean}}
     */
    this.exposed = { upper: true, lower: true };

    /**
     * Flag for whether or not this player is considered 'mostly clothed'.
     * Currently, this is `false` if:
     *  -  the player has taken off anything more significant than an accessory
     *     item, or if
     *  - the player began the game as 'exposed' in any way.
     * @type {boolean}
     */
    this.mostlyClothed = false;

    /**
     * Flag for whether this player is considered 'decent'.
     * Currently, this is set to `false` if
     *  - the player has taken off anything any 'major' or 'important' item,
     *    or if
     *  - the player began the game as 'exposed' in any way.
     * @type {boolean}
     */
    this.decent = false;

    /**
     * Flag indicating if this player has lost the game.
     * @type {boolean}
     */
    this.out = false;

    /**
     * Flag indicating if this player has completed their forfeit.
     * @type {boolean}
     */
    this.finished = false;
    
    /**
     * Indicates how many other players lost before this player.
     * If `undefined`, this player has not lost yet.
     * @type {?number}
     */
    this.outOrder = undefined;

    /**
     * Stores the biggest lead (in layer count) this player has had so far
     * over the course of the game.
     * @type {number}
     */
    this.biggestLead = 0;

    /**
     * Stores information about this character's current forfeit, if any.
     * 
     * `tag` is the dialogue tag to use when processing forfeit dialogue
     * for this player.
     * 
     * `can_speak` indicates whether they can use 'normal' dialogue tags
     * in addition to their forfeit dialogue `tag`.
     * 
     * @type {{tag: string, can_speak: boolean}}
     */
    this.forfeit = {
        'tag': "",
        'can_speak': true,
    };

    /**
     * This player's current stage.
     * @type {number}
     */
    this.stage = 0;

    /**
     * The current number of consecutive losses this player has experienced.
     * @type {number}
     */
    this.consecutiveLosses = 0;

    /**
     * How many rounds this player has been in their current stage.
     * @type {number}
     */
    this.timeInStage = -1;

    /**
     * Contains all current marker key-value pairs for this character.
     * @type {Object.<string, (number|string)>}
     */
    this.markers = {};

    /**
     * Stores whether a one-shot case ID has been used.
     * @type {Object.<string, boolean>}
     */
    this.oneShotCases = {};

    /**
     * Stores whether a one-shot state ID has been used.
     * @type {Object.<string, boolean>}
     */
    this.oneShotStates = {};

    if ($metaXml) {
        this.first = $metaXml.find('first').text();
        this.last = $metaXml.find('last').text();

        /**
         * The name label to use for this opponent on the main select screen.
         * Should not change due to e.g. alt costumes selected on
         * the main select screen.
         * @type {string}
         */
        this.selectLabel = $metaXml.find('label').text();

        /**
         * The current label to use for this opponent.
         * @type {string}
         */
        this.label = this.selectLabel;

        /**
         * The selection screen image to use for this opponent, not including
         * the folder path.
         * @type {string}
         */
        this.image = $metaXml.find('pic').text();

        this.gender = $metaXml.find('gender').text();

        /**
         * The listed height of this opponent. Might not be used anywhere?
         * @type {string}
         */
        this.height = $metaXml.find('height').text();

        /**
         * The source material for this character.
         * @type {string}
         */
        this.source = $metaXml.find('from').text();

        /**
         * The artist credits for this character.
         * @type {string}
         */
        this.artist = $metaXml.find('artist').text();

        /**
         * The writer credits for this character.
         * @type {string}
         */
        this.writer = $metaXml.find('writer').text();

        /**
         * The description for this character on the select screens.
         * @type {string}
         */
        this.description = fixupDialogue($metaXml.find('description').html());

        /**
         * A flag indicating whether or not this character has collectibles,
         * drawn from `meta.xml`.
         * @type {boolean}
         */
        this.has_collectibles = $metaXml.find('has_collectibles').text() === "true";

        /**
         * Contains this character's Collectibles.
         * @type {Collectible[]}
         */
        this.collectibles = null;

        this.layers = parseInt($metaXml.find('layers').text(), 10);
        this.scale = Number($metaXml.find('scale').text()) || 100.0;

        /**
         * This character's release number.
         * @type {number}
         */
        this.release = parseInt(releaseNumber, 10) || Number.POSITIVE_INFINITY;

        /**
         * This character's unique dialogue line count.
         * @type {?number}
         */
        this.uniqueLineCount = parseInt($metaXml.find('lines').text(), 10) || undefined;

        /**
         * This character's unique pose count.
         * @type {?number}
         */
        this.posesImageCount = parseInt($metaXml.find('poses').text(), 10) || undefined;

        /**
         * This character's game display z-index.
         * @type {number}
         * @default 0
         */
        this.z_index = parseInt($metaXml.find('z-index').text(), 10) || 0;

        /**
         * Indicates whether or not this character's dialogue bubble should be
         * `over` or `under` the character's images.
         * 
         * @type {('over'|'under')}
         * @default 'under'
         */
        this.dialogue_layering = $metaXml.find('dialogue-layer').text();

        if (['over', 'under'].indexOf(this.dialogue_layering) < 0) {
            this.dialogue_layering = 'under';
        }

        /**
         * A jQuery object containing `<epilogue>` elements from this character's
         * `meta.xml`.
         * @type {jQuery}
         */
        this.endings = $metaXml.find('epilogue');

        /**
         * A flag indicating whether or not this character has available
         * epilogues, drawn from `meta.xml`.
         * @type {boolean}
         */
        this.ending = $metaXml.find('has_ending').text() === "true";

        if (this.endings.length > 0) {
            this.endings.each(function (idx, elem) {
                var status = $(elem).attr('status');
                if (!status || includedOpponentStatuses[status]) {
                    this.ending = true;
                }
            }.bind(this));
        }

        /**
         * The folder path to the currently selected alternate costume for
         * this opponent.
         * @type {string}
         */
        this.selected_costume = null;

        /**
         * Character costume info structure.
         * 
         * @typedef {Object} CostumeInfo
         * 
         * @property {string|null} id The ID of this alternate costume.
         * @property {jQuery} labels A jQuery collection of <label> elements.
         * @property {{tag: string, to: string, from: string}[]} tags A list of base tags for this costume. See `Player#baseTags`.
         * @property {string|undefined} folder The path to the folder for this alternate costume, or `undefined` for the default costume.
         * @property {jQuery} folders A jQuery collection of <folder> elements.
         * @property {jQuery} wardrobe A jQuery object wrapping a <wardrobe> element.
         */

        /**
         * Contains information about this character's currently-selected
         * alternate costume.
         * @type {CostumeInfo}
         */
        this.alt_costume = null;

        /**
         * Contains information about this character's default appearance.
         * @type {CostumeInfo}
         */
        this.default_costume = null;

        /**
         * A collection of pose definitions to use for this character.
         * @type {Object.<string, PoseDefinition>}
         */
        this.poses = {};

        /**
         * A flag indicating whether or not this character's label has been
         * overridden by dialogue.
         */
        this.labelOverridden = false;

        /**
         * If set, points to a Collectible object that needs to be shown via
         * a "pending collectible" icon alongside this character.
         * @type {Collectible}
         */
        this.pendingCollectiblePopup = null;

        /**
         * A flag indicated whether or not this character has fully completed
         * loading yet.
         * @type {boolean}
         */
        this.loaded = false;

        /**
         * This character's load progress percentage, in the interval 0 - 1
         * (inclusive).
         * @type {number}
         */
        this.loadProgress = undefined;

        this.baseTags = $metaXml.find('tags').children().map(function () {
            return canonicalizeTag($(this).text());
        }).get();

        this.updateTags();

        /**
         * A list of tags to use for searching from the selection screens.
         * This is drawn directly from the list of tags in `meta.xml`, though
         * no tag implications are processed.
         * @type {string[]}
         */
        this.searchTags = this.baseTags.slice();

        /**
         * A `Map` of all dialogue `Case`s used by this opponent.
         * 
         * Keys within this map are of the form: `tag:stage`, where `tag` is
         * any dialogue case tag and `stage` is a stage number. The mapped-to
         * array contains all possible Cases for that tag and stage combination.
         * 
         * @type {Map.<string, Case[]>}
         */
        this.cases = new Map();

        /* Attempt to preload this opponent's picture for selection. */
        new Image().src = 'opponents/' + id + '/' + this.image;

        /**
         * Alternate costume descriptor.
         * 
         * @typedef {Object} CostumeDescriptor
         * @property {string} folder The folder path for this alt costume.
         * @property {string} label The label to use for this alt costume at
         * the selection screen.
         * @property {string} image The image filename to use for this alt
         * costume at the selection screen.
         * @property {string} set The costume set to associate this alt costume
         * with.
         * @property {ContentStatus} status The status of this alt costume.
         */

        /**
         * A list of metadata for all alternate costumes associated with this
         * character.
         * @type {CostumeDescriptor[]}
         */
        this.alternate_costumes = [];

        /**
         * The selection screen image to use for this opponent, including
         * the folder path.
         * @type {string}
         */
        this.selection_image = this.folder + this.image;

        $metaXml.find('alternates').find('costume').each(function (i, elem) {
            var set = $(elem).attr('set') || 'offline';
            var status = $(elem).attr('status') || 'offline';

            if (alternateCostumeSets['all'] || alternateCostumeSets[set]) {
                if (!includedOpponentStatuses[status]) {
                    return;
                }

                /** @type {CostumeDescriptor} */
                var costume_descriptor = {
                    'folder': $(elem).attr('folder'),
                    'label': $(elem).text(),
                    'image': $(elem).attr('img'),
                    'set': set,
                    'status': status,
                };

                if (set === FORCE_ALT_COSTUME) {
                    this.selection_image = costume_descriptor['folder'] + costume_descriptor['image'];
                    this.selectAlternateCostume(costume_descriptor);
                }

                this.alternate_costumes.push(costume_descriptor);
            }
        }.bind(this)).get();
    }
}

/**
 * Sets initial values of state variables used by `targetStatus`,
 * `targetStartingLayers` etc. according to wardrobe.
 */
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

/**
 * Initialize (or re-initialize) the player properties that change during a 
 * game.
 */
Player.prototype.resetState = function () {
    this.out = this.finished = false;
    this.outOrder = undefined;
    this.biggestLead = 0;
	this.forfeit = "";
	this.stage = this.consecutiveLosses = 0;
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
    return this.getByStage(this.intelligence) || eIntelligence.AVERAGE;
};

Player.prototype.updateLabel = function () {
    if (this.labels && !this.labelOverridden) this.label = this.getByStage(this.labels);
}

Player.prototype.updateFolder = function () {
    if (this.folders) this.folder = this.getByStage(this.folders);
}

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

Player.prototype.clone = function() {
	var clone = Object.create(Player.prototype);
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

Player.prototype.isLoaded = function() {
    return this.loaded;
}

Player.prototype.onSelected = function(individual) {
    this.resetState();

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
Player.prototype.getByStage = function (arr, stage) {
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

Player.prototype.selectAlternateCostume = function (costumeDesc) {
    if (!costumeDesc) {
        this.selected_costume = null;
        this.selection_image = this.base_folder + this.image;
    } else {
        this.selected_costume = costumeDesc.folder;
        this.selection_image = costumeDesc.folder + costumeDesc.image;
    }

    if (this.selectionCard) {
        this.selectionCard.update();
    }
};

Player.prototype.loadAlternateCostume = function (individual) {
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

Player.prototype.unloadAlternateCostume = function () {
    if (!this.alt_costume) {
        return;
    }
    
    this.alt_costume = null;
    this.selectAlternateCostume(null);
    this.resetState();
}

Player.prototype.loadCollectibles = function (onLoaded, onError) {
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
Player.prototype.unloadOpponent = function () {
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

/************************************************************
 * Loads and parses the start of the behaviour XML file of the
 * given opponent.
 *
 * The onLoadFinished parameter must be a function capable of
 * receiving a new player object and a slot number.
 ************************************************************/
Player.prototype.loadBehaviour = function (slot, individual) {
    this.slot = slot;
    if (this.isLoaded()) {
        if (this.selected_costume) {
            this.loadAlternateCostume();
        } else {
            this.unloadAlternateCostume();
            this.onSelected(individual);
        }
        return;
    }

    fetchCompressedURL('opponents/' + this.id + "/behaviour.xml")
		/* Success callback.
         * 'this' is bound to the Opponent object.
         */
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

Player.prototype.recordTargetedCase = function (caseObj) {
    var entities = new Set();

    if (caseObj.target) entities.add(caseObj.target);
    if (caseObj.alsoPlaying) entities.add(caseObj.alsoPlaying);
    if (caseObj.filter) entities.add(caseObj.filter);

    caseObj.counters.forEach(function (ctr) {
        if (ctr.id) entities.add(ctr.id);
        if (ctr.tag) entities.add(ctr.tag);
    });

    var lines = new Set();
    caseObj.states.forEach(function (s) { lines.add(s.rawDialogue); });

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
Player.prototype.loadXMLTriggers = function () {
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
Player.prototype.loadXMLStages = function (onComplete) {
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