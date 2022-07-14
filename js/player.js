/********************************************************************************
 This file contains all of the variables and functions for the Player object, as
 well as definitions for opponent and group lisitings.
 ********************************************************************************/

/**********************************************************************
 * Enumerations
 **********************************************************************/

/************************************************************
 * An enumeration for gender.
 **/
var eGender = {
    MALE   : "male",
    FEMALE : "female"
};

/************************************************************
 * An enumeration for player size.
 **/
var eSize = {
    SMALL  : "small",
    MEDIUM : "medium",
    LARGE  : "large"
};

/************************************************************
 * An enumeration for player intelligence.
 **/
var eIntelligence = {
    NOSWAP  : "no-swap",
    THROW   : "throw",
    BAD     : "bad",
    AVERAGE : "average",
    GOOD    : "good",
    BEST    : "best"
};

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
    this.persistentMarkers = {};
    this.exposed = { upper: false, lower: false };
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
    this.numStripped = { extra: 0, minor: 0, major: 0, important: 0 };
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
    this.forfeitLocked = false;
    this.finishingTarget = this;
    this.stage = this.consecutiveLosses = 0;
    this.timeInStage = 0;
    this.ticksInStage = 0;
    this.markers = {};
    this.saidDialogue = {};
    this.hand = null;

    if (this.xml !== null) {
        /* Initialize reaction handling state. */
        this.currentTarget = null;
        this.currentTriggers = [];
        this.stateCommitted = false;

        this.oneShotCases = {};
        this.oneShotStates = {};

        var appearance = this.default_costume;
        if (this.alt_costume) {
            appearance = this.alt_costume;
        }

        this.labels = appearance.labels;
        this.folders = appearance.folders;
        this.baseTags = appearance.tags.slice();
        this.labelOverridden = this.intelligenceOverridden = false;

        /* The gender listed in meta.xml and behaviour.xml might differ
         * (for example with gender-revealing characters)
         * So assume behaviour.xml holds the 'definitive' starting gender
         * for the character.
         */
        this.gender = appearance.gender;
        this.size = appearance.size;

        this.stamina = Number(this.xml.children('timer').text());

        /* Clear the repeat log between games. */
        this.repeatLog = {};

        /* Load the player's wardrobe. */

        /* Find and grab the wardrobe tag */
        $wardrobe = appearance.wardrobe;

        /* find and create all of their clothing */
        var clothingArr = [];
        $wardrobe.children('clothing').each(function () {
            var generic = $(this).attr('generic');
            var name = $(this).attr('name') || $(this).attr('lowercase');
            var type = $(this).attr('type');
            var position = $(this).attr('position');
            var plural = $(this).attr('plural');
            plural = (plural == 'null' ? null : plural == 'true');

            var newClothing = new Clothing(name, generic, type, position, plural);

            clothingArr.push(newClothing);
        });

        this.clothing = clothingArr;
        this.initClothingStatus();

        this.loadStylesheet();
        this.stageChangeUpdate();
    }
}

/* These shouldn't do anything for the human player, but exist as empty functions
   to make it easier to iterate over the entire players[] array. */
Player.prototype.updateLabel = function () { }
Player.prototype.updateIntelligence = function () { }
Player.prototype.updateFolder = function () { }
Player.prototype.updateBehaviour = function() { }
Player.prototype.singleBehaviourUpdate = function() { }

/**********************************************************************
 * Convert a tags list to canonical form:
 * - Canonicalize each input tag
 * - Resolve tag implications
 * This function also filters out duplicated tags.
 **********************************************************************/
Player.prototype.expandTagsList = function(input_tags) {
    let tmp = input_tags.map(canonicalizeTag);
    let output_tags = [];

    while (tmp.length > 0) {
        let tag = tmp.shift();

        // Ensure exactly one instance of each tag remains within the output array.
        if (output_tags.indexOf(tag) >= 0) continue;
        output_tags.push(tag);

        // If this tag implies other tags, queue those for processing as well.
        if (TAG_IMPLICATIONS.hasOwnProperty(tag)) {
            Array.prototype.push.apply(tmp, TAG_IMPLICATIONS[tag]);
        }
    }

    /* "chubby" implies "curvy" on female characters only,
       so this has to be done separately */
    if (output_tags.includes("chubby") && this.gender == eGender.FEMALE) {
        output_tags.push("curvy");
    }

    return output_tags;
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

    this.tags = this.expandTagsList(tags);
}

Player.prototype.stageChangeUpdate = function () {
    this.updateLabel();
    this.updateIntelligence();
    this.updateFolder();
    this.updateTags();
}

