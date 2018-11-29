/********************************************************************************
 This file contains the variables and functions that from the behaviours of the
 AI opponents. All the parsing of their files, as well as the structures to store
 that information are stored in this file.
 ********************************************************************************/

/**********************************************************************
 *****                    All Dialogue Triggers                   *****
 **********************************************************************/

var SELECTED = "selected";
var GAME_START = "game_start";

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
 *****                  State Object Specification                *****
 **********************************************************************/

function State($xml) {
	this.image = $xml.attr('img');
	this.direction = $xml.attr('direction') || 'down';
	this.location = $xml.attr('location') || '';
	this.marker = $xml.attr('marker');
	this.rawDialogue = $xml.html();
	
	if (this.location && Number(this.location) == this.location) {
		// It seems that location was specified as a number without "%"
		this.location = this.location + "%";
	}
}

State.prototype.expandDialogue = function(self, target) {
	this.dialogue = expandDialogue(this.rawDialogue, self, target);
}

/************************************************************
 * Expands variables etc. in a line of dialogue.
 ************************************************************/
function expandDialogue (dialogue, self, target) {
    function substitute(match, variable, fn, args) {
        // If substitution fails, return match unchanged.
        var substitution = match;
        if (fn) fn = fn.toLowerCase();
        try {
            switch (variable.toLowerCase()) {
            case 'player':
                substitution = players[HUMAN_PLAYER].label;
                break;
            case 'name':
                substitution = target.label;
                break;
            case 'clothing':
                var clothing = (target||self).removedClothing;
                if (fn == 'ifplural' && args) {
                    substitution = expandDialogue(args.split('|')[clothing.plural ? 0 : 1], self, target);
                } else if (fn == 'formal' && args === undefined) {
                    substitution = clothing.formal || clothing.generic;
                } else if (fn === undefined) {
                    substitution = clothing.generic;
                }
                break;
            case 'cards': /* determine how many cards are being swapped */
                var n = self.hand.tradeIns.countTrue();
                if (fn == 'ifplural') {
                    substitution = expandDialogue(args.split('|')[n == 1 ? 1 : 0], self, target);
                } else if (fn === undefined) {
                    substitution = String(n);
                }
                break;
            }
            if (variable[0] == variable[0].toUpperCase()) {
                substitution = substitution.initCap();
            }
        } catch (ex) {
            console.log("Invalid substitution caused exception " + ex);
        }
        return substitution;
    }
    // variable or
    // variable.attribute or
    // variable.function(arguments)
    return dialogue.replace(/~(\w+)(?:\.(\w+)(?:\(([^)]*)\))?)?~/g, substitute);
}


/************************************************************
 * Given a string containing a number or two numbers 
 * separated by a dash, returns an array with the same number 
 * twice, or the first and second number as the case may be
 ************************************************************/
function parseInterval (str) {
	if (!str) return undefined;
	var pieces = str.split("-");
	var min = pieces[0].trim() == "" ? null : parseInt(pieces[0], 10);
	var max = pieces.length == 1 ? min
		: pieces[1].trim() == "" ? null : parseInt(pieces[1], 10);
	return { min : min,
			 max : max };
}

function inInterval (value, interval) {
	return (interval.min === null || interval.min <= value)
		&& (interval.max === null || value <= interval.max);
}


/************************************************************
 * Check to see if a given marker predicate string is fulfilled
 * w.r.t. a given character.
 ************************************************************/
function checkMarker(predicate, target) {
	var match = predicate.match(/([\w\-]+)\s*((?:\>|\<|\=|\!)\=?)\s*(\-?\d+)/);
	
	if (!match) {
		if (target.markers[predicate]) return true;
		return false;
	}
	
	var val = target.markers[match[1]];
	if (!val) {
		val = 0;
	}
	
	var cmpVal = parseInt(match[3], 10);
	
	switch (match[2]) {
		case '>': return val > cmpVal;
		case '>=': return val >= cmpVal;
		case '<': return val < cmpVal;
		case '<=': return val <= cmpVal;
		case '!=': return val != cmpVal;
		default:
		case '=':
		case '==':
			return val == cmpVal;
	}
}

/**********************************************************************
 *****                  Case Object Specification                 *****
 **********************************************************************/

