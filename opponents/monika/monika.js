if(!monika) {
    var monika = {};

    console.log("[Monika] Executing monika.js...");

    /* Base probabilities for glitch effects */
    monika.CHARACTER_CONT_GLITCH_CHANCE = 0;
    monika.CHARACTER_GLITCH_CHANCE = 0;
    monika.DIALOGUE_GLITCH_CHANCE = 0;
    monika.EDIT_GLITCH_CHANCE = 0;
    monika.DELETE_GLITCH_CHANCE = 0;
    
    monika.GLITCH_VISUAL = 'visual';  // continuous visual glitching
    monika.GLITCH_REPEAT = 'corrupt'; // dialogue corruption
    monika.GLITCH_EDIT = 'edit';      // edited dialogue
    monika.GLITCH_DELETE = 'delete';  // fake-deleted character

    monika.configureGlitchChance = function (mode) {
        console.log("[Monika] Glitches configured to mode "+mode);
        
        $("#options-monika-glitches-1").removeClass("active");
        $("#options-monika-glitches-2").removeClass("active");
        $("#options-monika-glitches-3").removeClass("active");
        
        switch (mode) {
        /* Off */
        case 3:
            $("#options-monika-glitches-3").addClass("active");
            monika.effects_flag = false;
            monika.CHARACTER_CONT_GLITCH_CHANCE = 0;
            monika.CHARACTER_GLITCH_CHANCE = 0;
            monika.DIALOGUE_GLITCH_CHANCE = 0;
            monika.EDIT_GLITCH_CHANCE = 0;
            monika.DELETE_GLITCH_CHANCE = 0;
            break;
        
        /* Low */
        case 2:
            $("#options-monika-glitches-2").addClass("active");
            monika.effects_flag = true;
            monika.CHARACTER_CONT_GLITCH_CHANCE = 0.025;
            monika.CHARACTER_GLITCH_CHANCE = 0.025;
            monika.DIALOGUE_GLITCH_CHANCE = 0.025;
            monika.EDIT_GLITCH_CHANCE = 0.025;
            monika.DELETE_GLITCH_CHANCE = 0.025;
            break;
            
        /* Normal */
        case 1:
        default:
            $("#options-monika-glitches-1").addClass("active");
            monika.effects_flag = true;
            monika.CHARACTER_CONT_GLITCH_CHANCE = 0.05;
            monika.CHARACTER_GLITCH_CHANCE = 0.05;
            monika.DIALOGUE_GLITCH_CHANCE = 0.05;
            monika.EDIT_GLITCH_CHANCE = 0.05;
            monika.DELETE_GLITCH_CHANCE = 0.05;
            break;
        }
    }
    
    monika.loadScript = function (scriptName) {
        console.log("[Monika] Loading module: "+scriptName);
        return $.getScript(scriptName).fail(function( jqxhr, settings, exception ) {
            console.error("[Monika] Error loading "+scriptName+": \n"+exception.toString());
        })
    }

    monika.loadScript("opponents/monika/js/canvas_utils.js");
    monika.loadScript("opponents/monika/js/effects.js");
    monika.loadScript("opponents/monika/js/glitches.js");
    monika.loadScript("opponents/monika/js/behaviour_callbacks.js");
    monika.loadScript("opponents/monika/js/extended_dialogue.js");

    /* Load CSS: */
    $('head').append('<link rel="stylesheet" type="text/css" href="opponents/monika/css/monika.css">');

    /* Add Options Modal settings: */
    var glitchOptionsContainer = $('<tr><td style="width:25%"><h4 class="modal-title modal-left">Monika Glitches</h4></td></tr>');
    glitchOptionsContainer.append('<td><nav><ul class="pagination" id="monika-glitch-options-list"></ul></nav>')

    $("#options-modal table").append(glitchOptionsContainer);

    var glitchOptionsList = $("#monika-glitch-options-list");
    glitchOptionsList.append('<li id="options-monika-glitches-3"><a href="#" onclick="monika.configureGlitchChance(3)">Off</a></li>')
    glitchOptionsList.append('<li id="options-monika-glitches-2"><a href="#" onclick="monika.configureGlitchChance(2)">Low</a></li>')
    glitchOptionsList.append('<li id="options-monika-glitches-1" class="active"><a href="#" onclick="monika.configureGlitchChance(1)">Normal</a></li>')

    monika.configureGlitchChance(1);

    monika.reportException = function(prefix, e) {
        console.log("[Monika] Exception swallowed "+prefix+": ");
        console.error(e);
    }

    monika.find_slot_by_id = function(id) {
        var lowercase_id = id.toLowerCase();
        
        for(var i=0;i<players.length;i++) {
            if(players[i] && players[i].id == lowercase_id) {
                return i;
            }
        }
    }

    monika.find_slot = function () {
        return monika.find_slot_by_id('monika');
    }

    monika.find_monika_player = function() {
        var slot = monika.find_slot();
        if(slot) {
            return players[slot];
        }
    }
    
    monika.present = function() {
        return monika.find_slot() != undefined;
    }
    
    /* pause the main game-- ensures effects aren't skipped by accident
     * Call allowProgression() to reverse this.
     */
    monika.disable_progression = function () {
        $mainButton.attr('disabled', true);
        actualMainButtonState = true;
    }
    
    monika.get_random_player = function(include_monika, include_glitched) {
        var activeSlots = [];
        for(var i=1;i<players.length;i++) {
            if(players[i]) {
                if(include_monika || players[i].id !== 'monika') {
                    if(include_glitched || !monika.active_effects.character_glitching[i]) {
                        activeSlots.push(i);
                    }
                }
            }
        }

        return activeSlots[getRandomNumber(0, activeSlots.length)];
    }
    
    monika.getGlitchMultiplier = function() {        
        // allow glitch chance multiplier to be set manually for debugging purposes
        if(monika.glitch_chance_mult) {
            var glitch_chance_mult = monika.glitch_chance_mult;
        } else {
            var pl = monika.find_monika_player();
            var glitch_chance_mult = 0;
            
            if (pl.stage < 3) {
                glitch_chance_mult = 0;
            } else if (pl.stage === 3) {
                glitch_chance_mult = 0.5;
            } else if (pl.stage <= 5) {
                glitch_chance_mult = 1;
            } else if (pl.stage <= 7) {
                glitch_chance_mult = 1.5;
            } else {
                glitch_chance_mult = 2;
            }
        }
        
        console.log("[Monika] Current glitch chance multiplier: "+glitch_chance_mult.toString());
        
        return glitch_chance_mult;
    }
    
    monika.modifiedChance = function(base_chance, chance_multiplier) {
        var actual_chance = base_chance * chance_multiplier;
        actual_chance = Math.max(0, Math.min(1, actual_chance)); // clip to [0, 1];
        
        return Math.random() <= actual_chance;
    }
    
    monika.setGlitchingMarker = function(targetedSlot, glitch_type, value) {
        var monika_player = monika.find_monika_player();
        var targeted_player = players[targetedSlot];
        
        if (!targeted_player) {
            return;
        }
        
        var oppID = targeted_player.folder.substr(0, targeted_player.folder.length - 1);
        oppID = oppID.substr(oppID.lastIndexOf("/") + 1);
        
        var type_glitch_marker = 'glitch-type-'+glitch_type;
        var base_glitch_marker = 'glitching-'+oppID;
        var specific_glitch_marker = base_glitch_marker+'-'+glitch_type;
        
        if(value) {
            monika_player.markers[base_glitch_marker] = 1;
            monika_player.markers[specific_glitch_marker] = 1;
            monika_player.markers[type_glitch_marker] = 1;
            monika_player.markers['glitched'] = 1;
        } else {
            monika_player.markers[base_glitch_marker] = 0;
            monika_player.markers[specific_glitch_marker] = 0;
            monika_player.markers[type_glitch_marker] = 0;
        }
    }
    
    monika.setRoundGlitchMarker = function(value) {
        var monika_player = monika.find_monika_player();
        if(value) {
            monika_player.markers['round-glitched'] = 1;
        } else {
            monika_player.markers['round-glitched'] = 0;
        }
    }
    
    monika.onRoundStart = function() {
        var glitch_chance_mult = monika.getGlitchMultiplier();
        
        /* Clean up any lingering effects from e.g. dialogue scripting */
        monika.cleanUpEffects();
        
        monika.setRoundGlitchMarker(false);
        
        if(!monika.effects_enabled()) {
            return; // bail now if effects are disabled
        }
        
        if(monika.force_cont_glitching || monika.modifiedChance(monika.CHARACTER_CONT_GLITCH_CHANCE, glitch_chance_mult)) {
            /* Pick a character to start glitching continuously. */
            if(monika.force_cont_glitching) {
                var targetedSlot = monika.force_cont_glitching;
            } else {
                var targetedSlot = monika.get_random_player(false, true);
            }
            
            console.log("[Monika] Targeted slot "+targetedSlot.toString()+" for round continuous glitching...");
            
            monika.setGlitchingMarker(targetedSlot, monika.GLITCH_VISUAL, true);
            monika.active_effects.round_targeted_glitching = targetedSlot;
            
            monika.setRoundGlitchMarker(true);
        }
        
        if(monika.force_delete_glitch || monika.modifiedChance(monika.DELETE_GLITCH_CHANCE, glitch_chance_mult)) {
            console.log("[Monika] Performing 'delete' glitch...");
            
            if(monika.force_delete_glitch) {
                var targetedSlot = monika.force_delete_glitch;
            } else {
                var targetedSlot = monika.get_random_player(false, false);
            }
            
            var original_label = players[targetedSlot].label;
            monika.corruptCharacterLabel(targetedSlot);
            
            monika.get_canvas_async($gameImages[targetedSlot-1], function (cv) {
                monika.tile_filter(cv, $gameImages);
                
                monika.active_effects.round_delete_glitching = {
                    'slot': targetedSlot,
                    'cv': cv,
                    'original_label': original_label
                };
                
                monika.setGlitchingMarker(targetedSlot, monika.GLITCH_DELETE, true);
                monika.setRoundGlitchMarker(true);
            }, true);
        }
        
        if(monika.force_dialogue_glitch || monika.modifiedChance(monika.DIALOGUE_GLITCH_CHANCE, glitch_chance_mult)) {
            console.log("[Monika] Glitching a character's dialogue...");
            
            if(monika.force_dialogue_glitch) {
                var targetedSlot = monika.force_dialogue_glitch;
            } else {
                var targetedSlot = monika.get_random_player(false, true);
            }
            
            monika.active_effects.round_dialogue_glitching = targetedSlot;
            monika.setGlitchingMarker(targetedSlot, monika.GLITCH_REPEAT, true);
            monika.setRoundGlitchMarker(true);
        }
        
        if(monika.force_edit_glitch || monika.modifiedChance(monika.EDIT_GLITCH_CHANCE, glitch_chance_mult)) {
            console.log("[Monika] Performing 'edit' glitch...");
            
            if(monika.force_edit_glitch) {
                var targetedSlot = monika.force_edit_glitch;
            } else {
                var targetedSlot = monika.get_random_player(false, true);
            }
            
            monika.active_effects.round_edit_glitching = targetedSlot;
            monika.setGlitchingMarker(targetedSlot, monika.GLITCH_EDIT, true);
            monika.setRoundGlitchMarker(true);
        }
    }

    monika.onPlayerTurn = function () {
        if(!monika.effects_enabled()) {
            return; // bail now if effects are disabled
        }
        
        console.log("[Monika] It's now the player's turn!");
        
        var glitch_chance_mult = monika.getGlitchMultiplier();

        if(monika.modifiedChance(monika.CHARACTER_GLITCH_CHANCE, glitch_chance_mult)) {
            console.log("[Monika] Glitching a random character's images...");
            
            var targetedSlot = monika.get_random_player(true, false);
            monika.temporaryCharacterGlitch(targetedSlot, getRandomNumber(500, 1500), 750);
            
            if(monika.modifiedChance(monika.CHARACTER_GLITCH_CHANCE / 3, glitch_chance_mult)) {
                console.log("[Monika] Glitching an adjacent character as well...");
                
                var adjSlot = targetedSlot;
                if(Math.random() <= 0.5) {
                    adjSlot++;
                } else {
                    adjSlot--;
                }
                
                if(adjSlot <= 0) {
                    adjSlot = players.length-1;
                } else if(adjSlot >= players.length) {
                    adjSlot = 1;
                }
                
                monika.temporaryCharacterGlitch(adjSlot, getRandomNumber(500, 1500), 750);
            }
        }
    }

    monika.onMonikaTurn = function () {
        console.log("[Monika] It's Monika's turn!");
    }
    
    var original_showOptionsModal = showOptionsModal;
    showOptionsModal = function () {
        try {
            if(monika.present()) {
                glitchOptionsContainer.show();
            } else {
                glitchOptionsContainer.hide();
            }
        } catch (e) {
            monika.reportException("in pre-showOptionsModal prep", e);
            glitchOptionsContainer.hide();
        } finally {
            return original_showOptionsModal.apply(null, arguments);
        }
    }
    
    /* monkey patch advanceGame() so we can intercept main button presses */
    var original_advanceGame = advanceGame;
    advanceGame = function() {
        if(!monika.present()) { return original_advanceGame.apply(null, arguments); }
        
        /* we don't use a finally... clause here, because if we're using a custom
         * context then we don't want the original function to be called.
         */
        try {
            /* Fixes a bug with joint masturbation... */
            if (previousLoser >= 0 && !players[previousLoser]) {
                previousLoser = -1;
            } 
            
            if(gamePhase !== monika.extendedDialoguePhase) {
                return original_advanceGame.apply(null, arguments); 
            } else {
                $mainButton.attr('disabled', true);
                actualMainButtonState = true;
                autoForfeitTimeoutID = undefined;
                
                if (AUTO_FADE) {
                    forceTableVisibility(false);
                }
                
                monika.extended_dialogue_continue();
            }
        } catch(e) {
            monika.reportException("in pre-advanceGame prep", e);
        }
    }

    /* monkey patch advanceTurn() so we can do spooky stuff */
    var original_advanceTurn = advanceTurn;
    advanceTurn = function() {
        if(!monika.present()) {
            /* if Monika isn't in this game then strictly do normal behaviour */
            return original_advanceTurn.apply(null, arguments);
        }
        
        try {
            var active_slot = currentTurn+1;
            if(active_slot >= players.length) {
                active_slot = 0;
            }
            
            if(currentTurn === 0) {
                monika.onRoundStart();
            }
        } catch (e) {
            monika.reportException("in pre-advanceTurn prep", e);
        } finally {
            original_advanceTurn();
        }
        
        if(active_slot != undefined && players[active_slot]) {
            if(players[active_slot].id === 'human') {
                monika.onPlayerTurn();
            } else if(players[active_slot].id === 'monika') {
                monika.onMonikaTurn();
            } else if(active_slot === monika.active_effects.round_targeted_glitching) {
                monika.startCharacterGlitching(active_slot, 750, 750);
            }
        }
    }
    
    monika.undoDeleteGlitchEffect = function() {
        var deleteGlitchInfo = monika.active_effects.round_delete_glitching;
        if(deleteGlitchInfo) {
            monika.setGlitchingMarker(deleteGlitchInfo.slot, monika.GLITCH_DELETE, null);
            monika.undoLabelCorruption(deleteGlitchInfo.slot);
        }
        
        monika.active_effects.round_delete_glitching = null;
    }
    
    /* hook into completeContinuePhase to undo deletion glitches */
    var original_completeContinuePhase = completeContinuePhase;
    completeContinuePhase = function() {
        if(!monika.present()) { return original_completeContinuePhase.apply(null, arguments); }
            
        try {
            monika.undoDeleteGlitchEffect();
        } catch (e) {
            monika.reportException("in pre-completeContinuePhase prep", e);
        } finally {
            return original_completeContinuePhase.apply(null, arguments); 
        }
    }
    
    var original_completeMasturbatePhase = completeMasturbatePhase;
    completeMasturbatePhase = function() {
        if(!monika.present()) { return original_completeMasturbatePhase.apply(null, arguments); }
            
        try {
            monika.undoDeleteGlitchEffect();
        } catch (e) {
            monika.reportException("in pre-completeMasturbatePhase prep", e);
        } finally {
            return original_completeMasturbatePhase.apply(null, arguments); 
        }
    }
    
    /* hook into completeStripPhase to undo deletion glitches when the player loses the round */
    var original_completeStripPhase = completeStripPhase;
    completeStripPhase = function() {
        if(!monika.present()) { return original_completeStripPhase.apply(null, arguments); }
        
        try {
            monika.undoDeleteGlitchEffect();
        } catch (e) {
            monika.reportException("in pre-completeStripPhase prep", e);
        } finally {
            return original_completeStripPhase.apply(null, arguments);
        }
    }
    
    /* hook into completeRevealPhase to stop glitching when a round is complete */
    var original_completeRevealPhase = completeRevealPhase;
    completeRevealPhase = function() {
        if(!monika.present()) { return original_completeRevealPhase.apply(null, arguments); }
        
        try {
            var targetedSlot = monika.active_effects.round_targeted_glitching;
            if(targetedSlot) {
                monika.setGlitchingMarker(targetedSlot, monika.GLITCH_VISUAL, null);
                monika.stopCharacterGlitching(targetedSlot);
            }
            
            var editedPlayer = monika.active_effects.round_edit_glitching;
            if(editedPlayer) {
                monika.setGlitchingMarker(editedPlayer, monika.GLITCH_EDIT, null);
                monika.removeEditedDialogueStyle(editedPlayer);
            }
            
            var dialogueGlitchPlayer = monika.active_effects.round_dialogue_glitching;
            if(dialogueGlitchPlayer) {
                monika.setGlitchingMarker(dialogueGlitchPlayer, monika.GLITCH_REPEAT, null);
            }
            
            monika.active_effects.round_targeted_glitching = null;
            monika.active_effects.round_dialogue_glitching = null;
            monika.active_effects.round_edit_glitching = null;
        } catch (e) {
            monika.reportException("in pre-completeRevealPhase prep", e);
        } finally {
            return original_completeRevealPhase.apply(null, arguments); 
        }
    }

    /* Hook into updateGameVisual to handle:
     * - pose transitions while Monika is masturbating
     * - fixing dialogue box glitches for characters that are changing dialogue
     * - deletion glitch effects (character image manipulation, dialogue fuckery)
     */
    var original_fixupDialogue = fixupDialogue;
    var original_updateGameVisual = updateGameVisual;
    updateGameVisual = function(player) {
        if(!monika.present()) { return original_updateGameVisual.apply(null, arguments); }
        
        try {
            /* This is kind of a quick and dirty hack-- but we do these checks
             * here because this function is called pretty much in all phases of the game,
             * which means we don't have to hook into an entirely new function.
             */
            var monika_slot = monika.find_slot();
            if(players[monika_slot].stage === 9 && monika.active_effects.glitch_masturbation == null) {
                monika.start_masturbating();
            }
            
            if(players[monika_slot].stage === 10 && monika.active_effects.glitch_masturbation != null) {
                monika.finish_masturbating();
            }
            
            monika.active_effects.corrupted_dialogue[player-1] = null;
            if(monika.active_effects.overflow_text[player-1]) {
                monika.resetDialogueBoxStyles(player);
            }
            
            if (players[player]) {
                monika.setGlitchingMarker(player, monika.GLITCH_REPEAT, null);
                
                if (players[player].id === 'monika') {
                    // Temporarily disable dialogue fixups for this call.
                    fixupDialogue = function (str) { return str; };
                }
            }
        } catch (e) {
            monika.reportException("in pre-updateGameVisual prep", e);
        } finally {
            original_updateGameVisual(player);
            fixupDialogue = original_fixupDialogue;
        }
        
        if(monika.active_effects.round_delete_glitching) {
            var glitch_data = monika.active_effects.round_delete_glitching;
            var glitch_player = players[glitch_data.slot];
            
            var re = new RegExp(glitch_data.original_label, 'gi');
            var re2 = new RegExp(glitch_player.label, 'gi');
            
            var original_dialogue = $gameDialogues[player-1].html();
            
            var modified_dialogue = original_dialogue.replace(re, function (match) {
                return monika.generate_glitch_text(match.length + getRandomNumber(0, 5))
            });
            
            modified_dialogue = modified_dialogue.replace(re2, function (match) {
                return monika.generate_glitch_text(match.length)
            });
            
            if(glitch_data.slot === player) {
                monika.set_image_from_canvas($gameImages[player-1], glitch_data.cv);
                
                // lightly pepper a deleted character's dialogue with glitch chars
                modified_dialogue = monika.random_character_replacement(monika.inline_glitch_chars, modified_dialogue, 0.1)
            }
            
            $gameDialogues[player-1].html(modified_dialogue);
        }
        
        // dialogue-modifying effects will be undone at next visuals update for this player
        if(monika.active_effects.round_dialogue_glitching === player) {
            if(monika.force_zalgo_corruption || Math.random() < 0.5) {
                monika.zalgoCorruptDialogue(player);
            } else {
                monika.repeatCorruptDialogue(player); 
            }
        } 
        
        if(monika.active_effects.round_edit_glitching === player) {
            monika.applyEditedDialogueStyle(player);
        }
        
        if(players[player] && players[player].id === 'monika' && players[player].stage === 9) {
            if (monika.active_effects['joint_masturbation']) {
                /* Joint masturbation */
                $gameLabels[player].html("Monika & Sayori");
            } else {
                /* 'Regular' glitch masturbation */
                var el = $gameImages[player-1]
                var current_img = el.attr('src').substr(17);
                
                if (el[0].complete) {
                    monika.glitch_pose_transition(player, current_img, 0, 200);
                } else {
                    el.one('load', function () {
                        console.log("[Monika] image loaded, glitching to new pose...");
                        monika.glitch_pose_transition(player, current_img, 0, 200);
                    });
                }
            }
        }
    }
}
