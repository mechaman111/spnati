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
    if(!monika.active_effects.glitch_masturbation) {
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