function Case($xml, stage) {
	if (typeof stage === "number") {
        this.stage = {min: stage, max: stage};
    } else if (stage) {
        this.stage = parseInterval(stage);
    } else {
        this.stage = parseInterval($xml.attr('stage'));
    }
    
	this.tag =                      $xml.attr('tag');
	this.target =                   $xml.attr("target");
	this.filter =                   $xml.attr("filter");
	this.targetStage =              parseInterval($xml.attr("targetStage"));
	this.targetLayers =             parseInterval($xml.attr("targetLayers"));
	this.targetStartingLayers =     parseInterval($xml.attr("targetStartingLayers"));
	this.targetStatus =             $xml.attr("targetStatus");
	this.targetTimeInStage =        parseInterval($xml.attr("targetTimeInStage"));
	this.targetSaidMarker =         $xml.attr("targetSaidMarker");
	this.targetNotSaidMarker =      $xml.attr("targetNotSaidMarker");
    this.targetSayingMarker =       $xml.attr("targetSayingMarker");
	this.oppHand =                  $xml.attr("oppHand");
	this.hasHand =                  $xml.attr("hasHand");
	this.alsoPlaying =              $xml.attr("alsoPlaying");
	this.alsoPlayingStage =         parseInterval($xml.attr("alsoPlayingStage"));
	this.alsoPlayingHand =          $xml.attr("alsoPlayingHand");
	this.alsoPlayingTimeInStage =   parseInterval($xml.attr("alsoPlayingTimeInStage"));
	this.alsoPlayingSaidMarker =    $xml.attr("alsoPlayingSaidMarker");
	this.alsoPlayingNotSaidMarker = $xml.attr("alsoPlayingNotSaidMarker");
    this.alsoPlayingSayingMarker =  $xml.attr("alsoPlayingSayingMarker");
	this.totalMales =               parseInterval($xml.attr("totalMales"));
	this.totalFemales =             parseInterval($xml.attr("totalFemales"));
	this.timeInStage =              parseInterval($xml.attr("timeInStage"));
	this.consecutiveLosses =        parseInterval($xml.attr("consecutiveLosses"));
	this.totalAlive =               parseInterval($xml.attr("totalAlive"));
	this.totalExposed =             parseInterval($xml.attr("totalExposed"));
	this.totalNaked =               parseInterval($xml.attr("totalNaked"));
	this.totalMasturbating =        parseInterval($xml.attr("totalMasturbating"));
	this.totalFinished =            parseInterval($xml.attr("totalFinished"));
	this.totalRounds =              parseInterval($xml.attr("totalRounds"));
	this.saidMarker =               $xml.attr("saidMarker");
	this.notSaidMarker =            $xml.attr("notSaidMarker");
	this.customPriority =           $xml.attr("priority");
	
	var states = [];
	$xml.find('state').each(function () {
		states.push(new State($(this)));
	});
	this.states = states;
	
	var counters = [];
	$xml.find("condition").each(function () {
		counters.push($(this));
	});
	this.counters = counters;
	
	var tests = [];
	$xml.find("test").each(function () {
		tests.push($(this));
	});
	this.tests = tests;
	
	// Calculate case priority ahead of time.
    if (this.customPriority) {
        this.priority = this.customPriority;
    } else {
    	this.priority = 0;
    	if (this.target)                   this.priority += 300;
    	if (this.filter)                   this.priority += 150;
    	if (this.targetStage)              this.priority += 80;
    	if (this.targetLayers)             this.priority += 40;
    	if (this.targetStartingLayers)     this.priority += 40;
    	if (this.targetStatus)             this.priority += 70;
    	if (this.targetSaidMarker)         this.priority += 1;
    	if (this.targetSayingMarker)       this.priority += 1;
    	if (this.targetNotSaidMarker)      this.priority += 1;
    	if (this.consecutiveLosses)        this.priority += 60;
    	if (this.oppHand)                  this.priority += 30;
    	if (this.targetTimeInStage)        this.priority += 25;
    	if (this.hasHand)                  this.priority += 20;
    	if (this.alsoPlaying)              this.priority += 100;
    	if (this.alsoPlayingStage)         this.priority += 40;
    	if (this.alsoPlayingTimeInStage)   this.priority += 15;
    	if (this.alsoPlayingHand)          this.priority += 5;
    	if (this.alsoPlayingSaidMarker)    this.priority += 1;
    	if (this.alsoPlayingNotSaidMarker) this.priority += 1;
    	if (this.alsoPlayingSayingMarker)  this.priority += 1;
    	if (this.totalRounds)              this.priority += 10;
    	if (this.timeInStage)              this.priority += 8;
    	if (this.totalMales)               this.priority += 5;
    	if (this.totalFemales)             this.priority += 5;
    	if (this.saidMarker)               this.priority += 1;
    	if (this.notSaidMarker)            this.priority += 1;
    	if (this.totalAlive)               this.priority += 2 + this.totalAlive.max;
    	if (this.totalExposed)             this.priority += 4 + this.totalExposed.max;
    	if (this.totalNaked)               this.priority += 5 + this.totalNaked.max;
    	if (this.totalMasturbating)        this.priority += 5 + this.totalMasturbating.max;
    	if (this.totalFinished)            this.priority += 5 + this.totalFinished.max;
    
    	counters.forEach(function (ctr) {
    		var filterTag 	 = ctr.attr('filter');
    		var filterGender = ctr.attr('gender');
    		var filterStatus = ctr.attr('status');
    		
    		this.priority += (filterTag ? 10 : 0) + (filterGender ? 5 : 0) + (filterStatus ? 5 : 0);
    	}.bind(this));
    	
    	// Expression tests (priority = 50 for each)
    	this.priority += (tests.length * 50);
    }
}

