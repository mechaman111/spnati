/********************************************************************************
 This file contains code related to the Pose Engine,
 as well as code related to displaying tables for selection and the main game.
 ********************************************************************************/

/* NOTE: These are basically the same as epilogue engine sprites.
 * There's a _lot_ of common code here that can probably be merged.
 */
function PoseSprite(id, src, onload, pose, args) {
    this.pose = pose;
    this.id = id;
    this.player = args.player;
    this.src = this.prevSrc = 'opponents/' + src;
    this.x = args.x || 0;
    this.y = args.y || 0;
    this.z = args.z || 'auto';
    this.scalex = args.scalex || 1;
    this.scaley = args.scaley || 1;
    this.skewx = args.skewx || 0;
    this.skewy = args.skewy || 0;
    this.rotation = args.rotation || 0;
    this.alpha = args.alpha;
    this.pivotx = args.pivotx;
    this.pivoty = args.pivoty;
    this.height = args.height || 0;
    this.width = args.width || 0;
    this.delay = args.delay || 0;
    this.elapsed = 0;
    this.parentId = args.parent;
    
    this.vehicle = document.createElement('div');
    this.vehicle.id = id;
    this.pivot = document.createElement('div');
    this.vehicle.appendChild(this.pivot);
    
    this.img = document.createElement('img');
    this.img.className = "sprite";
    this.img.onload = this.img.onerror = function() {
        if (!this.height) this.height = this.img.naturalHeight;
        if (!this.width) this.width = this.img.naturalWidth;
        
        onload(this);
        this.draw();
    }.bind(this);
    this.img.src = this.src.replace('#', this.player.stage);
    
    this.pivot.appendChild(this.img);
    
    if (this.alpha === undefined) {
        this.alpha = 100;
    }
    
    if (this.pivotx || this.pivoty) {
        this.pivotx = this.pivotx || "center";
        this.pivoty = this.pivoty || "center";
        $(this.pivot).css("transform-origin", this.pivotx + " " + this.pivoty);
    }
    
    $(this.vehicle).css("z-index", this.z);
}

PoseSprite.prototype.linkParent = function () {
    if (this.parentId) {
        this.parent = this.pose.sprites[this.parentId];
        this.parent.pivot.appendChild(this.vehicle);
    }
}

PoseSprite.prototype.scaleToDisplay = function(x) {
    return x * this.pose.getHeightScaleFactor();
}

PoseSprite.prototype.update = function (dt) {
    if (this.elapsed < this.delay) {
        this.elapsed += dt;
        if (this.elapsed >= this.delay) {
            this.draw();
        }
    }
}

PoseSprite.prototype.draw = function() {
    var alpha = this.alpha / 100;
    if (this.elapsed < this.delay) {
        alpha = 0;
    }
    var properties = {
        "position": "absolute",
        "left": "50%",
        "top": "0",
        "transform": "translateX(-50%) translateX(" + this.scaleToDisplay(this.x) + "px) translateY(" + this.scaleToDisplay(this.y) + "px)",
        "transform-origin": "top left",
        "opacity": alpha,
        "height": '100%',
    };
    if (this.parent) {
        properties.left = 0;
        properties.transform = "translateX(" + this.scaleToDisplay(this.x) + "px) translateY(" + this.scaleToDisplay(this.y) + "px)";
    }
    $(this.vehicle).css(properties);
    
    if (this.prevSrc !== this.src) {        
        /* Recompute image height/width only _after_ images have loaded. */
        this.img.onload = function () {
            this.height = this.img.naturalHeight;
            this.width = this.img.naturalWidth;
            
            $(this.img).css({
                'height': this.scaleToDisplay(this.height) + "px",
                'width': this.scaleToDisplay(this.width) + "px"
            });
        }.bind(this);
        
        this.img.src = this.prevSrc = this.src;
    } else if (this.img) {
        $(this.img).css({
            'height': this.scaleToDisplay(this.height) + "px",
            'width': this.scaleToDisplay(this.width) + "px"
        });
    }


    $(this.pivot).css({
      "transform": "rotate(" + this.rotation + "deg) scale(" + this.scalex + ", " + this.scaley + ") skew(" + this.skewx + "deg, " + this.skewy + "deg)",
    });
}

PoseSprite.prototype.setWillChangeHints = function (enabled) {
    $(this.vehicle).css('will-change', enabled ? 'transform, opacity' : 'auto');
    $(this.pivot).css('will-change', enabled ? 'transform' : 'auto');
    if (this.img) {
        $(this.img).css('will-change', enabled ? 'height, width' : 'auto');
    }
}


function PoseAnimation (targetSprite, pose, args) {
    this.pose = pose;
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
    
    this.duration = this.keyframes[this.keyframes.length-1].time;
    this.delay = args.delay || 0;
    this.interpolation = args.interpolation || 'none';
    this.ease = args.ease || 'linear';
    this.clamp = args.clamp || 'wrap';
    this.iterations = parseInt(args.iterations, 10) || 0;
}

PoseAnimation.prototype.isComplete = function () {
    var life = this.elapsed - this.delay;
    if (this.looped) {
        return this.iterations > 0 ? life / this.duration >= this.iterations : false;
    }
    return life >= this.duration;
}

PoseAnimation.prototype.update = function (dt) {
    this.elapsed += dt;
    
    var t = (this.elapsed - this.delay);
    if (t < 0) return;
    if (this.duration === 0) {
        t = 1;
    }
    else {
        var easingFunction = this.ease;
        t /= this.duration;
        if (this.looped) {
            t = clampingFunctions[this.clamp](t);
            if (this.isComplete()) {
                t = 1;
            }
        }
        else {
            t = Math.min(1, t);
        }
        t = Animation.prototype.easingFunctions[easingFunction](t)
        t *= this.duration;  
    }
    
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
    if (toFrame.src && t >= 1) {
        this.target.src = toFrame.src;
    }
    else if (fromFrame.src) {
        this.target.src = fromFrame.src;
    }
    
    this.interpolate("x", fromFrame, toFrame, t, idx);
    this.interpolate("y", fromFrame, toFrame, t, idx);
    this.interpolate("rotation", fromFrame, toFrame, t, idx);
    this.interpolate("scalex", fromFrame, toFrame, t, idx);
    this.interpolate("scaley", fromFrame, toFrame, t, idx);
    this.interpolate("skewx", fromFrame, toFrame, t, idx);
    this.interpolate("skewy", fromFrame, toFrame, t, idx);
    this.interpolate("alpha", fromFrame, toFrame, t, idx);
    this.target.draw();
}


