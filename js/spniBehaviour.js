/********************************************************************************
 This file contains the variables and functions that from the behaviours of the
 AI opponents. All the parsing of their files, as well as the structures to store
 that information are stored in this file.
 ********************************************************************************/

/**********************************************************************
 *****                    All Dialogue Triggers                   *****
 **********************************************************************/

var SELECTED = "selected";
var OPPONENT_SELECTED = "opponent_selected";
var GAME_START = "game_start";

var DEALING_CARDS = "dealing_cards";

var SWAP_CARDS = "swap_cards";
var ANY_HAND = "hand";
var BAD_HAND = "bad_hand";
var OKAY_HAND = "okay_hand";
var GOOD_HAND = "good_hand";

var PLAYER_MUST_STRIP = "must_strip";
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
var PLAYER_AFTER_MASTURBATING = "after_masturbating";

var OPPONENT_LOST = "opponent_lost";
var OPPONENT_STRIPPING = "opponent_stripping";
var OPPONENT_STRIPPED = "opponent_stripped";

var OPPONENT_CHEST_WILL_BE_VISIBLE = "opponent_chest_will_be_visible";
var OPPONENT_CROTCH_WILL_BE_VISIBLE = "opponent_crotch_will_be_visible";
var OPPONENT_CHEST_IS_VISIBLE = "opponent_chest_is_visible";
var OPPONENT_CROTCH_IS_VISIBLE = "opponent_crotch_is_visible";
var OPPONENT_START_MASTURBATING = "opponent_start_masturbating";
var OPPONENT_MASTURBATING = "opponent_masturbating";
var OPPONENT_HEAVY_MASTURBATING = "opponent_heavy_masturbating";
var OPPONENT_FINISHING_MASTURBATING = "opponent_finishing_masturbating";
var OPPONENT_FINISHED_MASTURBATING = "opponent_finished_masturbating";

var PLAYERS_TIED = "tie";

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
var MALE_CROTCH_IS_VISIBLE = "male_crotch_is_visible";

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
var FEMALE_CHEST_IS_VISIBLE = "female_chest_is_visible";
var FEMALE_CROTCH_IS_VISIBLE = "female_crotch_is_visible";

var FEMALE_MUST_MASTURBATE = "female_must_masturbate";
var FEMALE_START_MASTURBATING = "female_start_masturbating";
var FEMALE_MASTURBATING = "female_masturbating";
var FEMALE_HEAVY_MASTURBATING = "female_heavy_masturbating";
var FEMALE_FINISHED_MASTURBATING = "female_finished_masturbating";

var GAME_OVER_VICTORY = "game_over_victory";
var GAME_OVER_DEFEAT = "game_over_defeat";

var GLOBAL_CASE = "global";

/* Lists of case types eligible to be autoconverted if a target condition is present. */
var CONVERT_STRIP_CASES = [
    MALE_REMOVING_ACCESSORY,
    MALE_REMOVING_MINOR,
    MALE_REMOVING_MAJOR,
    MALE_CHEST_WILL_BE_VISIBLE,
    MALE_CROTCH_WILL_BE_VISIBLE,
    FEMALE_REMOVING_ACCESSORY,
    FEMALE_REMOVING_MINOR,
    FEMALE_REMOVING_MAJOR,
    FEMALE_CHEST_WILL_BE_VISIBLE,
    FEMALE_CROTCH_WILL_BE_VISIBLE,
];

var CONVERT_STRIPPED_CASES = [
    MALE_REMOVED_ACCESSORY,
    MALE_REMOVED_MINOR,
    MALE_REMOVED_MAJOR,
    MALE_CHEST_IS_VISIBLE,
    MALE_SMALL_CROTCH_IS_VISIBLE,
    MALE_MEDIUM_CROTCH_IS_VISIBLE,
    MALE_LARGE_CROTCH_IS_VISIBLE,
    MALE_CROTCH_IS_VISIBLE,
    FEMALE_REMOVED_ACCESSORY,
    FEMALE_REMOVED_MINOR,
    FEMALE_REMOVED_MAJOR,
    FEMALE_SMALL_CHEST_IS_VISIBLE,
    FEMALE_MEDIUM_CHEST_IS_VISIBLE,
    FEMALE_LARGE_CHEST_IS_VISIBLE,
    FEMALE_CHEST_IS_VISIBLE,
    FEMALE_CROTCH_IS_VISIBLE,
];

/* Tag alias list, mapping aliases to canonical tag names. */
var TAG_ALIASES = {
    // Add new aliases as follows:
    // 'alias_name': 'tag_name',
    // Franchise abbreviations
    'jojos_bizarre_adventure':    'jjba',
    'mlp':   'my_little_pony',
    'tengen_toppa_gurren_lagann': 'ttgl',
    // Other aliases
    'redhead': 'ginger',
    'sword':   'blade',
};

/* Tag implications list, mapping tags to lists of implied tags. */
var TAG_IMPLICATIONS = {
    // Add tag implications as follows:
    'huge_breasts': ['large_breasts'],
    'muscular': ['athletic'],
    'very_long_hair': ['long_hair'],
    'blue_hair': ['exotic_hair'],
    'green_hair': ['exotic_hair'],
    'pink_hair': ['exotic_hair'],
    'purple_hair': ['exotic_hair'],
    'hairy': ['pubic_hair'],
    'trimmed': ['pubic_hair'],
};


function fixupTagFormatting(tag) {
    return tag.replace(/\s/g, '').toLowerCase();
}

function getRelevantStagesForTrigger(tag, layers) {
    switch (tag) {
    case SELECTED:
    case GAME_START:
        return { min: 0, max: 0 };
    case SWAP_CARDS:
    case GOOD_HAND:
    case OKAY_HAND:
    case BAD_HAND:
    case ANY_HAND:
    case GAME_OVER_VICTORY:
        return { min: 0, max: layers };
    case PLAYER_MUST_STRIP_WINNING:
    case PLAYER_MUST_STRIP_NORMAL:
    case PLAYER_MUST_STRIP_LOSING:
    case PLAYER_MUST_STRIP:
    case PLAYER_STRIPPING:
        return { min: 0, max: layers - 1 };
    case PLAYER_STRIPPED:
        return { min: 1, max: layers };
    case PLAYER_MUST_MASTURBATE_FIRST:
    case PLAYER_MUST_MASTURBATE:
    case PLAYER_START_MASTURBATING:
        return { min: layers, max: layers };
    case PLAYER_MASTURBATING:
    case PLAYER_HEAVY_MASTURBATING:
    case PLAYER_FINISHING_MASTURBATING:
        return { min: layers + 1, max: layers + 1 };
    case PLAYER_FINISHED_MASTURBATING:
    case PLAYER_AFTER_MASTURBATING:
    case GAME_OVER_DEFEAT:
        return { min: layers + 2, max: layers + 2 };
    default:
        return { min: 0, max: layers + 2 };
    }
}

/**********************************************************************
 * Convert a tag to its 'canonical' form:
 * - Remove all whitespace characters
 * - Lowercase the string
 * - Handle all alias conversions
 **********************************************************************/
function canonicalizeTag(tag) {
    if (!tag) return undefined;
    
    tag = fixupTagFormatting(tag);
    while (TAG_ALIASES.hasOwnProperty(tag)) {
        tag = TAG_ALIASES[tag];
    }
    
    return tag;
}

/* Ensure that the alias and implications mappings are themselves canonical.
 * This could also be done in-place, but it feels cleaner and better to 
 * ensure that TAG_ALIASES and TAG_IMPLICATIONS *only* have canonical-form tags.
 */
let fixedAliases = {};
for (alias in TAG_ALIASES) {
    fixedAliases[fixupTagFormatting(alias)] = fixupTagFormatting(TAG_ALIASES[alias]);
}
TAG_ALIASES = fixedAliases;

let fixedImplies = {};
for (child_tag in TAG_IMPLICATIONS) {    
    let implied = TAG_IMPLICATIONS[child_tag].map(canonicalizeTag);
    let canonical_child = canonicalizeTag(child_tag);
    
    // If multiple entries in TAG_IMPLICATIONS alias to the same tag,
    // merge their lists of implications.
    if (fixedImplies.hasOwnProperty(canonical_child)) {
        implied.forEach(function (t) {
            if (fixedImplies[canonical_child].indexOf(t) < 0)
                fixedImplies[canonical_child].push(t);
        });
    } else {
        fixedImplies[canonical_child] = implied;
    }
}
TAG_IMPLICATIONS = fixedImplies;

/**
 * Represents an operation on a marker.
 * 
 * @param {string} base_name 
 * @param {string} op 
 * @param {any} rhs 
 * @param {Case} parentCase 
 */
function MarkerOperation(base_name, op, rhs, parentCase) {
    /**
     * The operation to perform on the referenced marker.
     * One of '=', '+', '-', '*', '/', '%'.
     * @type {string}
     */
    this.op = op;

    if (typeof(rhs) === 'number') {
        /**
         * The right-hand side value for this marker operation.
         * @type {number | string}
         */
        this.rhs = rhs;
    } else if (typeof(rhs) === 'string') {
        var parsed = parseInt(rhs, 10);
        if (!isNaN(parsed)) {
            this.rhs = parsed;
        } else {
            this.rhs = rhs;
        }
    } else {
        this.rhs = !!rhs ? 1 : 0;
    }

    /**
     * Whether this marker operation works with perTarget markers or not.
     * @type {boolean}
     */
    this.perTarget = base_name.endsWith('*');

    /**
     * The base name of the marker affected by this operation.
     * @type {string}
     */
    this.name = this.perTarget ? base_name.slice(0, -1) : base_name;

    /**
     * The parent Case that will be used for expanding variables
     * in the right-hand side of this operation, if necessary.
     * (used for e.g. variable bindings)
     * @type {Case}
     */
    this.parentCase = parentCase || null;
}

/**
 * Parse a MarkerOperation from a string.
 * 
 * @param {string} operationSpec
 * @param {Case} parentCase
 * @returns {MarkerOperation}
 */
function parseMarkerOp(operationSpec, parentCase) {
    var match = operationSpec.match(/^(?:(\+|-)([\w-]+\*?)|([\w-]+\*?)\s*([-+*%\/]?=)\s*(.*?)\s*)$/);
    var base_name = operationSpec;
    var op = '=';
    var rhs = 1;

    if (match) {
        if (match[1]) {
            /* First alternative: increment or decrement ops
             *  match[1] = operation type ('+' or '-')
             *  match[2] = marker name, incl. per-target signifier
             */
            base_name = match[2];
            op = match[1];
            rhs = 1;
        } else {
            /* second alternative: set operation
             *  match[3] = marker name, incl. per-target signifier
             *  match[4] = operation
             *  match[5] = value
             * 
             * match[4] is either going to be a two-character operation
             * (+=, -=, *=, etc.) or just '='.
             * Either way, we only need the first character of match[4].
             */
            base_name = match[3];
            op = match[4][0];
            rhs = match[5];
        }
    } else {
        base_name = operationSpec;
    }

    return new MarkerOperation(base_name, op, rhs, parentCase);
}

/**
 * Construct a MarkerOperation from an XML `<marker>` element. 
 * 
 * @param {HTMLElement} xml 
 * @param {Case} parentCase 
 * @returns {MarkerOperation}
 */
function parseMarkerXML(xml, parentCase) {
    var $elem = $(xml);
    var name = $elem.attr("name");
    var op = $elem.attr("op") || "=";
    var rhs = $elem.attr("value");
    if (rhs === undefined) rhs = '1';

    return new MarkerOperation(name, op[0], rhs, parentCase);
}

/**
 * Evaluate the new value of the referenced marker after carrying out
 * this operation.
 * 
 * @param {Player} self
 * @param {Player} opp
 * @returns {number | string}
 */
