/********************************************************************************
 This file contains the variables and functions that form the poker mechanics and
 store information on the current poker state of each player. Anything regarding
 cards, except AI decisions, is part of this file (even UI).
 ********************************************************************************/
 
/**********************************************************************
 *****               Poker Hand Object Specification              *****
 **********************************************************************/
 
/* suit indices */
const SPADES   = 0;
const HEARTS   = 1;
const DIAMONDS = 2;
const CLUBS    = 3;

/* hand strengths */
var NONE            = 0;
var HIGH_CARD       = 1;
var PAIR            = 2;
var TWO_PAIR        = 3;
var THREE_OF_A_KIND = 4;
var STRAIGHT        = 5;
var FLUSH           = 6;
var FULL_HOUSE      = 7;
var FOUR_OF_A_KIND  = 8;
var STRAIGHT_FLUSH  = 9;
var ROYAL_FLUSH     = 10;

/**********************************************************************
 *****                      Poker UI Elements                     *****
 **********************************************************************/

/* hidden cards and hidden card areas */
$gameHiddenArea = $('#game-hidden-area');
 
/* player card cells */
$cardCells = [[$("#player-0-card-1"), $("#player-0-card-2"), $("#player-0-card-3"), $("#player-0-card-4"), $("#player-0-card-5")],
                 [$("#player-1-card-1"), $("#player-1-card-2"), $("#player-1-card-3"), $("#player-1-card-4"), $("#player-1-card-5")],
                 [$("#player-2-card-1"), $("#player-2-card-2"), $("#player-2-card-3"), $("#player-2-card-4"), $("#player-2-card-5")],
                 [$("#player-3-card-1"), $("#player-3-card-2"), $("#player-3-card-3"), $("#player-3-card-4"), $("#player-3-card-5")],
                 [$("#player-4-card-1"), $("#player-4-card-2"), $("#player-4-card-3"), $("#player-4-card-4"), $("#player-4-card-5")]];

/**********************************************************************
 *****                       Poker Variables                      *****
 **********************************************************************/

/* pseudo constants */
var ANIM_DELAY = 80;
var ANIM_TIME = 500;
var CARDS_PER_HAND = 5;
 
/* image constants */
var CARD_CONFIG_FILE = "cards.xml";
var BLANK_CARD_IMAGE = IMG + "blank.png";
var UNKNOWN_CARD_IMAGE = IMG + "unknown.jpg";
var SUIT_PREFIXES = ["spade", "heart", "diamo", "clubs"];
var ACTIVE_CARD_IMAGES = new ActiveCardImages();
var CARD_IMAGE_SETS = {};
var DEFAULT_CARD_DECK = 'default';

/* card decks */
var activeDeck;    /* deck for current round */

/* deal lock */
var dealLock = 0;

/************************************************************
 * Card class
 * (Ace represented as the number 14 so that the hand value 
 * array can be used to determine the cards to discard.)\
 * 
 * @param {number} suit
 * @param {number} rank
 ************************************************************/
function Card(suit, rank) {
    this.suit = suit;
    this.rank = rank;
}

/* This toString() method means that using a card object in a URL
 * yields the same filename as before */
Card.prototype.toString = function() {
    return cardImageKey(this.suit, this.rank);
};

Card.prototype.altText = function() {
    return (this.rank >= 11 ? "JQKA"[this.rank-11] : this.rank) + String.fromCharCode(0x2660 + this.suit);
};

/************************************************************
 * Hand class
 ************************************************************/
function Hand() {
    this.cards = Array(CARDS_PER_HAND);
    this.strength = NONE;
    this.value = [];
    this.tradeIns = Array(CARDS_PER_HAND);
}

Hand.prototype.toString = function() {
    return handStrengthToString(this.strength);
}

/************************************************************
 * Deck class
 ************************************************************/

