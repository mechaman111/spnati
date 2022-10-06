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
var backgrounds = {}
var BACKGROUND_CONFIG_FILE = 'backgrounds.xml';

/* Placeholder default value just in case we fail to load backgrounds.xml. */
var defaultBackground = new Background(
    'default',
    [new BackgroundLayer('img/backgrounds/inventory.png', true)],
    { name: "The Inventory", author: "Zeuses-Swan-Song" }
);
var defaultBackgroundID = 'inventory';

/* The currently displayed background */
var activeBackground = defaultBackground;

/* Background selected by the player in the options menu */
var optionsBackground = defaultBackground;

var useGroupBackgrounds = true;

/* Map from UI theme setting keys (in the options menu and in save data)
 * to the corresponding CSS classes to apply to the page root element.
 */
var UI_THEMES = {
    "default": "default-theme",
    "dark-mode": "dark-mode"
};

/**
 * 
 * @param {string} src 
 * @param {boolean} repeat 
 * @param {"left" | "center" | "right" | null} anchor 
 * @param {boolean} isOverlay 
 * @param {number} [viewportTop] 
 * @param {number} [viewportBottom] 
 */
function BackgroundLayer(src, repeat, anchor, isOverlay, viewportTop, viewportBottom) {
    /**
     * The path to this layer's image.
     * @type {string}
     */
    this.src = src;

    /**
     * Whether to repeat this layer's image horizontally.
     * @type {boolean}
     */
    this.repeat = repeat;

    /**
     * The anchoring mode for the layer:
     * - Default: the background is centered on the play area, but fills the screen.
     * - "center": the background is anchored and constrained to fit within the 4:3 central area.
     * - "left" / "right": the background is anchored to the left or right-hand edge of the central area, and fills its respective side of the screen.
     * @type {"left" | "center" | "right" | null}
     */
    this.anchor = anchor;

    /**
     * Whether to repeat this layer's image horizontally.
     * @type {boolean}
     */
    this.isOverlay = isOverlay;

    /**
     * The top image coordinate to match with the top of the play area.
     * @type {number | undefined}
     */
    this.viewportTop = (!isNaN(viewportTop) && viewportTop !== undefined) ? viewportTop : undefined;

    /**
     * The bottom image coordinate to match with the bottom of the play area.
     * @type {number | undefined}
     */
    this.viewportBottom = (!isNaN(viewportBottom) && viewportBottom !== undefined) ? viewportBottom : undefined;

    /**
     * A callback for when this image is loaded.
     * @type {undefined | (layer: BackgroundLayer) => void}
     */
    this.onload = undefined;

    /**
     * Whether or not this image has been loaded.
     * @type {boolean}
     */
    this.loaded = false;

    /**
     * The width of this background image.
     * @type {number}
     */
    this.width = undefined;

    /**
     * The height of this background image.
     * @type {number}
     */
    this.height = undefined;

    /**
     * An Image element for performing computations with this background image.
     * @type {HTMLImageElement?}
     */
    this.img = undefined;
}

/**
 * Wait for this layer's image to load.
 * @returns {Promise<void>}
 */
BackgroundLayer.prototype.waitForLoad = function () {
    return new Promise((resolve) => {
        if (this.loaded) {
            resolve();
        } else {
            let oldOnLoad = this.onload;
            this.onload = (layer) => {
                resolve();
                if (oldOnLoad) oldOnLoad(layer);
            };

            if (!this.img) {
                this.img = new Image();
                this.img.onload = this.img.onerror = (ev) => {
                    if (!this.height) this.height = this.img.naturalHeight;
                    if (!this.width) this.width = this.img.naturalWidth;
                    this.loaded = true;
                    if (this.onload) this.onload(this);
                };

                this.img.src = this.src;
            }
        }
    });
}

/**
 * Compute CSS property values for this background layer.
 * @param {number} w The width of the display area.
 * @param {number} h The height of the display area.
 * @returns {Promise<{
 *   "container": string,
 *   "elem": HTMLDivElement
 * }>}
 */
