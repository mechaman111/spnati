/**
 * Contains code relating to the points unlock system.
 */

/**
 * Encapsulates an intention to add (or subtract) points from the player's
 * unlock points total at the end of a game.
 * 
 * These intents will be listed and applied at the end of a game.
 * 
 * @constructor
 * @param {string} reason The reason for modifying the player's point count.
 * This will be listed at the end of a game.
 * @param {number} points The number of points to add or subtract from the
 * player's total.
 */
function PointIntent (reason, points) {
    this.reason = reason;
    this.points = points;
}

/**
 * @constructor
 * 
 * Contains information about player points and logic for working with the
 * points system.
 */
function PointsController () {
    /** 
     * A list of `PointIntent`s that will be applied at the end of the
     * current game.
     * 
     * @type PointIntent[]
     */
    this.additive_intents = [];
}

/**
 * Get how many unlock points the player currently has available
 * (not including any points that will be added at the end of the current game.)
 * 
 * @returns {number}
 */
PointsController.prototype.getCurrentPoints = function () {
    return save.getUnlockPoints();
}

/**
 * Directly set the number of unlock points the player has available.
 * @param {number} ct The value to set the player's unlock points count to.
 */
PointsController.prototype.setCurrentPoints = function (ct) {
    save.setUnlockPoints(ct);
}

/**
 * Get how many unlock points the player will have available at the end
 * of the game.
 * 
 * @returns {number}
 */
PointsController.prototype.getGameEndPoints = function () {
    return this.additive_intents.reduce(function (acc, val) {
        var new_acc = acc + val.points;
        return (new_acc >= 0) ? new_acc : 0;
    }, save.getUnlockPoints());
}

/**
 * Push a new `PointIntent` to the current list of intents to be applied.
 * 
 * @param {string} reason The reason for modifying the player's point count.
 * This will be listed at the end of a game.
 * @param {number} points The number of points to add or subtract from the
 * player's total.
 */
PointsController.prototype.addPoints = function (reason, points) {
    this.additive_intents.push(new PointIntent(reason, points));
}

/**
 * Applies all stored `PointIntent`s to the player's current point count,
 * and clears the list of stored Intents.
 */
PointsController.prototype.applyIntents = function () {
    this.setCurrentPoints(this.getGameEndPoints());
    this.additive_intents = [];
}


/**
 * @global
 */
var points_controller = new PointsController();