/********************************************************************************
 This file contains code related to the Pose Engine,
 as well as code related to displaying tables for selection and the main game.
 ********************************************************************************/

/* NOTE: These are basically the same as epilogue engine sprites.
 * There's a _lot_ of common code here that can probably be merged.
 */
function PoseSprite(id, src, onload, args) {
    this.id = id;
    this.player = args.player;
    this.src = args.player.folder + src;
    this.x = args.x || 0;
    this.y = args.y || 0;
    this.z = args.z || 'auto';
    this.height = args.height || '100%';
    this.width = args.width;
    this.scaleX = args.scaleX || 1;
    this.scaleY = args.scaleY || 1;
    this.rotation = args.rotation || 0;
    this.alpha = args.alpha;
    this.pivotX = args.pivotX;
    this.pivotY = args.pivotY;
    
    this.vehicle = document.createElement('div');
    this.vehicle.id = id;
    
    this.img = document.createElement('img');
    this.img.onload = this.img.onerror = onload;
    this.img.src = this.src;
    
    this.vehicle.appendChild(this.img);
    
    if (this.alpha === undefined) {
        this.alpha = 100;
    }
    
    if (this.pivotX || this.pivotY) {
        this.pivotX = this.pivotX || "center";
        this.pivotY = this.pivotY || "center";
        $(this.img).css("transform-origin", pivotX + " " + pivotY);
    }
    
    $(this.vehicle).css("z-index", this.z);
}

PoseSprite.prototype.draw = function() {
    $(this.vehicle).css({
      "position": "absolute",
      "left": "50%",
      "transform":  "translateX(-50%) translateX("+this.x+"px) translateY(" + this.y + "px)",
      "transform-origin": "top left",
      "opacity": this.alpha / 100,
      "height": '100%'
    });
    
    $(this.img).css({
      "transform": "rotate(" + this.rotation + "deg) scale(" + this.scaleX + ", " + this.scaleY + ")",
      'height': this.height,
      'width': this.width
    });
}


function PoseAnimation (targetSprite, args) {
    this.target = targetSprite;
    this.elapsed = 0;
    this.looped = args.looped || false;
    this.keyframes = args.keyframes.sort(function (kf1, kf2) {
        if (kf1.time === kf2.time) return 0;
        return (kf1.time < kf2.time) ? -1 : 1;
    });
    
    var totalTime = 0;
    this.keyframes.forEach(function (kf) {
        kf.startTime = totalTime;
        totalTime = kf.time;
    });
    
    console.log(this.keyframes);
    
    this.duration = this.keyframes[this.keyframes.length-1].time;
    this.delay = args.delay || 0;
    this.interpolation = args.interpolation || 'none';
}

PoseAnimation.prototype.isComplete = function () {
    return (this.elapsed - this.delay) >= this.duration;
}

PoseAnimation.prototype.update = function (dt) {
    this.elapsed += dt;
    
    if (this.looped && this.isComplete()) { this.elapsed -= this.duration; }
    var t = (this.elapsed - this.delay);
    if (t < 0) return;
    
    // Find current keyframe pair and update
    for (var i=this.keyframes.length-1;i>=0;i--) {
        var frame = this.keyframes[i];
        if (t <= frame.startTime) continue;

        var lastFrame = (i > 0) ? this.keyframes[i-1] : frame;
        var progress = (t - frame.startTime) / (frame.time - frame.startTime);
        progress = (t <= 0) ? 0 : Math.min(1, Math.max(0, progress));
        
        this.updateSprite(lastFrame, frame, progress, i);
        return;
    }
}

// Borrowed heavily from spniEpilogue
PoseAnimation.prototype.interpolate = function (prop, last, next, t, idx) {
    var current = this.target[prop];
    var start = last[prop];
    var end = next[prop];
    
    if (typeof start === "undefined" || isNaN(start) || typeof end === "undefined" || isNaN(end)) {
      return;
    }
    
    var mode = this.interpolation;
    this.target[prop] = interpolationModes[mode](prop, start, end, t, this.keyframes, idx);   
}

PoseAnimation.prototype.updateSprite = function (fromFrame, toFrame, t, idx) {
    this.interpolate("x", fromFrame, toFrame, t, idx);
    this.interpolate("y", fromFrame, toFrame, t, idx);
    this.interpolate("rotation", fromFrame, toFrame, t, idx);
    this.interpolate("scalex", fromFrame, toFrame, t, idx);
    this.interpolate("scaley", fromFrame, toFrame, t, idx);
    this.interpolate("alpha", fromFrame, toFrame, t, idx);
    this.target.draw();
}