Case.prototype.getAlsoPlaying = function (opp) {
    if (!this.alsoPlaying) return null;
    
    var ap = null;
    
    players.forEach(function (p) {
        if (!ap && p !== opp && p.id === this.alsoPlaying) {
            ap = p;
        }
    }.bind(this));
    
    return ap;
}

/* Is this case dependent on marker values in the same phase? */
Case.prototype.isVolatile = function () {
    if (this.target && this.targetSayingMarker) {
        return true;
    }
    
    if (this.alsoPlaying && this.alsoPlayingSayingMarker) {
        return true;
    }
    
    return false;
}

Case.prototype.basicRequirementsMet = function (self, opp) {
    // stage
    if (this.stage) {
        if (!inInterval(self.stage, this.stage)) {
            return false; // failed "stage" requirement
        }
    }
    
    // target
    if (opp && this.target) {
        if (this.target !== opp.id) {
            return false; // failed "target" requirement
        }
    }
    
    // filter
    if (opp && this.filter) {
        if (opp.tags.indexOf(this.filter) < 0) {
            return false; // failed "filter" requirement
        }
    }

    // targetStage
    if (opp && this.targetStage) {
        if(!inInterval(opp.stage, this.targetStage)) {
            return false; // failed "targetStage" requirement
        }
    }
    
    // targetLayers
    if (opp && this.targetLayers) {
        if (!inInterval(opp.clothing.length, this.targetLayers)) {
            return false; 
        }
    }

    // targetStartingLayers
    if (opp && this.targetStartingLayers) {
        if (!inInterval(opp.startingLayers, this.targetStartingLayers)) {
            return false;
        }
    }

    // targetStatus
    if (opp && this.targetStatus) {
        if (!checkPlayerStatus(opp, this.targetStatus)) {
            return false;
        }
    }

    // targetSaidMarker
    if (opp && this.targetSaidMarker) {
        if (!checkMarker(this.targetSaidMarker, opp)) {
            return false;
        }
    }
    
    // targetNotSaidMarker
    if (opp && this.targetNotSaidMarker) {
        if (opp.markers[this.targetNotSaidMarker]) {
            return false;
        }
    }
    
    // consecutiveLosses
    if (this.consecutiveLosses) {
        if (opp) { // if there's a target, look at their losses
            if (!inInterval(opp.consecutiveLosses, this.consecutiveLosses)) {
                return false; // failed "consecutiveLosses" requirement
            }
        }
        else { // else look at your own losses
            if (!inInterval(self.consecutiveLosses, this.consecutiveLosses)) {
                return false;
            }
        }
    }

    // oppHand
    if (opp && this.oppHand) {
        if (handStrengthToString(opp.hand.strength).toLowerCase() !== this.oppHand.toLowerCase()) {
            return false;
        }
    }

    // targetTimeInStage
    if (opp && this.targetTimeInStage) {
        if (!inInterval(opp.timeInStage == -1 ? 0 //allow post-strip time to count as 0
                       : opp.timeInStage, this.targetTimeInStage)) {
            return false; // failed "targetTimeInStage" requirement
        }
    }

    // hasHand
    if (this.hasHand) {
        if (handStrengthToString(self.hand.strength).toLowerCase() !== this.hasHand.toLowerCase()) {
            return false;
        }
    }

    // alsoPlaying, alsoPlayingStage, alsoPlayingTimeInStage, alsoPlayingHand
    if (this.alsoPlaying) {
        var ap = this.getAlsoPlaying(opp);
        
        if (!ap) {
            return false; // failed "alsoPlaying" requirement
        }
        
        if (this.alsoPlayingStage) {
            if (!inInterval(ap.stage, this.alsoPlayingStage)) {
                return false; // failed "alsoPlayingStage" requirement
            }
        }
        
        if (this.alsoPlayingTimeInStage) {
            if(!inInterval(ap.timeInStage, alsoPlayingTimeInStage)) {
                return false; // failed "alsoPlayingTimeInStage" requirement
            }
        }
        
        if (this.alsoPlayingHand) {
            if (handStrengthToString(ap.hand.strength).toLowerCase() !== this.alsoPlayingHand.toLowerCase())
            {
                return false;
            }
        }
        
        // marker checks have very low priority as they're mainly intended to be used with other target types
        if (this.alsoPlayingSaidMarker) {
            if (!checkMarker(this.alsoPlayingSaidMarker, ap)) {
                return false
            }
        }
        
        if (this.alsoPlayingNotSaidMarker) {
            if (ap.markers[this.alsoPlayingNotSaidMarker]) {
                return false;
            }
        }
    }

    // filter counter targets
    if(!this.counters.every(function (ctr) {
        var desiredCount = parseInterval(ctr.attr('count'));
        var filterTag =    ctr.attr('filter');
        var filterGender = ctr.attr('gender');
        var filterStatus = ctr.attr('status');
        
        var count = players.countTrue(function(p) {
            return p && (filterTag == undefined || (p.tags && p.tags.indexOf(filterTag) >= 0))
                && (filterGender == undefined || (p.gender == filterGender))
                && (filterStatus == undefined || checkPlayerStatus(p, filterStatus));
        });
        
        return inInterval(count, desiredCount);
    })) {
        return false; // failed filter count
    }

    if (!this.tests.every(function(test) {
        var expr = expandDialogue(test.attr('expr'), self, opp);
        var value = test.attr('value');
        var interval = parseInterval(value);
        if (interval ? inInterval(Number(expr), interval) : expr == value) {
            return true;
        } else return false;
    })) {
        return false;
    }

    // totalRounds
    if (this.totalRounds) {
        if (!inInterval(currentRound, this.totalRounds)) {
            return false; // failed "totalRounds" requirement
        }
    }

    // timeInStage
    if (this.timeInStage) {
        if (!inInterval(self.timeInStage == -1 ? 0 //allow post-strip time to count as 0
                       : self.timeInStage, this.timeInStage)) {
                           return false; // failed "timeInStage" requirement
        }
    }

    // totalMales
    if (this.totalMales) {
        var count = players.countTrue(function(p) {
            return p && p.gender === eGender.MALE;
        });
        
        if (!inInterval(count, this.totalMales)) {
            return false; // failed "totalMales" requirement
        }
    }

    // totalFemales
    if (this.totalFemales) {
        var count = players.countTrue(function(p) {
            return p && p.gender === eGender.FEMALE;
        });
        
        if (!inInterval(count, this.totalFemales)) {
            return false; // failed "totalFemales" requirement
        }
    }

    // totalAlive
    if (this.totalAlive) {
        if (!inInterval(getNumPlayersInStage(STATUS_ALIVE), this.totalAlive)) {
            return false; // failed "totalAlive" requirement
        }
    }

    // totalExposed
    if (this.totalExposed) {
        if (!inInterval(getNumPlayersInStage(STATUS_EXPOSED), this.totalExposed)) {
            return false; // failed "totalExposed" requirement
        }
    }

    // totalNaked
    if (this.totalNaked) {
        if (!inInterval(getNumPlayersInStage(STATUS_NAKED), this.totalNaked)) {
            return false; // failed "totalNaked" requirement
        }
    }

    // totalMasturbating
    if (this.totalMasturbating) {
        if (!inInterval(getNumPlayersInStage(STATUS_MASTURBATING), this.totalMasturbating)) {
            return false; // failed "totalMasturbating" requirement
        }
    }

    // totalFinished
    if (this.totalFinished) {
        if (!inInterval(getNumPlayersInStage(STATUS_FINISHED), this.totalFinished)) {
            return false; // failed "totalFinished" requirement
        }
    }

    // self marker checks
    if (this.saidMarker) {
        if (!checkMarker(saidMarker, self)) {
            return false;
        }
    }
    
    if (this.notSaidMarker) {
        if (self.markers[notSaidMarker]) {
            return false;
        }
    }
    
    return true;
}

