/********************************************************************************
 This file contains the variables and functions that store information on player
 clothing and player stripping.
 ********************************************************************************/

/**********************************************************************
 *****                Clothing Object Specification               *****
 **********************************************************************/

/* clothing types */
var IMPORTANT_ARTICLE = "important";
var MAJOR_ARTICLE = "major";
var MINOR_ARTICLE = "minor";
var EXTRA_ARTICLE = "extra";

/* clothing positions */
var UPPER_ARTICLE = "upper";
var LOWER_ARTICLE = "lower";
var FULL_ARTICLE = "both";
var OTHER_ARTICLE = "other";

var STATUS_LOST_SOME = "lost_some"
var STATUS_MOSTLY_CLOTHED = "mostly_clothed"
var STATUS_DECENT = "decent";
var STATUS_EXPOSED = "exposed";
var STATUS_EXPOSED_TOP = "chest_visible";
var STATUS_EXPOSED_BOTTOM = "crotch_visible";
var STATUS_EXPOSED_TOP_ONLY = "topless";
var STATUS_EXPOSED_BOTTOM_ONLY = "bottomless";
var STATUS_NAKED = "naked";
var STATUS_LOST_ALL = "lost_all";
var STATUS_ALIVE = "alive";
var STATUS_MASTURBATING = "masturbating";
var STATUS_FINISHED = "finished";

/************************************************************
 * Stores information on an article of clothing.
 ************************************************************/
function Clothing (name, generic, type, position, plural) {
    this.name = name;
    this.generic = generic || name;
    this.type = type;
    this.position = position;
    this.plural = (plural === undefined ? false : plural);
}

/*************************************************************
 * Check if the player has major articles covering both the upper and
 * lower body.  Currently only used to determine whether the human
 * player is "decent".
 *************************************************************/
Player.prototype.isDecent = function() {
    return !(this.exposed.upper || this.exposed.lower)
        && this.clothing.some(function(c) {
            return (c.position == UPPER_ARTICLE || c.position == FULL_ARTICLE) && c.type == MAJOR_ARTICLE;
        }) && this.clothing.some(function(c) {
            return (c.position == LOWER_ARTICLE || c.position == FULL_ARTICLE) && c.type == MAJOR_ARTICLE;
        });
};

/*************************************************************
 * Check if the player chest and/or crotch is covered (not exposed).
 *************************************************************/
Player.prototype.isCovered = function(position) {
    if (position == FULL_ARTICLE || position === undefined) {
        return [UPPER_ARTICLE, LOWER_ARTICLE].every(Player.prototype.isCovered, this);
    }
    return this.clothing.some(function(c) {
        return (c.type == IMPORTANT_ARTICLE || c.type == MAJOR_ARTICLE)
            && (c.position == position || c.position == FULL_ARTICLE);
    });
};

/**************************************************************
 * Look through player's remaining clothing for items of certain
 * types, in certain positions, and with certain names, excluding ones
 * covered by others. 
 **************************************************************/
Player.prototype.findClothing = function(types, positions, names) {
    var covered = { upper: false, lower: false };
    var matches = [];
    for (var i = this.clothing.length - 1; i >= 0; i--) {
        var article = this.clothing[i];
        if ((types == undefined || types.indexOf(article.type) >= 0)
            && (positions == undefined || positions.indexOf(article.position) >= 0)
                && (names == undefined || names.indexOf(article.name) >= 0
                    || names.indexOf(article.generic) >= 0)) {
            if (!(article.position == FULL_ARTICLE && covered.upper && covered.lower)
                && !covered[article.position]) {
                matches.push(article);
            }
        }
        if ([MINOR_ARTICLE, MAJOR_ARTICLE, IMPORTANT_ARTICLE].indexOf(article.type) >= 0) {
            for (var position in covered) {
                if (article.position == position || article.position == FULL_ARTICLE) {
                    covered[position] = true;
                }
            }
        }
    }
    return matches;
};