function Deck() {
    var cards = [];

    for (var i = 0; i < 4; i++) {
        for (var j = 2; j <= 14; j++) {
            cards.push(new Card(i, j));
        }
    }

    /* Fisher-Yates shuffling algorithm.  At step i, cards 0 through i -
     * 1 of the shuffled deck have already been selected, while cards i
     * through cards.length - 1 have not.  We select a card uniformly
     * randomly from the unselected cards.  It becomes card i, and
     * whatever card was stored at index i gets tossed into the set of
     * unselected cards.
     */
    this.shuffle = function() {
        for (var i = 0; i < cards.length - 1; i++) {
            swapIndex = getRandomNumber(i, cards.length);
            var c = cards[i];
            cards[i] = cards[swapIndex];
            cards[swapIndex] = c;
        }
    }

    /* The maximum number of cards we deal in a round is 50.  This
     * happens when there are five active players and they all exchange
     * all their cards.  Since the deck starts with 52 cards we will
     * never run out.
     */
    this.dealCard = function() {
        return cards.pop();
    }

    this.rigFor = function(player) {
        var rigSuit = getRandomNumber(0, 4);
        for (var n = 0; n < CARDS_PER_HAND; n++) {
            var i = cards.length - 1 - (player * CARDS_PER_HAND + n);
            for (var j = 0; j < cards.length; j++) {
                if (cards[j].suit == rigSuit && cards[j].rank == 14 - n && i != j) {
                    var c = cards[i]; cards[i] = cards[j]; cards[j] = c;
                }
            }
        }
    }
}

/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Sets up all of the information needed to start playing poker.
 ************************************************************/
function setupPoker () {
    ACTIVE_CARD_IMAGES.generateCardBackMapping();
    ACTIVE_CARD_IMAGES.preloadImages();

    /* set up the player hands */
    players.forEach(function(player) {
        player.hand = new Hand()
    });
}

/**********************************************************************
 *****                       UI Functions                         *****
 **********************************************************************/
 

/**
 * Get a unique string key from a card's suit and rank.
 * @param {number | string} suit 
 * @param {number} rank 
 * @returns {string}
 */
function cardImageKey(suit, rank) {
    return SUIT_PREFIXES[suit] + (rank == 14 ? 1 : rank);
}

/**
 * A collection of images for cards.
 * @param {Object.<string, [Card, string]>} frontImages
 * @param {Object.<string, string>} backImages 
 * @param {string} id
 * @param {string} title
 * @param {string} subtitle
 * @param {string} credits
 * @param {string} description
 * @param {string?} unlockChar
 * @param {string?} unlockCollectible
 * @param {string?} status
 */
function CardImageSet (frontImages, backImages, id, title, subtitle, credits, description, unlockChar, unlockCollectible, status) {
    /** @type {Object.<string, string>} */
    this.frontImages = {};

    /** @type {Array<Card>} */
    this.includedFrontCards = [];

    Object.entries(frontImages).forEach(function (kv) {
        var k = kv[0];
        var v_pair = kv[1];
        
        this.includedFrontCards.push(v_pair[0]);
        this.frontImages[k] = v_pair[1];
    }.bind(this));

    this.backImages = backImages;

    this.id = id;
    this.title = title;
    this.subtitle = subtitle;
    this.credits = credits;
    this.description = description;
    this.unlockChar = unlockChar;
    this.unlockCollectible = unlockCollectible;
    this.status = status;
}

/**
 * Get the Collectible that is required for unlocking this deck.
 * @returns {Promise<Collectible?>}
 */
CardImageSet.prototype.getCollectible = function () {
    if (!this.unlockChar || !this.unlockCollectible) {
        return Promise.resolve(null);
    }

    var pl = loadedOpponents.find(function (pl) {
        return pl.id === this.unlockChar;
    }.bind(this));

    if (!pl) return Promise.resolve(null);

    return pl.fetchCollectibles().then(function () {
        return pl.collectibles.find(function (c) {
            return c.id === this.unlockCollectible;
        }.bind(this));
    }.bind(this));
}

/**
 * Get whether this deck's `status` makes it available for use or not.
 * @returns {boolean}
 */
CardImageSet.prototype.isAvailable = function () {
    return !this.status || includedOpponentStatuses[this.status];
}

/**
 * Get whether this card deck is unlocked or not.
 * @returns {Promise<boolean>}
 */
CardImageSet.prototype.isUnlocked = function () {
    if (!this.isAvailable()) {
        return Promise.resolve(false);
    }

    return this.getCollectible().then(function (collectible) {
        if (!collectible) return true;
        return collectible.isUnlocked();
    });
}

/**
 * Load a CardImageSet from an XML description.
 * 
 * @param {*} $xml
 * @returns {CardImageSet} 
 */
