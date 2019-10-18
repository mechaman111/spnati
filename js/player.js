/********************************************************************************
 This file contains all of the variables and functions for the Player object, as
 well as definitions for opponent and group lisitings.
 ********************************************************************************/

/**********************************************************************
 * Enumerations
 **********************************************************************/

/**
 * Possible player penis / breast sizes.
 * 
 * @global
 * @readonly
 * @enum {string}
 */
var eSize = {
    SMALL: "small",
    MEDIUM: "medium",
    LARGE: "large"
};

/**
 * Possible AI player intelligence levels (difficulty settings).
 * 
 * @global
 * @readonly
 * @enum {string}
 */
var eIntelligence = {
    /** 'Throw' intelligence: this player actively tries to lose. */
    THROW: "throw",

    /** 'Bad' intelligence: this player swaps cards at random. */
    BAD: "bad",

    /**
     * 'Average' intelligence: this player makes some attempt at winning, but
     * may make slightly suboptimal moves.
     */
    AVERAGE: "average",

    /** 'Good' intelligence: only attempts to get or improve on pairs. */
    GOOD: "good",

    /**
     * 'Best' intelligence: same as 'Good', but also keeps the highest-value
     * card when receiving bad hands.
     */
    BEST: "best"
};