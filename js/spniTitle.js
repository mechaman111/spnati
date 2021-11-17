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
    "nami/0-calm.png",                      // High Roster Position
    "nami/0-seductive.png",
    "nami/0-smiling.png",
    "nami/0-smug.png",
    "meia/0-disappointed.png",              // High Roster Position
    "meia/1-busy.png",
    "meia/2-addressing.png",
    "meia/2-pleased.png",
    "natsuki/0-tsun.png",                   // High Roster Position
    "natsuki/1-laugh.png",
    "natsuki/2-happy.png",
    "natsuki/3-isthatapenis.png",
    "komi-san/0-bashful.png",               // High Roster Position
    "komi-san/0-determined.png",
    "komi-san/0-portrait.png",
    "komi-san/1-strippingA.png",
    "pit/0-awkward.png",                    // High Roster Position
    "pit/1-calm.png",
    "pit/2-pumped.png",
    "pit/2-victory.png",
    "jim/0-Neutral.png",                    // High Roster Position
    "jim/0-Confident.png",
    "jim/0-Relaxed.png",
    "jim/0-Selected.png",
    "adrien/0-confident.png",               // High Roster Position
    "adrien/0-sarcastic.png",
    "adrien/2-wink.png",
    "adrien/3-smug.png",
    "kumatora/0-confident.png",             // High Roster Position
    "kumatora/0-cheerful.png",
    "kumatora/0-idle2.png",
    "kumatora/0-stretching.png",
    "hu_tao/0-cocky.png",                   // High Roster Position
    "hu_tao/0-mischievous.png",
    "hu_tao/0-oh_my.png",
    "hu_tao/0-smug.png",
    "twisted_fate/0-Portrait.png",          // High Roster Position
    "twisted_fate/0-Charming.png",
    "twisted_fate/1-Deceiving.png",
    "twisted_fate/1-Happy.png",
    "monika/0-writing-tip.png",             // High Roster Position
    "monika/1-interested.png",
    "monika/2-happy.png",
    "monika/3-shy-happy.png",
    "yuri/0-calm.png",                      // High Roster Position
    "yuri/0-happy.png",
    "yuri/3-chat.png",
    "yuri/3-blush.png",
    "amy/0-start.png",                      // High Roster Position
    "amy/0-cheer.png",
    "amy/1-singing.png",
    "amy/1-chat.png",
    "ignatz/0-friendly.png",                // High Roster Position
    "ignatz/0-flustered.png",
    "ignatz/3-fearthedeer.png",
    "ignatz/3-inspired.png",
    "cynthia/0-battleready.png",            // High Roster Position
    "cynthia/0-pokeball.png",
    "cynthia/2-embarrassed.png",
    "cynthia/2-sarcastic.png",
    "fina/0-calm.png",                      // High Roster Position
    "fina/0-sheepish.png",
    "fina/2-shy.png",
    "fina/3-gazing.png",
    "revy/0-awkward.png",                   // High Roster Position
    "revy/1-heart.png",
    "revy/2-smoking.png",
    "revy/3-laughing.png",
    "jura/0-seductive.png",                 // High Roster Position
    "jura/1-teasing.png",
    "jura/2-interested.png",
    "jura/3-vain.png",
    "megumin/0-smug.png",                   // High Roster Position
    "megumin/0-flustered.png",
    "megumin/2-ecstatic.png",
    "megumin/3-embarrassed.png",
    "perona/0-calm.png",                    // High Roster Position
    "perona/0-smiling.png",
    "perona/1-enticed.png",
    "perona/2-positive.png",
    "pyrrha/0-calm.png",                    // High Roster Position
    "pyrrha/0-awkward.png",
    "pyrrha/1-horny.png",
    "pyrrha/2-encourage.png",
    "jessie/0-calm.png",                    // High Roster Position
    "jessie/0-friendly.png",
    "jessie/0-playful.png",
    "jessie/0-curious.png",
    "wikipe-tan/0-donations.png",           // High Roster Position
    "wikipe-tan/0-casual.png",
    "wikipe-tan/2-flirt.png",
    "wikipe-tan/3-hornyfact.png",
    "polly/0-Neutral.png",                  // High Roster Position
    "polly/0-Flirty.png",
    "polly/0-Excited.png",
    "polly/0-Partying.png",
    "ayano/0-happy.png",                    // Highlighted New Character
    "ayano/0-taunting.png",
    "ayano/0-interested.png",
    "ayano/0-study.png",
    "wasp/0-start.png",                     // Highlighted New Character
    "wasp/0-flirt.png",
    "wasp/0-excited.png",
    "wasp/0-tease.png",
    "yuno_uno/0-aa_select.png",             // Highlighted New Character
    "yuno_uno/0-casual.png",
    "yuno_uno/0-embarrassed.png",
    "yuno_uno/0-happy_to_be_here.png",
    "heris/0-calm.png",                     // Highlighted New Character
    "heris/0-happy.png",
    "heris/1-blush.png",
    "heris/1-interested.png",
    "dark_magician_girl/0-calm.png",        // Highlighted New Character
    "dark_magician_girl/0-flirty.png",
    "dark_magician_girl/0-happy.png",
    "dark_magician_girl/0-interested.png",
    "leon/0-idle.png",                      // Highlighted New Character
    "leon/0-fingerguns.png",
    "leon/0-grinning.png",
    "leon/0-snarky.png",
    "hatsune_miku/0-Casual.png",            // Has Recent Updates
    "hatsune_miku/0-Cheeky.png",
    "hatsune_miku/0-Encouraging.png",
    "hatsune_miku/0-Excited.png",
    "barbara/0-cheering.png",               // Has Recent Updates
    "barbara/0-surprised.png",
    "barbara/0-sheepish.png",
    "barbara/0-happy.png",
    "pinkie_pie/0-excited.png",             // Has Recent Updates
    "pinkie_pie/0-smug.png",
    "pinkie_pie/1-wink.png",
    "pinkie_pie/2-laughing.png",
    "weiss_schnee/0-start.png",             // Has Recent Updates
    "weiss_schnee/0-interested.png",
    "weiss_schnee/0-sarcastic.png",
    "weiss_schnee/0-aroused.png",
    "kyou/0-calm.png",                      // Has Recent Updates
    "kyou/0-sarcastic.png",
    "kyou/2-shy.png",
    "kyou/2-smug.png",
    "critical_darling/0-portrait.png",      // Has Recent Updates
    "critical_darling/0-ice.png",
    "critical_darling/3-sing.png",
    "critical_darling/3-rockin.png",
    "sakura/0-calm.png",                    // Has Recent Updates
    "sakura/0-hi5.png",
    "sakura/1-smile.png",
    "sakura/2-sing.png",
    "sayori/0-excited.png",                 // Has Recent Updates
    "sayori/1-happy.png",
    "sayori/2-thinking.png",
    "sayori/3-embarassed.png",
    "zoe/0-happy.png",                      // Has Recent Updates
    "zoe/0-blush.png",
    "zoe/0-fangirling.png",
    "zoe/0-smug.png",
    "mercy/0-portrait.png",                 // Has Recent Updates
    "mercy/0-confident.png",
    "mercy/3-interested.png",
    "mercy/3-horny.png",
    "noire/0-niya.png",                     // Has Recent Updates
    "noire/0-smug.png",
    "noire/0-teasing.png",
    "noire/0-thinking.png",
    "rosa/0-portrait.png",                  // Has Recent Updates
    "rosa/0-horny.png",
    "rosa/2-embarrassed.png",
    "rosa/1-thinking.png",
    "petra/0-select.png",                   // Has Recent Updates
    "petra/0-calmSmile.png",
    "petra/0-stretch.png",
    "petra/0-horny.png",
    "bernadetta/0-default.png",             // Has Recent Updates
    "bernadetta/0-happy.png",
    "bernadetta/2-comfortable.png",
    "bernadetta/2-timid.png",
    "estelle/0-calm.png",                   // Has Recent Updates
    "estelle/1-determind.png",
    "estelle/2-lecture.png",
    "estelle/3-brush.png",
    "sucrose/0-curious.png",                // Has Recent Updates
    "sucrose/0-aroused.png",
    "sucrose/0-shy.png",
    "sucrose/0-interested.png",
    "takatoshi/0-Select.png",               // Has Recent Updates
    "takatoshi/0-Yakisoba.png",
    "takatoshi/3-Embarrassed.png",
    "takatoshi/3-Yakisoba2.png",
    "streaming-chan/0-neutral.png",         // Has Recent Updates
    "streaming-chan/0-flusteredKawaii.png",
    "streaming-chan/4-lossdang.png",
    "streaming-chan/4-interview.png",
    "wiifitfemale/0-StretchBack.png",       // Has Recent Updates
    "wiifitfemale/0-calm.png",
    "wiifitfemale/0-happy.png",
    "wiifitfemale/0-interested.png",
    "leonie/0-calm.png",                    // Has Recent Updates
    "leonie/0-smug.png",
    "leonie/1-grin.png",
    "leonie/2-stretch-alt.png",
    "amy_rose/0-heart.png",                 // Has Recent Updates
    "amy_rose/0-hammertalk.png",
    "amy_rose/0-horny.png",
    "amy_rose/0-confident.png",
    "kazuma/0-happy.png",                   // Has Recent Updates
    "kazuma/0-appreciative.png",
    "kazuma/1-victory.png",
    "kazuma/3-smirk.png",
    "saki/0-calm.png",                      // Has Recent Updates
    "saki/0-happy.png",
    "saki/0-determined.png",
    "saki/0-horny.png",
    "aella/0-portrait.png",                 // Has Recent Updates
    "aella/0-competitive.png",
    "aella/1-enthused.png",
    "aella/2-horny.png",
    "chara_dreemurr/0-devious.png",         // Has Recent Updates
    "chara_dreemurr/0-relaxed.png",
    "chara_dreemurr/0-aroused.png",
    "chara_dreemurr/0-amused.png",
    "magma_grunt/0-team_magma.png",         // Has Recent Updates
    "magma_grunt/0-scheming2.png",
    "magma_grunt/4-scheming.png",
    "magma_grunt/4-horny_thoughts.png",
    "larachel/0-calm.png",                  // Has Recent Updates
    "larachel/1-boisterous.png",
    "larachel/2-confident.png",
    "larachel/3-dismissive.png",
    "senko/0-interested.png",               // Has Recent Updates
    "senko/0-araara.png",
    "senko/2-hug.png",
    "senko/2-pampering.png",
    "fluttershy/0-kind2.png",               // Has Recent Updates
    "fluttershy/0-flirty.png",
    "fluttershy/0-amused.png",
    "fluttershy/0-flattered.png",
    "laevatein/0-default.png",              // Has Recent Updates
    "laevatein/0-smile.png",
    "laevatein/2-sceptical.png",
    "laevatein/2-surprised.png",
    "asuna_yuuki/0-overjoyed.png",          // Has Recent Updates
    "asuna_yuuki/0-stripAh.png",
    "asuna_yuuki/4-embarrassed.png",
    "asuna_yuuki/4-pleased.png",
    "launch/0-start.png",                   // Has Recent Updates
    "launch/0-win.png",
    "launch/2-horny.png",
    "launch/3-embarrassed.png",
    "rouge/0-calm.png",                     // Has Recent Updates
    "rouge/0-mischievous.png",
    "rouge/0-comms.png",
    "rouge/0-flirty.png",
    "dust/0-calm.png",                      // Has Recent Updates
    "dust/0-victory.png",
    "dust/1-pensive.png",
    "dust/2-laugh.png",
    "ms.fortune/0-Happy.png",               // Has Recent Updates
    "ms.fortune/0-Stoopid.png",
    "ms.fortune/0-Pun.png",
    "ms.fortune/0-Horny.png",
    "yshtola/0-calm.png",                   // Has Recent Updates
    "yshtola/0-content.png",
    "yshtola/0-coy.png",
    "yshtola/0-snicker.png",
    "kamina/0-point.png",                   // Has Recent Updates
    "kamina/0-cross.png",
    "kamina/0-happy.png",
    "kamina/0-excited.png",
    "samus_aran/0-portrait.png",            // Has Recent Updates
    "samus_aran/1-introspective.png",
    "samus_aran/1-curious.png",
    "samus_aran/1-relaxed.png",
    "tomoko/0-idle1.png",                   // Has Recent Updates
    "tomoko/0-excited.png",
    "tomoko/0-serene.png",
    "tomoko/0-shy.png",
    "may/0-calm.png",                       // Has Recent Updates
    "may/0-happy.png",
    "may/0-oopsy.png",
    "may/0-cute.png",
    "gwen/0-calm.png",                      // Has Recent Updates
    "gwen/2-happy.png",
    "gwen/2-horny.png",
    "gwen/2-cocky.png",
    "beatrix/0-curtsy.png",                 // Has Recent Updates
    "beatrix/0-happy.png",
    "beatrix/0-interested.png",
    "beatrix/0-oops.png",
    "lux/0-calm.png",                       // Has Recent Updates
    "lux/0-cocky.png",
    "lux/3-quizzical.png",
    "lux/4-joyous.png",
    "stocking/0-happy.png",                 // Has Recent Updates
    "stocking/0-sipp.png",
    "stocking/0-hex.png",
    "stocking/0-smug.png",
    "nagisa/0-clapping.png",                // Has Recent Updates
    "nagisa/1-calm.png",
    "nagisa/2-z_stripping.png",
    "nagisa/3-embarrassed.png",
    "supernova/0-entering.png",             // Has Recent Updates
    "supernova/0-imagine.png",
    "supernova/4-giggle.png",
    "supernova/4-horny.png",
    "mikan/0-happy.png",
    "mikan/2-happy.png",
    "mikan/0-explain.png",
    "mikan/2-explain.png",
    "cagliostro/0-Cutesy.png",
    "cagliostro/0-Happy.png",
    "cagliostro/0-Shrug.png",
    "cagliostro/0-Interested.png",
    "mari/0-tease.png",
    "mari/0-cheer.png",
    "mari/3-relaxed.png",
    "mari/4-coy.png",
    "reimu/0-select.png",                   // Highlighted New Character
    "reimu/0-smug.png",
    "reimu/0-bluffing.png",
    "reimu/0-pleasant.png"
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
    new PlayerClothing('undershirt', 'shirt', MAJOR_ARTICLE, UPPER_ARTICLE, "player/male/undershirt.png", false, "undershirt", "male", null),

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
    new PlayerClothing('shoes', 'shoes', EXTRA_ARTICLE, 'feet', "player/female/shoes.png", true, "shoesB", "female", null),
];

