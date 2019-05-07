/********************************************************************************
 This file contains all of the variables and functions for the Player object, as
 well as definitions for opponent and group lisitings.
 ********************************************************************************/

/**********************************************************************
 * Enumerations
 **********************************************************************/

/************************************************************
 * An enumeration for gender.
 **/
var eGender = {
    MALE   : "male",
    FEMALE : "female"
};

/************************************************************
 * An enumeration for player size.
 **/
var eSize = {
    SMALL  : "small",
    MEDIUM : "medium",
    LARGE  : "large"
};

/************************************************************
 * An enumeration for player intelligence.
 **/
var eIntelligence = {
    THROW  : "throw",
    BAD  : "bad",
    AVERAGE : "average",
    GOOD  : "good",
    BEST  : "best"
};


/********************************************************************************
 * Player Object and Elements
 ********************************************************************************/

/**********************************************************************
 * (Object) A player in a strip poker game.
 **/
function Player(id, slot)
{
    this.id = null;
    this.slot = slot;
    this.name = "";
    this.gender = eGender.FEMALE;
    this.size = eSize.MEDIUM;
    this.intelligence = eIntelligence.AVERAGE
    //this.state = new PlayerState();
    //this.wardrobe = new Wardrobe();
    //this.hand = new PokerHand();
    this.out = false;
    //this.forfeit = new Forfeit();
    //this.ticket = new Ticket();
    this.xml = null;
    this.listing = null;
}
