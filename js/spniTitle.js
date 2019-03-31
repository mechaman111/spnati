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
    "ae86/x-left.png",
    "ae86/x-forward.png",
    "ae86/x-sideways.png",
    "mister_clean/0-flirty.png",
    "mister_clean/0-neutral.png",
    "mister_clean/0-happy.png",
    "default-chan/0-ok.png",
    "default-chan/0-salute.png",
    "default-chan/0-awkward.png",
    "gay_spaghetti_chef/0-calm.png",
    "gay_spaghetti_chef/0-flipped.png",
    "gay_spaghetti_chef/0-tilted.png",
    "gay_spaghetti_chef/0-calm.png",
    "kool-aid/0-kool.png",
];

var clothingChoices = [];
var selectedChoices = [false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false];
/* maybe move this data to an external file if the hardcoded stuff changes often enough */
var playerTagOptions = {
    'hair_color': {
        values: [
            { value: 'black_hair' }, { value: 'white_hair' },
            { value: 'brunette' }, { value: 'ginger' }, { value: 'blonde' },
            { value: 'green_hair', extraTags: ['exotic_hair'] },
            { value: 'blue_hair', extraTags: ['exotic_hair'] },
            { value: 'purple_hair', extraTags: ['exotic_hair'] },
            { value: 'pink_hair', extraTags: ['exotic_hair'] },
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
            { value: 'very_long_hair', extraTags: ['long_hair'], text: 'Very Long Hair - Reaches the Thighs or Beyond'},
        ],
    },
    'physical_build': {
        values: [
            { value: 'chubby' },
            { value: 'athletic' },
            { value: 'muscular', extraTags: ['athletic'] },
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
	/* clear previously loaded content */
	clothingChoices = [];

    /* load all hardcoded clothing, it's just easier this way */
	if (players[HUMAN_PLAYER].gender == eGender.MALE) {
		clothingChoices.push(createNewClothing('Hat', 'hat', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/hat.png", false, 0));
		clothingChoices.push(createNewClothing('Headphones', 'headphones', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/headphones.png", true, 1));
		clothingChoices.push(createNewClothing('Jacket', 'jacket', MINOR_ARTICLE, UPPER_ARTICLE, "player/male/jacket.png", false, 2));
		clothingChoices.push(createNewClothing('Shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/shirt.png", false, 3));
		clothingChoices.push(createNewClothing('T-Shirt', 't-shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/tshirt.png", false, 4));
		clothingChoices.push(createNewClothing('Undershirt', 'undershirt', IMPORTANT_ARTICLE, UPPER_ARTICLE, "player/male/undershirt.png", false, 5));

        clothingChoices.push(createNewClothing('Glasses', 'glasses', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/glasses.png", true, 6));
		clothingChoices.push(createNewClothing('Belt', 'belt', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/belt.png", false, 7));
		clothingChoices.push(createNewClothing('Pants', 'pants', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/pants.png", true, 8));
		clothingChoices.push(createNewClothing('Shorts', 'shorts', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/shorts.png", true, 9));
		clothingChoices.push(createNewClothing('Kilt', 'kilt', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/kilt.png", false, 10));
		clothingChoices.push(createNewClothing('Boxers', 'boxers', IMPORTANT_ARTICLE, LOWER_ARTICLE, "player/male/boxers.png", true, 11));

		clothingChoices.push(createNewClothing('Necklace', 'necklace', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/necklace.png", false, 12));
		clothingChoices.push(createNewClothing('Tie', 'tie', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/tie.png", false, 13));
		clothingChoices.push(createNewClothing('Gloves', 'gloves', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/gloves.png", true, 14));
		clothingChoices.push(createNewClothing('Socks', 'socks', MINOR_ARTICLE, OTHER_ARTICLE, "player/male/socks.png", true, 15));
		clothingChoices.push(createNewClothing('Shoes', 'shoes', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/shoes.png", true, 16));
		clothingChoices.push(createNewClothing('Boots', 'boots', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/boots.png", true, 17));
	} else if (players[HUMAN_PLAYER].gender == eGender.FEMALE) {
		clothingChoices.push(createNewClothing('Hat', 'hat', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/hat.png", false, 0));
		clothingChoices.push(createNewClothing('Headphones', 'headphones', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/headphones.png", true, 1));
		clothingChoices.push(createNewClothing('Jacket', 'jacket', MINOR_ARTICLE, UPPER_ARTICLE, "player/female/jacket.png", false, 2));
		clothingChoices.push(createNewClothing('Shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/female/shirt.png", false, 3));
		clothingChoices.push(createNewClothing('Tank Top', 'tank top', MAJOR_ARTICLE, UPPER_ARTICLE, "player/female/tanktop.png", false, 4));
		clothingChoices.push(createNewClothing('Bra', 'bra', IMPORTANT_ARTICLE, UPPER_ARTICLE, "player/female/bra.png", false, 5));

		clothingChoices.push(createNewClothing('Glasses', 'glasses', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/glasses.png", true, 6));
		clothingChoices.push(createNewClothing('Belt', 'belt', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/belt.png", false, 7));
		clothingChoices.push(createNewClothing('Pants', 'pants', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/pants.png", true, 8));
		clothingChoices.push(createNewClothing('Shorts', 'shorts', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/shorts.png", true, 9));
		clothingChoices.push(createNewClothing('Skirt', 'skirt', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/skirt.png", false, 10));
		clothingChoices.push(createNewClothing('Panties', 'panties', IMPORTANT_ARTICLE, LOWER_ARTICLE, "player/female/panties.png", true, 11));

		clothingChoices.push(createNewClothing('Necklace', 'necklace', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/necklace.png", false, 12));
        clothingChoices.push(createNewClothing('Bracelet', 'bracelet', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/bracelet.png", false, 13));
		clothingChoices.push(createNewClothing('Gloves', 'gloves', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/gloves.png", true, 14));
		clothingChoices.push(createNewClothing('Stockings', 'stockings', MINOR_ARTICLE, OTHER_ARTICLE, "player/female/stockings.png", true, 15));
		clothingChoices.push(createNewClothing('Socks', 'socks', MINOR_ARTICLE, OTHER_ARTICLE, "player/female/socks.png", true, 16));
		clothingChoices.push(createNewClothing('Shoes', 'shoes', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/shoes.png", true, 17));
	}
	updateTitleClothing();
}

/************************************************************
 * Updates the clothing on the title screen.
 ************************************************************/
function updateTitleClothing () {
	if (players[HUMAN_PLAYER].gender == eGender.MALE) {
		$('#female-clothing-container').hide();
		$('#male-clothing-container').show();
	} else if (players[HUMAN_PLAYER].gender == eGender.FEMALE) {
		$('#male-clothing-container').hide();
		$('#female-clothing-container').show();
	}

	for (var i = 0; i < selectedChoices.length; i++) {
		if (selectedChoices[i]) {
			$('#'+players[HUMAN_PLAYER].gender+'-clothing-option-'+i).css('opacity', '1');
		} else {
			$('#'+players[HUMAN_PLAYER].gender+'-clothing-option-'+i).css('opacity', '0.4');
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
	players[HUMAN_PLAYER].gender = gender;
	save.loadPlayer();
}

/************************************************************
 * Updates the gender dependent controls on the title screen.
 ************************************************************/
function updateTitleGender() {
    $titleContainer.removeClass('male female').addClass(players[HUMAN_PLAYER].gender);
    $playerTagsModal.removeClass('male female').addClass(players[HUMAN_PLAYER].gender);

	loadClothing();
	updateTitleClothing();
}

/************************************************************
 * The player clicked on one of the size icons on the title
 * screen, or this was called by an internal source.
 ************************************************************/
function changePlayerSize (size) {
	players[HUMAN_PLAYER].size = size;

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
    var playerTagList = ['human', 'human_' + players[HUMAN_PLAYER].gender,
                         players[HUMAN_PLAYER].size + (players[HUMAN_PLAYER].gender == 'male' ? '_penis' : '_breasts')];

    for (category in playerTagSelections) {
        var sel = playerTagSelections[category];
        if (!(category in playerTagOptions)) continue;
        var extraTags = [];
        playerTagOptions[category].values.some(function (choice) {
            if (playerTagOptions[category].type == 'range') {
                if (sel > choice.to) return false;
            } else {
                if (sel != choice.value) return false;
            }
            playerTagList.push(choice.value);
            if (choice.extraTags) {
                extraTags = choice.extraTags;
            }
            return true;
        });
        extraTags.forEach(function(t) { playerTagList.push(t); });
    }
    /* applies tags to the player*/
    console.log(playerTagList);
    players[HUMAN_PLAYER].tags = playerTagList.map(canonicalizeTag);
}

/************************************************************
 * The player clicked on the advance button on the title
 * screen.
 ************************************************************/
function validateTitleScreen () {
    /* determine the player's name */
	if ($nameField.val() != "") {
        players[HUMAN_PLAYER].first = $nameField.val();
        players[HUMAN_PLAYER].label = $nameField.val();
	} else if (players[HUMAN_PLAYER].gender == "male") {
		players[HUMAN_PLAYER].first = "Mister";
		players[HUMAN_PLAYER].label = "Mister";
	} else if (players[HUMAN_PLAYER].gender == "female") {
		players[HUMAN_PLAYER].first = "Missy";
		players[HUMAN_PLAYER].label = "Missy";
	}
	$gameLabels[HUMAN_PLAYER].html(players[HUMAN_PLAYER].label);

	/* count clothing */
	var clothingCount = [0, 0, 0, 0];
	for (i = 0; i < clothingChoices.length; i++) {
		if (selectedChoices[i]) {
			if (clothingChoices[i].position == UPPER_ARTICLE) {
				clothingCount[0]++;
			} else if (clothingChoices[i].position == LOWER_ARTICLE) {
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

    screenTransition($titleScreen, $selectScreen);

    if (!save.data.askedUsageTracking) {
    showUsageTrackingModal();
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

	/* sort the clothing by position */
	for (var i = clothingChoices.length - 1; i >= 0; i--) {
		if (selectedChoices[i] && clothingChoices[i].position == UPPER_ARTICLE) {
			position[0].push(clothingChoices[i]);
		} else if (selectedChoices[i] && clothingChoices[i].position == LOWER_ARTICLE) {
			position[1].push(clothingChoices[i]);
		} else if (selectedChoices[i]) {
			position[2].push(clothingChoices[i]);
		}
	}

	/* clear player clothing array */
	players[HUMAN_PLAYER].clothing = [];

	/* wear the clothing is sorted order */
	for (var i = 0; i < position[0].length || i < position[1].length; i++) {
		/* wear a lower article, if any remain */
		if (i < position[1].length) {
			players[HUMAN_PLAYER].clothing.push(position[1][i]);
		}

		/* wear an upper article, if any remain */
		if (i < position[0].length) {
			players[HUMAN_PLAYER].clothing.push(position[0][i]);
		}
	}

	/* wear any other clothing */
	for (var i = 0; i < position[2].length; i++) {
		players[HUMAN_PLAYER].clothing.push(position[2][i]);
	}

	players[HUMAN_PLAYER].initClothingStatus();

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