function imageSetFromXML($xml) {
    var mapping = {};
    var backs = {};

    var id = $xml.attr("id");
    var title = $xml.children("title").text();
    var subtitle = $xml.children("subtitle").text();
    var description = $xml.children("description").text();
    var credits = $xml.children("credits").html();
    var unlockChar = $xml.children("unlockChar").text() || null;
    var unlockCollectible = $xml.children("unlockCollectible").text() || null;
    var status = $xml.children("status").text() || null;

    $xml.children('front').each(function () {
        var $elem = $(this);
        var imageSrc = $elem.attr("src") || "";
        var ranks = $elem.attr("rank") || "2-14";
        var suits = $elem.attr("suit");

        if (!suits) {
            suits = SUIT_PREFIXES;
        } else {
            suits = suits.toLowerCase().split(/[\s,]+/).map(function (val) {
                switch (val[0]) {
                    case "s": return "spade";
                    case "c": return "clubs";
                    case "d": return "diamo";
                    case "h": return "heart";
                    default: return null;
                }
            }).filter(function (v) {
                return !!v
            });
        }

        ranks.split(/[\s,]+/).map(parseInterval).forEach(function (interval) {
            var min = 2;
            var max = 14;

            if (interval.min && interval.min >= 1 && interval.min <= 14) {
                min = interval.min;
            }

            if (interval.max && interval.max >= 1 && interval.max <= 14) {
                max = interval.max;
            }

            if (min > max) {
                // swap min, max
                var t = min;
                min = max;
                max = t;
            }

            for (var i = min; i <= max; i++) {
                var rank = (i === 1 ? 14 : i);
                var imgIdx = (i === 14 ? 1 : i);
                var im = imageSrc.replace("%i", imgIdx.toString(10));

                suits.forEach(function (suit) {
                    var c = new Card(SUIT_PREFIXES.indexOf(suit), rank);
                    mapping[c.toString()] = [c, im.replace("%s", suit)];
                });
            }
        });
    });

    $xml.children('back').each(function () {
        backs[$(this).attr("id")] = $(this).attr("src");
    });

    return new CardImageSet(
        mapping, backs, id, title, subtitle, credits, description,
        unlockChar, unlockCollectible, status
    );
}

/**
 * Load metadata for all custom card decks / image sets.
 */
function loadCustomDecks () {
    console.log("Loading custom card decks...");

    return fetchXML(CARD_CONFIG_FILE).then(function ($xml) {
        $xml.children("deck").each(function () {
            var imageSet = imageSetFromXML($(this));
            CARD_IMAGE_SETS[imageSet.id] = imageSet;
        });
    }).catch(function (err) {
        console.error("Could not load card decks:");
        captureError(err);
    });
}

/**
 * 
 * @param {string} dotPair
 * @returns {string} 
 */
function resolveBackImageRef(dotPair) {
    var sp = dotPair.split(".", 2);
    if (!sp || sp.length !== 2) return "";

    var set = CARD_IMAGE_SETS[sp[0]];
    if (set) return set.backImages[sp[1]] || "";
    return "";
}

function ActiveCardImages () {
    /** @type {Object.<string, string>} */
    this.frontImageMap = {};

    /** @type {Object.<string, string>} */
    this.backImageMap = {};

    /*
     * Note: backImages = null indicates that only default card back images are
     * in use.
     */

    /** @type {Set<string>?} */
    this.backImages = null;
}

ActiveCardImages.prototype.save = function () {
    var frontOverlay = {};
    Object.entries(this.frontImageMap).filter(function (kv) {
        if (!kv[1] || kv[1] === DEFAULT_CARD_DECK) return false;
        var set = CARD_IMAGE_SETS[kv[1]];
        return set && set.frontImages[kv[0]];
    }).forEach(function (kv) {
        frontOverlay[kv[0]] = kv[1];
    });

    var backArray = null;
    if (this.backImages) {
        backArray = Array.from(this.backImages).filter(resolveBackImageRef);

        if (backArray.every(function (dotPair) {
            return dotPair.startsWith(DEFAULT_CARD_DECK + ".");
        })) {
            backArray = null;
        }
    }

    var saveObj = {
        "front": frontOverlay,
        "back": backArray
    };

    save.setItem("cardDeck", saveObj);
}

