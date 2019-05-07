/********************************************************************************
 This file contains the variables and functions that form the poker mechanics and
 store information on the current poker state of each player. Anything regarding
 cards, except AI decisions, is part of this file (even UI).
 ********************************************************************************/
 
/**********************************************************************
 *****               Poker Hand Object Specification              *****
 **********************************************************************/
 
/* suit names */
var SPADES   = "spade";
var HEARTS   = "heart";
var CLUBS    = "clubs";
var DIAMONDS = "diamo";

/* hand strengths */
var NONE			= 0;
var HIGH_CARD 		= 1;
var PAIR			= 2;
var TWO_PAIR		= 3;
var THREE_OF_A_KIND	= 4;
var STRAIGHT		= 5;
var FLUSH			= 6;
var FULL_HOUSE		= 7;
var FOUR_OF_A_KIND	= 8;
var STRAIGHT_FLUSH	= 9;
var ROYAL_FLUSH 	= 10;

/************************************************************
 * Stores information on a poker hand.
 ************************************************************/
function createNewHand (cards, strength, value, tradeIns) {
	var newHandObject = {cards:cards, 
						 strength:strength, 
						 value:value, 
						 tradeIns:tradeIns};
						  
	return newHandObject;
}
 
/**********************************************************************
 *****                      Poker UI Elements                     *****
 **********************************************************************/

/* hidden cards and hidden card areas */
$gameHiddenArea = $('#game-hidden-area');
$hiddenLargeCard = $('#hidden-large-card');
 
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
var BLANK_CARD_IMAGE = IMG + "blankcard.jpg";
var UNKNOWN_CARD_IMAGE = IMG + "unknown.jpg";
 
/* card decks */
var inDeck = [];	/* cards left in the deck */
var outDeck = [];	/* cards waiting to be shuffled into the deck */

/* deal lock */
var dealLock = 0;

/************************************************************
 * Card class
 * (Ace represented as the number 14 so that the hand value 
 * array can be used to determine the cards to discard.)
 ************************************************************/
function Card(suit, rank) {
	this.suit = suit;
	this.rank = rank;
}

/* This toString() method means that using a card object in a URL
 * yields the same filename as before */
Card.prototype.toString = function() {
	return ["spade", "heart", "clubs", "diamo"][this.suit] + (this.rank == 14 ? 1 : this.rank);
}

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


/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Sets up all of the information needed to start playing poker.
 ************************************************************/
function setupPoker () {
    /* set up the player hands */
    players.forEach(function(player) {
        player.hand = new Hand()
    });
    
    /* compose a new deck */
    composeDeck();
}
 
/************************************************************
 * Composes a brand new deck of cards.
 ************************************************************/
function composeDeck () {
	inDeck = [];
    outDeck = [];
	var suit = "";
	
	for (var i = 0; i < 4; i++) {
		for (j = 2; j <= 14; j++) {
			inDeck.push(new Card(i, j));
		}
	}
}

/************************************************************
 * Prefetches all card images
 ************************************************************/
function preloadCardImages () {
	[SPADES, HEARTS, CLUBS, DIAMONDS].forEach(function(suit) {
		for (var r = 1; r < 14; r++) {
			new Image().src = IMG + suit + r + '.jpg';
		}
	});
	new Image().src = BLANK_CARD_IMAGE;
	new Image().src = UNKNOWN_CARD_IMAGE;
}

/**********************************************************************
 *****                       UI Functions                         *****
 **********************************************************************/
 
/************************************************************
 * Sets the given card to full opacity.
 ************************************************************/
function fillCard (player, card) {
    $cardCells[player][card].css({opacity: 1});
}

/************************************************************
 * Sets the given card to a lower opacity.
 ************************************************************/
function dullCard (player, card) {
    $cardCells[player][card].css({opacity: 0.4});
}

/************************************************************
 * Removes a card from display
 ************************************************************/
function clearCard (player, i) {
    $cardCells[player][i].css('visibility', 'hidden');
    $cardCells[player][i].attr('src', BLANK_CARD_IMAGE);
}

/************************************************************
 * Displays a card, face up or face down, or an empty space
 * if the card is missing.
 ************************************************************/
function displayCard (player, i, visible) {
    if (players[player].hand.cards[i]) {
        if (visible) {
            $cardCells[player][i].attr('src', IMG + players[player].hand.cards[i] + ".jpg");
        } else {
            $cardCells[player][i].attr('src', UNKNOWN_CARD_IMAGE);
        }
        fillCard(player, i);
        $cardCells[player][i].css('visibility', '');
    } else {
        clearCard(player, i);
    }
}