MarkerOperation.prototype.evaluate = function (self, opp) {
    if (!self) return;

    /* Convert this.rhs to either a number value if possible.
     * Otherwise make it a string.
     */
    var rhs = this.rhs;
    if (typeof(rhs) === 'string') {
        rhs = expandDialogue(
            this.rhs, self, opp, 
            this.parentCase && this.parentCase.variableBindings
        );
    }

    var parsed = parseInt(rhs, 10);
    if (!isNaN(parsed)) {
        rhs = parsed;
    }

    if (this.op === '=') {
        /* For = ops, just return the RHS directly */
        return rhs;
    } else {
        /* For arithmetic ops, convert the current marker value to a number first */
        var lhs = self.getMarker(
            this.name,
            this.perTarget ? opp : null,
            true
        );

        if (typeof(rhs) !== 'number' || isNaN(rhs)) {
            rhs = 0;
        }

        switch (this.op) {
        case '+':
        default:
            return lhs + rhs;
        case '-':
            return lhs - rhs;
        case '*':
            return lhs * rhs;
        case '/':
            return (rhs === 0) ? 0 : Math.round(lhs / rhs);
        case '%':
            return (rhs === 0) ? 0 : lhs % rhs;
        }
    }
}

/**
 * Evaluate this operation and apply the new calculated marker
 * value.
 * 
 * @param {Player} self
 * @param {Player} opp
 */
MarkerOperation.prototype.apply = function (self, opp) {
    if (!self) return;

    var new_val = this.evaluate(self, opp);
    self.setMarker(
        this.name,
        this.perTarget ? opp : null,
        new_val
    );
}

/**
 * Make a snapshot of this marker operation that can be serialized as
 * JSON.
 * 
 * @param {Player} self
 * @param {Player} opp
 * @param {Case} contextCase
 */
MarkerOperation.prototype.serialize = function (self, opp) {
    return {
        name: this.name,
        op: this.op,
        perTarget: this.perTarget,
        value: this.evaluate(self, opp)
    };
}


/**
 * A State-based operation for setting per-player attributes:
 * - AI Difficulty / Intelligence
 * - Label
 * - Size
 * - Gender
 * 
 * These operations take string values.
 * 
 * @param {string} attr 
 * @param {string} value 
 * @param {Case} parentCase 
 */
function PlayerAttributeOperation (attr, value, parentCase) {
    this.attr = attr;
    this.value = value;
    this.parentCase = parentCase;
}

/**
 * Apply this operation to a player.
 * 
 * @param {Player} self 
 * @param {Player} opp 
 * @returns 
 */
PlayerAttributeOperation.prototype.apply = function (self, opp) {
    var value = expandDialogue(
        this.value, self, opp, 
        this.parentCase && this.parentCase.variableBindings
    );

    switch (this.attr) {
    case "intelligence":
        self.setIntelligence(value);
        break;
    case "label":
        self.setLabel(value);
        break;
    case "size":
        self.size = value;
        break;
    case "gender":
        self.gender = value;
        break;
    default:
        console.error("Unknown player attribute: ", this.attr);
        break;
    }
}

/**
 * Get a key for sorting operations of this type.
 * 
 * Operations with lower sort values should be applied before
 * operations with higher values.
 * @returns {number}
 */
PlayerAttributeOperation.prototype.sortKey = function () {
    return (this.attr == "label") ? 0 : 1;
}

/**
 * A State-based operation for altering a character's forfeit:
 * - stamina (for opponents still in the game)
 * - current forfeit timer (for opponents currently masturbating)
 * - heavy masturbation status (ditto)
 * 
 * Operations on player stamina and timers take numeric RHS values and
 * support arithmetic operations, similarly to marker operations.
 * 
 * Operations on heavy masturbation status take either 'true', 'false', or 'reset',
 * and do not support arithmetic operations.
 * 
 * @param {string} attr 
 * @param {string} op 
 * @param {string} value 
 * @param {Case} parentCase 
 */
function ForfeitTimerOperation (attr, op, value, parentCase) {
    this.attr = attr;
    this.op = op || "=";
    this.value = value;
    this.parentCase = parentCase;
}

/**
 * Apply this operation to a player.
 * 
 * @param {Player} self 
 * @param {Player} opp 
 */
ForfeitTimerOperation.prototype.apply = function (self, opp) {
    if (
        self.finished ||
        (!self.out && this.attr != "stamina") ||
        (self.out && this.attr == "stamina")
    ) return;

    if (this.attr == "stamina" || this.attr == "timer") {
        var rhs = expandDialogue(
            this.value, self, opp, 
            this.parentCase && this.parentCase.variableBindings
        );

        let lhs = 0;
        if (this.attr == "timer") {
            lhs = self.timer;
        } else {
            lhs = self.stamina;
        }

        rhs = parseInt(rhs, 10);
        if ((typeof(rhs) !== 'number') || isNaN(rhs)) {
            rhs = 0;
        }

        let newValue = lhs;
        switch (this.op) {
        case '=':
        default:
            newValue = rhs;
            break;
        case '+':
            newValue = lhs + rhs;
            break;
        case '-':
            newValue = lhs - rhs;
            break;
        case '*':
            newValue = lhs * rhs;
            break;
        case '/':
            newValue = (rhs === 0) ? 0 : Math.round(lhs / rhs);
            break;
        case '%':
            newValue = (rhs === 0) ? 0 : lhs % rhs;
            break;
        }

        /* Values <= 0 are not valid for stamina and timer. */
        if (newValue <= 0) newValue = 1;

        if (this.attr == "timer") {
            self.timer = newValue;
        } else {
            self.stamina = newValue;
        }
    } else if (this.attr == "redirect-finish") {
        let finishingTarget = findVariablePlayer(
            this.value, self, opp, this.parentCase && this.parentCase.variableBindings
        );

        if (!finishingTarget) {
            console.error("Unknown finish redirection target: ", this.value);
        } else if (finishingTarget === humanPlayer) {
            console.error("Attempted to redirect " + self.id + "'s finishing dialogue to human player");
        } else {
            self.finishingTarget = finishingTarget;
        }
    } else if (this.attr == "heavy") {
        if (this.value == "true") {
            /* Force the player into heavy masturbation. */
            self.forfeit = [PLAYER_HEAVY_MASTURBATING, CANNOT_SPEAK];
            self.forfeitLocked = true;
        } else if (this.value == "false") {
            /* Force the player out of heavy masturbation. */
            self.forfeit = [PLAYER_MASTURBATING, CAN_SPEAK];
            self.forfeitLocked = true;
        } else if (this.value == "reset") {
            /* Reset the player's forfeit type. */
            self.forfeitLocked = false;
            self.updateHeavyMasturbation();
        } else {
            console.error("Unknown heavy forfeit value: ", this.value);
        }
    } else {
        console.error("Unknown forfeit attribute: ", this.attr);
    }

}

/**
 * Get a key for sorting operations of this type.
 * 
 * Operations with lower sort values should be applied before
 * operations with higher values.
 * @returns {number}
 */
ForfeitTimerOperation.prototype.sortKey = function () {
    /* Forfeit timer updates, at least, should come before any operations
     * that modify heavy masturbation status; this ensures that heavy
     * masturbation reset operations use the correct timer value when
     * rolling to see if the opponent should be heavily masturbating or not.
     */
    switch (this.attr) {
    case "timer": return 0;
    case "heavy": return 2;
    case "stamina":
    case "redirect-finish":
    default:
        return 1;
    }
}

/**
 * A State-based operation for altering player nicknames.
 * 
 * These operations take a target, an operation, and a string nickname value.
 * 
 * The target can be either '*' (to modify the default nickname list), or a 
 * player reference. Player references are resolved using the same rules as
 * variables: 'self', 'target', and 'winner' are all valid, as are bound
 * variable names. 
 * 
 * Depending on the operation type, the nickname list for the referenced player
 * is modified as follows:
 * - "=": (re)sets the target nickname list to only contain the operation value.
 * - "+": adds the operation value to the target nickname list, if not already present.
 * - "-": removes the operation value from the target nickname list, if present.
 * 
 * Nickname operations respect the `nickname` per-target marker: if set, it will
 * override the normal contents of the target nickname list, and the marker will
 * be cleared when the operation is applied.
 *  
 * @param {string} target 
 * @param {string} op
 * @param {string} value 
 * @param {Case} parentCase 
 */
 function NicknameOperation (target, op, value, parentCase) {
    this.target = target.toLowerCase();
    this.op = op;
    this.value = value;
    this.parentCase = parentCase;
}

/**
 * Apply this operation to a player.
 * 
 * @param {Player} self 
 * @param {Player} opp 
 */
NicknameOperation.prototype.apply = function (self, opp) {
    var newNicknames = [];
    var nickPlayerID = '';
    
    if (this.target === '*') {
        nickPlayerID = '*';
        if ("*" in self.nicknames) newNicknames = self.nicknames["*"].slice();
    } else {
        let nickPlayer = findVariablePlayer(
            this.target, self, opp, this.parentCase && this.parentCase.variableBindings
        );
    
        if (!nickPlayer) {
            console.error("Unknown player for nickname:", this.target);
            return;
        }

        nickPlayerID = nickPlayer.id;
        let nickmarker = self.getMarker('nickname', nickPlayer, false, true);

        if (nickmarker) {
            newNicknames = [nickmarker];
            self.setMarker("nickname", nickPlayer, "");
        } else if (nickPlayerID in self.nicknames || '*' in self.nicknames) {
            newNicknames = (self.nicknames[nickPlayerID] || self.nicknames['*']).slice();
        }
    }

    /* NOTE: don't apply variable expansion to this.value here, since it will be expanded later
     * during nickname substitution.
     */

    if (this.op == "clear" || (this.op == "=" && !this.value)) {
        newNicknames = [];
    } else if (this.op == "=") {
        newNicknames = [this.value];
    } else if (this.op == "+") {
        if (this.value.length > 0 && newNicknames.indexOf(this.value) < 0) newNicknames.push(this.value);
    } else if (this.op == "-") {
        newNicknames = newNicknames.filter(function (v) { return v !== this.value; });
    } else {
        console.error("Unknown nickname operation: ", this.op);
    }

    if (newNicknames.length === 0) {
        delete self.nicknames[nickPlayerID];
    } else {
        self.nicknames[nickPlayerID] = newNicknames;
    }
}

/**
 * Get a key for sorting operations of this type.
 * 
 * Operations with lower sort values should be applied before
 * operations with higher values.
 * @returns {number}
 */
NicknameOperation.prototype.sortKey = function () {
    /* Operations that clear the nickname list ("clear" and "=") should
     * come before operations that add nicknames ("=" and "+"), which should
     * themselves come before operations that remove nicknames ("-").
     * 
     * This allows the nickname list to be cleared and replaced with a new
     * set of (potentially multiple) nicknames, and it ensures that situations
     * where the same nickname is (for some reason) both added and removed in a single state
     * resolve in the same way every time.
     */
    switch (this.op) {
    case "clear": return 0;
    case "=": return 1;
    case "+": return 2;
    case "-": return 3;
    default: return 4;
    }
}

/**
 * Parse an XML operation element.
 * 
 * @param {HTMLElement} xml 
 * @param {Case} parentCase 
 * @returns {PlayerAttributeOperation|ForfeitTimerOperation|NicknameOperation}
 */
 function parseOperationXML(xml, parentCase) {
    var $elem = $(xml);
    var type = xml.tagName.toLowerCase();
    
    if (type == "forfeit") {
        return new ForfeitTimerOperation(
            $elem.attr("attr"), 
            $elem.attr("op") || "=",
            $elem.attr("value"),
            parentCase
        );
    } else if (type == "nickname") {
        return new NicknameOperation(
            $elem.attr("character"),
            $elem.attr("op") || "=",
            $elem.attr("name"),
            parentCase
        );
    } else if (type == "player") {
        return new PlayerAttributeOperation($elem.attr("attr"), $elem.attr("value"), parentCase);
    } else {
        console.error("Unknown operation tag type: ", type);
    }
}

/**********************************************************************
 *****                  State Object Specification                *****
 **********************************************************************/