ActiveCardImages.prototype.load = function () {
    var saveObj = save.getItem("cardDeck", false) || {};

    this.frontImageMap = {};
    this.backImages = null;
    this.activateSetFront(CARD_IMAGE_SETS[DEFAULT_CARD_DECK]);

    if (saveObj.front) {
        Object.entries(saveObj.front).filter(function (kv) {
            if (!kv[1] || kv[1] === DEFAULT_CARD_DECK) return false;
            var set = CARD_IMAGE_SETS[kv[1]];
            return set && set.frontImages[kv[0]];
        }).forEach(function (kv) {
            this.frontImageMap[kv[0]] = kv[1];
        }.bind(this));
    }

    if (saveObj.back) {
        var filtered = saveObj.back.filter(resolveBackImageRef);

        if (filtered.some(function (dotPair) {
            return !dotPair.startsWith(DEFAULT_CARD_DECK + ".");
        })) {
            this.backImages = new Set(filtered);
        }
    }

    this.preloadImages();
}

ActiveCardImages.prototype.reset = function () {
    this.frontImageMap = {};
    this.backImages = null;

    this.activateSetFront(CARD_IMAGE_SETS[DEFAULT_CARD_DECK]);
    this.preloadImages();
}

/**
 * Activate a front image from a set.
 * If the given set does not define a card front image for the specified card,
 * a default card image is used instead.
 * @param {CardImageSet} imageSet 
 * @param {Card} card
 */
ActiveCardImages.prototype.activateFrontImage = function (imageSet, card) {
    var k = card.toString();
    if (imageSet.frontImages[k]) {
        this.frontImageMap[k] = imageSet.id;
    } else {
        this.frontImageMap[k] = DEFAULT_CARD_DECK;
    }
}

/**
 * Activate all defined card front images from a set.
 * @param {CardImageSet} imageSet 
 */
ActiveCardImages.prototype.activateSetFront = function (imageSet) {
    Object.keys(imageSet.frontImages).forEach(function (k) {
        this.frontImageMap[k] = imageSet.id;
    }.bind(this));
}

/**
 * Deactivate the front image for a card, replacing it with a default card image.
 * @param {Card} card
 */
ActiveCardImages.prototype.deactivateFrontImage = function (card) {
    this.frontImageMap[card.toString()] = DEFAULT_CARD_DECK;
}

/**
 * Check whether the front image defined by a set for a given card is active.
 * 
 * @param {CardImageSet} imageSet 
 * @param {Card} card 
 * @returns {boolean}
 */
ActiveCardImages.prototype.isFrontImageActive = function (imageSet, card) {
    return (this.frontImageMap[card.toString()] || DEFAULT_CARD_DECK) === imageSet.id;
}

/**
 * Add a card back image to the active set.
 * 
 * @param {CardImageSet} imageSet 
 * @param {string} imgID 
 */
ActiveCardImages.prototype.addBackImage = function (imageSet, imgID) {
    if (!this.backImages) this.backImages = new Set();
    this.backImages.add(imageSet.id + "." + imgID);
}

/**
 * Remove a card back image from the active set.
 * 
 * @param {CardImageSet} imageSet 
 * @param {string} img 
 */
ActiveCardImages.prototype.removeBackImage = function (imageSet, imgID) {
    if (!this.backImages) return;

    this.backImages.delete(imageSet.id + "." + imgID);

    if (Array.from(this.backImages).every(function (dotPair) {
        return dotPair.startsWith(DEFAULT_CARD_DECK + ".");
    })) {
        this.backImages = null;
    }
}

/**
 * Check whether a given back image is active or not.
 * 
 * @param {CardImageSet} imageSet 
 * @param {string} imgID
 * @returns {boolean}
 */
ActiveCardImages.prototype.isBackImageActive = function (imageSet, imgID) {
    if (!this.backImages) return (imageSet.id === DEFAULT_CARD_DECK);
    return this.backImages.has(imageSet.id + "." + imgID);
}

/**
 * Generate a random mapping between all 52 cards and active card back images.
 */
ActiveCardImages.prototype.generateCardBackMapping = function () {
    var allCards = new Deck();
    allCards.shuffle();

    var backImages = null;
    if (this.backImages) {
        backImages = Array.from(this.backImages).map(resolveBackImageRef).filter(function (v) {
            return !!v;
        });
    }

    if (!backImages || backImages.length === 0) {
        backImages = Object.values(CARD_IMAGE_SETS[DEFAULT_CARD_DECK].backImages);
    }

    var i = 0, card;
    while (card = allCards.dealCard()) {
        this.backImageMap[card.toString()] = backImages[i++ % backImages.length];
    }

    /* Pre-set the "deck" image underneath the main button to an arbitrary
     * selected card back image.
     */
    $('#deck').attr('src', backImages[0]);
}

