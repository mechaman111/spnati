/********************************************************************************
 This file contains the variables and functions that from the behaviours of the
 AI opponents. All the parsing of their files, as well as the structures to store
 that information are stored in this file.
 ********************************************************************************/

/**********************************************************************
 *****                  State Object Specification                *****
 **********************************************************************/
 
/************************************************************
 * Stores information on AI state.
 ************************************************************/
function createNewState (dialogue, image, direction, silent, marker) {
	var newStateObject = {dialogue:dialogue,
                          image:image,
                          direction:direction,
                          silent:silent,
                          marker:marker};
						  
	return newStateObject;
}

/**********************************************************************
 *****                      All Dialogue Tags                     *****
 **********************************************************************/
 
var NAME = "~name~";
var CAPITALIZED_NAME = "~Name~";
var PROPER_CLOTHING = "~Clothing~";
var LOWERCASE_CLOTHING = "~clothing~";
var CARDS = "~cards~";
var PLAYER_NAME = "~player~";

/**********************************************************************
 *****                    All Dialogue Triggers                   *****
 **********************************************************************/

var SELECTED = "selected";
var GAME_START = "start";

var SWAP_CARDS = "swap_cards";
var BAD_HAND = "bad_hand";
var OKAY_HAND = "okay_hand";
var GOOD_HAND = "good_hand";
 
var PLAYER_MUST_STRIP_WINNING = "must_strip_winning";
var PLAYER_MUST_STRIP_NORMAL = "must_strip_normal";
var PLAYER_MUST_STRIP_LOSING = "must_strip_losing";
var PLAYER_STRIPPING = "stripping";
var PLAYER_STRIPPED = "stripped";

var PLAYER_MUST_MASTURBATE = "must_masturbate";
var PLAYER_MUST_MASTURBATE_FIRST = "must_masturbate_first";
var PLAYER_START_MASTURBATING = "start_masturbating";
var PLAYER_MASTURBATING = "masturbating";
var PLAYER_HEAVY_MASTURBATING = "heavy_masturbating";
var PLAYER_FINISHING_MASTURBATING = "finishing_masturbating";
var PLAYER_FINISHED_MASTURBATING = "finished_masturbating";

var MALE_HUMAN_MUST_STRIP = "male_human_must_strip";
var MALE_MUST_STRIP = "male_must_strip";

var MALE_REMOVING_ACCESSORY = "male_removing_accessory";
var MALE_REMOVING_MINOR = "male_removing_minor";
var MALE_REMOVING_MAJOR = "male_removing_major";
var MALE_CHEST_WILL_BE_VISIBLE = "male_chest_will_be_visible";
var MALE_CROTCH_WILL_BE_VISIBLE = "male_crotch_will_be_visible";

var MALE_REMOVED_ACCESSORY = "male_removed_accessory";
var MALE_REMOVED_MINOR = "male_removed_minor";
var MALE_REMOVED_MAJOR = "male_removed_major";
var MALE_CHEST_IS_VISIBLE = "male_chest_is_visible";
var MALE_SMALL_CROTCH_IS_VISIBLE = "male_small_crotch_is_visible";
var MALE_MEDIUM_CROTCH_IS_VISIBLE = "male_medium_crotch_is_visible";
var MALE_LARGE_CROTCH_IS_VISIBLE = "male_large_crotch_is_visible";

var MALE_MUST_MASTURBATE = "male_must_masturbate";
var MALE_START_MASTURBATING = "male_start_masturbating";
var MALE_MASTURBATING = "male_masturbating";
var MALE_HEAVY_MASTURBATING = "male_heavy_masturbating";
var MALE_FINISHED_MASTURBATING = "male_finished_masturbating";

var FEMALE_HUMAN_MUST_STRIP = "female_human_must_strip";
var FEMALE_MUST_STRIP = "female_must_strip";

var FEMALE_REMOVING_ACCESSORY = "female_removing_accessory";
var FEMALE_REMOVING_MINOR = "female_removing_minor";
var FEMALE_REMOVING_MAJOR = "female_removing_major";
var FEMALE_CHEST_WILL_BE_VISIBLE = "female_chest_will_be_visible";
var FEMALE_CROTCH_WILL_BE_VISIBLE = "female_crotch_will_be_visible";

