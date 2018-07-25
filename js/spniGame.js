/********************************************************************************
 This file contains the variables and functions that form the main game screen of
 the game. The main game progression (dealing, exchanging, revealing, stripping)
 and everything to do with displaying the main game screen.
 ********************************************************************************/

/**********************************************************************
 *****                    Game Screen UI Elements                 *****
 **********************************************************************/

/* game banner */
$gameBanner = $("#game-banner");

/* main UI elements */
$gameBubbles = [$("#game-bubble-1"),
                $("#game-bubble-2"),
                $("#game-bubble-3"),
                $("#game-bubble-4")];
$gameDialogues = [$("#game-dialogue-1"),
                  $("#game-dialogue-2"),
                  $("#game-dialogue-3"),
                  $("#game-dialogue-4")];
$gameAdvanceButtons = [$("#game-advance-button-1"),
                       $("#game-advance-button-2"),
                       $("#game-advance-button-3"),
                       $("#game-advance-button-4")];
$gameImages = [$("#game-image-1"),
               $("#game-image-2"),
               $("#game-image-3"),
               $("#game-image-4")];
$gameLabels = [$("#game-name-label-0"),
               $("#game-name-label-1"),
               $("#game-name-label-2"),
               $("#game-name-label-3"),
               $("#game-name-label-4")];
$gameOpponentAreas = [$("#game-opponent-area-1"),
                      $("#game-opponent-area-2"),
                      $("#game-opponent-area-3"),
                      $("#game-opponent-area-4")];
$gamePlayerCountdown = $("#player-countdown");
$gamePlayerClothingArea = $("#player-game-clothing-area");
$gamePlayerCardArea = $("#player-game-card-area");

/* dock UI elements */
$gameClothingLabel = $("#game-clothing-label");
$gameClothingCells = [$("#player-0-clothing-1"),
                      $("#player-0-clothing-2"),
                      $("#player-0-clothing-3"),
                      $("#player-0-clothing-4"),
                      $("#player-0-clothing-5"),
                      $("#player-0-clothing-6"),
                      $("#player-0-clothing-7"),
                      $("#player-0-clothing-8")];
$mainButton = $("#main-game-button");
$cardButtons = [$("#player-0-card-1"),
				$("#player-0-card-2"),
				$("#player-0-card-3"),
				$("#player-0-card-4"),
				$("#player-0-card-5")];
$debugButtons = [$("#debug-button-0"),
				 $("#debug-button-1"),
				 $("#debug-button-2"),
				 $("#debug-button-3"),
				 $("#debug-button-4")];

/* restart modal */
$restartModal = $("#restart-modal");

/**********************************************************************
 *****                   Game Screen Variables                    *****
 **********************************************************************/

/* pseudo constants */
var GAME_DELAY = 600;
var FORFEIT_DELAY = 7500;
var GAME_OVER_DELAY = 1000;
var SHOW_ENDING_DELAY = 5000; //5 seconds
var CARD_SUGGEST = false;
var AUTO_FORFEIT = false;
var AUTO_FADE = true;
var KEYBINDINGS_ENABLED = false;
var DEBUG = false;

/* colours */
var currentColour = "#63AAE7"; 	/* indicates current turn */
var clearColour = "#FFFFFF";	/* indicates neutral */
var loserColour = "#DD4444";	/* indicates loser of a round */

/* game state */
var currentTurn = 0;
var currentRound = -1;
var previousLoser = -1;
var recentLoser = -1;
var gameOver = false;
var actualMainButtonState = false;
var endWaitDisplay = 0;
var showDebug = false;
var chosenDebug = -1;
var autoForfeitTimeoutID; // Remember this specifically so that it can be cleared if auto forfeit is turned off.
                      
/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Loads all of the content required to display the title
 * screen.
 ************************************************************/
