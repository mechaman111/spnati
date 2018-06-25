/********************************************************************************
 This file contains the variables and functions that form the AI of the 
 non-player characters.
 ********************************************************************************/
 
/**********************************************************************
 *****                      AI Action Functions                   *****
 **********************************************************************/
 
/************************************************************
 * Uses a basic poker AI to exchange cards.
 * player is an object
 ************************************************************/
function determineAIAction (player) {
	/* determine the current hand */
	determineHand(player);
	
	/* collect the ranks and suits of the cards */
	var hand = player.hand.cards;
	var cardRanks = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
	var cardSuits = [0, 0, 0, 0];
	
	/*for low intelligence characters all trades are done at random. Technically this is the same as doing nothing but this way they won't always just do nothing.*/
	if (player.getIntelligence() == "bad") {
		player.hand.tradeIns = [false, false, false, false, false];

		/*choose number of cards to trade in*/
		var toTrade = Math.floor((Math.random()) * 6);

		/*choose cards at random to get rid of*/
		for (var i = 0; i < hand.length; i++) {
		/*set it to trade in cards randomly until we have to trade in cards*/
			if(Math.floor((Math.random()) * 2 )==1 || toTrade + i >= hand.length){
				player.hand.tradeIns[i] = true;
				toTrade--;
			}
		}

	/*for good intelligence characters only attempt to get pairs or improve on pairs*/
	} else if(player.getIntelligence() == "good") {
		for (var i = 0; i < hand.length; i++) {
			cardRanks[getCardValue(hand[i]) - 1]++;
			if (getCardValue(hand[i]) == 1) {
				cardRanks[13]++;
			}
			cardSuits[getCardSuitValue(hand[i])]++;
		}
		
		/* if the current hand is good enough, then take a pre-determined action */
		if (player.hand.strength >= STRAIGHT) {
			/* give up nothing */
			player.hand.tradeIns = [false, false, false, false, false];
			console.log("Hand is really good, will trade in nothing. "+player.hand.tradeIns);
			return;
		}
		
		/* if the current hand is good enough, then take a pre-determined action */
		if (player.hand.strength == THREE_OF_A_KIND) {
			/* give up the odd cards out */
			var sameValue = 0;
			for (var i = 0; i < cardRanks.length; i++) {
				if (cardRanks[i] == 3) {
					sameValue = i+1;
					break;
				}
			}
			
			player.hand.tradeIns = [false, false, false, false, false];
			for (var i = 0; i < hand.length; i++) {
				if (getCardValue(hand[i]) != sameValue) {
					player.hand.tradeIns[i] = true;
				}
			}
			console.log("Hand is good, will trade in two cards. "+player.hand.tradeIns);
			return;
		}
		
		/* if the current hand is good enough, then take a pre-determined action */
		if (player.hand.strength == TWO_PAIR) {
			/* give up the odd card out */
			var sameValue = [0, 0];
			for (var i = 1; i < cardRanks.length; i++) {
				if (cardRanks[i] == 2) {
					if (sameValue[0] == 0) {
						sameValue[0] = i+1;
					} else {
						sameValue[1] = i+1;
					}
				}
			}
	        
			player.hand.tradeIns = [false, false, false, false, false];
			for (var i = 0; i < hand.length; i++) {
				if (getCardValue(hand[i]) != sameValue[0] && getCardValue(hand[i]) != sameValue[1]) {
					player.hand.tradeIns[i] = true;
				}
			}
			console.log("Hand is good, will trade in one card. "+player.hand.tradeIns);
			return;
		}
		
		
		
		/* now, take each remaining hand into special consideration */
		if (player.hand.strength == PAIR) {
			
			/* otherwise, give up every other card */
			var sameValue = 0;
			for (var i = 0; i < cardRanks.length; i++) {
				if (cardRanks[i] == 2) {
					sameValue = i+1;
					break;
				}
			}
			
			player.hand.tradeIns = [false, false, false, false, false];
			for (var i = 0; i < hand.length; i++) {
				if (getCardValue(hand[i]) != sameValue) {
					player.hand.tradeIns[i] = true;
				}
			}
			console.log("Hand is okay, trading in three cards. "+player.hand.tradeIns);
			return;
		}
		
		/* now, take each remaining hand into special consideration */
		if (player.hand.strength == HIGH_CARD) {
			
			/* otherwise, give up everything but the high card */
			var highCard = 0;
			for (var i = cardRanks.length-1; i > 0; i--) {
				if (cardRanks[i] == 1) {
					if (i == 13) {
						highCard = 1;
					} else {
						highCard = i+1;
					}
					break;
				}
			}
			
			player.hand.tradeIns = [false, false, false, false, false];
			for (var i = 0; i < hand.length; i++) {
				if (getCardValue(hand[i]) != highCard) {
					player.hand.tradeIns[i] = true;
				}
			}
			console.log("Hand is bad, trading in four cards. "+player.hand.tradeIns);
			return;
		}
		
		/* end of function, otherwise just trade in everything */
		player.hand.tradeIns = [true, true, true, true, true];
		console.log("Hand is horrid, trading in everything. "+player.hand.tradeIns);
		return;
	


	/*for average intelligence characters use the standard algorithm. Average intelligence is also the default case*/
	} else {
		for (var i = 0; i < hand.length; i++) {
			cardRanks[getCardValue(hand[i]) - 1]++;
			if (getCardValue(hand[i]) == 1) {
				cardRanks[13]++;
			}
			cardSuits[getCardSuitValue(hand[i])]++;
		}
		
		/* if the current hand is good enough, then take a pre-determined action */
		if (player.hand.strength >= STRAIGHT) {
			/* give up nothing */
			player.hand.tradeIns = [false, false, false, false, false];
			console.log("Hand is really good, will trade in nothing. "+player.hand.tradeIns);
			return;
		}
		
		/* if the current hand is good enough, then take a pre-determined action */
		if (player.hand.strength == THREE_OF_A_KIND) {
			/* give up the odd cards out */
			var sameValue = 0;
			for (var i = 0; i < cardRanks.length; i++) {
				if (cardRanks[i] == 3) {
					sameValue = i+1;
					break;
				}
			}
			
			player.hand.tradeIns = [false, false, false, false, false];
			for (var i = 0; i < hand.length; i++) {
				if (getCardValue(hand[i]) != sameValue) {
					player.hand.tradeIns[i] = true;
				}
			}
			console.log("Hand is good, will trade in two cards. "+player.hand.tradeIns);
			return;
		}
		
		/* if the current hand is good enough, then take a pre-determined action */
		if (player.hand.strength == TWO_PAIR) {
			/* give up the odd card out */
			var sameValue = [0, 0];
			for (var i = 1; i < cardRanks.length; i++) {
				if (cardRanks[i] == 2) {
					if (sameValue[0] == 0) {
						sameValue[0] = i+1;
					} else {
						sameValue[1] = i+1;
					}
				}
			}
	        
			player.hand.tradeIns = [false, false, false, false, false];
			for (var i = 0; i < hand.length; i++) {
				if (getCardValue(hand[i]) != sameValue[0] && getCardValue(hand[i]) != sameValue[1]) {
					player.hand.tradeIns[i] = true;
				}
			}
			console.log("Hand is good, will trade in one card. "+player.hand.tradeIns);
			return;
		}
		
		/* otherwise, consider your options */
		var one_from_flush = -1;
		var straight_gap = -1;
		var straight_block = -1;
		var one_from_straight = -1;
		
		/* first, look to see if a straight is possible */
		var sequence = 0;
		for (var i = 0; i < cardRanks.length; i++) {
			if (cardRanks[i] == 0 && straight_gap < 0 && sequence != 1) {
				/* 0, potential gap */
				straight_gap = i;
			} else if (cardRanks[i] == 0 && straight_gap < 0 && sequence == 1) {
				/* 0, potential gap and potential block */
				straight_gap = i;
				straight_block = i;
			} else if (cardRanks[i] == 0 && straight_gap >= 0) {
				/* 00, potential gap but restart the sequence */
				straight_gap = i+1;
				sequence = 0;
			} else if (cardRanks[i] == 1) {
				/* 1, adds to the sequence */
				straight_gap = -1;
				sequence++;
			} else if (cardRanks[i] == 2 && straight_block < 0) {
				/* 2, adds to the sequence, the gap, and the block */
				straight_gap = i;
				straight_block = i+1;
				sequence++;
			} else if (cardRanks[i] == 2 && straight_block >= 0) {
				/* 22, no potential*/
				break;
			} else {
				/* >2, no potential */
				break;
			}
			
			/* if there is 4 in sequence */
			if (sequence == hand.length - 1) {
				/* chance at a straight */
				if (straight_block < 0) {
					for (var j = i; j < cardRanks.length; j++) {
						if (cardRanks[i] <= 2) {
							straight_block = j;
						}
					}
				}

				for (var j = 0; j < hand.length; j++) {
					if (getCardValue(hand[j]) == straight_block) {
						one_from_straight = j;
						break;
					}
				}
				break;
			}
		}
		
		/* second, look to see if a flush is possible */
		for (var i = 0; i < cardSuits.length; i++) {
			if (cardSuits[i] == hand.length - 1) {
				/* chance at a flush */
				for (var j = 0; j < hand.length; j++) {
					if (getCardSuitValue(hand[j]) != i) {
						one_from_flush = j;
					}
				}
				break;
			} else if (cardSuits[i] > 1) {
				/* very little chance at a flush */
				break;
			}
		}
		
		/* now, take each remaining hand into special consideration */
		if (player.hand.strength == PAIR) {
			/* if you are one away from a flush, give up the pair for it */
			if (one_from_flush > 0) {
				player.hand.tradeIns = [false, false, false, false, false];
				player.hand.tradeIns[one_from_flush] = true;
				console.log("Hand is okay, going for a flush, trading in one card. "+player.hand.tradeIns);
				return;
			}
			
			/* otherwise, give up every other card */
			var sameValue = 0;
			for (var i = 0; i < cardRanks.length; i++) {
				if (cardRanks[i] == 2) {
					sameValue = i+1;
					break;
				}
			}
			
			player.hand.tradeIns = [false, false, false, false, false];
			for (var i = 0; i < hand.length; i++) {
				if (getCardValue(hand[i]) != sameValue) {
					player.hand.tradeIns[i] = true;
				}
			}
			console.log("Hand is okay, trading in three cards. "+player.hand.tradeIns);
			return;
		}
		
		/* now, take each remaining hand into special consideration */
		if (player.hand.strength == HIGH_CARD) {
			/* if you are one away from a flush, go for it */
			if (one_from_flush > 0) {
				player.hand.tradeIns = [false, false, false, false, false];
				player.hand.tradeIns[one_from_flush] = true;
				console.log("Hand is bad, going for a flush, trading in one card. "+player.hand.tradeIns);
				return;
			}
			
			/* if you are one away from a straight, go for it */
			if (one_from_straight > 0) {
				player.hand.tradeIns = [false, false, false, false, false];
				player.hand.tradeIns[one_from_straight] = true;
				console.log("Hand is bad, going for a straight, trading in one card. "+player.hand.tradeIns);
				return;
			}
			
			/* otherwise, give up everything but the high card */
			var highCard = 0;
			for (var i = cardRanks.length-1; i > 0; i--) {
				if (cardRanks[i] == 1) {
					if (i == 13) {
						highCard = 1;
					} else {
						highCard = i+1;
					}
					break;
				}
			}
			
			player.hand.tradeIns = [false, false, false, false, false];
			for (var i = 0; i < hand.length; i++) {
				if (getCardValue(hand[i]) != highCard) {
					player.hand.tradeIns[i] = true;
				}
			}
			console.log("Hand is bad, trading in four cards. "+player.hand.tradeIns);
			return;
		}
		
		/* end of function, otherwise just trade in everything */
		player.hand.tradeIns = [true, true, true, true, true];
		console.log("Hand is horrid, trading in everything. "+player.hand.tradeIns);
		return;
	}


	
}
