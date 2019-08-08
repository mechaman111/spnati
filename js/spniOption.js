/********************************************************************************
 This file contains the variables and functions that form the options menu.
 ********************************************************************************/

/**********************************************************************
 *****                      Options Variables                     *****
 **********************************************************************/

$masturbationTimerBox = $("#player-masturbation-timer-box");
$masturbationWarningLabel = $("#masturbation-warning-label");

var localDayOrNight;
function setLocalDayOrNight () {
    var hour = new Date().getHours();
    localDayOrNight = (hour >= 7 && hour < 19) ? 'day' : 'night';            
}

/**********************************************************************
 *****                    Background metadata                     *****
 **********************************************************************/
var backgrounds = {
    'inventory': new Background('inventory', 'img/backgrounds/inventory.png', {
        name: "The Inventory",
        author: "Zeuses-Swan-Song",
        location: 'indoors'
    }),
	'tiki bar': new Background('tiki bar', 'img/backgrounds/tiki_bar.png', {
        name: "Tiki Bar",
        author: "Zeuses-Swan-Song",
        location: 'outdoors',
        time: 'day'
    }),
    'beach': new Background('beach', 'img/backgrounds/beach.png', {
        name: "Beach",
        author: "ANDRW & Patisdom",
        location: 'outdoors',
        time: 'day'
    }),
    'classroom': new Background('classroom', 'img/backgrounds/classroom.png', {
        name: "Classroom",
        author: "ANDRW",
        location: 'indoors'
    }),
    'roof': new Background('roof', 'img/backgrounds/roof.png', {
        name: "Roof",
        author: "ANDRW",
        location: 'outdoors',
        time: 'day'
    }),
    'poolside': new Background('poolside', 'img/backgrounds/poolside.png', {
        name: "Poolside",
        author: "ANDRW",
        location: 'outdoors',
        time: 'day'
    }),
    'hot spring': new Background('hot spring', 'img/backgrounds/hot_spring.png', {
        name: "Hot Spring",
        author: "Gon-san & Patisdom",
        location: 'outdoors',
        time: 'day'
    }),
    'mansion': new Background('mansion', 'img/backgrounds/mansion.png', {
        name: "Mansion",
        author: "Anymouse-68",
        location: 'indoors',
        time: 'night'
    }),
    'purple room': new Background('purple room', 'img/backgrounds/purple_room.png', {
        name: "Purple Room",
        author: "ANDRW",
        location: 'indoors'
    }),
    'street': new Background('street', 'img/backgrounds/street.png', {
        name: "Street",
        author: "DrankeyKrang",
        location: 'outdoors',
        time: 'night'
    }),
    'bedroom': new Background('bedroom', 'img/backgrounds/bedroom.png', {
        name: "Bedroom",
        author: "XKokone-chanX (bed) & throwaway927263 (room)",
        location: 'indoors',
        time: 'night'
    }),
    'locker room': new Background('locker room', 'img/backgrounds/locker_room.png', {
        name: "Locker Room",
        author: "UnderscorM3",
        location: 'indoors'
    }),
    'haunted forest': new Background('haunted forest','img/backgrounds/haunted_forest.png', {
        name: "Haunted Forest",
        author: "UnderscorM3",
        location: 'outdoors',
        time: 'night',
        filter: 'brightness(0.7) saturate(0.9)'
    }),
    'romantic': new Background('romantic', 'img/backgrounds/romantic.png', {
        name: "Romantic",
        author: "Patisdom",
        location: 'indoors'
    }),
    'classic': new Background('classic', 'img/backgrounds/classic.png', {
        name: "Classic",
        location: 'indoors'
    })
}

var defaultBackground = backgrounds['tiki bar'];

/* The currently displayed background */
var activeBackground = defaultBackground;

/* Background selected by the player in the options menu */
var optionsBackground = defaultBackground;

var useGroupBackgrounds = true;

