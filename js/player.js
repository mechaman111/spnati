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
    THROW  : "throw",
    BAD  : "bad",
    AVERAGE : "average",
    GOOD  : "good",
    BEST  : "best"
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
    this.stage = this.consecutiveLosses = 0;
    this.timeInStage = -1;
    this.markers = {};
    this.oneShotCases = {};
    this.oneShotStates = {};
    this.hand = null;

    if (this.xml !== null) {
        /* Initialize reaction handling state. */
        this.currentTarget = null;
        this.currentTags = [];
        this.stateCommitted = false;

        if (this.startStates.length > 0) this.updateChosenState(new State(this.startStates[0]));

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

/* These shouldn't do anything for the human player, but exist as empty functions
   to make it easier to iterate over the entire players[] array. */
Player.prototype.updateLabel = function () { }
Player.prototype.updateIntelligence = function () { }
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
    this.updateIntelligence();
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
 * Calculates how many lines from currently-selected characters target this
 * character.
 *
 * @param {string} filterStatus? If passed, only lines from characters with the
 * given status will be considered.
 *
 * @returns {number}
 */
Player.prototype.inboundLinesFromSelected = function (filterStatus) {
    var id = this.id;

    return players.reduce(function(sum, p) {
        if (p && p.targetedLines && id in p.targetedLines
            && (!filterStatus || p.status === filterStatus)) {
            sum += p.targetedLines[id].seen.size;
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
    this.first = $metaXml.children('first').text();
    this.last = $metaXml.children('last').text();

    // For label and gender, track the original, default value from
    // meta.xml, the value for the currently selected costume to be
    // shown on the selection card, and the current in-game value.
    this.label = this.selectLabel = this.metaLabel = $metaXml.children('label').text();
    this.gender = this.selectGender = this.metaGender = $metaXml.children('gender').text();

    this.image = $metaXml.children('pic').text();
    this.height = $metaXml.children('height').text();
    this.source = $metaXml.children('from').text();
    this.artist = $metaXml.children('artist').text();
    this.writer = $metaXml.children('writer').text();
    this.description = fixupDialogue($metaXml.children('description').html());
    this.has_collectibles = $metaXml.children('has_collectibles').text() === "true";
    this.collectibles = null;
    this.layers = parseInt($metaXml.children('layers').text(), 10);
    this.scale = Number($metaXml.children('scale').text()) || 100.0;
    this.release = parseInt(releaseNumber, 10) || Number.POSITIVE_INFINITY;
    this.uniqueLineCount = parseInt($metaXml.children('lines').text(), 10) || undefined;
    this.posesImageCount = parseInt($metaXml.children('poses').text(), 10) || undefined;
    this.z_index = parseInt($metaXml.children('z-index').text(), 10) || 0;
    this.dialogue_layering = $metaXml.children('dialogue-layer').text();
    this.lastUpdated = parseInt($metaXml.children('lastupdate').text(), 10) || 0;

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
    this.labelOverridden = this.intelligenceOverridden = false;
    this.pendingCollectiblePopups = [];

    this.loaded = false;
    this.loadProgress = undefined;

    /* baseTags stores tags that will be later used in resetState to build the
     * opponent's true tags list. It does not store implied tags.
     *
     * The tags list stores the fully-expanded list of tags for the opponent,
     * including implied tags.
     */
    this.baseTags = $metaXml.find('>tags>tag').map(function() { return canonicalizeTag($(this).text()); }).get();
    this.removeTag(this.id);
    this.updateTags();
    this.searchTags = expandTagsList(this.baseTags);

    this.cases = new Map();

    /* Attempt to preload this opponent's picture for selection. */
    new Image().src = 'opponents/'+id+'/'+this.image;

    this.alternate_costumes = [];
    this.selection_image = this.folder + this.image;

    if (!ALT_COSTUMES_ENABLED) return;
    $metaXml.find('>alternates>costume').each(function (i, elem) {
        var set = $(elem).attr('set') || 'offline';
        var status = $(elem).attr('status') || 'offline';

        if (alternateCostumeSets['all'] || alternateCostumeSets[set]) {
            if (!includedOpponentStatuses[status] && set !== DEFAULT_COSTUME_SET) {
                return;
            }

            var costume_descriptor = {
                'folder': $(elem).attr('folder'),
                'name': $(elem).text(),
                'image': $(elem).attr('img'),
                'gender': $(elem).attr('gender') || this.selectGender,
                'label': $(elem).attr('label') || this.selectLabel,
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
    } else {
        this.labelOverridden = false;
        this.updateLabel();
    }
}

Opponent.prototype.updateIntelligence = function () {
    if (!this.intelligenceOverridden) {
        if (this.intelligences && this.intelligences.length) {
            this.intelligence = this.getByStage(this.intelligences);
        } else {
            this.intelligence = eIntelligence.AVERAGE;
        }
    }
}

Opponent.prototype.setIntelligence = function (intelligence) {
    if (intelligence) {
        this.intelligence = intelligence;
        this.intelligenceOverridden = true;
    } else {
        this.intelligenceOverridden = false;
        this.updateIntelligence();
    }
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
                id: $xml.children('id').text(),
                labels: $xml.children('label'),
                tags: [],
                folder: this.selected_costume,
                folders: $xml.children('folder'),
                wardrobe: $xml.children('wardrobe'),
                gender: $xml.children('gender').text() || this.selectGender,
            };

            var poses = $xml.children('poses');
            var poseDefs = {};
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
            $(xml).children('collectible').each(function (idx, elem) {
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
            needsCharacter: null,
            missingCharacter: false,
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
            if (!players.some(function (p) { return p.id == alsoPlaying; })) {
                /* Player requirement not met */
                summary.missingCharacter = true;
            }
            summary.needsCharacter = alsoPlaying;
        }

        summary.hint = $elem.attr('hint');
        summary.extraConditions = $elem.attr('markers') == 'true';
        summary.score = (summary.wrongGender ? 4 : 0)
            + (summary.missingCharacter ? 2 : 0)
            + (summary.needsCharacter || summary.extraConditions ? 1 : 0);

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
    } else if ((bestMatchEpilogue.needsCharacter && !mainSelect) || bestMatchEpilogue.extraConditions) {
        badge = "-conditional";
    }
    var tooltip;
    if (bestMatchEpilogue) {
        if (bestMatchEpilogue.wrongGender) {
            tooltip = "Play as " + bestMatchEpilogue.gender + " for a chance to unlock another epilogue";
        } else if (bestMatchEpilogue.needsCharacter && (!mainSelect || bestMatchEpilogue.missingCharacter)) {
            var opp = loadedOpponents.find(function (p) {
                return p && p.id === bestMatchEpilogue.needsCharacter;
            });

            tooltip = "Play with " + (opp ? opp.selectLabel : bestMatchEpilogue.needsCharacter.initCap())
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

    this.slot = undefined;
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
            this.size = $xml.children('size').text();
            this.stamina = Number($xml.children('timer').text());
            this.intelligences = $xml.children('intelligence');

            /* Load in the legacy "start" lines, and also
             * initialize player.chosenState to the first listed line.
             * This may be overridden by later updateBehaviour calls if
             * the player has (new-style) selected or game start case lines.
             */
            this.startStates = $xml.children('start').children('state').get().map(function (el) {
                return new State($(el));
            });

            this.stylesheet = null;

            var stylesheet = $xml.children('stylesheet').text();
            if (stylesheet) {
                var m = stylesheet.match(/[a-zA-Z0-9()~!*:@,;\-.\/]+\.css/i);
                if (m) {
                    this.stylesheet = 'opponents/'+this.id+'/'+m[0];
                }
            }

            this.default_costume = {
                id: null,
                labels: $xml.children('label'),
                tags: null,
                folders: this.folder,
                wardrobe: $xml.children('wardrobe'),
                gender: $xml.children('gender').text(),
            };

            var poses = $xml.children('poses');
            var poseDefs = {};
            $(poses).children('pose').each(function (i, elem) {
                var def = new PoseDefinition($(elem), this);
                poseDefs[def.id] = def;
            }.bind(this));

            this.default_costume.poses = poseDefs;

            var tagsArray = $xml.find('>tags>tag').map(function () {
                return {
                    'tag': canonicalizeTag($(this).text()),
                    'from': $(this).attr('from'),
                    'to': $(this).attr('to'),
                }
            }).get();

            this.default_costume.tags = tagsArray;

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

            if (this.xml.children('behaviour').children('trigger').length > 0) {
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
 * @returns {$.Promise} A Promise. Progress callbacks are fired after each
 * chunk of work, and the promise resolves once all cases have been processed.
 * All callbacks are fired with the Opponent as `this`.
 */
Opponent.prototype.loadXMLTriggers = function () {
    var deferred = $.Deferred();

    var $cases = this.xml.find('>behaviour>trigger>case');

    var loadItemsTotal = $cases.length;
    if (loadItemsTotal == 0) {
        return deferred.resolveWith(this, [0]).promise();
    }
    var loadItemsCompleted = 0;

    function process() {
        var startTS = performance.now();

        /* break tasks into roughly 50ms chunks */
        while (performance.now() - startTS < 50) {
            if (loadItemsCompleted >= loadItemsTotal) {
                deferred.resolveWith(this, [loadItemsCompleted]);
                return;
            }

            let $case = $($cases.get(loadItemsCompleted));
            let c = new Case($case);
            let tag = $case.parent().attr('id');
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
        setTimeout(process.bind(this), 10);
    }

    setTimeout(process.bind(this), 0);
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

    var $cases = this.xml.find('>behaviour>stage>case');

    var loadItemsTotal = $cases.length;
    if (loadItemsTotal == 0) {
        return deferred.resolveWith(this, [0]).promise();
    }
    var loadItemsCompleted = 0;

    function process() {
        var startTS = performance.now();

        /* break tasks into roughly 50ms chunks */
        while (performance.now() - startTS < 50) {
            if (loadItemsCompleted >= loadItemsTotal) {
                deferred.resolveWith(this, [loadItemsCompleted]);
                return;
            }

            let $case = $($cases.get(loadItemsCompleted));
            let c = new Case($case);
            let stage = $case.parent().attr('id');
            this.recordTargetedCase(c);

            var key = c.tag + ':' + stage;
            if (!this.cases.has(key)) {
                this.cases.set(key, []);
            }

            this.cases.get(key).push(c);

            loadItemsCompleted++;
        }

        deferred.notifyWith(this, [loadItemsCompleted, loadItemsTotal]);
        setTimeout(process.bind(this), 10);
    }

    setTimeout(process.bind(this), 0);
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