var FEMALE_REMOVED_ACCESSORY = "female_removed_accessory";
var FEMALE_REMOVED_MINOR = "female_removed_minor";
var FEMALE_REMOVED_MAJOR = "female_removed_major";
var FEMALE_SMALL_CHEST_IS_VISIBLE = "female_small_chest_is_visible";
var FEMALE_MEDIUM_CHEST_IS_VISIBLE = "female_medium_chest_is_visible";
var FEMALE_LARGE_CHEST_IS_VISIBLE = "female_large_chest_is_visible";
var FEMALE_CROTCH_IS_VISIBLE = "female_crotch_is_visible";

var FEMALE_MUST_MASTURBATE = "female_must_masturbate";
var FEMALE_START_MASTURBATING = "female_start_masturbating";
var FEMALE_MASTURBATING = "female_masturbating";
var FEMALE_HEAVY_MASTURBATING = "female_heavy_masturbating";
var FEMALE_FINISHED_MASTURBATING = "female_finished_masturbating";

var GAME_OVER_VICTORY = "game_over_victory";
var GAME_OVER_DEFEAT = "game_over_defeat";
 
/**********************************************************************
 *****                 Behaviour Parsing Functions                *****
 **********************************************************************/

/************************************************************
 * Loads and parses the start of the behaviour XML file of the 
 * given opponent id.
 *
 * The callFunction parameter must be a function capable of 
 * receiving a new player object and a slot number.
 ************************************************************/
function loadBehaviour (id, callFunction, slot) {
	$.ajax({
        type: "GET",
		url: 'opponents/' + id + "/behaviour.xml",
		dataType: "text",
		success: function(xml) {
            var first = $(xml).find('first').text();
            var last = $(xml).find('last').text();
            var labels = $(xml).find('label');
            var gender = $(xml).find('gender').text().trim().toLowerCase(); //convert everything to lowercase, for comparison to the strings "male" and "female"
            var size = $(xml).find('size').text();
            var timer = $(xml).find('timer').text();
            var intelligence = $(xml).find('intelligence');
            
            var tags = $(xml).find('tags');
            var tagsArray = [];
            if (typeof tags !== typeof undefined && tags !== false) {
                $(tags).find('tag').each(function () {
                    tagsArray.push($(this).text());
                });
            }
            
            var newPlayer = createNewPlayer(id, first, last, labels, gender, size, intelligence, Number(timer), tagsArray, xml);
            
			callFunction(newPlayer, slot);
		}
	});
}

/************************************************************
 * Parses and loads the wardrobe section of an opponent's XML 
 * file.
 ************************************************************/
function loadOpponentWardrobe (player) {
	/* grab the relevant XML file, assuming its already been loaded */
	var xml = player.xml;
	player.clothing = [];
	
	/* find and grab the wardrobe tag */
	$wardrobe = $(xml).find('wardrobe');
	
	/* find and create all of their clothing */
	$wardrobe.find('clothing').each(function () {
		var properName = $(this).attr('proper-name');
		var lowercase = $(this).attr('lowercase');
		var type = $(this).attr('type');
		var position = $(this).attr('position');
		
		var newClothing = createNewClothing(properName, lowercase, type, position, null, 0, 0);
		
		player.clothing.push(newClothing);
	});
}

/************************************************************
 * Parses the dialogue states of a player, given the case object.
 ************************************************************/