/**
 * 
 * @param {string} name The specific name of the clothing article.
 * @param {string} generic The generic name for the clothing article.
 * @param {string} type The clothing's type.
 * @param {string} position The clothing's position.
 * @param {string} image The image used for the clothing in selection menus.
 * @param {Boolean} plural Whether or not the clothing is plural.
 * @param {string} id A unique ID for this clothing. If this clothing is attached to a collectible,
 * the ID of the character associated with that collectible will be prepended to this ID.
 * @param {string} applicable_genders What genders are allowed to wear this clothing. Can be "all".
 * @param {Collectible} collectible An optional collectible that must be unlocked before this clothing can be selected.
 */
function PlayerClothing (
    name, generic, type, position, image, plural, id, applicable_genders, collectible
) {
    Clothing.call(this, name, generic, type, position, plural);

    this.id = ((collectible && collectible.player) ? "" : "_default.") + id;
    this.image = image;
    this.applicable_genders = applicable_genders.toLowerCase();
    this.collectible = collectible;
}

PlayerClothing.prototype = Object.create(Clothing.prototype);
PlayerClothing.prototype.constructor = PlayerClothing;

/**
 * Get whether this clothing is available to wear at the title screen.
 * @returns {Boolean}
 */
PlayerClothing.prototype.isAvailable = function () {
    if (this.applicable_genders !== "all" && humanPlayer.gender !== this.applicable_genders) {
        return false;
    }

    return !this.collectible || this.collectible.isUnlocked();
}

/**
 * Create a basic HTML <input> element for this clothing.
 * @returns {HTMLInputElement}
 */
PlayerClothing.prototype.createSelectionElement = function () {
    var title = this.name.initCap();
    if (this.collectible && this.collectible.player) {
        title += " (from " + this.collectible.player.metaLabel + ")";
    }

    var img = document.createElement("img");
    img.setAttribute("src", this.image);
    img.setAttribute("alt", title);
    img.className = "custom-clothing-img";

    var elem = document.createElement("button");
    elem.setAttribute("title", title);
    elem.className = "bordered player-clothing-select";
    elem.appendChild(img);

    return elem;
}

/**
 * Get whether this clothing has been selected for the current player gender.
 * @returns {Boolean}
 */
PlayerClothing.prototype.isSelected = function () {
    return save.isClothingSelected(this);
}

/**
 * Set whether this clothing has been selected for the current player gender.
 * @param {Boolean} selected 
 */
PlayerClothing.prototype.setSelected = function (selected) {
    return save.setClothingSelected(this, selected);
}

/**********************************************************************
 *****                    Stripping Variables                     *****
 **********************************************************************/

/* stripping modal */
$stripModal = $("#stripping-modal");
$stripClothing = $("#stripping-clothing-area");
$stripButton = $("#stripping-modal-button");

/**
 * @type {StripClothingSelectionIcon[]}
 */
var clothingStripSelectors = [];

/**********************************************************************
 *****                      Strip Functions                       *****
 **********************************************************************/

 /**
  * Calculate the position a removed article of clothing reveals, if any.
  *
  * This is either FULL_ARTICLE, LOWER_ARTICLE, UPPER_ARTICLE, or null.
  *
  * @param {Player} player
  * @param {Clothing} clothing
  * @returns {string?} The revealed position, if any.
  */
