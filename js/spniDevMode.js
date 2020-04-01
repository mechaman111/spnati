var devExportModal = $('#dev-export-modal');
var devExportField = $('#export-edit-log');

var devSelectorButtons = [
    $('#dev-select-button-1'),
    $('#dev-select-button-2'),
    $('#dev-select-button-3'),
    $('#dev-select-button-4'),
];

var devModeActive = false;
var devModeTarget = 0;

function showDevExportModal () {
    if (!devModeActive || !devModeTarget) return;
    
    var editLog = players[devModeTarget].editLog || [];
    var serialized = JSON.stringify(editLog);
    
    devExportField.val(serialized);
    devExportModal.modal('show');
}

function setDevSelectorVisibility (visible) {
    if (visible) {
        $('.dev-select-button').show();
        if (devModeActive && devModeTarget) devSelectorButtons[devModeTarget-1].addClass('active');
    } else {
        $('.dev-select-button').removeClass('active').hide();
    }
}

function setDevModeTarget (target) {
    $('.dev-select-button').removeClass('active');
    
    if (!target || (devModeActive && target === devModeTarget)) {
        devModeActive = false;
        devModeTarget = 0;
        $gameScreen.removeClass('dev-mode');
    } else {
        devModeActive = true;
        devModeTarget = target;
        $gameScreen.addClass('dev-mode');
        $('#dev-select-button-'+target).addClass('active');
        
        players.forEach(function (p) {
            if (p !== humanPlayer && !p.devModeInitialized) p.initDevMode();
        })
    }
    
    for (var i=1;i<players.length;i++) {
        gameDisplays[i-1].devModeController.update(players[i]);
    }
}