function Pose(poseDef, display) {
    this.id = poseDef.id;
    this.player = poseDef.player;
    this.display = display;
    this.sprites = {};
    this.totalSprites = 0;
    this.loaded_sprites = {};
    this.animations = [];
    this.loaded = false;
    this.onLoadComplete = null;
    this.lastUpdateTS = null;
    this.active = false;
    this.baseHeight = poseDef.baseHeight || 1400;
    
    var container = document.createElement('div');
    $(container).addClass("opponent-image custom-pose").css({
        "position": "relative"
    });
    this.container = container;
    
    poseDef.sprites.forEach(function (def) {
        if (def.marker && !checkMarker(def.marker, this.player)) {
            return;
        }
        var sprite = new PoseSprite(def.id, def.src, this.onSpriteLoaded.bind(this), this, def);
        this.sprites[def.id] = sprite
        this.totalSprites++;
        
        container.appendChild(sprite.vehicle);
    }.bind(this));

    for (var id in this.sprites) {
        if (this.sprites.hasOwnProperty(id)) {
            this.sprites[id].linkParent();
        }
    }
    
    poseDef.animations.forEach(function (def) {
        if (def.marker && !checkMarker(def.marker, this.player)) {
          return;
        }
        var target = this.sprites[def.id];
        if (!target) return;
        
        var anim = new PoseAnimation(target, this, def);
        this.animations.push(anim);
    }.bind(this));
}

Pose.prototype.getHeightScaleFactor = function() {
    return this.display.imageAreaHeight / this.baseHeight;
}

Pose.prototype.onSpriteLoaded = function(sprite) {
    if (this.loaded_sprites[sprite.id]) { return; }
    
    this.loaded_sprites[sprite.id] = true;
    var n_loaded = Object.keys(this.loaded_sprites).length;
    
    if (n_loaded >= this.totalSprites && !this.loaded) {
        this.loaded = true;
        if (this.onLoadComplete) { return this.onLoadComplete(); }
    }
}