function getRevealedPosition (player, clothing) {
    var type = clothing.type;
    var pos = clothing.position;

    /* Reveals only happen for important and major articles... */
    if (type == IMPORTANT_ARTICLE || type == MAJOR_ARTICLE) {
        var hasLower = player.clothing.some(function(c) {
            return ([LOWER_ARTICLE, FULL_ARTICLE].indexOf(c.position) >= 0) && (c !== clothing) && ([IMPORTANT_ARTICLE, MAJOR_ARTICLE].indexOf(c.type) >= 0);
        });

        var hasUpper = player.clothing.some(function(c) {
            return ([UPPER_ARTICLE, FULL_ARTICLE].indexOf(c.position) >= 0) && (c !== clothing) && ([IMPORTANT_ARTICLE, MAJOR_ARTICLE].indexOf(c.type) >= 0);
        });

        if (pos == FULL_ARTICLE) {
            if (!hasLower && !hasUpper) {
                /* Article exposes both at once.
                 * We can actually return early in this case:
                 * we'd do the same thing for both major and important articles anyways.
                 */
                return FULL_ARTICLE;
            } else if (!hasLower && hasUpper) {
                /* Article only exposes crotch. */
                pos = LOWER_ARTICLE;
            } else if (hasLower && !hasUpper) {
                /* Article only exposes chest. */
                pos = UPPER_ARTICLE;
            } else {
                /* Article doesn't actually reveal anything.
                 * For major items, we can just return null early.
                 * For important items, we'd end up returning FULL_ARTICLE anyways, no matter what.
                 */
                return (type == IMPORTANT_ARTICLE) ? FULL_ARTICLE : null;
            }
        }

        /* pos cannot be FULL_ARTICLE at this point. */

        if (
            (type == MAJOR_ARTICLE) && (
                ([UPPER_ARTICLE, LOWER_ARTICLE].indexOf(pos) < 0) ||
                ((pos == UPPER_ARTICLE) && hasUpper) ||
                ((pos == LOWER_ARTICLE) && hasLower)
            )
        ) {
            /* There is another article left covering this part of the body. */
            return null;
        }

        return pos;
    } else {
        return null;
    }
}

 /************************************************************
 * Fetches the appropriate dialogue trigger for the provided
 * article of clothing, based on whether the article is going
 * to be removed or has been removed. Written to prevent duplication.
 ************************************************************/
function getClothingTrigger (player, clothing, removed) {
    var revealPos = getRevealedPosition(player, clothing);
    var type = clothing.type;
    var gender = player.gender;
    var size = player.size;

    if (revealPos == UPPER_ARTICLE) {
        if (gender == eGender.MALE) {
            if (removed) {
                return [MALE_CHEST_IS_VISIBLE, OPPONENT_CHEST_IS_VISIBLE];
            } else {
                return [MALE_CHEST_WILL_BE_VISIBLE, OPPONENT_CHEST_WILL_BE_VISIBLE];
            }
        } else if (gender == eGender.FEMALE) {
            if (removed) {
                if (size == eSize.LARGE) {
                    return [FEMALE_LARGE_CHEST_IS_VISIBLE, FEMALE_CHEST_IS_VISIBLE, OPPONENT_CHEST_IS_VISIBLE];
                } else if (size == eSize.SMALL) {
                    return [FEMALE_SMALL_CHEST_IS_VISIBLE, FEMALE_CHEST_IS_VISIBLE, OPPONENT_CHEST_IS_VISIBLE];
                } else {
                    return [FEMALE_MEDIUM_CHEST_IS_VISIBLE, FEMALE_CHEST_IS_VISIBLE, OPPONENT_CHEST_IS_VISIBLE];
                }
            } else {
                return [FEMALE_CHEST_WILL_BE_VISIBLE, OPPONENT_CHEST_WILL_BE_VISIBLE];
            }
        }
    } else if ((revealPos == LOWER_ARTICLE) || (revealPos == FULL_ARTICLE)) {
        /* Treat full-article reveals as being crotch reveals for the purposes of case triggering. */
        if (gender == eGender.MALE) {
            if (removed) {
                if (size == eSize.LARGE) {
                    return [MALE_LARGE_CROTCH_IS_VISIBLE, MALE_CROTCH_IS_VISIBLE, OPPONENT_CROTCH_IS_VISIBLE];
                } else if (size == eSize.SMALL) {
                    return [MALE_SMALL_CROTCH_IS_VISIBLE, MALE_CROTCH_IS_VISIBLE, OPPONENT_CROTCH_IS_VISIBLE];
                } else {
                    return [MALE_MEDIUM_CROTCH_IS_VISIBLE, MALE_CROTCH_IS_VISIBLE, OPPONENT_CROTCH_IS_VISIBLE];
                }
            } else {
                return [MALE_CROTCH_WILL_BE_VISIBLE, OPPONENT_CROTCH_WILL_BE_VISIBLE];
            }
        } else if (gender == eGender.FEMALE) {
            if (removed) {
                return [FEMALE_CROTCH_IS_VISIBLE, OPPONENT_CROTCH_IS_VISIBLE];
            } else {
                return [FEMALE_CROTCH_WILL_BE_VISIBLE, OPPONENT_CROTCH_WILL_BE_VISIBLE];
            }
        }
    } else {
        if (type == MAJOR_ARTICLE) {
            if (gender == eGender.MALE) {
                return removed ? [MALE_REMOVED_MAJOR] : [MALE_REMOVING_MAJOR];
            } else if (gender == eGender.FEMALE) {
                return removed ? [FEMALE_REMOVED_MAJOR] : [FEMALE_REMOVING_MAJOR];
            }
        } else if (type == MINOR_ARTICLE) {
            if (gender == eGender.MALE) {
                return removed ? [MALE_REMOVED_MINOR] : [MALE_REMOVING_MINOR];
            } else if (gender == eGender.FEMALE) {
                return removed ? [FEMALE_REMOVED_MINOR] : [FEMALE_REMOVING_MINOR];
            }
        } else if (type == EXTRA_ARTICLE) {
            if (gender == eGender.MALE) {
                return removed ? [MALE_REMOVED_ACCESSORY] : [MALE_REMOVING_ACCESSORY];
            } else if (gender == eGender.FEMALE) {
                return removed ? [FEMALE_REMOVED_ACCESSORY] : [FEMALE_REMOVING_ACCESSORY];
            }
        }
    }

    /* Shouldn't get here... */
    console.error("Could not determine strip triggers for player ", player, " and clothing ", clothing);
    return [];
}

