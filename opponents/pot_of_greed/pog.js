if (!pog) var pog = (function (root) {

    /* Polyfill String.prototype.repeat */
    if (!String.prototype.repeat) {
        String.prototype.repeat = function (count) {
            'use strict';
            if (this == null)
                throw new TypeError('can\'t convert ' + this + ' to object');

            var str = '' + this;
            // To convert string to integer.
            count = +count;
            // Check NaN
            if (count != count)
                count = 0;

            if (count < 0)
                throw new RangeError('repeat count must be non-negative');

            if (count == Infinity)
                throw new RangeError('repeat count must be less than infinity');

            count = Math.floor(count);
            if (str.length == 0 || count == 0)
                return '';

            // Ensuring count is a 31-bit integer allows us to heavily optimize the
            // main part. But anyway, most current (August 2014) browsers can't handle
            // strings 1 << 28 chars or longer, so:
            if (str.length * count >= 1 << 28)
                throw new RangeError('repeat count must not overflow maximum string size');

            var maxCount = str.length * count;
            count = Math.floor(Math.log(count) / Math.log(2));
            while (count) {
                str += str;
                count--;
            }
            str += str.substring(0, maxCount - str.length);
            return str;
        }
    }

    var exports = {};

    if (root.SENTRY_INITIALIZED) {
        root.Sentry.setTag('pog-loaded', true);
        root.Sentry.addBreadcrumb({
            category: 'pot-of-greed',
            message: 'Initializing pog.js...',
            level: 'info'
        });
    }
    
    var SIX_STRAIGHT = 7.2;
    var STRAIGHT_PLUS_PAIR = 7.35;
    var THREE_PAIR = 7.5;
    var SIX_FLUSH = 7.65;
    var FULL_HOUSE_PLUS_PAIR = 7.8;
    var SEVEN_STRAIGHT = 8.2;
    var TWO_THREE_OF_A_KINDS = 8.5;
    var FULLER_HOUSE = 8.8;
    var SEVEN_FLUSH = 9.5;
    var STRAIGHT_FLUSH_PLUS_PAIR = 10;
    var SIX_STRAIGHT_FLUSH = 11;
    var FULLEST_HOUSE = 12;
    var STRAIGHT_AND_PAIR_FLUSH = 13;
    var ROYAL_FLUSH_PLUS_PAIR = 14;
    var SIX_ROYAL_FLUSH = 15;
    var ROYAL_STRAIGHT_AND_PAIR_FLUSH = 16;
    var SEVEN_STRAIGHT_FLUSH = 17;
    var SEVEN_ROYAL_FLUSH = 18;

    function loadScript(scriptName) {
        console.log("[Pot of Greed] Loading module: " + scriptName);

        const script = document.createElement('script');
        script.src = scriptName;
        script.async = false;

        script.addEventListener('load', function () {
            console.log("[Pot of Greed] Loaded module: " + scriptName);
        });

        script.addEventListener('error', function () {
            console.error("[Pot of Greed] Error loading " + scriptName);
        });

        root.document.head.appendChild(script);
    }

    function reportException(prefix, e) {
        console.log("[Pot of Greed] Exception swallowed " + prefix + ": ");

        if (USAGE_TRACKING && SENTRY_INITIALIZED) {
            Sentry.withScope(function (scope) {
                scope.setTag("pot-of-greed-error", true);
                scope.setExtra("where", prefix);
                captureError(e);
            });
            return;
        }

        captureError(e);
    }
    exports.reportException = reportException;

    function registerBehaviourCallback(name, func) {
        exports[name] = function () {
            try {
                return func.apply(null, arguments);
            } catch (e) {
                reportException('in behaviour callback ' + name, e);
            }
        };
    }

    exports.registerBehaviourCallback = registerBehaviourCallback;

    /* Set up hooks system... */
    var registeredHooks = {};
    exports.hooks = registeredHooks;

    function registerHook(hookedID, hookType, hookFunc) {
        registeredHooks[hookedID][hookType].push(hookFunc);
        return hookFunc;
    }
    exports.registerHook = registerHook;

    function unregisterHook(hookedID, hookType, hookFunc) {
        var hookList = registeredHooks[hookedID][hookType];
        var idx = hookList.indexOf(hookFunc);
        if (idx < 0) return;

        hookList.splice(idx, 1);
    }
    exports.unregisterHook = unregisterHook;

    function hookWrapper(func_id) {
        registeredHooks[func_id] = {
            'pre': [],
            'instead': [],
            'post': []
        };

        var original_function = root[func_id];
        return function () {
            /* Prevent the original function from firing if any pre-hook
             * returns true.
             */
            var wrapper_args = arguments;
            
            if (registeredHooks[func_id].instead[0]) {
                try {
                    return registeredHooks[func_id].instead[0].apply(null, arguments);
                } catch (e) {
                    reportException("in instead-" + func_id + " hooks", e);
                }
            } else {
                try {
                    if (registeredHooks[func_id].pre.some(function (hook) {
                            return hook.apply(null, wrapper_args);
                        })) {
                        return;
                    };
                } catch (e) {
                    reportException("in pre-" + func_id + " hooks", e);
                }

                var retval = original_function.apply(null, arguments);

                try {
                    registeredHooks[func_id].post.forEach(function (hook) {
                        hook.apply(null, wrapper_args);
                    });
                } catch (e) {
                    reportException("in post-" + func_id + " hooks", e);
                } finally {
                    return retval;
                }
            }
        }
    }
    
    root.startDealPhase = hookWrapper('startDealPhase');
    root.displayHand = hookWrapper('displayHand');
    root.clearHand = hookWrapper('clearHand');
    root.dealHand = hookWrapper('dealHand');
    root.exchangeCards = hookWrapper('exchangeCards');
    root.showHand = hookWrapper('showHand');
    root.handStrengthFromString = hookWrapper('handStrengthFromString');
    root.determineAIAction = hookWrapper('determineAIAction');
    root.Hand.prototype.determine = hookWrapper('Hand.prototype.determine');
    root.Hand.prototype.toString = hookWrapper('Hand.prototype.toString');
    root.Hand.prototype.describe = hookWrapper('Hand.prototype.describe');
    root.restartGame = hookWrapper('restartGame');
    
    function chooseCardAmt(player) {
        if (players[player].id === "pot_of_greed") {
            CARDS_PER_HAND = 7;
        }
    }
    
    function resetCardAmt() {
        CARDS_PER_HAND = 5;
    }
    
    registerHook('displayHand', 'pre', chooseCardAmt);
    registerHook('clearHand', 'post', resetCardAmt);
    registerHook('dealHand', 'pre', chooseCardAmt);
    registerHook('dealHand', 'post', resetCardAmt);
    registerHook('exchangeCards', 'pre', chooseCardAmt);
    registerHook('exchangeCards', 'post', resetCardAmt);
    
    function addExtraCards() {
        for (var i = 1; i < players.length; i++) {
            if (players[i] && players[i].id === "pot_of_greed") {
                $("#opponent-card-area-" + i).append($("<img>", {
                                                                    "id": "player-" + i + "-card-6",
                                                                    "class": "bordered small-card-image",
                                                                    "src": "img/blank.png",
                                                                    "alt": "",
                                                                    "style": "visibility: hidden;"
                                                                }));
                $("#opponent-card-area-" + i).append($("<img>", {
                                                                    "id": "player-" + i + "-card-7",
                                                                    "class": "bordered small-card-image",
                                                                    "src": "img/blank.png",
                                                                    "alt": "",
                                                                    "style": "visibility: hidden;"
                                                                }));
                
                $cardCells[i] = [$("#player-" + i + "-card-1"),
                                 $("#player-" + i + "-card-2"),
                                 $("#player-" + i + "-card-3"),
                                 $("#player-" + i + "-card-4"),
                                 $("#player-" + i + "-card-5"),
                                 $("#player-" + i + "-card-6"),
                                 $("#player-" + i + "-card-7")];
                                 
                players[i].hand.cards = Array(CARDS_PER_HAND + 2);
                players[i].hand.tradeIns = Array(CARDS_PER_HAND + 2);
            }
        }
    }
    
    function removeExtraCards() {
        for (var i = 1; i < $cardCells.length; i++) {
            if ($cardCells[i] && $cardCells[i].length > 5) {
                $cardCells[i] = [$("#player-" + i + "-card-1"),
                                 $("#player-" + i + "-card-2"),
                                 $("#player-" + i + "-card-3"),
                                 $("#player-" + i + "-card-4"),
                                 $("#player-" + i + "-card-5")];
                                 
                $("#player-" + i + "-card-6").remove();
                $("#player-" + i + "-card-7").remove();
            }
        }
    }
    registerHook('restartGame', 'post', removeExtraCards);
    
    // exactly the existing Deck class with PoG support on rigFor
    // Note: please make this less copy-paste later, but I don't know how
    function pogDeck() {
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
            
            var handCards = CARDS_PER_HAND;
            
            // give PoG a seven-royal-flush
            if (players[player] && players[player].id === "pot_of_greed") {
                handCards += 2;
            }
            
            for (var n = 0; n < handCards; n++) {
                var i = cards.length - 1 - (player * CARDS_PER_HAND + n);
                
                // extra two per PoG
                for (var k = 1; k < player; k++) {
                    if (players[k] && players[k].id === "pot_of_greed") {
                        i -= 2;
                    }
                }
                
                for (var j = 0; j < cards.length; j++) {
                    if (cards[j].suit == rigSuit && cards[j].rank == 14 - n && i != j) {
                        var c = cards[i]; cards[i] = cards[j]; cards[j] = c;
                    }
                }
            }
        }
    }
    
    // exactly the existing startDealPhase function with PoG support
    // Note: please make this less copy-paste later, but I don't know how
    function pogStartDealPhase () {
        currentRound++;
        saveTranscriptMessage("Starting round "+(currentRound+1)+"...");

        if (SENTRY_INITIALIZED) {
            Sentry.addBreadcrumb({
                category: 'game',
                message: 'Starting round '+(currentRound+1)+'...',
                level: 'info'
            });
        }
            
        // add the extra PoG cards in round 1
        if (currentRound === 0) {
            addExtraCards();
        }
        
        /* dealing cards */
        dealLock = getNumPlayersInStage(STATUS_ALIVE) * CARDS_PER_HAND;
        for (var i = 0; i < players.length; i++) {
            if (players[i]) {
                /* collect the player's hand */
                chooseCardAmt(i);
                clearHand(i);
                
                if (i !== 0) {
                    $gameOpponentAreas[i-1].removeClass('opponent-revealed-cards opponent-lost');
                }
            }
        }

        activeDeck = new pogDeck();
        activeDeck.shuffle();

        var numPlayers = getNumPlayersInStage(STATUS_ALIVE);

        var n = 0;
        for (var i = 0; i < players.length; i++) {
            if (players[i]) {
                console.log(players[i] + " "+ i);
                if (!players[i].out) {
                    // PoG gets 2 extra
                    if (players[i].id === "pot_of_greed") {
                        dealLock += 2;
                    }
                    
                    /* deal out a new hand to this player */
                    dealHand(i, numPlayers, n++);
                } else {
                    if (HUMAN_PLAYER == i) {
                        $gamePlayerCardArea.hide();
                        $gamePlayerClothingArea.hide();
                    }
                    else {
                        $gameOpponentAreas[i-1].hide();
                    }
                }
            }
        }

        /* IMPLEMENT STACKING/RANDOMIZED TRIGGERS HERE SO THAT AIs CAN COMMENT ON PLAYER "ACTIONS" */

        /* clear the labels */
        for (var i = 0; i < players.length; i++) {
            $gameLabels[i].removeClass("loser tied");
        }
        
        var realDelay = ANIM_DELAY * CARDS_PER_HAND * numPlayers;
        
        for (var i = 0; i < players.length; i++) {
            // PoG gets 2 extra
            if (players[i] && players[i].id === "pot_of_greed" && !players[i].out) {
                realDelay += ANIM_DELAY * 2;
            }
        }

        timeoutID = window.setTimeout(checkDealLock, realDelay + ANIM_TIME);
    }
    registerHook('startDealPhase', 'instead', pogStartDealPhase);
    
    function markAllOfRank(rank, cards, usedCards) {
        for (var i = 0; i < cards.length; i++) {
            if (cards[i].rank === rank) {
                usedCards[i] = true;
            }
        }
    }
    
    function markAllOfSuit(suit, cards, usedCards) {
        for (var i = 0; i < cards.length; i++) {
            if (cards[i].suit === suit) {
                usedCards[i] = true;
            }
        }
    }
    
    function markAllInStraight(topRank, len, cards, usedCards) {
        for (var i = 0; i < cards.length; i++) {
            if (cards[i].rank <= topRank && cards[i].rank > (topRank - len)) {
                usedCards[i] = true;
            }
        }
    }
    
    function hasSameCardsMarked(markCards1, markCards2, requiredNum) {
        var numSame = 0;
        
        for (var i = 0; i < markCards1.length; i++) {
            if (markCards1[i] && markCards2[i]) {
                numSame++;
            }
        }
        
        return (numSame >= requiredNum);
    }
    
    function hasAllCardsMarked(markCards1, markCards2) {
        for (var i = 0; i < markCards1.length; i++) {
            if (!markCards1[i] && !markCards2[i]) {
                return false;
            }
        }
        
        return true;
    }
    
    // exactly the existing Hand.prototype.determine function with PoG support
    // Note: please make this less copy-paste later, but I don't know how
    function pogHandDetermine() {
        /* start by getting a shorthand variable and resetting */
        
        // garbage and easily-broken workaround for no "this" due to hook function
        var hand;
        
        if (i == null) {
            hand = players[currentTurn].hand;
        } else {
            hand = players[i].hand;
        }
        
        // I have no idea why I need to check for this, but...
        if (hand == null) return;
        
        /* look for each strength, in composition */
        var have_pair = [];
        var have_three_kind = [];
        var have_four_kind = 0;
        var have_straight = 0;
        var have_flush = 0;
        var have_six_straight = 0;
        var have_six_flush = 0;
        var have_seven_straight = 0;
        var have_seven_flush = 0;

        /* start by collecting the ranks and suits of the cards */
        hand.ranks = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        hand.suits = [0, 0, 0, 0];
        hand.strength = NONE;
        hand.value = [];
        
        if (hand.cards.length > CARDS_PER_HAND) {
            pairedCards = [false, false, false, false, false, false, false];
            straightCards = [false, false, false, false, false, false, false];
            flushCards = [false, false, false, false, false, false, false];
        } else {
            pairedCards = [false, false, false, false, false];
            straightCards = [false, false, false, false, false];
            flushCards = [false, false, false, false, false];
        }

        hand.cards.forEach(function(card) {
            hand.ranks[card.rank - 1]++;
            hand.suits[card.suit]++;
        }, hand);
        hand.ranks[0] = hand.ranks[13];
        
        /* look for four of a kind, three of a kind, and pairs */
        for (var i = hand.ranks.length-1; i > 0; i--) {
            if (hand.ranks[i] == 4) {
                have_four_kind = i+1;
                markAllOfRank(i+1, hand.cards, pairedCards);
            } else if (hand.ranks[i] == 3) {
                have_three_kind.push(i+1);
                markAllOfRank(i+1, hand.cards, pairedCards);
            } else if (hand.ranks[i] == 2) {
                have_pair.push(i+1);
                markAllOfRank(i+1, hand.cards, pairedCards);
            }
        }
        
        /* determine two/three/four of a kind and combinations thereof */
        if (have_four_kind && have_three_kind.length > 0) {
            hand.strength = FULLEST_HOUSE;
        } else if (have_four_kind && have_pair.length > 0) {
            hand.strength = FULLER_HOUSE;
        } else if (have_three_kind.length >= 2) {
            hand.strength = TWO_THREE_OF_A_KINDS;
        } else if (have_three_kind.length > 0 && have_pair.length >= 2) {
            hand.strength = FULL_HOUSE_PLUS_PAIR;
        } else if (have_four_kind) {
            hand.strength = FOUR_OF_A_KIND;
        } else if (have_three_kind.length > 0 && have_pair.length > 0) {
            hand.strength = FULL_HOUSE;
        } else if (have_pair.length >= 3) {
            hand.strength = THREE_PAIR;
        } else if (have_three_kind.length > 0) {
            hand.strength = THREE_OF_A_KIND;
        } else if (have_pair.length > 0) {
            hand.strength = have_pair.length == 2 ? TWO_PAIR : PAIR;
        }
        
        /* look for straights and flushes */
        /* first, straights */
        var sequence = 0;

        for (var i = 0; i < hand.ranks.length; i++) {
            if (hand.ranks[i] == 1) {
                sequence++;
                if (sequence == CARDS_PER_HAND) {
                    /* one card each of five consecutive ranks is a
                     * straight */
                    have_straight = i+1;
                    markAllInStraight(i+1, CARDS_PER_HAND, hand.cards, straightCards);
                } else if (sequence == CARDS_PER_HAND + 1) {
                    have_six_straight = i+1;
                    markAllInStraight(i+1, CARDS_PER_HAND + 1, hand.cards, straightCards);
                } else if (sequence == CARDS_PER_HAND + 2) {
                    have_seven_straight = i+1;
                    markAllInStraight(i+1, CARDS_PER_HAND + 2, hand.cards, straightCards);
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
        for (var i = 0; i < hand.suits.length; i++) {
            if (hand.suits[i] == CARDS_PER_HAND + 2) {
                have_seven_flush = 1;
                have_six_flush = 1;
                have_flush = 1;
                markAllOfSuit(i, hand.cards, flushCards);
                break;
            } else if (hand.suits[i] == CARDS_PER_HAND + 1) {
                have_six_flush = 1;
                have_flush = 1;
                markAllOfSuit(i, hand.cards, flushCards);
                break;
            } else if (hand.suits[i] == CARDS_PER_HAND) {
                /* hand is a flush */
                have_flush = 1;
                markAllOfSuit(i, hand.cards, flushCards);
                break;
            } else if (hand.suits[i] > 2) {
                /* can't have a flush */
                break;
            }
        }
        
        /* determine all flushes, all straights, and high card. */
        have_straight_plus_pair = !have_six_straight && have_straight && (have_pair.length > 0 || have_three_kind.length > 0) && hasAllCardsMarked(straightCards, pairedCards);
        
        if (have_seven_flush && have_seven_straight == 14) {
            hand.strength = SEVEN_ROYAL_FLUSH;
        } else if (have_seven_flush && have_straight == 14 && have_straight_plus_pair) {
            hand.strength = ROYAL_STRAIGHT_AND_PAIR_FLUSH;
        } else if (have_six_flush && have_six_straight == 14 && hasSameCardsMarked(straightCards, flushCards, 6)) {
            hand.strength = SIX_ROYAL_FLUSH;
        } else if (have_flush && have_straight == 14 && have_straight_plus_pair && hasSameCardsMarked(straightCards, flushCards, 5)) {
            hand.strength = ROYAL_FLUSH_PLUS_PAIR;
        } else if (have_seven_flush && have_seven_straight) {
            hand.strength = SEVEN_STRAIGHT_FLUSH;
        } else if (have_flush && have_straight == 14 && hasSameCardsMarked(straightCards, flushCards, 5)) {
            hand.strength = ROYAL_FLUSH;
        } else if (have_seven_flush && have_straight_plus_pair) {
            hand.strength = STRAIGHT_AND_PAIR_FLUSH;
        } else if (have_six_flush && have_six_straight && hasSameCardsMarked(straightCards, flushCards, 6)) {
            hand.strength = SIX_STRAIGHT_FLUSH;
        } else if (have_flush && have_straight_plus_pair && hasSameCardsMarked(straightCards, flushCards, 5)) {
            hand.strength = STRAIGHT_FLUSH_PLUS_PAIR;
        } else if (have_flush && have_straight && hasSameCardsMarked(straightCards, flushCards, 5)) {
            hand.strength = STRAIGHT_FLUSH;
        } else if (have_straight_plus_pair) {
            hand.strength = STRAIGHT_PLUS_PAIR;
        } else if (have_seven_flush) {
            hand.strength = SEVEN_FLUSH;
        } else if (have_seven_straight) {
            hand.strength = SEVEN_STRAIGHT;
        } else if (have_six_flush) {
            hand.strength = SIX_FLUSH;
        } else if (have_six_straight) {
            hand.strength = SIX_STRAIGHT;
        } else if (have_flush) {
            hand.strength = FLUSH;
        } else if (have_straight) {
            hand.strength = STRAIGHT;
        } else if (hand.strength === NONE) {
            hand.strength = HIGH_CARD;
        }
        
        switch (hand.strength) {
        case PAIR:
        case TWO_PAIR:
        case THREE_PAIR:
            hand.value = have_pair;
            break;
        case THREE_OF_A_KIND:
        case TWO_THREE_OF_A_KINDS:
            hand.value = have_three_kind;
            break;
        case FULL_HOUSE:
            hand.value = [have_three_kind[0], have_pair[0]];
            break;
        case FOUR_OF_A_KIND:
            hand.value = [have_four_kind];
            break;
        case FULL_HOUSE_PLUS_PAIR:
            hand.value = [have_three_kind[0], have_pair[0], have_pair[1]];
            break;
        case FULLER_HOUSE:
            hand.value = [have_four_kind, have_pair[0]];
            break;
        case FULLEST_HOUSE:
            hand.value = [have_four_kind, have_three_kind[0]];
            break;
        case STRAIGHT:
        case STRAIGHT_FLUSH:
        case STRAIGHT_PLUS_PAIR:
        case STRAIGHT_FLUSH_PLUS_PAIR:
        case STRAIGHT_AND_PAIR_FLUSH:
        case ROYAL_FLUSH_PLUS_PAIR:
        case ROYAL_STRAIGHT_AND_PAIR_FLUSH:
            if (have_straight == 5) { // Wheel, special case, sort ace last
                hand.value = [5, 4, 3, 2, 14];
            } else {
                hand.value = [have_straight, have_straight-1, have_straight-2, have_straight-3, have_straight-4];
            }
            
            if (hand.strength !== STRAIGHT && hand.strength !== STRAIGHT_FLUSH) {
                if (have_three_kind.length > 0) {
                    hand.value.push(have_three_kind[0]);
                } else {
                    hand.value.push(have_pair[0]);
                }
            }
            break;
        case SIX_STRAIGHT:
        case SIX_STRAIGHT_FLUSH:
            if (have_six_straight == 6) { // Wheel, special case, sort ace last
                hand.value = [6, 5, 4, 3, 2, 14];
            } else {
                hand.value = [have_six_straight, have_six_straight-1, have_six_straight-2, have_six_straight-3, have_six_straight-4, have_six_straight-5];
            }
            break;
        case SEVEN_STRAIGHT:
        case SEVEN_STRAIGHT_FLUSH:
            if (have_seven_straight == 7) { // Wheel, special case, sort ace last
                hand.value = [7, 6, 5, 4, 3, 2, 14];
            }
            break;
        case ROYAL_FLUSH:
            hand.value = [14, 13, 12, 11, 10];
            break;
        case SIX_ROYAL_FLUSH:
            hand.value = [14, 13, 12, 11, 10, 9];
            break;
        case FLUSH:
        case SIX_FLUSH:
            for (var i = hand.ranks.length-1; i > 0; i--) {
                if (hand.ranks[i] > 0) {
                    for (var j = 0; j < hand.cards.length; j++) {
                        if (hand.cards[j].rank == i+1 && flushCards[j]) {
                            hand.value.push(i+1);
                            break;
                        }
                    }
                }
            }
            break;
        case SEVEN_FLUSH:
        case SEVEN_ROYAL_FLUSH:
            break;
        }
                
        for (var i = hand.ranks.length-1; i > 0; i--) {
            if (hand.ranks[i] == 1) {
                hand.value.push(i+1);
            }
        }

        /* stats for the log */
        console.log();
        console.log("Rank: "+hand.ranks);
        console.log("Suit: "+hand.suits);
        console.log("Player has " +pogHandStrengthToString(hand.strength, hand.cards.length)+" of value "+hand.value);
    }
    registerHook('Hand.prototype.determine', 'instead', pogHandDetermine);
    
    // exactly the existing handStrengthToString function with PoG support
    function pogHandStrengthToString (number, numCards) {
        switch (number) {
            case NONE:                          return undefined;
            case HIGH_CARD:                     return "High card";
            case PAIR:                          return "One pair";
            case TWO_PAIR:                      return "Two pair";
            case THREE_OF_A_KIND:               return "Three of a kind";
            case THREE_PAIR:                    return "Three pair";
            case SIX_STRAIGHT:                  return "Six-card straight";
            case SIX_FLUSH:                     return "Six-card flush";
            case SEVEN_STRAIGHT:                return "Seven-card straight";
            case SEVEN_FLUSH:                   return "Seven-card flush";
            case STRAIGHT_PLUS_PAIR:            return "Straight + pair";
            case FOUR_OF_A_KIND:                return "Four of a kind";
            case FULL_HOUSE_PLUS_PAIR:          return "Full house + pair";
            case TWO_THREE_OF_A_KINDS:          return "Two three-of-a-kinds";
            case FULLER_HOUSE:                  return "4-by-2 full house";
            case FULLEST_HOUSE:                 return "4-by-3 full house";
            case STRAIGHT_FLUSH_PLUS_PAIR:      return "Straight flush + pair";
            case SIX_STRAIGHT_FLUSH:            return "Six-card straight flush";
            case STRAIGHT_AND_PAIR_FLUSH:       return "Straight + pair flush";
            case SEVEN_STRAIGHT_FLUSH:          return "Seven-card straight flush";
            case ROYAL_FLUSH_PLUS_PAIR:         return "Royal flush + pair";
            case SIX_ROYAL_FLUSH:               return "Six-card royal flush";
            case ROYAL_STRAIGHT_AND_PAIR_FLUSH: return "Royal straight + pair flush";
            case SEVEN_ROYAL_FLUSH:             return "Seven-card royal flush";
            
            case STRAIGHT:
                if (numCards > 5) {
                    return "Five-card straight";
                } else {
                    return "Straight";
                }
            case FLUSH:
                if (numCards > 5) {
                    return "Five-card flush";
                } else {
                    return "Flush";
                }
            case FULL_HOUSE:
                if (numCards > 5) {
                    return "3-by-2 full house";
                } else {
                    return "Full house";
                }
            case STRAIGHT_FLUSH:
                if (numCards > 5) {
                    return "Five-card straight flush";
                } else {
                    return "Straight flush";
                }
            case ROYAL_FLUSH:
                if (numCards > 5) {
                    return "Five-card royal flush";
                } else {
                    return "Royal flush";
                }
        }
    }
    
    function pogHandToString() {
        // broken but nothing uses it
        return pogHandStrengthToString(this.strength, this.cards.length);
    }
    registerHook('Hand.prototype.toString', 'instead', pogHandToString);
    
    // exactly the existing handStrengthFromString function with PoG support
    function pogHandStrengthFromString (string) {
        if (!string) return NaN;
        switch (string.trim().toLowerCase()) {
        case "high card":                   return HIGH_CARD;
        case "one pair":                    return PAIR;
        case "two pair":                    return TWO_PAIR;
        case "three of a kind":             return THREE_OF_A_KIND;
        case "three pair":                  return THREE_PAIR;
        case "six-card straight":           return SIX_STRAIGHT;
        case "six-card flush":              return SIX_FLUSH;
        case "seven-card straight":         return SEVEN_STRAIGHT;
        case "seven-card flush":            return SEVEN_FLUSH;
        case "straight + pair":             return STRAIGHT_PLUS_PAIR;
        case "four of a kind":              return FOUR_OF_A_KIND;
        case "full house + pair":           return FULL_HOUSE_PLUS_PAIR;
        case "two three-of-a-kinds":        return TWO_THREE_OF_A_KINDS;
        case "4-by-2 full house":           return FULLER_HOUSE;
        case "4-by-3 full house":           return FULLEST_HOUSE;
        case "straight flush + pair":       return STRAIGHT_FLUSH_PLUS_PAIR;
        case "six-card straight flush":     return SIX_STRAIGHT_FLUSH;
        case "straight + pair flush":       return STRAIGHT_AND_PAIR_FLUSH;
        case "seven-card straight flush":   return SEVEN_STRAIGHT_FLUSH;
        case "royal flush + pair":          return ROYAL_FLUSH_PLUS_PAIR;
        case "six-card royal flush":        return SIX_ROYAL_FLUSH;
        case "royal straight + pair flush": return ROYAL_STRAIGHT_AND_PAIR_FLUSH;
        case "seven-card royal flush":      return SEVEN_ROYAL_FLUSH;
        
        case "straight":
        case "five-card straight":
            return STRAIGHT;
        case "flush":
        case "five-card flush":
            return FLUSH;
        case "full house":
        case "3-by-2 full house":
            return FULL_HOUSE;
        case "straight flush":
        case "five-card straight flush":
            return STRAIGHT_FLUSH;
        case "royal flush":
        case "five-card royal flush":
            return ROYAL_FLUSH;
        }
        return NaN;
    }
    registerHook('handStrengthFromString', 'instead', pogHandStrengthFromString);
    
    function pogDescribeHand(with_article) {
        // garbage and easily-broken workaround for no "this" due to hook function
        var hand = player.hand;
        
        var use_article = false;
        var description;
        switch (hand.strength) {
        case NONE:
            break;
        case HIGH_CARD:
            description = cardRankToString(hand.value[0], false) + " high"; break;
        case PAIR:
            description = "pair of " + cardRankToString(hand.value[0]);
            use_article = true; break;
        case TWO_PAIR:
            description = "two pair of "
                + cardRankToString(hand.value[0]) + " and "
                + cardRankToString(hand.value[1]);
            break;
        case THREE_OF_A_KIND:
            description = "three " + cardRankToString(hand.value[0]); break;
        case THREE_PAIR:
            description = "three pair of "
                + cardRankToString(hand.value[0]) + ", "
                + cardRankToString(hand.value[1]) + ", and "
                + cardRankToString(hand.value[2]);
            break;
        case STRAIGHT_PLUS_PAIR:
            description = "straight and a pair of " + cardRankToString(hand.value[5]);
            use_article = true; break;
        case FOUR_OF_A_KIND:
            description = "four " + cardRankToString(hand.value[0]); break;
        case FULL_HOUSE_PLUS_PAIR:
            description = "full house and a pair of " + cardRankToString(hand.value[2]);
            use_article = true; break;
        case TWO_THREE_OF_A_KINDS:
            description = "two three-of-a-kinds of "
                + cardRankToString(hand.value[0]) + " and "
                + cardRankToString(hand.value[1]);
            break;
        case FULLER_HOUSE:
            description = "fuller house";
            use_article = true; break;
        case FULLEST_HOUSE:
            description = "fullest house";
            use_article = true; break;
        case STRAIGHT_FLUSH_PLUS_PAIR:
            description = "straight flush and a pair of " + cardRankToString(hand.value[5]);
            use_article = true; break;
        case STRAIGHT_AND_PAIR_FLUSH:
            description = "straight and a pair of " + cardRankToString(hand.value[5]) + ", flush";
            use_article = true; break;
        case ROYAL_FLUSH_PLUS_PAIR:
            description = "royal flush and a pair of " + cardRankToString(hand.value[5]);
            use_article = true; break;
        case ROYAL_STRAIGHT_AND_PAIR_FLUSH:
            description = "royal flush and a pair of " + cardRankToString(hand.value[5]) + ", also flush";
            use_article = true; break;
        default:
            description = pogHandStrengthToString(hand.strength, hand.cards.length).toLowerCase();
            use_article = true;
        }
        if (with_article && use_article) {
            return (description[0] == 'a' || description[0] == 'e' ? 'an ' : 'a ') + description;
        } else return description;
    }
    registerHook('Hand.prototype.describe', 'instead', pogDescribeHand);
    
    function pogDescribeHandFormal(hand) {
        var description = pogHandStrengthToString(hand.strength, hand.cards.length) + ', ';
        switch (hand.strength) {
        case NONE:
            description = undefined;
        case HIGH_CARD:
            description += cardRankToString(hand.value[0], false); break;
        case PAIR:
        case THREE_OF_A_KIND:
            description += cardRankToString(hand.value[0]); break;
        case TWO_PAIR:
        case TWO_THREE_OF_A_KINDS:
            description += cardRankToString(hand.value[0]) + " over "
                + cardRankToString(hand.value[1], true);
            break;
        case STRAIGHT:
        case FLUSH:
        case SIX_STRAIGHT:
        case SIX_FLUSH:
        case SEVEN_STRAIGHT:
        case SEVEN_FLUSH:
        case STRAIGHT_FLUSH:
        case SIX_STRAIGHT_FLUSH:
        case SEVEN_STRAIGHT_FLUSH:
            description += cardRankToString(hand.value[0], false) + ' high'; break;
        case THREE_PAIR:
            description += cardRankToString(hand.value[0]) + " over "
                + cardRankToString(hand.value[1], true) + " over "
                + cardRankToString(hand.value[2], true);
            break;
        case FULL_HOUSE:
        case FULLER_HOUSE:
        case FULLEST_HOUSE:
            description += cardRankToString(hand.value[0]) + " full of "
                + cardRankToString(hand.value[1]); break;
        case STRAIGHT_PLUS_PAIR:
            description = "Straight, " + cardRankToString(hand.value[0], false) + ' high, ';
            description += "over pair, " + cardRankToString(hand.value[5]); break;
        case FOUR_OF_A_KIND:
            description += cardRankToString(hand.value[0]); break;
        case FULL_HOUSE_PLUS_PAIR:
            description = "Full house, " + cardRankToString(hand.value[0]) + " full of "
                + cardRankToString(hand.value[1]) + ", ";
            description += "over pair, " + cardRankToString(hand.value[2]); break;
        case STRAIGHT_FLUSH_PLUS_PAIR:
            description = "Straight flush, " + cardRankToString(hand.value[0], false) + ' high,';
            description += "over pair, " + cardRankToString(hand.value[5]); break;
        case STRAIGHT_AND_PAIR_FLUSH:
            description = "Straight flush, " + cardRankToString(hand.value[0], false) + ' high,';
            description += "over pair, " + cardRankToString(hand.value[5]) + ", also flush"; break;
        case ROYAL_FLUSH_PLUS_PAIR:
            description = "Royal flush over pair, " + cardRankToString(hand.value[5]); break;
        case ROYAL_STRAIGHT_AND_PAIR_FLUSH:
            description = "Royal flush over pair, " + cardRankToString(hand.value[5]) + ", also flush"; break;
        // Royal Flush needs no further description
        }
        return description;
    }
    
    function pogShowHand (player) {
        displayHand(player, true);
        resetCardAmt(); // couldn't do as hook since it would break exchangeCards
        
        if (player > 0) {
            $gameOpponentAreas[player-1].attr('data-original-title', pogDescribeHandFormal(players[player].hand));
            if (EXPLAIN_ALL_HANDS) $gameOpponentAreas[player-1].tooltip('show');
        } else {
            $gamePlayerCardArea.attr('data-original-title', pogDescribeHandFormal(players[player].hand));
            if (EXPLAIN_ALL_HANDS) $gamePlayerCardArea.tooltip('show');
        }
    }
    registerHook('showHand', 'instead', pogShowHand);

    // exactly the existing determineAIAction function with PoG support
    // Note: please make this less copy-paste later, but I don't know how
    function pogDetermineAIAction (player) {
        var eStrategies = {
            OPTIMAL    : "optimal",
            SUBOPTIMAL : "suboptimal",
            RANDOM     : "random",
            WORST      : "worst",
            KEEPALL    : "keep-all"
        };
        
        var numCards = CARDS_PER_HAND;
        
        if (player.id === "pot_of_greed") {
            numCards += 2;
        }

        /* Choose a strategy for the hand based on intelligence. Always use suboptimal for the player for card suggestions. */
        var strategy = eStrategies.KEEPALL;
        if (player.id == 'human') {
            strategy = eStrategies.SUBOPTIMAL;
        } else {
          var AIRoll = Math.random();
          switch (player.intelligence) {
            case eIntelligence.NOSWAP:
              strategy = eStrategies.KEEPALL;
              break;
            case eIntelligence.BEST:
              if (AIRoll > 0.01) {
                  strategy = eStrategies.OPTIMAL;
              } else {
                  strategy = eStrategies.RANDOM;
              }
              break;
            case eIntelligence.GOOD:
              if (AIRoll > 0.5) {
                  strategy = eStrategies.OPTIMAL;
              } else if (AIRoll > 0.1) {
                  strategy = eStrategies.SUBOPTIMAL;
              } else {
                  strategy = eStrategies.RANDOM;
              }
              break;
            case eIntelligence.AVERAGE:
              if (AIRoll > 0.6) {
                  strategy = eStrategies.SUBOPTIMAL;
              } else {
                  strategy = eStrategies.RANDOM;
              }
              break;
            case eIntelligence.BAD:
              if (AIRoll > 0.85) {
                  strategy = eStrategies.SUBOPTIMAL;
              } else if (AIRoll > 0.35) {
                  strategy = eStrategies.RANDOM;
              } else {
                  strategy = eStrategies.WORST;
              }
              break;
            case eIntelligence.THROW:
              if (AIRoll > 0.99) {
                  strategy = eStrategies.RANDOM;
              } else {
                  strategy = eStrategies.WORST;
              }
              break;
            default:
              console.log("No intelligence match found for " + player.id + ". Defaulting to no-swap.");
          }
        }
        
        /* determine the current hand */
        player.hand.determine();
        
        /* collect the ranks and suits of the cards */
        var hand = player.hand.cards;

        /* Player tries hard to lose */
        /* NO NEED FOR POG SUPPORT - POG IS AVERAGE */
        if (strategy == eStrategies.WORST) {
            if (player.hand.strength == STRAIGHT || player.hand.strength == FLUSH || player.hand.strength >= STRAIGHT_FLUSH) {
                var sortedRanks = hand.map(function(c) { return c.rank; }).sort();
                // Keep the two lowest cards.
                player.hand.tradeIns = hand.map(function(c) { return c.rank != sortedRanks[0] && c.rank != sortedRanks[1]; })
                return;
            }
            
            if (player.id === "pot_of_greed") {
                player.hand.tradeIns = [false, false, false, false, false, false, false];
            } else {
                player.hand.tradeIns = [false, false, false, false, false];
            }
            
            for (var i = 0; i < hand.length; i++) {
                if (hand[i].rank >= 12) {
                    player.hand.tradeIns[i] = true;
                } else if (player.hand.strength >= PAIR) {
                    for (var j = i + 1; j < hand.length; j++) {
                        if (hand[i].rank == hand[j].rank) {
                            // Discard this card if there's another card
                            // of the same rank (keeping the last card of
                            // each rank, as long as it's below queen.
                            player.hand.tradeIns[i] = true;
                            break;
                        }
                    }
                }
            }

        /*for random strategy users all trades are done at random. Technically this is the same as doing nothing but this way they won't always just do nothing.*/
        } else if (strategy == eStrategies.RANDOM) {
            if (player.id === "pot_of_greed") {
                player.hand.tradeIns = [false, false, false, false, false, false, false];
            } else {
                player.hand.tradeIns = [false, false, false, false, false];
            }

            /*choose number of cards to trade in*/
            var toTrade = Math.floor((Math.random()) * (numCards + 1));

            /*choose cards at random to get rid of*/
            for (var i = 0; i < hand.length; i++) {
            /*set it to trade in cards randomly until we have to trade in cards*/
                if(Math.floor((Math.random()) * 2 )==1 || toTrade + i >= hand.length){
                    player.hand.tradeIns[i] = true;
                    toTrade--;
                }
            }

        /*for keep-all strategy users just don't swap any cards*/
        } else if (strategy == eStrategies.KEEPALL) {
            if (player.id === "pot_of_greed") {
                player.hand.tradeIns = [false, false, false, false, false, false, false];
            } else {
                player.hand.tradeIns = [false, false, false, false, false];
            }

        /*optimal strategy users only attempt to get pairs or improve on pairs*/
        /*suboptimal strategy users use the standard algorithm. Suboptimal strategy is also the default case*/
        } else {
            /* if the current hand is good enough, then take a pre-determined action */
            if (player.hand.strength >= STRAIGHT) {
                /* give up nothing */
                if (player.id === "pot_of_greed") {
                    player.hand.tradeIns = [false, false, false, false, false, false, false];
                } else {
                    player.hand.tradeIns = [false, false, false, false, false];
                }
                
                console.log("Hand is really good, will trade in nothing. "+player.hand.tradeIns);
                return;
            }
            
            /* if the current hand is good enough, then take a pre-determined action */
            if (player.hand.strength == THREE_OF_A_KIND) {
                /* Keep the three cards (rank value[0]) - discard the rest */
                player.hand.tradeIns = hand.map(function(c) { return c.rank != player.hand.value[0]; });
                console.log("Hand is good, will trade in two cards. "+player.hand.tradeIns);
                return;
            }
            
            /* if the current hand is good enough, then take a pre-determined action */
            if (player.hand.strength == TWO_PAIR) {
                /* Discard the odd card (value[2]) */
                player.hand.tradeIns = hand.map(function(c) { return c.rank != player.hand.value[0] && c.rank != player.hand.value[1]; });
                console.log("Hand is good, will trade in one card. "+player.hand.tradeIns);
                return;
            }

            if (strategy == eStrategies.SUBOPTIMAL) {
                /* Check for flush draw, even if holding a pair */
                /* CARDS_PER_HAND instead of numCards is not a mistake, since 5-flushes are possible */
                if (player.hand.suits.some(function(s) { return s == CARDS_PER_HAND - 1; })) {
                    player.hand.tradeIns = hand.map(function(c) { return player.hand.suits[c.suit] == 1; });
                    console.log("Hand is " + (player.hand.strength == PAIR ? "okay" : "bad") + ", going for a flush, trading in one card. " + player.hand.tradeIns);
                    return;
                }

                for (var start_rank = 2; start_rank <= 10; start_rank++) {
                    /* Outside straight draw - four ranks in a row */
                    if (player.hand.ranks.slice(start_rank - 1, start_rank - 1 + 4).countTrue() == 4) {
                        player.hand.tradeIns = hand.map(function(c, idx) {
                            return c.rank < start_rank || c.rank > start_rank + 3
                                || hand.slice(0, idx).some(function(c2) { return c2.rank == c.rank; });
                        });
                        console.log("Hand is " + (player.hand.strength == PAIR ? "okay" : "bad") + ", going for a straight, trading in one card. "+player.hand.tradeIns);
                        return;
                    }
                }
                for (var start_rank = 10; start_rank >= 1; start_rank--) {
                    /* Inside straight draw - four ranks out of five in a row */
                    if (player.hand.ranks.slice(start_rank - 1, start_rank - 1 + 5).countTrue() == 4) {
                        player.hand.tradeIns = hand.map(function(c, idx) {
                            return ((c.rank < start_rank || c.rank > start_rank + 4) && !(start_rank == 1 && c.rank == 14))
                                || hand.slice(0, idx).some(function(c2) { return c2.rank == c.rank; });
                        });
                        console.log("Hand is " + (player.hand.strength == PAIR ? "okay" : "bad") + ", going for a straight, trading in one card. "+player.hand.tradeIns);
                        return;
                    }
                }
            }

            if (player.hand.strength == PAIR) {
                /* Keep the pair (rank = value[0]), discard the rest */
                player.hand.tradeIns = hand.map(function(c) { return c.rank != player.hand.value[0]; });
                console.log("Hand is okay, trading in three cards. "+player.hand.tradeIns);
                return;
            }

            if (strategy == eStrategies.SUBOPTIMAL) {
                for (var start_rank = 2; start_rank <= 11; start_rank++) {
                    if (player.hand.ranks.slice(start_rank - 1, start_rank - 1 + 3).countTrue() == 3) {
                        player.hand.tradeIns = hand.map(function(c, idx) {
                            return c.rank < start_rank || c.rank > start_rank + 2;
                        });
                        console.log("Hand is bad, going for a straight, trading in two cards. "+player.hand.tradeIns);
                        return;
                    }
                }
            }
            if (player.hand.strength == HIGH_CARD) {
                if (strategy == eStrategies.SUBOPTIMAL) {
                    player.hand.tradeIns = hand.map(function(c) { return player.hand.value.slice(0, AVERAGE_KEEP_HIGH).indexOf(c.rank) < 0; });
                    console.log("Hand is bad, trading in "+ (numCards - AVERAGE_KEEP_HIGH) +" cards. "+player.hand.tradeIns);
                    return;
                } else if (strategy != eStrategies.OPTIMAL || player.hand.value[0] >= 10) {
                    player.hand.tradeIns = hand.map(function(c) { return c.rank != player.hand.value[0]; });
                    console.log("Hand is bad, trading in four cards. "+player.hand.tradeIns);
                    return;
                }
            }
            
            /* end of function, otherwise just trade in everything */
            if (player.id === "pot_of_greed") {
                player.hand.tradeIns = [true, true, true, true, true, true, true];
            } else {
                player.hand.tradeIns = [true, true, true, true, true];
            }
            
            console.log("Hand is horrid, trading in everything. "+player.hand.tradeIns);
            return;
        }
    
    }
    registerHook('determineAIAction', 'instead', pogDetermineAIAction);

    return exports;
}(this));