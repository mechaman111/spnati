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
	player.hand.determine();
	
	/* collect the ranks and suits of the cards */
	var hand = player.hand.cards;
	
	/*for low intelligence characters all trades are done at random. Technically this is the same as doing nothing but this way they won't always just do nothing.*/
	if (player.getIntelligence() == eIntelligence.BAD) {
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
			var flush_draw = player.hand.suits.indexOf(function(s) { return s == CARDS_PER_HAND - 1; });
			if (flush_draw >= 0) {
				player.hand.tradeIns = hand.map(function(c) { return c.suit != flush_draw; });
				console.log("Hand is " + (player.hand.strength == PAIR ? "okay" : "bad") + ", going for a flush, trading in one card. " + player.hand.tradeIns);
				return;
			}
		}

		/* We give up a pair for a flush draw, but not for a straight draw. */
		if (player.hand.strength == PAIR) {
			/* Keep the pair (rank = value[0]), discard the rest */
			player.hand.tradeIns = hand.map(function(c) { return c.rank != player.hand.value[0]; });
			console.log("Hand is okay, trading in three cards. "+player.hand.tradeIns);
			return;
		}

		if (player.getIntelligence() == eIntelligence.AVERAGE) {
			/* then, look to see if a straight is possible. Note that no
			 * pairs or better can exist at this point and that when a
			 * straight draw exists in this situation, the difference
			 * between the second highest and the lowest card, or the
			 * highest and the second lowest card, has to be 3 or 4. */
			/* Special-casing the Ace is easier this way than having to
			 * account for a ranks array with 6 non-zero elements. */ 
			var sorted_ranks = hand.map(function(c) { return c.rank; }).sort(function(a, b) { return a - b; });
			var straight_draw_discard = null;
			if (sorted_ranks[4] - sorted_ranks[1] == 3) { // Open-ended straight draw with a lower card (e.g. 3 6 7 8 9)
				straight_draw_discard = sorted_ranks[0];
			} else if (sorted_ranks[3] - sorted_ranks[0] == 3) { // Likewise (unless A-4), with a higher card (e.g. 6 7 8 9 Q)
				straight_draw_discard = sorted_ranks[4];
			} else if (sorted_ranks[4] == 14 && sorted_ranks[2] <= 5) { // Special case: 2 3 4 x A or e.g. 3 4 5 x A
				straight_draw_discard = sorted_ranks[1];
			} else if (sorted_ranks[4] - sorted_ranks[1] == 4) { // Inside straight draw with a lower card (e.g. 3 6 7 9 10)
				straight_draw_discard = sorted_ranks[0];
			} else if (sorted_ranks[3] - sorted_ranks[0] == 4) { // Likewise with a higher card (e.g. 6 7 9 10 Q)
				straight_draw_discard = sorted_ranks[4];
			}
			if (straight_draw_discard !== null) {
				player.hand.tradeIns = hand.map(function(c) { return c.rank == straight_draw_discard; });
				console.log("Hand is bad, going for a straight, trading in one card. "+player.hand.tradeIns);
				return;
			}
		}

		if (player.hand.strength == HIGH_CARD) {
			if (player.getIntelligence() != eIntelligence.BEST || player.hand.value[0] >= 11) {
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