function loadGameScreen () {
    $gameScreen.show();

    /* reset all of the player's states */
    for (var i = 1; i < players.length; i++) {
        if (players[i]) {
            players[i].current = 0;
            $gameOpponentAreas[i-1].show();
            $gameImages[i-1].css('height', players[i].scale + '%');
            $gameLabels[i].css({"background-color" : clearColour});
            clearHand(i);
        }
        else {
            $gameOpponentAreas[i-1].hide();
            $gameBubbles[i-1].hide();
        }
    }
    $gameLabels[HUMAN_PLAYER].css({"background-color" : clearColour});
    clearHand(HUMAN_PLAYER);

    recentLoser = -1;
    currentRound = -1;
    gameOver = false;
    $gamePlayerCardArea.show();
    $gamePlayerCountdown.hide();
    chosenDebug = -1;
    updateDebugState(showDebug);

    updateAllBehaviours(null, GAME_START);
    
    /* set up the visuals */
    updateAllGameVisuals();

    /* set up the poker library */
    setupPoker();

    /* disable player cards */
    for (var i = 0; i < $cardButtons.length; i++) {
        $cardButtons[i].attr('disabled', true);
    }

    /* enable and set up the main button */
    $mainButton.html("Deal");
    $mainButton.attr("disabled", false);
    actualMainButtonState = false;

    /* late settings */
    KEYBINDINGS_ENABLED = true;
    document.addEventListener('keyup', game_keyUp, false);
}

/**********************************************************************
 *****                      Display Functions                     *****
 **********************************************************************/

/************************************************************
 * Updates all of the main visuals on the main game screen.
 ************************************************************/
function updateGameVisual (player) {
    /* update all opponents */
    if (players[player]) {
        if (players[player].chosenState) {
            var chosenState = players[player].chosenState;

            /* update dialogue */
            $gameDialogues[player-1].html(chosenState.dialogue);
            
            /* update image */
            $gameImages[player-1].attr('src', players[player].folder + chosenState.image);
			$gameImages[player-1].show()

            /* update label */
            $gameLabels[player].html(players[player].label.initCap());

            /* check silence */
            if (chosenState.silent) {
                $gameBubbles[player-1].hide();
            }
            else {
                $gameBubbles[player-1].show();
            }
        } else {
            /* hide their dialogue bubble */
            $gameDialogues[player-1].html("");
            $gameAdvanceButtons[player-1].css({opacity : 0});
            $gameBubbles[player-1].hide();
        }
    }
    else {
        $gameDialogues[player-1].html("");
        $gameAdvanceButtons[player-1].css({opacity : 0});
        $gameBubbles[player-1].hide();

		$gameImages[player-1].hide();
    }
}

/************************************************************
 * Updates all of the main visuals on the main game screen.
 ************************************************************/
function updateAllGameVisuals () {
    /* update all opponents */
    for (var i = 1; i < players.length; i++) {
        updateGameVisual (i);
    }
}

/************************************************************
 * Updates the visuals of the player clothing cells.
 ************************************************************/
function displayHumanPlayerClothing () {
    /* collect the images */
    var clothingImages = players[HUMAN_PLAYER].clothing.map(function(c) {
		return c.image;
	});
    
    /* display the remaining clothing items */
    clothingImages.reverse();
	$gameClothingLabel.html("Your Clothing");
	for (var i = 0; i < 8; i++) {
		if (clothingImages[i]) {
			$gameClothingCells[i].attr('src', clothingImages[i]);
			$gameClothingCells[i].css({opacity: 1});
		} else {
			$gameClothingCells[i].css({opacity: 0});
		}
	}
}

/**********************************************************************
 *****                        Flow Functions                      *****
 **********************************************************************/

/************************************************************
 * Determines what the AI's action will be.
 ************************************************************/
function makeAIDecision () {
	/* determine the AI's decision */
	determineAIAction(players[currentTurn]);
	
	/* dull the cards they are trading in */
	for (var i = 0; i < players[currentTurn].hand.tradeIns.length; i++) {
		if (players[currentTurn].hand.tradeIns[i]) {
			dullCard(currentTurn, i);
		}
	}

	/* update a few hardcoded visuals */
	updateBehaviour(currentTurn, SWAP_CARDS);
	updateGameVisual(currentTurn);

	/* wait and implement AI action */
	timeoutID = window.setTimeout(implementAIAction, GAME_DELAY);
}

/************************************************************
 * Implements the AI's chosen action.
 ************************************************************/
