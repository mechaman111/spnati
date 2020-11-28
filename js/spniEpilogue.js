/* Epilogue selection modal elements */
var $epilogueSelectionModal = $('#epilogue-modal'); //the modal box
var $epilogueHeader = $('#epilogue-header-text'); //the header text for the epilogue selection box
var $epilogueTip = $('#epilogue-header-tip');
var $epilogueList = $('#epilogue-list'); //the list of epilogues
var $epilogueAcceptButton = $('#epilogue-modal-accept-button'); //click this button to go with the chosen ending

var epilogueSelections = []; //references to the epilogue selection UI elements

var epilogues = []; //list of epilogue data objects
var chosenEpilogue = null;
var epiloguePlayer = null;
var epilogueSuffix = 0;

/* Epilogue UI elements */
var $epilogueScreen = $('#epilogue-screen');
var $epilogueButtons = $('#epilogue-buttons');

var $epiloguePrevButton = $('#epilogue-buttons #epilogue-previous');
var $epilogueNextButton = $('#epilogue-buttons #epilogue-next');
var $epilogueRestartButton = $('#epilogue-buttons #epilogue-restart');

var $epilogueSkipSelector = $('#epilogue-skip-scene');
var $epilogueHotReloadBtn = $('#epilogue-reload');

// Winning the game, with endings available.
var winStr = "You've won the game, and some of your competitors might be ready for something <i>extra</i>...<br>Who among these players did you become close with?";

// Player won the game, but none of the characters have an ending written.
var winStrNone = "You've won the game!<br>We hope you enjoyed the game!  Be sure to play again sometime!";

// Player lost the game. Currently no support for ending scenes when other players win.
var lossStr = "Although you didn't win this time, we hope you had a good time anyway.<br>You were probably just unlucky, so be sure to give it another try soon!";

// Player won the game, but epilogues are disabled.
var winEpiloguesDisabledStr = winStrNone; // "You won... but epilogues have been disabled.";

// Player lost the game with epilogues disabled.
var lossEpiloguesDisabledStr = lossStr; // "You lost... but epilogues have been disabled.";

/* Random tips displayed at game end. */
var endingTips = [
    "Keeping pairs is usually better than going for rare hands.",
    "Sit back and enjoy the game, maybe you'll find something new!",
    "Some characters have special interactions with each other. How many have you found?",
    "Found a cool, sexy, or funny scene? Share it with us!",
    "Really stuck and don't know how to play well? Give Card Suggest a try!",
    "Curious about the game or have any questions? Check out our FAQ!",
    "SPNATI is an open-source project, and it's brought to life by people like you. Why not give making your own characters a shot?",
    "All of these characters were made by someone like you!",
    "One of your favorite opponents might turn out to be someone you've never heard of before. Try some new characters; you might end up falling in love!",
    "Some characters have secret interactions. See if you can uncover the conditions to trigger these events.",
    "Bug reports are sent to publicly viewable channels on Discord. They are not sent to the characters in-game. If you use the bug report function to RP, you will be openly made fun of, then blocked for spam.",
    "Card suggest is useful when you don't feel like thinking, but it isn't perfect.",
    "Enjoying a character? Let us know in the Discord! We love to get praise!",
    "Cant find a character? It may have been moved to the offline version.",
    "Have a character you want to see in the game? Make them!",
    "Poker is mostly a game of luck. If you don't win the first time, try again!",
    "If you have no other hand, then you're dependent on your high card. Good luck.",
    "If you have two cards of the same rank, that is called a Pair. Pairs are the easiest and most efficient hand to go for. Get them whenever you can!",
    "The Two Pair hand is pretty self explanatory: It's when you have two sets of two of the same rank card. These are stronger than a Pair, and you don't have to risk losing a Pair to get it.",
    "The Three of a Kind hand is, naturally, what having three of the same rank card is called. This is stronger than the Two Pair hand, and if you go for it and fail you might still get a Pair.",
    "Five cards in a sequence (for example, 2/3/4/5/6) is called a Straight. Straights beat Three of a Kind and lower. Remember that, even if you are only one card away from a Straight, you still only have at best a nearly one in thirteen chance of getting it.",
    "Five cards of the same suit with no other hand is called a Flush, and it's stronger than a Straight. Remember that, even if you are only card away, you still only have at best a nearly one in four chance of getting this hand.",
    "A Full House isn't just a great two word horror story, but also the term for the hand you have when you have both a set of three cards of the same rank, and another set of two cards of the same rank. It is stronger than a flush.",
    "A Four of a Kind is the name for a hand with four cards of the same rank. It's stronger than a full house, and one of the highest hands you can go for while still having a pair to fall back on.",
    "A Straight Flush is a combination of a Straight and a Flush, all five cards in sequence and of the same suit, and is stronger than a Four of a Kind. Remember that, even if you are only one away, there is literally only one card in the deck that will give this hand to you.",
    "A Royal Flush is specifically an Ace, King, Queen, Jack, and 10, all of the same suit. It is the rarest and strongest hand. It is never really worth going for if you want to win, but it's still pretty damn cool if you can get it.",
    "Despite what a certain spiky haired protagonist might say, believing in the heart of the cards is not a great strategy. Going for safe hands is usually the way to go.",
    "Remember, you don't need the best hand: you just need to not have the worst. Getting any hand above a high card is usually enough to survive a round.",
];

/************************************************************
 * Animation class. Used instead of CSS animations for the control over stopping/rewinding/etc.
 ************************************************************/
function Animation(id, frames, updateFunc, loop, easingFunction, clampFunction, iterations) {
    this.id = id;
    this.looped = loop === "1" || loop === "true";
    this.keyframes = Animation.prototype.cloneFrames(frames);
    this.iterations = iterations;
    this.easingFunction = easingFunction || "smooth";
    this.clampFunction = clampFunction || "wrap";
    this.duration = frames[frames.length - 1].end;
    this.elapsed = 0;
    this.updateFunc = updateFunc;
}
Animation.prototype.easingFunctions = {
    "linear": function (t) { return t; },
    "smooth": function (t) { return 3 * t * t - 2 * t * t * t; },
    "ease-in": function (t) { return t * t; },
    "ease-out": function (t) { return t * (2 - t); },
    "elastic": function (t) { return (.04 - .04 / t) * Math.sin(25 * t) + 1; },
    "ease-in-cubic": function (t) { return t * t * t; },
    "ease-out-cubic": function (t) { t--; return 1 + t * t * t; },
    "ease-in-sin": function (t) { return 1 + Math.sin(Math.PI / 2 * t - Math.PI / 2); },
    "ease-out-sin": function (t) { return Math.sin(Math.PI / 2 * t); },
    "ease-in-out-cubic": function (t) { return t < .5 ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1; },
    "ease-out-in": function (t) { return t < .5 ? Animation.prototype.easingFunctions["ease-out"](2 * t) * 0.5 : Animation.prototype.easingFunctions["ease-in"](2 * (t - 0.5)) * 0.5 + 0.5; },
    "ease-out-in-cubic": function (t) { return t < .5 ? Animation.prototype.easingFunctions["ease-out-cubic"](2 * t) * 0.5 : Animation.prototype.easingFunctions["ease-in-cubic"](2 * (t - 0.5)) * 0.5 + 0.5; },
    "bounce": function (t) {
        if (t < 0.3636) {
            return 7.5625 * t * t;
        }
        else if (t < 0.7273) {
            t -= 0.5455;
            return 7.5625 * t * t + 0.75;
        }
        else if (t < 0.9091) {
            t -= 0.8182;
            return 7.5625 * t * t + 0.9375;
        }
        else {
            t -= 0.9545;
            return 7.5625 * t * t + 0.984375;
        }
    },
};
/**
 * Clones a frame
 */
Animation.prototype.cloneFrames = function (frames) {
    var copy = [];
    for (var i = 0; i < frames.length; i++) {
        var keyframe = jQuery.extend({}, frames[i]);
        keyframe.index = i;
        keyframe.keyframes = copy;
        copy.push(keyframe);
    }
    return copy;
};
Animation.prototype.isComplete = function () {
    var life = this.elapsed;
    if (this.looped) {
        return this.iterations > 0 ? life / this.duration >= this.iterations : false;
    }
    return life >= this.duration;
};
Animation.prototype.update = function (elapsedMs) {
    this.elapsed += elapsedMs;

    if (!this.updateFunc) { return; }

    //determine what keyframes we're between
    var last;
    var t = this.elapsed;
    if (t < 0) {
        return;
    }
    if (this.duration === 0) {
        t = 1;
    }
    else {
        var easingFunction = this.easingFunction;
        t /= this.duration;
        if (this.looped) {
            t = clampingFunctions[this.clampFunction](t);
            if (this.isComplete()) {
                t = 1;
            }
        }
        else {
            t = Math.min(1, t);
        }
        t = this.easingFunctions[easingFunction](t)
        t *= this.duration;
    }
    for (var i = this.keyframes.length - 1; i >= 0; i--) {
        var frame = this.keyframes[i];
        if (isNaN(frame.start)) { frame.start = 0; frame.end = 0; }
        if (t >= frame.start) {
            last = (i > 0 ? this.keyframes[i - 1] : frame);
            //normalize the time between frames
            var time;
            if (frame.end === frame.start) {
                time = 1;
            }
            else {
                time = t === 0 ? 0 : (t - frame.start) / (frame.end - frame.start);
            }
            this.updateFunc(this.id, last, frame, time);
            return;
        }
    }
}
Animation.prototype.halt = function (properties) {
    var frame = this.keyframes[this.keyframes.length - 1];
    this.updateFunc && this.updateFunc(this.id, frame, frame, 1, properties);
}

/************************************************************
 * Linear interpolation
 ************************************************************/
function lerp(a, b, t) {
    return (b - a) * t + a;
}

/************************************************************
 * Clamping functions for what to do with values that go outside [0:1] to put them back inside.
 ************************************************************/
var clampingFunctions = {
    "clamp": function (t) { return Math.max(0, Math.min(1, t)); },  //everything after 1 is clamped to 1
    "wrap": function (t) { return t % 1.0; },                       //passing 1 wraps back to 0 (ex. 1.1 => 0.1)
    "mirror": function (t) { t %= 2.0; return t > 1 ? 2 - t : t; }, //bouncing back and forth from 0->1->0 (ex. 1.1 => 0.9, 2.1 => 0.1)
};

/************************************************************
 * Interpolation functions for animation movement interpolation
 ************************************************************/
var interpolationModes = {
    "none": function noInterpolation(prop, start, end, t, frames, index) {
        return t >= 1 ? end : start;

    },
    "linear": function linear(prop, start, end, t, frames, index) {
        return lerp(start, end, t);
    },
    "spline": function catmullrom(prop, start, end, t, frames, index) {
        var p0 = index > 0 ? frames[index - 1][prop] : start;
        var p1 = start;
        var p2 = end;
        var p3 = index < frames.length - 1 ? frames[index + 1][prop] : end;

        if (typeof p0 === "undefined" || isNaN(p0)) {
            p0 = start;
        }
        if (typeof p3 === "undefined" || isNaN(p3)) {
            p3 = end;
        }

        var a = 2 * p1;
        var b = p2 - p0;
        var c = 2 * p0 - 5 * p1 + 4 * p2 - p3;
        var d = -p0 + 3 * p1 - 3 * p2 + p3;

        var p = 0.5 * (a + (b * t) + (c * t * t) + (d * t * t * t));
        return p;
    },
};

/************************************************************
 * Converts a px or % value to the equivalent scene value
 ************************************************************/
function toSceneX(x, scene) {
    if (typeof x === "undefined") { return; }
    if ($.isNumeric(x)) { return parseInt(x, 10); }
    if (x.endsWith("%")) {
        return parseInt(x, 10) / 100 * scene.width;
    }
    else {
        return parseInt(x, 10);
    }
}

/************************************************************
 * Converts a px or % value to the equivalent scene value
 ************************************************************/
function toSceneY(y, scene) {
    if (typeof y === "undefined") { return; }
    if ($.isNumeric(y)) { return parseInt(y, 10); }
    if (y.endsWith("%")) {
        return parseInt(y, 10) / 100 * scene.height;
    }
    else {
        return parseInt(y, 10);
    }
}

/************************************************************
 * Return the numerical part of a string s. E.g. "20%" -> 20
 ************************************************************/
function getNumericalPart(s) {
    return parseFloat(s); //apparently I don't actually need to remove the % (or anything else) from the string before I do the conversion
}

/************************************************************
 * Return the approriate left: setting so that a text box of the specified width is centred
 * Assumes a % width
 ************************************************************/
function getCenteredPosition(width) {
    var w = getNumericalPart(width); //numerical value of the width
    var left = 50 - (w / 2); //centre of the text box is at the 50% position
    return left + "%";
}

/************************************************************
 * Load the Epilogue data for a character
 ************************************************************/

// This is just a list of all possible conditional attribute names.
var EPILOGUE_CONDITIONAL_ATTRIBUTES = [
    'alsoPlaying', 'playerStartingLayers', 'markers',
    'not-markers', 'any-markers', 'alsoplaying-markers',
    'alsoplaying-not-markers', 'alsoplaying-any-markers'
]