function State($xml_or_state, parentCase) {
    if ($xml_or_state instanceof State) {
        /* Shallow-copy the state: */
        for (var prop in $xml_or_state) {
            if (!$xml_or_state.hasOwnProperty(prop)) continue;
            this[prop] = $xml_or_state[prop];
        }

        return;
    }

    var $xml = $xml_or_state;

    this.parentCase = parentCase;
    this.id = $xml.attr('dev-id') || null;
    this.image = $xml.attr('img');
    this.direction = $xml.attr('direction') || 'down';
    this.location = $xml.attr('location') || '';
    this.dialogue_layering = $xml.attr('dialogue-layer');
    this.fontSize = $xml.attr('font-size');
    if (!['normal', 'small', 'smaller'].includes(this.fontSize)) this.fontSize = undefined;
    this.alt_images = null;

    /** @type {MarkerOperation[]} */
    this.markers = [];
    
    var markerOp = $xml.attr('marker');
    if (markerOp) {
        this.markers.push(parseMarkerOp(markerOp, parentCase));
    }

    /** @type {(PlayerAttributeOperation|NicknameOperation|ForfeitTimerOperation)[]} */
    this.operations = [];

    ["intelligence", "size", "gender", "label"].forEach(function (attr) {
        var val = $xml.attr("set-" + attr);
        if (val !== undefined) {
            this.operations.push(new PlayerAttributeOperation(attr, val, parentCase));
        }
    }.bind(this));

    if (this.rawDialogue = $xml.children('text').html()) {
        this.alt_images = $xml.children('alt-img');
        $xml.children('markers').children('marker').each(function (idx, elem) {
            this.markers.push(parseMarkerXML(elem, parentCase));
        }.bind(this));
        $xml.children('operations').children().each(function (idx, elem) {
            this.operations.push(parseOperationXML(elem, parentCase));
        }.bind(this));
    } else {
        this.rawDialogue = $xml.html();
    }

    this.operations.sort(function (a, b) {
        return a.sortKey() - b.sortKey();
    });

    this.weight = Number($xml.attr('weight')) || 1;
    if (this.weight < 0) this.weight = 0;
    
    if (this.location && Number(this.location) == this.location) {
        // It seems that location was specified as a number without "%"
        this.location = this.location + "%";
    }

    this.oneShotId = $xml.attr('oneShotId');
    
    var collectibleId = $xml.attr('collectible') || undefined;
    var collectibleOp = $xml.attr('collectible-value') || undefined;
    
    /* Keep track of the old persist-marker flag.
     * recordTargetedCase() (in spniCore.js) checks this flag and, if set,
     * will add the marker name attached to this State to the character's
     * persistentMarkers list.
     *
     * TODO: Remove this once all characters using persistent markers
     * have migrated over to the system in #74.
     */
    this.legacyPersistentFlag = ($xml.attr('persist-marker') === 'true');

    if (collectibleId) {
        this.collectible = {id: collectibleId, op: 'unlock', val: null};
        
        if (collectibleOp) {
            if (collectibleOp.startsWith('+')) {
                this.collectible.op = 'inc';
                this.collectible.val = parseInt(collectibleOp.substring(1), 10);
            } else if (collectibleOp.startsWith('-')) {
                this.collectible.op = 'dec';
                this.collectible.val = parseInt(collectibleOp.substring(1), 10);
            } else {
                this.collectible.op = 'set';
                this.collectible.val = parseInt(collectibleOp, 10);
            }
            
            if (!this.collectible.val || this.collectible.val <= 0) {
                this.collectible.op = 'unlock';
                this.collectible.val = null;
            }
        }
    }
}

/**
 * Evaluate a particular marker change tied to this state.
 * 
 * @param {string} name
 * @param {Player} self
 * @param {Player} opp
 * @param {boolean} perTarget
 */
State.prototype.evaluateMarker = function (name, self, opp, perTarget) {
    for (var i = 0; i < this.markers.length; i++) {
        var marker = this.markers[i];

        if (marker.name === name && ((perTarget && opp) || !marker.perTarget)) {
            return marker.evaluate(self, opp);
        }
    }
}

/**
 * Apply all marker changes tied to this state.
 * 
 * @param {Player} self
 * @param {Player} opp
 */
State.prototype.applyMarkers = function (self, opp) {
    for (var i = 0; i < this.markers.length; i++) {
        var marker = this.markers[i];

        var newVal = marker.evaluate(self, opp);
        self.setMarker(
            marker.name,
            marker.perTarget ? opp : null,
            newVal
        );
    }
}

/**
 * Apply player operations tied to this state.
 * 
 * @param {Player} self
 * @param {Player} opp
 */
State.prototype.applyOperations = function (self, opp) {
    this.operations.forEach(function (op) {
        op.apply(self, opp);
    });
}

State.prototype.expandDialogue = function(self, target) {
    this.dialogue = expandDialogue(this.rawDialogue, self, target, this.parentCase && this.parentCase.variableBindings);
}

/**
 * Get all possible images that can be used by this state.
 * 
 * @param stage {number} The stage number to use when checking alt image
 * stage conditions, and for replacing '#' in image names.
 * @returns {Array} An array of image names.
 */
State.prototype.getPossibleImages = function (stage) {
    if (this.alt_images) {
        var images = this.alt_images.filter(function () {
            return checkStage(stage, $(this).attr('stage'));
        }).map(function () {
            return $(this).text().replace('#', stage);
        }).get();
        if (images.length) return images;
    }
    return this.image ? [ this.image.replace('#', stage) ] : [];
}

State.prototype.selectImage = function (stage) {
    if (this.alt_images) {
        var $altImages = this.alt_images.filter(function () {
            return checkStage(stage, $(this).attr('stage'));
        });
        if ($altImages.length > 0) {
            this.image = $($altImages.get(getRandomNumber(0, $altImages.length))).text();
        }
    }
}

State.prototype.applyCollectible = function (player) {
    if (COLLECTIBLES_ENABLED && this.collectible && player.collectibles) {
        player.collectibles.some(function (collectible) {
            if (collectible.id === this.collectible.id) {
                console.log(
                    "Performing collectible op: "+
                    this.collectible.op.toUpperCase()+
                    " on ID: "+
                    this.collectible.id
                );

        if (collectible.isUnlocked()) {
                    console.log("Collectible already unlocked; returning");
            return;
        }
                
                switch (this.collectible.op) {
                default:
                case 'unlock':
                    collectible.unlock();
                    break;
                case 'inc':
                    collectible.incrementCounter(this.collectible.val);
                    break;
                case 'dec':
                    collectible.incrementCounter(-this.collectible.val);
                    break;
                case 'set':
                    collectible.setCounter(this.collectible.val);
                    break;
                }
                
                if (collectible.isUnlocked() && !COLLECTIBLES_UNLOCKED) {
                    player.pendingCollectiblePopups.push(collectible);
                }
                
                return true;
            }
        }.bind(this));
    }
}

State.prototype.applyOneShot = function (player) {
    if (this.oneShotId) {
        player.oneShotStates[this.oneShotId] = true;
    }
}

/********************************************************************
 * Check that the state doesn't set a marker or contains text that
 * another player has already ascertained a maximum number of
 * opponents saying. 
 *
 * Each item in unwantedMarkers and unwantedSayings is a two-element
 * array where the first element is the restricted player and the
 * second slement is the marker or dialogue text.
 ********************************************************************/
State.prototype.checkUnwanteds = function (self, target) {
    var savedState = self.chosenState;
    var ok = !players.some(function(p) { // Check that none of the other players' unwanted lists are violated,
        if (p == self) return false;  // Shouldn't happen
        if (!p.chosenState || !p.updatePending || !p.chosenState.parentCase) return false; // Ignore if they don't have a case
        if (this.markers.length > 0 && p.chosenState.parentCase.unwantedMarkers
            && p.chosenState.parentCase.unwantedMarkers.some(function(item) {
                if (self == item[0]) {
                    self.chosenState = this;  // Temporarily set chosenState to this state to be able to use checkMarker()
                    return checkMarker(item[1], self, target, true); // item[1] is the marker predicate from the <condition>
                    // If the marker matched, true is returned, which means not OK.
                }
                return false;  // Not us
            }, this)) {
            return true;  // At least one marker matched.
        }
        if (p.chosenState.parentCase.unwantedSayings
            && p.chosenState.parentCase.unwantedSayings.some(function(item) {
                return self == item[0]
                    && normalizeConditionText(this.rawDialogue).indexOf(normalizeConditionText(item[1])) >= 0;
            }, this)) {
            return true;  // At least one line of text matched.
        }
        if (p.chosenState.parentCase.unwantedPoses
            && p.chosenState.parentCase.unwantedPoses.some(function (item) {
                return self == item[0] && poseNameMatches(item[1], this.image);
            }, this)) {
            return true; // At least one pose matched.
        }
        return false;
    }, this);
    self.chosenState = savedState;
    return ok;
}

function getTargetMarker(marker, target) {
    if (!target) { return marker; }
    return "__" + target.id + "_" + marker;
}

/**
 * Normalizes a name meant for use as a variable, such as a variable binding
 * or a character ID.
 * @param {string} variable The binding name or character ID to normalize.
 * @returns {string} A normalized variable name.
 */
function normalizeBindingName (variable) {
    if (!variable) return variable;
    return variable.replace(/\W/g, '').toLowerCase();
}

function findVariablePlayer(variable, self, target, bindings) {
    var player;
    if (!variable) return null;
    if (variable == 'self') return self;
    if (variable == 'target') return target;
    if (variable == 'winner' && recentWinner >= 0) return players[recentWinner];

    var normVariable = normalizeBindingName(variable);
    if (bindings && normVariable in bindings) return bindings[normVariable];
    if (players.some(function (p) {
        if (normalizeBindingName(p.id) === normVariable) {
            player = p;
            return true;
        }
    })) {
        return player;
    }
    return null;
}

/************************************************************
 * Applies any personal nicknames to target player.
 * First looks for a per-character marker called "nickname".
 * Second, picks a random nickname from the list of nicknames
 * for the character from the <nicknames> section after 
 * variable expansion (with self set to null in order to 
 * avoid infinite recursion).
 ************************************************************/
function expandNicknames (self, target) {
    if (self) {
        var nickmarker = self.getMarker('nickname', target, false, true);
        if (nickmarker) return nickmarker;
        if (target.id in self.nicknames || '*' in self.nicknames) {
            var nickList = self.nicknames[target.id] || self.nicknames['*'];
            return expandDialogue(nickList[getRandomNumber(0, nickList.length)], null, target);
        }
    }
    return target.label.escapeHTML();
}

function expandClothingVariable(clothing, fn, args, self, target, bindings, isRemoved) {
    if (fn == 'ifplural' && args) {
        args = args.split('|');
        return expandDialogue(args[clothing.plural ? 0 : clothing.plural === null && args.length > 2 ? 2 : 1], self, target, bindings);
    } else if (fn === 'plural') {
        return clothing.plural ? 'plural'
            : clothing.plural === null ? 'uncountable' : 'single';
    } else if (fn === 'toplural') {
        if (clothing.plural === false) {
            return pluralize(clothing.name);
        } else {
            return clothing.name;
        }
    } else if (fn == 'a') {
        if (clothing.plural === false) {
            return indefiniteArticle(clothing.name) + ' ';
        } else {
            return '';
        }
    } else if (fn == "withart") {
        if (clothing.plural === false) {
            return indefiniteArticle(clothing.name) + ' ' + clothing.name;
        } else {
            return clothing.name;
        }
    } else if ((fn == 'type' || fn == 'position' || fn == 'generic') && args === undefined) {
        return clothing[fn];
    } else if (fn === "id") {
        return clothing.id || "";
    } else if (fn === "reveals") {
        if (!isRemoved) return "none";

        var revealedPos = getRevealedPosition(target || self, clothing);
        if (revealedPos == FULL_ARTICLE) {
            return "both";
        } else if (revealedPos == UPPER_ARTICLE) {
            return "chest";
        } else if (revealedPos == LOWER_ARTICLE) {
            return "crotch";
        } else {
            return "none";
        }
    } else if (fn === undefined && args === undefined) {
        return clothing.name;
    }
}

