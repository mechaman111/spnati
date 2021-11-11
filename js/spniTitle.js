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

var $gameLoadLabel = $(".game-load-label");
var $gameLoadProgress = $(".game-load-progress");

/**********************************************************************
 *****                    Title Screen Variables                  *****
 **********************************************************************/

var CANDY_LIST = [
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
    "gwen/0-calm.png",
    "gwen/2-happy.png",
    "gwen/2-horny.png",
    "gwen/2-cocky.png",
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
    "komi-san/0-bashful.png",
    "komi-san/0-determined.png",
    "komi-san/0-portrait.png",
    "komi-san/1-strippingA.png",
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
    //"nagisa/0-clapping.png",
    //"nagisa/1-calm.png",
    //"nagisa/2-z_stripping.png",
    //"nagisa/3-embarrassed.png",
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

/* Order matters here. */
var DEFAULT_CLOTHING_OPTIONS = [
    new PlayerClothing('hat', 'hat', EXTRA_ARTICLE, 'head', "player/male/hat.png", false, "hat", "all", null),
    new PlayerClothing('headphones', 'headphones', EXTRA_ARTICLE, 'head', "player/male/headphones.png", true, "headphones", "all", null),

    /****/

    new PlayerClothing('jacket', 'jacket', MINOR_ARTICLE, UPPER_ARTICLE, "player/male/jacket.png", false, "jacketA", "male", null),
    new PlayerClothing('shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/shirt.png", false, "shirtA", "male", null),
    new PlayerClothing('t-shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/tshirt.png", false, "tshirt", "male", null),
    new PlayerClothing('undershirt', 'shirt', IMPORTANT_ARTICLE, UPPER_ARTICLE, "player/male/undershirt.png", false, "undershirt", "male", null),

    new PlayerClothing('jacket', 'jacket', MINOR_ARTICLE, UPPER_ARTICLE, "player/female/jacket.png", false, "jacketB", "female", null),
    new PlayerClothing('shirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/female/shirt.png", false, "shirtB", "female", null),
    new PlayerClothing('tank top', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/female/tanktop.png", false, "tanktop", "female", null),
    new PlayerClothing('bra', 'bra', IMPORTANT_ARTICLE, UPPER_ARTICLE, "player/female/bra.png", false, "bra", "female", null),

    /****/

    new PlayerClothing('glasses', 'glasses', EXTRA_ARTICLE, 'head', "player/male/glasses.png", true, "glasses", "all", null),
    new PlayerClothing('belt', 'belt', EXTRA_ARTICLE, 'waist', "player/male/belt.png", false, "belt", "all", null),
    
    /****/

    new PlayerClothing('pants', 'pants', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/pants.png", true, "pantsA", "male", null),
    new PlayerClothing('shorts', 'shorts', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/shorts.png", true, "shortsA", "male", null),
    new PlayerClothing('kilt', 'skirt', MAJOR_ARTICLE, LOWER_ARTICLE, "player/male/kilt.png", false, "kilt", "male", null),
    new PlayerClothing('boxers', 'underwear', IMPORTANT_ARTICLE, LOWER_ARTICLE, "player/male/boxers.png", true, "boxers", "male", null),

    new PlayerClothing('pants', 'pants', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/pants.png", true, "pantsB", "female", null),
    new PlayerClothing('shorts', 'shorts', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/shorts.png", true, "shortsB", "female", null),
    new PlayerClothing('skirt', 'skirt', MAJOR_ARTICLE, LOWER_ARTICLE, "player/female/skirt.png", false, "skirt", "female", null),
    new PlayerClothing('panties', 'underwear', IMPORTANT_ARTICLE, LOWER_ARTICLE, "player/female/panties.png", true, "panties", "female", null),

    /****/

    new PlayerClothing('necklace', 'jewelry', EXTRA_ARTICLE, 'neck', "player/male/necklace.png", false, "necklace", "all", null),
    new PlayerClothing('gloves', 'gloves', EXTRA_ARTICLE, 'hands', "player/male/gloves.png", true, "gloves", "all", null),
    
    /****/

    new PlayerClothing('tie', 'tie', EXTRA_ARTICLE, 'neck', "player/male/tie.png", false, "tie", "male", null),

    new PlayerClothing('bracelet', 'jewelry', EXTRA_ARTICLE, 'arms', "player/female/bracelet.png", false, "bracelet", "female", null),

    /****/

    new PlayerClothing('socks', 'socks', MINOR_ARTICLE, 'feet', "player/male/socks.png", true, "socksA", "male", null),
    new PlayerClothing('shoes', 'shoes', EXTRA_ARTICLE, 'feet', "player/male/shoes.png", true, "shoesA", "male", null),
    new PlayerClothing('boots', 'shoes', EXTRA_ARTICLE, 'feet', "player/male/boots.png", true, "boots", "male", null),

    new PlayerClothing('stockings', 'socks', MINOR_ARTICLE, 'legs', "player/female/stockings.png", true, "stockings", "female", null),
    new PlayerClothing('socks', 'socks', MINOR_ARTICLE, 'feet', "player/female/socks.png", true, "socksB", "female", null),
    new PlayerClothing('shoes', 'shoes', EXTRA_ARTICLE, 'feet', "player/female/shoes.png", true, "shoesB", "female", null),
];

/**
 * @type {Object<string, PlayerClothing>}
 */
var PLAYER_CLOTHING_OPTIONS = {};
DEFAULT_CLOTHING_OPTIONS.forEach(function (clothing) {
    PLAYER_CLOTHING_OPTIONS[clothing.id] = clothing;
});

/**
 * @type {TitleClothingSelectionIcon[]}
 */
var titleClothingSelectors = [];

 /* Keep in sync with total number of calls to beginStartupStage */
var totalLoadStages = 7;
var curLoadStage = -1;

/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Functions for the startup loading progress menu.
 ************************************************************/

function beginStartupStage (label) {
    curLoadStage++;
    $gameLoadLabel.text(label);
    updateStartupStageProgress(0, 1);
}

function updateStartupStageProgress (curItems, totalItems) {
    /*
     * Add the overall loading progress for all prior stages (curLoadStage / totalLoadStages)
     * to a fraction of (1 / totalLoadStages).
     * (1 / totalLoadStages) * (curItems / totalItems) == curItems / (totalItems * totalLoadStages)
     */
    var progress = Math.floor(100 * (
        (curLoadStage / totalLoadStages) +
        (curItems / (totalItems * totalLoadStages))
    ));
    $gameLoadProgress.text(progress.toString(10));
}

function finishStartupLoading () {
    $("#warning-start-container").removeAttr("hidden");
    $("#warning-load-container").hide();
}

/**
 * Schedule a function to be called when a swipe happens (on a touchscreen).
 * @param {HTMLElement} elem 
 * @param {(touch: Touch, lastPos: [number, number], startPos: [number, number]) => void} onSwipe 
 */
 function handleSwipe (elem, onSwipe) {
    var curTouchIdentifier = null;
    var startPos = [0, 0];
    var lastPos = [0, 0];

    $(elem).on("touchstart", function (ev) {
        if (curTouchIdentifier === null) {
            let touch = null;
            if (ev.targetTouches.length >= 1) {
                touch = ev.targetTouches.item(0);
            } else {
                touch = ev.touches.item(0);
            }

            curTouchIdentifier = touch.identifier;
            startPos = [touch.clientX, touch.clientY];
            lastPos = [touch.clientX, touch.clientY];
        }
    }).on("touchmove", function (ev) {
        if (curTouchIdentifier !== null) {
            let touch = null;
            for (let i = 0; i < ev.touches.length; i++) {
                if (ev.touches.item(i).identifier === curTouchIdentifier) {
                    touch = ev.touches.item(i);
                }
            }

            if (!touch) {
                curTouchIdentifier = null;
                return;
            }

            onSwipe(touch, lastPos, startPos);
            lastPos = [touch.clientX, touch.clientY];
        }
    }).on("touchend", function (ev) {
        if (curTouchIdentifier !== null) {
            let touch = null;
            for (let i = 0; i < ev.touches.length; i++) {
                if (ev.touches.item(i).identifier === curTouchIdentifier) {
                    touch = ev.touches.item(i);
                }
            }

            if (!touch) {
                curTouchIdentifier = null;
            }
        }
    });
}

/**
 * @param {PlayerClothing} clothing 
 */
function TitleClothingSelectionIcon (clothing) {
    this.clothing = clothing;
    this.elem = clothing.createSelectionElement();

    $(this.elem).on("click", this.onClick.bind(this)).addClass("title-content-button");

    /* Selectors block default touch-scrolling behavior, so manually handle that */
    handleSwipe(this.elem, function (touch, lastPos, startPos) {
        var listElem = document.getElementById("title-clothing-container");
        listElem.scrollTop -= touch.clientY - lastPos[1];

        /* Hide tooltips when the user is scrolling */
        if (Math.abs(touch.clientY - startPos[1]) >= 25) {
            $(this.elem).tooltip("hide");
        }
    }.bind(this));

    if (this.clothing.collectible) {
        $(this.elem).attr("title", null).tooltip({
            delay: { show: 50 },
            title: this.tooltip.bind(this)
        }).on("touchstart", function () {
            $(this.elem).tooltip("show");
        }.bind(this));
    }
}

TitleClothingSelectionIcon.prototype.visible = function () {
    if (this.clothing.isAvailable()) {
        return true;
    }
    
    if (this.clothing.applicable_genders !== "all" && humanPlayer.gender !== this.clothing.applicable_genders) {
        return false;
    }

    if (this.clothing.collectible) {
        return !this.clothing.collectible.hidden;
    }

    return false;
}

TitleClothingSelectionIcon.prototype.tooltip = function () {
    var collectible = this.clothing.collectible;
    if (!collectible) return "";

    if (this.clothing.isAvailable()) {
        let tooltip = collectible.title;
        if (collectible.player && tooltip.indexOf(collectible.player.metaLabel) < 0) {
            tooltip += " - from " + collectible.player.metaLabel;
        }

        return tooltip;
    } else {
        return "To unlock: " + collectible.unlock_hint;
    }
}

TitleClothingSelectionIcon.prototype.update = function () {
    $(this.elem).removeClass("available selected");
    if (this.clothing.isAvailable()) {
        $(this.elem).addClass("available");
    }

    if (this.clothing.isSelected()) {
        $(this.elem).addClass("selected");
    }
}

TitleClothingSelectionIcon.prototype.onClick = function () {
    if (this.clothing.isAvailable()) {
        this.clothing.setSelected(!this.clothing.isSelected());
        this.update();
    }
}


/************************************************************
 * Loads all of the content required to display the title
 * screen.
 ************************************************************/
function loadTitleScreen () {
    /* hide Extra Opponents menu if online version */
    if (getReportedOrigin().includes("spnati.net")) {
        document.getElementById("title-fullscreen-button").style.left = "25.5%";
    } else {
        $(".title-extras-button").prop("hidden", false);
    }
}

function setupTitleClothing () {
    loadedOpponents.forEach(function (opp) {
        if (!opp.has_collectibles || !opp.collectibles) return;

        opp.collectibles.forEach(function (collectible) {
            var clothing = collectible.clothing;
            if (
                (!clothing || !PLAYER_CLOTHING_OPTIONS[clothing.id]) ||
                (collectible.status && !includedOpponentStatuses[collectible.status])
            ) {
                return;
            }

            var selector = new TitleClothingSelectionIcon(clothing);
            titleClothingSelectors.push(selector);
        });
    });

    DEFAULT_CLOTHING_OPTIONS.forEach(function (clothing) {
        var selector = new TitleClothingSelectionIcon(clothing);
        titleClothingSelectors.push(selector);
    });
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
    updateTitleScreen();
    updateSelectionVisuals(); // To update epilogue availability status
}

function createClothingSeparator () {
    var separator = document.createElement("hr");
    separator.className = "clothing-separator";
    return separator;
}

/************************************************************
 * Updates the gender dependent controls on the title screen.
 ************************************************************/
function updateTitleScreen () {
    $titleContainer.removeClass('male female').addClass(humanPlayer.gender);
    $playerTagsModal.removeClass('male female').addClass(humanPlayer.gender);

    var availableSelectors = [];
    var defaultSelectors = [];
    var lockedSelectors = [];

    titleClothingSelectors.forEach(function (selector) {
        var clothing = selector.clothing;
        $(selector.elem).detach();

        if (!selector.visible()) {
            return;
        }

        if (selector.clothing.collectible) {
            if (selector.clothing.isAvailable()) {
                availableSelectors.push(selector);
            } else {
                lockedSelectors.push(selector);
            }
        } else {
            defaultSelectors.push(selector);
        }

        selector.update();
    });

    $("#title-clothing-container").empty();

    if (availableSelectors.length > 0) {
        $("#title-clothing-container").append(availableSelectors.map(function (s) {
            return s.elem;
        })).append(createClothingSeparator());
    }

    $("#title-clothing-container").append(defaultSelectors.map(function (s) {
        return s.elem;
    }));

    if (lockedSelectors.length > 0) {
        $("#title-clothing-container").append(createClothingSeparator()).append(
            lockedSelectors.map(function (s) {
                return s.elem;
            })
        );
    }
}

/************************************************************
 * The player clicked on one of the size icons on the title
 * screen, or this was called by an internal source.
 ************************************************************/
function changePlayerSize (size) {
    humanPlayer.size = size;

    $sizeBlocks.removeClass(eSize.SMALL + ' ' + eSize.MEDIUM + ' ' + eSize.LARGE).addClass(size).attr('data-size', size);
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
    var clothingItems = save.selectedClothing();
    console.log(clothingItems.length);

    /* ensure the player is wearing enough clothing */
    if (clothingItems.length > 8) {
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
        updateAnnouncementDropdown();
        showAnnouncements();

        if (curResortEvent && !curResortEvent.resort.checkCharacterThreshold()) {
            curResortEvent.resort.setFlag(false);
        }
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

    save.selectedClothing().reverse().forEach(function (clothing) {
        if (clothing.position == UPPER_ARTICLE) {
            position[0].push(clothing);
        } else if (clothing.position == LOWER_ARTICLE) {
            position[1].push(clothing);
        } else {
            position[2].push(clothing);
        }
    });

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