/**
 * @type {Object<string, PlayerClothing>}
 */
var PLAYER_CLOTHING_OPTIONS = {};
DEFAULT_CLOTHING_OPTIONS.forEach(function (clothing) {
    PLAYER_CLOTHING_OPTIONS[clothing.id] = clothing;
});

/* note: "full" clothing is not included here, so that it's grouped with misc. items */
var CLOTHING_POSITION_SORT_ORDER = [
    [UPPER_ARTICLE],
    [LOWER_ARTICLE],
    ["waist"],
    ["feet", "legs"],
    ["head", "neck"],
    ["hands", "arms"],
]

/**
 * @type {TitleClothingSelectionIcon[]}
 */
var titleClothingSelectors = [];

 /* Keep in sync with total number of calls to beginStartupStage */
var totalLoadStages = 6;
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
    this.elem = clothing.createIconElement("button");

    $(this.elem).on("click", this.onClick.bind(this)).addClass("title-content-button");
    $(this.elem).on("mouseover", displayClothingDescription.bind(null, this.clothing));
    $(this.elem).on("focus", displayClothingDescription.bind(null, this.clothing));

    handleSwipe(this.elem, function (touch, lastPos, startPos) {
        var container = document.getElementById("title-clothing-container");
        var delta = touch.clientY - lastPos[1];

        /* subtract delta so that upward swipes scroll downward and vice-versa */
        container.scroll({
            top: container.scrollTop - delta
        });
    });
}