function Pose(poseDef) {
    this.id = poseDef.id;
    this.player = poseDef.player;
    this.sprites = {};
    this.totalSprites = 0;
    this.animations = [];
    this.loaded = false;
    this.n_loaded = 0;
    this.onLoadComplete = null;
    this.lastUpdateTS = null;
    this.active = false;
    
    var container = document.createElement('div');
    $(container).addClass("opponent-image custom-pose").css({
        "position": "relative",
        'z-index': -1
    });
    this.container = container;
    
    poseDef.sprites.forEach(function (def) {
        var sprite = new PoseSprite(def.id, def.src, this.onSpriteLoaded.bind(this), def);
        this.sprites[def.id] = sprite
        this.totalSprites++;
        
        container.appendChild(sprite.vehicle);
    }.bind(this));
    
    poseDef.animations.forEach(function (def) {
        var target = this.sprites[def.id];
        if (!target) return;
        
        var anim = new PoseAnimation(target, def);
        this.animations.push(anim);
    }.bind(this));
}

Pose.prototype.onSpriteLoaded = function() {
    this.n_loaded++;
    if (this.n_loaded >= this.totalSprites) {
        this.loaded = true;
        
        if (this.onLoadComplete) { return this.onLoadComplete(); }
    }
}

Pose.prototype.update = function (timestamp) {
    if (this.animations.length === 0) return;
    
    if (this.lastUpdateTS === null) { this.lastUpdateTS = timestamp; }
    var dt = timestamp - this.lastUpdateTS;

    for (var i=0;i<this.animations.length;i++) {
        this.animations[i].update(dt);
    }
    
    this.lastUpdateTS = timestamp;
}

Pose.prototype.draw = function() {
    for (key in this.sprites) {
        this.sprites[key].draw();
    }
}


function xmlToObject($xml) {
    var targetObj = {};
    $.each($xml.attributes, function (i, attr) {
      var name = attr.name.toLowerCase();
      var value = attr.value;
      targetObj[name] = value;
    });
    
    return targetObj;
}


/* Common function for parsing sprite and directive definitions. */
function parseSpriteDefinition ($xml, player) {
    var targetObj = xmlToObject($xml);
  
    //properties needing special handling
    if (targetObj.alpha) { targetObj.alpha = parseFloat(targetObj.alpha, 10); }
    targetObj.zoom = parseFloat(targetObj.zoom, 10);
    targetObj.rotation = parseFloat(targetObj.rotation, 10);
    if (targetObj.scale) {
        targetObj.scalex = targetObj.scaley = targetObj.scale;
    }
    targetObj.scalex = parseFloat(targetObj.scalex, 10);
    targetObj.scaley = parseFloat(targetObj.scaley, 10);
    
    targetObj.player = player;
    
    return targetObj;
}

function parseKeyframeDefinition($xml) {
    var targetObj = parseSpriteDefinition($xml);
    targetObj.time = parseFloat(targetObj.time) * 1000;
    
    return targetObj;
}

function parseDirective ($xml) {
    var targetObj = xmlToObject($xml);
    
    if (targetObj.type === 'animation') {
        // Keyframe / interpolated animation
        targetObj.keyframes = [];
        $($xml).find('keyframe').each(function (i, elem) {
            targetObj.keyframes.push(parseKeyframeDefinition(elem));
        });
    }
    
    return targetObj;
}


function PoseDefinition ($xml, player) {
    this.id = $xml.attr('id').trim();
    
    this.sprites = [];
    $xml.find('sprite').each(function (i, elem) {
        this.sprites.push(parseSpriteDefinition(elem, player));
    }.bind(this));
    
    this.animations = [];
    $xml.find('directive').each(function (i, elem) {
        var directive = parseDirective(elem);
        if (directive.type === 'animation') {
            this.animations.push(directive);
        }
    }.bind(this));
    
    this.player = player;
}

PoseDefinition.prototype.getUsedImages = function(stage) {
    var baseFolder = this.player.folders ? this.player.getByStage(this.player.folders, stage) : this.player.folder;
    var imageSet = {};
    
    this.sprites.forEach(function (sprite) {
        imageSet[baseFolder+sprite.src] = true;
    });
    
    return Object.keys(imageSet);
}


function OpponentDisplay(slot, bubbleElem, dialogueElem, simpleImageElem, imageArea, labelElem) {
    this.slot = slot;
    
    this.bubble = bubbleElem;
    this.dialogue = dialogueElem;
    this.simpleImage = simpleImageElem;
    this.imageArea = imageArea;
    this.label = labelElem;
    
    this.animCallbackID = window.requestAnimationFrame(this.loop.bind(this));
}

OpponentDisplay.prototype.hideBubble = function () {
    this.dialogue.html("");
    this.bubble.hide();
}

OpponentDisplay.prototype.clearPose = function () {
    this.pose = null;
    this.simpleImage.hide();
    this.imageArea.children('.custom-pose').remove();
}