BackgroundLayer.prototype.computeDisplayProperties = function (w, h) {
    if (h > (3/4) * w) {
        h = (3/4) * w;
    } else {
        w = 4 * h / 3;
    }

    return this.waitForLoad().then(() => {
        var bgSize = "auto auto";
        var bgPositionY = "center";

        if ((this.viewportBottom !== undefined) && (this.viewportTop !== undefined)) {
            let scale = this.height / (this.viewportBottom - this.viewportTop);
            let offset = ((this.height - this.viewportBottom) - this.viewportTop) / 2;
            bgSize = "auto " + Math.round(scale * h) + "px";
            bgPositionY = "calc(50% + " + h * offset / this.height + "px)";
        } else if (this.anchor) {
            bgSize = "auto 100%";
        } else {
            let ar = this.width / this.height;
            if (ar > 4/3) {
                let scale = Math.sqrt(16/9 / ar);
                if (scale < 1.0) scale = 1.0;
                bgSize = "auto " + Math.round(scale * h) + "px";
            } else {
                let scale = Math.sqrt(ar);
                bgSize = Math.round(scale * w) + "px auto";
            }
        }

        var bgElem = document.createElement("div");
        $(bgElem).css({
            "background-image": "url(" + this.src + ")",
            "background-size": bgSize,
            "background-position-y": bgPositionY,
            "background-repeat": (this.repeat ? "repeat-x" : "no-repeat")
        }).addClass("bg-image " + (this.anchor || "whole"));

        return {
            "container": this.isOverlay ? "#bg-front" : "#bg-back",
            "elem": bgElem
        };
    });
}

/**
 * Constructor for game Background objects.
 * 
 * @constructor
 * @this {Background}
 * @param {string} id The internal ID for this background. 
 *  `~background~` evaluates to this value.
 * @param {BackgroundLayer[]} layers The image layers for this background.
 * @param {Object} metadata Metadata to associate with this background, 
 *  such as `name`, `author`, `filter`, `tags`, etc.
 */
function Background (id, layers, metadata) {
    /**
     * @type {string}
     */
    this.id = id;

    /**
     * @type {BackgroundLayer[]}
     */
    this.layers = layers;

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
     * @type {string}
     * The online / offline / etc. status of this background.
     */
    this.status = metadata.status || '';

    /** 
     * @type {Object}
     * Contains the raw metadata passed to the Background constructor.
     */
    this.metadata = metadata;
}

Background.prototype.update = function () {
    return Promise.all(this.layers.map(
        (layer) => layer.computeDisplayProperties(window.innerWidth, window.innerHeight)
    )).then((layerElems) => {
        $("#bg-back").empty();
        $("#bg-front").empty();
        layerElems.forEach(function (layer) {
            $(layer.container).append(layer.elem);
        });

        $('.screen').css('filter', this.filter || '');
    });
}


/**
 * Sets this background to be displayed.
 * 
 * @returns {Promise<Background>} A Promise that resolves once this background
 * loads.
 */
Background.prototype.activateBackground = function () {
    console.log("Activating background: " + this.id);
    return this.update().then(() => {
        activeBackground = this;
        return this;
    });
}

/**
 * Load background information from an XML element.
 * 
 * Direct children of the given element will be converted to metadata
 * for the background.
 * 
 * Any <tags> element, if found, will be searched for child <tag>
 * elements, which will be pushed to the background's `tags` attribute.
 * 
 * @param {Object} $xml The XML object to load information from.
 * @param {Object} auto_tag_values If provided, a mapping of metadata
 * keys to automatically create tags from.
 * 
 * @returns {Background} The loaded Background object.
 */
function loadBackgroundFromXml($xml, auto_tag_values) {
    var id = $xml.attr('id');
    var layers = [];
    var metadata = {
        tags: []
    };

    $xml.children().each(function () {
        var $elem = $(this);
        var tagName = $elem.prop('tagName').toLowerCase();

        if (tagName == 'src') {
            layers.push(new BackgroundLayer(
                $elem.text(),
                ($elem.attr("repeat") !== "false"),
                $elem.attr("anchor"),
                ($elem.attr("overlay") === "true"),
                parseInt($elem.attr("top"), 10),
                parseInt($elem.attr("bottom"), 10),
            ));
        } else if (tagName === 'tags') {
            $elem.children('tag').each(function() {
                var tag = $(this).text() || '';
                metadata.tags.push(fixupTagFormatting(tag));
            });
        } else {
            var val = $elem.text() || true;

            if (auto_tag_values) {
                if (auto_tag_values[tagName] === 'boolean') {
                    metadata.tags.push(fixupTagFormatting(tagName));
                } else if (auto_tag_values[tagName]) {
                    metadata.tags.push(fixupTagFormatting(val));
                }
            }

            metadata[tagName] = val;
        }
    });

    return new Background(id, layers, metadata);
}

/**
 * Load information for all game backgrounds from BACKGROUND_CONFIG_FILE.
 * 
 * Must be called _after_ loadConfigFile(), to ensure the default
 * background ID is properly set.
 */