Pose.prototype.update = function (timestamp) {    
    if (this.lastUpdateTS === null) { this.lastUpdateTS = timestamp; }
    var dt = timestamp - this.lastUpdateTS;

    for (var id in this.sprites) {
        if (this.sprites.hasOwnProperty(id)) {
            this.sprites[id].update(dt);
        }
    }

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

Pose.prototype.needsAnimationLoop = function () {
    if (this.animations.some(function (a) { return a.looped || !a.isComplete(); })) {
        return true;
    }

    for (var id in this.sprites) {
        if (this.sprites.hasOwnProperty(id) && this.sprites[id].elapsed < this.sprites[id].delay) {
            return true;
        }
    }

    return false;
}

Pose.prototype.setWillChangeHints = function (enabled) {
    for (key in this.sprites) {
        if (this.sprites.hasOwnProperty(key)) {
            this.sprites[key].setWillChangeHints(enabled);
        }
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
    } else {
        targetObj.scalex = parseFloat(targetObj.scalex, 10);
        targetObj.scaley = parseFloat(targetObj.scaley, 10);
    }
    
    targetObj.skewx = parseFloat(targetObj.skewx, 10);
    targetObj.skewy = parseFloat(targetObj.skewy, 10);
    targetObj.x = parseFloat(targetObj.x, 10);
    targetObj.y = parseFloat(targetObj.y, 10);
    targetObj.delay = parseFloat(targetObj.delay) * 1000 || 0;
    
    targetObj.player = player;
    
    return targetObj;
}

function parseKeyframeDefinition($xml) {
    var targetObj = parseSpriteDefinition($xml);
    targetObj.time = parseFloat(targetObj.time) * 1000;
    if (targetObj.src) {
        targetObj.src = "opponents/" + targetObj.src;
    }
    
    return targetObj;
}

function parseDirective ($xml) {
    var targetObj = xmlToObject($xml);
    
    if (targetObj.type === 'animation') {
        // Keyframe / interpolated animation
        targetObj.keyframes = [];
        targetObj.delay = parseFloat(targetObj.delay) * 1000 || 0;
        targetObj.looped = targetObj.looped || targetObj.loop;
        $($xml).find('keyframe').each(function (i, elem) {
            targetObj.keyframes.push(parseKeyframeDefinition(elem));
        });
    } else if (targetObj.type === 'sequence') {
        // Sequential frame sequence
        targetObj.frameTime = parseFloat(targetObj.frametime);
        targetObj.delay = parseFloat(targetObj.delay) || 0;
        targetObj.frames = [];
        $($xml).find('animFrame').each(function (i, elem) {
            targetObj.frames.push(xmlToObject(elem));
        });
    }
    
    return targetObj;
}


function PoseDefinition ($xml, player) {
    this.id = $xml.attr('id').trim();
    this.baseHeight = $xml.attr('baseHeight');
    
    this.sprites = [];
    $xml.find('sprite').each(function (i, elem) {
        this.sprites.push(parseSpriteDefinition(elem, player));
    }.bind(this));
    
    this.animations = [];
    $xml.find('directive').each(function (i, elem) {
        var directive = parseDirective(elem);
        if (directive.type === 'animation') {
            this.addAnimation(directive);
        } else if (directive.type === 'sequence') {
            // Convert the sequence into a set of Animation objects.
            var curDelay = directive.delay;
            var totalTime = directive.frameTime * directive.frames.length;
            
            directive.frames.forEach(function (frame) {
                this.animations.push({
                    type: 'animation',
                    id: frame.id,
                    looped: directive.looped || directive.loop,
                    interpolation: 'none',
                    delay: curDelay * 1000,
                    keyframes: [
                        {time: 0, alpha: 100},
                        {time: directive.frameTime*1000, alpha:0},
                        {time: totalTime*1000, alpha:0}
                    ]
                });
                
                curDelay += directive.frameTime;
            }.bind(this));
        }
    }.bind(this));
    
    this.player = player;
}

//This is pretty much the same thing as spniEpilogue's addDirectiveToScene
PoseDefinition.prototype.addAnimation = function (directive) {
    if (directive.keyframes.length > 1) {
        //first split the properties into buckets of frame indices where they appear
        var propertyMap = {};
        for (var i = 0; i < directive.keyframes.length; i++) {
            var frame = directive.keyframes[i];
            for (var j = 0; j < animatedProperties.length; j++) {
                var property = animatedProperties[j];
                if (frame.hasOwnProperty(property) && !Number.isNaN(frame[property])) {
                    if (!propertyMap[property]) {
                        propertyMap[property] = [];
                    }
                    propertyMap[property].push(i);
                }
            }
        }

        //next create directives for each combination of frames
        var directives = {};
        for (var prop in propertyMap) {
            var key = propertyMap[prop].join(',');
            var workingDirective = directives[key];
            if (!workingDirective) {
                //shallow copy the directive
                workingDirective = {};
                for (var srcProp in directive) {
                    if (directive.hasOwnProperty(srcProp)) {
                        workingDirective[srcProp] = directive[srcProp];
                    }
                }
                workingDirective.keyframes = [];
                directives[key] = workingDirective;
                this.animations.push(workingDirective);
            }
            var lastStart = 0;
            for (var i = 0; i < propertyMap[prop].length; i++) {
                var srcFrame = directive.keyframes[propertyMap[prop][i]];
                var targetFrame;
                if (workingDirective.keyframes.length <= i) {
                    //shallow copy the frame minus the animatable properties
                    targetFrame = {};
                    for (var srcProp in srcFrame) {
                        if (srcFrame.hasOwnProperty(srcProp)) {
                            targetFrame[srcProp] = srcFrame[srcProp];
                        }
                    }
                    for (var j = 0; j < animatedProperties.length; j++) {
                        var property = animatedProperties[j];
                        delete targetFrame[property];
                    }

                    targetFrame.startTime = lastStart;
                    workingDirective.keyframes.push(targetFrame);
                    lastStart = srcFrame.time;
                }
                else {
                    targetFrame = workingDirective.keyframes[i];
                }
                targetFrame[prop] = srcFrame[prop];
            }
        }
    }
    else {
        this.animations.push(directive);
    }
}

PoseDefinition.prototype.getUsedImages = function(stage) {
    var baseFolder = 'opponents/';
    var imageSet = {};
    
    this.sprites.forEach(function (sprite) {
        imageSet[baseFolder+sprite.src] = true;
    });
    this.animations.forEach(function (animation) {
        animation.keyframes.forEach(function (keyframe) {
            if (keyframe.src) {
                imageSet[keyframe.src] = true;
            }
        });
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
    this.animCallbackID = undefined;

    this.imageAreaHeight = this.imageArea.height();

    this.resizeObserver = new ResizeObserver(function (entries) {
        if (entries[0].contentBoxSize) {
            if (Array.isArray(entries[0].contentBoxSize)) { // Chrome 84
                this.imageAreaHeight = entries[0].contentBoxSize[0].blockSize;
            } else {
                this.imageAreaHeight = entries[0].contentBoxSize.blockSize;
            }
        } else {
            this.imageAreaHeight = entries[0].contentRect.height;
        }
        this.onResize();
    }.bind(this));

    this.resizeObserver.observe(this.imageArea[0]);
}

OpponentDisplay.prototype.rescaleSimplePose = function (base_scale) {
    /* Required to properly scale oddly-sized simple poses. */
    var nh = this.simpleImage[0].naturalHeight;
    if (nh <= 1400) {
        this.simpleImage.css("height", base_scale+"%");
    } else {
        var sf = nh / 1400;
        this.simpleImage.css("height", "calc("+base_scale+"% * "+sf+")");
    }
}

OpponentDisplay.prototype.hideBubble = function () {
    this.dialogue.html("");
    this.bubble.hide();
}

OpponentDisplay.prototype.clearCustomPose = function () {
    if (this.pose instanceof Pose) {
        this.pose.setWillChangeHints(false);
    }

    this.imageArea.children('.custom-pose').remove();
    if (this.animCallbackID) {
        window.cancelAnimationFrame(this.animCallbackID);
        this.animCallbackID = undefined;
    }
}

OpponentDisplay.prototype.clearSimplePose = function () {
    this.simpleImage.hide();
}

OpponentDisplay.prototype.clearPose = function () {
    this.clearCustomPose();
    this.clearSimplePose();
    this.pose = null;
}

OpponentDisplay.prototype.drawPose = function (pose) {
    if (typeof(pose) === 'string') {
        // clear out previously shown custom poses if necessary
        if (this.pose instanceof Pose) {
            this.clearCustomPose(); 
        }
        this.simpleImage.attr('src', pose).show();
    } else if (pose instanceof Pose) {
        if (typeof(this.pose) === 'string') {
            // clear out previously shown simple poses
            this.clearSimplePose();
        } else if (this.pose instanceof Pose) {
            // Remove any previously shown custom poses too
            this.clearCustomPose();
        }
        
        this.imageArea.append(pose.container);
        pose.draw();
        if (pose.needsAnimationLoop()) {
            pose.setWillChangeHints(true);
            this.animCallbackID = window.requestAnimationFrame(this.loop.bind(this));
        }
    }
    
    this.pose = pose;
}

OpponentDisplay.prototype.onResize = function () {
    if (this.pose && (this.pose instanceof Pose)) {
        this.pose.draw();
    }
}

OpponentDisplay.prototype.updateText = function (player) {
    if (!player.chosenState.dialogue) {
        this.dialogue.empty();
        return;
    }

    var displayElems = parseStyleSpecifiers(player.chosenState.dialogue).map(function (comp) {
        /* {'text': 'foo', 'classes': 'cls1 cls2 cls3'} --> <span class="cls1 cls2 cls3">foo</span> */
        
        var wrapperSpan = document.createElement('span');
        wrapperSpan.innerHTML = fixupDialogue(comp.text);
        wrapperSpan.className = comp.classes;
        wrapperSpan.setAttribute('data-character', player.id);
        
        return wrapperSpan;
    });
    
    this.dialogue.empty().append(displayElems);
}

OpponentDisplay.prototype.updateImage = function(player) {
    var chosenState = player.chosenState;
    
    if (!chosenState.image) {
        this.clearPose();
    } else if (chosenState.image.startsWith('custom:')) {
        var key = chosenState.image.split(':', 2)[1].replace('#', player.stage);
        var poseDef = player.poses[key];
        if (poseDef) {
            this.drawPose(new Pose(poseDef, this));
        } else {
            this.clearPose();
        }
    } else {
        this.drawPose(player.folder + chosenState.image.replace('#', player.stage));
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
    this.updateText(player);
    
    /* update image */
    this.updateImage(player);    
    
    /* update label */
    this.label.html(player.label.initCap());

    /* check silence */
    if (!chosenState.dialogue) {
        this.hideBubble();
    } else {
        this.bubble.show();
        this.bubble.removeClass('arrow-down arrow-left arrow-right arrow-up');
        if (chosenState.direction != 'none') this.bubble.addClass('arrow-'+chosenState.direction);
        bubbleArrowOffsetRules[this.slot-1][0].style.left = chosenState.location;
        bubbleArrowOffsetRules[this.slot-1][1].style.top = chosenState.location;
    }
    
    /* Configure z-indices */
    this.imageArea.css('z-index', player.z_index);
    this.bubble.removeClass('over under').addClass(chosenState.dialogue_layering || player.dialogue_layering);
    
    if (showDebug && !inRollback()) {
        appendRepeats(this.slot);
    }
}

OpponentDisplay.prototype.loop = function (timestamp) {
    if (!this.pose || !(this.pose instanceof Pose)) return;
    this.pose.update(timestamp);
    if (this.pose.needsAnimationLoop()) {
        this.animCallbackID = window.requestAnimationFrame(this.loop.bind(this));
    } else {
        this.pose.setWillChangeHints(false);
        this.animCallbackID = undefined;
    }
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
    
    /* The overarching imageArea is properly scaled according to opponent
     * specifications already, so we only need to scale relative to the imageArea
     * dimensions.
     */
    this.simpleImage.on('load', OpponentDisplay.prototype.rescaleSimplePose.bind(this, 100));
    
    this.opponentArea = $('#game-opponent-area-'+slot);
    this.collectibleIndicator = $('#collectible-button-'+slot);
    
    this.collectibleIndicator.click(this.onCollectibleIndicatorClick.bind(this));
    this.devModeController = new DevModeDialogueBox(this.bubble);
}
GameScreenDisplay.prototype = Object.create(OpponentDisplay.prototype);
GameScreenDisplay.prototype.constructor = GameScreenDisplay;

GameScreenDisplay.prototype.reset = function (player) {
    clearHand(this.slot);
    
    /* Keep a reference to the player
     * (for handling collectible indicator clicks)
     */
    this.player = player;
    this.collectibleIndicator.hide();
    
    if (player) {
        this.opponentArea.show();
        this.imageArea.css('height', 0.8 * player.scale + '%').show();
        this.label.removeClass("current loser tied");
    } else {
        this.opponentArea.hide();
        this.clearPose();
        this.bubble.hide();
    }
}

GameScreenDisplay.prototype.update = function (player) {
    this.player = player;
    OpponentDisplay.prototype.update.call(this, player);
    
    if (devModeActive) this.devModeController.update(player);
    
    if (player && player.pendingCollectiblePopups.length) {
        this.collectibleIndicator.show();
    } else {
        this.collectibleIndicator.hide();
    }
}

GameScreenDisplay.prototype.onCollectibleIndicatorClick = function (ev) {
    if (!this.player || this.player.pendingCollectiblePopups.length == 0) return;
    
    this.player.pendingCollectiblePopups.shift().displayInfoModal();

    if (this.player.pendingCollectiblePopups.length == 0) {
        this.collectibleIndicator.hide();
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

    this.badges = {
        'epilogue': $('#select-badge-'+slot),
        'costume': $('#select-costume-badge-'+slot),
    };
    this.badges.epilogue.tooltip({placement: 'right', container: this.imageArea, delay: 200});
    this.allBadges = $('#select-image-area-'+slot+'>.select-badge-bar>img');
    this.layerIcon = $('#select-layer-'+slot);
    this.genderIcon = $('#select-gender-'+slot);
    this.statusIcon = $('#select-status-'+slot);
    
    this.targetSuggestionsShown = false;
    this.targetSuggestions = Array(4);
    this.suggestionQuad = Array(4);
    this.suggestionQuadContainer = $("#opponent-suggestions-"+slot);
    for (var i = 0; i < 4; i++) {
        this.suggestionQuad[i] = $("#opponent-suggestion-"+slot+"-"+(i+1));
        this.suggestionQuad[i].children(".opponent-suggestion-label").click(
            this.targetSuggestionSelected.bind(this, i)
        );
    }

    this.prefillSuggestion = null;
    this.prefillButton = $("#select-prefill-button-" + slot);
    this.prefillButton.click(
        this.onSingleSuggestionSelected.bind(this)
    );

    this.prefillBadgeRow = $('#selection-badge-row-' + slot);
    this.prefillSuggestionBadges = {
        'new': this.prefillBadgeRow.children('.badge-icon[data-badge="new"]'),
        'updated': this.prefillBadgeRow.children('.badge-icon[data-badge="updated"]'),
        'epilogue': this.prefillBadgeRow.children('.badge-icon[data-badge="epilogue"]'),
        'costume': this.prefillBadgeRow.children('.badge-icon[data-badge="costume"]'),
    }

    this.altCostumeSelector = $("#main-costume-select-"+slot);
    this.selectButton = $("#select-slot-button-"+slot);

    this.altCostumeSelector.on("change", this.altCostumeSelected.bind(this));
}

MainSelectScreenDisplay.prototype = Object.create(OpponentDisplay.prototype);
MainSelectScreenDisplay.prototype.constructor = MainSelectScreenDisplay;

MainSelectScreenDisplay.prototype.updateTargetSuggestionDisplay = function (quad, opponent) {
    var img_elem = this.suggestionQuad[quad].children('.opponent-suggestion-image');
    var label_elem = this.suggestionQuad[quad].children('.opponent-suggestion-label');
    var tooltip = null;
    
    this.targetSuggestions[quad] = opponent;
    if (opponent.status && statusIndicators[opponent.status]) {
        tooltip = statusIndicators[opponent.status].tooltip;
    } else if (opponent.highlightStatus && statusIndicators[opponent.highlightStatus]) {
        tooltip = statusIndicators[opponent.highlightStatus].tooltip;
    }

    img_elem.attr({
        'src': opponent.selection_image,
        'alt': opponent.label,
        'data-original-title': tooltip || null
    }).show();
    label_elem.text(opponent.label);
}

MainSelectScreenDisplay.prototype.targetSuggestionSelected = function (quad) {
    players[this.slot] = this.targetSuggestions[quad];

    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            category: 'select',
            message: 'Loading suggested opponent ' + this.targetSuggestions[quad].id,
            level: 'info'
        });
    }

    players[this.slot].loadBehaviour(this.slot, true);
    updateSelectionVisuals();
}

MainSelectScreenDisplay.prototype.displayTargetSuggestions = function (show) {
    this.targetSuggestionsShown = show;
    if (show) {
        this.suggestionQuadContainer.show();
    } else {
        this.suggestionQuadContainer.hide();
    }
}

MainSelectScreenDisplay.prototype.setPrefillSuggestion = function (player) {
    this.prefillSuggestion = player;
}

MainSelectScreenDisplay.prototype.displaySingleSuggestion = function () {
    var player = this.prefillSuggestion;

    this.hideBubble();
    this.drawPose(player.selection_image);
    this.label.html(player.label.initCap()).addClass('suggestion-label');
    this.imageArea.addClass('prefill-suggestion');
    this.selectButton.html("Select Other Opponent").removeClass("red").addClass("green suggestion-shown").attr('disabled', false);
    this.prefillButton.show();
    this.altCostumeSelector.hide();

    this.prefillSuggestionBadges.new.toggle(player.highlightStatus === 'new');
    this.prefillSuggestionBadges.updated.toggle(player.highlightStatus === 'updated');
    this.badges.epilogue.toggle(!!player.endings);
    var epilogueStatus = player.getEpilogueStatus();
    if (epilogueStatus) {
        this.badges.epilogue.attr({ 'src': epilogueStatus.badge,
                                    'data-original-title': epilogueStatus.tooltip });
    }
    //this.prefillSuggestionBadges.costume.toggle(player.alternate_costumes.length > 0);
    this.layerIcon.attr({
        src: "img/layers" + player.layers + ".png",
        alt: player.layers + " layers",
    }).show() ;
    updateGenderIcon(this.genderIcon, player);
    this.statusIcon.hide();
}

MainSelectScreenDisplay.prototype.onSingleSuggestionSelected = function () {
    players[this.slot] = this.prefillSuggestion;

    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            category: 'select',
            message: 'Loading prefill suggested opponent ' + this.prefillSuggestion.id,
            level: 'info'
        });
    }

    players[this.slot].loadBehaviour(this.slot, true);
    updateSelectionVisuals();
}