/************************************************************
 * Determines whether or not the provided player is winning
 * or losing and returns the appropriate dialogue trigger.
 ************************************************************/
function determineStrippingSituation (player) {
    /* determine if this player's clothing count is the highest or lowest */
    var isMax = true;
    var isMin = true;

    players.forEach(function(p) {
        if (p !== player) {
            if (p.countLayers() <= player.countLayers() - 1) {
                isMin = false;
            }
            if (p.countLayers() >= player.countLayers() - 1) {
                isMax = false;
            }
        }
    });

    /* return appropriate trigger */
    if (isMax) {
        return PLAYER_MUST_STRIP_WINNING;
    } else if (isMin) {
        return PLAYER_MUST_STRIP_LOSING;
    } else {
        return PLAYER_MUST_STRIP_NORMAL;
    }
}

/************************************************************
 * Manages the dialogue triggers before a player strips or forfeits.
 ************************************************************/
function playerMustStrip (player) {
    /* count the clothing the player has remaining */
    /* assume the player only has IMPORTANT_ARTICLES */
    var clothing = players[player].clothing;

    saveTranscriptMessage("<b>"+players[recentLoser].label.escapeHTML()+"</b> has lost the hand"
                          + (clothing.length > 0 ? '.' : ', and is out of clothes.'));

    if (clothing.length) {
        /* the player has clothes and will strip */
        if (player == HUMAN_PLAYER) {
            var trigger;
            if (clothing.length == 1 && (clothing[0].type == IMPORTANT_ARTICLE || clothing[0].type == MAJOR_ARTICLE)) {
                if (humanPlayer.gender == eGender.MALE) {
                    if (clothing[0].position == LOWER_ARTICLE) {
                        trigger = [[MALE_CROTCH_WILL_BE_VISIBLE, OPPONENT_CROTCH_WILL_BE_VISIBLE]];
                    } else {
                        trigger = [[MALE_CHEST_WILL_BE_VISIBLE, OPPONENT_CHEST_WILL_BE_VISIBLE]];
                    }
                } else {
                    if (clothing[0].position == LOWER_ARTICLE) {
                        trigger = [[FEMALE_CROTCH_WILL_BE_VISIBLE, OPPONENT_CROTCH_WILL_BE_VISIBLE]];
                    } else {
                        trigger = [[FEMALE_CHEST_WILL_BE_VISIBLE, OPPONENT_CHEST_WILL_BE_VISIBLE]];
                    }
                }
                humanPlayer.removedClothing = clothing[0];
            } else {
                if (humanPlayer.gender == eGender.MALE) {
                    trigger = [MALE_HUMAN_MUST_STRIP, MALE_MUST_STRIP, OPPONENT_LOST];
                } else {
                    trigger = [FEMALE_HUMAN_MUST_STRIP, FEMALE_MUST_STRIP, OPPONENT_LOST];
                }
            }
            
            updateAllBehaviours(player, null, trigger);
        } else {
            var trigger = determineStrippingSituation(players[player]);
            
            updateAllBehaviours(
                player,
                [trigger, PLAYER_MUST_STRIP],
                [[(players[player].gender == eGender.MALE ? MALE_MUST_STRIP : FEMALE_MUST_STRIP), OPPONENT_LOST]]
            );
        }
    } else {
        /* the player has no clothes and will have to accept a forfeit */
        var trigger = null;
        if (player != HUMAN_PLAYER) {
            trigger = determineForfeitSituation(player);
        }
        
        updateAllBehaviours(
            player,
            trigger,
            [[(players[player].gender == eGender.MALE ? MALE_MUST_MASTURBATE : FEMALE_MUST_MASTURBATE), OPPONENT_LOST]]
        );
        
        players[player].preloadStageImages(players[player].stage + 2);
    }
    
    return clothing.length;
}

