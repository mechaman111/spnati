/**
 * Contains code relating to the points unlock system.
 */

/**
 * Points earned per AI player defeated.
 * @global
 */
var AI_DEFEAT_POINTS = 20;

/**
 * Points earned for a complete victory.
 * This is scaled by the number of AI players at the table.
 * @global
 */
var TABLE_VICTORY_POINTS = 10;

var $pointsRows = $('#point-intent-rows');
var pointModalTips = [
    'Playing against a wide variety of characters will earn you more [InsertNameHere], so try to mix things up!',
    'Playing against characters on the Testing Tables will give you more [InsertNameHere].',
    'Certain characters may provide bonus [InsertNameHere] for playing against them. Be sure to check in regularly!'
]

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
 * Get the current net amount of points the player will earn at the end
 * of the game.
 * 
 * @returns {number}
 */
PointsController.prototype.getNetPointsEarned = function () {
    return this.additive_intents.reduce(function (acc, val) {
        var new_acc = acc + val.points;
        return (new_acc >= 0) ? new_acc : 0;
    }, 0);
}

/**
 * Get how many unlock points the player will have available at the end
 * of the game.
 * 
 * @returns {number}
 */
PointsController.prototype.getGameEndPoints = function () {
    return save.getUnlockPoints() + this.getNetPointsEarned();
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

function createPointIntentRow (reason, points, addedClass, omitLeadingPlus) {
    var ptsText = points.toString();
    if (points >= 0 && !omitLeadingPlus) {
        ptsText = '+' + ptsText;
    }

    var row = $('<tr>', {
        'class': 'point-intent-row'
    });
    row.append($('<td>', {
        'text': reason,
        'class': 'point-intent-reason ' + (addedClass || '')
    })).append($('<td>', {
        'text': ptsText,
        'class': 'point-intent-points ' + (addedClass || '')
    }));

    return row;
}

function showPointsModal() {
    var net_earned = points_controller.getNetPointsEarned();

    $('#points-modal-earned-count').text(net_earned);

    $pointsRows.empty();
    points_controller.additive_intents.forEach(function (intent) {
        createPointIntentRow(intent.reason, intent.points).appendTo($pointsRows);
    });

    createPointIntentRow(
        'Total Earned', net_earned,
        'point-intent-total', false
    ).appendTo($pointsRows);

    createPointIntentRow(
        'Current Points', points_controller.getCurrentPoints() + net_earned,
        'point-intent-total', true
    ).appendTo($pointsRows);

    points_controller.applyIntents();
    $('#points-modal-tip').text('Tip: ' + pointModalTips[getRandomNumber(0, pointModalTips.length)]);

    $pointsModal.modal('show');
}