OpponentDisplay.prototype.drawPose = function (pose) {
    this.clearPose();
    
    if (typeof(pose) === 'string') {
        this.simpleImage.attr('src', pose).show();
    } else if (pose instanceof Pose) {
        this.pose = pose;
        this.imageArea.append(pose.container);
        pose.draw();
    }
}

OpponentDisplay.prototype.update = function(player) {
    if (!player) {
        this.hideBubble();
        this.clearPose();
        return;
    }
    
    if (!player.chosenState) {
        /* hide their dialogue bubble */
        this.hideBubble();
        return;
    }
    
    var chosenState = player.chosenState;
    
    /* update dialogue */
    this.dialogue.html(fixupDialogue(chosenState.dialogue));
    
    /* update image */
    if (chosenState.image.startsWith('custom:')) {
        var key = chosenState.image.split(':', 2)[1];
        var poseDef = player.poses[key];
        if (poseDef) {
            this.drawPose(new Pose(poseDef));
        } else {
            this.clearPose();
        }
    } else {
        this.pose = player.folder + chosenState.image;
        this.drawPose(this.pose);
    }
    
    /* update label */
    this.label.html(player.label.initCap());

    /* check silence */
    if (!chosenState.dialogue) {
        this.hideBubble();
    } else {
        this.bubble.show();
        this.bubble.children('.dialogue-bubble').attr('class', 'dialogue-bubble arrow-'+chosenState.direction);
        bubbleArrowOffsetRules[this.slot-1][0].style.left = chosenState.location;
        bubbleArrowOffsetRules[this.slot-1][1].style.top = chosenState.location;
    }
    
    if (showDebug) {
        appendRepeats(player);
    }
}

OpponentDisplay.prototype.loop = function (timestamp) {
    this.animCallbackID = window.requestAnimationFrame(this.loop.bind(this));
    
    if (!this.pose) return;
    this.pose.update(timestamp);
}


function GameScreenDisplay (slot) {
    OpponentDisplay.call(
        this,
        slot,
        $('#game-bubble-'+slot),
        $('#game-dialogue-'+slot),
        $('#game-image-'+slot),
        $('#game-image-area-'+slot),
        $('#game-name-label-'+slot)
    );
    
    this.opponentArea = $('#game-opponent-area-'+slot);
}
GameScreenDisplay.prototype = Object.create(OpponentDisplay.prototype);
GameScreenDisplay.prototype.constructor = GameScreenDisplay;

GameScreenDisplay.prototype.reset = function (player) {
    if (player) {
        this.opponentArea.show();
        this.imageArea.css('height', player.scale + '%');
        this.label.css({"background-color" : clearColour});
        clearHand(i);
    } else {
        this.opponentArea.hide();
        this.bubble.hide();
    }
}


/* Wraps logic for handling the Main Select screen displays. */
function MainSelectScreenDisplay (slot) {
    OpponentDisplay.call(this, 
        slot,
        $('#select-bubble-'+slot),
        $('#select-dialogue-'+slot),
        $('#select-image-'+slot),
        $('#select-image-area-'+slot),
        $('#select-name-label-'+slot)
    );
    
    this.selectButton = $("#select-slot-button-"+slot);
}

MainSelectScreenDisplay.prototype = Object.create(OpponentDisplay.prototype);
MainSelectScreenDisplay.prototype.constructor = MainSelectScreenDisplay;

MainSelectScreenDisplay.prototype.update = function (player) {
    if (!player) {
        this.hideBubble();
        this.clearPose();
        
        /* change the button */
        this.selectButton.html("Select Opponent");
        this.selectButton.removeClass("smooth-button-red");
        this.selectButton.addClass("smooth-button-green");
        return;
    }
    
    if (!player.isLoaded()) {
        this.hideBubble();
        this.clearPose();
        
        this.label.html(player.label.initCap());
        this.selectButton.attr('disabled', true).html('Loading...');
    } else {
        OpponentDisplay.prototype.update.call(this, player);
        
        this.selectButton.attr('disabled', false).html("Remove Opponent");
        this.selectButton.removeClass("smooth-button-green");
        this.selectButton.addClass("smooth-button-red");
        
        if (typeof(this.pose) === "string") {
            this.simpleImage.one('load', function() {
                this.bubble.show();
                this.simpleImage.css('height', player.scale + '%').show();
            }.bind(this));
        } else {
            this.pose.onLoadComplete = function () {
                this.bubble.show();
                this.imageArea.css('height', player.scale + '%').show();
            }.bind(this);
        }
        
    }
}



