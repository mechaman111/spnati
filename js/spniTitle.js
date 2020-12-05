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
$warningContainer = $('#warning-container');
$titleContainer = $('#main-title-container');
$sizeBlocks = $('.title-size-block');
$clothingTable = $("#title-clothing-table");
$warningLabel = $("#title-warning-label");
$titleCandy = [$("#left-title-candy"), $("#right-title-candy")];


/**********************************************************************
 *****                    Title Screen Variables                  *****
 **********************************************************************/

var CANDY_LIST = [

/* VALENTINE'S EVENT */

/*
    "reskins/meia_cupid/0-disappointed.png",
    "reskins/meia_cupid/1-busy.png",
    "reskins/meia_cupid/2-pleased.png",
    "reskins/meia_cupid/2-addressing.png",
    "reskins/cheerleaderochako/0-calm.png",
    "reskins/cheerleaderochako/1-heroic.png",
    "reskins/cheerleaderochako/2-happy.png",
    "reskins/cheerleaderochako/3-embarrassed.png",
    "reskins/monika_love_bug/0-writing-tip.png",
    "reskins/monika_love_bug/1-interested.png",
    "reskins/monika_love_bug/2-happy.png",
    "reskins/monika_love_bug/3-shy-happy.png",
    "reskins/d.va_cruiser/0-winking.png",
    "reskins/d.va_cruiser/1-excited.png",
    "reskins/d.va_cruiser/2-peace.png",
    "reskins/d.va_cruiser/3-shocked.png",
    "reskins/flower_girl_ini/0-sleepy.png",
    "reskins/flower_girl_ini/1-excited.png",
    "reskins/flower_girl_ini/2-dumb.png",
    "reskins/flower_girl_ini/3-cracker.png",
    "reskins/festival_aella/0-happy.png",
    "reskins/festival_aella/1-flustered.png",
    "reskins/festival_aella/2-interested.png",
    "reskins/festival_aella/2-happy.png",
    "reskins/lynbride/0-calm.png",
    "reskins/lynbride/1-happy.png",
    "reskins/lynbride/2-interested.png",
    "reskins/lynbride/3-happy.png",
    "reskins/chiakimaid/0-sleepy.png",
    "reskins/chiakimaid/1-happy.png",
    "reskins/chiakimaid/2-excited.png",
    "reskins/chiakimaid/3-embarrassed.png",
    "reskins/zizou_valentine/0-happy.png",
    "reskins/zizou_valentine/1-excited.png",
    "reskins/zizou_valentine/2-appreciative.png",
    "reskins/zizou_valentine/2-puzzled.png",
    "reskins/nagisa_maid_cafe/0-happy.png",
    "reskins/nagisa_maid_cafe/1-z_stripping.png",
    "reskins/nagisa_maid_cafe/2-wink.png",
    "reskins/nagisa_maid_cafe/3-embarrassed_2.png",
*/

/* SUMMER EVENT */
/*
    "reskins/juri_summer/0-calm.png",
    "reskins/juri_summer/0-horny.png",
    "reskins/juri_summer/1-excited.png",
    "reskins/juri_summer/1-interested.png",

    "reskins/monika_stirring_mermaid/0-writing-tip.png",
    "reskins/monika_stirring_mermaid/1-interested.png",
    "reskins/monika_stirring_mermaid/2-happy.png",
    "reskins/monika_stirring_mermaid/3-shy-happy.png",

    "reskins/summertime_ryuji/0-cocky.png",
    "reskins/summertime_ryuji/1-cheerful.png",
    "reskins/summertime_ryuji/2-startled.png",
    "reskins/summertime_ryuji/3-awkward.png",

    "reskins/swimsuit_sheena/0-calm.png",
    "reskins/swimsuit_sheena/1-excited.png",
    "reskins/swimsuit_sheena/2-interested.png",
    "reskins/swimsuit_sheena/3-sulky.png",

    "reskins/summer_larachel/0-calm.png",
    "reskins/summer_larachel/1-confident.png",
    "reskins/summer_larachel/2-calm.png",
    "reskins/summer_larachel/3-confident.png",

    "reskins/nugi-chan_bikini/0-select.png",
    "reskins/nugi-chan_bikini/1-loss.png",
    "reskins/nugi-chan_bikini/2-embarrassed.png",
    "reskins/nugi-chan_bikini/3-interested.png",

    "reskins/estelle_refreshing_dress/0-calm.png",
    "reskins/estelle_refreshing_dress/1-strip.png",
    "reskins/estelle_refreshing_dress/2-happy.png",
    "reskins/estelle_refreshing_dress/2-after.png",

    "reskins/zizou_summer/0-appreciative.png",
    "reskins/zizou_summer/0-victory.png",
    "reskins/zizou_summer/0-angry.png",
    "reskins/zizou_summer/0-awkward.png",

    "reskins/summertime_sayaka/0-happy.png",
    "reskins/summertime_sayaka/1-calm.png",
    "reskins/summertime_sayaka/2-bored.png",
    "reskins/summertime_sayaka/3-awkward.png",
*/

/* HALLOWEEN EVENT */
/*
    "reskins/emi_shadow/0-annoyed.png",
    "reskins/emi_shadow/1-shrug.png",
    "reskins/emi_shadow/2-calm.png",
    "reskins/emi_shadow/3-stripped.png",
    "reskins/misato_catrina/0-Calm.png",
    "reskins/misato_catrina/1-Confident.png",
    "reskins/misato_catrina/2-Happy.png",
    "reskins/misato_catrina/3-Drink.png",
    "reskins/ghost_bride_weiss_schnee/0-angry.png",
    "reskins/ghost_bride_weiss_schnee/1-grinning.png",
    "reskins/ghost_bride_weiss_schnee/2-happy.png",
    "reskins/ghost_bride_weiss_schnee/3-confused.png",
    "reskins/d.va_black_cat/0-winking.png",
    "reskins/d.va_black_cat/1-excited.png",
    "reskins/d.va_black_cat/2-peace.png",
    "reskins/d.va_black_cat/3-shocked.png",
    "reskins/larachel_harvest_princess/0-calm.png",
    "reskins/larachel_harvest_princess/1-smug.png",
    "reskins/larachel_harvest_princess/2-dismissive.png",
    "reskins/larachel_harvest_princess/3-confident.png",
    "reskins/white_raven/0-selected.png",
    "reskins/white_raven/1-loss.png",
    "reskins/white_raven/2-happy.png",
    "reskins/white_raven/3-thinking.png",
    "reskins/witchs_costume/0-calm.png",
    "reskins/witchs_costume/1-pushing.png",
    "reskins/witchs_costume/2-interested.png",
    "reskins/witchs_costume/3-happy.png",
    "reskins/yunyun_trick_or_friends/0-Happy.png",
    "reskins/yunyun_trick_or_friends/1-Excited.png",
    "reskins/yunyun_trick_or_friends/2-Posing2.png",
    "reskins/yunyun_trick_or_friends/3-Posing.png",
*/

    "9s/0-happy.png",
    "9s/1-excited.png",
    "9s/2-clever.png",
    "9s/2-confident.png",
    "aimee/0-calm.png",
    "aimee/0-hunt.png",
    "aimee/1-shy.png",
    "aimee/1-yell.png",
    "amalia/0-blam.png",
    "amalia/0-cheerful.png",
    "amalia/1-embarrassed.png",
    "amalia/1-smug.png",
    "chun-li/0-fight.png",
    "chun-li/1-happy.png",
    "chun-li/2-shocked.png",
    "chun-li/2-victory.png",
    "corrin_f/0-cheer.png",
    "corrin_f/1-shock.png",
    "corrin_f/2-confident.png",
    "corrin_f/3-humble.png",
    "d.va/0-winking.png",
    "d.va/1-excited.png",
    "d.va/2-peace.png",
    "d.va/3-shocked.png",
    "elizabeth/0-calm.png",
    "elizabeth/1-awkward.png",
    "elizabeth/2-interested.png",
    "elizabeth/3-happy.png",
    "emi/1-excited.png",
    "emi/1-laughing.png",
    "emi/2-excited.png",
    "emi/2-interested.png",
    "futaba/0-happy.png",
    "futaba/1-triumphant.png",
    "futaba/2-awkward.png",
    "futaba/3-excited.png",
    "gwen/0-start.png",
    "gwen/2-happy.png",
    "gwen/2-horny.png",
    "gwen/2-normal.png",
    "haru/0-creepy.png",
    "haru/1-perky.png",
    "haru/2-joyful.png",
    "haru/3-aroused.png",
    "jin/0-Cracker.png",
    "jin/1-Excited.png",
    "jin/2-Happy.png",
    "jin/3-Flirt.png",
    "joey/0-introS.png",
    "joey/1-happy.png",
    "joey/2-laugh.png",
    "joey/2-wink.png",
    "jura/0-seductive.png",
    "jura/1-teasing.png",
    "jura/2-interested.png",
    "jura/3-vain.png",
    "juri/0-calm.png",
    "juri/0-horny.png",
    "juri/1-excited.png",
    "juri/2-interested.png",
    "kamina/0-cross.png",
    "kamina/1-point.png",
    "kamina/2-strip.png",
    "kamina/3-happy.png",
    "kizuna/0-confident.png",
    "kizuna/0-domo.png",
    "kizuna/0-exult.png",
    "kizuna/0-pleased.png",
    "kyu/0-happy-neutral.png",
    "kyu/1-mischievous.png",
    "kyu/2-excited.png",
    "kyu/3-cheerful.png",
    //"marinette/0-wink.png",
    //"marinette/2-confident.png",
    //"marinette/3-bored.png",
    //"marinette/4-excited.png",
    "meia/0-disappointed.png",
    "meia/1-busy.png",
    "meia/2-addressing.png",
    "meia/2-pleased.png",
    "misato/0-Confident.png",
    "misato/1-Happy.png",
    "misato/2-Smug.png",
    "misato/3-Drink.png",
    "monika/0-writing-tip.png",
    "monika/1-interested.png",
    "monika/2-happy.png",
    "monika/3-shy-happy.png",
    "nagisa/0-clapping.png",
    "nagisa/1-calm.png",
    "nagisa/2-z_stripping.png",
    "nagisa/3-embarrassed.png",
    "natsuki/0-tsun.png",
    "natsuki/1-laugh.png",
    "natsuki/2-happy.png",
    "natsuki/3-isthatapenis.png",
    "palutena/0-divine.png",
    "palutena/1-calm.png",
    "palutena/2-tranquil.png",
    "palutena/3-surprised.png",
    "panty/0-laughing.png",
    "panty/0-impressed.png",
    "panty/0-bored.png",
    "panty/0-xxstripping.png",
    "pinkie_pie/0-excited.png",
    "pinkie_pie/0-smug.png",
    "pinkie_pie/1-wink.png",
    "pinkie_pie/2-laughing.png",
    "pit/0-awkward.png",
    "pit/1-calm.png",
    "pit/2-pumped.png",
    "pit/3-shy.png",
    "raven/0-calm.png",
    "raven/1-awkward.png",
    "raven/2-loss.png",
    "raven/3-happy.png",
    "revy/0-awkward.png",
    "revy/1-heart.png",
    "revy/2-smoking.png",
    "revy/3-laughing.png",
    "ryuji/0-cocky.png",
    "ryuji/1-cheerful.png",
    "ryuji/2-startled.png",
    "ryuji/3-awkward.png",
    "ryuko/0-mako-support.png",
    "ryuko/0-start.png",
    "ryuko/2-smug.png",
    "ryuko/3-thinking.png",
    "sayori/0-excited.png",
    "sayori/1-happy.png",
    "sayori/2-thinking.png",
    "sayori/3-embarassed.png",
    "shimakaze/0-ganbatte.png",
    "shimakaze/1-calling.png",
    "shimakaze/2-stumped.png",
    "shimakaze/3-determined.png",
    "twilight/0-calm.png",
    "twilight/0-interested.png",
    "twilight/1-happy.png",
    "twilight/1-horny.png",
    "uravity/0-calm.png",
    "uravity/1-heroic.png",
    "uravity/2-happy.png",
    "uravity/3-embarrassed.png",
    "velma/0-calm.png",
    "velma/0-happy.png",
    "velma/1-confident.png",
    "velma/1-shocked.png",
    "wiifitfemale/0-StretchBack.png",
    "wiifitfemale/0-calm.png",
    "wiifitfemale/0-happy.png",
    "wiifitfemale/0-interested.png",
    "zizou/0-gloating.png",
    "zizou/1-excited.png",
    "zizou/2-happy.png",
    "zizou/3-interested.png",
    "zone-tan/0-pleased.png",
    "zone-tan/1-explain.png",
    "zone-tan/1-smirk.png",
    "zone-tan/2-stroking.png",
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
            { value: 'skinny' },
            { value: 'chubby' },
            { value: 'curvy', gender: 'female' },
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
    updateSelectionVisuals(); // To update epilogue availability status
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
        playerName = 'Miss';
	}

    // Nuke all angle-brackets
    playerName = playerName.replace(/<|>/g, '');

    humanPlayer.first = playerName;
    humanPlayer.label = playerName;

	$gameLabels[HUMAN_PLAYER].html(humanPlayer.label);

	/* count clothing */
	var clothingCount = [0, 0, 0, 0];
	var genderClothingChoices = clothingChoices[humanPlayer.gender];
	for (var i = 0; i < genderClothingChoices.length; i++) {
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

    setLocalDayOrNight();
    updateAllBehaviours(null, null, SELECTED);
    updateSelectionVisuals();

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