function implementAIAction () {
	exchangeCards(currentTurn);

	/* refresh the hand */
	hideHand(currentTurn);

	/* update behaviour */
	determineHand(players[currentTurn]);
	if (players[currentTurn].hand.strength == HIGH_CARD) {
		updateBehaviour(currentTurn, BAD_HAND);
	} else if (players[currentTurn].hand.strength == PAIR) {
		updateBehaviour(currentTurn, OKAY_HAND);
	} else {
		updateBehaviour(currentTurn, GOOD_HAND);
	}
	updateGameVisual(currentTurn);

	/* wait and then advance the turn */
	timeoutID = window.setTimeout(advanceTurn, GAME_DELAY);
}

/************************************************************
 * Advances the turn or ends the round.
 ************************************************************/
function advanceTurn () {
	currentTurn++;
	if (currentTurn >= players.length) {
		currentTurn = 0;
	}

    if (players[currentTurn]) {
        /* highlight the player who's turn it is */
        for (var i = 0; i < players.length; i++) {
            if (currentTurn == i) {
                $gameLabels[i].css({"background-color" : currentColour});
            } else {
                $gameLabels[i].css({"background-color" : clearColour});
            }
        }

        /* check to see if they are still in the game */
        if (players[currentTurn].out && currentTurn > 0) {
            /* update their speech and skip their turn */
            updateBehaviour(currentTurn, players[currentTurn].forfeit[0]);
            updateGameVisual(currentTurn);

            timeoutID = window.setTimeout(advanceTurn, GAME_DELAY);
            return;
        }
    }

	/* allow them to take their turn */
	if (currentTurn == 0) {
        /* human player's turn */
        if (players[HUMAN_PLAYER].out) {
            $mainButton.html("Reveal");
		}
		allowProgression();
	} else if (!players[currentTurn]) {
        /* There is no player here, move on. */
        advanceTurn();
    } else {
        /* AI player's turn */
		makeAIDecision();
	}
}

/************************************************************
 * Deals cards to each player and resets all of the relevant
 * information.
 ************************************************************/
function startDealPhase () {
    currentRound++;
    /* dealing cards */
	dealLock = 0;
    for (var i = 0; i < players.length; i++) {
        if (players[i]) {
            /* collect the player's hand */
            collectPlayerHand(i);
        }
    }
    shuffleDeck();
    for (var i = 0; i < players.length; i++) {
        if (players[i]) {
            console.log(players[i] + " "+ i);
            if (!players[i].out) {
                /* deal out a new hand to this player */
                dealHand(i);
            } else {
                if (HUMAN_PLAYER == i) {
                    $gamePlayerCardArea.hide();
                    $gamePlayerClothingArea.hide();
                }
                else {
                    $gameOpponentAreas[i-1].hide();
                }
            }
            players[i].timeInStage++;
        }
    }

	/* IMPLEMENT STACKING/RANDOMIZED TRIGGERS HERE SO THAT AIs CAN COMMENT ON PLAYER "ACTIONS" */

	/* clear the labels */
	for (var i = 0; i < players.length; i++) {
		$gameLabels[i].css({"background-color" : clearColour});
	}

	timeoutID = window.setTimeout(checkDealLock, (ANIM_DELAY*(players.length))+ANIM_TIME);
}

/************************************************************
 * Checks the deal lock to see if the animation is finished.
 ************************************************************/
function checkDealLock () {
	/* count the players still in the game */
	var inGame = 0;
	for (var i = 0; i < players.length; i++) {
		if (players[i] && !players[i].out) {
			inGame++;
		}
	}

	/* check the deal lock */
	if (dealLock < inGame * 5) {
		timeoutID = window.setTimeout(checkDealLock, 100);
	} else {
        /* Set up main button.  If there is not pause for the human
		   player to exchange cards, and someone is masturbating, and
		   the card animation speed is to great, we need a pause so
		   that the masturbation talk can be read. */
        if (players[HUMAN_PLAYER].out && getNumPlayersInStage(STAGE_MASTURBATING) > 0 && ANIM_DELAY < 350) { 
            $mainButton.html("Next");
            allowProgression();
        } else {
            continueDealPhase();
        }
    }
}

