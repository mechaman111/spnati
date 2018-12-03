monika.delete_blazer = function() {
    try {
        var slot = monika.find_slot();
        monika.glitch_pose_transition(slot, '2-removed-blazer.png', 750, 500, 400, 776-400);
        setTimeout(monika.disable_progression, 0);
    } catch (e) {
        allowProgression();
        console.error(e);
    } finally {
        setTimeout(allowProgression, 1750);
    }
}

monika.start_masturbating = function() {
    if(!monika.active_effects.glitch_masturbation && !monika.active_effects.affections_glitch) {
        var slot = monika.find_slot();
        monika.glitch_mast_heavy(false);
        monika.glitch_masturbation(slot);
    }
}

monika.finish_masturbating = function() {
    monika.stop_glitch_masturbation(monika.find_slot());
}

monika.scream_player_name = function () {
    var slot = monika.find_slot();
    var player_name = players[HUMAN_PLAYER].label.trim();

    var modified_player_name = player_name;
    var first_char = player_name.charAt(0);
    var last_char = player_name.charAt(player_name.length-1);

    if(first_char.match(/[a-zA-Z]/i)) {
        // if the first character of the name is a letter, stutter the beginning of the name:
        // "M-Mister", for example
        modified_player_name = first_char.toUpperCase() + '-' + modified_player_name;
    }

    if(last_char.match(/[a-zA-Z]/i)) {
        // if the last character of the name is a letter, then drag out the name:
        // "M-Misterrr!", for example.
        modified_player_name = modified_player_name + last_char.repeat(2) + '!';
    }

    $gameDialogues[slot-1].html(modified_player_name);
}

monika.start_heavy_masturbation = function () {
    monika.glitch_mast_heavy(true);
}

monika.react_aimee_strip = function() {
    // Aimee's slot is in the `recentLoser` variable
    // Move Monika as far away from that slot as possible.
    if(recentLoser <= 2) {
        /* Move to far right */
        monika.moveCharacterOverUI(monika.find_slot(), 5);
    } else {
        /* Move to far left */
        monika.moveCharacterOverUI(monika.find_slot(), 0);
    }
}

monika.react_9s_hack = function () {
    // find 9S:
    var nines_slot = monika.find_slot_by_id('9s');
    var current_stage = players[nines_slot].stage;

    if(current_stage < 6) {
        /* Briefly glitch 9S to another stage
         * If he's at stages 0 or 1, we glitch him to stage 3 (lost shirt)
         * Otherwise we just glitch him to his next stage.
         */
        var current_img = $gameImages[nines_slot-1].attr('src');
        var pose = monika.split_pose_filename(current_img);
        pose.stage = Math.max(3, current_stage+1).toString();

        var next_img = monika.assemble_pose_filename(pose);

        var do_glitch_1 = function () {
            try {
                monika.glitchCharacter(nines_slot);
                monika.disable_progression();
            } catch (e) {
                allowProgression();
                console.error(e);
            } finally {
                monika.schedule_when_loaded($gameImages[nines_slot-1], do_transition, 500);
            }
        }

        var do_transition = function () {
            try {
                monika.active_effects.character_glitch[nines_slot-1] = null;
                $gameImages[nines_slot-1].attr('src', next_img);
            } catch (e) {
                allowProgression();
                console.error(e);
            } finally {
                monika.schedule_when_loaded($gameImages[nines_slot-1], do_glitch_2, 1500);
            }
        }

        var do_glitch_2 = function() {
            try {
                monika.glitchCharacter(nines_slot);
            } catch (e) {
                allowProgression();
                console.error(e);
            } finally {
                monika.schedule_when_loaded($gameImages[nines_slot-1], do_revert, 500);
            }
        }

        var do_revert = function () {
            try {
                monika.active_effects.character_glitch[nines_slot-1] = null;
                $gameImages[nines_slot-1].attr('src', current_img);
            } catch (e) {
                console.error(e);
            } finally {
                allowProgression();
            }
        }

        setTimeout(allowProgression, 4500); // juuust in case
        setTimeout(monika.disable_progression, 0);
        monika.schedule_when_loaded($gameImages[nines_slot-1], do_glitch_1, 1500);
    } else {
        /* Simply glitch 9S a bit... */
        monika.glitchCharacter(nines_slot);
    }
}

monika.setRealizationMarkers = function(id) {
    var m = monika.find_monika_player();
    if(m) {
        m.markers['realization_'+id] = true;
    }
}

monika.delayChange = function(text, delay) {
    var slot = monika.find_slot();
    var currentHTML = $gameDialogues[slot-1].html();
    
    if(slot) {
        setTimeout(function () {
            if($gameDialogues[slot-1].html() === currentHTML) {
                $gameDialogues[slot-1].html(text);
            }
        }, delay);
    }
}

