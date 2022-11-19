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
$warningContainer = $('#initial-warning');
$titleContainer = $('#main-title-container');
$sizeBlocks = { male: $('#male-size-container'), female: $('#female-size-container') };
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
    "meia/0-interested.png",              // High Roster Position
    "meia/2-pleased.png",
    "meia/2-reminiscing.png",
    "meia/3-happy.png",
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
    "noire/0-niya.png",                     // Has Recent Updates
    "noire/0-smug.png",
    "noire/0-teasing.png",
    "noire/0-embarrassed.png",
    "rosa/0-portrait.png",                  // Has Recent Updates
    "rosa/0-horny.png",
    "rosa/2-embarrassed.png",
    "rosa/1-thinking.png",
    "petra/0-select.png",                   // Has Recent Updates
    "petra/0-wink.png",
    "reskins/petra_war_outfit/0-happy.png",
    "reskins/petra_war_outfit/0-horny.png",
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
    "reskins/leonie_war_outfit/1-grin.png",
    "reskins/leonie_war_outfit/2-stretch-alt.png",
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
    "rouge/0-calm.png",                     // Has Recent Updates
    "rouge/0-mischievous.png",
    "rouge/0-comms.png",
    "rouge/0-flirty.png",
    "dust/0-calm.png",                      // Has Recent Updates
    "dust/0-victory.png",
    "dust/1-pensive.png",
    "dust/2-laugh.png",
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
    "beatrix/0-curtsy.png",                 // Has Recent Updates
    "beatrix/0-happy.png",
    "beatrix/0-interested.png",
    "beatrix/0-oops.png",
    "lux/0-calm.png",                       // Has Recent Updates
    "lux/0-cocky.png",
    "lux/3-quizzical.png",
    "lux/4-joyous.png",
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
    "cagliostro/0-Excited.png",
    "cagliostro/3-Smug.png",
    "cagliostro/3-Grin.png",
    "mari/0-tease.png",
    "reskins/mari_thanksgiving/0-wink-2.png",
    "reskins/mari_office/3-relaxed.png",
    "mari/4-coy.png",
    "reimu/0-select.png",                   // Highlighted New Character
    "reimu/0-smug.png",
    "reimu/0-bluffing.png",
    "reimu/0-pleasant.png",
    "mari_setogaya/0-happy.png",                   // Highlighted New Character
    "mari_setogaya/1-grin.png",
    "mari_setogaya/3-silly.png",
    "mari_setogaya/4-after.png",
    "erufuda/0-pleased.png",                   // Has Recent Updates
    "erufuda/1-smug.png",
    "erufuda/1-sucking.png",
    "erufuda/3-eating.png",
    "ryuji/0-cocky.png",                   // Has Recent Updates
    "ryuji/2-cheerful.png",
    "ryuji/4-what.png",
    "ryuji/5-fingerguns.png",
    "futaba/0-nyoro.png",                   // Has Recent Updates
    "futaba/1-happy.png",
    "futaba/2-bored.png",
    "futaba/3-gremlin.png",
    "videl/0-confident.png",                   // Has Recent Updates
    "videl/1-flying.png",
    "videl/4-happy.png",
    "videl/5-embarrassed.png",
    "yusuke/0-Excited.png",   // Highlighted New Character
    "yusuke/3-Expository.png",
    "yusuke/4-Confused.png",
    "yusuke/4-Frame.png",
    "sly_cooper/0-Select.png",
    "sly_cooper/0-Comms+.png",
    "sly_cooper/0-Select.png",
    "sly_cooper/0-Comms+.png",
    /* TODO: add two more for Sly, instead of using the same two twice */
    "ringo_ando/0-bounceapple.png",
    "ringo_ando/1-glad.png",
    "ringo_ando/2-balanceapple.png",
    "ringo_ando/3-bounceapple.png",
    "hilda/0-conversational.png",
    "hilda/1-relaxed.png",
    "hilda/2-smug.png",
    "hilda/3-flirty.png",
    "joey/0-cheer.png",
    "joey/2-approve.png",
    "joey/3-wink.png",
    "joey/4-hot.png",
    "stocking/0-sipp.png",
    "stocking/2-hex.png",
    "stocking/3-stripping+.png",
    "stocking/4-aroused.png",
    "n/0-coNfused.png",
    "n/4-Naturally.png",
    "n/2-fiddliNg.png",
    "n/6-iNform.png",
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
            { value: 'reverse_bi-curious', gender: 'male', text: 'Male-leaning bi-curious ' },
            { value: 'reverse_bi-curious', gender: 'female', text: 'Female-leaning bi-curious' },
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
 * @param {PlayerClothing} clothing 
 */