/************************************************************
 * Finishes the deal phase and allows the game to progress.
 ************************************************************/
function continueDealPhase () {
	/* hide the dialogue bubbles */
    for (var i = 1; i < players.length; i++) {
        $gameDialogues[i-1].html("");
        $gameAdvanceButtons[i-1].css({opacity : 0});
        $gameBubbles[i-1].hide();
    }

	/* set visual state */
    if (!players[HUMAN_PLAYER].out) {
        showHand(HUMAN_PLAYER);
    }

    /* enable player cards */
    for (var i = 0; i < $cardButtons.length; i++) {
       $cardButtons[i].attr('disabled', false);
    }

	/* suggest cards to swap, if enabled */
	if (CARD_SUGGEST && !players[HUMAN_PLAYER].out) {
		determineAIAction(players[HUMAN_PLAYER]);
		
		/* dull the cards they are trading in */
		for (var i = 0; i < players[HUMAN_PLAYER].hand.tradeIns.length; i++) {
			if (players[HUMAN_PLAYER].hand.tradeIns[i]) {
				dullCard(HUMAN_PLAYER, i);
			}
		}
	}

    /* allow each of the AIs to take their turns */
    currentTurn = 0;
    advanceTurn();
}

/************************************************************
 * Processes everything required to complete the exchange phase
 * of a round. Trades in the cards the player has selected and
 * draws new ones.
 ************************************************************/
function completeExchangePhase () {
    /* exchange the player's chosen cards */
    exchangeCards(HUMAN_PLAYER);
    showHand(HUMAN_PLAYER);

    /* disable player cards */
    for (var i = 0; i < $cardButtons.length; i++) {
       $cardButtons[i].attr('disabled', true);
    }
    allowProgression();
}

/************************************************************
 * Processes everything required to complete the reveal phase
 * of a round. Shows everyone's hand and determines who lost
 * the hand.
 ************************************************************/
function completeRevealPhase () {
    /* reveal everyone's hand */
    for (var i = 0; i < players.length; i++) {
        if (players[i] && !players[i].out) {
            determineHand(players[i]);
            showHand(i);
        }
    }

    /* figure out who has the lowest hand */
    previousLoser = recentLoser;
    recentLoser = determineLowestHand();

    if (chosenDebug !== -1 && DEBUG) {
        recentLoser = chosenDebug;
    }

    console.log("Player "+recentLoser+" is the loser.");

    /* look for the unlikely case of an absolute tie */
    if (recentLoser == -1) {
        console.log("Fuck... there was an absolute tie");
        /* inform the player */

        /* hide the dialogue bubbles */
        for (var i = 1; i < players.length; i++) {
            $gameDialogues[i-1].html("");
            $gameAdvanceButtons[i-1].css({opacity : 0});
            $gameBubbles[i-1].hide();
        }

        /* reset the round */
        $mainButton.html("Deal");
        allowProgression();
        return;
    }

    // update loss history
    if (previousLoser < 0) {
        // first loser
        players[recentLoser].consecutiveLosses = 1;
    }
    else if (previousLoser === recentLoser) {
        // same player lost again
        players[recentLoser].consecutiveLosses++;
    }
    else {
        // a different player lost
        players[previousLoser].consecutiveLosses = 0; //reset last loser
        players[recentLoser].consecutiveLosses = 1;
    }


    /* update behaviour */
	var clothes = playerMustStrip (recentLoser);
    updateAllGameVisuals();

    /* highlight the loser */
    for (var i = 0; i < players.length; i++) {
        if (recentLoser == i) {
            $gameLabels[i].css({"background-color" : loserColour});
        } else {
            $gameLabels[i].css({"background-color" : clearColour});
        }
    }

    /* set up the main button */
	if (recentLoser != HUMAN_PLAYER && clothes > 0) {
	    $mainButton.html("Continue");
	} else if (clothes == 0) {
	    $mainButton.html("Masturbate");
	} else {
		$mainButton.html("Strip");
	}
    allowProgression();
}

/************************************************************
 * Processes everything required to complete the continue phase
 * of a round. A very short phase in which a player removes an
 * article of clothing.
 ************************************************************/