function loadEpilogueData(player) {
    if (!player || !player.xml) { //return an empty list if a character doesn't have an XML variable. (Most likely because they're the player.)
        return [];
    }

    var playerGender = humanPlayer.gender;

    //get the XML tree that relates to the epilogue, for the specific player gender
    //var epXML = $($.parseXML(xml)).find('epilogue[gender="'+playerGender+'"]'); //use parseXML() so that <image> tags come through properly //IE doesn't like this

    var epilogues = player.xml.children('epilogue').filter(function (index) {
        /* Returning true from this function adds the current epilogue to the list of selectable epilogues.
         * Conversely, returning false from this function will make the current epilogue not selectable.
         */
        var epilogue_status = $(this).attr('status');
        if (epilogue_status && !includedOpponentStatuses[epilogue_status]) {
            return false;
        }

        /* 'gender' attribute: the epilogue will only be selectable if the player character has the given gender, or if the epilogue is marked for 'any' gender. */
        var epilogue_gender = $(this).attr('gender');
        if (epilogue_gender && epilogue_gender !== playerGender && epilogue_gender !== 'any') {
            // if the gender doesn't match, don't make this epilogue selectable
            return false;
        }

        var alsoPlaying = $(this).attr('alsoPlaying');
        if (alsoPlaying
            && !alsoPlaying.trim().split(/\s+/).every(function(ap) {
                return players.some(function (p) { return p.id == ap; })
            })) {
            return false;
        }

        var playerStartingLayers = parseInterval($(this).attr('playerStartingLayers'));
        if (playerStartingLayers !== undefined && !inInterval(humanPlayer.startingLayers, playerStartingLayers)) {
            return false;
        }

        /* 'markers' attribute: the epilogue will only be selectable if the character has ALL markers listed within the attribute set. */
        var all_marker_attr = $(this).attr('markers');
        if (all_marker_attr
            && !all_marker_attr.trim().split(/\s+/).every(function (marker) {
                return checkMarker(marker, player);
            })) {
            // not every marker set
            return false;
        }

        /* 'not-markers' attribute: the epilogue will only be selectable if the character has NO markers listed within the attribute set. */
        var no_marker_attr = $(this).attr('not-markers');
        if (no_marker_attr
            && no_marker_attr.trim().split(/\s+/).some(function (marker) {
                return checkMarker(marker, player);
            })) {
            // some disallowed marker set
            return false;
        }

        /* 'any-markers' attribute: the epilogue will only be selectable if the character has at least ONE of the markers listed within the attribute set. */
        var any_marker_attr = $(this).attr('any-markers');
        if (any_marker_attr
            && !any_marker_attr.trim().split(/\s+/).some(function (marker) {
                return checkMarker(marker, player);
            })) {
            // none of the markers set
            return false;
        }

        /* 'alsoplaying-markers' attribute: this epilogue will only be selectable if ALL markers within the attribute are set for any OTHER characters in the game. */
        var alsoplaying_marker_attr = $(this).attr('alsoplaying-markers');
        if (alsoplaying_marker_attr
            && !alsoplaying_marker_attr.trim().split(/\s+/).every(function (marker) {
                return players.some(function (p) {
                    return p !== player && checkMarker(marker, p);
                });
            })) {
            // not every marker set by some other character
            return false;
        }

        /* 'alsoplaying-not-markers' attribute: this epilogue will only be selectable if NO markers within the attribute are set for other characters in the game. */
        var alsoplaying_not_marker_attr = $(this).attr('alsoplaying-not-markers');
        if (alsoplaying_not_marker_attr
            && alsoplaying_not_marker_attr.trim().split(/\s+/).some(function (marker) {
                return players.some(function (p) {
                    return p !== player && checkMarker(marker, p);
                });
            })) {
            // some disallowed marker set by some other character
            return false;
        }

        /* 'alsoplaying-any-markers' attribute: this epilogue will only be selectable if at least one marker within the attribute are set for any OTHER character in the game. */
        var alsoplaying_any_marker_attr = $(this).attr('alsoplaying-any-markers');
        if (alsoplaying_any_marker_attr
            && !alsoplaying_any_marker_attr.trim().split(/\s+/).some(function (marker) {
                return players.some(function (p) {
                    return p !== player && checkMarker(marker, p);
                });
            })) {
            // none of the markers set by any other player
            return false;
        }

        // if we made it this far the epilogue must be selectable
        return true;
    }).map(function (i, e) { return parseEpilogue(player, e); }).get();

    return epilogues;
}

var animatedProperties = ["x", "y", "rotation", "scalex", "scaley", "skewx", "skewy", "alpha", "src", "zoom", "color", "rate"];

function addDirectiveToScene(scene, directive) {
    switch (directive.type) {
    case "move":
    case "camera":
    case "fade":
        //split the directive into multiple directives so that all the keyframes affect the same properties
        //for instance, an animation with [frame1: {x:5, y:2}, frame2: {y:4}, frame3: {x:8, y:10}] would be split into two: [frame1: {x:5}, frame3: {x:8}] and [frame1: {y:2}, frame2: {y:4}, frame3: {y:10}]
        //this makes the tweening simple for properties that don't change in intermediate frames, because those intermediate frames are removed

        //first split the properties into buckets of frame indices where they appear
        var propertyMap = {};
        for (var i = 0; i < directive.keyframes.length; i++) {
            var frame = directive.keyframes[i];
            for (var j = 0; j < animatedProperties.length; j++) {
                var property = animatedProperties[j];
                if (frame.hasOwnProperty(property) && (property === "color" || !Number.isNaN(frame[property]))) {
                    if (!propertyMap[property]) {
                        propertyMap[property] = [];
                    }
                    propertyMap[property].push(i);
                }
            }
        }

        //next create directives for each combination of frames
        var directives = {};
        for (var prop in propertyMap) {
            var key = propertyMap[prop].join(',');
            var workingDirective = directives[key];
            if (!workingDirective) {
                //shallow copy the directive
                workingDirective = {};
                for (var srcProp in directive) {
                    if (directive.hasOwnProperty(srcProp)) {
                        workingDirective[srcProp] = directive[srcProp];
                    }
                }
                workingDirective.keyframes = [];
                directives[key] = workingDirective;
                scene.directives.push(workingDirective);
            }
            var lastStart = 0;
            for (var i = 0; i < propertyMap[prop].length; i++) {
                var srcFrame = directive.keyframes[propertyMap[prop][i]];
                var targetFrame;
                if (workingDirective.keyframes.length <= i) {
                    targetFrame = {
                        start: lastStart,
              end: srcFrame.end,
              interpolation: srcFrame.interpolation,              
                    };

                    workingDirective.keyframes.push(targetFrame);
                    workingDirective.time = srcFrame.end;
                    lastStart = srcFrame.end;
                }
                else {
                    targetFrame = workingDirective.keyframes[i];
                }
                targetFrame[prop] = srcFrame[prop];
            }
        }
        break;
    default:
        scene.directives.push(directive);
    }
}

function parseEpilogue(player, rawEpilogue) {
    //use parseXML() so that <image> tags come through properly
    //not using parseXML() because internet explorer doesn't like it

    if (!rawEpilogue) {
        return;
    }

    player.markers = player.markers || {}; //ensure markers collection exists in the gallery even though they'll be empty

    var $epilogue = $(rawEpilogue);
    var title = $epilogue.children("title").html().trim();
    var gender = $epilogue.attr("gender") || 'any';
    var allowSceneSkip = $epilogue.attr("allowSceneSkip") == 'true';

    var markers = [];
    $epilogue.children("markers").children("marker").each(function() {
        var $elem = $(this);
        var markerOp = parseMarkerXML(this, null);

        /* By default, execute marker ops only when playing from the game end modal. */
        var run_on = $elem.attr('when');

        switch (run_on) {
        case "game":
        default:
            markerOp.from_game = true;
            markerOp.from_gallery = false;
            break;
        case "gallery":
            markerOp.from_game = false;
            markerOp.from_gallery = true;
            break;
        case "always":
            markerOp.from_game = true;
            markerOp.from_gallery = true;
            break;
        }

        markers.push(markerOp);
    });

    var epilogue = {
        title: title,
        player: player,
        gender: gender,
        scenes: [],
        markers: markers,
        allowSceneSkip: allowSceneSkip,
    };
    var scenes = epilogue.scenes;

    //determine what type of epilogue this is and parse accordingly
    var isLegacy = $epilogue.children("screen").length > 0;
    if (isLegacy) {
        parseLegacyEpilogue(player, epilogue, $epilogue);
    }
    else {
        var scene;
        $epilogue.children("scene").each(function (index, rawScene) {
            var $scene = $(rawScene);
            if ($scene.attr("transition")) {
                if (scenes.length === 0) {
                    //add a blank scene to transition from
                    scene = {
                        color: $scene.attr("color"),
                        directives: [],
                    };
                    scenes.push(scene);
                }
                scene = scenes[scenes.length - 1];
                scene.transition = readProperties(rawScene, scene);
            }
            else {
                var width = parseInt($scene.attr("width"), 10);
                var height = parseInt($scene.attr("height"), 10);
                scene = {
                    name: $scene.attr("name") || null,
                    background: $scene.attr("background"),
                    width: width,
                    height: height,
                    aspectRatio: width / height,
                    zoom: parseFloat($scene.attr("zoom"), 10),
                    color: $scene.attr("color"),
                    overlayColor: $scene.attr("overlay"),
                    overlayAlpha: $scene.attr("overlay-alpha"),
                    directives: [],
                }
                scenes.push(scene);
                scene.x = toSceneX($scene.attr("x"), scene);
                scene.y = toSceneY($scene.attr("y"), scene);

                var directives = scene.directives;

                $scene.children("directive").each(function (i, item) {
                    var totalTime = 0;
                    var directive = readProperties(item, scene);
                    directive.keyframes = [];
                    $(item).children("keyframe").each(function (i2, frame) {
                        var keyframe = readProperties(frame, scene);
                        keyframe.ease = keyframe.ease || directive.ease;
                        keyframe.start = totalTime;
                        totalTime = Math.max(totalTime, keyframe.time);
                        keyframe.end = totalTime;
                        keyframe.interpolation = directive.interpolation || "linear";
                        directive.keyframes.push(keyframe);
                    });
                    if (directive.keyframes.length === 0) {
                        //if no keyframes were explicitly provided, use the directive itself as a keyframe
                        directive.start = 0;
                        directive.end = directive.time;
                        directive.keyframes.push(directive);
                    }
                    else {
                        directive.time = totalTime;
                    }

                    if (directive.marker && !checkMarker(directive.marker, player)) {
                        directive.type = "skip";
                    }
                    addDirectiveToScene(scene, directive);
                });
            }
        });

        //if the last scene has a transition, add a dummy scene to the end
        if (scenes.length > 0 && scenes[scenes.length - 1].transition) {
            scene = {
                color: scenes[scenes.length - 1].transition.color,
                directives: [],
            };
            scenes.push(scene);
        }
    }
    return epilogue;
}

/**
 * Parses an old screen-based epilogue and converts it into directive format
 */
function parseLegacyEpilogue(player, epilogue, $xml) {
    var scenes = epilogue.scenes;
    $xml.children("screen").each(function () {
        var $this = $(this);

        var image = $this.attr("img").trim();

        //create a scene for each screen
        var scene = {
            directives: [],
            background: image,
        };
        scenes.push(scene);
        parseSceneContent(player, scene, $this);
    });
}

function parseSceneContent(player, scene, $scene) {
    var directive;
    var backgroundTransform = [$scene.attr('background-position-x'), $scene.attr('background-position-y'), $scene.attr('background-zoom') || 100];
    var addedPause = false;
    try {
        scene.x = toSceneX(backgroundTransform[0], scene);
        scene.y = toSceneY(backgroundTransform[1], scene);
        scene.zoom = parseFloat(backgroundTransform[2]) / 100;
    } catch (e) { }

    // Find the image data for this shot
    $scene.find('sprite').each(function () {
        var x = $(this).find("x").html().trim();
        var y = $(this).find("y").html().trim();
        var width = $(this).find("width").html().trim();
        var src = $(this).find('src').html().trim();

        var css = $(this).attr('css');

        directive = {
            type: "sprite",
            id: "obj" + (epilogueSuffix++),
            x: toSceneX(x, scene),
            y: toSceneY(y, scene),
            width: width,
            src: src,
            css: css,
        }
        scene.directives.push(directive);

    });

    //get the information for all the text boxes
    $scene.find("text").each(function () {

        //the text box's position and width
        var x = $(this).find("x").html().trim();
        var y = $(this).find("y").html().trim();
        var w = $(this).find("width").html();
        var a = $(this).find("arrow").html();

        //the width component is optional. Use a default of 20%.
        if (w) {
            w = w.trim();
        }
        if (!w || (w.length <= 0)) {
            w = "20%"; //default to text boxes having a width of 20%
        }

        //dialogue bubble arrow
        if (a) {
            a = a.trim().toLowerCase();
            if (a.length >= 1) {
                a = "arrow-" + a; //class name for the different arrows. Only use if the writer specified something.
            }
        } else {
            a = "";
        }

        //automatically centre the text box, if the writer wants that.
        if (x && x.toLowerCase() == "centered") {
            x = getCenteredPosition(w);
        }

        var text = fixupDialogue($(this).find("content").html().trim()); //the actual content of the text box

        var css = $(this).attr('css');

        directive = {
            type: "text",
            id: "obj" + (epilogueSuffix++),
            x: x,
            y: y,
            arrow: a,
            width: w,
            text: text,
            css: css,
        }
        scene.directives.push(directive);
        scene.directives.push({ type: "pause" });
        addedPause = true;
    });
    if (!addedPause) {
        scene.directives.push({ type: "pause" });
    }
}

/************************************************************
 * Read attributes from a source XML object and put them into properties of a JS object
 ************************************************************/