MainSelectScreenDisplay.prototype.update = function (player) {
    if (this.prefillSuggestion && this.prefillSuggestion != player
        && players.some(function (p) { return p && p.id === this.prefillSuggestion.id; }, this)) {
        loadDefaultFillSuggestions();
        return updateSelectionVisuals();
    }

    if (!player && !this.targetSuggestionsShown) {
        // attempt to load a prefill suggestion if missing
        if (!this.prefillSuggestion) loadDefaultFillSuggestions();

        // if we had one to begin with, or if we were able to load one, display
        // it
        if (this.prefillSuggestion) return this.displaySingleSuggestion();
    }

    this.prefillBadgeRow.children().hide();
    this.label.removeClass("suggestion-label");
    this.imageArea.removeClass("prefill-suggestion");
    this.selectButton.removeClass("suggestion-shown");
    this.prefillButton.hide();

    if (!player) {
        this.hideBubble();
        this.clearPose();
        this.imageArea.css('z-index', '-100');
        this.label.html("Opponent " + this.slot);

        /* change the button */
        this.selectButton.html("Select Opponent");
        this.selectButton.removeClass("red");
        this.selectButton.addClass("green").attr('disabled', false);
        this.altCostumeSelector.hide();

        this.allBadges.hide();
        this.layerIcon.hide();
        this.genderIcon.hide();
        this.statusIcon.hide();
        return;
    }
    
    this.badges.epilogue.toggle(!!player.endings);
    var epilogueStatus = player.getEpilogueStatus(true);
    if (epilogueStatus) {
        this.badges.epilogue.attr({'src': epilogueStatus.badge,
                                   'data-original-title': epilogueStatus.tooltip || ''});
    }
    //this.badges.costume.toggle(player.alternate_costumes.length > 0);
    //updateStatusIcon(this.statusIcon, player);
    this.layerIcon.attr({
        src: "img/layers" + player.layers + ".png",
        alt: player.layers + " layers",
    }).show() ;
    updateGenderIcon(this.genderIcon, player);
    
    if (!player.isLoaded()) {
        this.hideBubble();
        this.clearPose();
        
        this.label.html(player.label.initCap());
        this.selectButton.attr('disabled', true);
        this.updateLoadPercentage(player);
    } else {
        OpponentDisplay.prototype.update.call(this, player);
        
        this.selectButton.attr('disabled', false).html("Remove Opponent");
        this.selectButton.removeClass("green");
        this.selectButton.addClass("red");
        
        if (!(this.pose instanceof Pose)) {
            this.simpleImage.one('load', function() {
                this.bubble.show();
                OpponentDisplay.prototype.rescaleSimplePose.call(this, player.scale);
                this.simpleImage.show();
            }.bind(this));
        } else {
            this.pose.onLoadComplete = function () {
                this.bubble.show();
                this.imageArea.css('height', 0.8 * player.scale + '%').show();
            }.bind(this);
        }

        this.altCostumeSelector.hide();
        if (player.alternate_costumes.length > 0) {
            fillCostumeSelector(this.altCostumeSelector, player.alternate_costumes, player.selected_costume)
                .show();
        }
    }
}