monika.jealousGlitch = function() {
    try {
        if(!monika.effects_enabled()) return;
        
        var slot = monika.find_slot();
        for(var i=1;i<players.length;i++) {
            if(players[i] && i !== slot) {
                monika.temporaryCharacterGlitch(i, 100, 500);
            }
        }
    } catch (e) {
        console.error(e);
    }
}

monika.majorJealousGlitch = function() {
    try {
        if(!monika.effects_enabled()) return;
        
        var slot = monika.find_slot();
        for(var i=1;i<players.length;i++) {
            if(players[i] && i !== slot) {
                monika.temporaryCharacterGlitch(i, 100, 750);
            }
        }
    } catch (e) {
        console.error(e);
    }
}


/* The player responded "Yes." to the Sayonika start prompt. */
monika.sayonikaYes = function () {
    try {
        var slot = monika.find_slot();
        var sayori_slot = monika.find_slot_by_id('sayori');
        
        monika.active_effects['affections_glitch'] = true;
        
        players[sayori_slot].markers['affections-glitch'] = true;
        players[slot].markers['affections-glitch'] = true;
        
        $gameDialogues[slot-1].html("W-Wait, really? I wasn't expecting you to answer-- whoops!");
        $gameImages[slot-1].attr('src', 'opponents/monika/2-shocked.png');
        
        monika.temporaryCharacterGlitch(sayori_slot, 0, 500);
        
        setTimeout(function () {
            $gameDialogues[slot-1].html("S-Sorry about that... your answer kinda caught me by surprise. A-And, uhm, I think I just messed up something in the code...");
            $gameImages[slot-1].attr('src', 'opponents/monika/2-awkward-question.png');
        }, 1500);
    } catch (e) {
        console.error(e);
    }
}


/* The player responded "No." to the Sayonika start prompt.*/
monika.sayonikaNo = function () {
    try {
        var slot = monika.find_slot();
        $gameDialogues[slot-1].html("Oh, I was just teasing you a bit! I didn't expect you to actually answer that, ehehe~!");
        $gameImages[slot-1].attr('src', 'opponents/monika/2-happy.png');
    } catch (e) {
        console.error(e);
    }
}

monika.saved_sayori_player = null;
monika.saved_sayori_slot = null;

monika.startJointMasturbation = function () {
    try {
        monika.stop_glitch_masturbation();
        
        var monika_slot = monika.find_slot();
        var sayori_slot = monika.find_slot_by_id('sayori');
        
        monika.active_effects['joint_masturbation'] = true;
        
        players[monika_slot].markers['joint-masturbation'] = true;
        players[sayori_slot].markers['joint-masturbation'] = true;
        
        players[monika_slot].tags.push('tandem');
        
        /* Hard reset stage and forfeit timers for Monika. */
        players[monika_slot].forfeit = [PLAYER_MASTURBATING, CAN_SPEAK];
        
    	players[monika_slot].out = true;
        timers[monika_slot] = 25;
        
    	players[monika_slot].stage = 8;
        
        $gameImages[monika_slot-1].off('load');
        
        setTimeout(function () {
            /* Make Sayori's player entry disappear for the time being. */
            monika.saved_sayori_player = players[sayori_slot];
            monika.saved_sayori_slot = sayori_slot;
            delete players[sayori_slot];
            
            updateAllBehaviours(monika_slot, FEMALE_MASTURBATING, players[monika_slot]);
            updateBehaviour(monika_slot, PLAYER_START_MASTURBATING);
            
            updateAllVolatileBehaviours();
        	commitAllBehaviourUpdates();
            updateAllGameVisuals();
            
        	players[monika_slot].stage = 9;
        }, 0);
    } catch (e) {
        console.error("Error in monika.startJointMasturbation(): "+e.toString());
    }
}

monika.endJointMasturbation = function () {
    try {
        var monika_slot = monika.find_slot();
        var sayori_slot = monika.saved_sayori_slot;
        
        monika.active_effects['joint_masturbation'] = false;
        
        var pos = players[monika_slot].tags.indexOf('tandem');
        if (pos >= 0) {
            players[monika_slot].tags.splice(pos, 1);
        }
        
        setTimeout(function () {
            /* Put Sayori back in her slot. */
            players[sayori_slot] = monika.saved_sayori_player;
            
            /* Hard reset stage, forfeit status, and other data for Sayori. */
            players[sayori_slot].stage = 9;
            players[sayori_slot].forfeit = [PLAYER_FINISHED_MASTURBATING, CAN_SPEAK];
        	players[sayori_slot].timeInStage = -1;
        	players[sayori_slot].finished = true;
            timers[sayori_slot] = 0;
        }, 0);
    } catch (e) {
        console.error("Error in monika.endJointMasturbation(): "+e.toString());
    }
}
