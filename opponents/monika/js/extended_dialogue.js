(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(['monika'], factory);
    } else if (typeof exports === 'object') {
        // Node, CommonJS-like
        module.exports = factory(require('monika'));
    } else {
        // Browser globals (root is window)
        root.monika_ext_dialogue = factory(root, root.monika);
        root.monika.ext_dialogue = root.monika_ext_dialogue;
    }
}(this, function (root, monika) {
var exports = {};

var extended_dialogues = {
    'psa1': [
        ["happy", "Thanks, ~player~! That really means a lot to me. I promise it won't take too long!"],
        ["awkward-question", "So, um... I know there are a lot of, um... other people... that you might want to see here at the Inventory!"],
        ["writing-tip", "And that's great! I know everyone working, um, <i>behind the scenes</i> here would love to see more people join us!"],
        ["writing-tip", "But we can't do it alone. We've all got so many other characters and things we're already working on..."],
        ["exasperated", "And we have lives of our own, as well!"],
        ["exasperated", "So if you just throw your ideas out there and walk away, we're probably not going to pay any attention to them at all!"],
        ["writing-tip", "So, if you really, really want to see a character here at the Inventory..."],
        ["writing-tip", "...then you should try making them yourself, first!"],
        ["exasperated", "And, yeah, it can be kind of difficult and daunting, especially at first..."],
        ["writing-tip", "But, there are a lot of people working here, behind the scenes, that would be more than willing to help you out!"],
        ["calm", "But <i>you</i> need to take the first step."],
        ["happy", "...anyway, that's my advice for right now."],
        ["calm", "Thanks for listening~"],
    ]
}

exports.dialogues = extended_dialogues;

var extendedDialoguePhase = [ "Talking...", function() { monika.ext_dialogue.continue_extended_dialogue(); }, false ];
var previousGamePhase = null;
var currentExtendedDialogue = null;
var backgroundEffects = [];

exports.extendedDialoguePhase = extendedDialoguePhase;

function display_dialogue (pose, dialogue) {
    var pl = monika.utils.get_monika_player();
    var img = pl.stage + '-' + pose + '.png';
    
    pl.chosenState.dialogue = expandDialogue(dialogue, pl, pl.currentTarget, null);
    pl.chosenState.image = img;
    
    gameDisplays[pl.slot-1].update(pl);
}

function continue_extended_dialogue () {
    var curr_info = currentExtendedDialogue;
    
    try {
        var next_line = extended_dialogues[curr_info.id][curr_info.line];
        display_dialogue(next_line[0], next_line[1]);
        curr_info.line++;
    } catch (e) {
        monika.reportException('in extended dialogue', e);
    } finally {
        if(curr_info.line >= extended_dialogues[curr_info.id].length) {
            end_extended_dialogue();
        } else {
            allowProgression(extendedDialoguePhase);
        }
    }
}
exports.continue_extended_dialogue = continue_extended_dialogue;

function extended_dialogue_start (id) {
    var slot = monika.utils.monika_slot();
    
    try {
        console.log("[Monika] Beginning extended dialogue with ID "+id);
        
        if (root.SENTRY_INITIALIZED) {
            root.Sentry.addBreadcrumb({
                category: 'monika',
                message: 'Beginning extended dialogue: '+id,
                level: 'info'
            });

            root.Sentry.setTag("extended_dialogue", id);
        }

        if (AUTO_FADE) forceTableVisibility(false);
        
        currentExtendedDialogue = {
            'id': id,
            'line': 0,
        }

        for(var i=1;i<players.length;i++) {
            if(players[i] && i !== slot) {
                try {
                    $gameBubbles[i-1].hide();
                    
                    var eff = new monika.effects.VisualGlitchEffect(i);
                    eff.execute();
                    backgroundEffects.push(eff);
                } catch (e) {
                    monika.reportException('when starting BG effects in extended_dialogue_start', e);
                }
            }
        }
    } catch(e) {
        monika.reportException('in extended_dialogue_start', e);
    } finally {
        previousGamePhase = gamePhase;
        $gameBubbles[slot-1].show();
        
        continue_extended_dialogue();
    }
}
monika.registerBehaviourCallback('extended_dialogue_start', extended_dialogue_start);

function end_extended_dialogue() {
    if (root.SENTRY_INITIALIZED) {
        root.Sentry.addBreadcrumb({
            category: 'monika',
            message: 'Ending extended dialogue...',
            level: 'info'
        });

        root.Sentry.setTag("extended_dialogue", null);
    }

    allowProgression(previousGamePhase);
    currentExtendedDialogue = null;
    
    for(var i=1;i<players.length;i++) {
        if(players[i]) {
            try {
                $gameBubbles[i-1].show();
            } catch (e) {
                monika.reportException('when re-showing bubbles in end_extended_dialogue', e);
            }
        }
    }
    
    backgroundEffects.forEach(function (eff) {
        try {
            eff.revert();    
        } catch (e) {
            monika.reportException('when reverting effects in end_extended_dialogue', e);
        }
    });
    backgroundEffects = [];
    
    if (AUTO_FADE) forceTableVisibility(true);
}

return exports;
}));