/**
 * Constructor for game Background objects.
 * 
 * @constructor
 * @this {Background}
 * @param {string} id The internal ID for this background. 
 *  `~background~` evaluates to this value.
 * @param {string} src The path to the image for this background.
 * @param {Object} metadata Metadata to associate with this background, 
 *  such as `name`, `author`, `filter`, `tags`, etc.
 */
function Background (id, src, metadata) {
    this.id = id;
    this.src = src;

    /** 
     * @type {string}
     * The human-friendly name for this background.
     * Displayed in i.e. the Options menu.
     */
    this.name = metadata.name || id;

    /** 
     * @type {string}
     * The author(s) of this background.
     * Shown in the Options modal.
     * Should not include 'by:' or similar phrases.
     */
    this.author = metadata.author || '';

    /** 
     * @type {string[]} 
     * An array of string tags to associate with this background.
     */
    this.tags = metadata.tags || [];

    /** 
     * @type {string} 
     * A CSS `filter` value to apply to all screens whenever this
     * background is active.
     */
    this.filter = metadata.filter || '';

    /** 
     * @type {Object}
     * Contains the raw metadata passed to the Background constructor.
     */
    this.metadata = metadata;
}

/**
 * Sets this background to be displayed.
 */
Background.prototype.activateBackground = function () {
    /* backgroundImage is defined in spniCore */
    if (backgroundImage === undefined) {
        backgroundImage = new Image();
    }

    backgroundImage.src = this.src;
    backgroundImage.onload = function () {
        $("body").css("background-image", "url(" + this.src + ")");
        activeBackground = this;
        
        $('.screen').css('filter', this.filter || '');
        autoResizeFont();
    }.bind(this);
}

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
    setActiveOption('options-explain-hands', EXPLAIN_ALL_HANDS);
    setActiveOption('options-ai-turn-time', GAME_DELAY);
    setActiveOption('options-deal-speed', ANIM_TIME);
    setActiveOption('options-auto-forfeit', FORFEIT_DELAY);
    setActiveOption('options-auto-ending', ENDING_DELAY);
    setActiveOption('options-minimal-ui', MINIMAL_UI);
    $("#options-modal").modal('show');
}
$("#options-modal").on('shown.bs.modal', function() {
	$("#options-modal").find('li.active a').first().focus();
});

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

$('ul#options-explain-hands').on('click', 'a', function() {
    EXPLAIN_ALL_HANDS = $(this).attr('data-value') == "true";
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
 * Push a selection image for the given background onto the
 * background selection modal.
 ************************************************************/
function pushBackgroundOption (background) {
    var container = $('<div>', {
        "class": "background-option",
        "data-background": background.id,
        "css": {"background-image": "url(" + background.src + ")"},
        "click": function() {
            optionsBackground = background;
            optionsBackground.activateBackground();
            save.saveSettings();
            $('#game-settings-modal').modal('hide');
        }
    });

    /* Create title element: */
    $('<span>', {
        "class": "background-info background-title",
        "text": background.name
    }).appendTo(container);

    if (background.author) {
        /* Create author element: */
        $('<span>', {
            "class": "background-info background-author",
            "text": background.author
        }).appendTo(container);
    }
    
    $("#settings-background").append(container);
}

/************************************************************
 * Shows the background selection modal.
 ************************************************************/
function showGameSettingsModal () {
    Object.keys(backgrounds).forEach(function (id) {
        /* Push selection images for all backgrounds not already on the menu. */
        if ($('#settings-background .background-option[data-background="'+id+'"]').length === 0) {
            pushBackgroundOption(backgrounds[id]);
        }
    });

    $('#game-settings-modal').modal('show');
}

$('#game-settings-modal').on('shown.bs.modal', function() {
	console.log($('.modal:visible'));
});

/************************************************************
 * Loading the player masturbation timer.
 ************************************************************/
function loadMasturbationTimer () {
	$masturbationTimerBox.val(humanPlayer.stamina);
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
		humanPlayer.stamina = newTime;
	}
	$masturbationWarningLabel.css("visibility", isValidTimerValue ? "hidden" : "visible");
});
