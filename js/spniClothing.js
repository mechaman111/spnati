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
function Clothing (name, generic, type, position, image, plural, id) {
    this.name = name;
    this.generic = generic || name;
    this.type = type;
    this.position = position;
    this.image = image;
    this.plural = plural || false;
    this.id = id;
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

/**********************************************************************
 *****                    Stripping Variables                     *****
 **********************************************************************/

/* stripping modal */
$stripModal = $("#stripping-modal");
$stripClothing = $("#stripping-clothing-area");
$stripButton = $("#stripping-modal-button");

/**********************************************************************
 *****                      Strip Functions                       *****
 **********************************************************************/

 /************************************************************
 * Fetches the appropriate dialogue trigger for the provided
 * article of clothing, based on whether the article is going
 * to be removed or has been removed. Written to prevent duplication.
 ************************************************************/
function getClothingTrigger (player, clothing, removed) {
	var type = clothing.type;
	var pos = clothing.position;
	var gender = player.gender;
	var size = player.size;

	/* starting with important articles */
	if (type == IMPORTANT_ARTICLE || type == MAJOR_ARTICLE) {
		if (pos == FULL_ARTICLE) {
			if (!player.clothing.some(function(c) {
				return c.position == LOWER_ARTICLE && c !== clothing && [IMPORTANT_ARTICLE, MAJOR_ARTICLE].indexOf(c.type) >= 0;
			})) {
				// If removing this article exposes the crotch,
				// pretend that it's an lower body article, even if it
				// also exposes the chest (which is not a good idea).
				pos = LOWER_ARTICLE;
			} else {
				// Otherwise treat it as a upper body article, whether
				// it exposes the chest or not (it doesn't matter,
				// except for with an important article).
				pos = UPPER_ARTICLE;
			}
		}
		if (type == MAJOR_ARTICLE
			&& ([UPPER_ARTICLE, LOWER_ARTICLE, FULL_ARTICLE].indexOf(pos) < 0 || player.clothing.some(function(c) {
				return (c.position == pos || c.position == FULL_ARTICLE)
					&& c !== clothing && (c.type == IMPORTANT_ARTICLE || c.type == MAJOR_ARTICLE);
			}))) { // There is another article left covering this part of the body
			if (gender == eGender.MALE) {
				if (removed) {
					return [MALE_REMOVED_MAJOR];
				} else {
					return [MALE_REMOVING_MAJOR];
				}
			} else if (gender == eGender.FEMALE) {
				if (removed) {
					return [FEMALE_REMOVED_MAJOR];
				} else {
					return [FEMALE_REMOVING_MAJOR];
				}
			}
		} else if (pos == UPPER_ARTICLE) {
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
		} else if (pos == LOWER_ARTICLE) {
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
		}
	}
	/* next minor articles */
	else if (type == MINOR_ARTICLE) {
		if (gender == eGender.MALE) {
			if (removed) {
				return [MALE_REMOVED_MINOR];
			} else {
				return [MALE_REMOVING_MINOR];
			}
		} else if (gender == eGender.FEMALE) {
			if (removed) {
				return [FEMALE_REMOVED_MINOR];
			} else {
				return [FEMALE_REMOVING_MINOR];
			}
		}
	}
	/* next accessories */
	else {
		if (gender == eGender.MALE) {
			if (removed) {
				return [MALE_REMOVED_ACCESSORY];
			} else {
				return [MALE_REMOVING_ACCESSORY];
			}
		} else if (gender == eGender.FEMALE) {
			if (removed) {
				return [FEMALE_REMOVED_ACCESSORY];
			} else {
				return [FEMALE_REMOVING_ACCESSORY];
			}
		}
	}
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
				    trigger = [[MALE_HUMAN_MUST_STRIP, OPPONENT_LOST], [MALE_MUST_STRIP, OPPONENT_LOST]];
				} else {
				    trigger = [[FEMALE_HUMAN_MUST_STRIP, OPPONENT_LOST], [FEMALE_MUST_STRIP, OPPONENT_LOST]];
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
		
        players[player].preloadStageImages(players[player].stage + 1);
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
        players[player].preloadStageImages(players[player].stage + 1);
    }
}

/************************************************************
 * Sets up and displays the stripping modal, so that the human
 * player can select an article of clothing to remove.
 ************************************************************/
function showStrippingModal () {
  console.log("The stripping modal is being set up.");
  
  /* Prevent double-clicks from calling up the modal twice */
  $mainButton.attr('disabled', true);
  actualMainButtonState = true;

  /* clear the area */
  $stripClothing.html("");

  /* load the player's clothing into the modal */
  for (var i = 0; i < humanPlayer.clothing.length; i++) {
    var clothingCard =
      "<div class='clothing-modal-container'><input type='image' class='bordered modal-clothing-image' src="+
      humanPlayer.clothing[i].image+" onclick='selectClothingToStrip.bind(this)("+i+")'/></div>";

    $stripClothing.append(clothingCard);
  }

  /* disable the strip button */
  $stripButton.attr('disabled', true);

    /* display the stripping modal */
    $stripModal.modal({show: true, keyboard: false, backdrop: 'static'});
    $stripModal.one('shown.bs.modal', function() {
        $stripClothing.find('input').last().focus();
    });
    $stripModal.on('hidden.bs.modal', function () {
        if (gamePhase === eGamePhase.STRIP) {
            console.error("Possible softlock: player strip modal hidden with game phase still at STRIP");

            if (SENTRY_INITIALIZED) {
                Sentry.captureException(new Error("Possible softlock: player strip modal hidden with phase still at STRIP"));
            }

            allowProgression();
        }
    });
    $(document).keyup(clothing_keyUp);
}

/************************************************************
 * The human player clicked on an article of clothing in
 * the stripping modal.
 ************************************************************/
function selectClothingToStrip (id) {
  console.log(id);
  if (humanPlayer.clothing.length <= id) {
    console.error('Error: Attempted to select clothing out of bounds', id, humanPlayer.clothing);
    return;
  }

  /* designate the selected article */
  $(".modal-selected-clothing-image").removeClass("modal-selected-clothing-image");
  $(this).addClass("modal-selected-clothing-image");

  /* enable the strip button */
  $stripButton.attr('disabled', false).attr('onclick', 'closeStrippingModal(' + id + ');');
}

/************************************************************
 * A keybound handler.
 ************************************************************/
function clothing_keyUp(e) {
    if (e.keyCode == 32 && !$stripButton.prop('disabled')  // Space
        && $('.modal-clothing-image:focus').not('.modal-selected-clothing-image').length == 0) {
		$stripButton.click();
        e.preventDefault();
    } else if (e.keyCode >= 49 && e.keyCode < 49 + humanPlayer.clothing.length) { // A number key
        $('.clothing-modal-container:nth-child('+(e.keyCode - 48)+') > .modal-clothing-image').focus().click();
    }
}

/************************************************************
 * The human player closed the stripping modal. Removes an
 * article of clothing from the human player.
 ************************************************************/
function closeStrippingModal (id) {
    if (id >= 0) {
		/* prevent double-clicking the stripping modal buttons. */
		$stripButton.attr('disabled', true).removeAttr('onclick');
		$stripClothing.html("");
				
        /* grab the removed article of clothing */
        var removedClothing = humanPlayer.clothing[id];
        var origClothingType = removedClothing.type;

        humanPlayer.clothing.splice(id, 1);
        humanPlayer.timeInStage = -1;
        humanPlayer.removedClothing = removedClothing;

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
    } else {
        /* how the hell did this happen? */
        console.log("Error: there was no selected article.");
        showStrippingModal();
    }
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