/************************************************************
 * Expands ~target.*~ and ~[player].*~ variables.
 ************************************************************/
function expandPlayerVariable(split_fn, args, player, self, target, bindings) {
    if (split_fn.length > 0) var fn = split_fn[0].toLowerCase();
    
    switch (fn) {
    case 'id':
        return player.id;
    case 'position':
        var other = (!args ? self : findVariablePlayer(args, self, target, bindings));
        if (player.slot === other.slot) return 'self';
        if (player === humanPlayer || other === humanPlayer) return 'across';
        return (player.slot < other.slot) ? 'left' : 'right';
    case 'distance':
        var other = (!args ? self : findVariablePlayer(args, self, target, bindings));
        if (player !== other && (player === humanPlayer || other === humanPlayer)) return undefined;
        return Math.abs(player.slot - other.slot);
    case 'slot':
        return player.slot;
    case 'compatible':
    case 'attracted':
        if (player.hasTag("bisexual")) return true;
        var other = (!args ? self : findVariablePlayer(args, self, target, bindings));
        if (player.hasTag('straight')) {
            return player.gender !== other.gender;
        }
        if (player.gender == eGender.MALE && player.hasTag('gay')) {
            return other.gender === eGender.MALE;
        }
        if (player.gender == eGender.FEMALE && player.hasTag('lesbian')) {
            return other.gender === eGender.FEMALE;
        }
        if (player.hasTag('bi-curious')) {
            return fn == 'compatible' || player.gender !== other.gender
        }
        if (player.hasTag('reverse_bi-curious')) {
            return fn == 'compatible' || player.gender === other.gender
        }
        return undefined;
    case 'collectible':
        var collectibleID = split_fn[1];
        if (collectibleID) {
            var collectibles = player.collectibles.filter(function (c) { return c.id === collectibleID; });
            var targetCollectible = collectibles[0];
            
            if (split_fn[2] && split_fn[2] === 'counter') {
                if (targetCollectible) return targetCollectible.getCounter();
                return 0;
            } else if (split_fn[2] && split_fn[2] === 'wearing') {
                if (targetCollectible && targetCollectible.clothing) {
                    return humanPlayer.clothing.some(function (clothing) {
                        return clothing.id === targetCollectible.clothing.id;
                    });
                }
                return false;
            } else {
                if (targetCollectible) return targetCollectible.isUnlocked();
                return false;
            }
        } else {
            return "collectible"; // no collectible ID supplied
        }
    case 'marker':
    case 'persistent':
    case 'targetmarker':
        var markerName = split_fn[1];
        if (markerName) {
            return player.getMarker(markerName, target, false, fn === 'targetmarker') || "";
        } else {
            return fn; //didn't supply a marker name
        }
    case 'tag':
        return player.hasTag(split_fn[1]) ? 'true' : 'false';
    case 'costume':
        if (!player.alt_costume) return 'default';
        return player.alt_costume.id;
    case 'size':
        return player.size;
    case 'gender':
        return player.gender;
    case 'intelligence':
        return player.intelligence;
	case 'tostutter':
		var n = Math.min(Math.max((parseInt(args, 10) || 1), 1), 10);
		var name = expandNicknames(self, player);
		var ret = name;
		for (var i = 0; i < n; i++) {
			ret = name[0] + "-" + ret;
		}
		return ret;
    case 'ifmale':
        return args.split('|')[(player.gender == 'male' ? 0 : 1)];
    case 'place':
        if (player.out) return players.countTrue() + 1 - player.outOrder;
        return 1 + players.countTrue(function(p) { return p.countLayers() > player.countLayers(); });
    case 'revplace':
        if (player.out) return player.outOrder;
        return 1 + players.countTrue(function(p) { return p.out || p.countLayers() < player.countLayers(); });
    case 'biggestlead':
        return player.biggestLead;
    case 'lead':
        return player.countLayers() - players.reduce(function(max, p) {
            if (p != player) {
                return Math.max(max, p.countLayers());
            } else return max;
        }, 0);
    case 'trail':
        return players.reduce(function(min, p) {
            if (p != player && !p.out) {
                return Math.min(min, p.countLayers());
            } else return min;
        }, 10) - player.countLayers();
    case 'diff':
        var other = (!args ? self : findVariablePlayer(args, self, target, bindings));
        if (other) {
            return player.countLayers() - other.countLayers();
        }
        return undefined;
    case 'stage':
        return player.stage;
    case 'hand':
        if (split_fn[1] == 'score') {
            return player.hand.score();
        } else if (split_fn[1] == 'noart' || split_fn[1] === undefined) {
            return player.hand.describe(split_fn[1] == undefined);
        }
        throw new Error('Incorrect use of .hand');
    case 'cards':
        var n = player.hand.tradeIns.countTrue();
        if (split_fn.length == 1) {
            return String(n);
        } else if (split_fn[1] == 'ifplural') {
            return expandDialogue(args.split('|')[n == 1 ? 1 : 0], player, target, bindings);
        } else if (split_fn[1] == 'text' && args === undefined) {
            return [ 'zero', 'one', 'two', 'three', 'four', 'five' ][n];
        }
        throw new Error('Incorrect use of .cards');
    case 'wearing':
        {
            var types = [], positions = [], names = [];
            args.split('|').forEach(function(keyword) {
                if ([IMPORTANT_ARTICLE, MAJOR_ARTICLE, MINOR_ARTICLE, EXTRA_ARTICLE].indexOf(keyword) >= 0) {
                    types.push(keyword);
                } else if ([UPPER_ARTICLE, LOWER_ARTICLE, FULL_ARTICLE, OTHER_ARTICLE,
                            'arms', 'feet', 'hands', 'head', 'legs', 'neck', 'waist'].indexOf(keyword) >= 0) {
                    positions.push(keyword);
                } else {
                    names.push(keyword);
                }
            });
            var result = player.findClothing(types.length ? types : undefined,
                                             positions.length ? positions : undefined,
                                             names.length ? names : undefined);
            if (result.length) {
                return result[getRandomNumber(0, result.length)].name;
            }
            return "";
        }
    case 'numstripped':
        return args.split('|').reduce(function(sum, type) {
            return sum + (player.numStripped[type] || 0);
        }, 0);
    case 'timeinstage':
        return player.timeInStage;
    case 'ticksinstage':
        return player.ticksInStage;
    case 'timer':
        return player.out ? player.timer : player.stamina;
    case 'heavy':
        return (player.forfeit[0] === PLAYER_HEAVY_MASTURBATING) ? "true" : "false";
    default:
        return expandNicknames(self, player);
    }
}

function pluralize (text) {
    if (text.match(/(ff?|ife)$/)) {
        return text.replace(/f[fe]?$/, 'ves');
    } else if (text.match(/(s|[cs]h)$/)) {
        return text + 'es';
    } else if (text.match(/[^ae]y$/)) {
        return text.replace(/y$/, 'ies');
    } else {
        return text + 's';
    }
}

function indefiniteArticle(word) {
    if (/^[aeio]|^u(?!ni)/.test(word)) {
        return "an";
    } else {
        return "a";
    }
}

/************************************************************
 * Expands variables etc. in a line of dialogue.
 ************************************************************/
function expandDialogue (dialogue, self, target, bindings) {
    function substitute(match, variable, fn, args) {
        // If substitution fails, return match unchanged.
        var substitution = match;
        var fn_parts = [];
        
        if (fn) {
            fn_parts = fn.split('.');
            fn = fn_parts[0].toLowerCase();
        }
        
        try {
            switch (variable.toLowerCase()) {
            case 'player':
                substitution = expandPlayerVariable(fn_parts, args, humanPlayer, self, target, bindings);
                break;
            case 'name':
                substitution = expandNicknames(self, target);
                break;
            case 'clothing':
                var clothing = (target||self).removedClothing;
                substitution = expandClothingVariable(clothing, fn, args, self, target, bindings, true);
                break;
            case 'revealed':
                target = target || self;
                clothing = target.removedClothing;
                substitution = '';
                if ([UPPER_ARTICLE, LOWER_ARTICLE, FULL_ARTICLE].indexOf(clothing.position) >= 0) {
                    var revealedClothing
                        = target.findClothing(undefined,
                                              clothing.position == FULL_ARTICLE
                                              ? [UPPER_ARTICLE, LOWER_ARTICLE, FULL_ARTICLE]
                                              : [clothing.position, FULL_ARTICLE]);
                    if (revealedClothing.length) {
                        substitution = expandClothingVariable(revealedClothing[getRandomNumber(0, revealedClothing.length)],
                                                              fn, args, self, target, bindings, false);
                    }
                }
                break;
            case 'cards': /* determine how many cards are being swapped */
                substitution = expandPlayerVariable(['cards'].concat(fn_parts), args, self, self, target, bindings);
                break;
            case 'collectible':
                fn = fn_parts[0];
                if (fn) {
                    var collectibles = self.collectibles.filter(function (c) { return c.id === fn; });
                    var targetCollectible = collectibles[0];
                    
                    if (fn_parts[1] && fn_parts[1] === 'counter') {
                        if (targetCollectible) {
                            substitution = targetCollectible.getCounter();
                        } else {
                            substitution = 0;
                        }
                    } else if (fn_parts[1] && fn_parts[1] === 'wearing') {
                        if (targetCollectible && targetCollectible.clothing) {
                            substitution = humanPlayer.clothing.some(function (clothing) {
                                return clothing.id === targetCollectible.clothing.id;
                            });
                        } else {
                            substitution = false;
                        }
                    } else {
                        if (targetCollectible) {
                            substitution = targetCollectible.isUnlocked();
                        } else {
                            substitution = false;
                        }
                    }
                } else {
                    console.error("No collectible ID specified");
                }
                break;
            case 'marker':
            case 'persistent':
            case 'targetmarker':
                fn = fn_parts[0];  // make sure to keep the original string case intact 
                if (fn) {
                    /* if variable is 'targetmarker', specifically only look for per-target markers */
                    substitution = self.getMarker(fn, target, false, variable.toLowerCase() === 'targetmarker') || "";
                } else {
                    console.error("No marker name specified");
                }
                break;
            case 'background':
                if (fn == undefined || fn === 'id') {
                    substitution = activeBackground.id;
                } else if (fn === 'tag') {
                    var bg_tag = fixupTagFormatting(fn_parts[1]);
                    substitution = !!activeBackground.tags && (activeBackground.tags.indexOf(bg_tag) >= 0);
                } else if (fn === 'preposition') {
                    substitution = activeBackground.metadata[fn] || 'in';
                } else if (fn === 'term') {
                    substitution = activeBackground.metadata.term || activeBackground.id;
                } else if (fn == 'time' && !('time' in activeBackground.metadata) && args === undefined) {
                    substitution = localDayOrNight;
                } else if (fn == 'adverb') {
                    substitution = activeBackground.metadata.surface == 'roof' ? 'up'
                        : activeBackground.metadata.location == 'outdoors' ? 'out' : 'in';
                } else if (fn == 'if' && fn_parts.length == 2) {
                    let bg_tag = fixupTagFormatting(fn_parts[1]), val;
                    if ((bg_tag == 'day' || bg_tag == 'night') && !('time' in activeBackground.metadata)) {
                        val = localDayOrNight == bg_tag;
                    } else {
                        val = activeBackground.tags && (activeBackground.tags.includes(bg_tag));
                    }
                    substitution = expandDialogue(args.split('|')[val ? 0 : 1]);
                } else if (args === undefined) {
                    substitution = activeBackground.metadata[fn] || '';
                }
                break;
            case 'weekday':
                if (fn == 'number') {
                    substitution = new Date().getDay() || 7;
                } else if (fn === undefined) {
                    substitution = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][new Date().getDay()];
                }
                break;
            case 'day':
                if (fn == 'number') {
                    substitution = new Date().getDate();
                } else if (fn === undefined) {
                    var day = new Date().getDate();
                    substitution = day + ((day - 1) % 10 < 3 && (day < 4 || day > 13) ? ['st', 'nd', 'rd'][day % 10 - 1] : 'th');
                }
                break;
            case 'month':
                if (fn == 'number') {
                    substitution = new Date().getMonth() + 1;
                } else if (fn === undefined) {
                    substitution = ['January', 'February', 'March', 'April', 'May', 'June',
                                    'July', 'August', 'September', 'November', 'December'][new Date().getMonth()];
                }
                break;
            case 'year':
                substitution = new Date().getFullYear();
                break;
            case 'blank':
                return '';
            case 'rng':
                if (fn !== undefined) break;
                var range = new Interval(args);
                if (range.isValid() && range.min != null && range.max != null) {
                    return getRandomNumber(range.min, range.max+1);
                } else {
                    console.error("Invalid RNG range");
                }
                break;
            case 'event':
                fn = fn_parts[0];
                var found_event = false;
                for (let i = 0; i < activeGameEvents.length; i++) {
                    if (activeGameEvents[i].id === fn) {
                        substitution = "true";
                        found_event = true;
                        break;
                    }
                }
                if (!found_event) {
                    substitution = "false";
                }
                break;
            case 'target':
            case 'self':
            case 'winner':
            default:
                var variablePlayer = findVariablePlayer(variable.toLowerCase(), self, target, bindings);
                if (variablePlayer) {
                    substitution = expandPlayerVariable(fn_parts, args, variablePlayer, self, target, bindings);
                } else {
                    console.error("Unknown variable:", variable);
                }
                break;
            }
            if (variable == variable.toUpperCase()) {
                substitution = substitution.toUpperCase();
            } else if (variable[0] == variable[0].toUpperCase()) {
                substitution = substitution.initCap();
            }
        } catch (ex) {
            //throw ex;
            console.error("Invalid substitution caused exception:", ex);
        }
        return substitution;
    }
    // variable or
    // variable.attribute or
    // variable.function(arguments)
    return dialogue.replace(/~(\w+)(?:\.(\w.*?))?(?:\(([^)]*)\))?~/g, substitute);
}

