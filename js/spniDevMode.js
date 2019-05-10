Opponent.prototype.initDevMode = function () {
    /* Assign a unique ID number to each case and state, for later lookup. */
    var curStateID = 0;
    var stateIndex = {};
    
    this.xml.find('case').each(function (caseIdx, elem) {
        $(this).attr('dev-id', caseIdx);
        
        $(elem).find('state').each(function () {
            var pose = $(this).attr('img') || '';
            var text = $(this).text() || '';
            
            /* Extract a pose name: */
            var match = pose.match(/^(?:custom:)?(?:\d+-)([^.]+)(?:\..*)?$/m);
            if (match) {
                pose = match[1];
            }
            
            var key = pose.trim() + ':' + text.trim();
            if (stateIndex[key]) {
                $(this).attr('dev-id', stateIndex[key]);
            } else {
                $(this).attr('dev-id', curStateID);
                stateIndex[key] = curStateID;
                curStateID += 1;
            }
        });
    });
    
    this.stateIndex = stateIndex;
    this.originalXml = $(this.xml).clone();
    this.editLog = [];
}


function DevModeDialogueBox(gameBubbleElem) {
    this.container = gameBubbleElem;
    this.editField = gameBubbleElem.find('.dialogue-edit-entry');
    this.dialogueArea = gameBubbleElem.find('.dialogue');
    this.statusLine = gameBubbleElem.find('.dialogue-edit-status');
    
    this.editButton = gameBubbleElem.find('.dialogue-edit-button');
    this.newResponseButton = gameBubbleElem.find('.dialogue-respond-button');
    this.confirmButton = gameBubbleElem.find('.edit-confirm');
    this.cancelButton = gameBubbleElem.find('.edit-cancel');
    this.previewButton = gameBubbleElem.find('.edit-preview');
    this.poseButton = gameBubbleElem.find('.edit-pose');
    
    this.poseFieldContainer = gameBubbleElem.find('.edit-pose-row');
    this.poseList = gameBubbleElem.find('.edit-pose-list');
    this.poseField = gameBubbleElem.find('.dialogue-pose-entry');
    
    this.editButton.click(this.editCurrentState.bind(this));
    this.newResponseButton.click(this.startNewResponse.bind(this));
    
    this.previewButton.click(this.togglePreview.bind(this));
    this.cancelButton.click(this.cancelEdit.bind(this));
    this.confirmButton.click(this.saveEdits.bind(this));
    this.poseButton.click(function () {
        this.poseFieldContainer.toggle();
    }.bind(this));
    
    this.editField.on('change', this.updateEditField.bind(this));
    this.poseField.on('change', this.updatePoseField.bind(this));
    
    this.player = null;
    this.editingState = false;
    this.respondingToPlayer = null;
    this.currentStateDialogue = null;
    this.currentStatePose = null;
}

DevModeDialogueBox.prototype.update = function (player) {
    this.player = player;
    
    if (!player) return;
    
    this.currentCase = player.chosenState.parentCase;
    this.currentState = {'text': player.chosenState.rawDialogue, 'pose': player.chosenState.image};
    
    if (this.container.attr('data-editing')) {
        this.cancelEdit();
    }
    
    if (devModeActive && devModeTarget === player.slot) {
        this.container.attr('data-dev-target', 'true');
    } else {
        this.container.attr('data-dev-target', null);
    }
}

DevModeDialogueBox.prototype.updateEditField = function () {
    this.currentStateDialogue = this.editField.val();
}

DevModeDialogueBox.prototype.updatePoseField = function () {
    this.currentStatePose = this.poseField.val();
    this.player.chosenState.image = this.currentStatePose;
    
    gameDisplays[this.player.slot-1].updateImage(this.player);
}

DevModeDialogueBox.prototype.startEditMode = function () {
    KEYBINDINGS_ENABLED = false;
    this.editField.val(this.currentStateDialogue).attr('placeholder', this.player.chosenState.rawDialogue);
    this.container.attr('data-editing', 'entry');
    
    this.currentStatePose = this.player.chosenState.image;
    this.poseField.val(this.currentStatePose);
    
    var caseSelector = (this.player.stage == -1 ? 'start, stage[id=1] case[tag=game_start]' : 'stage[id='+this.player.stage+'] case');
    var poseNames = {};
    
    this.player.xml.find(caseSelector).each(function () {
        $(this).find('state').each(function () {
            var img = $(this).attr('img');
            poseNames[img] = true;
        })
    });
    
    this.poseList.empty().append(Object.keys(poseNames).map(function (img) {
        var elem = document.createElement('option');
        $(elem).attr('value', img);
        return $(elem);
    }));
    
    this.editButton.hide();
    gameDisplays.forEach(function (disp) {
        disp.devModeController.newResponseButton.hide();
    });
}