/**
 * Get the appropriate front or back image to display for a given card.
 * 
 * @param {boolean} visible
 * @param {Card | number | string} card_or_suit
 * @param {number?} rank
 * @returns {string}
 */
ActiveCardImages.prototype.getCardImage = function (visible, card) {
    var k = card.toString();

    if (visible) {
        var set = this.frontImageMap[k];
        var ret = CARD_IMAGE_SETS[DEFAULT_CARD_DECK].frontImages[k] || (IMG + k + ".jpg");

        if (set && CARD_IMAGE_SETS[set]) {
            ret = CARD_IMAGE_SETS[set].frontImages[k] || ret;
        }

        return ret;
    } else {
        return this.backImageMap[k] || UNKNOWN_CARD_IMAGE;
    }
}

/**
 * Displays a card, face up or face down, or an empty space if the card is missing.
 * @param {number} player
 * @param {number} slot 
 * @param {boolean} visible
 */
ActiveCardImages.prototype.displayCard = function (player, slot, visible) {
    var card = players[player].hand.cards[slot];

    if (card) {
        detectCheat();
        var img = this.getCardImage(visible, card);
        var altText = card.altText();

        if (!visible) altText = '?';
        $cardCells[player][slot].attr({
            src: img,
            alt: altText
        });

        fillCard(player, slot);
        $cardCells[player][slot].css('visibility', '');
    } else {
        clearCard(player, slot);
    }
}

/**
 * Prefetch all active card images.
 */
ActiveCardImages.prototype.preloadImages = function () {
    for (var suit = 0; suit < 4; suit++) {
        for (var i = 2; i < 15; i++) {
            var key = cardImageKey(suit, i);
            var src = CARD_IMAGE_SETS[DEFAULT_CARD_DECK].frontImages[key] || (IMG + k + ".jpg");
            var set_id = this.frontImageMap[key];

            if (set_id && CARD_IMAGE_SETS[set_id]) {
                src = CARD_IMAGE_SETS[set_id].frontImages[key] || src;
            }
            
            new Image().src = src;
        }
    }
    
    var backImages = null;
    if (this.backImages) {
        backImages = Array.from(this.backImages).map(resolveBackImageRef).filter(function (v) {
            return !!v;
        });
    }

    if (!backImages || backImages.length === 0) {
        backImages = Object.values(CARD_IMAGE_SETS[DEFAULT_CARD_DECK].backImages);
    }

    backImages.forEach(function (src) {
        if (src) new Image().src = src;
    });

    new Image().src = BLANK_CARD_IMAGE;
}

/************************************************************
 * Sets the given card to full opacity.
 ************************************************************/
function fillCard (player, card) {
    $cardCells[player][card].removeClass('tradein');
}

/************************************************************
 * Sets the given card to a lower opacity.
 ************************************************************/
function dullCard (player, card) {
    $cardCells[player][card].addClass('tradein');
}

/************************************************************
 * Removes a card from display
 ************************************************************/
function clearCard (player, i) {
    $cardCells[player][i].css('visibility', 'hidden')
        .removeClass('tradein')
        .attr({src: BLANK_CARD_IMAGE, alt: '-'});
}

/************************************************************
 * Shows the given player's hand at full opacity.
 ************************************************************/
function showHand (player) {
    displayHand(player, true);
    if (player > 0) {
        $gameOpponentAreas[player-1].attr('data-original-title', players[player].hand.describeFormal());
        if (EXPLAIN_ALL_HANDS) $gameOpponentAreas[player-1].tooltip('show');
    } else {
        $gamePlayerCardArea.attr('data-original-title', players[player].hand.describeFormal());
        if (EXPLAIN_ALL_HANDS) $gamePlayerCardArea.tooltip('show');
    }
}

/************************************************************
 * Renders the given player's hand
 ************************************************************/
function displayHand (player, visible) {
    for (var i = 0; i < CARDS_PER_HAND; i++) {
        ACTIVE_CARD_IMAGES.displayCard(player, i, visible);
    }
}

/************************************************************
 * Clears the given player's hand (in preparation of a new game).
 ************************************************************/
function clearHand (player) {
    for (var i = 0; i < CARDS_PER_HAND; i++) {
        clearCard(player, i);
    }
    if (player > 0) {
        $gameOpponentAreas[player-1].attr('data-original-title', '').tooltip('hide');
    } else {
        $gamePlayerCardArea.attr('data-original-title', '').tooltip('hide');
    }
}