function TitleClothingSelectionIcon (clothing) {
    this.clothing = clothing;
    $(this.elem = clothing.createSelectionElement())
        .addClass("title-content-button").click(this.onClick.bind(this))
        .on('touchstart', function() {
            $(this.elem).tooltip('show');
        }.bind(this)).tooltip({
            delay: 50,
            title: function() { return clothing.tooltip(); }
        });
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

TitleClothingSelectionIcon.prototype.update = function () {
    $(this.elem).removeClass("locked selected");
    if (!this.clothing.isAvailable()) {
        $(this.elem).addClass("locked");
    }
    if (this.clothing.isSelected()) {
        $(this.elem).addClass("selected");
    }
}

TitleClothingSelectionIcon.prototype.onClick = function () {
    if (this.clothing.isAvailable()) {
        this.clothing.setSelected(!this.clothing.isSelected());
        this.update();
        updateClothingCount();
    }
}

function setupTitleClothing () {
    var prevScroll = 0;
    $('#title-clothing-container').on('scroll', function() {
        if (Math.abs(this.scrollTop - prevScroll) > this.clientHeight / 4) {
            $("#title-clothing-container .player-clothing-select").tooltip('hide');
            prevScroll = this.scrollTop;
        }
    }).on('show.bs.tooltip', function(ev) {
        $("#title-clothing-container .player-clothing-select").not(ev.target).tooltip('hide');
        prevScroll = this.scrollTop;
    });

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

    updateClothingCount();
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

$('.title-gender-button').on('click', function(ev) {
    changePlayerGender($(ev.target).data('gender'));
});

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
    $('.title-gender-button').each(function() {
        $(this).toggleClass('selected', $(this).data('gender') == humanPlayer.gender);
    });

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
    updateClothingCount();
}

/************************************************************
 * The player clicked on one of the size icons on the title
 * screen, or this was called by an internal source.
 ************************************************************/
function changePlayerSize (size) {
    humanPlayer.size = size;
    $sizeBlocks[humanPlayer.gender].find('.title-size-button').each(function() {
        $(this).toggleClass('selected', $(this).data('size') == size);
    });
}

$('.title-size-block').on('click', '.title-size-button', function(ev) {
    changePlayerSize($(ev.target).data('size'));
});

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

    Sentry.setTag("screen", "select-main");
    screenTransition($titleScreen, $selectScreen);

    updateAnnouncementDropdown();
    showAnnouncements();

    if (curResortEvent && !curResortEvent.resort.checkCharacterThreshold()) {
        curResortEvent.resort.setFlag(false);
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
    var typeIdx = {
        "important": 0,
        "major": 1,
        "minor": 2,
        "extra": 3,
    };

    save.selectedClothing().sort(function (a, b) {
        return typeIdx[a.type] - typeIdx[b.type];
    }).forEach(function (clothing) {
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

/************************************************************
 * Update the warning text to say how many items of clothing are being worn.
 ************************************************************/
function updateClothingCount(){
	/* the amount of clothing being worn */
	var clothingCount = save.selectedClothing();
	
	$warningLabel.html(`Select from 0 to 8 articles. Wear whatever you want. (${clothingCount.length}/8)`);
	return;
}