function completeContinuePhase () {
	/* show the player removing an article of clothing */
	prepareToStripPlayer(recentLoser);
    updateAllGameVisuals();
    if (players[recentLoser].clothing.length > 0) {
	    $mainButton.html("Strip");
    } else {
	    $mainButton.html("Masturbate");
    }
    allowProgression();
}

/************************************************************
 * Processes everything required to complete the strip phase
 * of a round. Makes the losing player strip.
 ************************************************************/
function completeStripPhase () {
    /* strip the player with the lowest hand */
    stripPlayer(recentLoser);
}

/************************************************************
 * Processes everything required to complete the strip phase of a
 * round when the loser has no clothes left. Makes the losing player
 * start their forfeit. May also end the game if only one player
 * remains.
 ************************************************************/
function completeMasturbatePhase () {
    /* strip the player with the lowest hand */
    startMasturbation(recentLoser);
    updateAllGameVisuals();
}

/************************************************************
 * Handles everything that happens at the end of the round.
 * Including the checks for the end of game.
 ************************************************************/
function endRound () {
	/* check to see how many players are still in the game */
    var inGame = 0;
    var lastPlayer = 0;
    for (var i = 0; i < players.length; i++) {
        if (players[i] && !players[i].out) {
            inGame++;
            lastPlayer = i;
        }
    }

    /* if there is only one player left, end the game */
    if (inGame <= 1) {
		console.log("The game has ended!");
		$gameBanner.html("Game Over! "+players[lastPlayer].label+" won Strip Poker Night at the Inventory!");
		gameOver = true;

        for (var i = 0; i < players.length; i++) {
            if (HUMAN_PLAYER == i) {
                $gamePlayerCardArea.hide();
                $gamePlayerClothingArea.hide();
            }
            else {
                $gameOpponentAreas[i-1].hide();
            }
        }
        endWaitDisplay = 0;
		handleGameOver();
	} else {
		$mainButton.html("Deal");
		allowProgression();
	}
}

/************************************************************
 * Handles the end of the game. Currently just waits for all
 * players to finish their forfeits.
 ************************************************************/
function handleGameOver() {
	/* determine how many timers are left */
	var left = 0;
	for (var i = 0; i < timers.length; i++) {
		if (players[i] && timers[i] > 0) {
			left++;
		}
	}

	/* determine true end */
	if (left == 0) {
		/* true end */

		//identify winner
		var winner = -1;
		for (var i = 0; i < players.length; i++){
			if (players[i] && !players[i].out){
				winner = i;
				break;
			}
		}
		for (var i = 1; i < players.length; i++){
			var tag = (i == winner) ? GAME_OVER_VICTORY : GAME_OVER_DEFEAT;
			updateBehaviour(i, tag, players[winner]);
		}

        updateAllGameVisuals();

		$mainButton.html("Ending?");
		$mainButton.attr('disabled', false);
        actualMainButtonState = false;
		//window.setTimeout(doEpilogueModal, SHOW_ENDING_DELAY); //start the endings
	} else {
        var dots = "";
        for (var i = 0; i < endWaitDisplay; i++) {
            dots += ".";
        }
        endWaitDisplay = (endWaitDisplay + 1) % 4;
        
		/* someone is still forfeiting */
		$mainButton.html("Wait" + dots);
		allowProgression();
	}
}

/**********************************************************************
 *****                    Interaction Functions                   *****
 **********************************************************************/

/************************************************************
 * The player selected one of their cards.
 ************************************************************/
function selectCard (card) {
	players[HUMAN_PLAYER].hand.tradeIns[card] = !players[HUMAN_PLAYER].hand.tradeIns[card];
	
	if (players[HUMAN_PLAYER].hand.tradeIns[card]) {
		dullCard(HUMAN_PLAYER, card);
	} else {
		fillCard(HUMAN_PLAYER, card);
	}
}

/************************************************************
 * Allow progression by enabling the main button *or*
 * setting up the auto forfeit timer.
 ************************************************************/
function allowProgression () {
    if (players[HUMAN_PLAYER].out && AUTO_FORFEIT) {
        timeoutID = setTimeout(advanceGame, FORFEIT_DELAY);
    } else {
        $mainButton.attr('disabled', false);
        actualMainButtonState = false;
    }
}