/*************************************************************
 * Stops all card animations.
 *************************************************************/
function stopCardAnimations () {
    $('.shown-card').stop(true, true);
}


/**********************************************************************
 *****                      Card Functions                        *****
 **********************************************************************/

/************************************************************
 * Compose and shuffle a new deck.
 ************************************************************/
function setupDeck () {
    activeDeck = new Deck();
    activeDeck.shuffle();
}

/************************************************************
 * Deals new cards to the given player.
 ************************************************************/
function dealHand (player, numPlayers, playersBefore) {
    /* The card animation gets wonky if the table isn't visible;
     * Cards will fly off to the corner of the screen if they don't have a place to go.
     */
    forceTableVisibility(1);

    /* deal the new cards */
    for (var i = 0; i < CARDS_PER_HAND; i++) {
        players[player].hand.tradeIns[i] = false;
        players[player].hand.cards[i] = activeDeck.dealCard();
        // Simulate dealing one card to each player, then another to
        // each player, and so on.
        animateDealtCard(player, i, numPlayers * i + playersBefore);
    }
    players[player].hand.determine();
}

/************************************************************
 * Exchanges the chosen cards for the given player.
 ************************************************************/
function exchangeCards (player) {
    /* delete swapped cards */
    /* Move kept cards to the left */
    players[player].hand.cards = players[player].hand.cards.filter(function(c, i) {
        return !players[player].hand.tradeIns[i];
    });
    
    Sentry.addBreadcrumb({
        category: 'game',
        message: players[player].id+' swaps '+players[player].hand.tradeIns.countTrue()+' cards',
        level: 'debug'
    });

    /* See above comment in dealHand re: the card animation and table visibility */
    forceTableVisibility(1);

    /* Refresh display. */
    displayHand(player, player == HUMAN_PLAYER);

    /* draw new cards */
    var n = 0;
    for (var i = players[player].hand.cards.length; i < CARDS_PER_HAND; i++, n++) {
        dealLock++;
        players[player].hand.cards.push(activeDeck.dealCard());
        animateDealtCard(player, i, n);
    }
    players[player].hand.determine();
}

/**********************************************************************
 *****                    Animation Functions                     *****
 **********************************************************************/

/************************************************************
 * Animates a small card into a player's hand.  n is the card's number
 * in the order dealt, used to calculate the initial delay.
 ************************************************************/
function animateDealtCard (player, card, n) {
    $('#deck').attr('src', ACTIVE_CARD_IMAGES.getCardImage(
        false, players[player].hand.cards[card]
    ));

    var $clonedCard = $('#deck').clone().attr('id', '').addClass('shown-card').prependTo($gameHiddenArea);
    
    if (player == HUMAN_PLAYER) {
        $clonedCard.addClass("large-card-image");
    } else {
        $clonedCard.addClass('small-card-image');
    }

    var offset = $cardCells[player][card].offset();
    var top = offset.top - $gameHiddenArea.offset().top;
    var left = offset.left - $gameHiddenArea.offset().left - 6;

    // Skip animation time calculation if skipping animation
    if (ANIM_TIME === 0) {
        var animTime = 0;
    } else {
        // Set card speed according to desired time to deal card to farthest position
        var speed = getFarthestDealDistance() / ANIM_TIME;
        var distance = offsetDistance($clonedCard.offset(), offset);
        var animTime = distance / speed;
    }

    $clonedCard.delay(n * ANIM_DELAY).animate({top: top, left: left}, animTime, function() {
        $clonedCard.remove();
        ACTIVE_CARD_IMAGES.displayCard(player, card, player == HUMAN_PLAYER);
        dealLock--;
        if (dealLock <= 0) {
            $gameScreen.removeClass('prompt-exchange');
        }
    });
}

/************************************************************
 * Get the farthest distance between any player card and the dealer
 ************************************************************/
function getFarthestDealDistance()
{
    return offsetDistance($('#player-4-card-5').offset(), $('#deck').offset());
}

/************************************************************
 * Get the distance between 2 offsets (objects with top & left)
 ************************************************************/
function offsetDistance (offset1, offset2)
{
    return distance2d(offset1.left, offset1.top, offset2.left, offset2.top);
}


/************************************************************
 * Pythagorean theorem for 2 dimensions
 ************************************************************/
