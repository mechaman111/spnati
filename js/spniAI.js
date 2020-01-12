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
var AVERAGE_KEEP_HIGH = 2;

function determineAIAction (player) {
	/* determine the current hand */
	player.hand.determine();
	
	/* collect the ranks and suits of the cards */
	var hand = player.hand.cards;

	/* Player tries hard to lose */
	if (player.getIntelligence() == eIntelligence.THROW) {
		if (player.hand.strength == STRAIGHT || player.hand.strength == FLUSH || player.hand.strength >= STRAIGHT_FLUSH) {
			var sortedRanks = hand.map(function(c) { return c.rank; }).sort();
			// Keep the two lowest cards.
			player.hand.tradeIns = hand.map(function(c) { return c.rank != sortedRanks[0] && c.rank != sortedRanks[1]; })
			return;
		}
		player.hand.tradeIns = [false, false, false, false, false];
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

	/*for low intelligence characters all trades are done at random. Technically this is the same as doing nothing but this way they won't always just do nothing.*/
	} else if (player.getIntelligence() == eIntelligence.BAD) {
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
	/*for average intelligence characters use the standard algorithm. Average intelligence is also the default case*/
	} else {
		/* if the current hand is good enough, then take a pre-determined action */
		if (player.hand.strength >= STRAIGHT) {
			/* give up nothing */
			player.hand.tradeIns = [false, false, false, false, false];
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
			player.hand.tradeIns = hand.map(function(c) { return c.rank == player.hand.value[2]; });
			console.log("Hand is good, will trade in one card. "+player.hand.tradeIns);
			return;
		}

		if (player.getIntelligence() == eIntelligence.AVERAGE) {
			/* Check for flush draw, even if holding a pair */
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

		if (player.getIntelligence() == eIntelligence.AVERAGE) {
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
			if (player.getIntelligence() == eIntelligence.AVERAGE) {
				player.hand.tradeIns = hand.map(function(c) { return player.hand.value.slice(0, AVERAGE_KEEP_HIGH).indexOf(c.rank) < 0; });
				console.log("Hand is bad, trading in "+ (CARDS_PER_HAND - AVERAGE_KEEP_HIGH) +" cards. "+player.hand.tradeIns);
				return;
			} else if (player.getIntelligence() != eIntelligence.BEST || player.hand.value[0] >= 10) {
				player.hand.tradeIns = hand.map(function(c) { return c.rank != player.hand.value[0]; });
				console.log("Hand is bad, trading in four cards. "+player.hand.tradeIns);
				return;
			}
		}
		
		/* end of function, otherwise just trade in everything */
		player.hand.tradeIns = [true, true, true, true, true];
		console.log("Hand is horrid, trading in everything. "+player.hand.tradeIns);
		return;
	}

}
