/********************************************************************************
 This file contains the variables and functions that form the options menu.
 ********************************************************************************/

/**********************************************************************
 *****                      Options Variables                     *****
 **********************************************************************/

$optionsModal = $("#options-modal");
$tableStyleOptions = [$("#options-table-style-1"), $("#options-table-style-2"), $("#options-table-style-3")];
$autoFadeOptions = [$("#options-auto-fade-1"), $("#options-auto-fade-2")];
$cardSuggestOptions = [$("#options-card-suggest-1"), $("#options-card-suggest-2")];
$AITurnTimeOptions = [$("#options-ai-turn-time-1"), $("#options-ai-turn-time-2"), $("#options-ai-turn-time-3"), $("#options-ai-turn-time-4"), $("#options-ai-turn-time-5")];
$dealSpeedOptions = [$("#options-deal-speed-1"), $("#options-deal-speed-2"), $("#options-deal-speed-3"), $("#options-deal-speed-4")];
$autoForfeitOptions = [$("#options-auto-forfeit-1"), $("#options-auto-forfeit-2"), $("#options-auto-forfeit-3"), $("#options-auto-forfeit-4")];
$autoEndingOptions = [$("#options-auto-ending-1"), $("#options-auto-ending-2"), $("#options-auto-ending-3"), $("#options-auto-ending-4")];

$masturbationTimerBox = $("#player-masturbation-timer-box");
$masturbationWarningLabel = $("#masturbation-warning-label");

/**********************************************************************
 *****                    Background metadata                     *****
 **********************************************************************/
var backgrounds = [
	{ name: 'inventory', location: 'indoors' },
	{ name: 'beach', location: 'outdoors' },
	{ name: 'classroom', location: 'indoors' },
	{ name: 'brick', location: 'indoors' },
	{ name: 'night', location: 'outdoors', filter: 'brightness(0.8)' },
	{ name: 'roof', location: 'outdoors' },
	{ name: 'seasonal', location: 'indoors' },
	{ name: 'library', location: 'indoors' },
	{ name: 'bathhouse', location: 'indoors' },
	{ name: 'poolside', location: 'outdoors' },
	{ name: 'hot spring', location: 'outdoors' },
	{ name: 'mansion', location: 'indoors' },
	{ name: 'purple room', location: 'indoors' },
	{ name: 'showers', location: 'indoors' },
	{ name: 'street', location: 'outdoors' },
	{ name: 'green screen', location: 'indoors' },
	{ name: 'arcade', location: 'indoors' },
	{ name: 'club', location: 'indoors' },
	{ name: 'bedroom', location: 'indoors' },
	{ name: 'hall', location: 'indoors' },
	{ name: 'locker room', location: 'indoors' },
	{ name: 'haunted forest', location: 'outdoors', filter: 'brightness(0.7) saturate(0.9)'},
	{ name: 'romantic', location: 'indoors' },
	{ name: 'classic', location: 'indoors' }
];
var defaultBackground = 1;
var selectedBackground = defaultBackground-1;

/**********************************************************************
 *****                      Option Functions                      *****
 **********************************************************************/

/************************************************************
 * The player clicked the options button. Shows the options modal.
 ************************************************************/
function showOptionsModal () {
    $optionsModal.modal('show');
}

/************************************************************
 * Displays the active option correctly.
 ************************************************************/
function setActiveOption (options, choice) {
	/* make all inactive */
	for (var i = 0; i < options.length; i++) {
		options[i].removeClass("active");
	}

	/* set the right active option */
	options[choice-1].addClass("active");
}

/************************************************************
 * The player changed the table style.
 ************************************************************/
function setTableStyle (choice) {
	/* get the tables */
	$tables = $('.game-table');
	$surfaces = $('.game-table-surface');
	$areas = $('.opponent-area');
	$player = $('.player-table-area');

	/* implement the option change */
	switch (choice) {
		case 1: $tables.removeClass();
				$tables.addClass('bordered game-table');
				$surfaces.removeClass();
				$surfaces.addClass('bordered game-table-surface');
				$areas.removeClass();
				$areas.addClass('bordered opponent-area');
				$player.removeClass();
				$player.addClass('bordered player-table-area');
				break;
		case 2: $tables.removeClass();
				$tables.addClass('bordered game-table game-table-glass');
				$surfaces.removeClass();
				$surfaces.addClass('bordered game-table-surface game-table-surface-glass');
				$areas.removeClass();
				$areas.addClass('bordered opponent-area opponent-area-glass');
				$player.removeClass();
				$player.addClass('bordered player-table-area player-table-area-glass');
				break;
        case 3: $tables.removeClass();
				$tables.addClass('bordered game-table game-table-none');
				$surfaces.removeClass();
				$surfaces.addClass('bordered game-table-surface game-table-surface-none');
				$areas.removeClass();
				$areas.addClass('bordered opponent-area opponent-area-none');
				$player.removeClass();
				$player.addClass('bordered player-table-area player-table-area-none');
				break;
		default: $tables.removeClass();
				 $tables.addClass('bordered game-table');
				 $surfaces.removeClass();
				 $surfaces.addClass('bordered game-table-surface');
				 $areas.removeClass();
				 $areas.addClass('bordered opponent-area');
	}
	setActiveOption($tableStyleOptions, choice);
}