MainSelectScreenDisplay.prototype.altCostumeSelected = function () {
    var opponent = players[this.slot];
    var costumeDesc = this.altCostumeSelector.children(':selected').data('costumeDescriptor');
    opponent.selectAlternateCostume(costumeDesc);
    if (opponent.selected_costume) {
        opponent.loadAlternateCostume().then(
            opponent.onSelected.bind(opponent, true)
        );
    } else {
        opponent.unloadAlternateCostume();
        opponent.onSelected(true);
    }
    updateSelectionVisuals();
}

MainSelectScreenDisplay.prototype.updateLoadPercentage = function (player) {
    if (player.isLoaded()) return;

    if (typeof player.loadProgress !== 'number' || isNaN(player.loadProgress)) {
        this.selectButton.html('Loading...');
    } else {
        this.selectButton.html('Loading (' + Math.floor(player.loadProgress * 100) + '%)');
    }
}

function createElementWithClass (elemType, className) {
    var elem = document.createElement(elemType);
    elem.className = className;
    
    return elem;
}


function OpponentSelectionCard (opponent) {
    this.opponent = opponent;
    
    this.mainElem = createElementWithClass('div', 'selection-card');

    if (opponent.highlightStatus)
        this.mainElem.dataset.highlight = opponent.highlightStatus;
    
    var clipElem = this.mainElem.appendChild(createElementWithClass('div', 'selection-card-image-clip'));
    this.imageArea = clipElem.appendChild(createElementWithClass('div', 'selection-card-image-area'));
    this.simpleImage = $(this.imageArea.appendChild(createElementWithClass('img', 'selection-card-image-simple')));
    
    this.imageArea = $(this.imageArea);

    var badgeSidebar = this.mainElem.appendChild(createElementWithClass('div', 'badge-sidebar'));

    if (opponent.endings) {
        this.epilogueBadge = $(badgeSidebar.appendChild(createElementWithClass('img', 'badge-icon epilogue-badge'))).attr({
            src: "img/epilogue.svg",
            alt: "SPNatI Epilogue available"
        }).tooltip({placement: 'right', delay: 200, container: this.mainElem});
    }

    if (opponent.alternate_costumes.length > 0) {
        $(badgeSidebar.appendChild(createElementWithClass('img', 'badge-icon'))).attr({
            src: "img/costume_badge.svg",
            alt: "SPNatI Alternate Costume available"
        });
    }

    var sidebarElem = this.mainElem.appendChild(createElementWithClass('div', 'selection-card-sidebar'));
    this.layerIcon = $(sidebarElem.appendChild(createElementWithClass('img', 'layer-icon')));
    this.genderIcon = $(sidebarElem.appendChild(createElementWithClass('img', 'gender-icon')));
    this.statusIcon = $(sidebarElem.appendChild(createElementWithClass('img', 'status-icon')));
    
    var footerElem = this.mainElem.appendChild(createElementWithClass('div', 'selection-card-footer'));
    this.label = $(footerElem.appendChild(createElementWithClass('div', 'selection-card-label selection-card-name')));
    this.source = $(footerElem.appendChild(createElementWithClass('div', 'selection-card-label selection-card-source')));
    
    /** Has this opponent been filtered out of the individual selection screen? */
    this.filtered = false;

    this.update();

    this.mainElem.addEventListener('click', this.handleClick.bind(this));
}