function readProperties(sourceObj, scene) {
    var targetObj = {};
    var $obj = $(sourceObj);
    $.each(sourceObj.attributes, function (i, attr) {
        var name = attr.name.toLowerCase();
        var value = attr.value;
        targetObj[name] = value;
    });

    //properties needing special handling
    targetObj.delay = parseFloat(targetObj.delay, 10) * 1000 || 0;

    if (targetObj.type !== "text") {
        // scene directives
        targetObj.time = parseFloat(targetObj.time, 10) * 1000 || 0;
        if (targetObj.alpha) { targetObj.alpha = parseFloat(targetObj.alpha, 10); }
        targetObj.zoom = parseFloat(targetObj.zoom, 10);
        targetObj.rotation = parseFloat(targetObj.rotation, 10);
        targetObj.angle = parseFloat(targetObj.angle, 10) || 0;
        if (targetObj.scale) {
            targetObj.scalex = targetObj.scaley = targetObj.scale;
        }
        targetObj.scalex = parseFloat(targetObj.scalex, 10);
        targetObj.scaley = parseFloat(targetObj.scaley, 10);
        targetObj.skewx = parseFloat(targetObj.skewx, 10);
        targetObj.skewy = parseFloat(targetObj.skewy, 10);
        if (targetObj.x) { targetObj.x = toSceneX(targetObj.x, scene); }
        if (targetObj.y) { targetObj.y = toSceneY(targetObj.y, scene); }
        targetObj.iterations = parseInt(targetObj.iterations) || 0;
        targetObj.rate = parseFloat(targetObj.rate, 10) || 0;
        targetObj.count = parseFloat(targetObj.count, 10) || 0;
    }
    else {
        // textboxes

        // ensure an ID
        var id = targetObj.id;
        if (!id) {
            targetObj.id = "obj" + (epilogueSuffix++);
        }

        // text (not from an attribute, so not populated automatically)
        targetObj.text = fixupDialogue($obj.html().trim());

        var w = targetObj.width;
        //the width component is optional. Use a default of 20%.
        if (w) {
            w = w.trim();
        }
        if (!w || (w.length <= 0)) {
            w = "20%"; //default to text boxes having a width of 20%
        }
        targetObj.width = w;

        //dialogue bubble arrow
        var a = targetObj.arrow; if (a) {
            a = a.trim().toLowerCase();
            if (a.length >= 1) {
                a = "arrow-" + a; //class name for the different arrows. Only use if the writer specified something.
            }
        } else {
            a = "";
        }
        targetObj.arrow = a;

        //automatically centre the text box, if the writer wants that.
        var x = targetObj.x;
        if (x && x.toLowerCase() == "centered") {
            targetObj.x = getCenteredPosition(w);
        }
    }
    return targetObj;
}

/************************************************************
 * Add the epilogue to the Epilogue modal
 ************************************************************/

function addEpilogueEntry(epilogue) {
    var num = epilogues.length; //index number of the new epilogue
    epilogues.push(epilogue);
    var player = epilogue.player;

    var nameStr = player.first + " " + player.last;
    if (player.first.length <= 0 || player.last.length <= 0) {
        nameStr = player.first + player.last; //only use a space if they have both first and last names
    }

    var epilogueTitle = nameStr + ": " + epilogue.title;
    var idName = 'epilogue-option-' + num;
    var clickAction = "selectEpilogue(" + num + ")";
    var unlocked = save.hasEnding(player.id, epilogue.title) ? " unlocked" : "";

    var htmlStr = '<li id="' + idName + '" class="epilogue-entry' + unlocked + '"><button onclick="' + clickAction + '">' + epilogueTitle + '</button></li>';

    $epilogueList.append(htmlStr);
    epilogueSelections.push($('#' + idName));
}

/************************************************************
 * Add entries to the epilogue modal for unavailable epilogues.
 ************************************************************/
function populateUnavailableEpilogues() {
    var unavailable = [];

    players.forEach(function (opp) {
        if (!opp || opp === humanPlayer || !opp.endings || opp.endings.length === 0) return;

        opp.endings.each(function() {
            var ending = $(this);
            var title = ending.text();
            
            /* Don't display unlisted epilogues */
            if (ending.attr('status') === "Unlisted") return;

            /* Skip any epilogues that share titles with available epilogues */
            if (epilogues.some(function (e) { return e.title === title; })) return;

            /* Don't duplicate unavailable epilogue entries */
            if (unavailable.some(function (e) { return e[1].text() === title })) return;

            unavailable.push([opp, ending]);
        });
    });

    var elems = unavailable.map(function (ent) {
        var player = ent[0];
        var endingMeta = ent[1];

        var nameStr = player.first + " " + player.last;
        if (player.first.length <= 0 || player.last.length <= 0) {
            nameStr = player.first + player.last; //only use a space if they have both first and last names
        }

        var epilogueTitle = nameStr + ": " + endingMeta.text();

        var elem = createElementWithClass('li', 'epilogue-entry unavailable');

        var btn = createElementWithClass('button', '');
        $(btn).prop('disabled', true).appendTo(elem);

        var titleElem = createElementWithClass('span', 'epilogue-title');
        $(titleElem).text(epilogueTitle).appendTo(btn);

        var hint = endingMeta.attr('hint');
        if (!hint) {
            var epGender = endingMeta.attr('gender');
            if (epGender && epGender !== 'any' && epGender !== humanPlayer.gender) {
                hint = 'Play as a '+epGender+'.';
            }
        }

        if (hint) {
            var hintElem = createElementWithClass('div', 'epilogue-hint');
            $(hintElem).text(hint).appendTo(elem);
        }

        return elem;
    });

    $epilogueList.append(elems);
}

/************************************************************
 * Clear the Epilogue modal
 ************************************************************/

function clearEpilogueList() {
    $epilogueHeader.html('');
    $epilogueList.html('');
    epilogues = [];
    epilogueSelections = [];
}

/************************************************************
 * Cleans up epilogue data
 ************************************************************/
function clearEpilogue() {
    if (epiloguePlayer) {
        epiloguePlayer.destroy();
        epiloguePlayer = null;
    }
}

/************************************************************
 * The user has clicked on a button to choose a particular Epilogue
 ************************************************************/

function selectEpilogue(epNumber) {
    chosenEpilogue = epilogues[epNumber]; //select the chosen epilogues

    for (var i = 0; i < epilogues.length; i++) {
        epilogueSelections[i].removeClass("active"); //make sure no other epilogue is selected
    }
    epilogueSelections[epNumber].addClass("active"); //mark the selected epilogue as selected
    $epilogueAcceptButton.prop("disabled", false); //allow the player to accept the epilogue
}

/************************************************************
 * Show the modal for the player to choose an Epilogue, or restart the game.
 ************************************************************/
function doEpilogueModal() {
    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            category: 'ui',
            message: 'Showing epilogue modal.',
            level: 'info'
        });
    }

    clearEpilogueList(); //remove any already loaded epilogues
    chosenEpilogue = null; //reset any currently-chosen epilogue
    $epilogueAcceptButton.prop("disabled", true); //don't let the player accept an epilogue until they've chosen one

    //whether or not the human player won
    var playerWon = !humanPlayer.out;

    if (EPILOGUES_ENABLED && playerWon) { //all the epilogues are for when the player wins, so don't allow them to choose one if they lost
        //load the epilogue data for each player
        players.forEach(function (p) {
            loadEpilogueData(p).forEach(addEpilogueEntry);
        });

        populateUnavailableEpilogues();
    }

    //are there any epilogues available for the player to see?
    var haveEpilogues = (epilogues.length >= 1); //whether or not there are any epilogues available
    $epilogueAcceptButton.css("visibility", haveEpilogues ? "visible" : "hidden");

    var randomTip = endingTips[getRandomNumber(0, endingTips.length)];
    $epilogueTip.html(randomTip);

    if (EPILOGUES_ENABLED) {
        //decide which header string to show the player. This describes the situation.
        var headerStr = '';
        if (playerWon) {
            headerStr = winStr; //player won, and there are epilogues available
            if (!haveEpilogues) {
                headerStr = winStrNone; //player won, but none of the NPCs have epilogues
            }
        } else {
            headerStr = lossStr; //player lost
        }
    } else {
        if (playerWon) {
            headerStr = winEpiloguesDisabledStr;
        } else {
            headerStr = lossEpiloguesDisabledStr;
        }
    }

    $epilogueHeader.html(headerStr); //set the header string
    $epilogueSelectionModal.modal({
        show: true,
        keyboard: false,
        backdrop: "static",
    });//show the epilogue selection modal
}

/************************************************************
 * Start the Epilogue
 ************************************************************/
function doEpilogue() {
    save.addEnding(chosenEpilogue.player.id, chosenEpilogue.title);

    /* Prevent players from trying to load an epilogue twice. */
    $epilogueAcceptButton.prop("disabled", true);

    /* Persistent marker forward declarations should already be loaded.
     * Execute any marker operations attached to this epilogue. 
     */
    chosenEpilogue.markers.forEach(function(markerOp) {
        if (markerOp.from_game) {
            markerOp.apply(chosenEpilogue.player, null);
        }
    });

    if (USAGE_TRACKING) {
        recordEpilogueEvent(false, chosenEpilogue);
    }

    loadEpilogue(chosenEpilogue);

    screenTransition($gameScreen, $epilogueScreen);
    $epilogueSelectionModal.modal("hide");
}

/************************************************************
 * Starts up an epilogue, pre-fetching all its images before displaying anything in order to handle certain computations that rely on the image sizes
 ************************************************************/
function loadEpilogue(epilogue, loadToScene, fromGallery) {
    $("#epilogue-spinner").show();
    epiloguePlayer = new EpiloguePlayer(epilogue, fromGallery);
    if (typeof loadToScene === "number") epiloguePlayer.hotReloadScene = loadToScene;

    epiloguePlayer.load();
    $epilogueButtons.toggleClass('nav-active', !!(fromGallery && epilogue.allowSceneSkip));

    if (DEBUG || fromGallery) {
        $epilogueSkipSelector.empty();
        for (var i = 0; i < epiloguePlayer.epilogue.scenes.length; i++) {
            var label = (i+1).toString();
            if (epiloguePlayer.epilogue.scenes[i].name) {
                label = label + ": " + epiloguePlayer.epilogue.scenes[i].name;
            }

            $epilogueSkipSelector.append($('<option>', {
                val: i,
                text: label
            }));
        }
    }

    updateEpilogueButtons();
}

function moveEpilogueForward(showRestartModalAtEnd) {
    if (epiloguePlayer && epiloguePlayer.loaded) {
        if (epiloguePlayer.hasMoreDirectives()) {
            if (SENTRY_INITIALIZED) {
                Sentry.addBreadcrumb({
                    category: 'epilogue',
                    message: 'Advancing epilogue from scene ' + epiloguePlayer.sceneIndex + ', directive' + epiloguePlayer.directiveIndex,
                    level: 'info'
                });
            }
            epiloguePlayer.advanceDirective();
            updateEpilogueButtons();
        } else if (showRestartModalAtEnd) {
            showRestartModal();
        }
    }
}

function moveEpilogueBack() {
    if (epiloguePlayer && epiloguePlayer.loaded && epiloguePlayer.hasPreviousDirectives()) {
        if (SENTRY_INITIALIZED) {
            Sentry.addBreadcrumb({
                category: 'epilogue',
                message: 'Reverting epilogue from scene ' + epiloguePlayer.sceneIndex + ', directive' + epiloguePlayer.directiveIndex,
                level: 'info'
            });
        }

        epiloguePlayer.revertDirective();
        updateEpilogueButtons();
    }
}

function skipToEpilogueScene () {
    var scene = parseInt($epilogueSkipSelector.val(), 10);

    if (isNaN(scene) || !epiloguePlayer || !epiloguePlayer.loaded) return;
    epiloguePlayer.skipToScene(scene);
    updateEpilogueButtons();
}

/* Reloads an epilogue by re-fetching behaviour.xml, and reparsing the
 * epilogue XML data.
 */
function hotReloadEpilogue () {
    if (!epiloguePlayer || !epiloguePlayer.loaded) return;

    $epilogueHotReloadBtn.attr('disabled', true);

    /* Make sure to save state from the old EpiloguePlayer that we will need later.
     */
    var curScene = epiloguePlayer.sceneIndex;
    var epilogueTitle = epiloguePlayer.epilogue.title;
    var epilogueGender = epiloguePlayer.epilogue.gender;
    var player = epiloguePlayer.epilogue.player;
    var fromGallery = epiloguePlayer.fromGallery;

    player.fetchBehavior()
    /* Success callback.
     * 'this' is bound to the Opponent object.
     */
        .then(function($xml) {
            var endingElem = null;

            $xml.children('epilogue').each(function () {
                if ($(this).children('title').html() === epilogueTitle && $(this).attr('gender') === epilogueGender) {
                    endingElem = this;
                }
            });

            epilogue = parseEpilogue(player, endingElem);

            /* Clean up the old EpiloguePlayer. */
            clearEpilogue();

            loadEpilogue(epilogue, curScene, fromGallery);
            $epilogueHotReloadBtn.attr('disabled', false);
        });
}

/************************************************************
 * Updates enabled state of buttons
 ************************************************************/

function updateEpilogueButtons() {
    if (!epiloguePlayer) {
        return;
    }
    var atStart = !epiloguePlayer.hasPreviousDirectives(),
        atEnd = !epiloguePlayer.hasMoreDirectives();

    $epiloguePrevButton.prop("disabled", atStart);
    $epilogueNextButton.prop("disabled", atEnd);
    $('#epilogue-end-indicator').toggle(atEnd);
    /* Force button bar visible at end of epilogue, un-force it if
     * going back, but avoid un-forcing it at the start. */
    if (!atStart) {
        $epilogueButtons.toggleClass('force-show', atEnd);
    }

    if (DEBUG) {
        $epilogueSkipSelector.val(
            epiloguePlayer.sceneIndex < 0 ? 0 : epiloguePlayer.sceneIndex
        );
    }
}