function distance2d (x1, y1, x2, y2)
{
    return Math.sqrt(Math.pow(x1 - x2, 2) + Math.pow(y1 - y2, 2));
}

/**********************************************************************
 *****                      Poker Functions                       *****
 **********************************************************************/

/************************************************************
 * Maps the hand strength to a string.
 ************************************************************/
function handStrengthToString (number) {
    switch (number) {
        case NONE:              return undefined;
        case HIGH_CARD:         return "High card";
        case PAIR:              return "One pair";
        case TWO_PAIR:          return "Two pair";
        case THREE_OF_A_KIND:   return "Three of a kind";
        case STRAIGHT:          return "Straight";
        case FLUSH:             return "Flush";
        case FULL_HOUSE:        return "Full house";
        case FOUR_OF_A_KIND:    return "Four of a kind";
        case STRAIGHT_FLUSH:    return "Straight flush";
        case ROYAL_FLUSH:       return "Royal flush";
    }
}

function handStrengthFromString (string) {
    if (!string) return NaN;
    switch (string.trim().toLowerCase()) {
    case "high card":       return HIGH_CARD;
    case "one pair":        return PAIR;
    case "two pair":        return TWO_PAIR;
    case "three of a kind": return THREE_OF_A_KIND;
    case "straight":        return STRAIGHT;
    case "flush":           return FLUSH;
    case "full house":      return FULL_HOUSE;
    case "four of a kind":  return FOUR_OF_A_KIND;
    case "straight flush":  return STRAIGHT_FLUSH;
    case "royal flush":     return ROYAL_FLUSH;
    }
    return NaN;
}

function cardRankToString(rank, plural) {
    var str = [ 'deuce', 'three', 'four', 'five', 'six',
                'seven', 'eight', 'nine', 'ten',
                'jack', 'queen', 'king', 'ace' ][rank - 2];
    if (plural !== false) {
        str += (rank == 6 ? 'es' : 's');
    }
    return str;
}

function cardSuitToString (suit) {
    switch (suit) {
        case SPADES:   return "Spades";
        case HEARTS:   return "Hearts";
        case DIAMONDS: return "Diamonds";
        case CLUBS:    return "Clubs";
        default:       return "";
    }
}

Hand.prototype.describe = function(with_article) {
    var use_article = false;
    var description;
    switch (this.strength) {
    case NONE:
        break;
    case HIGH_CARD:
        description = cardRankToString(this.value[0], false) + " high"; break;
    case PAIR:
        description = "pair of " + cardRankToString(this.value[0]);
        use_article = true; break;
    case TWO_PAIR:
        description = "two pair of "
            + cardRankToString(this.value[0]) + " and "
            + cardRankToString(this.value[1]);
        break;
    case THREE_OF_A_KIND:
        description = "three " + cardRankToString(this.value[0]); break;
    case FOUR_OF_A_KIND:
        description = "four " + cardRankToString(this.value[0]); break;
    default:
        description = handStrengthToString(this.strength).toLowerCase();
        use_article = true;
    }
    if (with_article && use_article) {
        return (description[0] == 'a' || description[0] == 'e' ? 'an ' : 'a ') + description;
    } else return description;
};

Hand.prototype.describeFormal = function() {
    var description = handStrengthToString(this.strength) + ', ';
    switch (this.strength) {
    case NONE:
        description = undefined;
    case HIGH_CARD:
        description += cardRankToString(this.value[0], false); break;
    case PAIR:
        description += cardRankToString(this.value[0]); break;
    case TWO_PAIR:
        description += cardRankToString(this.value[0]) + " over "
            + cardRankToString(this.value[1], true);
        break;
    case THREE_OF_A_KIND:
        description += cardRankToString(this.value[0]); break;
    case STRAIGHT:
    case FLUSH:
    case STRAIGHT_FLUSH:
        description += cardRankToString(this.value[0], false) + ' high'; break;
    case FULL_HOUSE:
        description += cardRankToString(this.value[0]) + " full of "
            + cardRankToString(this.value[1]); break;
    case FOUR_OF_A_KIND:
        description += cardRankToString(this.value[0]); break;
    // Royal Flush needs no further description
    }
    return description;
};

Hand.prototype.score = function() {
    return this.strength != NONE ? (this.strength - 1) * 100 + this.value[0] : undefined;
};

// Sort the cards
Hand.prototype.sort = function() {
    // Sort the cards such that the pair, triplet, etc. comes
    // first, then the kickers. .value[] is sorted in the
    // right order by .determine().
    this.cards.sort(function(a, b) {
        return this.value.indexOf(a.rank) - this.value.indexOf(b.rank);
    }.bind(this));
};

