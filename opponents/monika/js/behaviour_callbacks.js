(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(['monika'], factory);
    } else if (typeof exports === 'object') {
        // Node, CommonJS-like
        module.exports = factory(require('monika'));
    } else {
        // Browser globals (root is window)
        root.monika_callbacks = factory(root, root.monika);
        root.monika.callbacks = root.monika_callbacks;
    }
}(this, function (root, monika) {

    var exports = {};

    monika.registerBehaviourCallback('delete_blazer', function () {
        var slot = monika.utils.monika_slot();
        var effect = new monika.effects.GlitchPoseChange(slot, '2-removed-blazer.png', 250, 400, 776 - 400);

        setTimeout(function () {
            effect.execute();
        }, 750);
    });


    monika.registerBehaviourCallback('delete_glasses', function () {
        var slot = monika.utils.monika_slot();
        var effect = new monika.effects.GlitchPoseChange(slot, '2-removed-glasses.png', 250, 400, 776 - 400);

        setTimeout(function () {
            effect.execute();
        }, 750);
    });

    monika.registerBehaviourCallback('start_masturbating', function () {
        if (!monika.ACTIVE_FORFEIT_EFFECT && !monika.JOINT_FORFEIT_ACTIVE) {
            var slot = monika.utils.monika_slot();
            monika.ACTIVE_FORFEIT_EFFECT = new monika.effects.GlitchMasturbationEffect(slot);
            monika.ACTIVE_FORFEIT_EFFECT.execute();
        }
    });

    monika.registerBehaviourCallback('finish_masturbating', function () {
        if (monika.ACTIVE_FORFEIT_EFFECT) {
            monika.ACTIVE_FORFEIT_EFFECT.revert();
        }
    });

    monika.registerBehaviourCallback('scream_player_name', function () {
        var slot = monika.utils.monika_slot();
        var player_name = players[HUMAN_PLAYER].label.trim();

        var modified_player_name = player_name;
        var first_char = player_name.charAt(0);
        var last_char = player_name.charAt(player_name.length - 1);

        if (first_char.match(/[a-zA-Z]/i)) {
            // if the first character of the name is a letter, stutter the beginning of the name:
            // "M-Mister", for example
            modified_player_name = first_char.toUpperCase() + '-' + modified_player_name;
        }

        if (last_char.match(/[a-zA-Z]/i)) {
            // if the last character of the name is a letter, then drag out the name:
            // "M-Misterrr!", for example.
            modified_player_name = modified_player_name + last_char.repeat(2) + '!';
        }

        var effect = new monika.effects.DialogueContentEffect(slot, modified_player_name);
        effect.execute();
    });

    monika.registerBehaviourCallback('start_heavy_masturbation', function () {
        if (monika.ACTIVE_FORFEIT_EFFECT) {
            monika.ACTIVE_FORFEIT_EFFECT.setHeavyMode(true);
        }
    });

    monika.registerBehaviourCallback('react_aimee_strip', function () {
        // Aimee's slot is in the `recentLoser` variable
        // Move Monika as far away from that slot as possible.
        var targetSlot = undefined;
        var monikaSlot = monika.utils.monika_slot();
        if (recentLoser <= 2) {
            /* Move to far right */
            targetSlot = 5;
        } else {
            /* Move to far left */
            targetSlot = 0;
        }

        var eff = new monika.effects.CharacterDisplacementEffect(monikaSlot, targetSlot);
        eff.execute();
    });

    /* Helper function that induces a temporary glitch effect. */
    function transientGlitchEffect(slot, glitchTime) {
        var visEffect = new monika.effects.VisualGlitchEffect(slot);
        visEffect.execute(function () {
            setTimeout(function () {
                visEffect.revert();
            }, glitchTime);
        });
    }

    monika.registerBehaviourCallback('react_9s_hack', function () {
        var nines = monika.utils.find_player_by_id('9s');
        var current_stage = nines.stage;
        var slot = nines.slot;

        if (current_stage < 6) {
            /* Briefly glitch 9S to another stage
             * If he's at stages 0 or 1, we glitch him to stage 3 (lost shirt)
             * Otherwise we just glitch him to his next stage.
             */
            var to_stage = Math.max(3, current_stage + 1).toString();

            var current_img = nines.chosenState.image;
            var splits = current_img.split('-', 2);
            var suffix = current_img;

            if (splits.length === 2) {
                suffix = splits[1];
            }

            var next_img = to_stage + '-' + suffix;

            var eff1 = new monika.effects.GlitchPoseChange(slot, next_img, 500);
            var eff2 = new monika.effects.GlitchPoseChange(slot, current_img, 500);

            var phase1 = function () {
                eff1.execute(function () {
                    setTimeout(phase2, 1500);
                });
            }

            var phase2 = function () {
                eff2.execute();
            }

            setTimeout(phase1, 750);
        } else {
            /* Simply glitch 9S a bit... */
            transientGlitchEffect(slot, 1000);
        }
    });

    monika.registerBehaviourCallback('setRealizationMarkers', function (id) {
        var m = monika.utils.get_monika_player();
        if (m) {
            m.markers['realization_' + id] = 1;
        }
    });

    monika.registerBehaviourCallback('delayChange', function (text, delay) {
        var slot = monika.utils.monika_slot();
        var currentHTML = $gameDialogues[slot - 1].html();

        var effect = new monika.effects.DialogueContentEffect(slot, text);
        setTimeout(function () {
            if ($gameDialogues[slot - 1].html() === currentHTML) {
                effect.execute();
            }
        }, delay);
    });

    // no-ops
    monika.registerBehaviourCallback('jealousGlitch', function () {});
    monika.registerBehaviourCallback('majorJealousGlitch', function () {});


    /* The player responded "Yes." to the Sayonika start prompt.*/
    monika.registerBehaviourCallback('sayonikaYes', function () {
        var monika_pl = monika.utils.get_monika_player();
        var sayori = monika.utils.find_player_by_id('sayori');

        monika_pl.markers['affections-glitch'] = 1;
        sayori.markers['affections-glitch'] = 1;

        monika.SAYORI_AFFECTIONS_GLITCH = true;

        transientGlitchEffect(sayori.slot, 500);

        if (root.SENTRY_INITIALIZED) {
            root.Sentry.addBreadcrumb({
                category: 'monika',
                message: 'Sayonika activated.',
                level: 'info'
            });

            root.Sentry.setTag("sayonika", true);
        }

        monika_pl.chosenState.dialogue = "W-Wait, really? I wasn't expecting you to answer-- whoops!";
        monika_pl.chosenState.image = '2-shocked.png';
        gameDisplays[monika_pl.slot - 1].update(monika_pl);

        setTimeout(function () {
            if (monika_pl.alt_costume && monika_pl.alt_costume.id === 'monika_love_bug') {
                monika_pl.chosenState.dialogue = "Oh, I think I just messed up something in the code... A-Ahaha. <br> Eh? Some of these files are missing. I'll just replace them with working versions, hang on...";
            } else {
                monika_pl.chosenState.dialogue = "S-Sorry about that... your answer kinda caught me by surprise. A-And, uhm, I think I just messed up something in the code...";
            }
            
            monika_pl.chosenState.image = '2-awkward-question.png';
            
            gameDisplays[monika_pl.slot - 1].update(monika_pl);
        }, 1500);
    })


    /* The player responded "No." to the Sayonika start prompt.*/
    monika.registerBehaviourCallback('sayonikaNo', function () {
        var monika_pl = monika.utils.get_monika_player();

        if (root.SENTRY_INITIALIZED) {
            root.Sentry.addBreadcrumb({
                category: 'monika',
                message: 'Sayonika disabled.',
                level: 'info'
            });

            root.Sentry.setTag("sayonika", false);
        }

        monika_pl.chosenState.dialogue = "S-Sorry about that... your answer kinda caught me by surprise. A-And, uhm, I think I just messed up something in the code...";
        monika_pl.chosenState.image = '2-awkward-question.png';
        gameDisplays[monika_pl.slot - 1].update(monika_pl);
    });

    var saved_sayori_player = null;

    monika.registerBehaviourCallback('startJointMasturbation', function () {
        if (monika.JOINT_FORFEIT_ACTIVE) return;

        if (root.SENTRY_INITIALIZED) {
            root.Sentry.addBreadcrumb({
                category: 'monika',
                message: 'Beginning Sayonika forfeit...',
                level: 'info'
            });

            root.Sentry.setTag("sayonika_forfeit", true);
        }

        if (monika.ACTIVE_FORFEIT_EFFECT) {
            monika.ACTIVE_FORFEIT_EFFECT.revert();
            monika.ACTIVE_FORFEIT_EFFECT = null;
        }

        var monika_pl = monika.utils.get_monika_player();
        var sayori = monika.utils.find_player_by_id('sayori');

        monika_pl.markers['joint-masturbation'] = 1;
        sayori.markers['joint-masturbation'] = 1;
        monika_pl.addTag('tandem');
        monika_pl.addTag('bondage_forfeit');
        monika_pl.forfeit = [PLAYER_MASTURBATING, CAN_SPEAK];

        monika_pl.out = true;
        monika_pl.timer = 35;
        monika_pl.stage = 8;
        monika_pl.timeInStage = -1;
        monika_pl.finished = false;

        monika.JOINT_FORFEIT_ACTIVE = true;

        if (root.previousLoser == sayori.slot) {
            root.previousLoser = -1;
        }

        if (root.recentLoser == sayori.slot) {
            root.recentLoser = -1;
        }

        setTimeout(function () {
            /* Make Sayori's player entry disappear for the time being. */

            saved_sayori_player = players[sayori.slot];
            delete players[sayori.slot];

            updateAllBehaviours(monika_pl.slot, PLAYER_START_MASTURBATING, FEMALE_MASTURBATING);
            monika_pl.stage = 9;
        }, 0);
    });

    monika.registerBehaviourCallback('endJointMasturbation', function () {
        if (!monika.JOINT_FORFEIT_ACTIVE) return;

        if (root.SENTRY_INITIALIZED) {
            root.Sentry.addBreadcrumb({
                category: 'monika',
                message: 'Ending Sayonika forfeit...',
                level: 'info'
            });

            root.Sentry.setTag("sayonika_forfeit", false);
        }

        var monika_pl = monika.utils.get_monika_player();
        var sayori = saved_sayori_player;
        var sayori_slot = sayori.slot;

        monika.JOINT_FORFEIT_ACTIVE = false;
        monika_pl.removeTag('tandem');
        monika_pl.removeTag('bondage_forfeit');

        setTimeout(function () {
            /* Put Sayori back in her slot. */
            players[sayori_slot] = saved_sayori_player;
            sayori.markers['joint-masturbation'] = 1;
            sayori.markers['joint-masturbation-finished-1'] = 1;

            /* Hard reset stage, forfeit status, and other data for Sayori. */
            sayori.stage = 9;
            sayori.forfeit = [PLAYER_FINISHED_MASTURBATING, CAN_SPEAK];
            sayori.timeInStage = -1;
            sayori.finished = true;
            sayori.timer = 0;
        }, 0);
    });

    return exports;
}));