/************************************************************
 * Class for playing through an epilogue
 ************************************************************/
function EpiloguePlayer(epilogue, fromGallery) {
    this.resizeHandler = this.resizeViewport.bind(this);
    $(window).resize(this.resizeHandler);
    this.epilogue = epilogue;
    this.lastUpdate = performance.now();
    this.sceneIndex = -1;
    this.directiveIndex = -1;
    this.assetMap = {};
    this.loaded = false;
    this.loadingImages = 0;
    this.totalImages = 0;
    this.loadedImages = 0;
    this.waitingForAnims = false;
    this.views = [];
    this.viewIndex = 0;
    this.activeTransition = null;
    this.hotReloadScene = null;
    this.startingDirectiveIndex = 1;
    this.fromGallery = fromGallery; /* This is just to remember whether the epilogue was
                                       started from the gallery when hot-reloading.
                                       Sure, you'd have to have DEBUG on to do that, but
                                       pressing Q should only hide the reload button. */
}

EpiloguePlayer.prototype.load = function () {
    for (var i = 0; i < this.epilogue.scenes.length; i++) {
        var scene = this.epilogue.scenes[i];
        if (scene.background) {
            scene.background = scene.background.charAt(0) === '/' ? scene.background.substring(1) : this.epilogue.player.base_folder + scene.background;
            this.fetchImage(scene.background);
        }
        for (var j = 0; j < scene.directives.length; j++) {
            var directive = scene.directives[j];
            if (directive.src) {
                directive.src = directive.src.charAt(0) === '/' ? directive.src.substring(1) : this.epilogue.player.base_folder + directive.src;
                this.fetchImage(directive.src);
            }
            
            if (directive.keyframes) {
                for (var k = 0; k < directive.keyframes.length; k++) {
                    var keyframe = directive.keyframes[k];
                    if (keyframe.src && keyframe !== directive) {
                        keyframe.src = keyframe.src.charAt(0) === '/' ? keyframe.src.substring(1) : this.epilogue.player.base_folder + keyframe.src;
                        this.fetchImage(keyframe.src);
                    }
                }
            }
        }
    }
    this.readyToLoad = true;
    this.onLoadComplete();
}

/**
 * Called whenever all images being pre-fetched have been returned (which isn't necessarily when the total number of images that will be pre-fetched have been requested)
 * This is a workaround for IE11 not supporting promises
 */
EpiloguePlayer.prototype.onLoadComplete = function () {
    $("#epilogue-progress").text(Math.floor(this.loadedImages / Math.max(1, this.totalImages) * 100) + "%");
    if (this.loadingImages > 0) { return; }

    if (this.readyToLoad) {
        $("#epilogue-spinner").hide();
        var container = $("#epilogue-container");
        this.views.push(new SceneView(container, 0, this.assetMap));
        this.views.push(new SceneView(container, 1, this.assetMap));
        container.append($("<div id='scene-fade' class='epilogue-overlay' style='z-index: 10000'></div>")); //scene transition overlay
        this.loaded = true;
        $epilogueButtons.addClass('force-show');
        setTimeout(function() {
            $epilogueButtons.removeClass('force-show');
        }, 3000);
        
        if (
            typeof this.hotReloadScene === "number" &&
                this.hotReloadScene < this.epilogue.scenes.length
        ) {
            this.sceneIndex = this.hotReloadScene;
            this.setupScene(this.sceneIndex, true);
            this.hotReloadScene = null;

            /* We won't be able to set startingDirectiveIndex here.
             * It's not worth going through the trouble to remedy this, though, since it's such
             * a minor detail in the scheme of things.
             */
        } else {
            this.advanceScene();
            this.startingDirectiveIndex = this.directiveIndex;
        }
        updateEpilogueButtons();

        window.requestAnimationFrame(this.loop.bind(this));
    }
}

/**
 * Fetches an image asset ahead of time so it's ready before we need it
 * @param {string} path URL for image
 */
EpiloguePlayer.prototype.fetchImage = function (path) {
    var img = new Image();
    this.loadingImages++;
    this.totalImages++;
    var $this = this;
    img.onload = img.onerror = function () {
        $this.assetMap[path] = img;
        $this.loadingImages--;
        $this.loadedImages++;
        $this.onLoadComplete();
    };
    img.src = path;
}

EpiloguePlayer.prototype.destroy = function () {
    $(window).off('resize', this.resizeHandler);
    for (var i = 0; i < this.views.length; i++) {
        this.views[i].destroy();
    }

    $("#scene-fade").remove();

    EpiloguePlayer.prototype.layer = 0;
}

EpiloguePlayer.prototype.hasMoreDirectives = function () {
    return this.loaded && (this.sceneIndex < this.epilogue.scenes.length - 1 || this.directiveIndex < this.activeScene.directives.length - 1);
}

EpiloguePlayer.prototype.hasPreviousDirectives = function () {
    return this.loaded && (this.sceneIndex > 0 || this.directiveIndex > this.startingDirectiveIndex);
}

EpiloguePlayer.prototype.loop = function (timestamp) {
    var elapsed = timestamp - this.lastUpdate;

    if (this.activeTransition) {
        this.activeTransition.update(elapsed);
        if (this.activeTransition.isComplete()) {
            this.activeTransition = null;
        }
    }

    for (var i = 0; i < this.views.length; i++) {
        if (this.views[i].isActive()) {
            this.update(elapsed);
            this.draw();
            break;
        }
    }

    this.lastUpdate = timestamp;
    window.requestAnimationFrame(this.loop.bind(this));
}

EpiloguePlayer.prototype.update = function (elapsed) {
    var nonLoopingCount = 0;
    for (var i = 0; i < this.views.length; i++) {
        nonLoopingCount += this.views[i].update(elapsed);
    }
    if (nonLoopingCount === 0 && this.waitingForAnims) {
        this.advanceDirective();
    }
}

EpiloguePlayer.prototype.draw = function () {
    for (var i = 0; i < this.views.length; i++) {
        this.views[i].draw();
    }
}

/** Advances to the next scene if there is one */
EpiloguePlayer.prototype.advanceScene = function () {
    this.sceneIndex++;
    if (this.sceneIndex < this.epilogue.scenes.length) {
        this.setupScene(this.sceneIndex);
    }
}

EpiloguePlayer.prototype.layer = 0;

EpiloguePlayer.prototype.setupScene = function (index, skipTransition) {
    var lastScene = this.activeScene;

    this.lastUpdate = performance.now();
    this.activeScene = this.epilogue.scenes[index];

    var view = this.activeScene.view = this.views[this.viewIndex];
    this.viewIndex = (this.viewIndex + 1) % this.views.length;
    this.directiveIndex = -1;

    view.setup(this.activeScene, index, this.epilogue, skipTransition ? null : lastScene);

    //fit the viewport based on the scene's aspect ratio and the window size
    this.resizeViewport();

    //scene transition effect
    if (lastScene) {
        if (lastScene.transition && this.activeScene && !skipTransition) {
            this.activeTransition = new SceneTransition(lastScene.view, this.activeScene.view, lastScene.transition, $("#scene-fade"));
        }
        if (!this.activeTransition) {
            lastScene.view.cleanup();
        }
    }

    this.performDirective();
}

EpiloguePlayer.prototype.resizeViewport = function () {
    if (!this.activeScene) {
        return;
    }

    for (var i = 0; i < this.views.length; i++) {
        this.views[i].resize();
    }
    $(':root').css('font-size', (this.activeScene.view.viewportHeight / 75)+'px');

    $epilogueButtons.css('font-size', Math.max(window.innerHeight / 50,
                                               Math.min(30 * (window.devicePixelRatio || 1),
                                                        window.innerWidth / 20,
                                                        window.innerHeight / 12)) + 'px');
    this.draw();
}

EpiloguePlayer.prototype.advanceDirective = function () {
    if (this.activeTransition) { return; } //prevent advancing during a scene transition

    this.waitingForAnims = false;
    this.activeScene.view.haltAnimations(false);
    this.performDirective();
}

EpiloguePlayer.prototype.performDirective = function () {
    if (this.sceneIndex >= this.epilogue.scenes.length) { return; }
    
    this.directiveIndex++;
    if (this.directiveIndex < this.activeScene.directives.length) {
        var view = this.activeScene.view;
        var directive = this.activeScene.directives[this.directiveIndex];
        directive.action = null;
        if (directive.delay && directive.type !== "pause" && directive.type !== "wait") {
            view.pendDirective(this, directive, directive.delay);
        }
        else {
            if (view.runDirective(this, directive)) {
                return;
            }
        }
        this.performDirective();
    }
    else {
        this.advanceScene();
    }
}

/**
 * Reverts all changes up until the last "pause" directive
 */
EpiloguePlayer.prototype.revertDirective = function () {
    if (this.activeTransition) { return; }
    this.activeScene.view.haltAnimations(false);

    var canRevert = (this.sceneIndex > 0);
    if (!canRevert) {
        //on the initial scene, make sure there is a pause directive to revert to. Otherwise we can't rewind any further
        for (var i = this.directiveIndex - 1; i >= 0; i--) {
            if (this.activeScene.directives[i].type === "pause") {
                canRevert = true;
                break;
            }
        }
    }

    if (!canRevert) { return; }

    var currentIndex = this.directiveIndex;
    for (var i = currentIndex - 1; i >= 0; i--) {
        this.directiveIndex = i;
        var directive = this.activeScene.directives[i];
        if (directive.action) {
            directive.action.revert(directive, directive.action.context);
        }
        if (i < currentIndex - 1 && directive.type === "pause") {

            if (this.sceneIndex >= this.epilogue.scenes.length
                && this.activeScene === this.epilogue.scenes[this.epilogue.scenes.length-1]
               ) {
                /* Reverting to a directive on the last scene, so reset sceneIndex */
                this.sceneIndex = this.epilogue.scenes.length-1;
            }
            return;
        }
    }

    //reached the start of the scene, so time to back up an entire scene
    if (this.sceneIndex >= this.epilogue.scenes.length) {
        this.sceneIndex--; //the last scene had finished, so back up an extra time to move past that scene
    }

    //it would be better to make scene setup/teardown an undoable action, but for a quick and dirty method for now, just fast forward the whole scene to its last pause
    this.sceneIndex--;
    this.setupScene(this.sceneIndex, true);

    if (!this.activeTransition) {
        var pauseIndex;
        for (pauseIndex = this.activeScene.directives.length - 1; pauseIndex >= 0; pauseIndex--) {
            if (this.activeScene.directives[pauseIndex].type === "pause") {
                break;
            }
        }
        while (this.directiveIndex < pauseIndex) {
            this.advanceDirective();
        }
    }
}

/**
 * Skips to the start of a specific scene in an epilogue.
 * 
 * Intended only for use with Debug Mode.
 */
EpiloguePlayer.prototype.skipToScene = function (scene) {
    if (this.activeTransition) { return; }

    if (scene < 0) scene = 0;
    if (scene >= this.epilogue.scenes.length) scene = this.epilogue.scenes.length-1;

    this.waitingForAnims = false;
    this.activeScene.view.haltAnimations(false);
    this.sceneIndex = scene;
    
    this.setupScene(this.sceneIndex, true);
}

fromHex = function (hex) {
    var value = parseInt(hex.substring(1), 16);
    var r = (value & 0xff0000) >> 16;
    var g = (value & 0x00ff00) >> 8;
    var b = (value & 0x0000ff);
    return [r, g, b];
}

toHexPiece = function (v) {
    var hex = Math.round(v).toString(16);
    if (hex.length < 2) {
        hex = "0" + hex;
    }
    return hex;
}

toHex = function (rgb) {
    return "#" + this.toHexPiece(rgb[0]) + this.toHexPiece(rgb[1]) + this.toHexPiece(rgb[2]);
}

EpiloguePlayer.prototype.awaitAnims = function (directive, context) {
    for (var i = 0; i < this.views.length; i++) {
        if (this.views[i].isAnimRunning()) {
            this.waitingForAnims = true;
            return;
        }
    }
    this.advanceDirective();
}

function SceneView(container, index, assetMap) {
    this.scene = null;
    this.index = index;
    this.pendingDirectives = [];
    this.anims = [];
    this.camera = null;
    this.assetMap = assetMap;
    this.sceneObjects = {};
    this.textObjects = {};
    this.viewportWidth = 0;
    this.viewportHeight = 0;
    this.particlePool = [];

    var viewport = this.$viewport = $("<div id='epilogue-viewport" + index + "' class='epilogue-viewport'></div>");
    this.$canvas = $("<div id='epilogue-canvas" + index + "' class='epilogue-canvas'></div>");
    viewport.append(this.$canvas);
    this.$overlay = $("<div id='epilogue-overlay" + index + "' class='epilogue-overlay'></div>");
    viewport.append(this.$overlay);
    this.$textContainer = $("<div id='epilogue-content" + index + "' class='epilogue-content'></div>");
    viewport.append(this.$textContainer);
    this.overlay = { rgb: [0, 0, 0], a: 0 };
    container.append(this.$viewport);
    viewport.hide();
}

SceneView.prototype.cleanup = function () {
    this.haltAnimations(true);

    //clear old textboxes
    this.$textContainer.empty();
    this.textObjects = {};

    //clear old images
    for (var obj in this.sceneObjects) {
        this.sceneObjects[obj].destroy();
    }
    this.sceneObjects = {};

    //hide until needed again
    this.$viewport.hide();
}