function escapeRegExp(string) {
    return string.replace(/[\[\].*+?^${}()|\\]/g, '\\$&'); // $& means the whole matched string
}
var fixupDialogueSubstitutions = { // Order matters
    '...': '\u2026', // ellipsis
    '---': '\u2015', // em dash
    '--':  '\u2014', // en dash
    ' - ': ' \u2014 ', // hyphen used as en dash
    '``':  '\u201c', // left double quotation mark
    '`':   '\u2018', // left single quotation mark
    "''":  '\u201d', // right double quotation mark
    "'":   '\u2019', // right single quotation mark
    '\\':  '\xad', // soft hyphen
    '~':   '\u223c', // tilde operator, bigger than ordinary tilde
    '&lt;i&gt;': '<i>',
    '&lt;br&gt;': '<br>',
    '&lt;hr&gt;': '<hr>',
    '&lt;/i&gt;': '</i>',
    '&lt;/br&gt;': '</br>',
    '&lt;/hr&gt;': '</hr>'
};
var fixupDialogueRE = new RegExp(Object.keys(fixupDialogueSubstitutions).map(escapeRegExp).join('|'), 'gi');

function fixupDialogue (str) {
    if (str === undefined || str === null) return null;

    return str.split(/(<script>.*?<\/script>|<[^>]+>)/i).map(function(part, idx) {
        // Odd parts will be script tags with content, or other tags;
        // leave them alone and do substitutions on the rest
        return (idx % 2) ? part :
            part.replace(/"([^"]*)"/g, "\u201c$1\u201d")
            .replace(fixupDialogueRE, function(match) {
                return fixupDialogueSubstitutions[match.toLowerCase()]
            });
    }).join('');
}

var styleSpecifierRE = /({(?:[a-zA-Z0-9\-\_!]+\s*)+})/gi;
function parseStyleSpecifiers (str) {
    var rawFragments = str.split(styleSpecifierRE);
    var styledComponents = [];
    var curClasses = '';
    
    rawFragments.forEach(function (frag) {
        if (frag.length === 0) return;
        
        if (frag[0] === '{') {
            var classes = frag.slice(1, -1).trim();
            
            curClasses = (classes === '!reset') ? '' : classes;
        } else {
            styledComponents.push({'text': frag, 'classes': curClasses});
        }
    });
    
    return styledComponents;
}

/* Strip all formatting instructions (HTML tags and style specifiers)
 * and backslashes from a string, and lowercase the entire string.
 * 
 * Used for *sayingText conditions.
 */
function normalizeConditionText (str) {
    return str.toLowerCase().split(/(<script>.*?<\/script>|<[^>]+>)/i)
            .map(function (part, idx) {
                if (part.length === 0 || part[0] === '<') return '';

                return fixupDialogue(part.replace(/&lt;.+?&gt;|\\/gi, '').replace(styleSpecifierRE, ''));
            }).join('');
}

