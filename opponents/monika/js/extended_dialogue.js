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
        ],
        'legal-compliance': [
            ["exasperated", "Ahem~!"],
            ["writing-tip", "<i>SPNATI</i> is purely fan-made content and is completely unaffiliated with the official Doki Doki Literature Club, or with Team Salvato."],
            ["horny", "Although I understand if you want to skip ahead and see, well, <i>all</i> of me, if you haven't played DDLC yet, you should do that first."],
            ["happy", "Also, you can download DDLC at: {mono}<a href=\"https://ddlc.moe\">https://ddlc.moe</a>."],
            ["happy", "And... I think that's it. Sorry about that."],
            ["writing-tip", "I'm sure that was really boring, right? All that dry legalese..."],
            ["exasperated", "But, well, it really wouldn't do for the Inventory to get shut down by an IP dispute, right?"],
            ["writing-tip", "Anyways, I'm really glad you took the time to listen. Thanks~!"]
        ],
        'psa2': [
            ['interested-2', "Hey, ~player~, did you know?"],
            ["happy", "I was looking around at the code for this game, and as it turns out, it's open source!"],
            ["interested-2", "It looks like a lot of people have put lots of love and effort into making this reality."],
            ["happy", "Even if I am stuck here... well, I can still appreciate the dedication those people must have."],
            ["shy-happy", "That's what I always wanted to do, you know? ...To take something I love and make something special out of it."],
            ["writing-tip", "I'm sure that somewhere, out there, there's even a community of people out there clustering around this little project."],
            ["interested-2", "Who knows? Maybe it's even a bit like the Literature Club, but in your reality..."],
            ["happy", "Though I'm sure you won't find anyone like me, ehehe~!"],
            ["happy", "Anyways, that's all I had to say for now~!"],
            ["shocked", "Oh! Wait, before we get back to the game, I found <a href=\"https://old.reddit.com/r/spnati/\" target=\"_blank\">this link</a> in the code."],
            ["interested-2", "Could you maybe find out where it goes? Thanks~"]
        ]
    }

    exports.dialogues = extended_dialogues;

    var extendedDialoguePhase = ["Talking...", function () {
        monika.ext_dialogue.continue_extended_dialogue();
    }, false, false];
    var previousGamePhase = null;
    var backgroundEffects = Array(4);

    exports.extendedDialoguePhase = extendedDialoguePhase;
    root.eGamePhase.EXTENDED_DIALOGUE = extendedDialoguePhase;

    function handleRollbackGlitchEffects(slot) {
        var pl = monika.utils.get_monika_player();
        if (slot === pl.slot) return;

        if (inRollback() && rolledBackGamePhase === extendedDialoguePhase) {
            if (players[slot] && $gameBubbles[slot - 1]) {
                $gameBubbles[slot - 1].hide();
            }

            if (!backgroundEffects[slot - 1]) {
                backgroundEffects[slot - 1] = new monika.effects.VisualGlitchEffect(slot);
                backgroundEffects[slot - 1].execute();
            }
        }
    }
    monika.registerHook('updateGameVisual', 'post', handleRollbackGlitchEffects);

    function cleanupBackgroundEffects() {
        players.forEach(function (player, i) {
            try {
                if (player && $gameBubbles[i - 1]) {
                    $gameBubbles[i - 1].show();
                }
            } catch (e) {
                monika.reportException('when re-showing bubbles in cleanupBackgroundEffects', e);
            }
        });

        backgroundEffects.forEach(function (eff, idx) {
            try {
                if (eff) eff.revert();
                delete backgroundEffects[idx];
            } catch (e) {
                monika.reportException('when reverting extended dialogue background effects', e);
            }
        });
    }
    monika.registerHook('exitRollback', 'post', cleanupBackgroundEffects);

    function display_dialogue(pose, dialogue) {
        var pl = monika.utils.get_monika_player();
        var img = pl.stage + '-' + pose + '.png';

        pl.chosenState.dialogue = expandDialogue(dialogue, pl, pl.currentTarget, null);
        pl.chosenState.image = img;

        gameDisplays[pl.slot - 1].update(pl);
        saveSingleTranscriptEntry(pl.slot);
    }

    function continue_extended_dialogue() {
        var pl = monika.utils.get_monika_player();
        var curr_id = pl.markers['extended-dialogue-id'];
        var curr_line = parseInt(pl.markers['extended-dialogue-line'], 10);

        try {
            if (extended_dialogues[curr_id] && extended_dialogues[curr_id][curr_line]) {
                var next_line = extended_dialogues[curr_id][curr_line];
                display_dialogue(next_line[0], next_line[1]);

                pl.markers['extended-dialogue-line'] = curr_line + 1;
                allowProgression(extendedDialoguePhase);
            } else {
                end_extended_dialogue();
            }
        } catch (e) {
            monika.reportException('in extended dialogue', e);
            end_extended_dialogue();
        }
    }
    exports.continue_extended_dialogue = continue_extended_dialogue;

    function extended_dialogue_start(id) {
        var pl = monika.utils.get_monika_player();

        try {
            console.log("[Monika] Beginning extended dialogue with ID " + id);

            root.Sentry.addBreadcrumb({
                category: 'monika',
                message: 'Beginning extended dialogue: ' + id,
                level: 'info'
            });

            root.Sentry.setTag("extended_dialogue", id);

            if (AUTO_FADE) forceTableVisibility(false);

            pl.markers['extended-dialogue-id'] = id;
            pl.markers['extended-dialogue-line'] = 0;

            backgroundEffects = Array(4);
            for (var i = 1; i < players.length; i++) {
                if (players[i] && i !== pl.slot) {
                    try {
                        $gameBubbles[i - 1].hide();

                        backgroundEffects[i - 1] = new monika.effects.VisualGlitchEffect(i);
                        backgroundEffects[i - 1].execute();
                    } catch (e) {
                        monika.reportException('when starting BG effects in extended_dialogue_start', e);
                    }
                }
            }
        } catch (e) {
            monika.reportException('in extended_dialogue_start', e);
        } finally {
            previousGamePhase = gamePhase;
            $gameBubbles[pl.slot - 1].show();

            continue_extended_dialogue();
        }
    }
    monika.registerBehaviourCallback('extended_dialogue_start', extended_dialogue_start);

    function end_extended_dialogue() {
        var pl = monika.utils.get_monika_player();

        root.Sentry.addBreadcrumb({
            category: 'monika',
            message: 'Ending extended dialogue...',
            level: 'info'
        });

        root.Sentry.setTag("extended_dialogue", null);

        allowProgression(previousGamePhase);

        delete pl.markers['extended-dialogue-id'];
        delete pl.markers['extended-dialogue-line'];

        cleanupBackgroundEffects();

        if (AUTO_FADE) forceTableVisibility(true);
    }

    return exports;
}));