SceneView.prototype.destroy = function () {
    this.cleanup();

    this.particlePool = null;
    this.$viewport.remove();
}

SceneView.prototype.runDirective = function (epiloguePlayer, directive) {
    switch (directive.type) {
    case "sprite":
        this.addAction(directive, this.addSprite.bind(this), this.removeSceneObject.bind(this));
        break;
    case "text":
        this.addAction(directive, this.addText.bind(this), this.removeText.bind(this));
        break;
    case "clear":
        this.addAction(directive, this.clearText.bind(this), this.restoreText.bind(this));
        break;
    case "clear-all":
        this.addAction(directive, this.clearAllText.bind(this), this.restoreText.bind(this));
        break;
    case "move":
        this.addAction(directive, this.moveSprite.bind(this), this.returnSprite.bind(this));
        break;
    case "camera":
        this.addAction(directive, this.moveCamera.bind(this), this.returnCamera.bind(this));
        break;
    case "fade":
        this.addAction(directive, this.fade.bind(this), this.restoreOverlay.bind(this));
        break;
    case "stop":
        this.addAction(directive, this.stopAnimation.bind(this), this.restoreAnimation.bind(this));
        break;
    case "wait":
        this.addAction(directive, epiloguePlayer.awaitAnims.bind(epiloguePlayer), function () { });
        return true;
    case "pause":
        return true;
    case "remove":
        this.addAction(directive, this.hideSceneObject.bind(this), this.showSceneObject.bind(this));
        break;
    case "emitter":
        this.addAction(directive, this.addEmitter.bind(this), this.removeSceneObject.bind(this));
        break;
    case "emit":
        this.addAction(directive, this.burstParticles.bind(this), this.clearParticles.bind(this));
        break;
    case "skip":
        this.addAction(directive, function () { }, function () { });
        break;
    }
    return false;
}

/**
 * Adds an undoable action to the history
 * @param {any} context Context to pass to do and undo functions
 * @param {Function} doFunc Function to perform the directive
 * @param {Function} undoFunc Function to undo the directive
 */
SceneView.prototype.addAction = function (directive, doFunc, undoFunc) {
    var context = {}; //contextual information for the do action to store off that the revert action can refer to
    var action = { directive: directive, context: context, perform: doFunc, revert: undoFunc };
    directive.action = action;
    action.perform(directive, context);
}

SceneView.prototype.pendDirective = function (epiloguePlayer, directive, delay) {
    var info = { epiloguePlayer: epiloguePlayer, directive: directive };
    info.handle = setTimeout(this.runPendedDirective.bind(this), delay, info, true);
    this.pendingDirectives.push(info);
}

SceneView.prototype.runPendedDirective = function (info, remove) {
    info.handle = 0;
    this.runDirective(info.epiloguePlayer, info.directive);
    if (remove) {
        var index = this.pendingDirectives.indexOf(info);
        this.pendingDirectives.splice(index, 1);
    }
}

SceneView.prototype.updateOverlay = function (id, last, next, t, properties) {
    var rgb = this.overlay.rgb;
    var alpha = this.overlay.a;
    if (!properties || properties.has("color")) {
        if (typeof next.color !== "undefined") {
            var rgb1 = fromHex(last.color);
            var rgb2 = fromHex(next.color);

            rgb = [0, 0, 0];
            for (var i = 0; i < rgb.length; i++) {
                rgb[i] = lerp(rgb1[i], rgb2[i], t);
            }
        }
    }
    if (!properties || properties.has("alpha")) {
        alpha = lerp(last.alpha, next.alpha, t);
        if (isNaN(alpha)) {
            alpha = this.overlay.a;
        }
    }
    this.setOverlay(rgb, alpha);
}

SceneView.prototype.setOverlay = function (color, alpha) {
    if (typeof color !== "undefined") {
        this.overlay.rgb = color;
    }
    this.overlay.a = alpha;
    this.$overlay.css({
        "opacity": alpha / 100,
        "background-color": toHex(this.overlay.rgb)
    });
}

SceneView.prototype.isActive = function () {
    if (this.anims.length > 0) {
        return true;
    }
    for (var obj in this.sceneObjects) {
        var sceneObj = this.sceneObjects[obj];
        if (sceneObj instanceof Emitter && (sceneObj.activeParticles.length > 0 || sceneObj.rate > 0)) {
            return true;
        }
    }
    return false;
}

SceneView.prototype.update = function (elapsed) {
    var nonLoopingCount = this.pendingDirectives.length;

    for (var obj in this.sceneObjects) {
        this.sceneObjects[obj].update(elapsed);
    }

    for (var i = this.anims.length - 1; i >= 0; i--) {
        var anim = this.anims[i];
        anim.update(elapsed);
        if (anim.isComplete()) {
            this.anims.splice(i, 1);
        }
        else {
            if (!anim.looped) {
                nonLoopingCount++;
            }
        }
    }
    return nonLoopingCount;
}

SceneView.prototype.draw = function () {
    for (var obj in this.sceneObjects) {
        this.sceneObjects[obj].draw();
    }
}

SceneView.prototype.drawObject = function (obj) {
    if (!obj.element) { return; }
    var properties = [];
    if (obj.parent) {
        properties.push("translate(" + obj.x + "px, " + obj.y + "px)");
    }
    else {
        properties.push("scale(" + this.viewportWidth / this.scene.width * this.camera.zoom + ")");
        properties.push("translate(" + this.toViewX(obj.x) + ", " + this.toViewY(obj.y) + ")");
    }
    var transform = properties.join(" ");

    $(obj.element).css({
        "transform": transform,
        "transform-origin": "top left",
        "opacity": obj.alpha / 100,
    });
    $(obj.rotElement).css({
        "transform": "rotate(" + obj.rotation + "deg) scale(" + obj.scalex + ", " + obj.scaley + ") skew(" + obj.skewx + "deg, " + obj.skewy + "deg)",
    });
}

SceneView.prototype.toViewX = function (x) {
    var sceneWidth = this.camera.width;
    var offset = sceneWidth / this.camera.zoom / 2 - sceneWidth / 2 + x - this.camera.x;
    return offset + "px";
}

SceneView.prototype.toViewY = function (y) {
    var sceneHeight = this.camera.height;
    var offset = sceneHeight / this.camera.zoom / 2 - sceneHeight / 2 + y - this.camera.y;
    return offset + "px";
}

SceneView.prototype.setup = function (scene, sceneIndex, epilogue, lastScene) {
    this.scene = scene;

    //copy the overlay values from the previous scene
    if (lastScene) {
        this.setOverlay(lastScene.view.overlay.rgb, lastScene.view.overlay.a);
    }
    else {
        //otherwise clear them completely
        this.setOverlay([0, 0, 0], 0);
    }

    if (!scene.width) {
        //if no scene dimensions were provided, use the background image's dimensions
        var backgroundImg = this.assetMap[scene.background];
        if (backgroundImg) {
            scene.width = backgroundImg.naturalWidth;
            scene.height = backgroundImg.naturalHeight;
            scene.aspectRatio = backgroundImg.naturalWidth / backgroundImg.naturalHeight;

            //backwards compatibility: for really skinny ratios, we probably don't want to use it since it'll make textboxes really squished. Use the first scene's instead
            if (sceneIndex > 0) {
                var previousScene = epilogue.scenes[0];
                if (scene.aspectRatio < 0.5) {
                    scene.width = previousScene.width;
                    scene.height = previousScene.height;
                    scene.aspectRatio = previousScene.aspectRatio;
                }
            }
        }
    }

    this.camera = {
        x: isNaN(scene.x) ? 0 : toSceneX(scene.x, scene),
        y: isNaN(scene.y) ? 0 : toSceneY(scene.y, scene),
        width: scene.width,
        height: scene.height,
        zoom: scene.zoom || 1,
    }

    this.initOverlay(scene.overlayColor, scene.overlayAlpha);

    if (scene.background) {
        this.addBackground(scene.background);
    }
    this.$viewport.css({
        "background-color": scene.color,
        "z-index": EpiloguePlayer.prototype.layer++,
    });
    this.$viewport.show();
}

SceneView.prototype.initOverlay = function (rgb, a) {
    var alpha;
    if (!this.overlay.rgb) {
        this.setOverlay([0, 0, 0], 0);
    }
    if (a) {
        alpha = parseInt(a, 10);
        if (typeof alpha === "undefined") {
            alpha = 100;
        }
    }
    else {
        alpha = this.overlay.a || 0;
    }
    if (rgb) {
        this.setOverlay(fromHex(rgb), alpha);
    }
}

SceneView.prototype.resize = function () {
    if (!this.scene) {
        return;
    }
    var windowHeight = $(window).height();
    var windowWidth = $(window).width();

    var viewWidth = this.scene.aspectRatio * windowHeight;
    var width = viewWidth;
    var height = windowHeight;
    if (viewWidth > windowWidth) {
        //take full width of window
        width = windowWidth;
        height = windowWidth / this.scene.aspectRatio;
    }

    width = Math.ceil(width);
    height = Math.ceil(height);
    this.viewportWidth = width;
    this.viewportHeight = height;
    this.$viewport.width(width);
    this.$viewport.height(height);

    for (var id in this.textObjects) {
        var box = this.textObjects[id];
        var directive = box.data("directive");
        this.applyTextDirective(directive, box);
    }
}

SceneView.prototype.haltAnimations = function (haltLooping) {
    for (var i = 0; i < this.pendingDirectives.length; i++) {
        var info = this.pendingDirectives[i];
        if (info.handle) {
            clearTimeout(info.handle);
            this.runPendedDirective(info, false);
        }
    }
    this.pendingDirectives = [];

    var animloop = this.anims.slice();
    var j = 0;
    for (var i = 0; i < animloop.length; i++) {
        if (haltLooping || !animloop[i].looped) {
            animloop[i].halt();
            this.anims.splice(j, 1);
        }
        else {
            j++;
        }
    }
    this.draw();
}

SceneView.prototype.addBackground = function (background) {
    var img = this.assetMap[background];
    this.addImage("background", background, { x: 0, y: 0, width: img.naturalWidth + "px", height: img.naturalHeight + "px" });
}

SceneView.prototype.addImage = function (id, src, args) {
    var img = document.createElement("img");
    img.className = "sprite";
    if (src) {
        img.src = this.assetMap[src].src;
        args.naturalWidth = args.naturalWidth || this.assetMap[src].naturalWidth;
        args.naturalHeight = args.naturalHeight || this.assetMap[src].naturalHeight;
    }

    var obj = new SceneObject(id, img, this, args);
    obj.setImage(src);
    this.addSceneObject(obj);
}

SceneView.prototype.addSprite = function (directive) {
    this.addImage(directive.id, directive.src, directive);
}

SceneView.prototype.addSceneObject = function (obj) {
    /* Don't make duplicate scene objects */
    if (this.sceneObjects[obj.id]) return;
    
    this.sceneObjects[obj.id] = obj;
    if (obj.element) {
        if (obj.parentid) {
            obj.parent = this.sceneObjects[obj.parentid];
            if (obj.parent) {
                obj.parent.rotElement.appendChild(obj.element);
            }
        }
        else {
            this.$canvas.append(obj.element);
        }
    }
    this.draw();
}

SceneView.prototype.removeSceneObject = function (directive) {
    /* If this object has already been deleted, our work here is done */
    if (!this.sceneObjects[directive.id]) return;

    this.sceneObjects[directive.id].destroy();
    delete this.sceneObjects[directive.id];
}

SceneView.prototype.hideSceneObject = function (directive, context) {
    var sceneObject = context.object = this.sceneObjects[directive.id];
    context.anims = {};
    if (context.object) {
        $(context.object.element).hide();
        this.stopAnimation(directive, context.anims);
        context.rate = sceneObject.rate;
        sceneObject.rate = 0;
        if (!sceneObject instanceof Emitter) {
            delete this.sceneObjects[directive.id];
        }
    }
}

SceneView.prototype.showSceneObject = function (directive, context) {
    var obj = context.object;
    if (obj) {
        this.sceneObjects[directive.id] = obj;
        this.sceneObjects[directive.id].rate = context.rate;
        this.restoreAnimation(directive, context.anims);
        $(obj.element).show();
    }
}

SceneView.prototype.addText = function (directive, context) {
    var id = directive.id;
    context.id = id;
    this.lastTextId = id;

    var box = this.textObjects[id];
    if (box) {
        //reuse the DOM element if one of the same ID already exists
        context.oldDirective = box.data("directive");
    }
    else {
        box = $('<div>', { 'class': 'bordered dialogue-bubble' });
        //attach new div element to the content div
        this.$textContainer.append(box[0]);
        box.data("id", id);
        this.textObjects[id] = box;
    }
    this.applyTextDirective(directive, box);
}

SceneView.prototype.removeText = function (directive, context) {
    this.lastTextId = context.id;
    var box = this.textObjects[directive.id];
    if (context.oldDirective) {
        this.applyTextDirective(context.oldDirective, box);
    }
    else {
        this.$textContainer.get(0).removeChild(box[0]);
        delete this.textObjects[directive.id];
    }
}