function normalizeImageName(img) {
    if (img.startsWith('custom:')) img = img.substring(7);
    return img.toLowerCase().replace(/\.(?:png|jpg|jpeg|gif)/gi, '').replace(/(?:\#|\d+)\-/gi, '');
}

function poseNameMatches(nameA, nameB) {
    if (!nameA || !nameB) return false;
    return normalizeImageName(nameA) === normalizeImageName(nameB);
}

/************************************************************
 * Given a string containing a number or two numbers 
 * separated by a dash, returns an array with the same number 
 * twice, or the first and second number as the case may be
 ************************************************************/
function Interval (str) {
    if (str === undefined) {
        this.min = this.max = null; return;
    }
    var m = str.match(/^\s*(-?\d+)?\s*-\s*(-?\d+)?\s*$/);
    if (m) {
        this.min = m[1] ? parseInt(m[1]) : null;
        this.max = m[2] ? parseInt(m[2]) : null;
    } else if (str.match(/^\s*(\d+)\s*$/)) {
        var val = parseInt(str);
        this.min = this.max = val;
    } else {
        this.min = this.max = NaN;
    }
}

Interval.prototype.contains = function (number) {
    return (this.min === null || this.min <= number)
        && (this.max === null || number <= this.max);
};

Interval.prototype.isValid = function() {
    return !isNaN(this.min) && !isNaN(this.max);
};

Interval.prototype.toString = Interval.prototype.toJSON = function(key) {
    if (isNaN(this.min)) return '#ERR';
    if (this.min === null && this.max === null) {
        return undefined;
    }
    if (this.min == this.max && this.min >= 0) {
        return this.min.toString();
    } else {
        return (this.min === null ? '' : this.min.toString()) + '-' +
            (this.max === null ? '' : this.max.toString());
    }
};

function parseInterval (str) {
    if (str) {
        return new Interval(str);
    }
    return undefined;
}

function inInterval (value, interval) {
    return !interval || interval.contains(value);
}

/************************************************************
 * Special function to check stage conditions, which can contain a
 * space-separated list of intervals.
 ************************************************************/
function checkStage(curStage, stageStr) {
    return stageStr === undefined
        || stageStr.split(/\s+/).some(function(s) {
        return inInterval(curStage, parseInterval(s));
    });
}

function evalOperator (val, op, cmpVal) {
    switch (op) {
    case '>': return val > cmpVal;
    case '>=': return val >= cmpVal;
    case '<': return val < cmpVal;
    case '<=': return val <= cmpVal;
    case '!=': return val != cmpVal;
    case '!!': return !!val;
    default:
    case '=':
    case '==':
        return val == cmpVal;
    }
}

/************************************************************
 * Check to see if a given marker predicate string is fulfilled
 * w.r.t. a given character.  If currentOnly = true, then the
 * predicate will be tested against the current state marker
 * only. This is used for volatile conditions.  If committedOnly =
 * true, then the predicate will be tested against the committed
 * markers only. If neither is true, the committed state combined with
 * the current state marker.
 ************************************************************/
function checkMarker(predicate, self, target, currentOnly) {
    var match = predicate.match(/^([\w\-]+)(\*?)(\s*((?:\>|\<|\=|\!)\=?)\s*(.+))?\s*$/);
    
    var name;
    var perTarget;
    var val;
    var cmpVal;
    var op;
    
    /* Get comparison values if we can, otherwise default to 'normal' behaviour. */
    if (!match) {
        name = predicate;
        perTarget = false;
        op = '!!';
    } else {
        name = match[1];
        perTarget = match[2];
        
        if (match[3]) {
            op = match[4];
            cmpVal = expandDialogue(match[5], self, target);
            if (!isNaN(parseInt(cmpVal, 10))) {
                cmpVal = parseInt(cmpVal, 10);
            }
        } else {
            op = '!!';
        }
    }
    
    if (currentOnly) {
        if (self.updatePending || !self.chosenState) {
            return false;
        }

        var evaluated = self.chosenState.evaluateMarker(name, self, target, perTarget);
        if (evaluated === undefined) {
            /* Could not find any marker matching criteria */
            return false;
        } 

        return evalOperator(evaluated, op, cmpVal);
    }

    val = self.getMarker(name, perTarget ? target : null) || 0;
    return evalOperator(val, op, cmpVal);
}


function checkSaidText(predicate, player) {
    var match = predicate.match(/^(.+)##\s*((?:\>|\<|\=|\!)\=?)\s*(\d+)\s*$/);

    var matchText = predicate;
    var cmpVal = 0;
    var op = "!!";

    if (match) {
        matchText = match[1];
        op = match[2];
        cmpVal = parseInt(match[3], 10);
    }

    matchText = normalizeConditionText(matchText);
    var matches = 0;

    /* Entries in saidDialogue should already be normalized. */
    Object.entries(player.saidDialogue).forEach(function (pair) {
        var candidate = normalizeConditionText(pair[0]);
        var candidateCount = pair[1];

        if (candidate.indexOf(matchText) >= 0) {
            matches += candidateCount;
        }
    });

    console.log("Checking said text (" + player.id + "): " + matchText + " (" + matches + ") " + op + " " + cmpVal);

    return evalOperator(matches, op, cmpVal);
}


function Condition($xml) {
    this.count  = parseInterval($xml.attr('count') || "1-");
    this.role   = $xml.attr('role');
    this.variable = normalizeBindingName($xml.attr('var'));
    this.id     = $xml.attr('character');
    this.tag    = $xml.attr('filter');
    this.stage  = parseInterval($xml.attr('stage'));
    this.layers = parseInterval($xml.attr('layers'));
    this.startingLayers = parseInterval($xml.attr('startingLayers'));
    this.gender         = $xml.attr('gender');
    this.status         = $xml.attr('status');
    this.timeInStage    = parseInterval($xml.attr('timeInStage'));
    this.hand           = $xml.attr('hasHand');
    this.consecutiveLosses = parseInterval($xml.attr('consecutiveLosses'));
    this.saidMarker     = $xml.attr('saidMarker');
    this.sayingMarker   = $xml.attr('sayingMarker');
    this.notSaidMarker  = $xml.attr('notSaidMarker');
    this.saying         = $xml.attr('saying');
    this.said           = $xml.attr('said');
    this.pose           = $xml.attr('pose');
    this.priority = 0;

    if (this.role == "self") {
        this.priority = (this.tag ? 0 : 0) + (this.status ? 20 : 0)
            + (this.consecutiveLosses ? 60 : 0) + (this.timeInStage ? 8 : 0)
            + (this.hand ? 20 : 0) + (this.gender ? 5 : 0)
    } else if (this.role == "target") {
        this.priority = (this.id ? 300 : 0) + (this.tag ? 150 : 0)
            + (this.stage ? 80 : 0) + (this.status ? 70 : 0)
            + (this.layers ? 40 : 0) + (this.startingLayers ? 40 : 0)
            + (this.consecutiveLosses ? 60 : 0) + (this.timeInStage ? 25 : 0)
            + (this.hand ? 30 : 0) + (this.gender ? 5 : 0)
    } else {
        this.priority = (this.role == "winner" ? 1.5 : 1) *
            ((this.id ? 100 : 0) + (this.tag ? 10 : 0)
             + (this.stage ? 40 : 0) + (this.status ? 5 : 0)
             + (this.layers ? 20 : 0) + (this.startingLayers ? 20 : 0)
             + (this.consecutiveLosses ? 30 : 0) + (this.timeInStage ? 15 : 0)
             + (this.hand ? 15 : 0) + (this.gender ? 5 : 0))
    }
    this.priority += (this.saidMarker ? 1 : 0) + (this.notSaidMarker ? 1 : 0)
        + (this.sayingMarker ? 1 : 0) + (this.saying ? 1 : 0)
        + (this.said ? 1 : 0) + (this.pose ? 1 : 0);

    if (this.id && !this.variable) {
        /* Apply correct normalization to player ID when using it as a variable. */
        this.variable = normalizeBindingName(this.id);
    }
}

/**********************************************************************
 *****                  Case Object Specification                 *****
 **********************************************************************/

function Case($xml, trigger) {
    this.trigger =                  trigger;
    this.stage =                    $xml.attr('stage');
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
    this.targetSaying =             $xml.attr("targetSaying");
    this.oppHand =                  $xml.attr("oppHand");
    this.hasHand =                  $xml.attr("hasHand");
    this.alsoPlaying =              $xml.attr("alsoPlaying");
    this.alsoPlayingStage =         parseInterval($xml.attr("alsoPlayingStage"));
    this.alsoPlayingHand =          $xml.attr("alsoPlayingHand");
    this.alsoPlayingTimeInStage =   parseInterval($xml.attr("alsoPlayingTimeInStage"));
    this.alsoPlayingSaidMarker =    $xml.attr("alsoPlayingSaidMarker");
    this.alsoPlayingNotSaidMarker = $xml.attr("alsoPlayingNotSaidMarker");
    this.alsoPlayingSayingMarker =  $xml.attr("alsoPlayingSayingMarker");
    this.alsoPlayingSaying =        $xml.attr("alsoPlayingSaying");
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
    this.customPriority =           parseInt($xml.attr("priority"), 10);
    this.hidden =                   $xml.attr("hidden");
    this.addTags =                  $xml.attr("addCharacterTags");
    this.removeTags =               $xml.attr("removeCharacterTags");
    this.oneShotId =                $xml.attr("oneShotId");
    
    if (this.addTags) {
        this.addTags = this.addTags.split(',').map(canonicalizeTag);
    } else {
        this.addTags = [];
    }

    if (this.removeTags) {
        this.removeTags = this.removeTags.split(',').map(canonicalizeTag);
    } else {
        this.removeTags = [];
    }
    
    this.states = [];
    $xml.children('state').each(function (idx, elem) {
        this.states.push(new State($(elem), this));
    }.bind(this));
    
    this.counters = [];
    $xml.children("condition").each(function (idx, elem) {
        this.counters.push(new Condition($(elem)));
    }.bind(this));
    
    var tests = [];
    $xml.children("test").each(function () {
        tests.push($(this));
    });
    this.tests = tests;

    if (isNaN(this.customPriority)) {
        this.customPriority = undefined;
    }

    var targetCondition = this.counters.find(function (ctr) {
        return (ctr.role == "target") && (isNaN(ctr.count.max) || (ctr.count.max === null) || (ctr.count.max > 0)) && ctr.id;
    });

    var targetID = targetCondition ? targetCondition.id : this.target;
    var hasTargetStage = !!this.targetStage || this.counters.some(function (ctr) {
        return (ctr.role == "target") && (isNaN(ctr.count.max) || (ctr.count.max === null) || (ctr.count.max > 0)) && ctr.stage;
    });

    if (targetID && hasTargetStage && (targetID != "human")) {
        if (this.trigger == MALE_MUST_STRIP || this.trigger == FEMALE_MUST_STRIP) {
            this.trigger = OPPONENT_LOST;
        } else if (CONVERT_STRIP_CASES.indexOf(this.trigger) >= 0) {
            this.trigger = OPPONENT_STRIPPING;
        } else if (CONVERT_STRIPPED_CASES.indexOf(this.trigger) >= 0) {
            this.trigger = OPPONENT_STRIPPED;
        }
    }
    
    // Calculate case priority ahead of time.
    if (this.hidden) {
        /* Only use custom priority for hidden cases, defaulting to 0 if no custom priority is set. */
        this.priority = this.customPriority || 0;
    } else if (this.customPriority !== undefined) {
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
        if (this.targetSaying)             this.priority += 1;
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
        if (this.alsoPlayingSaying)        this.priority += 1;

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

        this.counters.forEach(function (c) { this.priority += c.priority; }, this);

        if ((this.trigger == MALE_HUMAN_MUST_STRIP
             || this.trigger == FEMALE_HUMAN_MUST_STRIP)
            && this.target != "human"
            && !this.counters.some(c => c.id == "human" && c.role == "target")) {
            this.priority += 300;
        }

        // Expression tests (priority = 50 for each)
        this.priority += (tests.length * 50);
    }

    this.isVolatile = this.targetSayingMarker || this.targetSaying
        || this.alsoPlayingSayingMarker || this.alsoPlayingSaying
        || this.counters.some(function(c) {
            return c.sayingMarker || c.saying || c.pose;
        });
}

/**
 * Get all stages that this Case can potentially apply to.
 * If a `stage` condition is provided, we just rely on those. Otherwise,
 * returns all stage numbers within the interval returned by
 * `getRelevantStagesForTrigger`.
 * 
 * @returns {Array} An array of unique stage numbers that this case may apply to.
 */
Case.prototype.getStages = function (n_layers) {    
    if (this.stage) {
        var intervals = this.stage.split(/\s+/).map(parseInterval);
    } else {
        /* Currently never used in practice, because the CE always
         * adds a stage attributes instead of relying on the
         * default. */
        var intervals = [getRelevantStagesForTrigger(this.trigger, n_layers)];
    }
    
    return intervals.reduce(function (acc, interval) {
        for (var i = interval.min; i <= interval.max; i++) {
            if (acc.indexOf(i) < 0) acc.push(i);
        }

        return acc;
    }, []);
}

/**
 * Get all possible images that can be used by the States in this case.
 * 
 * @param stage {number} A stage number to pass to
 * `State.prototype.getPossibleImages`.
 * @returns {Array} An array of image names.
 */
Case.prototype.getPossibleImages = function (stage) {
    var case_images = [];

    this.states.forEach(function (state) {
        Array.prototype.push.apply(case_images, state.getPossibleImages(stage));
    });

    return case_images;
}

/* Convert this case's conditions into a plain object, into a format suitable
 * for e.g. JSON serialization.
 */
Case.prototype.toJSON = function () {
    var ser = {};
    
    Object.keys(this).forEach(function (prop) {
        var val = this[prop];
        if (prop == 'priority') return;
        if (prop == 'customPriority') prop = 'priority';
        if (val === undefined || (typeof val === 'object' && !(val instanceof Interval))) return;
        ser[prop] = val;
    }, this);
    
    ser.tests = this.tests.map(function (test) {
        return {
            'expr': test.attr('expr'),
            'value': test.attr('value'),
            'cmp': test.attr('cmp'),
        };
    });
    
    ser.counters = this.counters;
    
    return ser;
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

Case.prototype.checkConditions = function (self, opp) {
    var volatileDependencies = new Set();
    
    // one-time use
    if (this.oneShotId && self.oneShotCases[this.oneShotId]) {
        return false;
    }

    // all states used up or excluded by other player's negative conditions
    if (this.states.every(function (state) {
        return (state.oneShotId && self.oneShotStates[state.oneShotId])
            || !state.checkUnwanteds(self, opp);
    })) {
        return false;
    }

    // stage
    if (this.stage !== undefined) {
        if (!checkStage(self.stage, this.stage)) {
            return false; // failed "stage" requirement
        }
    }
    
    // target
    if (this.target) {
        if (!opp || this.target !== opp.id) {
            return false; // failed "target" requirement
        }
    }
    
    // filter
    if (this.filter) {
        if (!opp || !opp.hasTag(this.filter)) {
            return false; // failed "filter" requirement
        }
    }

    // targetStage
    if (this.targetStage) {
        if (!opp || !inInterval(opp.stage, this.targetStage)) {
            return false; // failed "targetStage" requirement
        }
    }
    
    // targetLayers
    if (this.targetLayers) {
        if (!opp || !inInterval(opp.countLayers(), this.targetLayers)) {
            return false; 
        }
    }
    
    // targetStatus
    if (this.targetStatus) {
        if (!opp || !opp.checkStatus(this.targetStatus)) {
            return false;
        }
    }

    // targetStartingLayers
    if (this.targetStartingLayers) {
        if (!opp || !inInterval(opp.startingLayers, this.targetStartingLayers)) {
            return false;
        }
    }

    // targetSaidMarker
    if (this.targetSaidMarker) {
        if (!opp || !checkMarker(this.targetSaidMarker, opp, null)) {
            return false;
        }
    }
    
    // targetNotSaidMarker
    if (this.targetNotSaidMarker) {
        if (!opp || checkMarker(this.targetNotSaidMarker, opp, null)) {
            return false;
        }
    }

    if (this.targetSayingMarker) {
        if (!opp || !checkMarker(this.targetSayingMarker, opp, null, true)) {
            return false;
        }
        volatileDependencies.add(opp);
    }
    if (this.targetSaying) {
        if (!opp || !opp.chosenState || opp.updatePending) return false;
        if (normalizeConditionText(opp.chosenState.rawDialogue).indexOf(normalizeConditionText(this.targetSaying)) < 0) return false;
        volatileDependencies.add(opp);
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
    if (this.oppHand) {
        if (!opp || !opp.hand || opp.hand.strength !== handStrengthFromString(this.oppHand)) {
            return false;
        }
    }

    // targetTimeInStage
    if (this.targetTimeInStage) {
        if (!opp || !inInterval(opp.timeInStage == -1 ? 0 //allow post-strip time to count as 0
                                : opp.timeInStage, this.targetTimeInStage)) {
            return false; // failed "targetTimeInStage" requirement
        }
    }

    // hasHand
    if (this.hasHand) {
        if (!self.hand || self.hand.strength !== handStrengthFromString(this.hasHand)) {
            return false;
        }
    }

    // alsoPlaying, alsoPlayingStage, alsoPlayingTimeInStage, alsoPlayingHand (priority = 100, 40, 15, 5)
    if (this.alsoPlaying) {
        var ap = this.getAlsoPlaying(opp);
        
        if (!ap) {
            return false; // failed "alsoPlaying" requirement
        } else {
            if (this.alsoPlayingStage) {
                if (!inInterval(ap.stage, this.alsoPlayingStage)) {
                    return false;        // failed "alsoPlayingStage" requirement
                }
            }
                    
            if (this.alsoPlayingTimeInStage) {
                if (!inInterval(ap.timeInStage, this.alsoPlayingTimeInStage)) {
                    return false;        // failed "alsoPlayingTimeInStage" requirement
                }
            }
                    
            if (this.alsoPlayingHand) {
                if (!ap.hand || ap.hand.strength !== handStrengthFromString(this.alsoPlayingHand))
                {
                    return false;        // failed "alsoPlayingHand" requirement
                }
            }
                    
            // marker checks have very low priority as they're mainly intended to be used with other target types
            if (this.alsoPlayingSaidMarker) {
                if (!checkMarker(this.alsoPlayingSaidMarker, ap, opp)) {
                    return false;
                }
            }
                    
            if (this.alsoPlayingNotSaidMarker) {
                // Negated marker condition - false if it matches
                if (checkMarker(this.alsoPlayingNotSaidMarker, ap, opp)) {
                    return false;
                }
            }

            if (this.alsoPlayingSayingMarker) {
                if (!checkMarker(this.alsoPlayingSayingMarker, ap, opp, true)) {
                    return false;
                }
                volatileDependencies.add(ap);
            }
            if (this.alsoPlayingSaying) {
                if (ap.updatePending || !ap.chosenState || normalizeConditionText(ap.chosenState.rawDialogue).indexOf(normalizeConditionText(this.alsoPlayingSaying)) < 0) {
                    return false;
                }
                volatileDependencies.add(ap);
            }
        }
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
        if (!checkMarker(this.saidMarker, self, opp)) {
            return false;
        }
    }
    
    if (this.notSaidMarker) {
        if (checkMarker(this.notSaidMarker, self, opp)) {
            return false;
        }
    }

    var counterMatches = {};
    var unwantedSayings = [], unwantedMarkers = [], unwantedPoses = [];
    // filter counter targets
    if (!this.counters.every(function (ctr) {
        var matches = players.filter(function(p) {
            return p && (ctr.role === undefined
                    || (ctr.role == "self" && p == self)
                    || (ctr.role == "target" && p == opp)
                    || (ctr.role == "winner" && p.slot == recentWinner)
                    || (ctr.role == "opp" && p != self)
                    || (ctr.role == "aiopp" && p != humanPlayer && p != self)
                    || (ctr.role == "other" && p != self && p != opp))
                && (ctr.id === undefined || p.id == ctr.id)
                && (ctr.stage === undefined || inInterval(p.stage, ctr.stage))
                && (ctr.tag === undefined || p.hasTag(ctr.tag))
                && (ctr.gender === undefined || p.gender == ctr.gender)
                && (ctr.status === undefined || p.checkStatus(ctr.status))
                && (ctr.layers === undefined || inInterval(p.clothing.length, ctr.layers))
                && (ctr.startingLayers === undefined || inInterval(p.startingLayers, ctr.startingLayers))
                && (ctr.timeInStage === undefined || inInterval(p.timeInStage, ctr.timeInStage))
                && (ctr.hand === undefined || (p.hand && p.hand.strength === handStrengthFromString(ctr.hand)))
                && (ctr.consecutiveLosses === undefined || inInterval(p.consecutiveLosses, ctr.consecutiveLosses))
                && (ctr.saidMarker === undefined || checkMarker(ctr.saidMarker, p, opp))
                && (ctr.notSaidMarker === undefined || !checkMarker(ctr.notSaidMarker, p, opp))
                && (ctr.said === undefined || checkSaidText(ctr.said, p));
        });
        var hasUpperBound = (ctr.count.max !== null && ctr.count.max < matches.length);
        if (ctr.sayingMarker !== undefined || ctr.saying !== undefined || ctr.pose !== undefined) matches = matches.filter(function(p) {
            if (ctr.sayingMarker !== undefined) {
                // The human player can't talk, and using
                // saying/sayingMarker/pose on self would be circular.
                if (p == self || p == humanPlayer) return false;
                if (checkMarker(ctr.sayingMarker, p, opp, true)) {
                    volatileDependencies.add(p);
                } else {
                    /* In case the condition could be violated by some
                     * of the players fulfilling the non-volatile
                     * conditions changing state, record those players
                     * and the violating marker. */
                    if (hasUpperBound) {
                        unwantedMarkers.push([p, ctr.sayingMarker]);
                    }
                    return false;
                }
            }
            if (ctr.saying !== undefined) {
                if (p == self || p == humanPlayer) return false;
                if (!p.updatePending && p.chosenState && normalizeConditionText(p.chosenState.rawDialogue).indexOf(normalizeConditionText(ctr.saying)) >= 0) {
                    volatileDependencies.add(p);
                } else {
                    if (hasUpperBound) {
                        unwantedSayings.push([p, ctr.saying]);
                    }
                    return false;
                }
            }
            if (ctr.pose !== undefined) {
                if (p == self || p == humanPlayer) return false;
                if (!p.updatePending && p.chosenState && poseNameMatches(ctr.pose, p.chosenState.image)) {
                    volatileDependencies.add(p);
                } else {
                    if (hasUpperBound) {
                        unwantedPoses.push([p, ctr.pose]);
                    }
                    return false;
                }
            }
            return true;
        });
        /* Don't limit what other characters can say before the've had
         * a first chance to pick something to say. */
        if ((unwantedSayings.length || unwantedMarkers.length || unwantedPoses.length) && players.some(function(p) {
            return p.updatePending && (unwantedSayings.some(function(item) { return item[0] == p; })
                                       || unwantedMarkers.some(function(item) { return item[0] == p; })
                                       || unwantedPoses.some(function (item) { return item[0] == p; }));
        })) {
            return false;
        }
        if (inInterval(matches.length, ctr.count)) {
            if (matches.length && ctr.variable) {
                if (counterMatches.hasOwnProperty(ctr.variable)) {
                    // If two <condition> elements define the same variable, take the intersection of the matches.
                    // If any intersection is empty, getAllBindingCombinations() will return an empty array and the
                    // case will not match.
                    counterMatches[ctr.variable] = counterMatches[ctr.variable].filter(function(m) { return matches.indexOf(m) >= 0; });
                } else {
                    counterMatches[ctr.variable] = matches;
                }
            }
            return true;
        }
        return false;
    })) {
        return false; // failed filter count
    }
    var bindingCombinations = getAllBindingCombinations(Object.entries(counterMatches));
    shuffleArray(bindingCombinations);
    /* In the trivial case with no condition variables, we get a single binding combination of {}.
       And with no tests, this.tests.every() trivially returns true. */
    for (var i = 0; i < bindingCombinations.length; i++) {
        addExtraNumberedBindings(bindingCombinations[i], Object.entries(counterMatches));
        if (this.tests.every(function(test) {
            var expr = expandDialogue(test.attr('expr'), self, opp, bindingCombinations[i]);
            var value = test.attr('value') || "";
            if (value) {
                value = expandDialogue(value, self, opp, bindingCombinations[i]);
            }
            
            var cmp = test.attr('cmp');

            /* For backwards compatibility, if cmp is unspecified, try
             * parsing value as an interval, and if it's not, fall
             * back to equality. If cmp is @ or !@, fail if value is
             * not an interval. */
            if (!cmp || cmp == '@' || cmp == '!@') {
                var interval = parseInterval(value);
                if ((interval != undefined && interval.isValid()) || cmp) {
                    return cmp === '!@' ? !inInterval(Number(expr), interval) : inInterval(Number(expr), interval);
                }
            }

            if (!isNaN(Number(expr))) expr = Number(expr);
            if (!isNaN(Number(value))) value = Number(value);

            switch (cmp) {
            case '>':
                return expr > value;
            case '>=':
                return expr >= value;
            case '<':
                return expr < value;
            case '<=':
                return expr <= value;
            case '!=':
                return expr != value;
            default:
                return expr == value;
            }

            return true;
        })) {
            this.variableBindings = bindingCombinations[i];
            this.volatileDependencies = volatileDependencies;
            this.unwantedSayings = unwantedSayings;
            this.unwantedMarkers = unwantedMarkers;
            this.unwantedPoses = unwantedPoses;
            return true;
        }
    }

    return false;
}

Case.prototype.cleanupMutableState = function () {
    delete this.variableBindings;
    delete this.volatileDependencies;
    delete this.unwantedMarkers;
    delete this.unwantedSayings;
    delete this.unwantedPoses;
}

Case.prototype.applyOneShot = function (player) {
    if (this.oneShotId) {
        player.oneShotCases[this.oneShotId] = true;
    }
}

/*
 * Helper function to add more triggers to a set of triggers, keeping in
 * mind that triggers can be either a single trigger, an array of
 * triggers tried in parallel, or an array of arrays of triggers tried
 * in sequence.  In the last case, adds the new trigger(s) to the first set,
 * because there's no point searching them more than once.
 * newTriggers can be a single trigger or an array, because concat() handles both.
 * Does not modify the original set of triggers.
 */
function addTriggers(triggers, newTriggers) {
    if (Array.isArray(triggers) && Array.isArray(triggers[0])) {
        return [triggers[0].concat(newTriggers), triggers.slice(1)];
    } else {
        return (Array.isArray(triggers) ? triggers : [triggers]).concat(newTriggers);
    }
}

/**********************************************************************
 *****                 Behaviour Parsing Functions                *****
 **********************************************************************/

Opponent.prototype.findBehaviour = function(triggers, opp, volatileOnly) {
    /* get the AI stage */
    var stageNum = this.stage;
    var bestMatchPriority = 0;
    if (volatileOnly && this.chosenState && this.chosenState.parentCase) {
        bestMatchPriority = this.chosenState.parentCase.priority + 1;
    }

    var cases = [];
    triggers.forEach(function (trigger) {
        var relCases = this.cases.get(trigger+':'+stageNum) || [];
        relCases.forEach(function (c) {
            if (!c.hidden && (cases.indexOf(c) < 0)) cases.push(c);
        });
    }, this);

    /* quick check to see if the trigger exists */
    if (cases.length <= 0) {
        console.log("Warning: couldn't find " + triggers + " dialogue for player " + this.slot + " at stage " + stageNum);
        return false;
    }

    /* Evaluate pre-dialogue hidden cases if we're not doing a reaction pass. */
    if (!volatileOnly) this.evaluateHiddenCases(triggers, opp, false);

    /* Find the best match, as well as potential volatile matches. */
    var bestMatch = [];
    
    for (var i = 0; i < cases.length; i++) {
        var curCase = cases[i];

        if ((curCase.priority >= bestMatchPriority) &&
            (!volatileOnly || curCase.isVolatile) &&
            curCase.checkConditions(this, opp))
        {
            if (curCase.priority > bestMatchPriority) {
                /* Cleanup all mutable state on previous best-match cases. */
                bestMatch.forEach(function (c) { c.cleanupMutableState(); });

                bestMatch = [curCase];
                bestMatchPriority = curCase.priority;
            } else {
                bestMatch.push(curCase);
            }
        }
    }
    var states = bestMatch.reduce(function(list, caseObject) {
        return list.concat(caseObject.states);
    }.bind(this), []).filter(function(state) {
        return (!state.oneShotId || !this.oneShotStates[state.oneShotId])
            && state.checkUnwanteds(this, opp);
    }.bind(this));
    
    var weightSum = states.reduce(function(sum, state) { return sum + state.weight; }, 0);
    if (weightSum > 0) {
        console.log("Current case priority for player "+this.slot+": "+bestMatchPriority);

        var rnd = Math.random() * weightSum;
        for (var i = 0, x = 0; x <= rnd && i < states.length; x += states[i++].weight);
        
        /* Clean up mutable state on cases not selected. */
        var chosenState = states[i-1];
        bestMatch.forEach(function (c) {
            if (c !== chosenState.parentCase) c.cleanupMutableState();
        });

        return new State(chosenState);
    }

    console.log("-------------------------------------");
    return null;
}

Opponent.prototype.evaluateHiddenCases = function (triggers, opp, postDialogue) {
    var cases = [];

    triggers.forEach(function (trigger) {
        var relCases = this.cases.get(trigger+':'+this.stage) || [];
        relCases.forEach(function (c) {
            if (
                c.hidden &&
                (postDialogue || !c.isVolatile) &&
                ((c.priority < 0) == postDialogue) &&
                (cases.indexOf(c) < 0)
            ) {
                cases.push(c);
            }
        });
    }, this);

    if (cases.length <= 0) return;

    /* Sort hidden cases in order of descending custom priority. */
    cases.sort(function (a, b) {
        return b.priority - a.priority;
    });

    /* Group hidden cases together that have the same custom priority value. */
    var curHiddenPriority = 0;
    var curHiddenGroup = [];
    var hiddenGroups = [];
    cases.forEach(function (c) {
        if (curHiddenPriority != c.priority && curHiddenGroup.length > 0) {
            hiddenGroups.push(curHiddenGroup);
            curHiddenGroup = [];
        }
        curHiddenGroup.push(c);
        curHiddenPriority = c.priority;
    });
    if (curHiddenGroup.length > 0) hiddenGroups.push(curHiddenGroup);

    /* Check conditions of all cases in each group in parallel, by filtering before applying effects. */
    hiddenGroups.forEach(function (group) {
        console.log("[" + this.id + "] Evaluating " + group.length + (postDialogue ? " post" : " pre") + "-dialogue hidden cases at priority " + group[0].priority);
        group.filter(function (curCase) {
            return curCase.checkConditions(this, opp);
        }, this).forEach(function (matchedCase) {
            this.applyHiddenStates(matchedCase, opp);
            matchedCase.cleanupMutableState();
        }, this);
    }, this);
}

/**
 * Updates this Opponent's chosenState and related information.
 * Also cleans up the previous chosenState's parent Case, if it exists.
 */
Opponent.prototype.updateChosenState = function (state) {
    if (this.chosenState && this.chosenState.parentCase && state.parentCase != this.chosenState.parentCase) {
        this.chosenState.parentCase.cleanupMutableState();
    }

    this.chosenState = state;
    this.stateCommitted = false;
    this.chosenState.selectImage(this.stage);
}

/**
 * Clears out this Opponent's previous chosenState, leaving it at null.
 */
Opponent.prototype.clearChosenState = function () {
    if (this.chosenState && this.chosenState.parentCase) {
        this.chosenState.parentCase.cleanupMutableState();
    }

    this.chosenState = null;
    this.stateCommitted = false;
}

/************************************************************
 * Updates the behaviour of the given player based on the 
 * provided triggers.
 ************************************************************/
Opponent.prototype.updateBehaviour = function(triggers, opp) {
    /* determine if the AI is dialogue locked */
    if (this.out && this.forfeit[1] == CANNOT_SPEAK && triggers !== DEALING_CARDS) {
        /* their is restricted to this only */
        triggers = [this.forfeit[0]];
    }

    if (Array.isArray(triggers) && Array.isArray(triggers[0])) {
        /* Return which trigger set actually matched within the passed-in array. */
        for (let i=0; i < triggers.length; i++) {
            let ret = this.updateBehaviour(triggers[i], opp);
            if (ret !== null) return ret;
        }
        return null;
    }
    if (!Array.isArray(triggers)) {
        triggers = [triggers];
    }
    
    /* Global lines play in any phase except DEALING_CARDS */
    if (triggers[0] !== DEALING_CARDS) {
        triggers.push(GLOBAL_CASE);
    }
    
    this.currentTarget = opp;
    this.currentTriggers = triggers;

    var state = this.findBehaviour(triggers, opp, false);

    if (state) {
        this.updateChosenState(state);
        this.lastUpdateTriggers = triggers;
        return triggers;
    }
    return null;
}

/**
 * Perform a full dialogue update for a single player.
 * 
 * This handles pre-dialogue hidden case evaluation, regular dialogue evaluation,
 * committing chosen states, and post-dialogue hidden case evaluation in a single call.
 * 
 * @param {Array<string|Array<string>>|string} triggers 
 * @param {Player} opp 
 */
Opponent.prototype.singleBehaviourUpdate = function (triggers, opp) {
    /* Note the trigger set that actually matched in updateBehaviour,
     * so that we can later evaluate post-dialogue hidden cases using those triggers.
     */
    var evaluatedTrigger = this.updateBehaviour(triggers, opp);
    this.commitBehaviourUpdate();
    
    if (evaluatedTrigger) {
        saveSingleTranscriptEntry(this.slot);
        this.evaluateHiddenCases(evaluatedTrigger, opp, true);
    }
}

/************************************************************
 * Attempt to find a higher-priority volatile match case if
 * one exists.
 * If a higher-priority volatile case is found, its volatile
 * dependencies will be locked, unlocking prior volatile state locks if necessary.
 ************************************************************/
Opponent.prototype.updateVolatileBehaviour = function () {
    if (players.some(function(p) {
        if (p !== players[HUMAN_PLAYER]
            && !p.updatePending && p.chosenState && p.chosenState.parentCase) {
            var dependencies = p.chosenState.parentCase.volatileDependencies;
            return dependencies && dependencies.has(this);
        } else return false;
    }, this)) {
        console.log("Player "+this.slot+" state is locked.");
        return;
    }

    if (this.chosenState && this.chosenState.parentCase) {
        console.log("Player "+this.slot+": Current priority "+this.chosenState.parentCase.priority);
    }
    
    var newState = this.findBehaviour(this.currentTriggers, this.currentTarget, true);

    if (newState) {
        /* Assign new best-match case and state. */
        console.log("Found new volatile state for player "+this.slot+" with priority "+newState.parentCase.priority);
        this.updateChosenState(newState);

        return true;
    } else {
        console.log("Found no volatile matches for player "+this.slot);
        return false;
    }
}

/************************************************************
 * Finalizes a behaviour update,
 * expanding state dialogue and updating player markers. 
 ************************************************************/
Opponent.prototype.commitBehaviourUpdate = function () {
    if (!this.chosenState) return;
    if (this.stateCommitted) return;

    /* Use rawDialogue so that variables don't affect repeat count.  */
    this.repeatLog[this.chosenState.rawDialogue] = this.getRepeatCount() + 1;

    this.chosenState.expandDialogue(this, this.currentTarget);

    this.applyState(this.chosenState, this.currentTarget);
    
    this.stateCommitted = true;
    updateGameVisual(this.slot);
}

/************************************************************
 * Applies markers and other operations from a state
 ************************************************************/
Opponent.prototype.applyState = function(state, opp) {
    state.applyMarkers(this, opp);
    state.applyCollectible(this);
    state.applyOneShot(this);
    state.applyOperations(this, opp);
    var parentCase = state.parentCase;
    if (parentCase) {
        if (parentCase.removeTags.length > 0 || parentCase.addTags.length > 0) {
            parentCase.removeTags.forEach(this.removeTag.bind(this));
            parentCase.addTags.forEach(this.addTag.bind(this));
            this.updateTags();
        }
        parentCase.applyOneShot(this);

        if (state.rawDialogue && !parentCase.hidden) {
            var condText = normalizeConditionText(state.rawDialogue);
            this.saidDialogue[condText] = (this.saidDialogue[condText] || 0) + 1;
        }
    }
}

/************************************************************
 * Applies markers and operations from all lines in a case
 ************************************************************/
Opponent.prototype.applyHiddenStates = function (chosenCase, opp) {
    chosenCase.states.forEach(function (c) {
        this.applyState(c, opp);
        /* Yes, this may apply the case-level oneShot multiple times,
         * but that's no real problem. */
    }, this);
}

/************************************************************
 * Updates the behaviour of all players except the given player
 * based on the provided tag.
 ************************************************************/
function updateAllBehaviours (target, target_tags, other_tags) {
    for (var i = 2; i < players.length; i++) {
        if (!players[i]) continue;
        /* Indicate that current state will be overwritten and can't
         * be used with *SayingMarker and *Saying checks. */
        players[i].updatePending = true;
    }

    /* We need to keep track of which triggers (if any) actually matched for each player,
     * so that we can evaluate post-dialogue hidden cases for those triggers afterwards.
     */
    var postProcessingTriggers = new Array(5);
    for (var i = 1; i < players.length; i++) {
        if (!players[i] || !players[i].isLoaded()) continue;
        if (target === null || i != target) {
            postProcessingTriggers[i] = players[i].updateBehaviour(other_tags, players[target]);
        } else if (i == target && target_tags !== null) {
            postProcessingTriggers[i] = players[i].updateBehaviour(target_tags, null);
        }
        players[i].updatePending = false;
    }
    
    updateAllVolatileBehaviours();
    commitAllBehaviourUpdates(target_tags !== null ? players[target] : null);

    for (var i = 1; i < players.length; i++) {
        if (!players[i] || !players[i].isLoaded() || !postProcessingTriggers[i]) continue;
        if (target === null || i != target) {
            players[i].evaluateHiddenCases(postProcessingTriggers[i], players[target], true);
        } else if (i == target && target_tags !== null) {
            players[i].evaluateHiddenCases(postProcessingTriggers[i], null, true);
        }
    }
}

/************************************************************
 * Handles volatile cases for dialogue processing.
 * 'Promotes' players who have available volatile cases to using those cases.
 ************************************************************/
function updateAllVolatileBehaviours () {
    for (var pass = 0; pass < 3; pass++) {
        console.log("Reaction pass "+(pass+1));
        var anyUpdated = false;
        
        players.forEach(function (p) {
            if (p !== humanPlayer && p.isLoaded()) {
                anyUpdated = p.updateVolatileBehaviour() || anyUpdated;
            }
        });
        
        console.log("-------------------------------------");
        
        // If nothing's changed, assume we've reached a stable state.
        if (!anyUpdated) break;
    }
}

/************************************************************
 * Commits all player behaviour updates.
 ************************************************************/
function commitAllBehaviourUpdates (target) {
    /* Apply label-setting operations first so that ~name~ is the same for all players.
     * NOTE: maybe apply nickname operations here too?
     */
    players.opponents.forEach(function (p) {
        if (p.chosenState) {
            p.chosenState.operations.forEach(function (op) {
                if ((op instanceof PlayerAttributeOperation) && op.attr === "label") {
                    op.apply(p, (p !== target) ? target : null);
                }
            });
        }
    });

    /* Record updated states only. */
    var updatedPlayers = [];
    players.opponents.forEach(function (p) {
        if (p.chosenState && !p.stateCommitted) {
            p.commitBehaviourUpdate();
            updatedPlayers.push(p.slot);
        }
    });

    saveTranscriptEntries(updatedPlayers);
}

/*
 * Produces all combinations of variable bindings
 * Input: an array of pairs of variable name and matching player references
 * Return value: an array of objects with each variable name bound to a player reference.
 */
function getAllBindingCombinations (variableMatches) {
    if (variableMatches.length > 0) {
        var ret = [];
        var cur = variableMatches[0];
        var variable = cur[0];
        var matches = cur[1];
        var rest = getAllBindingCombinations(variableMatches.slice(1));

        for (var i = 0; i < matches.length; i++) {
            for (var j = 0; j < rest.length; j++) {
                var bindings = {};
                for (var key in rest[j]) { // copy properties
                    bindings[key] = rest[j][key];
                }
                bindings[variable] = matches[i];
                ret.push(bindings);
            }
        }
        return ret;
    } else return [{}];
}

/*
 * Adds additional numbered variables from 2 and up binding to the
 * remaining variable matches not already used in bindings.
 */
function addExtraNumberedBindings (bindings, variableMatches) {
    variableMatches.forEach(function(pair) {
        var variable = pair[0], matches = pair[1];
        var otherMatches = matches.filter(function(match) { return match != bindings[variable]; });
        shuffleArray(otherMatches);
        otherMatches.forEach(function(match, i) {
            bindings[variable + (i + 2)] = match;
        });
    });
}

function shuffleArray (array) {
    for (var i = array.length - 1; i > 0; i--) {
        var j = getRandomNumber(0, i);
        var tmp = array[i];
        array[i] = array[j];
        array[j] = tmp;
    }
}

/*
 * Given an array arr, return an array containing all arr.length!
 * permutations of arr. Not used at the moment, but may be later.
 * (Since there are at most five players, the number of permutations
 * will be at most 5! = 120).
 */
function getAllArrayPermutations (arr) {
    if (arr.length == 1) return [arr];
    var ret = [];
    for (var i = 0; i < arr.length; i++) {
        var copy = arr.slice();
        var el = copy.splice(i, 1)[0];
        getAllArrayPermutations(copy).forEach(function(permutation) {
            permutation.push(el);
            ret.push(permutation);
        });
    }
    return ret;
}