/************************************************************
 * Manages the dialogue triggers as player begins to strip
 ************************************************************/
function prepareToStripPlayer (player) {
    if (player == HUMAN_PLAYER) { // Never happens (currently)
        updateAllBehaviours(
            player,
            null,
            humanPlayer.gender == eGender.MALE ? MALE_HUMAN_MUST_STRIP : FEMALE_HUMAN_MUST_STRIP
        );
    } else {
        var toBeRemovedClothing = players[player].clothing[players[player].clothing.length - 1];
        players[player].removedClothing = toBeRemovedClothing;
        var dialogueTrigger = getClothingTrigger(players[player], toBeRemovedClothing, false);
        dialogueTrigger.push(OPPONENT_STRIPPING);

        updateAllBehaviours(player, PLAYER_STRIPPING, [dialogueTrigger]);
        players[player].preloadStageImages(players[player].stage + 2);
    }
}


/**
 * @param {PlayerClothing} clothing 
 */
 function StripClothingSelectionIcon (clothing) {
    this.clothing = clothing;
    this.elem = clothing.createSelectionElement();
    this.selected = false;

    $(this.elem).on("click", this.select.bind(this)).addClass("player-strip-selector");
}

StripClothingSelectionIcon.prototype.canSelect = function () {
    return humanPlayer.clothing.some(function (clothing) {
        return clothing.id == this.clothing.id;
    }.bind(this));
}

StripClothingSelectionIcon.prototype.update = function () {
    $(this.elem).removeClass("available selected");

    if (this.canSelect()) {
        $(this.elem).addClass("available");

        if (this.selected) {
            $(this.elem).addClass("selected");
        }
    }
}

StripClothingSelectionIcon.prototype.select = function () {
    if (!this.canSelect) return;
    clothingStripSelectors.forEach(function (selector) {
        selector.selected = (selector === this);
        selector.update();
    }.bind(this));

    /* enable the strip button */
    $stripButton.attr('disabled', false).attr('onclick', 'closeStrippingModal();');
}

function setupStrippingModal () {
    clothingStripSelectors = humanPlayer.clothing.map(function (clothing) {
        return new StripClothingSelectionIcon(clothing);
    });

    $stripClothing.empty().append(clothingStripSelectors.map(function (selector) {
        return selector.elem;
    }));
}


