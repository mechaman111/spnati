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
    
    this.editButton.click(this.editCurrentState.bind(this));
    this.newResponseButton.click(this.startNewResponse.bind(this));
    
    this.previewButton.click(this.togglePreview.bind(this));
    this.cancelButton.click(this.cancelEditMode.bind(this));
    this.confirmButton.click(this.saveEdits.bind(this));
    
    this.editField.on('change', this.updateEditField.bind(this));
    
    this.player = null;
    this.editingState = false;
    this.respondingToPlayer = null;
    this.currentStateDialogue = null;
}

DevModeDialogueBox.prototype.update = function (player) {
    this.player = player;
    
    if (!player) return;
    
    this.currentState = player.chosenState;
    
    if (devModeActive && devModeTarget === player.slot) {
        this.container.attr('data-dev-target', 'true');
    } else {
        this.container.attr('data-dev-target', null);
    }
}

DevModeDialogueBox.prototype.updateEditField = function () {
    this.currentStateDialogue = this.editField.val();
}

DevModeDialogueBox.prototype.startEditMode = function () {
    KEYBINDINGS_ENABLED = false;
    this.editField.val(this.currentStateDialogue).attr('placeholder', this.player.chosenState.rawDialogue);
    this.container.attr('data-editing', 'entry');
    
    this.editButton.hide();
    gameDisplays.forEach(function (disp) {
        disp.devModeController.newResponseButton.hide();
    });
}

DevModeDialogueBox.prototype.cancelEditMode = function () {
    KEYBINDINGS_ENABLED = true;
    this.editField.val('');
    this.currentStateDialogue = '';
    this.respondingToPlayer = null;
    this.container.attr('data-editing', null);
    
    this.player.chosenState.expandDialogue(this.player, this.player.currentTarget);
    gameDisplays[this.player.slot-1].update(this.player);
    
    this.editButton.show();
    gameDisplays.forEach(function (disp) {
        disp.devModeController.newResponseButton.show();
    });
}

DevModeDialogueBox.prototype.saveEdits = function () {
    if (this.editingState) {
        var stateID = this.player.chosenState.id;
        
        this.player.editLog.push({'type': 'edit', 'stateID': stateID, 'text': this.currentStateDialogue});
        
        this.player.chosenState.rawDialogue = this.currentStateDialogue;
        this.player.chosenState.expandDialogue(this.player, this.player.currentTarget);
        
        this.player.xml.find('state[dev-id="'+stateID+'"]').each(function (idx, elem) {
            $(elem).html(this.currentStateDialogue);
        }.bind(this));
        
        gameDisplays[this.player.slot-1].update(this.player);
    } else {
        // TODO: implement this
    }
    
    this.cancelEditMode();
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
        this.statusLine.text("Generic Dialogue");
    } else {
        this.statusLine.text("Responding to "+respondTo.label);
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
        
        this.container.attr('data-editing', 'preview');
    } else {
        this.container.attr('data-editing', 'entry');
    }
};