/**********************************************************************
 * Return -1, 0, or 1 depending on whether h1 is respectively worse,
 * exactly tied with, or better than h2.  Assumes that both hands have
 * been correctly determined, so that if the hand types are equal, the
 * value arrays are of equal length.
 **********************************************************************/
function compareHands (h1, h2) {
    if (h1.strength < h2.strength) return -1;
    if (h1.strength > h2.strength) return 1;
    for (var i = 0; i < h1.value.length; i++) {
        if (h1.value[i] < h2.value[i]) return -1;
        if (h1.value[i] > h2.value[i]) return 1;
    }
    return 0;
}

/************************************************************
 * Determine value of a given player's hand.
 * player is a player object, not an index.
 ************************************************************/
Hand.prototype.determine = function() {
    /* start by getting a shorthand variable and resetting */
    
    /* look for each strength, in composition */
    var have_pair = [];
    var have_three_kind = 0;
    var have_straight = 0;
    var have_flush = 0;

    /* start by collecting the ranks and suits of the cards */
    this.ranks = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
    this.suits = [0, 0, 0, 0];
    this.strength = NONE;
    this.value = [];

    this.cards.forEach(function(card) {
        this.ranks[card.rank - 1]++;
        this.suits[card.suit]++;
    }, this);
    this.ranks[0] = this.ranks[13];
    
    /* look for four of a kind, three of a kind, and pairs */
    for (var i = this.ranks.length-1; i > 0; i--) {
        if (this.ranks[i] == 4) {
            this.strength = FOUR_OF_A_KIND;
            this.value = [i+1];
            break;
        } else if (this.ranks[i] == 3) {
            have_three_kind = i+1;
        } else if (this.ranks[i] == 2) {
            have_pair.push(i+1);
        }
    }
    
    /* determine full house, three of a kind, two pair, and pair */
    if (this.strength == NONE) {
        if (have_three_kind && have_pair.length > 0) {
            this.strength = FULL_HOUSE;
            this.value = [have_three_kind, have_pair[0]];
        } else if (have_three_kind) {
            this.strength = THREE_OF_A_KIND;
            this.value = [have_three_kind];
        } else if (have_pair.length > 0) {
            this.strength = have_pair.length == 2 ? TWO_PAIR : PAIR;
            this.value = have_pair;
        }
    }

    for (var i = this.ranks.length-1; i > 0; i--) {
        if (this.ranks[i] == 1) {
            this.value.push(i+1);
        }
    }
    
    /* look for straights and flushes */
    if (this.strength == NONE) {
        /* first, straights */
        var sequence = 0;

        for (var i = 0; i < this.ranks.length; i++) {
            if (this.ranks[i] == 1) {
                sequence++;
                if (sequence == CARDS_PER_HAND) {
                    /* one card each of five consecutive ranks is a
                     * straight */
                    have_straight = i+1;
                    break;
                }
            } else if (sequence > 0) {
                if (i == 1) {
                    /* Ace but no deuce is OK - we might have 10-A */
                    sequence = 0;
                } else {
                    /* A hole in the sequence - can't have a straight */
                    break;
                }
            }
        }
        
        /* second, flushes */
        for (var i = 0; i < this.suits.length; i++) {
            if (this.suits[i] == CARDS_PER_HAND) {
                /* this is a flush */
                have_flush = 1;
                break;
            } else if (this.suits[i] > 0) {
                /* can't have a flush */
                break;
            }
        }
        
        /* determine royal flush, straight flush, flush, straight, and high card.
           this.value[] has already been populated */
        if (have_flush && have_straight == 14) {
            this.strength = ROYAL_FLUSH;
        } else if (have_flush && have_straight) {
            this.strength = STRAIGHT_FLUSH;
        } else if (have_straight) {
            this.strength = STRAIGHT;
        } else {
            this.strength = (have_flush ? FLUSH : HIGH_CARD);
        }
        if (have_straight == 5) { // Wheel, special case, sort ace last
            this.value = [5, 4, 3, 2, 14];
        }
    }

    /* stats for the log */
    console.log();
    console.log("Rank: "+this.ranks);
    console.log("Suit: "+this.suits);
    console.log("Player has " +handStrengthToString(this.strength)+" of value "+this.value);
}