/* Handles common logic for displaying opponents in the group and individual displays. */
function OpponentPickerDisplay (slot, id_base) {
    this.nameLabel = $("#"+id_base+"-name-label-"+slot);
    this.prefersLabel = $("#"+id_base+"-prefers-label-"+slot);
    this.sexLabel = $("#"+id_base+"-sex-label-"+slot);
    this.heightLabel = $("#"+id_base+"-height-label-"+slot);
    this.sourceLabel = $("#"+id_base+"-source-label-"+slot);
    this.writerLabel = $("#"+id_base+"-writer-label-"+slot);
    this.artistLabel = $("#"+id_base+"-artist-label-"+slot);
    this.countBox = $("#"+id_base+"-counts-"+slot);
    this.lineCountLabel = $("#"+id_base+"-line-counts-label-"+slot);
    this.poseCountLabel = $("#"+id_base+"-pose-counts-label-"+slot);
    this.descriptionLabel = $("#"+id_base+"-description-label-"+slot);
    this.badge = $("#"+id_base+"-badge-"+slot);
    this.status = $("#"+id_base+"-status-"+slot);
    this.layers = $("#"+id_base+"-layer-"+slot);
    this.costumeSelector = $("#"+id_base+"-costume-select-"+slot);
    this.image = $("#"+id_base+"-image-"+slot);
}

OpponentPickerDisplay.prototype.clear = function() {
    this.nameLabel.html("");
    this.prefersLabel.html("");
    this.sexLabel.html("");
    this.sourceLabel.html("");
    this.writerLabel.html("");
    this.artistLabel.html("");
    this.countBox.css("visibility", "hidden");
    this.descriptionLabel.html("");
    this.badge.hide();
    this.status.hide();
    this.layers.hide();
    this.image.hide();
    this.costumeSelector.hide();
}

OpponentPickerDisplay.prototype.update = function (opponent) {
    this.nameLabel.html(opponent.first + " " + opponent.last);
    this.prefersLabel.html(opponent.label);
    this.sexLabel.html(opponent.gender);
    this.sourceLabel.html(opponent.source);
    this.writerLabel.html(wordWrapHtml(opponent.writer));
    this.artistLabel.html(wordWrapHtml(opponent.artist));
    this.descriptionLabel.html(opponent.description);

    if (EPILOGUE_BADGES_ENABLED && opponent.ending) {
        this.badge.show();
    } else {
        this.badge.hide();
    }

    if (opponent.status) {
        var status_icon_img = 'img/testing-badge.png';
        var status_tooltip = TESTING_STATUS_TOOLTIP;
        
        if (opponent.status === 'offline') {
            status_icon_img = 'img/offline-badge.png';
            status_tooltip = OFFLINE_STATUS_TOOLTIP;
        } else if (opponent.status === 'incomplete') {
            status_icon_img = 'img/incomplete-badge.png';
            status_tooltip = INCOMPLETE_STATUS_TOOLTIP;
        }
    
        this.status.attr({
            'src': status_icon_img,
            'title': status_tooltip,
            'data-original-title': status_tooltip,
        }).show().tooltip({
            'placement': 'left'
        });
    } else {
        this.status.removeAttr('title').removeAttr('data-original-title').hide();
    }

    this.layers.show();
    this.layers.attr("src", "img/layers" + opponent.layers + ".png");

    this.image.attr('src', opponent.folder + opponent.image);
    this,image.css('height', opponent.scale + '%');
    this.image.show();
    
    if (ALT_COSTUMES_ENABLED && opponent.alternate_costumes.length > 0) {
        this.costumeSelector.empty().append($('<option>', {val: '', text: 'Default Skin'}));
        opponent.alternate_costumes.forEach(function (alt) {
            this.costumeSelector.append($('<option>', {
                val: alt_costume.folder,
                text: 'Alternate Skin: '+alt_costume.label
            }));
        }.bind(this));
        this.costumeSelector.show();
    } else {
        this.costumeSelector.hide();
    }
}



function IndividualSelectDisplay (slot) {
    OpponentPickerDisplay.call(slot, "individual");
    this.button = $('#individual-button-'+slot);
}

IndividualSelectDisplay.prototype = Object.create(OpponentPickerDisplay.prototype);
IndividualSelectDisplay.prototype.constructor = IndividualSelectDisplay;

IndividualSelectDisplay.prototype.update = function (opponent) {
    OpponentPickerDisplay.prototype.update.call(this, opponent);
    
    if (opponent.enabled == "true") {
        this.button.html('Select Opponent');
        this.button.attr('disabled', false);
    } else {
        this.button.html('Coming Soon');
        this.button.attr('disabled', true);
    }
}

IndividualSelectDisplay.prototype.clear = function () {
    OpponentPickerDisplay.prototype.clear.call(this);
    this.button.attr('disabled', true);
}
