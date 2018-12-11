monika.current_ext_dialogue = null;

monika.ext_dialogues = {
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

monika.extendedDialoguePhase = [ "Talking...", function() { monika.extended_dialogue_continue(); }, false ];
monika.previousGamePhase = null;

monika.display_dialogue = function(pose, dialogue) {
    var slot = monika.find_slot();
    
    var current_img = monika.assemble_pose_filename({
        'opponent': "monika",
        'stage': players[slot].stage,
        'pose': pose,
        'ext': 'png'
    });
    
    var expanded = expandDialogue(dialogue, players[slot], null);
    
    $gameImages[slot-1].attr('src', current_img);
    $gameDialogues[slot-1].html(expanded);
}

monika.extended_dialogue_continue = function () {
    var curr_info = monika.current_ext_dialogue;
    
    try {        
        var next_line = monika.ext_dialogues[curr_info.id][curr_info.line];
        monika.display_dialogue(next_line[0], next_line[1]);
        curr_info.line++;
    } catch (e) {
        console.error(e);
    } finally {
        if(curr_info.line >= monika.ext_dialogues[curr_info.id].length) {
            monika.extended_dialogue_end();
        } else {
            allowProgression(monika.extendedDialoguePhase);
        }
    }
}

monika.extended_dialogue_end = function() {
    allowProgression(monika.previousGamePhase);
    monika.current_ext_dialogue = null;
    
    for(var i=1;i<players.length;i++) {
        if(players[i]) {
            try {
                $gameBubbles[i-1].show();
                monika.undoCharacterGlitch(i);
            } catch (e) {
                console.error(e);
            }
        }
    }
}

monika.extended_dialogue_start = function (id) {
    var monika_slot = monika.find_slot();
    
    try {
        console.log("[Monika] Beginning extended dialogue with ID "+id);
        
        monika.current_ext_dialogue = {
            'id': id,
            'line': 0,
        }

        for(var i=1;i<players.length;i++) {
            if(players[i] && i !== monika_slot) {
                try {
                    $gameBubbles[i-1].hide();
                    monika.glitchCharacter(i);
                } catch (e) {
                    console.error(e);
                }
            }
        }
    } catch(e) {
        console.error(e);
    } finally {
        monika.previousGamePhase = gamePhase;
        $gameBubbles[monika_slot-1].show();
        
        monika.extended_dialogue_continue();
    }
}
