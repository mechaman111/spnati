/********************************************************************************
 This file contains the variables and functions that form the title and setup screens
 of the game. The parsing functions for the player.xml file, the clothing organization
 functions, and human player initialization.
 ********************************************************************************/

/**********************************************************************
 *****                   Title Screen UI Elements                 *****
 **********************************************************************/

$titlePanels = [$("#title-panel-1"), $("#title-panel-2")];
$nameField = $("#player-name-field");
$titleContainer = $('.main-title-container');
$sizeBlocks = $('.title-size-block');
$clothingTable = $("#title-clothing-table");
$warningLabel = $("#title-warning-label");
$titleCandy = [$("#left-title-candy"), $("#right-title-candy")];


/**********************************************************************
 *****                    Title Screen Variables                  *****
 **********************************************************************/

var CANDY_LIST = [
    "reskins/d.va_black_cat/0-calm.png",
    "reskins/d.va_black_cat/0-excited.png",
    "reskins/d.va_black_cat/0-peace.png",
    "reskins/d.va_black_cat/0-heart.png",

    "reskins/ghost_bride_weiss_schnee/0-calm.png",
    "reskins/ghost_bride_weiss_schnee/0-grinning.png",
    "reskins/ghost_bride_weiss_schnee/0-interested.png",
    "reskins/ghost_bride_weiss_schnee/0-start.png",

    "reskins/larachel_harvest_princess/0-calm.png",
    "reskins/larachel_harvest_princess/0-happy.png",
    "reskins/larachel_harvest_princess/0-interested.png",
    "reskins/larachel_harvest_princess/0-smug.png",

    "reskins/misato_catrina/0-Calm.png",
    "reskins/misato_catrina/0-Confident.png",
    "reskins/misato_catrina/0-Interested.png",
    "reskins/misato_catrina/0-Smug.png",

    "reskins/normal_girl_tharja/0-calm.png",
    "reskins/normal_girl_tharja/0-cocky.png",
    "reskins/normal_girl_tharja/0-excited.png",
    "reskins/normal_girl_tharja/0-normal.png",

    "reskins/shadow_emi/0-calm.png",
    "reskins/shadow_emi/0-content.png",
    "reskins/shadow_emi/0-happy.png",
    "reskins/shadow_emi/0-interested.png",

    "reskins/white_raven/0-awkward.png",
    "reskins/white_raven/0-calm.png",
    "reskins/white_raven/0-interested.png",
    "reskins/white_raven/0-selected.png",

    "reskins/witchs_costume/0-calm.png",
    "reskins/witchs_costume/0-excited.png",
    "reskins/witchs_costume/0-interested.png",
    "reskins/witchs_costume/0-surprised.png",
];

var clothingChoices = [];
var selectedChoices;
/* maybe move this data to an external file if the hardcoded stuff changes often enough */
var playerTagOptions = {
    'hair_color': {
        values: [
            { value: 'black_hair' }, { value: 'white_hair' },
            { value: 'brunette' }, { value: 'ginger' }, { value: 'blonde' },
            { value: 'green_hair' },
            { value: 'blue_hair' },
            { value: 'purple_hair' },
            { value: 'pink_hair' },
        ],
    },
    'eye_color': {
        values: [
            { value: 'dark_eyes' }, { value: 'pale_eyes' },
            { value: 'red_eyes' }, { value: 'amber_eyes' },
            { value: 'green_eyes' }, { value: 'blue_eyes' },
            { value: 'violet_eyes' },
        ],
    },
    'skin_color': {
        type: 'range',
        values: [
            { value: 'pale-skinned', from: 0, to: 25 },
            { value: 'fair-skinned', from: 25, to: 50 },
            { value: 'olive-skinned', from: 50, to: 75 },
            { value: 'dark-skinned', from: 75, to: 100 },
        ],
    },
    'hair_length': {
        values: [
            { value: 'bald', text: 'Bald - No Hair'},
            { value: 'short_hair', text: 'Short Hair - Does Not Pass Jawline'},
            { value: 'medium_hair', text: 'Medium Hair - Reaches Between Jawline and Shoulders'},
            { value: 'long_hair', text: 'Long Hair - Reaches Beyond Shoulders'},
            { value: 'very_long_hair', text: 'Very Long Hair - Reaches the Thighs or Beyond'},
        ],
    },
    'physical_build': {
        values: [
            { value: 'chubby' },
            { value: 'athletic' },
            { value: 'muscular' },
        ],
    },
    'height': {
        values: [
            { value: 'tall' },
            { value: 'average' },
            { value: 'short' },
        ],
    },
    'pubic_hair_style': {
        values: [
            { value: 'shaved' },
            { value: 'trimmed' },
            { value: 'hairy' },
        ],
    },
    'circumcision': {
        gender: 'male',
        values: [
            { value: 'circumcised' },
            { value: 'uncircumcised' }
        ],
    },
    'sexual_orientation': {
        values: [
            { value: 'straight' },
            { value: 'bi-curious' },
            { value: 'bisexual' },
            { value: 'gay', gender: 'male' },
            { value: 'lesbian', gender: 'female' },
        ]
    }
};
var playerTagSelections = {};