function parseDialogue (caseObject, self, target) {
    var states = [];

	var substitutions = {};
	substitutions[PLAYER_NAME] = players[HUMAN_PLAYER].label;
	if (target) {
		substitutions[NAME] = target.label;
		substitutions[CAPITALIZED_NAME] = target.label.initCap();
		if (target.removedClothing) {
			substitutions[PROPER_CLOTHING] = target.removedClothing.proper;
			substitutions[LOWERCASE_CLOTHING] = target.removedClothing.lower;
		}
	} else if (self.removedClothing) {
		substitutions[PROPER_CLOTHING] = self.removedClothing.proper;
		substitutions[LOWERCASE_CLOTHING] = self.removedClothing.lower;
	}
	if (caseObject.attr('tag') == SWAP_CARDS) {
		/* determine how many cards are being swapped */
		substitutions[CARDS] = self.hand.tradeIns.reduce(function(acc, x) { return acc + (x ? 1 : 0); }, 0);
	}
	
	caseObject.find('state').each(function () {
        var image = $(this).attr('img');
        var dialogue = $(this).html();
        var direction = $(this).attr('direction');
        var silent = $(this).attr('silent');
        var marker = $(this).attr('marker');
        
		for (var placeholder in substitutions) {
			dialogue = dialogue.split(placeholder).join(substitutions[placeholder]);
		}
        
        if (silent !== null && typeof silent !== typeof undefined) {
            silent = true;
        }
        else {
            silent = false;
        }
        
        states.push(createNewState(dialogue, image, direction, silent, marker));
	});
	
	return states;
}

/************************************************************
 * Given a string containing a number or two numbers 
 * separated by a dash, returns an array with the same number 
 * twice, or the first and second number as the case may be
 ************************************************************/
function parseInterval (str) {
	if (!str) return undefined;
	var pieces = str.split("-");
	var min = parseInt(pieces[0], 10);
	var max = pieces.length > 1 ? parseInt(pieces[1], 10) : min;
	return { min : min,
			 max : max };
}

function inInterval (value, interval) {
	return interval.min <= value && value <= interval.max;
}

/************************************************************
 * Updates the behaviour of the given player based on the 
 * provided tag.
 ************************************************************/
