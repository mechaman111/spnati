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
function createNewClothing (formal, generic, type, position, image, plural, id) {
	var newClothingObject = {formal:formal,
						     generic:generic,
						     type:type,
						     position:position,
                             image:image,
							 plural:plural||false,
							 id:id};

	return newClothingObject;
}

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
				return c.position == LOWER_ARTICLE && c !== clothing;
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
			if (p.clothing.length <= player.clothing.length - 1) {
				isMin = false;
			}
			if (p.clothing.length >= player.clothing.length - 1) {
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
			if (clothing.length == 1 && clothing[0].type == IMPORTANT_ARTICLE) {
				if (players[HUMAN_PLAYER].gender == eGender.MALE) {
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
				players[HUMAN_PLAYER].removedClothing = clothing[0];
			} else {
				if (players[HUMAN_PLAYER].gender == eGender.MALE) {
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
	
	saveAllTranscriptEntries();

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
			players[HUMAN_PLAYER].gender == eGender.MALE ? MALE_HUMAN_MUST_STRIP : FEMALE_HUMAN_MUST_STRIP
		);
    } else {
        var toBeRemovedClothing = players[player].clothing[players[player].clothing.length - 1];
        players[player].removedClothing = toBeRemovedClothing;
        var dialogueTrigger = getClothingTrigger(players[player], toBeRemovedClothing, false);
        dialogueTrigger.push(OPPONENT_STRIPPING);

        updateAllBehaviours(player, PLAYER_STRIPPING, [dialogueTrigger]);
        players[player].preloadStageImages(players[player].stage + 1);
    }
	
	saveAllTranscriptEntries();
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
  for (var i = 0; i < players[HUMAN_PLAYER].clothing.length; i++) {
    var clothingCard =
      "<div class='clothing-modal-container'><input type='image' class='bordered modal-clothing-image' src="+
      players[HUMAN_PLAYER].clothing[i].image+" onclick='selectClothingToStrip.bind(this)("+i+")'/></div>";

    $stripClothing.append(clothingCard);
  }

  /* disable the strip button */
  $stripButton.attr('disabled', true);

  /* display the stripping modal */
  $stripModal.modal('show');

  /* hijack keybindings */
  KEYBINDINGS_ENABLED = true;
  document.removeEventListener('keyup', game_keyUp, false);
  document.addEventListener('keyup', clothing_keyUp, false);
}

/************************************************************
 * The human player clicked on an article of clothing in
 * the stripping modal.
 ************************************************************/
function selectClothingToStrip (id) {
  console.log(id);
  if (players[HUMAN_PLAYER].clothing.length <= id) {
    console.error('Error: Attempted to select clothing out of bounds', id, players[HUMAN_PLAYER].clothing);
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
  if (KEYBINDINGS_ENABLED) {
    if (e.keyCode == 32 && !$stripButton.prop('disabled')) { // Space
      $stripButton.click();
    }
    else if (e.keyCode >= 49 && e.keyCode < 49 + players[HUMAN_PLAYER].clothing.length) { // A number key
      $('.clothing-modal-container:nth-child('+(e.keyCode - 48)+') > .modal-clothing-image').click();
    }
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
				
        /* return keybindings */
        KEYBINDINGS_ENABLED = true;
        document.removeEventListener('keyup', clothing_keyUp, false);
        document.addEventListener('keyup', game_keyUp, false);

        /* grab the removed article of clothing */
        var removedClothing = players[HUMAN_PLAYER].clothing[id];

        players[HUMAN_PLAYER].clothing.splice(id, 1);
        players[HUMAN_PLAYER].timeInStage = -1;
        players[HUMAN_PLAYER].removedClothing = removedClothing;

        /* figure out if it should be important */
        if ([UPPER_ARTICLE, LOWER_ARTICLE, FULL_ARTICLE].indexOf(removedClothing.position) >= 0
            && (removedClothing.type == IMPORTANT_ARTICLE || removedClothing.type == MAJOR_ARTICLE)) {
            var otherClothing;
            for (var i = 0; i < players[HUMAN_PLAYER].clothing.length; i++) {
                if (players[HUMAN_PLAYER].clothing[i].position === removedClothing.position
                    && players[HUMAN_PLAYER].clothing[i].type != MINOR_ARTICLE) {
                    console.log(players[HUMAN_PLAYER].clothing[i]);
                    otherClothing = players[HUMAN_PLAYER].clothing[i];
                    break;
                }
            }
            console.log(otherClothing);
            if (!otherClothing) {
                removedClothing.type = IMPORTANT_ARTICLE;
            } else if (removedClothing.type == IMPORTANT_ARTICLE) {
                removedClothing.type = MAJOR_ARTICLE;
                /* Just make any other remaining article important instead,
                   so that, if it is the last one, it's considered as such by
                   playerMustStrip() */
                otherClothing.type = IMPORTANT_ARTICLE;
            }
        }
        if ([IMPORTANT_ARTICLE, MAJOR_ARTICLE, MINOR_ARTICLE].indexOf(removedClothing.type) >= 0) {
            players[HUMAN_PLAYER].mostlyClothed = false;
        }
        if ([IMPORTANT_ARTICLE, MAJOR_ARTICLE].indexOf(removedClothing.type) >= 0
			&& [UPPER_ARTICLE, LOWER_ARTICLE, FULL_ARTICLE].indexOf(removedClothing.position) >= 0) {
            players[HUMAN_PLAYER].decent = false;
        }
        if (removedClothing.type == IMPORTANT_ARTICLE) {
            players[HUMAN_PLAYER].exposed[removedClothing.position] = true;
        }
        
        /* determine its dialogue trigger */
        var dialogueTrigger = getClothingTrigger(players[HUMAN_PLAYER], removedClothing, true);
        console.log(removedClothing);
        /* display the remaining clothing */
        displayHumanPlayerClothing();
        
        /* count the clothing the player has remaining */
        players[HUMAN_PLAYER].stage++
        
        /* update label */
        if (players[HUMAN_PLAYER].clothing.length > 0) {
            $gameClothingLabel.html("Your Remaining Clothing");
        } else {
            $gameClothingLabel.html("You're Naked");
        }
            
        /* update behaviour */
        dialogueTrigger.push(OPPONENT_STRIPPED);
        updateAllBehaviours(HUMAN_PLAYER, null, [dialogueTrigger]);

		saveAllTranscriptEntries();

        /* allow progression */
        $('#stripping-modal').modal('hide');
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
	if (removedClothing.type === IMPORTANT_ARTICLE) {
	    players[player].exposed[removedClothing.position] = true;
	    players[player].decent = false;
	} else if (removedClothing.type === MAJOR_ARTICLE) {
		for (position in players[player].exposed) {
			if (!players[player].clothing.some(function(c) {
				return (c.type == IMPORTANT_ARTICLE || c.type == MAJOR_ARTICLE) && (c.position == position || c.position == FULL_ARTICLE);
			})) {
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
	
	saveAllTranscriptEntries();
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
		return checkPlayerStatus(player, stage)
	});
}

function checkPlayerStatus(player, status) {
	if (status.substr(0, 4) == "not_") {
		return !checkPlayerStatus(player, status.substr(4));
	}
	switch (status.trim()) {
	case STATUS_LOST_SOME:
		return player.stage > 0;
	case STATUS_MOSTLY_CLOTHED:
		return player.mostlyClothed;
	case STATUS_DECENT:
		return player.decent;
	case STATUS_EXPOSED_TOP:
		return player.exposed.upper;
	case STATUS_EXPOSED_BOTTOM:
		return player.exposed.lower;
	case STATUS_EXPOSED:
		return player.exposed.upper || player.exposed.lower;
	case STATUS_EXPOSED_TOP_ONLY:
		return player.exposed.upper && !player.exposed.lower;
	case STATUS_EXPOSED_BOTTOM_ONLY:
		return !player.exposed.upper && player.exposed.lower;
	case STATUS_NAKED:
		return player.exposed.upper && player.exposed.lower;
	case STATUS_ALIVE:
		return !player.out;
	case STATUS_LOST_ALL:
		return player.clothing.length == 0;
	case STATUS_MASTURBATING:
		return player.out && !player.finished;
	case STATUS_FINISHED:
		return player.finished;
	}
}