Player.prototype.addTag = function(tag) {
    if (tag) this.baseTags.push(canonicalizeTag(tag));
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
    if (tag && tag[0] == "!") {
        return !this.hasTag(tag.substring(1));
    }

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

    if (!this.markers) {
        console.error("Marker object not initialized for opponent " + this.id, this);
        console.trace();

        /* This might be a bad idea, since if we get here, then resetState()
         * must not have been called for some reason.
         * But better this than crashing... maybe?
         */
        this.markers = {};
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
 * Calculates how many lines from currently-selected characters target this
 * character.
 *
 * @param {string} [filterStatus] If passed, only lines from characters with the
 * given status will be considered.
 * 
 * @param {number} [cap] If passed, each currently selected character's contribution
 * to the total inbound line count will be capped to this number.
 *
 * @returns {number}
 */
Player.prototype.inboundLinesFromSelected = function (filterStatus, cap) {
    var id = this.id;

    return players.reduce(function(sum, p) {
        if (p && p.targetedLines && id in p.targetedLines
            && (!filterStatus || p.status === filterStatus)) {
            if (cap) {
                sum += Math.min(p.targetedLines[id].seen.size, cap);
            } else {
                sum += p.targetedLines[id].seen.size;
            }
        }

        return sum;
    }, 0);
}

/**
 * Subclass of Player for AI-controlled players.
 *
 * @constructor
 *
 * @param {string} id
 * @param {jQuery} $metaXml
 * @param {string} status
 * @param {number} [rosterScore]
 * @param {number} [releaseNumber]
 * @param {string} [highlightStatus]
 */
function Opponent (id, metaFiles, status, rosterScore, releaseNumber, highlightStatus) {
    Player.call(this, id);

    this.id = id;
    this.folder = 'opponents/'+id+'/';
    this.base_folder = 'opponents/'+id+'/';
    
    var $metaXml = metaFiles[0];
    var $tagsXml = metaFiles[1];

    this.status = status;
    this.highlightStatus = eventCharacterSettings.highlights[id] ||  highlightStatus || status || '';
    this.first = $metaXml.children('first').text();
    this.last = $metaXml.children('last').text();

    // For label and gender, track the original, default value from
    // meta.xml, the value for the currently selected costume to be
    // shown on the selection card, and the current in-game value.
    this.label = this.selectLabel = this.metaLabel = $metaXml.children('label').text();
    this.gender = this.selectGender = this.metaGender = $metaXml.children('gender').text();

    var picElem = $metaXml.children('pic');

    this.image = picElem.text();
    this.height = $metaXml.children('height').text();
    this.source = $metaXml.children('from').text();
    this.artist = $metaXml.children('artist').text();
    this.writer = $metaXml.children('writer').text();
    this.description = fixupDialogue($metaXml.children('description').html());
    this.has_collectibles = $metaXml.children('has_collectibles').text() === "true";
    this.collectibles = null;
    this.layers = parseInt($metaXml.children('layers').text(), 10);
    this.scale = Number($metaXml.children('scale').text()) || 100.0;
    this.release = releaseNumber;
    this.uniqueLineCount = parseInt($metaXml.children('lines').text(), 10) || undefined;
    this.posesImageCount = parseInt($metaXml.children('poses').text(), 10) || undefined;
    this.z_index = parseInt($metaXml.children('z-index').text(), 10) || 0;
    this.dialogue_layering = $metaXml.children('dialogue-layer').text();
    this.fontSize = $metaXml.children('font-size').text();
    if (!['small', 'smaller'].includes(this.fontSize)) this.fontSize = undefined;
    this.lastUpdated = parseInt($metaXml.children('lastupdate').text(), 10) || 0;

    this.rosterScore = rosterScore;
    this.effectiveScore = -Infinity;

    this.endings = null;
    if (EPILOGUES_ENABLED) {
        var $endings = $metaXml.children('epilogue').filter(function (idx, elem) {
            var status = $(elem).attr('status');
            return (!status || includedOpponentStatuses[status]);
        }.bind(this));
        if ($endings.length) {
            this.endings = $endings;
        }
    }

    if (['over', 'under'].indexOf(this.dialogue_layering) < 0) {
        this.dialogue_layering = 'under';
    }

    this.selected_costume = null;
    this.alt_costume = null;
    this.default_costume = null;
    this.poses = {};
    this.imageCache = {};
    this.labelOverridden = this.intelligenceOverridden = false;
    this.pendingCollectiblePopups = [];
    this.repeatLog = {};

    this.loaded = false;
    this.loadProgress = undefined;

    /* originalTags stores tags that will be later used in resetState to build the
     * opponent's true tags list. It does not store implied tags.
     *
     * The tags list stores the fully-expanded list of tags for the opponent,
     * including implied tags.
     */
    this.originalTags = $tagsXml.find('>tags>tag').map(function () {
        return {
            'tag': canonicalizeTag($(this).text()),
            'from': $(this).attr('from'),
            'to': $(this).attr('to'),
        }
    }).get();
    this.searchTags = this.expandTagsList(this.originalTags.map(obj => obj.tag));

    this.magnetismTag = undefined;
    this.searchTags.forEach((tag) => {
        if (MAGNET_TAGS.indexOf(tag) >= 0) this.magnetismTag = tag;
    });

    this.cases = new Map();

    /* Attempt to preload this opponent's picture for selection. */
    new Image().src = 'opponents/'+id+'/'+this.image;

    this.alternate_costumes = [];
    this.selection_image = this.folder + this.image;
    this.selection_image_adjustment = {
        x: (Number(picElem.attr("x")) || 0), /* negative values move to the left, positive to the right */
        y: (-Number(picElem.attr("y")) || 0), /* negative values move down, positive moves up */
        scale: Number(picElem.attr("scale")) || 100.0,
    };

    this.favorite = save.isCharacterFavorited(this);

    this.event_character = eventCharacterSettings.ids.has(id);
    this.event_sort_order = (
        (eventCharacterSettings.sorting[id] !== undefined) ? eventCharacterSettings.sorting[id]
        : (eventCharacterSettings.ids.has(id) ? 1 : 0)
    );
    this.event_partition = eventCharacterSettings.partitions[id] || 0;
    this.force_prefill = (eventCharacterSettings.prefills[id] !== undefined) ? eventCharacterSettings.prefills[id] : false;
    this.allow_testing_guest = (eventCharacterSettings.allowTestingGuests[id] !== undefined) ? eventCharacterSettings.allowTestingGuests[id] : false;

    this.matchesEventTag = false;
    eventTagList.some(function (tag) {
        if (this.searchTags.indexOf(tag) >= 0) {
            this.matchesEventTag = true;
            this.event_character = true;
            if (eventTagSettings.highlights[tag] && !eventCharacterSettings.highlights[id]) {
                this.highlightStatus = eventTagSettings.highlights[tag];
            }

            if (eventCharacterSettings.sorting[id] === undefined) {
                this.event_sort_order = (eventTagSettings.sorting[tag] !== undefined) ? eventTagSettings.sorting[tag] : 2;
            }

            if (eventCharacterSettings.partitions[id] === undefined && eventTagSettings.partitions[tag] !== undefined) {
                /* The default partition value in all cases is 0, so if eventTagSettings.partitions[tag] === undefined, we don't need to do anything. */
                this.event_partition = eventTagSettings.partitions[tag];
            }

            if (eventCharacterSettings.prefills[id] === undefined) {
                if (eventTagSettings.ids.has(tag) && eventTagSettings.prefills[tag] === undefined) {
                    this.force_prefill = true;
                } else {
                    this.force_prefill = eventTagSettings.prefills[tag];
                }
            }

            if (eventCharacterSettings.allowTestingGuests[id] === undefined && eventTagSettings.allowTestingGuests[tag] !== undefined) {
                this.allow_testing_guest = eventTagSettings.allowTestingGuests[tag];
                // The default value of allowTestingGuests for specific characters and for tagged characters is false, so if the tag
                // has no specified attribute value, we don't need to do anything.
            }

            return true;
        }
        return false;
    }.bind(this));

    if (this.event_sort_order !== 0 || this.event_partition !== 0) eventSortingActive = true;

    if (!ALT_COSTUMES_ENABLED) return;

    var defaultCostumes = [];
    $metaXml.find('>alternates>costume').each(function (i, elem) {
        var set = $(elem).attr('set');
        var status = $(elem).attr('status') || 'online';

        if ((set === undefined || alternateCostumeSets['all'] || alternateCostumeSets[set]) && includedOpponentStatuses[status]) {
            var costume_descriptor = {
                'folder': $(elem).attr('folder'),
                'name': $(elem).text(),
                'image': $(elem).attr('img'),
                'gender': $(elem).attr('gender') || this.selectGender,
                'label': $(elem).attr('label') || this.selectLabel,
                'set': set,
                'status': status,
            };

            if (set && DEFAULT_COSTUME_SETS.has(set)) {
                defaultCostumes.push(costume_descriptor);
            }

            this.alternate_costumes.push(costume_descriptor);
        }
    }.bind(this)).get();

    this.hasDefaultCostume = defaultCostumes.length > 0;
    if (this.hasDefaultCostume) {
        var selectedDefault = defaultCostumes[getRandomNumber(0, defaultCostumes.length)];
        var costumeSet = selectedDefault.set;

        this.selection_image = selectedDefault['folder'] + selectedDefault['image'];
        this.selectAlternateCostume(selectedDefault);

        if (eventCostumeSettings.ids.has(costumeSet)) {
            this.event_character = true;
            
            if (eventCostumeSettings.highlights[costumeSet] && !eventCharacterSettings.highlights[id]) {
                this.highlightStatus = eventCostumeSettings.highlights[costumeSet];
            }
    
            if (eventCharacterSettings.sorting[id] === undefined) {
                if (eventCostumeSettings.sorting[costumeSet] !== undefined) {
                    this.event_sort_order = eventCostumeSettings.sorting[costumeSet];
                } else if (!this.matchesEventTag) {
                    this.event_sort_order = 3;
                }

                if (this.event_sort_order != 0) eventSortingActive = true;
            }
    
            if (eventCharacterSettings.partitions[costumeSet] === undefined && eventCostumeSettings.partitions[costumeSet] !== undefined) {
                this.event_partition = eventCostumeSettings.partitions[costumeSet];
                if (this.event_partition != 0) eventSortingActive = true;
            }

            if (eventCharacterSettings.prefills[id] === undefined) {
                if (eventCostumeSettings.prefills[costumeSet] !== undefined) {
                    this.force_prefill = eventCostumeSettings.prefills[costumeSet];
                } else if (!this.matchesEventTag) {
                    this.force_prefill = true;
                }
                // If an event tag is matched, fall back to the value used there
            }

            if (eventCharacterSettings.allowTestingGuests[id] === undefined) {
                if (eventCostumeSettings.allowTestingGuests[costumeSet] !== undefined) {
                    this.allow_testing_guest = eventCostumeSettings.allowTestingGuests[costumeSet];
                } else if (!this.matchesEventTag) {
                    this.allow_testing_guest = false;
                }
            }
        }
    }

    // Not reached if alt costumes are disabled
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

    Sentry.addBreadcrumb({
        category: 'select',
        message: 'Load completed for ' + this.id,
        level: 'info'
    });

    this.loaded = true;

    this.preloadStageImages(-1);
    if (individual) {
        updateAllBehaviours(this.slot, SELECTED, [[OPPONENT_SELECTED]]);
    } else {
        this.singleBehaviourUpdate(SELECTED, null);
    }

    updateSelectionVisuals();
}

Opponent.prototype.loadStylesheet = function () {
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
}

Opponent.prototype.updateLabel = function () {
    if (!this.labelOverridden) {
        if (this.labels && this.labels.length) {
            this.label = this.getByStage(this.labels);
        } else {
            this.label = this.selectLabel;
        }
    }
}

Opponent.prototype.setLabel = function(label) {
    if (label) {
        this.label = label;
        this.labelOverridden = true;
    } else if (label !== undefined) {
        this.labelOverridden = false;
        this.updateLabel();
    }
}

Opponent.prototype.updateIntelligence = function () {
    if (!this.intelligenceOverridden) {
        if (this.intelligences && this.intelligences.length) {
            this.intelligence = this.getByStage(this.intelligences);
        }
        if (!this.intelligence) {
            this.intelligence = eIntelligence.AVERAGE;
        }
    }
}

Opponent.prototype.setIntelligence = function (intelligence) {
    if (intelligence) {
        this.intelligence = intelligence;
        this.intelligenceOverridden = true;
    } else if (intelligence !== undefined) {
        this.intelligenceOverridden = false;
        this.updateIntelligence();
    }
}

Opponent.prototype.updateFolder = function () {
    if (this.folders) this.folder = this.getByStage(this.folders);
    if (!this.folder) {
        /* Shouldn't happen, but... */
        captureError(new Error(
            "Could not find folder for " + this.id + " at stage " + this.stage +
            (this.selected_costume ? " with alt costume " + this.selected_costume : "")
        ));

        this.folder = this.selected_costume || this.base_folder;
    }

    if (this.folder == this.base_folder) {
        this.poses = this.default_costume.poses;
    } else if (this.alt_costume) {
        this.poses = this.alt_costume.poses;
    }
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

/**
 * Get the repeat count for the currently displayed line, if any.
 * @returns {number}
 */
Opponent.prototype.getRepeatCount = function () {
    if (!this.chosenState || !this.chosenState.rawDialogue) {
        return 0;
    }

    return this.repeatLog[this.chosenState.rawDialogue] || 0;
}

/**
 * Mark this character as favorited or not.
 * @param {boolean} value 
 */
Opponent.prototype.setFavorited = function (value) {
    this.favorite = value;
    save.setCharacterFavorited(this, value);
    updateIndividualSelectSort();
}

Opponent.prototype.selectAlternateCostume = function (costumeDesc) {
    if (!costumeDesc) {
        this.selected_costume = null;
        this.selection_image = this.base_folder + this.image;
        this.selectLabel = this.metaLabel;
        this.selectGender = this.metaGender;
    } else {
        this.selected_costume = costumeDesc.folder;
        this.selection_image = costumeDesc.folder + costumeDesc.image;
        this.selectLabel = costumeDesc.label;
        this.selectGender = costumeDesc.gender;
    }

    if (this.selectionCard)
        this.selectionCard.update();
};

/**
 * Loads and parses the selected alternate costume for this opponent.
 * 
 * @returns {Promise<void>} A Promise that resolves after all loading for the
 * selected costume is complete.
 * 
 * @throws The returned Promise will reject if the costume data cannot be fetched
 * or if an error is encountered during loading.
 */
Opponent.prototype.loadAlternateCostume = function () {
    if (this.alt_costume) {
        if (this.alt_costume.folder != this.selected_costume) {
            this.unloadAlternateCostume();
        } else {
            return immediatePromise();
        }
    }

    console.log("Loading alternate costume: "+this.selected_costume);
    this.loaded = false;

    return metadataIndex.getFile(this.selected_costume+'costume.xml').then(function ($xml) {
        Sentry.addBreadcrumb({
            category: 'select',
            message: 'Initializing alternate costume for ' + this.id + ': ' + this.selected_costume,
            level: 'info'
        });

        this.alt_costume = {
            id: $xml.children('id').text(),
            labels: $xml.children('label'),
            tags: [],
            folder: this.selected_costume,
            folders: $xml.children('folder'),
            wardrobe: $xml.children('wardrobe'),
            gender: $xml.children('gender').text() || this.selectGender,
            size: $xml.children('size').text() || this.default_costume.size,
        };

        var poses = $xml.children('poses');
        var poseDefs = {};
        Object.assign(poseDefs, this.default_costume.poses);
        $(poses).children('pose').each(function (i, elem) {
            var def = new PoseDefinition($(elem), this);
            poseDefs[def.id] = def;
        }.bind(this));

        this.alt_costume.poses = poseDefs;

        var costumeTags = this.default_costume.tags.slice();
        var tagMods = $xml.children('tags');
        if (tagMods) {
            var newTags = [];
            tagMods.children('tag').each(function (idx, elem) {
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
    }.bind(this)).catch(function (err) {
        console.error("Failed to load alternate costume: "+this.selected_costume);
        throw err;
    }.bind(this));
}

Opponent.prototype.unloadAlternateCostume = function () {
    if (!this.alt_costume) {
        return;
    }

    this.alt_costume = null;
    this.resetState();
}

/**
 * Load the collectibles for this opponent by fetching collectibles.xml if necessary.
 * 
 * @returns {Promise<void>} A Promise that resolves after all collectibles are
 * loaded.
 * 
 * @throws The returned Promise will reject if the collectibles for this character
 * cannot be fetched or if loading them causes an error.
 */
Opponent.prototype.fetchCollectibles = function () {
    if (!this.has_collectibles || this.collectibles !== null) {
        return immediatePromise();
    }

    console.log("Fetching collectibles for " + this.id);

    return metadataIndex.getFile(this.folder + "collectibles.xml").then(function ($xml) {
        var collectiblesArray = [];
        $xml.children('collectible').each(function (idx, elem) {
            collectiblesArray.push(new Collectible($(elem), this));
        }.bind(this));

        this.collectibles = collectiblesArray;
        this.has_collectibles = this.collectibles.some(function (c) {
            return !c.status || includedOpponentStatuses[c.status];
        });
    }.bind(this)).catch(function (err) {
        console.error("Error loading collectibles for "+this.id);
        throw err;
    }.bind(this));
}

/**
 * Get quick details on epilogue conditions and whether they're met, given
 * the current player gender and table composition.
 */
Opponent.prototype.getAllEpilogueStatus = function () {
    if (!this.endings) {
        return [];
    }

    var ret = [];
    this.endings.each(function (idx, elem) {
        var $elem = $(elem);

        var summary = {
            title: $elem.text(),
            extraConditions: false,
            wrongGender: false,
            requiredCharacters: null,
            characterIsMissing: false,
            hint: undefined,
        };

        summary.unlocked = save.hasEnding(this.id, $elem.text());

        /* Check what conditions we can for this epilogue: */
        summary.gender = $elem.attr('gender') || 'any';
        if (summary.gender !== humanPlayer.gender && summary.gender !== 'any') {
            summary.wrongGender = true;
        }

        var alsoPlaying = $elem.attr("alsoPlaying");
        if (alsoPlaying) {
            alsoPlaying = alsoPlaying.trim().split(/\s+/);
            if (!alsoPlaying.every(function(ap) {
                return players.some(function (p) { return p.id == ap; });
            })) {
                /* Player requirement not met */
                summary.characterIsMissing = true;
            }
            summary.requiredCharacters = alsoPlaying;
            summary.requiredCharactersLabels = alsoPlaying.map(function(id) {
                var opp = loadedOpponents.find(function (p) {
                    return p && p.id === id;
                });
                return opp ? opp.selectLabel : id.initCap();
            });
        }

        summary.hint = $elem.attr('hint');
        summary.extraConditions = $elem.attr('markers') == 'true';
        summary.score = (summary.wrongGender ? 4 : 0)
            + (summary.characterIsMissing ? 2 : 0)
            + (summary.requiredCharacters || summary.extraConditions ? 1 : 0);

        ret.push(summary);
    }.bind(this));

    return ret;
}

/**
 * Helper function that trims down opponent status into something that'll be
 * easy to display on a selection screen.
 *
 * To be specific, this attempts to identify the epilogue with the most matching
 * static requirements, taking the player's gender and current table setup into
 * account.
 */
Opponent.prototype.getEpilogueStatus = function(mainSelect) {
    /* Find the epilogue that matches the most requirements possible.
     * Prefer matching gender requirements first before character reqs.
     */
    if (!this.endings) {
        return;
    }

    var epilogueStatus = this.getAllEpilogueStatus();
    var epilogueTitles = new Set();
    var epiloguesUnlocked = new Set();
    var bestMatchEpilogue = null;
    for (var i = 0; i < epilogueStatus.length; i++) {
        var status = epilogueStatus[i];
        epilogueTitles.add(status.title);
        if (status.unlocked) {
            epiloguesUnlocked.add(status.title);
            continue;
        }

        if (!bestMatchEpilogue || status.score < bestMatchEpilogue.score) {
            bestMatchEpilogue = status;
        }
    }

    /* Prior to main selection screen, show gender icon for wrong
     * gender and conditional icon for other conditions, fulfilled or
     * not (on the group selection screen, we'd need to look at the
     * other characters in the group rather than the currently
     * selected ones to give an accurate statement; let's do that
     * later).  On the main selection screen, show the warning icon
     * for wrong gender or character missing and the conditional icon
     * for marker conditions.
     */
    var badge = '';
    if (epiloguesUnlocked.size == epilogueTitles.size) {
        badge = "-completed";
    } else if (mainSelect && bestMatchEpilogue.score > 1) {
        badge = "-unavailable";
    } else if (bestMatchEpilogue.wrongGender) {
        badge = '-' + bestMatchEpilogue.gender;
    } else if ((bestMatchEpilogue.requiredCharacters && !mainSelect) || bestMatchEpilogue.extraConditions) {
        badge = "-conditional";
    }
    var tooltip;
    if (bestMatchEpilogue) {
        if (bestMatchEpilogue.wrongGender) {
            tooltip = "Play as " + bestMatchEpilogue.gender + " for a chance to unlock another epilogue";
        } else if (bestMatchEpilogue.requiredCharacters && (!mainSelect || bestMatchEpilogue.characterIsMissing)) {
            bestMatchEpilogue.requiredCharactersAsText = englishJoin(bestMatchEpilogue.requiredCharacters.map(function(id) {
                var opp = loadedOpponents.find(function (p) {
                    return p && p.id === id;
                });
                return opp ? opp.selectLabel : id.initCap();
            }));

            tooltip = "Play with " + bestMatchEpilogue.requiredCharactersAsText
                + " for a chance to unlock another epilogue";
        } else if (bestMatchEpilogue.extraConditions) {
            if (bestMatchEpilogue.hint) {
                tooltip = "Hint: " + bestMatchEpilogue.hint;
            } else {
                tooltip = "Unknown conditions apply";
            }
        }
    }

    return {
        total: epilogueTitles.size,
        unlocked: epiloguesUnlocked.size,
        match: bestMatchEpilogue,
        badge: 'img/epilogue' + badge + '.svg',
        tooltip: tooltip,
    };
}

/* Called prior to removing a character from the table. */
Opponent.prototype.unloadOpponent = function () {
    Sentry.addBreadcrumb({
        category: 'select',
        message: 'Unloading opponent ' + this.id,
        level: 'info'
    });

    if (this.stylesheet) {
        /* Remove the <link> to this opponent's stylesheet. */
        $('link[href=\"'+this.stylesheet+'\"]').remove();
    }

    this.slot = undefined;
}

Opponent.prototype.fetchBehavior = function() {
    // Optionally, replace with fetchCompressedURL(this.folder + "behaviour.xml")
    return fetchXML(this.folder + "behaviour.xml").then(function($xml) {
        /* Always parse the stylesheet element, so we can use it both
         * when selecting a character (loading the entire behaviour)
         * and when playing an epilogue from the gallery */
        this.stylesheet = null;
        var stylesheet = $xml.children('stylesheet').text();
        if (stylesheet) {
            var m = stylesheet.match(/[a-zA-Z0-9()~!*:@,;\-.\/]+\.css/i);
            if (m) {
                this.stylesheet = 'opponents/'+this.id+'/'+m[0];
            }
        }
        return $xml;
    }.bind(this));
}

/**
 * Loads and parses the start of the behaviour XML file of the
 * given opponent.
 *
 * @returns {Promise<void>} A Promise that resolves after all loading is complete.
 * This includes calls to loadAlternateCostume() and onSelected().
 */
Opponent.prototype.loadBehaviour = function (slot, individual) {
    this.slot = slot;
    if (this.isLoaded()) {
        var p = null;
        
        if (this.selected_costume) {
            p = this.loadAlternateCostume();
        } else {
            this.unloadAlternateCostume();
            p = immediatePromise();
        }

        return p.then(function () {
            this.onSelected(individual);
        }.bind(this)).catch(function(err) {
            /* Handle any errors that loadAlternateCostume might throw. */
            console.error("Failed to load " + this.id);
            captureError(err);

            delete players[this.slot];
            updateSelectionVisuals();
        }.bind(this));
    }

    // start loading collectibles in parallel with behaviour.xml
    var collectiblesPromise = this.fetchCollectibles();

    /* Success callback.
     * 'this' is bound to the Opponent object.
     */
    return this.fetchBehavior()
        .then(function($xml) {
            Sentry.addBreadcrumb({
                category: 'select',
                message: 'Fetched and parsed opponent ' + this.id + ', initializing...',
                level: 'info'
            });

            this.xml = $xml;
            this.intelligences = $xml.children('intelligence');

            this.default_costume = {
                id: null,
                labels: $xml.children('label'),
                tags: this.originalTags,
                folders: this.folder,
                wardrobe: $xml.children('wardrobe'),
                gender: $xml.children('gender').text(),
                size: $xml.children('size').text(),
            };

            var poses = $xml.children('poses');
            var poseDefs = {};
            $(poses).children('pose').each(function (i, elem) {
                var def = new PoseDefinition($(elem), this);
                poseDefs[def.id] = def;
            }.bind(this));

            this.default_costume.poses = poseDefs;

            /* Load forward-declarations for persistent markers. */
            $xml.find('persistent-markers>marker').each(function (i, elem) {
                this.persistentMarkers[$(elem).text()] = true;
            }.bind(this));

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
            $xml.children('behaviour').find('case>alternative:first-of-type').each(function() {
                var $case = $(this).parent();
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
            $xml.children('nicknames').children('nickname').each(function() {
                if ($(this).attr('for') in nicknames) {
                    nicknames[$(this).attr('for')].push($(this).text());
                } else {
                    nicknames[$(this).attr('for')] = [ $(this).text() ];
                }
            });
            this.nicknames = nicknames;

            return this.loadXMLTriggers();
        }.bind(this)).then(function () {
            /* Wait for loading of all other stuff to complete: */
            if (this.selected_costume) {
                return Promise.all([this.loadAlternateCostume(), collectiblesPromise]);
            }

            return collectiblesPromise;
        }.bind(this)).then(
            this.onSelected.bind(this, individual)
        ).catch(function(err) {
            /* Error callback. */
            console.error("Failed to load " + this.id);
            captureError(err);
            
            delete players[this.slot];
            updateSelectionVisuals();
        }.bind(this));
}

Opponent.prototype.recordTargetedCase = function (caseObj) {
    var entities = new Set();

    if (caseObj.target) entities.add(caseObj.target);
    if (caseObj.alsoPlaying) entities.add(caseObj.alsoPlaying);
    if (caseObj.filter && caseObj.filter[0] !== "!") entities.add(caseObj.filter);

    caseObj.counters.forEach(function (ctr) {
        /* Conditions checking if a character/tag is not at the table don't count as targeted. */
        if (ctr.id && ctr.count.max !== 0) entities.add(ctr.id);
        if (ctr.tag) {
            if (ctr.tag[0] !== "!" && ctr.count.max !== 0) {
                entities.add(ctr.tag);
            } else if (ctr.tag[0] === "!" && ctr.count.max === 0) {
                /* (filter="!tag" and count: 0) implies checking if everyone has the given tag */
                entities.add(ctr.tag.substring(1));
            }
        }
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
        if (s.legacyPersistentFlag) {
            s.markers.forEach(function (marker) {
                this.persistentMarkers[marker.name] = true;
            }.bind(this));
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
 * @returns {Promise<number>} A Promise that resolves once all cases have been processed.
 */
Opponent.prototype.loadXMLTriggers = function () {
    return new Promise(function (resolve) {
        var $cases = this.xml.find('>behaviour>trigger>case:not([disabled="1"])');

        var loadItemsTotal = $cases.length;
        if (loadItemsTotal == 0) {
            return resolve(0);
        }

        var loadItemsCompleted = 0;
        function process() {
            var startTS = performance.now();

            /* break tasks into roughly 50ms chunks */
            while (performance.now() - startTS < 50) {
                if (loadItemsCompleted >= loadItemsTotal) {
                    this.loadProgress = undefined;
                    return resolve(loadItemsCompleted);
                }

                let $case = $($cases.get(loadItemsCompleted));
                let trigger = $case.parent().attr('id');
                let c = new Case($case, trigger);
                this.recordTargetedCase(c);

                c.getStages().forEach(function (stage) {
                    var key = c.trigger+':'+stage;  // Case constructor may have altered the trigger
                    if (!this.cases.has(key)) {
                        this.cases.set(key, []);
                    }

                    this.cases.get(key).push(c);
                }, this);

                loadItemsCompleted++;
            }

            this.loadProgress = loadItemsCompleted / loadItemsTotal;
            mainSelectDisplays[this.slot - 1]?.updateLoadPercentage(this);

            setTimeout(process.bind(this), 10);
        }

        setTimeout(process.bind(this), 0);
    }.bind(this));
}

Player.prototype.getImagesForStage = function (stage) {
    if(!this.xml) return [];

    var poseSet = {};
    var imageSet = {};
    var folder = this.folders ? this.getByStage(this.folders, stage === -1 ? 0 : stage) : this.folder;
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
        if (this.cases.has(GAME_START + ':0')) {
            this.cases.get(GAME_START + ':0').forEach(processCase);
        }
    }

    /* Finally, transform the set of collected pose names into a
     * set of image file paths.
     */
    Object.keys(poseSet).forEach(function (poseName) {
        if (poseName.startsWith('custom:')) {
            var actualStage = (stage > -1) ? stage : 0;
            var key = poseName.split(':', 2)[1].replace('#', actualStage);
            var pose = advPoses[key];
            if (pose) pose.getUsedImages(actualStage).forEach(function (img) {
                imageSet[img.replace('#', actualStage)] = true;
            });
        } else {
            imageSet[folder + poseName] = true;
        }
    });

    return Object.keys(imageSet);
};

/**
 * Preload all images referenced by this character's dialogue for a given stage.
 * @param {number} stage 
 * @returns {Promise<Array<HTMLImageElement>>}
 */
Player.prototype.preloadStageImages = function (stage) {
    return Promise.all(this.getImagesForStage(stage).map(function (fn) {
        return new Promise(function (resolve, reject) {
            /* Keep references to the Image elements around so they don't get GC'd. */
            if (this.imageCache[fn]) {
                resolve(this.imageCache[fn]);
            } else {
                var img = new Image();
                img.addEventListener('load', function() { resolve(img); });
                img.src = fn;
                this.imageCache[fn] = img;
            }
        }.bind(this));
    }, this));
};
