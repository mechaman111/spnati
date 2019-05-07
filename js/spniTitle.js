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
    "meia/0-disappointed.png",
    "meia/1-busy.png",
    "meia/2-pleased.png",
    "meia/2-addressing.png",
    "elizabeth/0-calm.png",
    "elizabeth/1-awkward.png",
    "elizabeth/2-interested.png",
    "elizabeth/3-happy.png",
    "marinette/0-wink.png",
    "marinette/2-confident.png",
    "marinette/3-bored.png",
    "marinette/4-excited.png",
    "pit/0-awkward.png",
    "pit/1-calm.png",
    "pit/2-pumped.png",
    "pit/3-shy.png",
    "uravity/0-calm.png",
    "uravity/1-heroic.png",
    "uravity/2-happy.png",
    "uravity/3-embarrassed.png",
    "misato/0-Confident.png",
    "misato/1-Happy.png",
    "misato/2-Smug.png",
    "misato/3-Drink.png",
    "palutena/0-divine.png",
    "palutena/1-calm.png",
    "palutena/2-tranquil.png",
    "palutena/3-surprised.png",
    "zone-tan/0-calm.png",
    "zone-tan/1-interested.png",
    "zone-tan/1-serene2.png",
    "zone-tan/2-wink.png",
    "videl/0-confident.png",
    "videl/1-flying.png",
    "videl/2-happy.png",
    "videl/3-shocked.png",
    "chun-li/0-fight.png",
    "chun-li/1-happy.png",
    "chun-li/2-shocked.png",
    "chun-li/2-victory.png",
    "kyu/0-happy-neutral.png",
    "kyu/1-mischievous.png",
    "kyu/2-excited.png",
    "kyu/3-cheerful.png",
    "aimee/0-calm.png",
    "aimee/0-hunt.png",
    "aimee/1-yell.png",
    "aimee/1-shy.png",
    "amalia/0-blam.png",
    "amalia/0-cheerful.png",
    "amalia/1-smug.png",
    "amalia/1-embarrassed.png",
    "wiifitfemale/0-calm.png",
    "wiifitfemale/0-StretchBack.png",
    "wiifitfemale/0-interested.png",
    "wiifitfemale/0-happy.png",
    "moon/0-joy.png",
    "moon/1-relief.png",
    "moon/2-regret.png",
    "moon/3-smug.png",
    "jin/0-Cracker.png",
    "jin/1-Excited.png",
    "jin/2-Happy.png",
    "jin/3-Flirt.png",
    "corrin_m/0-calm.png",
    "corrin_m/1-happy.png",
    "corrin_m/2-excited.png",
    "corrin_m/3-shy.png",
    "futaba/0-happy.png",
    "futaba/1-triumphant.png",
    "futaba/2-awkward.png",
    "futaba/3-excited.png",
    "monika/0-writing-tip.png",
    "monika/1-interested.png",
    "monika/2-happy.png",
    "monika/3-shy-happy.png",
    "velma/0-happy.png",
    "velma/0-calm.png",
    "velma/1-confident.png",
    "velma/1-shocked.png",
    "d.va/0-winking.png",
    "d.va/1-excited.png",
    "d.va/2-peace.png",
    "d.va/3-shocked.png",
    "nagisa/0-dango.png",
    "nagisa/1-calm.png",
    "nagisa/2-interested.png",
    "nagisa/3-veryembarrassed.png",
    "gwen/0-start.png",
    "gwen/2-happy.png",
    "gwen/2-normal.png",
    "gwen/2-horny.png",
    "lyn/0-calm.png",
    "lyn/1-happy.png",
    "lyn/2-interested.png",
    "lyn/3-shocked.png",
    "corrin_f/0-cheer.png",
    "corrin_f/1-shock.png",
    "corrin_f/2-confident.png",
    "corrin_f/3-humble.png",
    "jura/0-seductive.png",
    "jura/1-teasing.png",
    "jura/2-interested.png",
    "jura/3-vain.png",
    "shimakaze/0-ganbatte.png",
    "shimakaze/1-calling.png",
    "shimakaze/2-stumped.png",
    "shimakaze/3-determined.png",
    "kizuna/0-domo.png",
    "kizuna/0-exult.png",
    "kizuna/0-confident.png",
    "kizuna/0-pleased.png",
    "raven/0-calm.png",
    "raven/1-awkward.png",
    "raven/2-loss.png",
    "raven/3-happy.png",
    "9s/0-happy.png",
    "9s/1-excited.png",
    "9s/2-confident.png",
    "9s/2-clever.png",
    "natsuki/0-tsun.png",
    "natsuki/1-laugh.png",
    "natsuki/2-happy.png",
    "natsuki/3-isthatapenis.png",
    "sayori/0-excited.png",
    "sayori/1-happy.png",
    "sayori/2-thinking.png",
    "sayori/3-embarassed.png",
    "joey/0-introS.png",
    "joey/1-happy.png",
    "joey/2-laugh.png",
    "joey/2-wink.png",
    "twilight/0-calm.png",
    "twilight/0-interested.png",
    "twilight/1-happy.png",
    "twilight/1-horny.png",
    "zizou/0-gloating.png",
    "zizou/1-excited.png",
    "zizou/2-happy.png",
    "zizou/3-interested.png",
    "nagisa/0-dango.png",
    "nagisa/1-calm.png",
    "nagisa/2-interested.png",
    "nagisa/3-veryembarrassed.png",
    "revy/0-awkward.png",
    "revy/1-heart.png",
    "revy/2-smoking.png",
    "revy/3-laughing.png",
];