/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Loads all of the content required to display the title
 * screen.
 ************************************************************/
function loadTitleScreen () {
	//selectedChoices = [false, false, true, false, true, false, true, true, false, true, false, false, true, false, true];
    loadClothing();
}

/************************************************************
 * Loads and parses the player clothing XML file.
 ************************************************************/
function loadClothing () {
    clothingChoices = {
        'male': [
            new Clothing('hat', 'hat', EXTRA_ARTICLE, 'head', "player/male/hat.png", false, 0),
            new Clothing('headphones', 'headphones', EXTRA_ARTICLE, 'head', "player/male/headphones.png", true, 1),
            new Clothing('jacket', 'jacket', MINOR_ARTICLE, UPPER_ARTICLE, "player/male/jacket.png", false, 2),
            new Clothing('shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/shirt.png", false, 3),
            new Clothing('t-shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/tshirt.png", false, 4),
            new Clothing('undershirt', 'undershirt', IMPORTANT_ARTICLE, UPPER_ARTICLE, "player/male/undershirt.png", false, 5),

            new Clothing('glasses', 'glasses', EXTRA_ARTICLE, 'head', "player/male/glasses.png", true, 6),
            new Clothing('belt', 'belt', EXTRA_ARTICLE, 'waist', "player/male/belt.png", false, 7),
            new Clothing('pants', 'pants', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/pants.png", true, 8),
            new Clothing('shorts', 'shorts', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/shorts.png", true, 9),
            new Clothing('kilt', 'kilt', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/kilt.png", false, 10),
            new Clothing('boxers', 'underwear', IMPORTANT_ARTICLE, LOWER_ARTICLE, "player/male/boxers.png", true, 11),

            new Clothing('necklace', 'necklace', EXTRA_ARTICLE, 'neck', "player/male/necklace.png", false, 12),
            new Clothing('tie', 'tie', EXTRA_ARTICLE, 'neck', "player/male/tie.png", false, 13),
            new Clothing('gloves', 'gloves', EXTRA_ARTICLE, 'hands', "player/male/gloves.png", true, 14),
            new Clothing('socks', 'socks', MINOR_ARTICLE, 'feet', "player/male/socks.png", true, 15),
            new Clothing('shoes', 'shoes', EXTRA_ARTICLE, 'feet', "player/male/shoes.png", true, 16),
            new Clothing('boots', 'shoes', EXTRA_ARTICLE, 'feet', "player/male/boots.png", true, 17),
        ],
        female: [
            new Clothing('hat', 'hat', EXTRA_ARTICLE, 'head', "player/female/hat.png", false, 0),
            new Clothing('headphones', 'headphones', EXTRA_ARTICLE, 'head', "player/female/headphones.png", true, 1),
            new Clothing('jacket', 'jacket', MINOR_ARTICLE, UPPER_ARTICLE, "player/female/jacket.png", false, 2),
            new Clothing('shirt', 'top', MAJOR_ARTICLE, UPPER_ARTICLE, "player/female/shirt.png", false, 3),
            new Clothing('tank top', 'top', MAJOR_ARTICLE, UPPER_ARTICLE, "player/female/tanktop.png", false, 4),
            new Clothing('bra', 'bra', IMPORTANT_ARTICLE, UPPER_ARTICLE, "player/female/bra.png", false, 5),

            new Clothing('glasses', 'glasses', EXTRA_ARTICLE, 'head', "player/female/glasses.png", true, 6),
            new Clothing('belt', 'belt', EXTRA_ARTICLE, 'waist', "player/female/belt.png", false, 7),
            new Clothing('pants', 'pants', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/pants.png", true, 8),
            new Clothing('shorts', 'shorts', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/shorts.png", true, 9),
            new Clothing('skirt', 'skirt', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/skirt.png", false, 10),
            new Clothing('panties', 'panties', IMPORTANT_ARTICLE, LOWER_ARTICLE, "player/female/panties.png", true, 11),

            new Clothing('necklace', 'necklace', EXTRA_ARTICLE, 'neck', "player/female/necklace.png", false, 12),
            new Clothing('bracelet', 'bracelet', EXTRA_ARTICLE, 'arms', "player/female/bracelet.png", false, 13),
            new Clothing('gloves', 'gloves', EXTRA_ARTICLE, 'hands', "player/female/gloves.png", true, 14),
            new Clothing('stockings', 'socks', MINOR_ARTICLE, 'legs', "player/female/stockings.png", true, 15),
            new Clothing('socks', 'socks', MINOR_ARTICLE, 'feet', "player/female/socks.png", true, 16),
            new Clothing('shoes', 'shoes', EXTRA_ARTICLE, 'feet', "player/female/shoes.png", true, 17),
        ]
    };
}

/************************************************************
 * Updates the clothing on the title screen.
 ************************************************************/
function updateTitleClothing () {
	if (humanPlayer.gender == eGender.MALE) {
		$('#female-clothing-container').hide();
		$('#male-clothing-container').show();
	} else if (humanPlayer.gender == eGender.FEMALE) {
		$('#male-clothing-container').hide();
		$('#female-clothing-container').show();
	}

	for (var i = 0; i < selectedChoices.length; i++) {
		if (selectedChoices[i]) {
			$('#'+humanPlayer.gender+'-clothing-option-'+i).css('opacity', '1');
		} else {
			$('#'+humanPlayer.gender+'-clothing-option-'+i).css('opacity', '0.4');
		}
	}
	//$warningLabel.html("");
}

/**********************************************************************
 *****                   Interaction Functions                    *****
 **********************************************************************/

/************************************************************
 * The player clicked on one of the gender icons on the title
 * screen, or this was called by an internal source.
 ************************************************************/
function changePlayerGender (gender) {
	save.savePlayer();
	humanPlayer.gender = gender;
	save.loadPlayer();
	updateTitleGender();
}

/************************************************************
 * Updates the gender dependent controls on the title screen.
 ************************************************************/
function updateTitleGender() {
    $titleContainer.removeClass('male female').addClass(humanPlayer.gender);
    $playerTagsModal.removeClass('male female').addClass(humanPlayer.gender);

	updateTitleClothing();
}

/************************************************************
 * The player clicked on one of the size icons on the title
 * screen, or this was called by an internal source.
 ************************************************************/
function changePlayerSize (size) {
	humanPlayer.size = size;

    $sizeBlocks.removeClass(eSize.SMALL + ' ' + eSize.MEDIUM + ' ' + eSize.LARGE).addClass(size).attr('data-size', size);
}

/************************************************************
 * The player clicked on an article of clothing on the title
 * screen.
 ************************************************************/
function selectClothing (id) {
	if (selectedChoices[id]) {
		selectedChoices[id] = false;
	} else {
		selectedChoices[id] = true;
	}
	updateTitleClothing();
}

/**************************************************************
 * Add tags to the human player based on the selections in the tag
 * dialog and the size.
 **************************************************************/
function setPlayerTags () {
    var playerTagList = ['human', 'human_' + humanPlayer.gender,
                         humanPlayer.size + (humanPlayer.gender == 'male' ? '_penis' : '_breasts')];

    for (category in playerTagSelections) {
        var sel = playerTagSelections[category];
        if (!(category in playerTagOptions)) continue;
        playerTagOptions[category].values.some(function (choice) {
            if (playerTagOptions[category].type == 'range') {
                if (sel > choice.to) return false;
            } else {
                if (sel != choice.value) return false;
            }
            playerTagList.push(choice.value);
            return true;
        });
    }
    /* applies tags to the player*/
    console.log(playerTagList);
    humanPlayer.baseTags = playerTagList.map(canonicalizeTag);
    humanPlayer.updateTags();
}

/************************************************************
 * The player clicked on the advance button on the title
 * screen.
 ************************************************************/
function validateTitleScreen () {
    /* determine the player's name */
    var playerName = '';
    
	if ($nameField.val() != "") {
        playerName = $nameField.val();
	} else if (humanPlayer.gender == "male") {
        playerName = "Mister";
	} else if (humanPlayer.gender == "female") {
        playerName = 'Missy';
	}
    
    // Nuke all angle-brackets
    playerName = playerName.replace(/<|>/g, '');
    
    humanPlayer.first = playerName;
    humanPlayer.label = playerName;
    
	$gameLabels[HUMAN_PLAYER].html(humanPlayer.label);

	/* count clothing */
	var clothingCount = [0, 0, 0, 0];
	var genderClothingChoices = clothingChoices[humanPlayer.gender];
	for (i = 0; i < genderClothingChoices.length; i++) {
		if (selectedChoices[i]) {
			if (genderClothingChoices[i].position == UPPER_ARTICLE) {
				clothingCount[0]++;
			} else if (genderClothingChoices[i].position == LOWER_ARTICLE) {
				clothingCount[1]++;
			} else {
				clothingCount[2]++;
			}
			clothingCount[3]++;
		}
	}
	console.log(clothingCount);

	/* ensure the player is wearing enough clothing */
	if (clothingCount[3] > 8) {
		$warningLabel.html("You cannot wear more than 8 articles of clothing. Cheater.");
		return;
	}

    /* dress the player */
    wearClothing();
    setPlayerTags();

    save.savePlayer();
    console.log(players[0]);

    loadDefaultFillSuggestions();

    if (SENTRY_INITIALIZED) Sentry.setTag("screen", "select-main");
    screenTransition($titleScreen, $selectScreen);

    if (USAGE_TRACKING === undefined) {
        showUsageTrackingModal();
    } else {
        showResortModal();
    }
}

/**********************************************************************
 *****                    Additional Functions                    *****
 **********************************************************************/

/************************************************************
 * Takes all of the clothing selected by the player and adds it,
 * in a particular order, to the list of clothing they are wearing.
 ************************************************************/
function wearClothing () {
	var position = [[], [], []];
	var importantWorn = [false, false];
    var genderClothingChoices = clothingChoices[humanPlayer.gender];

    /* sort the clothing by position */
    for (var i = genderClothingChoices.length - 1; i >= 0; i--) {
        if (selectedChoices[i]) {
            if (genderClothingChoices[i].position == UPPER_ARTICLE) {
                position[0].push(genderClothingChoices[i]);
            } else if (genderClothingChoices[i].position == LOWER_ARTICLE) {
                position[1].push(genderClothingChoices[i]);
            } else {
                position[2].push(genderClothingChoices[i]);
            }
        }
    }

	/* clear player clothing array */
	humanPlayer.clothing = [];

	/* wear the clothing is sorted order */
	for (var i = 0; i < position[0].length || i < position[1].length; i++) {
		/* wear a lower article, if any remain */
		if (i < position[1].length) {
			humanPlayer.clothing.push(position[1][i]);
		}

		/* wear an upper article, if any remain */
		if (i < position[0].length) {
			humanPlayer.clothing.push(position[0][i]);
		}
	}

	/* wear any other clothing */
	for (var i = 0; i < position[2].length; i++) {
		humanPlayer.clothing.push(position[2][i]);
	}

	humanPlayer.initClothingStatus();

	/* update the visuals */
    displayHumanPlayerClothing();
}


/************************************************************
 * Randomly selects two characters for the title images.
 ************************************************************/
function selectTitleCandy() {
    console.log("Selecting Candy...");
    var candy1 = CANDY_LIST[getRandomNumber(0, CANDY_LIST.length)];
	var candy2 = CANDY_LIST[getRandomNumber(0, CANDY_LIST.length)];


    
    while (candy1.slice(0, candy1.lastIndexOf("/")) == candy2.slice(0, candy2.lastIndexOf("/"))) {
		candy2 = CANDY_LIST[getRandomNumber(0, CANDY_LIST.length)];
	}

    $titleCandy[0].attr("src", "opponents/" + candy1);
    $titleCandy[1].attr("src", "opponents/" + candy2);
}