/************************************************************
 * The player changed fade option.
 ************************************************************/
function setAutoFade (choice) {
	/* implement the option change */
	switch (choice) {
		case 1: AUTO_FADE = true; break;
		case 2: AUTO_FADE = false; break;
		default: AUTO_FADE = true;
	}
	setActiveOption($autoFadeOptions, choice);
}


/************************************************************
 * The player changed card suggestion.
 ************************************************************/
function setCardSuggest (choice) {
	/* implement the option change */
	switch (choice) {
		case 1: CARD_SUGGEST = true; break;
		case 2: CARD_SUGGEST = false; break;
		default: CARD_SUGGEST = false;
	}
	setActiveOption($cardSuggestOptions, choice);
}

/************************************************************
 * The player changed the AI turn time.
 ************************************************************/
function setAITurnTime (choice) {
	/* implement the option change */
	switch (choice) {
		case 1: GAME_DELAY = 0; break;
		case 2: GAME_DELAY = 300; break;
		case 3: GAME_DELAY = 600; break;
		case 4: GAME_DELAY = 800; break;
		case 5: GAME_DELAY = 1200; break;
		default: GAME_DELAY = 600;
	}
	setActiveOption($AITurnTimeOptions, choice);
}

/************************************************************
 * The player changed the card animation speed.
 ************************************************************/
function setDealSpeed (choice) {
	/* implement the option change */
	switch (choice) {
		case 1: ANIM_DELAY = 0;
				ANIM_TIME = 0;
				break;
		case 2: ANIM_DELAY = 150;
				ANIM_TIME = 500;
				break;
		case 3: ANIM_DELAY = 350;
				ANIM_TIME = 1000;
				break;
		case 4: ANIM_DELAY = 800;
				ANIM_TIME = 2000;
				break;
		default: ANIM_DELAY = 350;
				 ANIM_TIME = 1000;
				 break;
	}
	setActiveOption($dealSpeedOptions, choice);
}

function setAutoForfeit (choice) {
	switch (choice) {
		case 4: AUTO_FORFEIT = false;
				break;
		default: AUTO_FORFEIT = true;
				 break;
	}
	setActiveOption($autoForfeitOptions, choice);

    switch (choice) {
		case 1: FORFEIT_DELAY = 4000;
				break;
		case 2: FORFEIT_DELAY = 7500;
				break;
		case 3: FORFEIT_DELAY = 10000;
				break;
		default: FORFEIT_DELAY = 7500;
				 break;
	}
}

function setAutoEnding (choice) {
	switch (choice) {
		case 4: AUTO_ENDING = false;
				break;
		default: AUTO_ENDING = true;
				 break;
	}
	setActiveOption($autoEndingOptions, choice);

    switch (choice) {
		case 1: ENDING_DELAY = 4000;
				break;
		case 2: ENDING_DELAY = 7500;
				break;
		case 3: ENDING_DELAY = 10000;
				break;
		default: ENDING_DELAY = 7500;
				 break;
	}
}

$("#options-modal").on("hidden.bs.modal", function () {
	if (autoForfeitTimeoutID) {
		/* If we're waiting specifically for the auto forfeit timer,
		   cancel it and restart it or enable the button. */
		clearTimeout(autoForfeitTimeoutID);
		allowProgression();
	} else if (!actualMainButtonState) {
		/* Start auto advance if enabled in pertinent state. */
        $mainButton.attr('disabled', (actualMainButtonState = true));
		allowProgression();
	}
});


$backgroundSettings = [];
for (let i=1;i<=24;i++) {
	$backgroundSettings.push($("#settings-background-"+(i.toString())));
}

/************************************************************
 * The player clicked the options button. Shows the options modal.
 ************************************************************/
function showGameSettingsModal () {
	loadMasturbationTimer(); //set data values
    $gameSettingsModal.modal('show');
}


/************************************************************
 * The player changed the background.
 ************************************************************/
function setBackground (choice) {
	/* implement the option change */
	backgroundImage = new Image();
	backgroundImage.src = "img/background"+choice+".png";
	backgroundImage.onload = autoResizeFont;
    $("body").css("background-image", "url(img/background"+choice+".png)");
	setActiveOption($backgroundSettings, choice);
	selectedBackground = choice - 1;
	$('.screen').css('filter', backgrounds[selectedBackground].filter || '');
}

/************************************************************
 * Loading the player masturbation timer.
 ************************************************************/
function loadMasturbationTimer () {
	$masturbationTimerBox.val(players[HUMAN_PLAYER].timer);
	$masturbationWarningLabel.css("visibility", "hidden");
}
 /************************************************************
 * The player changed their masturbation timer.
 ************************************************************/

function changeMasturbationTimer () {
	var newTimerValue = $masturbationTimerBox.val();
	var newTime = Number(newTimerValue);
	var isValidTimerValue = (newTime != "NaN") && (newTime > 0);
	if (isValidTimerValue){
		players[HUMAN_PLAYER].timer = newTime;
	}
	$masturbationWarningLabel.css("visibility", isValidTimerValue ? "hidden" : "visible");
}