OpponentSelectionCard.prototype = Object.create(OpponentDisplay.prototype);
OpponentSelectionCard.prototype.constructor = OpponentSelectionCard;

OpponentSelectionCard.prototype.update = function () {    
    updateStatusIcon(this.statusIcon, this.opponent);

    this.layerIcon.attr({
        src: "img/layers" + this.opponent.layers + ".png",
        alt: this.opponent.layers + " layers",
    }).show() ;
    updateGenderIcon(this.genderIcon, this.opponent);

    this.simpleImage.one('load', OpponentDisplay.prototype.rescaleSimplePose.bind(this, this.opponent.scale));
    this.simpleImage.attr('src', this.opponent.selection_image).show();
    
    this.label.text(this.opponent.selectLabel);
    this.source.text(this.opponent.source);
}

OpponentSelectionCard.prototype.updateEpilogueBadge = function () {
    if (!this.epilogueBadge || !this.opponent.endings) return;

    var epilogueStatus = this.opponent.getEpilogueStatus();
    this.epilogueBadge.attr({'src': epilogueStatus.badge,
                             'data-original-title': epilogueStatus.tooltip || '' });
}

OpponentSelectionCard.prototype.clear = function () {}

OpponentSelectionCard.prototype.handleClick = function (ev) {
    individualDetailDisplay.update(this.opponent);
}

/**
 * Should this card be visible?
 * 
 * @param {boolean} testingView If true, visibility will be calculated for the
 * Testing Tables view instead of the regular roster view.
 * 
 * @param {boolean} ignoreFilter If true, any previous filtering status will be
 * ignored.
 */
OpponentSelectionCard.prototype.isVisible = function (testingView, ignoreFilter) {
    /* hide already selected opponents */
    if (this.opponent.slot) return false;

    var status = this.opponent.status;

    // Should this opponent be on the "main roster view"?
    var onMainView = (status === undefined || includedOpponentStatuses[status]);

    if (!testingView) {
        // Regular view: include all opponents with undefined status and with
        // included statuses.
        if (!onMainView) return false;
    } else {
        /* Testing view: include all opponents with `testing` status and those
         * targeted by testing opponents with 5+ lines.
         */
        if (status !== "testing" && this.opponent.inboundLinesFromSelected("testing") < 5)
            return false;
    }

    /* hide filtered opponents */
    return ignoreFilter || !this.filtered;
}

/**
 * Set whether this card should be hidden due to filtering.
 * 
 * @param {boolean} val If true, this card will be "filtered" and hidden.
 */
OpponentSelectionCard.prototype.setFiltered = function (val) {
    this.filtered = val;
}

OpponentDetailsDisplay = function () {
    this.displayContainer = $("#individual-select-screen .opponent-details-panel");
    
    this.mainView = $('#individual-select-screen .opponent-details-basic');
    
    this.epiloguesView = $('#individual-select-screen .opponent-details-epilogues');
    this.epiloguesContainer = $('#individual-select-screen .opponent-epilogues-container');
    
    this.collectiblesView = $('#individual-select-screen .opponent-details-collectibles');
    this.collectiblesContainer = $('#individual-select-screen .opponent-collectibles-container');
    
    this.epiloguesField = $('#individual-select-screen .opponent-epilogues-field');
    this.collectiblesField = $('#individual-select-screen .opponent-collectibles-field');
    
    this.nameLabel = $("#individual-select-screen .opponent-full-name");
    this.sourceLabel = $("#individual-select-screen .opponent-source");
    this.writerLabel = $("#individual-select-screen .opponent-writer");
    this.artistLabel = $("#individual-select-screen .opponent-artist");
    this.descriptionLabel = $("#individual-select-screen .opponent-details-description");
    this.linecountLabel = $("#individual-select-screen .opponent-linecount");
    this.posecountLabel = $("#individual-select-screen .opponent-posecount");
    this.costumeSelector = $("#individual-select-screen .alt-costume-dropdown");
    this.simpleImage = $("#individual-select-screen .opponent-details-simple-image");
    this.imageArea = $("#individual-select-screen .opponent-details-image-area");
    
    this.selectButton = $('#individual-select-screen .select-button');
    this.epiloguesNavButton = $('#individual-select-screen .opponent-epilogues');
    this.collectiblesNavButton = $('#individual-select-screen .opponent-collectibles');
    
    this.showMoreButton = $('#individual-select-screen .show-more-button');
    
    $('#individual-select-screen .opponent-nav-button').click(this.handlePanelNavigation.bind(this));
    
    this.costumeSelector.change(this.handleCostumeChange.bind(this));
    this.selectButton.click(this.handleSelected.bind(this));
    this.showMoreButton.click(function () {
        this.mainView.toggleClass('show-more');
    }.bind(this));
    
    this.epiloguesView.hide();
    this.collectiblesView.hide();
    
    var query = window.matchMedia('(min-aspect-ratio: 4/3)');
    if (query.matches) {
        this.mainView.addClass('show-more');
    } else {
        this.mainView.removeClass('show-more');
    }
}