/**********************************************************************
 *****                 Behaviour Parsing Functions                *****
 **********************************************************************/

/************************************************************
 * Updates the behaviour of the given player based on the 
 * provided tag.
 ************************************************************/
Opponent.prototype.updateBehaviour = function(tag, opp) {
	/* determine if the AI is dialogue locked */
	//Allow characters to speak. If we change forfeit ideas, we'll likely need to change this as well.
	//if (players[player].forfeit[1] == CANNOT_SPEAK) {
		/* their is restricted to this only */
		//tag = players[player].forfeit[0];
	//}
    
    if (!this.allCases[tag]) {
        console.log("Warning: couldn't find "+tag+" dialogue for player "+this.slot);
        return false;
    }
    
    /* Find the best match. */
    var bestMatch = [];
    var bestMatchPriority = -1;
    for (var i = 0; i < this.allCases[tag].length; i++) {
        var curCase = this.allCases[tag][i];
        
        if (curCase.priority >= bestMatchPriority && curCase.basicRequirementsMet(this, opp)) {
            if (curCase.priority > bestMatchPriority) {
                console.log("New best match with " + curCase.priority + " priority.");
                bestMatch = [curCase];
                bestMatchPriority = curCase.priority;
            } else {
                bestMatch.push(curCase);
            }
        }
    }
    
    if (bestMatch.length == 0) {
        console.log("Warning: couldn't find any "+tag+" dialogue for player "+this.slot+" that meets conditions");
        return false;
    }
    
    states = bestMatch.reduce(function(list, caseObject) {
        return list.concat(caseObject.states);
    }, []);

    if (states.length > 0) {
        var chosenState = states[getRandomNumber(0, states.length)];
		
		if (chosenState.marker) {
			var match = chosenState.marker.match(/^(?:(\+|\-)([\w\-]+)|([\w\-]+)\s*\=\s*(\-?\d+))$/);
			if (match) {
				if (match[1] === '+') {
					// increment marker value
					if(!this.markers[match[2]]) {
						this.markers[match[2]] = 1;
					} else {
						this.markers[match[2]] += 1;
					}
					
				} else if (match[1] === '-') {
					// decrement marker value
					if(!this.markers[match[2]]) {
						this.markers[match[2]] = 0;
					} else {
						this.markers[match[2]] -= 1;
					}
				} else {
					// set marker value
					this.markers[match[3]] = parseInt(match[4], 10);
				}
			} else if (!this.markers[chosenState.marker]) {
				this.markers[chosenState.marker] = 1;
			}
		}
		
        this.allStates = states;
        this.chosenState = chosenState;
        this.chosenState.expandDialogue(this, opp);
        
        return true;
    } else {
        console.log("Warning: matched "+tag+" dialogue for player "+this.slot+" has no states");
        return false;
    }
    
    console.log("-------------------------------------");
    return false;
}

/************************************************************
 * Updates the behaviour of all players except the given player
 * based on the provided tag.
 ************************************************************/
function updateAllBehaviours (player, tag) {
	for (var i = 1; i < players.length; i++) {
		if (players[i] && (player === null || i != player)) {
			if (typeof tag === 'object') {
				tag.some(function(t) {
					return players[i].updateBehaviour(t, players[player]);
				});
			} else {
				players[i].updateBehaviour(tag, players[player]);
			}
		}
	}
}