/************************************************************
 * Sets up and displays the stripping modal, so that the human
 * player can select an article of clothing to remove.
 ************************************************************/
function showStrippingModal () {
    console.log("The stripping modal is being shown.");

    clothingStripSelectors.forEach(function (selector) {
        selector.selected = false;
        selector.update();
    }.bind(this));

    /* disable the strip button */
    $stripButton.attr('disabled', true);

    /* display the stripping modal */
    $stripModal.modal({show: true, keyboard: false, backdrop: 'static'});
    $stripModal.one('shown.bs.modal', function() {
        $stripClothing.find('button').last().focus();
    });
    $stripModal.on('hidden.bs.modal', function () {
        if (gamePhase === eGamePhase.STRIP) {
            console.error("Possible softlock: player strip modal hidden with game phase still at STRIP");
            
            Sentry.captureException(new Error("Possible softlock: player strip modal hidden with phase still at STRIP"));

            allowProgression();
        }
    });

    $(document).keyup(clothing_keyUp);
}

/************************************************************
 * A keybound handler.
 ************************************************************/
function clothing_keyUp(e) {
    var availableSelectors = clothingStripSelectors.filter(function (selector) {
        return selector.canSelect();
    });

    if (e.key == ' ' && !$stripButton.prop('disabled')  // Space
        && !($('body').hasClass('focus-indicators-enabled') && $(document.activeElement).is('button:not(.selected)'))
        && availableSelectors.some(function (selector) { return selector.selected; })) {
        $stripButton.click();
        e.preventDefault();
    } else if (e.key >= '1' && e.key <= 1 + availableSelectors.length) { // A number key
        availableSelectors[e.key - 1].select();
    }
}

/************************************************************
 * The human player closed the stripping modal. Removes an
 * article of clothing from the human player.
 ************************************************************/
function closeStrippingModal () {
    var selector = clothingStripSelectors.find(function (selector) {
        return selector.selected;
    });

    if (!selector) {
        /* how the hell did this happen? */
        console.log("Error: there was no selected article.");
        showStrippingModal();
        return;
    }

    /* prevent double-clicking the stripping modal buttons. */
    $stripButton.attr('disabled', true).removeAttr('onclick');
    
    /* grab the removed article of clothing */
    var removedClothing = selector.clothing;
    var origClothingType = removedClothing.type;
    var clothingIdx = humanPlayer.clothing.indexOf(removedClothing);

    if (clothingIdx == -1) {
        console.log("Error: could not find clothing to remove");
        showStrippingModal();
        return;
    }

    humanPlayer.clothing.splice(clothingIdx, 1);
    humanPlayer.timeInStage = -1;
    humanPlayer.ticksInStage = 0;
    humanPlayer.removedClothing = removedClothing;
    humanPlayer.numStripped[removedClothing.type]++;

    /* figure out if it should be important */
    if ([UPPER_ARTICLE, LOWER_ARTICLE, FULL_ARTICLE].indexOf(removedClothing.position) >= 0
        && ([IMPORTANT_ARTICLE, MAJOR_ARTICLE].indexOf(removedClothing.type) >= 0)) {
        for (position in humanPlayer.exposed) {
            if (!humanPlayer.isCovered(position)) {
                humanPlayer.exposed[position] = true;
            } else if ((removedClothing.type == IMPORTANT_ARTICLE && removedClothing.position == position)) {
                removedClothing.type = MAJOR_ARTICLE;
            }
        }
        // For the future; there are no human clothes with position = both, especially no important ones.
        if (removedClothing.position == FULL_ARTICLE && removedClothing.type == IMPORTANT_ARTICLE && humanPlayer.isCovered()) {
            removedClothing.type = MAJOR_ARTICLE;
        }
    }
    if ([IMPORTANT_ARTICLE, MAJOR_ARTICLE, MINOR_ARTICLE].indexOf(removedClothing.type) >= 0) {
        humanPlayer.mostlyClothed = false;
    }
    humanPlayer.decent = humanPlayer.isDecent();
    
    /* determine its dialogue trigger */
    var dialogueTrigger = getClothingTrigger(humanPlayer, removedClothing, true);
    console.log(removedClothing);
    removedClothing.type = origClothingType;
    /* display the remaining clothing */
    displayHumanPlayerClothing();
    
    /* count the clothing the player has remaining */
    humanPlayer.stage++
    
    /* update label */
    if (humanPlayer.clothing.length > 0) {
        $gameClothingLabel.html("Your Remaining Clothing");
    } else {
        $gameClothingLabel.html("You're Naked");
    }
        
    /* update behaviour */
    dialogueTrigger.push(OPPONENT_STRIPPED);
    updateAllBehaviours(HUMAN_PLAYER, null, [dialogueTrigger]);

    /* allow progression */
    $('#stripping-modal').modal('hide');
    $stripModal.off("hidden.bs.modal");
    $(document).off('keyup', clothing_keyUp);
    endRound();
}