function updateBehaviour (player, tag, opp) {
	/* determine if the AI is dialogue locked */
	//Allow characters to speak. If we change forfeit ideas, we'll likely need to change this as well.
	//if (players[player].forfeit[1] == CANNOT_SPEAK) {
		/* their is restricted to this only */
		//tag = players[player].forfeit[0];
	//}
    
    if (!players[player]) {
        return;
    }
	
    /* get the AI stage */
    var stageNum = players[player].stage;
	
    /* try to find the stage */
    var stage = null;
    $(players[player].xml).find('behaviour').find('stage').each(function () {
       if (Number($(this).attr('id')) == stageNum) {
           stage = $(this);
       } 
    });
    
    /* quick check to see if the stage exists */
    if (!stage) {
        console.log("Error: couldn't find stage for player "+player+" on stage number "+stageNum+" for tag "+tag);
        return;
    }
    
    /* try to find the tag */
	var states = [];
	$(stage).find('case').each(function () {
		if ($(this).attr('tag') == tag) {
            states.push($(this));
		}
	});

    /* quick check to see if the tag exists */
	if (states.length <= 0) {
		console.log("Warning: couldn't find "+tag+" dialogue for player "+player+" at stage "+stageNum);
		return false;
	}
    else {
        // look for the best match
        var bestMatch = [];
		var bestMatchPriority = -1;
		
        for (var i = 0; i < states.length; i++) {

            var target =           states[i].attr("target");
            var filter =           states[i].attr("filter");
			var targetStage =      parseInterval(states[i].attr("targetStage"));
			var targetTimeInStage = parseInterval(states[i].attr("targetTimeInStage"));
			var targetSaidMarker =        states[i].attr("targetSaidMarker");
			var targetNotSaidMarker =     states[i].attr("targetNotSaidMarker");
			var oppHand =          states[i].attr("oppHand");
			var hasHand =          states[i].attr("hasHand");
			var alsoPlaying =      states[i].attr("alsoPlaying");
			var alsoPlayingStage = parseInterval(states[i].attr("alsoPlayingStage"));
			var alsoPlayingHand =  states[i].attr("alsoPlayingHand");
			var alsoPlayingTimeInStage = parseInterval(states[i].attr("alsoPlayingTimeInStage"));
			var alsoPlayingSaidMarker =   states[i].attr("alsoPlayingSaidMarker");
			var alsoPlayingNotSaidMarker = states[i].attr("alsoPlayingNotSaidMarker");
			var totalMales =	   parseInterval(states[i].attr("totalMales"));
			var totalFemales =	   parseInterval(states[i].attr("totalFemales"));
			var timeInStage =      parseInterval(states[i].attr("timeInStage"));
			var lossesInRow =      parseInterval(states[i].attr("consecutiveLosses"));
			var totalAlive =         parseInterval(states[i].attr("totalAlive"));
			var totalExposed =       parseInterval(states[i].attr("totalExposed"));
			var totalNaked =         parseInterval(states[i].attr("totalNaked"));
			var totalMasturbating =     parseInterval(states[i].attr("totalMasturbating"));
			var totalFinished =      parseInterval(states[i].attr("totalFinished"));
			var totalRounds = 	parseInterval(states[i].attr("totalRounds"));
			var saidMarker =        states[i].attr("saidMarker");
			var notSaidMarker =     states[i].attr("notSaidMarker");
			var customPriority =    states[i].attr("priority");
			var counters = [];
			states[i].find("condition").each(function () {
				var counter = $(this);
				if (counter.attr('filter')) {
					counters.push(counter);
				}
			});

			var totalPriority = 0; // this is used to determine which of the states that
									// doesn't fail any conditions should be used
			
			
			///////////////////////////////////////////////////////////////////////
			// go through different conditions required until one of them fails
			// if none of them fail, then this state is considered for use with a certain priority
			
			// target (priority = 300)
			if (opp && typeof target !== typeof undefined && target !== false) {
            target = target;
				if (target === opp.id) {
					totalPriority += 300; 	// priority
				}
				else {
					continue;				// failed "target" requirement
				}
			}
			
			// filter (priority = 150)
			if (opp && typeof filter !== typeof undefined && filter !== false) {
				// check against tags
				var found = false;
                for (var j = 0; j < opp.tags.length && found === false; j++) {
                    if (filter === opp.tags[j]) {
						totalPriority += 150;	// priority
						found = true;
                    }
                }
				if (found === false) {
					continue;				// failed "filter" requirement
				}
			}
			
			// targetStage (priority = 80)
			if (opp && typeof targetStage !== typeof undefined && targetStage !== false) {
				if (inInterval(opp.stage, targetStage)) {
					totalPriority += 80;		// priority
				}
				else {
					continue;				// failed "targetStage" requirement
				}
			}

			// markers (priority = 1)
			// marker checks have very low priority as they're mainly intended to be used with other target types
			if (opp && targetSaidMarker) {
				if (targetSaidMarker in opp.markers) {
					totalPriority += 1;
				}
				else {
					continue;
				}
			}
			if (opp && targetNotSaidMarker) {
				if (!(targetNotSaidMarker in opp.markers)) {
					totalPriority += 1;
				}
				else {
					continue;
				}
			}

			// consecutiveLosses (priority = 60)
			if (typeof lossesInRow !== typeof undefined && lossesInRow !== false) {
				if (opp) { // if there's a target, look at their losses
					if (inInterval(opp.consecutiveLosses, lossesInRow)) {
						totalPriority += 60;
					}
					else {
						continue;				// failed "consecutiveLosses" requirement
					}
				}
				else { // else look at your own losses
					if (inInterval(players[player].consecutiveLosses, lossesInRow)) {
						totalPriority += 60;
					}
					else {
						continue;
					}
				}
			}
			
			// oppHand (priority = 30)
			if (opp && typeof oppHand !== typeof undefined && oppHand !== false) {
				var failedOppHandReq = false;
				for (var q = 0; q < players.length; q++)
				{
					if (opp === players[q]) {
						if (handStrengthToString(players[q].hand.strength) === oppHand) {
							totalPriority += 30;	// priority
						}
						else {
							failedOppHandReq = true;
							break;
						}
					}
				}
				if (failedOppHandReq) {
					continue;
				}
			}

			// targetTimeInStage (priority = 25)
			if (opp && typeof targetTimeInStage !== typeof undefined) {
				if (inInterval(opp.timeInStage == -1 ? 0 //allow post-strip time to count as 0
							   : opp.timeInStage, targetTimeInStage)) {
					totalPriority += 25;
				}
				else {
					continue;				// failed "targetTimeInStage" requirement
				}
			}
			
			// hasHand (priority = 20)
			if (typeof hasHand !== typeof undefined && hasHand !== false) {
				if (handStrengthToString(players[player].hand.strength) === hasHand) {
					totalPriority += 20;		// priority
				}
				else {
					continue;				// failed "hasHand" requirement
				}
			}
			
            // alsoPlaying, alsoPlayingStage, alsoPlayingTimeInStage, alsoPlayingHand (priority = 100, 40, 15, 5)
			if (typeof alsoPlaying !== typeof undefined && alsoPlaying !== false) {
			
				var foundEm = false;
				var j = 0;
				for (j = 0; j < players.length && foundEm === false; j++) {
					if (players[j] && opp !== players[j]) {
						if (alsoPlaying === players[j].id) {
							totalPriority += 100; 	// priority
							foundEm = true;
                            break;
						}
					}
				}
				
				if (foundEm === false)
				{
					continue;				// failed "alsoPlaying" requirement
				}
				else
				{
					if (typeof alsoPlayingStage !== typeof undefined && alsoPlayingStage !== false) {
						if (inInterval(players[j].stage, alsoPlayingStage)) {
							totalPriority += 40;	// priority
						}
						else {
							continue;		// failed "alsoPlayingStage" requirement
						}
					}
					if (typeof alsoPlayingTimeInStage !== typeof undefined) {
						if (inInterval(players[j].timeInStage, alsoPlayingTimeInStage)) {
							totalPriority += 15;
						}
						else {
							continue;		// failed "alsoPlayingTimeInStage" requirement
						}
					}
					if (typeof alsoPlayingHand !== typeof undefined && alsoPlayingHand !== false) {
						if (handStrengthToString(players[j].hand.strength) === alsoPlayingHand)
						{
							totalPriority += 5;		// priority
						}
						else {
							continue;		// failed "alsoPlayingHand" requirement
						}
					}
					// marker checks have very low priority as they're mainly intended to be used with other target types
					if (alsoPlayingSaidMarker) {
						if (alsoPlayingSaidMarker in players[j].markers) {
							totalPriority += 1;
						}
						else {
							continue;
						}
					}
					if (alsoPlayingNotSaidMarker) {
						if (!(alsoPlayingNotSaidMarker in players[j].markers)) {
							totalPriority += 1;
						}
						else {
							continue;
						}
					}
				}
			}

			// filter counter targets (priority = 10)
			var matchCounter = true;
			for (var j = 0; j < counters.length; j++) {
				var desiredCount = parseInterval(counters[j].attr('count'));
				var filterTag = counters[j].attr('filter');
				var count = 0;
				for (var q = 0; q < players.length; q++) {
					if (players[q] && players[q].tags) {
						for (var t = 0; t < players[q].tags.length; t++) {
							if (filterTag === players[q].tags[t]) {
								count++;
								break;
							}
						}
					}
				}
				if (inInterval(count, desiredCount)) {
					totalPriority += 10;
				}
				else {
					matchCounter = false;
					break;
				}
			}
			if (!matchCounter) {
				continue; // failed filter count
			}

			// totalRounds (priority = 10)
			if (typeof totalRounds !== typeof undefined) {
				if (inInterval(currentRound, totalRounds)) {
					totalPriority += 10;
				}
				else {
					continue;		// failed "totalRounds" requirement
				}
			}

			// timeInStage (priority = 8)
			if (typeof timeInStage !== typeof undefined) {
				if (inInterval(players[player].timeInStage == -1 ? 0 //allow post-strip time to count as 0
							   : players[player].timeInStage, timeInStage)) {
					totalPriority += 8;
				}
				else {
					continue;		// failed "timeInStage" requirement
				}
			}
			
			// totalMales (priority = 5)
			if (typeof totalMales !== typeof undefined && totalMales !== false) {
				var count = 0;
				for (var q = 0; q < players.length; q++)
				{
					if (players[q] && players[q].gender === eGender.MALE)
					{
						count++;
					}
				}
				if (inInterval(count, totalMales)) {
					totalPriority += 5;		// priority
				}
				else {
					continue;		// failed "totalMales" requirement
				}
			}
			
			// totalFemales (priority = 5)
			if (typeof totalFemales !== typeof undefined && totalFemales !== false) {
				var count = 0;
				for (var q = 0; q < players.length; q++)
				{
					if (players[q] && players[q].gender === eGender.FEMALE)
					{
						count++;
					}
				}
				if (inInterval(count, totalFemales)) {
					totalPriority += 5;		// priority
				}
				else {
					continue;		// failed "totalFemales" requirement
				}
			}

			// totalAlive (priority = 3)
			if (typeof totalAlive !== typeof undefined) {
				if (inInterval(getNumPlayersInStage(STAGE_ALIVE), totalAlive)) {
					totalPriority += 2 + totalAlive.max; //priority is weighted by max, so that higher totals take priority
				}
				else {
					continue;		// failed "totalAlive" requirement
				}
			}

			// totalExposed (priority = 4)
			if (typeof totalExposed !== typeof undefined) {
				var count = 0;
				if (inInterval(getNumPlayersInStage(STAGE_EXPOSED), totalExposed)) {
					totalPriority += 4 + totalExposed.max; //priority is weighted by max, so that higher totals take priority
				}
				else {
					continue;		// failed "totalExposed" requirement
				}
			}

			// totalNaked (priority = 5)
			if (typeof totalNaked !== typeof undefined) {
				if (inInterval(getNumPlayersInStage(STAGE_NAKED), totalNaked)) {
					totalPriority += 5 + totalNaked.max; //priority is weighted by max, so that higher totals take priority;
				}
				else {
					continue;		// failed "totalNaked" requirement
				}
			}

			// totalMasturbating (priority = 5)
			if (typeof totalMasturbating !== typeof undefined) {
				if (inInterval(getNumPlayersInStage(STAGE_MASTURBATING), totalMasturbating)) {
					totalPriority += 5 + totalMasturbating.max; //priority is weighted by max, so that higher totals take priority;
				}
				else {
					continue;		// failed "totalMasturbating" requirement
				}
			}

			// totalFinished (priority = 5)
			if (typeof totalFinished !== typeof undefined) {
				if (inInterval(getNumPlayersInStage(STAGE_FINISHED), totalFinished)) {
					totalPriority += 5 + totalFinished.max; //priority is weighted by max, so that higher totals take priority
				}
				else {
					continue;		// failed "totalFinished" requirement
				}
			}

			// markers (priority = 1)
			// marker checks have very low priority as they're mainly intended to be used with other target types
			if (saidMarker) {
				if (saidMarker in players[player].markers) {
					totalPriority += 1;
				}
				else {
					continue;
				}
			}
			if (notSaidMarker) {
				if (!(notSaidMarker in players[player].markers)) {
					totalPriority += 1;
				}
				else {
					continue;
				}
			}

			if (typeof customPriority !== typeof undefined) {
				totalPriority = parseInt(customPriority, 10); //priority override
			}
			
			// Finished going through - if a state has still survived up to this point,
			// it's then determined if it's the highest priority so far
			
			if (totalPriority > bestMatchPriority)
			{
				console.log("New best match with " + totalPriority + " priority.");
				bestMatch = [states[i]];
				bestMatchPriority = totalPriority;
			}
			else if (totalPriority === bestMatchPriority)
			{
				bestMatch.push(states[i]);
			}
			
		}
        
        if (bestMatch.length > 0) {
			bestMatch = bestMatch[Math.floor(Math.random() * bestMatch.length)]
            players[player].current = 0;
            players[player].state = parseDialogue(bestMatch, players[player], opp);
            return true;
        }
        console.log("-------------------------------------");
    }
    return false;
}

/************************************************************
 * Updates the behaviour of all players except the given player 
 * based on the provided tag.
 ************************************************************/
function updateAllBehaviours (player, tag, opp) {
	for (i = 1; i < players.length; i++) {
		if (players[i] && (player === null || i != player)) {
			updateBehaviour(i, tag, opp);
		}
	}
}