function loadBackgrounds() {
    console.log("Loading backgrounds...");
    beginStartupStage("Backgrounds");

    return fetchXML(BACKGROUND_CONFIG_FILE).then(function ($xml) {
        var auto_tag_metadata = {};

        /* Find metadata keys to automatically tag. */
        $xml.children('auto-tag-metadata').each(function () {
            var $elem = $(this);

            $elem.children('key').each(function () {
                var $key = $(this);
                var key_name = $key.text();
                
                if ($key.attr('type') === 'boolean') {
                    auto_tag_metadata[key_name] = 'boolean';
                } else {
                    auto_tag_metadata[key_name] = 'value';
                }
            });
        });

        var backgroundElems = $xml.children('background').get();
        backgroundElems.forEach(function (elem, idx, arr) {
            updateStartupStageProgress(idx, arr.length);
            var bg = loadBackgroundFromXml($(elem), auto_tag_metadata);

            /* If the background has a listed status, check to ensure it's
             * available with the current configuration.
             */
            if (bg.status && !includedOpponentStatuses[bg.status]) return;
            backgrounds[bg.id] = bg;
        });

        if (defaultBackgroundID && backgrounds[defaultBackgroundID]) {
            defaultBackground = backgrounds[defaultBackgroundID];
        }
    }).catch(function (err) {
        console.error("Could not load backgrounds:");
        captureError(err);
    });
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
$('#options-modal .pagination, #game-settings-modal ul.pagination, #extras-modal .extra-characters-options ul.pagination').on('click', 'a', function() {
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
    setActiveOption('options-player-finishing-effect', PLAYER_FINISHING_EFFECT);
    $("#options-ui-font-weight").val(UI_FONT_WEIGHT);
    $("#options-ui-font-width").val(UI_FONT_WIDTH);
    setActiveOption('options-ui-theme', UI_THEME);

    /* Don't display UI theme selector on spnati.net yet
     * TODO: remove this once transition period is over
     */
    if (isMainSite) {
        $("#ui-theme-select-container").hide();
    } else {
        $("#ui-theme-select-container").show();
    }

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

function setUIFontSettings(weight, width) {
    weight = (typeof weight != "number") ? 500 : weight;
    width = (typeof width != "number") ? 100 : width;

    /* These are the max supported ranges for the Open Sans font.
     * The in-game UI font option sliders are more constrained than this.
     */
    if (weight < 300) {
        weight = 300;
    } else if (weight > 800) {
        weight = 800;
    }
    
    if (width < 75) {
        width = 75;
    } else if (width > 100) {
        width = 100;
    }

    UI_FONT_WEIGHT = weight;
    UI_FONT_WIDTH = width;

    /* Round width to 1 digit of precision */
    var roundedWidth = Math.round(width * 10) / 10;

    $("#text-weight-label").text(UI_FONT_WEIGHT);
    $("#text-width-label").text(roundedWidth);

    var targetSheet = document.getElementById("font-options-sheet");
    if (!targetSheet) {
        targetSheet = document.createElement("style");
        targetSheet.type = "text/css";
        targetSheet.id = "font-options-sheet";
        document.head.appendChild(targetSheet);
    }

    targetSheet.innerText = ":root { --base-font-weight: " + weight + "; --base-font-stretch: " + roundedWidth + "% }";
}

function setUITheme (scheme) {
    UI_THEME = scheme;

    $(":root").removeClass(Object.values(UI_THEMES)).addClass(UI_THEMES[scheme] || "default-theme");

    if (!UI_THEMES[scheme]) {
        console.error("Unknown UI theme: " + scheme);
    }
}

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

$('.ui-text-option').on('input', function() {
    setUIFontSettings(
        Number($('#options-ui-font-weight').val()),
        Number($('#options-ui-font-width').val()),
    );
});

$('ul#options-ui-theme').on('click', 'a', function() {
    setUITheme($(this).attr('data-value') || "default");
});


$('ul#options-player-finishing-effect').on('click', 'a', function() {
    PLAYER_FINISHING_EFFECT = $(this).attr('data-value') == 'true';
});

/************************************************************
 * Push a selection image for the given background onto the
 * background selection modal.
 ************************************************************/

/**
 * 
 * @param {Background} background 
 */
function pushBackgroundOption (background) {
    var container = $('<div>', {
        "class": "background-option",
        "data-background": background.id,
        "css": {
            "background-image": background.layers.filter((layer) => {
                return (layer.anchor !== "left" && layer.anchor !== "right");
            }).sort((a, b) => a.isOverlay - b.isOverlay).reverse().map((layer) => "url(" + layer.src + ")").join(", ")
        },
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
    var firstOfflineBackground = false;
    
    /* Push selection images for all backgrounds not already on the menu. */
    Object.keys(backgrounds).forEach(function (id) {
        var bg = backgrounds[id];
        
        if (bg.status === 'offline' && !firstOfflineBackground) {
            firstOfflineBackground = true;
            $("#settings-background").append($("<hr />", { "class": "background-separator" }));
        }

        if ($('#settings-background .background-option[data-background="'+id+'"]').length === 0) {
            pushBackgroundOption(bg);
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
    $masturbationWarningLabel.css("display", "none");
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
    $masturbationWarningLabel.css("display", isValidTimerValue ? "none" : "table-row");
});