SceneView.prototype.applyTextDirective = function (directive, box) {
    var content = expandDialogue(directive.text, null, humanPlayer);

    box.empty().append($('<span>', { html: content }));
    box.removeClass('arrow-down arrow-left arrow-right arrow-up').addClass(directive.arrow);
    box.attr('style', directive.css);

    //use css to position the box
    box.css('left', directive.x);
    box.css('top', directive.y);
    box.css('width', directive.width);

    var arrowHeight = (directive.arrow === "arrow-up" || directive.arrow === "arrow-down" ? 15 : 0);
    var arrowWidth = (directive.arrow === "arrow-left" || directive.arrow === "arrow-right" ? 15 : 0);
    switch (directive.alignmenty) {
    case "center":
        var height = box.height() + arrowHeight;
        var top = box.position().top;
        box.css("top", (top - height / 2) + "px");
        break;
    case "bottom":
        var height = box.height() + arrowHeight;
        var top = box.position().top;
        box.css("top", (top - height) + "px");
        break;
    }
    switch (directive.alignmentx) {
    case "center":
        var width = box.width() + arrowWidth;
        var left = box.position().left;
        box.css("left", (left - width / 2) + "px");
        break;
    case "right":
        var width = box.width() + arrowWidth;
        var left = box.position().left;
        box.css("left", (left - width) + "px");
        break;
    }

    box.data("directive", directive);
}

SceneView.prototype.clearAllText = function (directive, context) {
    var $this = this;
    context = context || {};
    context.boxes = context.boxes || [];
    
    for (var box in this.textObjects) {
        this.clearText({ id: this.textObjects[box].data("id") }, context, true);
    }
    this.textObjects = {};
}

SceneView.prototype.clearText = function (directive, context, keepObject) {
    context.boxes = context.boxes || [];
    var boxContext = {};

    var id = directive.id || this.lastTextId;
    boxContext.id = lastTextId = id;
    var box = this.textObjects[id];

    if (!box) {
        return;
    }

    boxContext.directive = box.data("directive");
    context.boxes.push(boxContext);
    this.$textContainer.get(0).removeChild(box[0]);
    if (!keepObject) {
        delete this.textObjects[id];
    }
}

SceneView.prototype.restoreText = function (directive, context) {
    for (var i = 0; i < context.boxes.length; i++) {
        var boxContext = context.boxes[i];
        var id = this.lastTextId = boxContext.id;
        var directive = boxContext.directive;
        directive.id = id;
        this.addText(directive, {});
    }
}

SceneView.prototype.interpolate = function (obj, prop, last, next, t, mode) {
    var current = obj[prop];
    var start = last[prop];
    var end = next[prop];
    if (mode !== "none" && (typeof start === "undefined" || isNaN(start) || typeof end === "undefined" || isNaN(end))) {
        return;
    }
    mode = mode || next.interpolation || "linear";
    obj[prop] = interpolationModes[mode](prop, start, end, t, last.keyframes, last.index);
}

SceneView.prototype.updateObject = function (id, last, next, t, properties) {
    var obj = this.sceneObjects[id];
    obj.interpolateProperties(last, next, t, properties);
}

SceneView.prototype.addAnimation = function (anim) {
    this.anims.push(anim);
    return anim;
}

SceneView.prototype.moveSprite = function (directive, context) {
    var sprite = this.sceneObjects[directive.id];
    if (sprite) {
        var frames = directive.keyframes.slice();
        context.x = sprite.x;
        context.y = sprite.y;
        context.rotation = sprite.rotation;
        context.scalex = sprite.scalex;
        context.scaley = sprite.scaley;
        context.skewx = sprite.skewx;
        context.skewy = sprite.skewy;
        context.alpha = sprite.alpha;
        context.src = sprite.src;
    context.rate = sprite.rate;
        frames.unshift(context);
        context.anim = this.addAnimation(new Animation(directive.id, frames, this.updateObject.bind(this), directive.loop, directive.ease, directive.clamp, directive.iterations));
    }
}

SceneView.prototype.returnSprite = function (directive, context) {
    var sprite = this.sceneObjects[directive.id];
    if (sprite) {
        if (typeof context.x !== "undefined") {
            sprite.x = context.x;
        }
        if (typeof context.y !== "undefined") {
            sprite.y = context.y;
        }
        if (typeof context.rotation !== "undefined") {
            sprite.rotation = context.rotation;
        }
        if (typeof context.scalex !== "undefined") {
            sprite.scalex = context.scalex;
        }
        if (typeof context.scaley !== "undefined") {
            sprite.scaley = context.scaley;
        }
        if (typeof context.skewx !== "undefined") {
            sprite.skewx = context.skewx;
        }
        if (typeof context.skewy !== "undefined") {
            sprite.skewy = context.skewy;
        }
        if (typeof context.alpha !== "undefined") {
            sprite.alpha = context.alpha;
        }
        if (typeof context.src !== "undefined") {
            sprite.setImage(context.src);
        }
        this.removeAnimation(context.anim);
        this.draw();
    }
}

SceneView.prototype.removeAnimation = function (anim) {
    if (anim) {
        var index = this.anims.indexOf(anim);
        if (index >= 0) {
            this.anims.splice(index, 1);
        }
    }
}

SceneView.prototype.updateCamera = function (id, last, next, t, properties) {
    if (!properties || properties.has("x")) {
        this.interpolate(this.camera, "x", last, next, t);
    }
    if (!properties || properties.has("y")) {
        this.interpolate(this.camera, "y", last, next, t);
    }
    if (!properties || properties.has("zoom")) {
        if (last.zoom && next.zoom) {
            this.camera.zoom = lerp(last.zoom, next.zoom, t);
        }
    }
}

SceneView.prototype.moveCamera = function (directive, context) {
    var frames = directive.keyframes.slice();
    context.x = this.camera.x;
    context.y = this.camera.y;
    context.zoom = this.camera.zoom;
    frames.unshift(context);
    context.anim = this.addAnimation(new Animation("camera", frames, this.updateCamera.bind(this), directive.loop, directive.ease, directive.clamp, directive.iterations));
}

SceneView.prototype.returnCamera = function (directive, context) {
    if (typeof context.x !== "undefined") {
        this.camera.x = context.x;
    }
    if (typeof context.y !== "undefined") {
        this.camera.y = context.y;
    }
    if (context.zoom) {
        this.camera.zoom = context.zoom;
    }
    this.removeAnimation(context.anim);
    this.draw();
}

SceneView.prototype.fade = function (directive, context) {
    var color = toHex(this.scene.view.overlay.rgb);
    var frames = directive.keyframes.slice();
    context.color = color;
    context.alpha = this.scene.view.overlay.a;
    frames.unshift(context);
    context.anim = this.addAnimation(new Animation("fade", frames, this.updateOverlay.bind(this), directive.loop, directive.ease, directive.clamp, directive.iterations));
}

SceneView.prototype.restoreOverlay = function (directive, context) {
    this.setOverlay(context.color, context.alpha);
    this.removeAnimation(context.anim);
}

SceneView.prototype.isAnimRunning = function () {
    if (this.pendingDirectives.length > 0) {
        return true;
    }
    for (var i = 0; i < this.anims.length; i++) {
        if (!this.anims[i].looped) {
            return true;
        }
    }
    return false;
}

SceneView.prototype.stopAnimation = function (directive, context) {
    var anim;
    var id = directive.id;
    var properties = directive.properties ? directive.properties.split(',') : [];
    var propertySet = null;
    if (properties.length > 0) {
        propertySet = new Set();
        properties.forEach(function (prop) { propertySet.add(prop); });
    }

    context.haltedAnims = [];
    context.oldFrames = new Map();
    for (var i = this.anims.length - 1; i >= 0; i--) {
        anim = this.anims[i];
        if (anim.id === id) {
            if (propertySet) {
                context.oldFrames.set(anim, anim.keyframes);
                anim.halt(propertySet);
                anim.keyframes = Animation.prototype.cloneFrames(anim.keyframes);
                var stillHasProperties = false;
                anim.keyframes.forEach(function(frame) {
                    //remove the halted properties
                    propertySet.forEach(function (prop) { delete frame[prop]; });
                    //see if there are any other animatable properties remaining
                    for (var prop in frame) {
                        if (frame.hasOwnProperty(prop) && animatedProperties.indexOf(prop) >= 0) {
                            stillHasProperties = true;
                            break;
                        }
                    }
                });

                if (!stillHasProperties) {
                    //just halt the whole thing instead
                    anim.keyframes = context.oldFrames.get(anim);
                    context.oldFrames.delete(anim);
                    anim.halt();
                    this.anims.splice(i, 1);
                }
            }
            else {
                anim.halt();
                this.anims.splice(i, 1);
            }
            context.haltedAnims.push(anim);
            this.draw();
        }
    }
}

SceneView.prototype.restoreAnimation = function (directive, context) {
    var haltedAnims = context.haltedAnims;
    for (var i = 0; i < haltedAnims.length; i++) {
        var anim = haltedAnims[i];
        var oldFrames = context.oldFrames.get(anim);
        if (oldFrames) {
            anim.keyframes = oldFrames;
        }
        else {
            anim.elapsed = 0;
            this.addAnimation(anim);
        }
    }
}

SceneView.prototype.addEmitter = function (directive, context) {
    var element;

    if (directive.src) {
        var srcImg = this.assetMap[directive.src];
        directive.width = directive.width || srcImg.naturalWidth;
        directive.height = directive.height || srcImg.naturalHeight;
    }

    this.addSceneObject(new Emitter(directive.id, element, this, directive, this.particlePool));
}

SceneView.prototype.burstParticles = function (directive, context) {
    var emitter = this.sceneObjects[directive.id];
    if (emitter && emitter.emit) {
        context.emitter = emitter;
        for (var i = 0; i < directive.count; i++) {
            emitter.emit();
        }
    }
}

SceneView.prototype.clearParticles = function (directive, context) {
    var emitter = context.emitter;
    if (emitter) {
        context.emitter = emitter;
        for (var i = 0; i < directive.count; i++) {
            emitter.killParticles();
        }
    }
}

function RandomParameter(startValue, endValue) {
    this.start = startValue;
    this.end = endValue;
}

RandomParameter.prototype = {
    get: function () {
        return lerp(this.start, this.end, Math.random());
    }
};

function RandomColor(startValue, endValue) {
    this.start = startValue;
    this.end = endValue;
}

RandomColor.prototype = {
    get: function () {
        var t = Math.random();
        return [lerp(this.start[0], this.end[0], t),
                lerp(this.start[1], this.end[1], t),
                lerp(this.start[2], this.end[2], t)];
    }
};

function TweenableParameter(startValue, endValue, ease) {
    this.start = startValue;
    this.end = endValue;
    this.value = this.start;
}

TweenableParameter.prototype = {
    tween: function (t) {
        this.value = lerp(this.start, this.end, t);
        return this.value;
    }
};

function TweenableColor(startValue, endValue) {
    this.start = startValue;
    this.end = endValue;
    this.value = this.start;
}

TweenableColor.prototype = {
    tween: function (t) {
        var value = [];
        value[0] = lerp(this.start[0], this.end[0], t);
        value[1] = lerp(this.start[1], this.end[1], t);
        value[2] = lerp(this.start[2], this.end[2], t);
        this.value = value;
        return value;
    }
};

function SceneObject(id, element, view, args) {
    var alpha = args.alpha;
    if (typeof alpha === "undefined") {
        alpha = 100;
    }

    this.tweenableProperties = ["x", "y", "rotation", "scalex", "scaley", "alpha", "skewx", "skewy"];
    this.id = id;
    this.x = args.x || 0;
    this.y = args.y || 0;
    this.scalex = args.scalex || 1;
    this.scaley = args.scaley || 1;
    this.skewx = args.skewx || 0;
    this.skewy = args.skewy || 0;
    this.rotation = args.rotation || 0;
    this.alpha = alpha;
    this.view = view;
    this.layer = args.layer;
    this.parentid = args.parent;

    var scene = this.view.scene;

    if (element) {
        var vehicle = document.createElement("div");
        vehicle.id = id;
        var pivot = document.createElement("div");
        vehicle.appendChild(pivot);
        pivot.appendChild(element);

        var pivotX = args.pivotx;
        var pivotY = args.pivoty;
        if (pivotX || pivotY) {
            pivotX = pivotX || "center";
            pivotY = pivotY || "center";
            $(pivot).css("transform-origin", pivotX + " " + pivotY);
        }
        if (this.layer) {
            $(vehicle).css("z-index", args.layer);
        }

        this.element = vehicle;
        this.rotElement = pivot;
        this.objElement = element;

        var width = args.width;
        var height = args.height;
        var naturalWidth = args.naturalWidth || element.naturalWidth || 100;
        var naturalHeight = args.naturalHeight || element.naturalHeight || 100;

        if (width) {
            if (width.endsWith("%")) {
                this.widthPct = parseInt(width, 10) / 100;
            }
            else {
                this.widthPct = parseInt(width, 10) / scene.width;
            }
            if (!height) {
                this.heightPct = naturalHeight / naturalWidth * this.widthPct * scene.aspectRatio;
            }
        }
        else {
            this.widthPct = naturalWidth / scene.width;
        }
        if (height) {
            if (height.endsWith("%")) {
                this.heightPct = parseInt(height, 10) / 100;
            }
            else {
                this.heightPct = parseInt(height, 10) / scene.height;
            }
            if (!width) {
                this.widthPct = naturalWidth / naturalHeight * this.heightPct / scene.aspectRatio;
            }
        }
        else if (!this.heightPct) {
            this.heightPct = naturalHeight / scene.height;
        }

        this.width = this.widthPct * scene.width;
        this.height = this.heightPct * scene.height;
        $(vehicle).css({
            position: "absolute",
            left: 0,
            top: 0,
            width: this.width,
            height: this.height,
        });
        $(element).css({
            width: this.width,
            height: this.height,
        });
    }
    else {
        this.width = parseInt(args.width, 10) || 10;
        this.height = parseInt(args.height, 10) || 10;
    }
}