/************************************************************
 * The player clicked the main button on the game screen.
 ************************************************************/
function advanceGame () {
    var context = $mainButton.html();

    /* disable the button to prevent double clicking */
    $mainButton.attr('disabled', true);
    actualMainButtonState = true;
    autoForfeitTimeoutID = undefined;
    
    /* lower the timers of everyone who is forfeiting */
    if (tickForfeitTimers(context)) return;

    /* handle the game */
    if (context == "Deal") {
        /* dealing the cards */
        if (AUTO_FADE) forceTableVisibility(true);
        $mainButton.html("Exchange");
        startDealPhase();
    } else if (context == "Next") {
        /* advance to next round if human player is masturbating */
        $mainButton.html("Exchange");
        continueDealPhase();
    } else if (context == "Exchange") {
        /* exchanging cards */
        if (AUTO_FADE) forceTableVisibility(true);
        $mainButton.html("Reveal");
        completeExchangePhase();
    } else if (context == "Reveal") {
        /* revealing cards */
        if (AUTO_FADE) forceTableVisibility(true);
        completeRevealPhase();
    } else if (context == "Continue") {
		/* waiting for the loser to strip */
        if (AUTO_FADE) forceTableVisibility(false);
        completeContinuePhase();
	} else if (context == "Strip") {
        /* stripping the loser */
        if (AUTO_FADE) forceTableVisibility(false);
        completeStripPhase();
    } else if (context == "Masturbate") {
        /* making the loser start masturbating */
        if (AUTO_FADE) forceTableVisibility(false);
		completeMasturbatePhase();
    } else if (context == "Continue...") {
        /* finished watching masturbation */
		    endMasturbation();
    } else if (context.substr(0, 4) == "Wait") {
		/* waiting for someone to finish */
		handleGameOver(); //No delay here
	} else if (context == "Ending?") {
        doEpilogueModal(); //start the endings
        actualMainButtonState = false;
    } else {
        if (AUTO_FADE) forceTableVisibility(true);
		console.log("Invalid main button state: "+context);
    }
}

/************************************************************
 * The player clicked the home button. Shows the restart modal.
 ************************************************************/
function showRestartModal () {
    $restartModal.modal('show');
}

function closeRestartModal() {
	$mainButton.attr('disabled', actualMainButtonState);
}

/************************************************************
 * A keybound handler.
 ************************************************************/
function game_keyUp(e)
{
    console.log(e);
    if (KEYBINDINGS_ENABLED) {
        if (e.keyCode == 32 && !$mainButton.prop('disabled')) { // Space
            advanceGame();
        }
        else if (e.keyCode == 49 && !$cardButtons[4].prop('disabled')) { // 1
            selectCard(4);
        }
        else if (e.keyCode == 50 && !$cardButtons[3].prop('disabled')) { // 2
            selectCard(3);
        }
        else if (e.keyCode == 51 && !$cardButtons[2].prop('disabled')) { // 3
            selectCard(2);
        }
        else if (e.keyCode == 52 && !$cardButtons[1].prop('disabled')) { // 4
            selectCard(1);
        }
        else if (e.keyCode == 53 && !$cardButtons[0].prop('disabled')) { // 5
            selectCard(0);
        }
        else if (e.keyCode == 81 && DEBUG) {
            showDebug = !showDebug;
            updateDebugState(showDebug);
        }
        else if (e.keyCode == 84) {
            toggleTableVisibility();
        }
    }
}


function selectDebug(player)
{
    if (chosenDebug === player) {
        chosenDebug = -1;
    }
    else {
        chosenDebug = player;
    }
    updateDebugState(showDebug);
}


function updateDebugState(show)
{
    if (!show) {
        for (var i = 0; i < $debugButtons.length; i++) {
            $debugButtons[i].hide();
        }
    }
    else {
        for (var i = 0; i < $debugButtons.length; i++) {
            if (players[i] && !players[i].out) {
                $debugButtons[i].show();
                $debugButtons[i].removeClass("active");
            }
        }

        if (chosenDebug !== -1) {
            $debugButtons[chosenDebug].addClass("active");
        }
    }
}