OpponentDetailsDisplay.prototype = Object.create(OpponentDisplay.prototype);
OpponentDetailsDisplay.prototype.constructor = OpponentDetailsDisplay;

OpponentDetailsDisplay.prototype.handleSelected = function (ev) {
    if (!this.opponent) return;
    
    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            category: 'select',
            message: 'Loading individual opponent ' + this.opponent.id,
            level: 'info'
        });
        Sentry.setTag("screen", "select-main");
    }

    players[selectedSlot] = this.opponent;
	players[selectedSlot].loadBehaviour(selectedSlot, true);
    updateSelectionVisuals();
	screenTransition($individualSelectScreen, $selectScreen);
    
    this.clear();
}

OpponentDetailsDisplay.prototype.handlePanelNavigation = function (ev) {
    var targetPanel = $(ev.target).attr('data-target');
    
    $('#individual-select-screen .opponent-details-view').hide();
    
    if (targetPanel === 'epilogues') {
        this.updateEpiloguesView();
        this.epiloguesView.show();
    } else if (targetPanel === 'collectibles') {
        this.updateCollectiblesView();
        this.collectiblesView.show();
    } else {
        this.mainView.show();
    }
}

OpponentDetailsDisplay.prototype.handleCostumeChange = function () {
    if (!this.opponent) return;
    var costumeDesc = this.costumeSelector.children(':selected').data('costumeDescriptor');
    this.opponent.selectAlternateCostume(costumeDesc);
    this.simpleImage.attr('src', this.opponent.selection_image);
}

OpponentDetailsDisplay.prototype.clear = function () {
    this.opponent = null;
    this.nameLabel.empty();
    this.sourceLabel.empty();
    this.writerLabel.empty();
    this.artistLabel.empty();
    this.descriptionLabel.empty();
    
    this.simpleImage.attr('src', null);
    this.selectButton.prop('disabled', true);
    this.epiloguesField.removeClass('has-epilogues');
    this.collectiblesField.removeClass('has-collectibles');
    this.costumeSelector.hide();
    
    this.displayContainer.hide();
}

OpponentDetailsDisplay.prototype.createEpilogueCard = function (title, gender, unlockHint) {
    // Add the opponent-epilogue-* classes for future extensibility and also
    // to minimize disruptions with caching
    var container = createElementWithClass('div', 'bordered opponent-subview-card opponent-epilogue-card');
    
    var titleElem = container.appendChild(createElementWithClass('div', 'opponent-subview-title opponent-epilogue-title'));
    $(titleElem).html(title);
    
    var genderElem = container.appendChild(createElementWithClass('div', 'bordered left-cap opponent-subview-row opponent-epilogue-row opponent-epilogue-gender'));
    var genderLabel = genderElem.appendChild(createElementWithClass('div', 'left-cap opponent-subview-label opponent-epilogue-label'));
    var genderValue = genderElem.appendChild(createElementWithClass('div', 'opponent-subview-value opponent-epilogue-value'));
    
    $(genderValue).html(gender);
    $(genderLabel).text("For");
    
    if (unlockHint) {
        var unlockHintElem = container.appendChild(createElementWithClass('div', 'bordered left-cap opponent-subview-row opponent-epilogue-row opponent-epilogue-unlock'));
        var unlockHintLabel = unlockHintElem.appendChild(createElementWithClass('div', 'left-cap opponent-subview-label opponent-epilogue-label'));
        var unlockHintValue = unlockHintElem.appendChild(createElementWithClass('div', 'opponent-subview-value opponent-epilogue-value'));
        $(unlockHintLabel).text("To Unlock");
        $(unlockHintValue).html(unlockHint);
    }
    
    return container;
}

function isEquivalentEpilogue(e1, e2) {
    if (e1.text() !== e2.text()) return false;
    
    return EPILOGUE_CONDITIONAL_ATTRIBUTES.every(function (condAttr) {
        return e1.attr(condAttr) == e2.attr(condAttr);
    });
}

OpponentDetailsDisplay.prototype.updateEpiloguesView = function () {
    if (!this.opponent.endings) return;

    // Group together any epilogues with a shared name and conditional attributes (but with different gender attributes).
    var groups = [];

    this.opponent.endings.each(function (idx, elem) {
        var $elem = $(elem);
        var title = $elem.text();

        if(!groups.some(function (group) {
            if (group.every(isEquivalentEpilogue.bind(null, $elem))) {
                // This group contains all equivalent epilogues to the current one, add the current epilogue 
                group.push(elem);
                return true;
            }
            return false;
        })) {
            // Add the current element as a new group
            groups.push([$elem]);
        }
    });
    
    var cards = groups.map(function (group) {
        var condGender = group[0].attr('gender');
        var genderText = '';
        
        if (group.length > 1) {
            genderText = 'All Genders';
        } else if (condGender === 'male') {
            genderText = 'Males';
        } else if (condGender === 'female') {
            genderText = 'Females';
        } else {
            genderText = 'All Genders';
        }
        
        return this.createEpilogueCard(
            group[0].text(), genderText, group[0].attr('hint')
        );
    }.bind(this));
    
    this.epiloguesContainer.empty().append(cards);
};

OpponentDetailsDisplay.prototype.createCollectibleCard = function (collectible) {
    var container = createElementWithClass('div', 'bordered opponent-subview-card');
    
    var titleElem = container.appendChild(createElementWithClass('div', 'opponent-subview-title'));
    var subtitleElem = container.appendChild(createElementWithClass('div', 'opponent-subview-subtitle'));
    
    if (!collectible.detailsHidden || collectible.isUnlocked()) {
        $(titleElem).html(collectible.title);
        $(subtitleElem).html(collectible.subtitle);
    } else {
        $(titleElem).html("[Locked]");
        $(subtitleElem).html("");
    }
    

    if (collectible.unlock_hint) {
        var unlockHintElem = container.appendChild(createElementWithClass('div', 'bordered left-cap opponent-subview-row opponent-collectible-unlock'));
        var unlockHintLabel = unlockHintElem.appendChild(createElementWithClass('div', 'left-cap opponent-subview-label'));
        var unlockHintValue = unlockHintElem.appendChild(createElementWithClass('div', 'opponent-subview-value'));
        $(unlockHintLabel).text("To Unlock");
        $(unlockHintValue).html(collectible.unlock_hint);
    }
    
    if (collectible.counter) {
        var counterElem = container.appendChild(createElementWithClass('div', 'bordered left-cap opponent-subview-row opponent-collectible-counter'));
        var counterLabel = counterElem.appendChild(createElementWithClass('div', 'left-cap opponent-subview-label'));
        var counterValue = counterElem.appendChild(createElementWithClass('div', 'opponent-subview-value'));
        
        var curCounter = collectible.getCounter()
        $(counterLabel).text("Progress");
        $(counterValue).html(curCounter + ' / ' + collectible.counter);
    }
    
    return container;
}

