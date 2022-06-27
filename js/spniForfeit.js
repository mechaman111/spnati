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

 /**
  * Update this player's heavy masturbation status and return it.
  *
  * This method will set the player to heavy masturbation if the appropriate
  * prerequisites are met: for AI players, this depends on random chance;
  * for human players, this checks the timer value against a constant value.
  *
  * This does nothing for players that aren't masturbating (i.e. still in the game
  * or already finished) and players whose forfeit statuses are locked by dialogue operations.
  *
  * In all cases, we return whether or not the player is heavily masturbating.
  *
  * @returns {boolean}
  */
Player.prototype.updateHeavyMasturbation = function () {
    if (this.finished || !this.out) return false;

    if (this.slot == HUMAN_PLAYER) {
        /* Human player: go into heavy masturbation at 5 ticks left. */
        this.forfeit[0] = (this.timer <= 4) ? PLAYER_HEAVY_MASTURBATING : PLAYER_MASTURBATING;
    } else if (!this.forfeitLocked) {
        /* AI player: roll random chance they go into heavy masturbation. */
        this.forfeit = (
            (this.timer <= getRandomNumber(HEAVY_LAST_ROUND, HEAVY_FIRST_ROUND)) ?
            [PLAYER_HEAVY_MASTURBATING, CANNOT_SPEAK] :
            [PLAYER_MASTURBATING, CAN_SPEAK]
        );
    }

    /* Players with locked forfeit status fall through with no changes. */

    return this.forfeit[0] == PLAYER_HEAVY_MASTURBATING;
}

/************************************************************
 * Initiate masturbation for the selected player.
 * In the future, we might want to make this a method of Player.
 ************************************************************/
function startMasturbation (player) {
    players[player].forfeit = [PLAYER_MASTURBATING, CAN_SPEAK];
    players[player].forfeitLocked = false;
    players[player].finishingTarget = players[player];
    players[player].out = true;
    players[player].hand = null;
    players[player].outOrder = players.countTrue(function(p) { return p.out; });

    if (chosenDebug === player) {
        chosenDebug = -1;
        updateDebugState(showDebug);
    }

    players[player].ticksInStage = 0;

    /* Set timer before playing dialogue, so that forfeitTimer conditions work properly. */
    players[player].timer = players[player].stamina;

    /* update behaviour */
    updateAllBehaviours(
        player, 
        PLAYER_START_MASTURBATING,
        [[players[player].gender == eGender.MALE ? MALE_START_MASTURBATING : FEMALE_START_MASTURBATING,
         OPPONENT_START_MASTURBATING]]
    );

    players[player].stage += 1;
    players[player].timeInStage = -1;
    players[player].stageChangeUpdate();
    
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
            players[i].ticksInStage++;
            /* set the button state */
            $mainButton.html("Cumming...");

            saveTranscriptMessage('<b>' + players[i].label.escapeHTML() + '</b> is finishing...');
            console.log(players[i].label+" is finishing!");
            if (i == HUMAN_PLAYER) {
                /* Hide everyone's dialogue bubbles. */
                gameDisplays.forEach(function (d) {
                    d.hideBubble();
                });

                /* player's timer is up */
                /* TEMP FIX: prevent this animation on Safari */
                if (PLAYER_FINISHING_EFFECT) {
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
                $gamePlayerCountdown.html('');
                $gameClothingLabel.html("<b>You're 'Finished'</b>");

            } else {
                /* let the player speak again */
                players[i].forfeit = [PLAYER_FINISHING_MASTURBATING, CAN_SPEAK];

                /* show them cumming */
                let finishTarget = players[i].finishingTarget;
                if (finishTarget && finishTarget.slot !== i) {
                    /* If the player has redirected their finishing dialogue to another character,
                     * play Opponent Finishing dialogue for them.
                     */

                    /* Hide everyone else's dialogue bubbles... */
                    gameDisplays.forEach(function (d) {
                        if (d.slot != finishTarget.slot) d.hideBubble();
                    });

                    finishTarget.singleBehaviourUpdate(OPPONENT_FINISHING_MASTURBATING, players[i]);
                } else {
                    gameDisplays.forEach(function (d) {
                        if (d.slot != i) d.hideBubble();
                    });

                    players[i].singleBehaviourUpdate(PLAYER_FINISHING_MASTURBATING);
                }

                /* Make sure the character's timer is _actually_ zero, in case they try to do something like
                 * reset their forfeit timer as part of a Finishing line.
                 * Question: do we actually want to disallow this?
                 */
                players[i].timer = 0;

                /* trigger the callback */
                var player = i, tableVisible = (tableOpacity > 0);
                timeoutID = window.setTimeout(function(){ allowProgression(eGamePhase.END_FORFEIT); }, ORGASM_DELAY);
                globalSavedTableVisibility = tableVisible;
                if (AUTO_FADE) forceTableVisibility(false);
            }
            return true;
        }
    }

    /* Always update ticksInStage for all players, even if they're not out. */
    players.forEach(function (p) {
        p.ticksInStage++;
    });

    for (var i = 0; i < players.length; i++) {
        if (players[i] && players[i].out && players[i].timer > 1) {
            players[i].timer--;
            masturbatingPlayers.push(i);

            let inHeavyMasturbation = players[i].updateHeavyMasturbation();
            if (inHeavyMasturbation) heavyMasturbatingPlayers.push(i);

            if (i == HUMAN_PLAYER) {
                /* human player */
                /* update the player label */
                $gameClothingLabel.html("<b>'Finished' in "+players[i].timer+" phases</b>");
                $gamePlayerCountdown.html(players[i].timer);
                if (inHeavyMasturbation) $gamePlayerCountdown.addClass('pulse');
                masturbatingPlayers.push(i); // Double the chance of commenting on human player
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
    players[player].ticksInStage = 0;
    players[player].timeInStage = 0;
    
    if (AUTO_FADE && globalSavedTableVisibility !== undefined) {
        forceTableVisibility(globalSavedTableVisibility);
        globalSavedTableVisibility = undefined;
    }
    allowProgression();
}