SceneObject.prototype = {
    destroy: function () {
        if (this.element) {
            $(this.element).remove();
        }
    },

    update: function (elapsedMs) {
    },

    draw: function () {
        this.view.drawObject(this);
    },

    interpolateProperties: function (last, next, t, properties) {
        for (var i = 0; i < this.tweenableProperties.length; i++) {
            var prop = this.tweenableProperties[i];
            if (!properties || properties.has(prop)) {
                this.view.interpolate(this, this.tweenableProperties[i], last, next, t);
            }
        }

        if (!properties || properties.has("src")) {
            if (next.src) {
                var oldSrc = this.src;
                this.view.interpolate(this, "src", last, next, t, "none");
                if (oldSrc !== this.src) {
                    this.setImage(this.src);
                }
            }
        }
    },

    setImage: function (src) {
        if (!src) return;

        this.objElement.src = this.view.assetMap[src].src;
        this.src = src;
    },
};

function Emitter(id, element, view, args, pool) {
    SceneObject.call(this, id, element, view, args);
    this.tweenableProperties.push("rate");

    this.pool = pool;
    this.rate = args.rate;
    this.emissionTimer = 0;
    if (this.rate > 0) {
        this.emissionTimer = 1000 / this.rate;
    }
    this.activeParticles = [];
    this.src = args.src;
    this.startScaleX = this.createRandomParameter(args.startscalex, 1, 1);
  if (args.endscalex) {
    this.endScaleX = this.createRandomParameter(args.endscalex, this.startScaleX);
  }
  if (args.startscaley) {
    this.startScaleY = this.createRandomParameter(args.startscaley, 1, 1);
    if (args.endscaley) {
    this.endScaleY = this.createRandomParameter(args.endscaley, this.startScaleY);
    }
  }
    this.startSkewX = this.createRandomParameter(args.startskewx, 1, 1);
    this.endSkewX = this.createRandomParameter(args.endskewx, this.startSkewX);
    this.startSkewY = this.createRandomParameter(args.startskewy, 1, 1);
    this.endSkewY = this.createRandomParameter(args.endskewy, this.startSkewY);
    this.speed = this.createRandomParameter(args.speed, 0, 0);
    this.accel = this.createRandomParameter(args.accel, 0, 0);
    this.forceX = this.createRandomParameter(args.forcex, 0, 0);
    this.forceY = this.createRandomParameter(args.forcey, 0, 0);
    this.startColor = this.createRandomColor(args.startcolor, [255, 255, 255], [255, 255, 255]);
    this.endColor = this.createRandomColor(args.endcolor, this.startColor);
    this.startAlpha = this.createRandomParameter(args.startalpha, 100, 100);
    this.endAlpha = this.createRandomParameter(args.endalpha, this.startAlpha);
    this.startRotation = this.createRandomParameter(args.startrotation, 0, 0);
    this.endRotation = this.createRandomParameter(args.endrotation, this.startRotation);
    this.lifetime = this.createRandomParameter(args.lifetime, 1, 1);
    this.angle = args.angle;
    this.ignoreRotation = args.ignorerotation === "1" || args.ignorerotation === "true";
}
Emitter.prototype = Object.create(SceneObject.prototype);
Emitter.prototype.constructor = Emitter;

Emitter.prototype.destroy = function () {
    SceneObject.prototype.destroy.call(this);
    this.killParticles();
}

Emitter.prototype.killParticles = function () {
    for (var i = 0; i < this.activeParticles.length; i++) {
        var particle = this.activeParticles[i];
        particle.destroy();
    }
}

Emitter.prototype.createRandomParameter = function (value, defaultMin, defaultMax) {
    if (typeof value !== "undefined") {
        var range = value.split(":");
        var min = parseFloat(range[0], 10);
        var max = (range.length > 1 ? parseFloat(range[1], 10) : min);
        if (!isNaN(min) && !isNaN(max)) {
            return new RandomParameter(min, max);
        }
    }
    if (defaultMin instanceof RandomParameter) {
        return defaultMin;
    }
    return new RandomParameter(defaultMin, defaultMax);
};

Emitter.prototype.createRandomColor = function (value, defaultMin, defaultMax) {
    if (typeof value !== "undefined") {
        var range = value.split(":");
        var min = fromHex(range[0]);
        var max = (range.length > 1 ? fromHex(range[1]) : min);
        if (min && max) {
            return new RandomColor(min, max);
        }
    }
    if (defaultMin instanceof RandomColor) {
        return defaultMin;
    }
    return new RandomColor(defaultMin, defaultMax);
};


Emitter.prototype.update = function (elapsedMs) {
    if (this.rate > 0) {
        var cooldown = 1000 / this.rate;
        this.emissionTimer += elapsedMs;
        while (this.emissionTimer >= cooldown) {
            this.emit();
            this.emissionTimer -= cooldown;
        }
    }

    for (var i = this.activeParticles.length - 1; i >= 0; i--) {
        var particle = this.activeParticles[i];
        particle.update(elapsedMs);
        if (particle.isDead()) {
            this.activeParticles.splice(i, 1);
            this.pool.push(particle); //return to the global inactive pool
        }
    }
};

Emitter.prototype.emit = function () {
    var particle = this.getFreeParticle();

    //randomize the rotation by the emission angle range
    var rotation = this.rotation;
    var angle = Math.floor(Math.random() * (this.angle * 2 + 1)) - this.angle;
    rotation += angle;

  //preserve aspect ratios if X and Y aren't specified individually
  var scaleStartX = this.startScaleX.get();
  var scaleEndX = (this.endScaleX ? this.endScaleX.get() : scaleStartX);
  var scaleStartY = (this.startScaleY ? this.startScaleY.get() : scaleStartX);
  var scaleEndY = (this.endScaleY ? this.endScaleY.get() : 
                   this.endScaleX ? scaleEndX : scaleStartY);

    particle.spawn(this.x - this.width / 2, this.y - this.height / 2, rotation, {
        src: this.src,
        width: this.width,
        height: this.height,
        duration: this.lifetime.get() * 1000,
    startScaleX: scaleStartX,
    endScaleX: scaleEndX,
    startScaleY: scaleStartY,
    endScaleY: scaleEndY,
        startSkewX: this.startSkewX.get(),
        endSkewX: this.endSkewX.get(),
        startSkewY: this.startSkewY.get(),
        endSkewY: this.endSkewY.get(),
        speed: this.speed.get(),
        accel: this.accel.get(),
        forceX: this.forceX.get(),
        forceY: this.forceY.get(),
        startColor: this.startColor.get(),
        endColor: this.endColor.get(),
        startAlpha: this.startAlpha.get(),
        endAlpha: this.endAlpha.get(),
        startRotation: this.startRotation.get(),
        endRotation: this.endRotation.get(),
        layer: this.layer,
        ignoreRotation: this.ignoreRotation,
    });
    this.activeParticles.push(particle);
};

Emitter.prototype.getFreeParticle = function () {
    var particle;
    if (this.pool.length === 0) {
        particle = new Particle("particle" + this.pool.length, this.view, {});
        this.pool.push(particle);
        this.view.$canvas.append(particle.element);
    }
    particle = this.pool[0];
    this.pool.splice(0, 1);
    return particle;
};

Emitter.prototype.draw = function () {
    if (this.element) {
        SceneObject.prototype.draw.call(this);
    }

    for (var i = this.activeParticles.length - 1; i >= 0; i--) {
        this.activeParticles[i].draw();
    }
};

function Particle(id, view, args) {
    var element = document.createElement("img");
    element.className = "sprite";
    SceneObject.call(this, id, element, view, args);
    this.$element = $(this.element);
    this.tweens = {};
}

Particle.prototype = Object.create(SceneObject.prototype);
Particle.prototype.constructor = Particle;

Particle.prototype.spawn = function (x, y, rotation, args) {
    var tweens = this.tweens;

    var particleElem = this.objElement;
    if (args.src) {
        particleElem.src = args.src;
        particleElem.className = "";
    }
    else {
        particleElem.removeAttribute("src");
        particleElem.className = "particle";
    }

    $(particleElem).css({
        "width": args.width + "px",
        "height": args.height + "px",
        "background-color": "",
    });
    $(this.element).css({
        "z-index": args.layer || "",
        "width": args.width + "px",
        "height": args.height + "px",
    });
    this.width = args.width;
    this.height = args.height;
    this.x = x;
    this.y = y;
    this.elapsed = 0;
    this.duration = args.duration;
    this.ease = args.ease || "smooth";
    this.ignoreRotation = args.ignoreRotation;
    tweens["scalex"] = new TweenableParameter(args.startScaleX, args.endScaleX);
    tweens["scaley"] = new TweenableParameter(args.startScaleY, args.endScaleY);
    tweens["skewx"] = new TweenableParameter(args.startSkewX, args.endSkewX);
    tweens["skewy"] = new TweenableParameter(args.startSkewY, args.endSkewY);
    tweens["alpha"] = new TweenableParameter(args.startAlpha, args.endAlpha);
    tweens["color"] = new TweenableColor(args.startColor, args.endColor);
    tweens["spin"] = new TweenableParameter(args.startRotation, args.endRotation);
    this.scalex = args.startScaleX;
    this.scaley = args.startScaleY;
    this.skewx = args.startSkewX;
    this.skewy = args.startSkewY;
    this.alpha = args.startAlpha;
    this.color = args.startColor;
    this.spin = args.startRotation;

    //initial speed is in the direction of the starting rotation
    this.rotation = this.ignoreRotation ? 0 : rotation;
    var degrees = rotation;
    var radians = degrees * (Math.PI / 180);
    var speed = args.speed;
    this.initialAngle = radians;

    //convert rotation angle to direction vector where 0 deg = [0,-1], 90 deg = [1,0]
    var u = Math.sin(radians);
    var v = -Math.cos(radians);

    this.speedX = speed * u;
    this.speedY = speed * v;

    this.accel = args.accel;
    this.forceX = args.forceX;
    this.forceY = args.forceY;

    this.$element.show();
},

Particle.prototype.isDead = function () {
    return this.elapsed >= this.duration;
};

Particle.prototype.die = function () {
    this.$element.hide();
};

Particle.prototype.update = function (elapsedMs) {
    this.elapsed += elapsedMs;
    var dt = elapsedMs / 1000;

    if (this.isDead()) {
        this.die();
    }

    var t = this.elapsed / this.duration;
    t = Animation.prototype.easingFunctions[this.ease](t);
    for (var prop in this.tweens) {
        this[prop] = this.tweens[prop].tween(t);
    }

    this.rotation += this.spin * dt;

    //accelerate in the forward direction
    var forward = this.initialAngle;
    //forward = this.rotation * (Math.PI / 180);
    var u = Math.sin(forward);
    var v = -Math.cos(forward);

    var accelX = this.accel * u;
    var accelY = this.accel * v;

    this.speedX += (accelX + this.forceX) * dt;
    this.speedY += (accelY + this.forceY) * dt;

    this.x += dt * this.speedX;
    this.y += dt * this.speedY;
};

Particle.prototype.draw = function (view) {
    if (!this.objElement.src) {
        var color = toHex(this.color);
        $(this.objElement).css({
            "background-color": color,
        });
    }
    SceneObject.prototype.draw.call(this);
};

function SceneTransition(fromView, toView, transitionDirective, overlay) {
    this.view1 = fromView;
    this.view2 = toView;
    this.duration = transitionDirective.time;
    this.elapsed = 0;
    this.overlay = overlay;
    this.overlay.css("background-color", transitionDirective.color);
    this.ease = transitionDirective.ease || "ease-out";
    switch (transitionDirective.effect) {
    case "dissolve":
        this.effect = this.dissolve;
        break;
    case "fade":
        this.effect = this.fade;
        break;
    case "wipe-right":
        this.effect = this.wipeRight;
        break;
    case "wipe-left":
        this.effect = this.wipeLeft;
        break;
    case "wipe-up":
        this.effect = this.wipeUp;
        break;
    case "wipe-down":
        this.effect = this.wipeDown;
        break;
    case "slide-right":
        this.effect = this.slideRight;
        break;
    case "slide-left":
        this.effect = this.slideLeft;
        break;
    case "slide-up":
        this.effect = this.slideUp;
        break;
    case "slide-down":
        this.effect = this.slideDown;
        break;
    case "push-left":
        this.effect = this.pushLeft;
        break;
    case "push-right":
        this.effect = this.pushRight;
        break;
    case "push-up":
        this.effect = this.pushUp;
        break;
    case "push-down":
        this.effect = this.pushDown;
        break;
    case "uncover-left":
        this.effect = this.uncoverLeft;
        break;
    case "uncover-right":
        this.effect = this.uncoverRight;
        break;
    case "uncover-up":
        this.effect = this.uncoverUp;
        break;
    case "uncover-down":
        this.effect = this.uncoverDown;
        break;
    case "barn-open-horizontal":
        this.effect = this.barnOpenHorizontal;
        break;
    case "barn-close-horizontal":
        this.effect = this.barnCloseHorizontal;
        break;
    case "barn-open-vertical":
        this.effect = this.barnOpenVertical;
        break;
    case "barn-close-vertical":
        this.effect = this.barnCloseVertical;
        break;
    case "fly-through":
        this.effect = this.flyThrough;
        break;
    case "spin":
        this.effect = this.spin;
        break;
    default:
        this.effect = this.cut;
        break;
    }
    this.effect(0);
}

SceneTransition.prototype.isComplete = function () {
    return this.elapsed >= this.duration;
}

SceneTransition.prototype.finish = function () {
    var styleReset = {
        "transform": "",
        "clip": "",
        "opacity": "",
    };
    this.view1.$viewport.css(styleReset);
    this.view2.$viewport.css(styleReset);
    this.view1.cleanup();
    this.overlay.css("opacity", "");
}

