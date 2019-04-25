/********************************************************************************
 This file contains the variables and functions that form the options menu.
 ********************************************************************************/

/**********************************************************************
 *****                      Options Variables                     *****
 **********************************************************************/

$masturbationTimerBox = $("#player-masturbation-timer-box");
$masturbationWarningLabel = $("#masturbation-warning-label");

/**********************************************************************
 *****                    Background metadata                     *****
 **********************************************************************/
var backgrounds = {
    'inventory': { location: 'indoors' },
    'beach': { location: 'outdoors' },
    'classroom': { location: 'indoors' },
    'brick': { location: 'indoors' },
    'night': { location: 'outdoors', filter: 'brightness(0.8)' },
    'roof': { location: 'outdoors' },
    'seasonal': { location: 'indoors' },
    'library': { location: 'indoors' },
    'bathhouse': { location: 'indoors' },
    'poolside': { location: 'outdoors' },
    'hot spring': { location: 'outdoors' },
    'mansion': { location: 'indoors' },
    'purple room': { location: 'indoors' },
    'showers': { location: 'indoors' },
    'street': { location: 'outdoors' },
    'green screen': { location: 'indoors' },
    'arcade': { location: 'indoors' },
    'club': { location: 'indoors' },
    'bedroom': { location: 'indoors' },
    'hall': { location: 'indoors' },
    'locker room': { location: 'indoors' },
    'haunted forest': { location: 'outdoors', filter: 'brightness(0.7) saturate(0.9)'},
    'romantic': { location: 'indoors' },
    'classic': { location: 'indoors' }
};
var defaultBackground = 'inventory';
var selectedBackground = defaultBackground;

/**********************************************************************
 *****                      Option Functions                      *****
 **********************************************************************/

function setActiveOption(optionGroupId, selected) {
    var lookFor = (selected == null ? undefined : selected.toString());
    $('#'+optionGroupId).find('a').each(function() {
        if ($(this).attr('data-value') === lookFor) {
            $(this).parent().addClass('active');
        } else {
            $(this).parent().removeClass('active');
        }
    });
}


// Handle changing of active option in one place.
$('#options-modal .pagination, #game-settings-modal ul.pagination').on('click', 'a', function() {
    $(this).parent().siblings().removeClass('active');
    $(this).parent().addClass('active');
});

/************************************************************
 * The player clicked the options button. Shows the options modal.
 ************************************************************/
function showOptionsModal () {
    loadMasturbationTimer();
    setActiveOption('options-auto-fade', AUTO_FADE);
    setActiveOption('options-card-suggest', CARD_SUGGEST);
    setActiveOption('options-ai-turn-time', GAME_DELAY);
    setActiveOption('options-deal-speed', ANIM_TIME);
    setActiveOption('options-auto-forfeit', FORFEIT_DELAY);
    setActiveOption('options-auto-ending', ENDING_DELAY);
    setActiveOption('options-minimal-ui', MINIMAL_UI);
    $("#options-modal").modal('show');
}

function setUIMode(minimal) {
    MINIMAL_UI = minimal;
    
    if (minimal) {
        $gameScreen.addClass('ui-minimal');
    } else {
        $gameScreen.removeClass('ui-minimal');
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

$('ul#options-auto-fade').on('click', 'a', function() {
    AUTO_FADE = $(this).attr('data-value') == "true";
});

$('ul#options-card-suggest').on('click', 'a', function() {
    CARD_SUGGEST = $(this).attr('data-value') == "true";
});

$('ul#options-ai-turn-time').on('click', 'a', function() {
    GAME_DELAY = Number($(this).attr('data-value'));
});

$('ul#options-deal-speed').on('click', 'a', function() {
    ANIM_TIME = Number($(this).attr('data-value'));
    ANIM_DELAY = 0.16 * ANIM_TIME;
});

$('ul#options-auto-forfeit').on('click', 'a', function() {
    FORFEIT_DELAY = Number($(this).attr('data-value')) || null;
});

$('ul#options-auto-ending').on('click', 'a', function() {
    ENDING_DELAY = Number($(this).attr('data-value')) || null;
});

$('ul#options-minimal-ui').on('click', 'a', function() {
    setUIMode($(this).attr('data-value') === 'true');
});

/************************************************************
 * The player clicked the options button. Shows the options modal.
 ************************************************************/
function showGameSettingsModal () {
    setActiveOption('settings-background', selectedBackground);
    $gameSettingsModal.modal('show');
}

$('ul#settings-background').on('click', 'a', function() {
    setBackground($(this).attr('data-value'));
});

/************************************************************
 * The player changed the background.
 ************************************************************/
function setBackground (choice) {
	/* implement the option change */
    if (!(choice in backgrounds)) {
        console.error("Invalid background", choice);
        return;
    }
    var filename = "img/backgrounds/"+choice.replace(/ /g, '_')+".png";
    if (backgroundImage === undefined) {
        backgroundImage = new Image();
    }
    backgroundImage.src = filename;
    backgroundImage.onload = function() {
        $("body").css("background-image", "url("+filename+")");
        selectedBackground = choice;
        $('.screen').css('filter', backgrounds[selectedBackground].filter || '');
        autoResizeFont();
    };
}

/************************************************************
 * Loading the player masturbation timer.
 ************************************************************/
function loadMasturbationTimer () {
	$masturbationTimerBox.val(players[HUMAN_PLAYER].stamina);
	$masturbationWarningLabel.css("visibility", "hidden");
}
 /************************************************************
 * The player changed their masturbation timer.
 ************************************************************/
$masturbationTimerBox.on('input', function() {
	var newTimerValue = $masturbationTimerBox.val();
	var newTime = Number(newTimerValue);
	var isValidTimerValue = (newTime != "NaN") && (newTime > 0);
	if (isValidTimerValue){
		players[HUMAN_PLAYER].stamina = newTime;
	}
	$masturbationWarningLabel.css("visibility", isValidTimerValue ? "hidden" : "visible");
});