OpponentDetailsDisplay.prototype.updateCollectiblesView = function () {
    if (!COLLECTIBLES_ENABLED || !this.opponent.has_collectibles || !this.opponent.collectibles) return;
    
    var cards = this.opponent.collectibles.map(function (collectible) {
        if (
            (collectible.status && !includedOpponentStatuses[collectible.status]) ||
            (collectible.hidden && !collectible.isUnlocked())
        ) {
            return null;
        } else {
            return this.createCollectibleCard(collectible);
        }
    }.bind(this));
    this.collectiblesContainer.empty().append(cards);
}

OpponentDetailsDisplay.prototype.update = function (opponent) {
    if (this.opponent === opponent) {
        // Interpret double-clicks as selection events.
        return this.handleSelected();
    }
    
    
    this.opponent = opponent;
    
    this.displayContainer.show();
    this.nameLabel.html(opponent.first + " " + opponent.last);
    this.sourceLabel.html(opponent.source);
    this.writerLabel.html(opponent.writer);
    this.artistLabel.html(opponent.artist);
    this.descriptionLabel.html(opponent.description);

    this.simpleImage.one('load', OpponentDisplay.prototype.rescaleSimplePose.bind(this, opponent.scale));
    this.simpleImage.attr('src', opponent.selection_image).show();
    
    this.selectButton.prop('disabled', false);
    
    var query = window.matchMedia('(min-aspect-ratio: 4/3)');
    if (query.matches) {
        this.mainView.addClass('show-more');
    } else {
        this.mainView.removeClass('show-more');
    }
    
    if (!opponent.endings) {
        this.epiloguesField.removeClass('has-epilogues');
    } else {
        this.epiloguesField.addClass('has-epilogues');
        var epilogueStatus = opponent.getEpilogueStatus();

        var ctr = '(' + epilogueStatus.unlocked + '/' + epilogueStatus.total + ' seen)';
        var text = "";

        var showPositive = false;
        var bestMatchEpilogue = epilogueStatus.match;
        if (bestMatchEpilogue == null) {
            text = 'All unlocked';
            showPositive = true;
        } else if (bestMatchEpilogue.wrongGender) {
            text = bestMatchEpilogue.gender.initCap() + 's Only';
        } else if (bestMatchEpilogue.missingCharacter) {
            var opp = loadedOpponents.find(function (p) {
                return p.id === bestMatchEpilogue.missingCharacter;
            });

            if (opp) {
                text = "Requires " + opp.selectLabel;
            } else {
                text = "Requires Missing Character";
            }
        } else {
            text = (bestMatchEpilogue.extraConditions > 0 ? "Conditionally " : "") + "Available";
            showPositive = true;
        }

        this.epiloguesNavButton
            .removeClass('red blue')

        text += " " + ctr;
        this.epiloguesNavButton.text(text);
        if (showPositive) {
            this.epiloguesNavButton.addClass('blue');
        } else {
            this.epiloguesNavButton.addClass('red');
        }
    }

    if (COLLECTIBLES_ENABLED && opponent.has_collectibles) {
        this.collectiblesField.addClass('has-collectibles');
        this.collectiblesNavButton
            .text("Available")
            .addClass('blue')
            .prop('disabled', false);
        
        opponent.loadCollectibles().then(function () {
            if (!opponent.has_collectibles) {
                this.collectiblesField.removeClass('has-collectibles');
            }

            var counts = opponent.collectibles.reduce(function (acc, collectible) {
                if (
                    (collectible.status && !includedOpponentStatuses[collectible.status]) ||
                    (collectible.hidden && !collectible.isUnlocked())
                ) {
                    return acc;
                }

                acc.total += 1;
                if (collectible.isUnlocked()) acc.unlocked += 1;
                
                return acc;
            }, {unlocked:0, total:0});
            
            this.collectiblesNavButton.text("Available ("+counts.unlocked+"/"+counts.total+" unlocked)");
        }.bind(this)).catch(function (err) {
            captureError(err);
            this.collectiblesField.removeClass('has-collectibles');
        }.bind(this));
    } else {
        this.collectiblesField.removeClass('has-collectibles');
    }

    if (opponent.alternate_costumes.length > 0) {
        fillCostumeSelector(this.costumeSelector, opponent.alternate_costumes, opponent.selected_costume)
            .show().prop('disabled', false);
    } else {
        this.costumeSelector.hide();
    }
    
    if (opponent.uniqueLineCount === undefined || opponent.posesImageCount === undefined) {
        // retrieve line and image counts
        if (DEBUG) {
            console.log("[LineImageCount] Fetching counts for " + opponent.label);
        }

        this.linecountLabel.text("Loading...");
        this.posecountLabel.text("Loading...");

        opponent.fetchBehavior().then(countLinesImages).then(function(response) {
            opponent.uniqueLineCount = response.numUniqueLines;
            opponent.posesImageCount = response.numPoses;

            // show line and image counts
            if (DEBUG) {
                console.log("[LineImageCount] Loaded " + opponent.label + " from behaviour: " +
                  opponent.uniqueLineCount + " lines, " + opponent.posesImageCount + " images");
            }
            
            this.linecountLabel.text(opponent.uniqueLineCount);
            this.posecountLabel.text(opponent.posesImageCount);
        }.bind(this)).catch(function (err) {
            console.error("Could not fetch counts for " + opponent.id);
            captureError(err);
            this.linecountLabel.text("???");
            this.posecountLabel.text("???");
        }.bind(this));
    }
    else {
        // this character's counts were previously loaded
        if (DEBUG) {
            console.log("[LineImageCount] Loaded previous count for " + opponent.label + ": " +
              opponent.uniqueLineCount + " lines, " + opponent.posesImageCount + " images)");
        }
        this.linecountLabel.text(opponent.uniqueLineCount);
        this.posecountLabel.text(opponent.posesImageCount);
    }
    
    this.epiloguesView.hide();
    this.collectiblesView.hide();
    this.mainView.show();
}