SceneTransition.prototype.update = function (elapsed) {
    this.elapsed += elapsed;

    if (this.isComplete()) {
        this.finish();
        return;
    }

    var t = Math.min(1, this.elapsed / this.duration);
    if (this.duration === 0) {
        t = 1;
    }
    else {
        var easingFunction = Animation.prototype.easingFunctions[this.ease];
        t = easingFunction(t);
    }
    this.effect(t);
}

SceneTransition.prototype.cut = function (t) {
    this.elapsed = this.duration;
    this.view1.$viewport.hide();
}

SceneTransition.prototype.dissolve = function (t) {
    var viewport1 = this.view1.$viewport;
    var viewport2 = this.view2.$viewport;

    viewport1.css("opacity", 1 - t);
    viewport2.css("opacity", t);
}

SceneTransition.prototype.fade = function (t) {
    var viewport1 = this.view1.$viewport;
    var viewport2 = this.view2.$viewport;

    var alpha = (t <= 0.5 ? t * 2 : (1 - (t - 0.5) * 2));

    this.overlay.css("opacity", alpha);
    viewport1.css("opacity", t < 0.5 ? 1 : 0);
    viewport2.css("opacity", t < 0.5 ? 0 : 1);
}

SceneTransition.prototype.slideRight = function (t) {
    var left = Math.ceil(this.view2.viewportWidth * (1 - t));
    this.view2.$viewport.css({
        "transform": "translate(calc(-50% - " + left + "px), -50%)",
        "clip": "rect(0, " + this.view2.viewportWidth + "px, " + this.view2.viewportHeight + "px, " + left + "px)",
    });
}

SceneTransition.prototype.slideLeft = function (t) {
    var left = Math.ceil(this.view2.viewportWidth * (1 - t));
    this.view2.$viewport.css({
        "transform": "translate(calc(-50% + " + left + "px), -50%)",
        "clip": "rect(0, " + (this.view2.viewportWidth - left) + "px, " + this.view2.viewportHeight + "px, 0)",
    });
}

SceneTransition.prototype.slideUp = function (t) {
    var top = Math.ceil(this.view2.viewportHeight * (1 - t));
    this.view2.$viewport.css({
        "transform": "translate(-50%, calc(-50% + " + top + "px)",
        "clip": "rect(0, " + this.view2.viewportWidth + "px, " + (this.view2.viewportHeight - top) + "px, 0)",
    });
}

SceneTransition.prototype.slideDown = function (t) {
    var top = Math.ceil(this.view2.viewportHeight * (1 - t));
    this.view2.$viewport.css({
        "transform": "translate(-50%, calc(-50% - " + top + "px)",
        "clip": "rect(" + top + "px, " + this.view2.viewportWidth + "px, " + this.view2.viewportHeight + "px, 0)",
    });
}

SceneTransition.prototype.wipeLeft = function (t) {
    var left = Math.ceil(this.view2.viewportWidth * (1 - t));
    this.view2.$viewport.css({
        "clip": "rect(0, " + this.view2.viewportWidth + "px, " + this.view2.viewportHeight + "px, " + left + "px)",
    });
}

SceneTransition.prototype.wipeRight = function (t) {
    var left = Math.ceil(this.view2.viewportWidth * (1 - t));
    this.view2.$viewport.css({
        "clip": "rect(0, " + (this.view2.viewportWidth - left) + "px, " + this.view2.viewportHeight + "px, 0)",
    });
}

SceneTransition.prototype.wipeUp = function (t) {
    var top = Math.ceil(this.view2.viewportHeight * (1 - t));
    this.view2.$viewport.css({
        "clip": "rect(" + top + "px, " + this.view2.viewportWidth + "px, " + this.view2.viewportHeight + "px, 0)",
    });
}

SceneTransition.prototype.wipeDown = function (t) {
    var top = Math.ceil(this.view2.viewportHeight * (1 - t));
    this.view2.$viewport.css({
        "clip": "rect(0, " + this.view2.viewportWidth + "px, " + (this.view2.viewportHeight - top) + "px, 0)",
    });
}

SceneTransition.prototype.pushRight = function (t) {
    var left = Math.ceil(this.view1.viewportWidth * t);
    this.view1.$viewport.css({
        "transform": "translate(calc(-50% + " + left + "px), -50%)",
        "clip": "rect(0, " + (this.view1.viewportWidth * (1 - t)) + "px, " + this.view1.viewportHeight + "px, 0)",
    });

    left = Math.ceil(this.view2.viewportWidth * (1 - t));
    this.view2.$viewport.css({
        "transform": "translate(calc(-50% - " + left + "px), -50%)",
        "clip": "rect(0, " + this.view2.viewportWidth + "px, " + this.view2.viewportHeight + "px, " + left + "px)",
    });
}

SceneTransition.prototype.pushLeft = function (t) {
    var left = -Math.ceil(this.view1.viewportWidth * t);
    this.view1.$viewport.css({
        "transform": "translate(calc(-50% + " + left + "px), -50%)",
        "clip": "rect(0, " + this.view1.viewportWidth + "px, " + this.view1.viewportHeight + "px, " + (-left) + "px)",
    });

    left = Math.ceil(this.view2.viewportWidth * (1 - t));
    this.view2.$viewport.css({
        "transform": "translate(calc(-50% + " + left + "px), -50%)",
        "clip": "rect(0, " + (this.view2.viewportWidth - left) + "px, " + this.view2.viewportHeight + "px, 0)",
    });
}

SceneTransition.prototype.pushUp = function (t) {
    var top = -Math.ceil(this.view1.viewportHeight * t);
    this.view1.$viewport.css({
        "transform": "translate(-50%, calc(-50% + " + top + "px)",
        "clip": "rect(" + (-top) + "px, " + this.view2.viewportWidth + "px, " + this.view2.viewportHeight + "px, 0)",
    });

    var top = Math.ceil(this.view2.viewportHeight * (1 - t));
    this.view2.$viewport.css({
        "transform": "translate(-50%, calc(-50% + " + top + "px)",
        "clip": "rect(0, " + this.view2.viewportWidth + "px, " + (this.view2.viewportHeight - top) + "px, 0)",
    });
}

SceneTransition.prototype.pushDown = function (t) {
    var top = Math.ceil(this.view1.viewportHeight * t);
    this.view1.$viewport.css({
        "transform": "translate(-50%, calc(-50% + " + top + "px)",
        "clip": "rect(0, " + this.view2.viewportWidth + "px, " + (this.view2.viewportHeight * (1 - t)) + "px, 0)",
    });

    top = Math.ceil(this.view2.viewportHeight * (1 - t));
    this.view2.$viewport.css({
        "transform": "translate(-50%, calc(-50% - " + top + "px)",
        "clip": "rect(" + top + "px, " + this.view2.viewportWidth + "px, " + this.view2.viewportHeight + "px, 0)",
    });
}

SceneTransition.prototype.uncoverRight = function (t) {
    var left = Math.ceil(this.view1.viewportWidth * t);
    this.view1.$viewport.css({
        "z-index": EpiloguePlayer.prototype.layer + 1,
        "transform": "translate(calc(-50% + " + left + "px), -50%)",
        "clip": "rect(0, " + (this.view1.viewportWidth * (1 - t)) + "px, " + this.view1.viewportHeight + "px, 0)",
    });
}

SceneTransition.prototype.uncoverLeft = function (t) {
    var left = -Math.ceil(this.view1.viewportWidth * t);
    this.view1.$viewport.css({
        "z-index": EpiloguePlayer.prototype.layer + 1,
        "transform": "translate(calc(-50% + " + left + "px), -50%)",
        "clip": "rect(0, " + this.view1.viewportWidth + "px, " + this.view1.viewportHeight + "px, " + (-left) + "px)",
    });
}

SceneTransition.prototype.uncoverUp = function (t) {
    var top = -Math.ceil(this.view1.viewportHeight * t);
    this.view1.$viewport.css({
        "z-index": EpiloguePlayer.prototype.layer + 1,
        "transform": "translate(-50%, calc(-50% + " + top + "px)",
        "clip": "rect(" + (-top) + "px, " + this.view2.viewportWidth + "px, " + this.view2.viewportHeight + "px, 0)",
    });
}

SceneTransition.prototype.uncoverDown = function (t) {
    var top = Math.ceil(this.view1.viewportHeight * t);
    this.view1.$viewport.css({
        "z-index": EpiloguePlayer.prototype.layer + 1,
        "transform": "translate(-50%, calc(-50% + " + top + "px)",
        "clip": "rect(0, " + this.view2.viewportWidth + "px, " + (this.view2.viewportHeight * (1 - t)) + "px, 0)",
    });
}

SceneTransition.prototype.barnOpenHorizontal = function (t) {
    this.view2.$viewport.css({
        "clip": "rect(0, " + (this.view2.viewportWidth / 2 + t * this.view2.viewportWidth / 2) + "px, " + this.view2.viewportHeight + "px, " + (this.view2.viewportWidth / 2 - t * this.view2.viewportWidth / 2) + "px)",
    });
}

SceneTransition.prototype.barnCloseHorizontal = function (t) {
    this.view1.$viewport.css({
        "z-index": EpiloguePlayer.prototype.layer + 1,
        "clip": "rect(0, " + (this.view2.viewportWidth / 2 + (1 - t) * this.view2.viewportWidth / 2) + "px, " + this.view2.viewportHeight + "px, " + (this.view2.viewportWidth / 2 - (1 - t) * this.view2.viewportWidth / 2) + "px)",
    });
}

SceneTransition.prototype.barnOpenVertical = function (t) {
    this.view2.$viewport.css({
        "clip": "rect(" + (this.view2.viewportHeight / 2 - t * this.view2.viewportHeight / 2) + "px, " + this.view2.viewportWidth + "px, " + (this.view2.viewportHeight / 2 + t * this.view2.viewportHeight / 2) + "px, 0)",
    });
}

SceneTransition.prototype.barnCloseVertical = function (t) {
    this.view1.$viewport.css({
        "z-index": EpiloguePlayer.prototype.layer + 1,
        "clip": "rect(" + (this.view2.viewportHeight / 2 - (1 - t) * this.view2.viewportHeight / 2) + "px, " + this.view2.viewportWidth + "px, " + (this.view2.viewportHeight / 2 + (1 - t) * this.view2.viewportHeight / 2) + "px, 0)",
    });
}

SceneTransition.prototype.spin = function (t) {
    var viewport1 = this.view1.$viewport;
    var viewport2 = this.view2.$viewport;

    if (t < 0.5) {
        t *= 2;
        viewport1.css("opacity", 1);
        viewport2.css("opacity", 0);
        this.view1.$viewport.css({
            "transform": "translate(-50%, -50%) rotate(" + t * 1080 + "deg) scale(" + (5 * t + 1) + ")",
        });
    }
    else {
        t = (1 - (t - 0.5) * 2);
        viewport1.css("opacity", 0);
        viewport2.css("opacity", 1);
        this.view2.$viewport.css({
            "transform": "translate(-50%, -50%) rotate(" + t * 1080 + "deg) scale(" + (5 * t + 1) + ")",
        });
    }
}

SceneTransition.prototype.flyThrough = function (t) {
    var zoom = lerp(1, 2, t);
    this.view1.$viewport.css({
        "z-index": EpiloguePlayer.prototype.layer + 1,
        "transform": "translate(-50%, -50%) scale(" + zoom + ")",
        "opacity": (1 - t),
    });
    zoom = lerp(0.5, 1, t);
    this.view2.$viewport.css({
        "z-index": EpiloguePlayer.prototype.layer + 1,
        "transform": "translate(-50%, -50%) scale(" + zoom + ")",
        "opacity": t,
    });
}

// Event handlers
$epilogueButtons.on('click', ':input', function(ev) {
    ev.stopImmediatePropagation();

    var action = $(ev.target).attr("data-action");
    switch (action) {
    case "next":
        moveEpilogueForward();
        break;
    case "prev":
        moveEpilogueBack();
        break;
    case "exit":
        showRestartModal();
        break;
    case "bug":
        showBugReportModal();
        break;
    case "reload":
        hotReloadEpilogue();
        break;
    case "skip":
        skipToEpilogueScene();
        break;
    case "fullscreen":
        toggleFullscreen();
        break;
    default:
        break;
    }
});

$('#epilogue-container').click(function(ev) {
    if (ev.pageX < window.innerWidth * 0.2) {
        moveEpilogueBack();
    } else {
        moveEpilogueForward(true);
    }
});

function epilogue_keyUp(ev) {
    if (epiloguePlayer && epiloguePlayer.loaded && $('.modal:visible').length == 0) {
        switch (ev.keyCode) {
        case 81:
            if (DEBUG) {
                $epilogueButtons.toggleClass('debug-active');
            }
            break;
        case 37:
            moveEpilogueBack(); break;
        case 32:
            moveEpilogueForward(true); break;
        case 39:
            moveEpilogueForward(); break;
        }
        ev.preventDefault();
    }
}

$epilogueScreen.on('mouseleave', function() {
    $epilogueButtons.removeClass('show');
});

$epilogueScreen.on('mousemove', function(ev) {
    $epilogueButtons.toggleClass('show', ev.pageY >= 0.75 * window.innerHeight);
});
$epilogueScreen.on('touchstart touchmove', function(ev) {
    var show = false;
    for (var i = 0; i < ev.originalEvent.changedTouches.length; i++) {
        if (ev.originalEvent.touches[0].pageY
            >= 0.75 * window.innerHeight) show = true;
    }
    $epilogueButtons.toggleClass('show', show);
});

$epilogueScreen.data('keyhandler', epilogue_keyUp);