var clothingChoices = [];
var selectedChoices;
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
    clothingChoices = {
        'male': [
            createNewClothing('Hat', 'hat', EXTRA_ARTICLE, 'head', "player/male/hat.png", false, 0),
            createNewClothing('Headphones', 'headphones', EXTRA_ARTICLE, 'head', "player/male/headphones.png", true, 1),
            createNewClothing('Jacket', 'jacket', MINOR_ARTICLE, UPPER_ARTICLE, "player/male/jacket.png", false, 2),
            createNewClothing('Shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/shirt.png", false, 3),
            createNewClothing('T-Shirt', 't-shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/tshirt.png", false, 4),
            createNewClothing('Undershirt', 'undershirt', IMPORTANT_ARTICLE, UPPER_ARTICLE, "player/male/undershirt.png", false, 5),

            createNewClothing('Glasses', 'glasses', EXTRA_ARTICLE, 'head', "player/male/glasses.png", true, 6),
            createNewClothing('Belt', 'belt', EXTRA_ARTICLE, OTHER_ARTICLE, "player/male/belt.png", false, 7),
            createNewClothing('Pants', 'pants', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/pants.png", true, 8),
            createNewClothing('Shorts', 'shorts', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/shorts.png", true, 9),
            createNewClothing('Kilt', 'kilt', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/kilt.png", false, 10),
            createNewClothing('Boxers', 'boxers', IMPORTANT_ARTICLE, LOWER_ARTICLE, "player/male/boxers.png", true, 11),

            createNewClothing('Necklace', 'necklace', EXTRA_ARTICLE, 'neck', "player/male/necklace.png", false, 12),
            createNewClothing('Tie', 'tie', EXTRA_ARTICLE, 'neck', "player/male/tie.png", false, 13),
            createNewClothing('Gloves', 'gloves', EXTRA_ARTICLE, 'hands', "player/male/gloves.png", true, 14),
            createNewClothing('Socks', 'socks', MINOR_ARTICLE, 'feet', "player/male/socks.png", true, 15),
            createNewClothing('Shoes', 'shoes', EXTRA_ARTICLE, 'feet', "player/male/shoes.png", true, 16),
            createNewClothing('Boots', 'boots', EXTRA_ARTICLE, 'feet', "player/male/boots.png", true, 17),
        ],
        female: [
            createNewClothing('Hat', 'hat', EXTRA_ARTICLE, 'head', "player/female/hat.png", false, 0),
            createNewClothing('Headphones', 'headphones', EXTRA_ARTICLE, 'head', "player/female/headphones.png", true, 1),
            createNewClothing('Jacket', 'jacket', MINOR_ARTICLE, UPPER_ARTICLE, "player/female/jacket.png", false, 2),
            createNewClothing('Shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/female/shirt.png", false, 3),
            createNewClothing('Tank Top', 'tank top', MAJOR_ARTICLE, UPPER_ARTICLE, "player/female/tanktop.png", false, 4),
            createNewClothing('Bra', 'bra', IMPORTANT_ARTICLE, UPPER_ARTICLE, "player/female/bra.png", false, 5),

            createNewClothing('Glasses', 'glasses', EXTRA_ARTICLE, 'head', "player/female/glasses.png", true, 6),
            createNewClothing('Belt', 'belt', EXTRA_ARTICLE, OTHER_ARTICLE, "player/female/belt.png", false, 7),
            createNewClothing('Pants', 'pants', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/pants.png", true, 8),
            createNewClothing('Shorts', 'shorts', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/shorts.png", true, 9),
            createNewClothing('Skirt', 'skirt', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/skirt.png", false, 10),
            createNewClothing('Panties', 'panties', IMPORTANT_ARTICLE, LOWER_ARTICLE, "player/female/panties.png", true, 11),

            createNewClothing('Necklace', 'necklace', EXTRA_ARTICLE, 'neck', "player/female/necklace.png", false, 12),
            createNewClothing('Bracelet', 'bracelet', EXTRA_ARTICLE, 'arms', "player/female/bracelet.png", false, 13),
            createNewClothing('Gloves', 'gloves', EXTRA_ARTICLE, 'hands', "player/female/gloves.png", true, 14),
            createNewClothing('Stockings', 'stockings', MINOR_ARTICLE, 'legs', "player/female/stockings.png", true, 15),
            createNewClothing('Socks', 'socks', MINOR_ARTICLE, 'feet', "player/female/socks.png", true, 16),
            createNewClothing('Shoes', 'shoes', EXTRA_ARTICLE, 'feet', "player/female/shoes.png", true, 17),
        ]
    };
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
	updateTitleGender();
}

/************************************************************
 * Updates the gender dependent controls on the title screen.
 ************************************************************/
function updateTitleGender() {
    $titleContainer.removeClass('male female').addClass(players[HUMAN_PLAYER].gender);
    $playerTagsModal.removeClass('male female').addClass(players[HUMAN_PLAYER].gender);

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
    players[HUMAN_PLAYER].baseTags = playerTagList.map(canonicalizeTag);
    players[HUMAN_PLAYER].updateTags();
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
	} else if (players[HUMAN_PLAYER].gender == "male") {
        playerName = "Mister";
	} else if (players[HUMAN_PLAYER].gender == "female") {
        playerName = 'Missy';
	}
    
    // Nuke all angle-brackets
    playerName = playerName.replace(/<|>/g, '');
    
    players[HUMAN_PLAYER].first = playerName;
    players[HUMAN_PLAYER].label = playerName;
    
	$gameLabels[HUMAN_PLAYER].html(players[HUMAN_PLAYER].label);

	/* count clothing */
	var clothingCount = [0, 0, 0, 0];
	var genderClothingChoices = clothingChoices[players[HUMAN_PLAYER].gender];
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

    screenTransition($titleScreen, $selectScreen);

    if (USAGE_TRACKING === undefined) {
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
    var genderClothingChoices = clothingChoices[players[HUMAN_PLAYER].gender];

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
