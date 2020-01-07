/********************************************************************************
 This file contains the variables and functions that allow for players to have
 multiple potential forfeits.
 ********************************************************************************/
 
/**********************************************************************
 *****                 Forfeit Object Specification               *****
 **********************************************************************/

var CAN_SPEAK = true;
var CANNOT_SPEAK = false;
 
/**********************************************************************
 *****                      Forfeit Variables                     *****
 **********************************************************************/
 
/* orgasm timer */
var ORGASM_DELAY = 2000;

/* The first and last rounds a character starts heavy masturbation, counted in phases before they finish */
var HEAVY_FIRST_ROUND = 6;
var HEAVY_LAST_ROUND = 2;
 
/**********************************************************************
 *****                      Forfeit Functions                     *****
 **********************************************************************/

/************************************************************
 * Sets the forfeit timer of the given player, with a random
 * offset, if applicable.
 ************************************************************/
Player.prototype.setForfeitTimer = function() {
	// THE TIMER IS HARD SET RIGHT NOW
	this.timer = this.stamina;
	
	// THE STAGE IS HARD SET RIGHT NOW
	this.stage += 1;
	this.timeInStage = -1;
	this.stageChangeUpdate();
}

/************************************************************
 * Initiate masturbation for the selected player
 ************************************************************/
function startMasturbation (player) {
	players[player].forfeit = [PLAYER_MASTURBATING, CAN_SPEAK];
	players[player].out = true;
    players[player].outOrder = players.countTrue(function(p) { return p.out; });

    if (chosenDebug === player) {
        chosenDebug = -1;
        updateDebugState(showDebug);
    }

	/* update behaviour */
    updateAllBehaviours(
        player, 
        PLAYER_START_MASTURBATING,
        [[players[player].gender == eGender.MALE ? MALE_START_MASTURBATING : FEMALE_START_MASTURBATING,
         OPPONENT_START_MASTURBATING]]
    );

    players[player].setForfeitTimer();
    
    if (player == HUMAN_PLAYER) {
        $gameClothingLabel.html("You're Masturbating...");
        $gamePlayerCountdown.html(humanPlayer.timer);
        $gamePlayerCountdown.show();
    }
    
	/* allow progression */
	endRound();
}

/************************************************************
 * The forfeit timers of all players tick down, if they have 
 * been set.
 ************************************************************/
