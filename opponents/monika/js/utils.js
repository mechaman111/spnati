(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD
        define([], factory);
    } else if (typeof exports === 'object') {
        // Node, CommonJS-like
        module.exports = factory();
    } else {
        // Browser globals (root is window)
        root.utils = factory(root);
        root.monika.utils = root.utils;
    }
}(this, function () {

var exports = {};

function find_player_by_id (id) {
    for (var i=0;i < players.length;i++) {
        if (players[i] && players[i].id === id) {
            return players[i];
        }
    }
    
    return null;
}
exports.find_player_by_id = find_player_by_id;

function get_monika_player() {
    return find_player_by_id('monika');
}
exports.get_monika_player = get_monika_player;

function monika_present() {
    return players.some(function (p) { return p.id === 'monika'; });
}
exports.monika_present = monika_present;

function monika_slot () {
    var idx = null;
    players.forEach(function (p) {
        if (p.id === 'monika') idx = p.slot;
    });
    
    return idx;
}
exports.monika_slot = monika_slot;

function disable_progression () {
    $mainButton.attr('disabled', true);
    actualMainButtonState = true;
}
exports.disable_progression = disable_progression;

function pick_glitch_target (exclude_slots) {
    var valid_targets = [];
    players.forEach(function (pl) {
        if (pl.id !== 'monika' && (!exclude_slots || exclude_slots.indexOf(pl.slot) < 0)) {
            valid_targets.push(pl.slot);
        }
    });
    
    if (valid_targets.length <= 0) return;
    return valid_targets[getRandomNumber(0, valid_targets.length)];
}
exports.pick_glitch_target = pick_glitch_target;

return exports;
}));