TitleClothingSelectionIcon.prototype.visible = function () {
    if (this.clothing.collectible) {
        return !this.clothing.collectible.hidden;
    }

    return true;
}

TitleClothingSelectionIcon.prototype.update = function () {
    $(this.elem).removeClass("available selected locked");
    if (this.clothing.isAvailable()) {
        $(this.elem).addClass("available");
        updateClothingCount();
    } else {
        $(this.elem).addClass("locked");
    }

    if (this.clothing.isSelected()) {
        $(this.elem).addClass("selected");
        updateClothingCount();
    }
}

TitleClothingSelectionIcon.prototype.onClick = function () {
    if (this.clothing.isAvailable()) {
        this.clothing.setSelected(!this.clothing.isSelected());
        this.update();
        updateSelectedClothingView();
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
    save.setItem("gender", gender);
    updateTitleScreen();
    updateSelectionVisuals(); // To update epilogue availability status
}

function createClothingSeparator () {
    return separator;
}

function appendSelectors (elem, selectors) {
    return $(elem).append(selectors.map(function (s) {
        return s.elem;
    }));
}

/**
 * 
 * @param {string} pos
 * @returns {number} 
 */
function getPositionSortKey(pos) {
    var ret = CLOTHING_POSITION_SORT_ORDER.findIndex(function (group) {
        return group.indexOf(pos) >= 0;
    });

    return (ret >= 0) ? ret : CLOTHING_POSITION_SORT_ORDER.length;
}

/**
 * 
 * @param {TitleClothingSelectionIcon[]} selectors 
 * @param {number} typeIdx 
 */
function appendClothingTypeGroup (selectors, typeIdx) {
    var positions = {};

    selectors.forEach(function (selector) {
        var key = getPositionSortKey(selector.clothing.position);
        if (!positions[key]) positions[key] = [];
        positions[key].push(selector);
    });

    var typeStr = (typeIdx == 0) ? "important" : ((typeIdx == 1) ? "major" : "other");
    var $container = $("#title-clothing-container");
    var separatorKeys = [];

    Object.keys(positions).sort().reduce(function (ret, key) {
        ret.push([key, positions[key]]);
        return ret;
    }, []).forEach(function (pair, subgroupIdx) {
        var positionKey = pair[0];
        var subgroup = pair[1];

        if ((typeIdx !== 0 || subgroupIdx !== 0) && subgroup.length > 0) {
            let separator = document.createElement("hr");
            separator.className = "clothing-separator";
            separator.setAttribute("data-position-key", positionKey);
            separator.setAttribute("data-clothing-type", typeStr);
            separatorKeys.push(positionKey);

            $container.append(separator);
        }

        $container.append(subgroup.map(function (s) {
            return s.elem;
        }));
    });

    $(".title-clothing-nav-button").filter(function (idx, elem) {
        return (
            (elem.getAttribute("data-nav-target-type") == typeStr) &&
            (separatorKeys.indexOf(elem.getAttribute("data-nav-target-key")) >= 0)
        );
    }).show();
}

function scrollToClothingSeparator (clothingType, positionKey) {
    var container = document.getElementById("title-clothing-container");
    var separator = $(".clothing-separator").filter(function (idx, elem) {
        return (
            (elem.getAttribute("data-clothing-type") == clothingType) &&
            (elem.getAttribute("data-position-key") == positionKey)
        );
    });

    if (separator.length === 0) {
        container.scrollTop = 0;
    } else {
        var offset = separator.position().top - separator.outerHeight(true);
        if (Math.abs(offset) > 5) {
            container.scrollTop += offset;
        }
    }
}

$(".title-clothing-nav-button").on("click", function (ev) {
    var target = ev.target;
    scrollToClothingSeparator(
        target.getAttribute("data-nav-target-type"),
        target.getAttribute("data-nav-target-key")
    );
});

/************************************************************
 * Updates the gender dependent controls on the title screen.
 ************************************************************/
function updateTitleScreen () {
    $('#title-gender-size-container').removeClass('male female').addClass(humanPlayer.gender)
    $playerTagsModal.removeClass('male female').addClass(humanPlayer.gender);

    var typeGroups = [[], [], []];
    var typeIdx = {
        "important": 0,
        "major": 1,
        "minor": 2,
        "extra": 3,
    };

    titleClothingSelectors.sort(function (a, b) {
        var cmpA = (a.clothing.applicable_genders === "all" || a.clothing.applicable_genders === humanPlayer.gender);
        var cmpB = (b.clothing.applicable_genders === "all" || b.clothing.applicable_genders === humanPlayer.gender);
        return cmpB - cmpA;
    }).sort(function (a, b) {
        if (a.clothing.generic === b.clothing.generic) {
            return 0;
        } else if (a.clothing.generic < b.clothing.generic) {
            return -1;
        } else {
            return 1;
        }
    }).sort(function (a, b) {
        return typeIdx[a.clothing.type] - typeIdx[b.clothing.type];
    }).sort(function (a, b) {
        return b.clothing.isAvailable() - a.clothing.isAvailable();
    });

    titleClothingSelectors.forEach(function (selector) {
        $(selector.elem).detach();
        if (!selector.visible()) return;

        switch (selector.clothing.type) {
        case "important": typeGroups[0].push(selector); break;
        case "major": typeGroups[1].push(selector); break;
        case "minor":
        case "extra":
        default:
            typeGroups[2].push(selector);
            break;
        }

        selector.update();
    });

    $("#title-clothing-container").empty();
    $(".title-clothing-nav-button").hide();
    typeGroups.forEach(appendClothingTypeGroup);

    $('.title-clothing-nav-button[data-nav-target-type="important"][data-nav-target-key="0"]').show();

    updateSelectedClothingView();

    $("#title-clothing-desc-block").css({"visibility": "hidden"});
    $("#title-stamina-box")[0].value = humanPlayer.stamina;

    updatePlayerTagsView();
}

function updateSelectedClothingView () {
    var selected = orderSelectedClothing();
    $("#selected-clothing-list").empty().append(selected.map(function (clothing) {
        var elem = clothing.createIconElement("div");
        $(elem).on("click", function () {
            clothing.setSelected(false);
            updateTitleScreen();
        }).on("mouseover", function () {
            displayClothingDescription(clothing);
        });

        return elem;
    }));

    if (selected.length > 0) {
        $("#selected-clothing-empty").hide();
        $("#selected-clothing-list").show();
    } else {
        $("#selected-clothing-empty").show();
        $("#selected-clothing-list").hide();
    }
}

/**
 * 
 * @param {string} tag 
 */
function prettyFormatTag(tag) {
    return tag.split("_").map(function (s) { return s.initCap(); }).join(" ");
}

function updatePlayerTagsView () {
    var anyTagSet = false;
    var resolvedTags = resolveSelectedPlayerTags();
    $("#title-tags-list").empty().hide();

    for (var category in resolvedTags) {
        let categoryLabel = (category === "sexual_orientation" ? "Orientation" : prettyFormatTag(category));
        let rawTag = resolvedTags[category];
        let formattedTag = "";

        if (category === "hair_length" || category === "eye_color" || category === "hair_color") {
            formattedTag = rawTag.split("_", 2)[0].initCap();
        } else if (category === "skin_color") {
            formattedTag = rawTag.split("-", 2)[0].initCap();
        } else {
            formattedTag = prettyFormatTag(rawTag);
        }

        let wrapper = document.createElement("tr");
        wrapper.className = "title-tag-option-wrapper";

        let labelElem = document.createElement("th");
        labelElem.className = "title-tag-option-label";
        labelElem.setAttribute("scope", "row");
        labelElem.innerText = categoryLabel;

        let valueElem = document.createElement("td");
        valueElem.className = "title-tag-option-value";
        valueElem.innerText = formattedTag;

        wrapper.appendChild(labelElem);
        wrapper.appendChild(valueElem);
        $("#title-tags-list").append(wrapper);

        anyTagSet = true;
    }

    if (anyTagSet) {
        $("#title-tags-empty").hide();
        $("#title-tags-list").show();
    } else {
        $("#title-tags-empty").show();
    }
}

/**
 * 
 * @param {PlayerClothing} clothing 
 */
function displayClothingDescription (clothing) {
    if (clothing.collectible) {
        let collectible = clothing.collectible;
        if (!clothing.isAvailable() && collectible.hidden) {
            return;
        }

        $("#title-clothing-title").html(collectible.title);
        
        if (clothing.isAvailable()) {
            $("#title-clothing-subtitle").html(collectible.subtitle).show();
            $("#title-clothing-desc-icon").removeClass("locked").addClass("available");
        } else {
            $("#title-clothing-subtitle").hide();
            $("#title-clothing-desc-icon").removeClass("available").addClass("locked");
        }
        if (
            collectible.player && !(
                (collectible.title.indexOf(collectible.player.metaLabel) >= 0) ||
                (clothing.isAvailable() && collectible.subtitle.indexOf(collectible.player.metaLabel) >= 0) ||
                (!clothing.isAvailable() && collectible.unlock_hint.indexOf(collectible.player.metaLabel) >= 0)
            )
        ) {
            $("#title-clothing-source").text(collectible.player.metaLabel).show();
        } else {
            $("#title-clothing-source").hide();
        }

        $("#title-clothing-desc-block .custom-clothing-img").attr("src", clothing.image);

        if (clothing.isAvailable()) {
            $("#title-clothing-unlock-label").hide();
            $("#title-clothing-description").addClass("unlocked").html(collectible.text).show();
            $("#title-clothing-desc-block .player-clothing-icon").addClass("available");
        } else {
            $("#title-clothing-unlock-label").show();
            $("#title-clothing-description").removeClass("unlocked").html(collectible.unlock_hint).show();
            $("#title-clothing-desc-block .player-clothing-icon").removeClass("available");
        }
    } else {
        $("#title-clothing-title").html(clothing.name.initCap());
        $("#title-clothing-subtitle").text("Always Available").show();
        $("#title-clothing-description").hide();
        $("#title-clothing-source").hide();
        $("#title-clothing-unlock-label").hide();
        $("#title-clothing-desc-block .custom-clothing-img").attr("src", clothing.image);
        $("#title-clothing-desc-block .player-clothing-icon").addClass("available");
    }

    $("#title-clothing-desc-block").css({"visibility": "visible"});
}

/************************************************************
 * The player clicked on one of the size icons on the title
 * screen, or this was called by an internal source.
 ************************************************************/
function changePlayerSize (size) {
    humanPlayer.size = size;
    $sizeBlocks.removeClass(eSize.SMALL + ' ' + eSize.MEDIUM + ' ' + eSize.LARGE).addClass(size).attr('data-size', size);
}


function resolveSelectedPlayerTags () {
    var playerTags = {};

    for (category in playerTagSelections) {
        var sel = playerTagSelections[category];
        if (!(category in playerTagOptions)) continue;
        playerTagOptions[category].values.some(function (choice) {
            if (playerTagOptions[category].type == 'range') {
                if (sel > choice.to) return false;
            } else {
                if (sel != choice.value) return false;
            }

            playerTags[category] = choice.value;
            return true;
        });
    }

    return playerTags;
}

/**************************************************************
 * Add tags to the human player based on the selections in the tag
 * dialog and the size.
 **************************************************************/
function setPlayerTags () {
    var playerTagList = ['human', 'human_' + humanPlayer.gender,
                         humanPlayer.size + (humanPlayer.gender == 'male' ? '_penis' : '_breasts')];

    var resolved = resolveSelectedPlayerTags();
    for (category in resolved) {
        playerTagList.push(resolved[category]);
    }

    /* applies tags to the player*/
    console.log(playerTagList);
    humanPlayer.baseTags = playerTagList.map(canonicalizeTag);
    humanPlayer.updateTags();
}

$("#title-stamina-box").on("change", function (ev) {
    var newTimerValue = $(ev.target).val();
    var newTime = Number(newTimerValue);
    var isValidTimerValue = (newTime != "NaN") && (newTime > 0);
    if (isValidTimerValue){
        humanPlayer.stamina = newTime;
        save.saveOptions();
    } else {
        ev.target.value = humanPlayer.stamina;
    }
});

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

    humanPlayer.first = playerName;
    humanPlayer.label = playerName;

    $gameLabels[HUMAN_PLAYER].text(humanPlayer.label);

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

function orderSelectedClothing () {
    var position = [[], [], []];
    var ret = [];
    var typeScores = {
        "important": 0,
        "major": 1,
        "minor": 2,
        "extra": 3
    };

    save.selectedClothing().sort(function (a, b) {
        return typeScores[a.type] - typeScores[b.type];
    }).forEach(function (clothing) {
        if (clothing.position == UPPER_ARTICLE) {
            position[0].push(clothing);
        } else if (clothing.position == LOWER_ARTICLE) {
            position[1].push(clothing);
        } else {
            position[2].push(clothing);
        }
    });

    /* wear the clothing is sorted order */
    for (var i = 0; i < position[0].length || i < position[1].length; i++) {
        /* wear a lower article, if any remain */
        if (i < position[1].length) {
            ret.push(position[1][i]);
        }

        /* wear an upper article, if any remain */
        if (i < position[0].length) {
            ret.push(position[0][i]);
        }
    }

    /* wear any other clothing */
    for (var i = 0; i < position[2].length; i++) {
        ret.push(position[2][i]);
    }

    return ret;
}

/************************************************************
 * Takes all of the clothing selected by the player and adds it,
 * in a particular order, to the list of clothing they are wearing.
 ************************************************************/
function wearClothing () {
    humanPlayer.clothing = orderSelectedClothing();
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

/************************************************************
 * Update the warning text to say how many items of clothing are being worn.
 ************************************************************/
function updateClothingCount(){
	/* the amount of clothing being worn */
	var clothingCount = save.selectedClothing();
	
	$warningLabel.html(`Select from 0 to 8 articles. Wear whatever you want. (${clothingCount.length}/8)`);
	return;
}