/************************************************************
 * Removes an article of clothing from an AI player. Also
 * handles all of the dialogue triggers involved in the process.
 ************************************************************/
function stripAIPlayer (player) {
    console.log("Opponent "+player+" is being stripped.");

    /* grab the removed article of clothing and determine its dialogue trigger */
    var removedClothing = players[player].clothing.pop();
    players[player].removedClothing = removedClothing;
    players[player].numStripped[removedClothing.type]++;
    if ([IMPORTANT_ARTICLE, MAJOR_ARTICLE, MINOR_ARTICLE].indexOf(removedClothing.type) >= 0) {
        players[player].mostlyClothed = false;
    }
    if ([IMPORTANT_ARTICLE, MAJOR_ARTICLE].indexOf(removedClothing.type) >= 0) {
        for (position in players[player].exposed) {
            if ((removedClothing.type == IMPORTANT_ARTICLE && position == removedClothing.position)
                || !players[player].isCovered(position)) {
                players[player].exposed[position] = true;
            }
        }
        players[player].decent = false;
    }
    var dialogueTrigger = getClothingTrigger(players[player], removedClothing, true);

    players[player].stage++;
    players[player].timeInStage = -1;
    players[player].ticksInStage = 0;
    players[player].stageChangeUpdate();

    /* update behaviour */
    dialogueTrigger.push(OPPONENT_STRIPPED);
    updateAllBehaviours(player, PLAYER_STRIPPED, [dialogueTrigger]);
}

/************************************************************
 * Determines whether or not the provided player is winning
 * or losing at the end and returns the appropriate dialogue trigger.
 ************************************************************/
function determineForfeitSituation (player) {
    /* check to see how many players are out */
    for (var i = 0; i < players.length; i++) {
            if (players[i] && players[i].out) {
                if (players[i].out) {
            return PLAYER_MUST_MASTURBATE;
                }
            }
    }
    return PLAYER_MUST_MASTURBATE_FIRST;
}

/************************************************************
 * Removes an article of clothing from the selected player.
 * Also handles all of the dialogue triggers involved in the
 * process.
 ************************************************************/
function stripPlayer (player) {
    if (player == HUMAN_PLAYER) {
        showStrippingModal();
    } else {
        stripAIPlayer(player);
        /* allow progression */
        endRound();
    }
}

/************************************************************
 * Counts the number of players in a certain stage
 ************************************************************/
function getNumPlayersInStage(stage) {
    return players.countTrue(function(player) {
        return player.checkStatus(stage)
    });
}

/************************************************************
 * Updates .biggestLead of the leader
 ************************************************************/
function updateBiggestLead() {
    var sortedPlayers = players.slice().sort(function(a, b) {
        return b.countLayers() - a.countLayers();
    });
    if (sortedPlayers[0].countLayers() - sortedPlayers[1].countLayers() > sortedPlayers[0].biggestLead) {
        sortedPlayers[0].biggestLead = sortedPlayers[0].countLayers() - sortedPlayers[1].countLayers();
    }
}