DevModeDialogueBox.prototype.exitEditMode = function () {
    KEYBINDINGS_ENABLED = true;
    this.editField.val('');
    this.currentStateDialogue = '';
    this.respondingToPlayer = null;
    this.container.attr('data-editing', null);
    this.poseFieldContainer.hide();
    
    this.player.chosenState.expandDialogue(this.player, this.player.currentTarget);
    gameDisplays[this.player.slot-1].updateText(this.player);
    gameDisplays[this.player.slot-1].updateImage(this.player);
    
    this.editButton.show();
    gameDisplays.forEach(function (disp) {
        disp.devModeController.newResponseButton.show();
    });
}

DevModeDialogueBox.prototype.cancelEdit = function () {
    this.player.chosenState.rawDialogue = this.currentState.text;
    this.player.chosenState.image = this.currentState.pose;
    
    this.exitEditMode();
}


DevModeDialogueBox.prototype.saveEdits = function () {
    if (this.editingState) {
        // Log an edited state.
        var stateID = this.player.chosenState.id;
        
        this.player.editLog.push({
            'type': 'edit',
            'stage': this.player.stage,
            'tag': this.currentCase.tag,
            'oldState': this.currentState,
            'state': {'text': this.currentStateDialogue, 'pose': this.currentStatePose}
        });
        
        this.player.xml.find('state[dev-id="'+stateID+'"]').each(function (idx, elem) {
            $(elem).html(this.currentStateDialogue);
            $(elem).attr('img', this.currentStatePose);
        }.bind(this));
    } else {
        var editEntry = {
            'type': 'new',
            'intent': 'generic',
            'phaseTarget': null,
            'responseTarget': null,
            'stage': this.player.stage,
            'state': {'text': this.currentStateDialogue, 'image': this.currentStatePose},
            'suggestedTag': this.player.lastUpdateTags[0],
        };
        
        function playerStateInfo (player) {
            var curState = player.chosenState;
            
            var info = {
                'id': player.id,
                'stage': player.stage,
            }
            
            if (curState) {
                var curCase = curState.parentCase;
                    
                info.case = curCase.serializeConditions();
                info.state = {'text': curState.rawDialogue, 'image': curState.image};
                
                if (curState.marker) {
                    info.state.marker = {
                        'name': curState.marker.name,
                        'perTarget': curState.marker.perTarget,
                    };
                    
                    var markerName = curState.marker.name;
                    if (curState.marker.perTarget) {
                        markerName = getTargetMarker(curState.marker.name, player.currentTarget);
                    }
                    
                    info.state.marker.value = player.markers[markerName];
                }
            }
            
            return info;
        }
        
        if (this.respondingToPlayer) {
            editEntry.intent = 'response';
            editEntry.responseTarget = playerStateInfo(this.respondingToPlayer);
            
            if (this.player.currentTarget) {
                editEntry.phaseTarget = playerStateInfo(this.player.currentTarget);
            }
        }
        
        this.player.editLog.push(editEntry);
    }
    
    this.player.chosenState.rawDialogue = this.currentStateDialogue;
    this.player.chosenState.image = this.currentStatePose;
    
    this.exitEditMode();
}

DevModeDialogueBox.prototype.editCurrentState = function () {
    if (!this.player || !this.player.chosenState) return;
    
    this.currentStateDialogue = this.player.chosenState.rawDialogue;
    this.statusLine.text("Editing");
    this.editingState = true;
    
    this.startEditMode();
}

DevModeDialogueBox.prototype.newResponseState = function (respondTo) {
    if (!this.player) return;
    
    this.currentStateDialogue = "";
    this.respondingToPlayer = respondTo;
    this.editingState = false;
    
    if (!respondTo) {
        this.statusLine.text("New Generic");
        // this.statusLine.text("Generic: "+this.genericResponseTag);
    } else {
        this.statusLine.text("Response: "+this.respondingToPlayer.label);
        // this.statusLine.text("Targeted: "+this.targetedResponseTag);
    }
    
    this.startEditMode();
}

DevModeDialogueBox.prototype.startNewResponse = function () {
    if (!this.player || !devModeActive) return;
    
    if (devModeTarget === this.player.slot) {
        // create a new generic state:
        this.newResponseState(null);
    } else {
        // delegate to the dev-target dialogue box controller:
        gameDisplays[devModeTarget-1].devModeController.newResponseState(this.player);
    }
}

DevModeDialogueBox.prototype.togglePreview = function () {
    if (this.container.attr('data-editing') !== 'preview') {
        var expanded = expandDialogue(this.currentStateDialogue, this.player, this.player.currentTarget, null);
        this.dialogueArea.html(fixupDialogue(expanded));
        this.poseFieldContainer.hide();
        
        this.container.attr('data-editing', 'preview');
    } else {
        this.container.attr('data-editing', 'entry');
    }
};