/************************************************************
 * Shows the given player's hand at full opacity.
 ************************************************************/
function showHand (player) {
    displayHand(player, true);
}

/************************************************************
 * Renders the given player's hand
 ************************************************************/
function displayHand (player, visible) {
    for (var i = 0; i < CARDS_PER_HAND; i++) {
        displayCard(player, i, visible);
    }
}

/************************************************************
 * Clears the given player's hand (in preparation of a new game).
 ************************************************************/
function clearHand (player) {
    for (var i = 0; i < CARDS_PER_HAND; i++) {
        clearCard(player, i);
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
 * Collects the given player's hand into the outDeck.
 ************************************************************/
function collectPlayerHand (player) {
	/* collect cards from the hand into the outDeck */
	for (var i = 0; i < CARDS_PER_HAND; i++) {
		if (players[player].hand.cards[i]) {
			outDeck.push(players[player].hand.cards[i]);
		}
		delete players[player].hand.cards[i];
	}
	clearHand(player);
}

/************************************************************
 * Shuffles the outDeck into the inDeck.
 ************************************************************/
function shuffleDeck () {
	/* shuffle the cards from the outDeck into the inDeck */
	for (var i = 0; i < outDeck.length; i++) {
        inDeck.push(outDeck[i]);
	}
	
	/* empty the outDeck */
	outDeck = [];
}

/************************************************************
 * Deals new cards to the given player.
 ************************************************************/
function dealHand (player, numPlayers, playersBefore) {
	/* collect their old hand */
	collectPlayerHand (player);
	
	/* first make sure the deck has enough cards */
	if (inDeck.length < CARDS_PER_HAND) {
		shuffleDeck();
	}
	
	/* deal the new cards */
	var drawnCard;
	for (var i = 0; i < CARDS_PER_HAND; i++) {
		drawnCard = getRandomNumber(0, inDeck.length);
		players[player].hand.cards[i] = inDeck[drawnCard];
		inDeck.splice(drawnCard, 1);
		// Simulate dealing one card to each player, then another to
		// each player, and so on.
		animateDealtCard(player, i, numPlayers * i + playersBefore);
	}
}

/************************************************************
 * Exchanges the chosen cards for the given player.
 ************************************************************/
function exchangeCards (player) {
	/* determine how many cards are being swapped */
	var swap = 0;
	for (var i = 0; i < CARDS_PER_HAND; i++) {
		if (players[player].hand.tradeIns[i]) {
			swap++;
		}
	}
	
	/* make sure the deck has enough cards */
	if (inDeck.length < swap) {
		shuffleDeck();
	}
	
	/* collect their old cards */
	for (var i = 0; i < CARDS_PER_HAND; i++) {
		if (players[player].hand.tradeIns[i] && players[player].hand.cards[i]) {
			outDeck.push(players[player].hand.cards[i]);
			delete players[player].hand.cards[i];
			players[player].hand.tradeIns[i] = false;
		}
	}

    /* Move kept cards to the left */
    players[player].hand.cards = players[player].hand.cards.filter(function(c) { return c; });
    /* Refresh display. */
    displayHand(player, player == HUMAN_PLAYER);
	
    /* take the new cards */
    var n = 0;
    var drawnCard;
    for (var i = players[player].hand.cards.length; i < CARDS_PER_HAND; i++) {
        drawnCard = getRandomNumber(0, inDeck.length);
        players[player].hand.cards.push(inDeck[drawnCard]);
        animateDealtCard(player, i, n++);
        inDeck.splice(drawnCard, 1);
    }
}

/**********************************************************************
 *****                    Animation Functions                     *****
 **********************************************************************/

/************************************************************
 * Animates a small card into a player's hand.  n is the card's number
 * in the order dealt, used to calculate the initial delay.
 ************************************************************/
function animateDealtCard (player, card, n) {
	$clonedCard = $hiddenLargeCard.clone().prependTo($gameHiddenArea);
	$clonedCard.addClass("shown-card");
	$clonedCard.attr('id', 'dealt-card-'+player+'-'+card);
	
	if (player == HUMAN_PLAYER) {
      $clonedCard.addClass("large-dealt-card");
	} else {
      $clonedCard.addClass("small-dealt-card");
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
		var distance = offsetDistance($clonedCard.offset(), {left: left, top: top});
		var animTime = distance / speed;
	}

	$clonedCard.delay(n * ANIM_DELAY).animate({top: top, left: left}, animTime, function() {
		$('#dealt-card-'+player+'-'+card).remove();
		displayCard(player, card, player == HUMAN_PLAYER);
		dealLock++;
	});
}

/************************************************************
 * Get the farthest distance between any player card and the dealer
 ************************************************************/
function getFarthestDealDistance()
{
	return offsetDistance($('#player-4-card-5').offset(), $('#hidden-large-card').offset());
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
		case NONE: 				return "Nothing";
		case HIGH_CARD: 		return "High Card";
		case PAIR: 				return "One Pair";
		case TWO_PAIR: 			return "Two Pair";
		case THREE_OF_A_KIND: 	return "Three of a Kind";
		case STRAIGHT: 			return "Straight";
		case FLUSH: 			return "Flush";
		case FULL_HOUSE: 		return "Full House";
		case FOUR_OF_A_KIND:	return "Four of a Kind";
		case STRAIGHT_FLUSH: 	return "Straight Flush";
		case ROYAL_FLUSH: 		return "Royal Flush";
	}
}
 
/************************************************************
 * Returns the player number with the lowest hand.
 ************************************************************/
function determineLowestHand () {
	var lowestStrength = 11;
	var lowestPlayers = [];
	console.log();
    
	for (i = 0; i < players.length; i++) {
		if (players[i] && !players[i].out) {
			if (players[i].hand.strength < lowestStrength) {
				lowestStrength = players[i].hand.strength;
				lowestPlayers = [i];
			} else if (players[i].hand.strength == lowestStrength) {
				lowestPlayers.push(i);
			}
		}
	}
    console.log("Start of lowest hand determination, currently: "+lowestPlayers);
	
	if (lowestPlayers.length == 1) {
		return lowestPlayers[0];
	}

	/* need to break a tie */
	var maxTieBreakers = players[lowestPlayers[0]].hand.value.length;

	for (var currentTieBreaker = 0; currentTieBreaker < maxTieBreakers; currentTieBreaker++) {
		var lowestValue = 15;
		var tiedPlayers = lowestPlayers;
		console.log("Players Tied: "+tiedPlayers);

		for (var i = 0; i < tiedPlayers.length; i++) {
			if (players[tiedPlayers[i]].hand.value[currentTieBreaker] < lowestValue) {
				lowestValue = players[tiedPlayers[i]].hand.value[currentTieBreaker];
				console.log("Player "+tiedPlayers[i]+" is the new lowest with: "+players[tiedPlayers[i]].hand.value[currentTieBreaker]);
				lowestPlayers = [tiedPlayers[i]];
			} else if (players[tiedPlayers[i]].hand.value[currentTieBreaker] == lowestValue) {
				lowestPlayers.push(tiedPlayers[i]);
				console.log("Player "+tiedPlayers[i]+" is tied with: "+players[tiedPlayers[i]].hand.value[currentTieBreaker]);
			}
		}

		if (lowestPlayers.length == 1) {
			return lowestPlayers[0];
		}
	}

	/* unresolvable tie */
	return lowestPlayers;
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
			this.value = [have_three_kind];
		} else if (have_three_kind) {
			this.strength = THREE_OF_A_KIND;
			this.value = [have_three_kind];
		} else if (have_pair.length > 0) {
			this.strength = have_pair.length == 2 ? TWO_PAIR : PAIR;
			this.value = have_pair;
			
			for (var i = this.ranks.length-1; i > 0; i--) {
				if (this.ranks[i] == 1) {
					this.value.push(i+1);
				}
			}
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
		
		/* determine royal flush, straight flush, flush, straight, and high card */
		if (have_flush && have_straight == 14) {
			this.strength = ROYAL_FLUSH;
			this.value = [have_straight];
		} else if (have_flush && have_straight) {
			this.strength = STRAIGHT_FLUSH;
			this.value = [have_straight];
		} else if (have_straight) {
			this.strength = STRAIGHT;
			this.value = [have_straight];
		} else {
			this.strength = (have_flush ? FLUSH : HIGH_CARD);
			this.value = [];
			
			for (var i = this.ranks.length-1; i > 0; i--) {
				if (this.ranks[i] == 1) {
					this.value.push(i+1);
				}
			}
		}
	}

	/* stats for the log */
    console.log();
	console.log("Rank: "+this.ranks);
	console.log("Suit: "+this.suits);
	console.log("Player has " +handStrengthToString(this.strength)+" of value "+this.value);
}