function tickForfeitTimers () {
    console.log("Ticking forfeit timers...");
    
    var masturbatingPlayers = [], heavyMasturbatingPlayers = [];

	for (var i = 0; i < players.length; i++) {
		if (players[i] && players[i].out && !players[i].finished && players[i].timer == 0) {
			finishMasturbation(i);
			return true;
		}
	}

    if (gamePhase != eGamePhase.STRIP) for (var i = 0; i < players.length; i++) {
        if (players[i] && players[i].out && players[i].timer == 1) {
            players[i].timer = 0;
            /* set the button state */
            $mainButton.html("Cumming...");
            $mainButton.attr('disabled', true);
            actualMainButtonState = true;

            /* hide everyone else's dialogue bubble */
            gameDisplays.forEach(function (d) {
                if (d.slot != i) d.hideBubble();
            });

            if (i == HUMAN_PLAYER) {
                /* player's timer is up */
                /* TEMP FIX: prevent this animation on Safari */
                if (PLAYER_FINISHING_EFFECT && (navigator.userAgent.indexOf("Safari") == -1)) {
                    $gamePlayerCountdown.one('animationend', function() {
                        $gamePlayerCountdown.hide();
                        $gamePlayerCountdown.removeClass('explode');
                        /* finish */
                        finishMasturbation(i);
                    });
                    $gamePlayerCountdown.addClass('explode');
                } else {
                    $gamePlayerCountdown.hide();
                    finishMasturbation(i);
                }
                console.log(players[i].label+" is finishing!");
                $gamePlayerCountdown.html('');
                $gameClothingLabel.html("<b>You're 'Finished'</b>");

            } else {
                console.log(players[i].label+" is finishing!");

                /* let the player speak again */
                players[i].forfeit = [PLAYER_FINISHING_MASTURBATING, CAN_SPEAK];

                /* show them cumming */
                players[i].updateBehaviour(PLAYER_FINISHING_MASTURBATING);
                players[i].commitBehaviourUpdate();
                
                saveSingleTranscriptEntry(i);

                /* trigger the callback */
                var player = i, tableVisible = (tableOpacity > 0);
                timeoutID = window.setTimeout(function(){ allowProgression(eGamePhase.END_FORFEIT); }, ORGASM_DELAY);
				globalSavedTableVisibility = tableVisible;
                if (AUTO_FADE) forceTableVisibility(false);
                players[i].preloadStageImages(players[i].stage + 1);
            }
            return true;
        }
    }

    for (var i = 0; i < players.length; i++) {
        if (players[i] && players[i].out && players[i].timer > 1) {
            players[i].timer--;

			if (i == HUMAN_PLAYER) {
				/* human player */
				/* update the player label */
				$gameClothingLabel.html("<b>'Finished' in "+players[i].timer+" phases</b>");
				$gamePlayerCountdown.html(players[i].timer);
                if (players[i].timer <= 4) {
                    players[i].forfeit[0] = PLAYER_HEAVY_MASTURBATING;
                    $gamePlayerCountdown.addClass('pulse');
                }
				masturbatingPlayers.push(i); // Double the chance of commenting on human player
			} else {
				/* AI player */
				/* random chance they go into heavy masturbation */
				// CHANGE THIS TO ACTIVATE ONLY IN THE LAST 4 TURNS
				var randomChance = getRandomNumber(HEAVY_LAST_ROUND, HEAVY_FIRST_ROUND);
				
				if (randomChance > players[i].timer-1) {
					/* this player is now heavily masturbating */
					players[i].forfeit = [PLAYER_HEAVY_MASTURBATING, CANNOT_SPEAK];
				}
			}
			masturbatingPlayers.push(i);
            if (players[i].forfeit[0] == PLAYER_HEAVY_MASTURBATING) {
                heavyMasturbatingPlayers.push(i);
            }
        }
    }

    if (heavyMasturbatingPlayers.length > 0) {
        masturbatingPlayers = heavyMasturbatingPlayers;
    }
    // Show a player masturbating while dealing or after the game, if there is one available
    if (masturbatingPlayers.length > 0
        && ((gamePhase == eGamePhase.DEAL && humanPlayer.out) || gamePhase == eGamePhase.EXCHANGE || gamePhase == eGamePhase.END_LOOP)) {
        var playerToShow = masturbatingPlayers[getRandomNumber(0, masturbatingPlayers.length)]
        var others_tags = [[players[playerToShow].gender == eGender.MALE ? MALE_MASTURBATING : FEMALE_MASTURBATING, OPPONENT_MASTURBATING]];
        if (players[playerToShow].forfeit[0] == PLAYER_HEAVY_MASTURBATING) {
            others_tags.unshift([
                players[playerToShow].gender == eGender.MALE ? MALE_HEAVY_MASTURBATING : FEMALE_HEAVY_MASTURBATING,
                OPPONENT_HEAVY_MASTURBATING
            ]);
        }
        
        updateAllBehaviours(
            playerToShow,
            players[playerToShow].forfeit[0],
            others_tags
        );
    } else if (gamePhase == eGamePhase.DEAL && (ANIM_TIME > 0 || GAME_DELAY > 0)) {
        updateAllBehaviours(null, null, DEALING_CARDS);
    }
	
	return false;
}

/************************************************************
 * A player has 'finished' masturbating.
 ************************************************************/
function finishMasturbation (player) {
	// HARD SET STAGE
	players[player].stage += 1;
	players[player].finished = true;
    players[player].forfeit = [[[PLAYER_AFTER_MASTURBATING], [PLAYER_FINISHED_MASTURBATING]], CAN_SPEAK];
	players[player].stageChangeUpdate();
    
	/* update player dialogue */
    updateAllBehaviours(
        player, 
        PLAYER_FINISHED_MASTURBATING,
        [[players[player].gender == eGender.MALE ? MALE_FINISHED_MASTURBATING : FEMALE_FINISHED_MASTURBATING,
         OPPONENT_FINISHED_MASTURBATING]]
    );
	players[player].timeInStage = 0;
    
	if (AUTO_FADE && globalSavedTableVisibility !== undefined) {
		forceTableVisibility(globalSavedTableVisibility);
		globalSavedTableVisibility = undefined;
	}
	allowProgression();
}