Opponent.prototype.initDevMode = function () {
    /* Assign a unique ID number to each state, for later lookup. */
    var curStateID = 0;
    var stateIndex = {};
    
    this.xml.find('case').each(function (caseIdx, elem) {
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
    
    this.devModeInitialized = true;
    this.stateIndex = stateIndex;
    this.originalXml = $(this.xml).clone();
    this.editLog = [];
}


function DevModeDialogueBox(gameBubbleElem) {
    this.container = gameBubbleElem;
    this.poseFieldContainer = gameBubbleElem.find('.edit-pose-row');
    
    this.dialogueArea = gameBubbleElem.find('.dialogue');
    this.editField = gameBubbleElem.find('.dialogue-edit-entry');
    this.poseField = gameBubbleElem.find('.dialogue-pose-entry');
    this.statusLine = gameBubbleElem.find('.dialogue-edit-status');
    this.poseList = gameBubbleElem.find('.edit-pose-list');
    
    this.editButton = gameBubbleElem.find('.dialogue-edit-button');
    this.newResponseButton = gameBubbleElem.find('.dialogue-respond-button');
    this.confirmButton = gameBubbleElem.find('.edit-confirm');
    this.cancelButton = gameBubbleElem.find('.edit-cancel');
    this.previewButton = gameBubbleElem.find('.edit-preview');
    this.poseButton = gameBubbleElem.find('.edit-pose');
    
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
    
    /* The player this object concerns itself with. */
    this.player = null;
    
    /* The player currently targeted for a new response line, if any.
     * (If null, indicates that a new generic line is being written.)
     */
    this.respondingToPlayer = null;
    
    /* If true, then a preexisting State is being edited. 
     * Affects handling in :saveEdits().
     */
    this.editingState = false;
    
    /* An object with 'text' and 'image' fields for storing state data
     * during editing.
     * (editData.text = current entered dialogue text)
     * (editData.image = current selected character pose)
     */
    this.editData = null;
    
    /* Contains the 'initial' dialogue/pose for the character prior to editing.
     * _Usually_ the same as player.chosenState, but not always!
     */
    this.currentState = null;
    
    /* If true, currentState contains 'new' data that isn't reflected in the original XML.
     * This indicates to :saveEdits() to modify edit log entries instead of XML data.
     */
    this.currentStateModified = false;
    
    /* The parentCase of the last played State for this player. */
    this.currentCase = null;
}

DevModeDialogueBox.prototype.update = function (player) {
    this.player = player;
    if (!player) return;
    
    this.currentCase = player.chosenState.parentCase;
    this.currentState = {'text': player.chosenState.rawDialogue, 'image': player.chosenState.image};
    this.currentStateModified = false;
    
    /* Reset all editing state... */
    if (this.container.attr('data-editing')) {
        this.cancelEdit();
    }
    
    /* Show the 'edit' button as appropriate. */
    if (devModeActive && devModeTarget === player.slot) {
        this.container.attr('data-dev-target', 'true');
        this.editButton.show();
    } else {
        this.container.attr('data-dev-target', null);
        this.editButton.hide();
    }
}

DevModeDialogueBox.prototype.updateEditField = function () {
    this.editData.text = this.editField.val();
}

DevModeDialogueBox.prototype.updatePoseField = function () {
    this.editData.image = this.poseField.val();
    this.player.chosenState.image = this.editData.image;
    
    gameDisplays[this.player.slot-1].updateImage(this.player);
}

DevModeDialogueBox.prototype.startEditMode = function (start_text, status_line) {
    this.editData = {
        'text': start_text,
        'image': this.currentState.image,
    }
    
    /* Set up the text and pose entry elements: */
    this.container.attr('data-editing', 'entry');
    this.editField.val(this.editData.text).attr('placeholder', this.currentState.text);
    this.poseField.val(this.editData.image);
    this.statusLine.text(status_line);
    
    /* Populate the pose entry autocomplete list: */
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
    
    /* Hide the 'edit' and 'response' buttons: */
    this.editButton.hide();
    gameDisplays.forEach(function (disp) {
        disp.devModeController.newResponseButton.hide();
    });
}

DevModeDialogueBox.prototype.exitEditMode = function () {
    this.editData = null;
    this.respondingToPlayer = null;
    
    /* Hide the data entry elements: */
    this.container.attr('data-editing', null);
    this.poseFieldContainer.hide();
    this.editField.val('');
    
    /* Reset the character's shown dialogue text and image: */
    this.player.chosenState.rawDialogue = this.currentState.text;
    this.player.chosenState.image = this.currentState.image;
    
    this.player.chosenState.expandDialogue(this.player, this.player.currentTarget);
    this.player.chosenState.selectImage(this.player.stage);

    gameDisplays[this.player.slot-1].updateText(this.player);
    gameDisplays[this.player.slot-1].updateImage(this.player);
    
    /* Show the 'edit' and 'response' buttons: */
    this.editButton.show();
    gameDisplays.forEach(function (disp) {
        disp.devModeController.newResponseButton.show();
    });
}

DevModeDialogueBox.prototype.cancelEdit = function () {
    this.exitEditMode();
}

DevModeDialogueBox.prototype.saveEdits = function () {
    if (this.editingState) {
        /* Handle edited states: */
                
        if (this.currentStateModified) {
            /* Update the last edit log entry to effect this change: */
            var idx = this.player.editLog.length - 1;
            if (idx < 0) return;
            
            this.player.editLog[idx].state = {'text': this.editData.text, 'image': this.editData.image};
        } else {
            /* Push a new edit log entry... */
            this.player.editLog.push({
                'type': 'edit',
                'stage': this.player.stage,
                'case': this.currentCase.serializeConditions(),
                'oldState': {'text': this.currentState.text, 'image': this.currentState.image},
                'state': {'text': this.editData.text, 'image': this.editData.image}
            });
            
            var stateID = this.player.chosenState.id;
            
            /* Go directly to the XML to update all similar states: */
            this.player.xml.find('state[dev-id="'+stateID+'"]').each(function (idx, elem) {
                $(elem).html(this.editData.text);
                $(elem).attr('img', this.editData.image);
            }.bind(this));
        }
    } else {
        var editEntry = {
            'type': 'new',                                                          // either 'new' or 'edit'
            'intent': 'generic',                                                    // either 'generic' or 'response'
            'responseTarget': null,                                                 // info on the selected response target
            'stage': this.player.stage,                                             // current character stage
            'state': {'text': this.editData.text, 'image': this.editData.image},    // edited state data
            'suggestedTag': this.player.lastUpdateTags[0],                          // a suggested tag for generic lines
        };
        
        /* Dumps relevant edit log data for a given player into a serializable object. */
        function playerStateInfo (player) {
            var curState = player.chosenState;
            
            var info = {
                'id': player.id,
                'stage': player.stage,
                'case': null,
                'state': null,
            }
            
            if (curState) {
                var curCase = curState.parentCase;
                    
                info.case = curCase.serializeConditions();
                info.state = {'text': curState.rawDialogue, 'image': curState.image, 'marker': null};
                
                if (curState.markers) {
                    info.state.markers = curState.markers.map(function (marker) {
                        return marker.serialize(player, player.currentTarget);
                    });
                }
            }
            
            return info;
        }
        
        if (this.respondingToPlayer) {
            editEntry.intent = 'response';
            editEntry.responseTarget = playerStateInfo(this.respondingToPlayer);
        }
        
        this.player.editLog.push(editEntry);
    }
    
    /* Save editData to currentState, which will then get flushed to
     * player.chosenState.
     */
    this.currentState.text = this.editData.text;
    this.currentState.image = this.editData.image;
    this.currentStateModified = true;
    
    this.exitEditMode();
}

DevModeDialogueBox.prototype.editCurrentState = function () {
    if (!this.currentState) return;
    
    this.editingState = true;
    this.startEditMode(this.currentState.text, "Editing");
}

DevModeDialogueBox.prototype.newResponseState = function (respondTo) {
    if (!this.player) return;
    
    this.respondingToPlayer = respondTo;
    this.editingState = false;

    this.startEditMode(
        "",
        !respondTo ? "New Generic" : "Response: "+this.respondingToPlayer.label
    );
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
        /* Update the displayed dialogue text and image for this player: */
        this.player.chosenState.rawDialogue = this.editData.text;
        this.player.chosenState.image = this.editData.image;
        
        this.player.chosenState.expandDialogue(this.player, this.player.currentTarget);
        this.player.chosenState.selectImage(this.player.stage);
        
        gameDisplays[this.player.slot-1].updateText(this.player);
        gameDisplays[this.player.slot-1].updateImage(this.player);
        
        this.poseFieldContainer.hide();
        this.container.attr('data-editing', 'preview');
    } else {
        this.container.attr('data-editing', 'entry');
    }
};
