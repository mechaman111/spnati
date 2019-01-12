/********************************************************************************
 This file contains code related to the Pose Engine.
 ********************************************************************************/

$imageAreas =  [$("#game-image-area-1"),
               $("#game-image-area-2"),
               $("#game-image-area-3"),
               $("#game-image-area-4")];

/* NOTE: These are basically the same as epilogue engine sprites.
 * There's a _lot_ of common code here that can probably be merged.
 */
function PoseSprite(id, src, args) {
    this.id = id;
    this.src = src;
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
      "transform":  "translate(" + this.x + "px, " + this.y + "px)",
      "transform-origin": "top left",
      "opacity": this.alpha / 100,
      "height": '100%'
    });
    
    $(this.img).css({
      "transform": "rotate(" + this.rotation + "deg) scale(" + this.scalex + ", " + this.scaley + ")",
      'height': this.height,
      'width': this.width
    });
}

function Pose(id, sprites) {
    this.id = id;
    this.sprites = sprites;
    
    var container = document.createElement('div');
    $(container).addClass("opponent-image custom-pose").css({
        "position": "relative",
        'z-index': -1
    });
    
    this.sprites.forEach(function(s) {
        container.appendChild(s.vehicle);
    });
    
    this.container = container;
}

Pose.prototype.draw = function() {
    this.sprites.forEach(function (s) {
        s.draw();
    });
}

function parseSpriteDefinition ($xml, imageBaseFolder) {
    var targetObj = {};
    var $obj = $($xml);
    $.each($xml.attributes, function (i, attr) {
      var name = attr.name.toLowerCase();
      var value = attr.value;
      targetObj[name] = value;
    });
  
    //properties needing special handling
    if (targetObj.alpha) { targetObj.alpha = parseFloat(targetObj.alpha, 10); }
    targetObj.zoom = parseFloat(targetObj.zoom, 10);
    targetObj.rotation = parseFloat(targetObj.rotation, 10);
    if (targetObj.scale) {
        targetObj.scalex = targetObj.scaley = targetObj.scale;
    }
    targetObj.scalex = parseFloat(targetObj.scalex, 10);
    targetObj.scaley = parseFloat(targetObj.scaley, 10);
    targetObj.src = imageBaseFolder+targetObj.src;
    
    return targetObj;
}

function PoseDefinition ($xml, imageBaseFolder) {
    this.id = $xml.attr('id').trim();

    var spriteDefs = [];
    $xml.find('sprite').each(function (i, elem) {
        spriteDefs.push(parseSpriteDefinition(elem, imageBaseFolder));
    });
    
    this.sprites = spriteDefs;
}

PoseDefinition.prototype.toPose = function() {
    var spriteElems = [];
    
    this.sprites.forEach(function (def) {
        spriteElems.push(new PoseSprite(def.id, def.src, def));
    });
    
    return new Pose(this.id, spriteElems);
}

function drawPoseToSlot(pose, slot) {
    if (typeof(pose) === 'string') {
        $imageAreas[slot-1].remove('.custom-pose');
        $gameImages[slot-1].show().attr('src', pose);
    } else if (pose instanceof Pose) {
        pose.draw();
        $gameImages[slot-1].hide();
        $imageAreas[slot-1].append(pose.container);
    } else {
        $gameImages[slot-1].hide();
        $imageAreas[slot-1].remove('.custom-pose');
    }
}
