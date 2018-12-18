/* Epilogue UI elements */
$epilogueScreen = $('#epilogue-screen');
var epilogueContainer = document.getElementById('epilogue-container');
var epilogueContent = document.getElementById('epilogue-content');

/* Epilogue selection modal elements */
$epilogueSelectionModal = $('#epilogue-modal'); //the modal box
$epilogueHeader = $('#epilogue-header-text'); //the header text for the epilogue selection box
$epilogueList = $('#epilogue-list'); //the list of epilogues
$epilogueAcceptButton = $('#epilogue-modal-accept-button'); //click this button to go with the chosen ending

var epilogueSelections = []; //references to the epilogue selection UI elements

var winStr = "You've won the game, and possibly made some friends. Who among these players did you become close with?"; //Winning the game, with endings available
var winStrNone = "You've won the game, and possibly made some friends? Unfortunately, none of your competitors are ready for a friend like you.<br>(None of the characters you played with have an ending written.)"; //Player won the game, but none of the characters have an ending written
var lossStr = "Well you lost. And you didn't manage to make any new friends. But you saw some people strip down and show off, and isn't that what life is all about?<br>(You may only view an ending when you win.)"; //Player lost the game. Currently no support for ending scenes when other players win

// Player won the game, but epilogues are disabled.
var winEpiloguesDisabledStr = "You won... but epilogues have been disabled.";

// Player lost the game with epilogues disabled.
var lossEpiloguesDisabledStr = "You lost... but epilogues have been disabled.";

var epilogues = []; //list of epilogue data objects
var chosenEpilogue = null;
var epiloguePlayer = null;
var epilogueSuffix = 0;

// Attach some event listeners
var previousButton = document.getElementById('epilogue-previous');
var nextButton = document.getElementById('epilogue-next');
previousButton.addEventListener('click', function(e) {
  e.preventDefault();
  e.stopPropagation();
  moveEpilogueBack();
});
nextButton.addEventListener('click', function(e) {
  e.preventDefault();
  e.stopPropagation();
  moveEpilogueForward();
});
document.getElementById('epilogue-restart').addEventListener('click', function(e) {
  e.preventDefault();
  e.stopPropagation();
  showRestartModal();
});
document.getElementById('epilogue-buttons').addEventListener('click', function() {
  if (!previousButton.disabled) {
    moveEpilogueBack();
  }
});
epilogueContent.addEventListener('click', function() {
  if (!nextButton.disabled) {
    moveEpilogueForward();
  }
});

/************************************************************
 * Animation class. Used instead of CSS animations for the control over stopping/rewinding/etc.
 ************************************************************/
function Animation(id, frames, updateFunc, loop) {
  this.id = id;
  this.looped = loop === "1" || loop === "true";
  this.keyframes = frames;
  for (var i = 0; i < frames.length; i++) {
    frames[i].index = i;
    frames[i].keyframes = frames;
  }
  this.duration = frames[frames.length - 1].end;
  this.elapsed = 0;
  this.updateFunc = updateFunc;
  this.easingFunction = "smooth";
}
Animation.prototype.easingFunctions = {
  "linear": function (t) { return t; },
  "smooth": function (t) { return 3 * t * t - 2 * t * t * t; },
  "ease": function (t) { return curve(t, 0, 1); },
  "ease-in": function (t) { return curve(t, 0, 0.5); },
  "ease-out": function (t) { return curve(t, 0.5, 0); },
};
Animation.prototype.update = function (elapsedMs) {
  this.elapsed += elapsedMs;

  if (!this.updateFunc) { return; }

  //determine what keyframes we're between
  var last;
  if (this.looped) {
    this.elapsed = this.elapsed % this.duration;
  }
  var t = this.elapsed;
  for (var i = this.keyframes.length - 1; i >= 0; i--) {
    var frame = this.keyframes[i];
    if (isNaN(frame.start)) { frame.start = 0; frame.end = 0; }
    if (t >= frame.start) {
      last = (i > 0 ? this.keyframes[i - 1] : frame);
      //normalize the time between frames
      var time = t === 0 ? 0 : Math.min(1, Math.max(0, (t - frame.start) / (frame.end - frame.start)));
      var easingFunction = frame.ease || "linear";
      time = this.easingFunctions[easingFunction](time);
      this.updateFunc(this.id, last, frame, time);
      return;
    }
  }
}
Animation.prototype.halt = function () {
  var frame = this.keyframes[this.keyframes.length - 1];
  this.updateFunc && this.updateFunc(this.id, frame, frame, 1);
}

/************************************************************
 * Creates a closure in order to maintain a function's "this"
 ************************************************************/
function createClosure(instance, func)
{
	var $this = instance;
	return function ()
	{
		func.apply($this, arguments);
	};
}

/************************************************************
 * Linear interpolation
 ************************************************************/
function lerp(a, b, t)
{
	t = Math.min(1, Math.max(t));
	return (b - a) * t + a;
}

/************************************************************
 * Interpolation functions for animation movement interpolation
 ************************************************************/
var interpolationModes = {
	"linear": function linear(prop, start, end, t, frames, index)
	{
		return lerp(start, end, t);
	},
	"spline": function catmullrom(prop, start, end, t, frames, index)
	{
		var p0 = index > 0 ? frames[index - 1][prop] : start;
		var p1 = start;
		var p2 = end;
		var p3 = index < frames.length - 2 ? frames[index + 1][prop] : end;

		if (typeof p0 === "undefined" || isNaN(p0))
		{
			p0 = start;
		}
		if (typeof p3 === "undefined" || isNaN(p3))
		{
			p3 = end;
		}

		var a = 2 * p1;
		var b = p2 - p0;
		var c = 2 * p0 - 5 * p1 + 4 * p2 - p3;
		var d = -p0 + 3 * p1 - 3 * p2 + p3;

		var p = 0.5 * (a + (b * t) + (c * t * t) + (d * t * t * t));
		return p;
	},
};

/************************************************************
 * Converts a px or % value to the equivalent scene value
 ************************************************************/
function toSceneX(x, scene)
{
	if (typeof x === "undefined") { return; }
	if ($.isNumeric(x)) { return parseInt(x, 10); }
	if (x.endsWith("%"))
	{
		return parseInt(x, 10) / 100 * scene.width;
	}
	else
	{
		return parseInt(x, 10);
	}
}

/************************************************************
 * Converts a px or % value to the equivalent scene value
 ************************************************************/
function toSceneY(y, scene)
{
	if (typeof y === "undefined") { return; }
	if ($.isNumeric(y)) { return parseInt(y, 10); }
	if (y.endsWith("%"))
	{
		return parseInt(y, 10) / 100 * scene.height;
	}
	else
	{
		return parseInt(y, 10);
	}
}

/************************************************************
 * Linear smoothing
 ************************************************************/
function linearInterpolation(t)
{
	return t;
}

/************************************************************
 * Catmull-Rom curve smoothing
 ************************************************************/
function curve(t, a, b)
{
	return 3 * a * Math.pow(1 - t, 2) * t + 3 * b * (1 - t) * (t * t) + (t * t * t);
}

/************************************************************
 * Return the numerical part of a string s. E.g. "20%" -> 20
 ************************************************************/

function getNumericalPart(s){
	return parseFloat(s); //apparently I don't actually need to remove the % (or anything else) from the string before I do the conversion
}

/************************************************************
 * Return the approriate left: setting so that a text box of the specified width is centred
 * Assumes a % width
 ************************************************************/
function getCenteredPosition(width){
	var w = getNumericalPart(width); //numerical value of the width
	var left = 50 - (w/2); //centre of the text box is at the 50% position
	return left + "%";
}

/************************************************************
 * Load the Epilogue data for a character
 ************************************************************/
function loadEpilogueData(player) {
    if (!player || !player.xml) { //return an empty list if a character doesn't have an XML variable. (Most likely because they're the player.)
        return [];
    }

	var playerGender = players[HUMAN_PLAYER].gender;

	//get the XML tree that relates to the epilogue, for the specific player gender
	//var epXML = $($.parseXML(xml)).find('epilogue[gender="'+playerGender+'"]'); //use parseXML() so that <image> tags come through properly //IE doesn't like this

  var epilogues = player.xml.find('epilogue').filter(function (index) {
      /* Returning true from this function adds the current epilogue to the list of selectable epilogues.
       * Conversely, returning false from this function will make the current epilogue not selectable.
       */

      /* 'gender' attribute: the epilogue will only be selectable if the player character has the given gender, or if the epilogue is marked for 'any' gender. */
      var epilogue_gender = $(this).attr('gender');
      if (epilogue_gender && epilogue_gender !== playerGender && epilogue_gender !== 'any') {
          // if the gender doesn't match, don't make this epilogue selectable
          return false;
      }

      var alsoPlaying = $(this).attr('alsoPlaying');
      if (alsoPlaying !== undefined && !(players.some(function(p) { return p.id == alsoPlaying; }))) {
          return false;
      }

      var playerStartingLayers = parseInterval($(this).attr('playerStartingLayers'));
      if (playerStartingLayers !== undefined && !inInterval(players[HUMAN_PLAYER].startingLayers, playerStartingLayers)) {
          return false;
      }

      /* 'markers' attribute: the epilogue will only be selectable if the character has ALL markers listed within the attribute set. */
      var all_marker_attr = $(this).attr('markers');
      if (all_marker_attr
          && !all_marker_attr.trim().split(/\s+/).every(function(marker) {
              return checkMarker(marker, player);
          })) {
          // not every marker set
          return false;
      }

      /* 'not-markers' attribute: the epilogue will only be selectable if the character has NO markers listed within the attribute set. */
      var no_marker_attr = $(this).attr('not-markers');
      if (no_marker_attr
          && no_marker_attr.trim().split(/\s+/).some(function(marker) {
              return checkMarker(marker, player);
          })) {
          // some disallowed marker set
          return false;
      }

      /* 'any-markers' attribute: the epilogue will only be selectable if the character has at least ONE of the markers listed within the attribute set. */
      var any_marker_attr = $(this).attr('any-markers');
      if (any_marker_attr
          && !any_marker_attr.trim().split(/\s+/).some(function(marker) {
              return checkMarker(marker, player);
          })) {
          // none of the markers set
          return false;
      }

      /* 'alsoplaying-markers' attribute: this epilogue will only be selectable if ALL markers within the attribute are set for any OTHER characters in the game. */
      var alsoplaying_marker_attr = $(this).attr('alsoplaying-markers');
      if (alsoplaying_marker_attr
          && !alsoplaying_marker_attr.trim().split(/\s+/).every(function(marker) {
              return players.some(function(p) {
                  return p !== player && checkMarker(marker, p);
              });
          })) {
          // not every marker set by some other character
          return false;
      }

      /* 'alsoplaying-not-markers' attribute: this epilogue will only be selectable if NO markers within the attribute are set for other characters in the game. */
      var alsoplaying_not_marker_attr = $(this).attr('alsoplaying-not-markers');
      if (alsoplaying_not_marker_attr
          && alsoplaying_not_marker_attr.trim().split(/\s+/).some(function(marker) {
              return players.some(function(p) {
                  return p !== player && checkMarker(marker, p);
              });
          })) {
          // some disallowed marker set by some other character
          return false;
      }

      /* 'alsoplaying-any-markers' attribute: this epilogue will only be selectable if at least one marker within the attribute are set for any OTHER character in the game. */
      var alsoplaying_any_marker_attr = $(this).attr('alsoplaying-any-markers');
      if (alsoplaying_any_marker_attr
          && !alsoplaying_any_marker_attr.trim().split(/\s+/).some(function(marker) {
              return players.some(function(p) {
                  return p !== player && checkMarker(marker, p);
              });
          })) {
          // none of the markers set by any other player
          return false;
      }

      // if we made it this far the epilogue must be selectable
      return true;
  }).map(function (i, e) { return parseEpilogue(player, e); }).get();

	return epilogues;
}

function parseEpilogue(player, rawEpilogue, galleryEnding) {
  //use parseXML() so that <image> tags come through properly
  //not using parseXML() because internet explorer doesn't like it

  if (!rawEpilogue) {
    return;
  }

  var $epilogue = $(rawEpilogue);
  var title = $epilogue.find("title").html().trim();

  var epilogue = {
    title: title,
    player: player,
    scenes: [],
  };
  var scenes = epilogue.scenes;

  //determine what type of epilogue this is and parse accordingly
  var isLegacy = $epilogue.children("screen").length > 0;
  if (isLegacy) {
    parseLegacyEpilogue(player, epilogue, $epilogue);
  }
  else if ($epilogue.children("background").length > 0) {
    var sceneWidth, sceneHeight;
    var rawRatio = $epilogue.attr('ratio');
    if (rawRatio) {
      rawRatio = rawRatio.split(':');
      sceneWidth = parseFloat(rawRatio[0]);
      sceneHeight = parseFloat(rawRatio[1]);
    }
    parseNotQuiteLegacyEpilogue(player, epilogue, $epilogue, sceneWidth, sceneHeight);
  }
  else {
    $epilogue.children("scene").each(function (index, rawScene) {
      var $scene = $(rawScene);
      var width = parseInt($scene.attr("width"), 10);
      var height = parseInt($scene.attr("height"), 10);
      var scene = {
        background: $scene.attr("background"),
        width: width,
        height: height,
        aspectRatio: width / height,
        zoom: parseFloat($scene.attr("zoom"), 10),
        color: $scene.attr("color"),
        overlayColor: $scene.attr("overlay"),
        overlayAlpha: $scene.attr("overlay-alpha"),
        directives: [],
      }
      scenes.push(scene);
      scene.x = toSceneX($scene.attr("x"), scene);
      scene.y = toSceneY($scene.attr("y"), scene);

      var directives = scene.directives;

      $scene.find("directive").each(function (i, item) {
        var totalTime = 0;
        var directive = readProperties(item, scene);
        directive.keyframes = [];
        $(item).find("keyframe").each(function (i2, frame) {
          var keyframe = readProperties(frame, scene);
          keyframe.ease = keyframe.ease || directive.ease;
          keyframe.start = totalTime;
          totalTime = Math.max(totalTime, keyframe.time);
          keyframe.end = totalTime;
          keyframe.interpolation = directive.interpolation || "linear";
          directive.keyframes.push(keyframe);
        });
        if (directive.keyframes.length === 0) {
          //if no explicity keyframes were provided, use the directive itself as a keyframe
          directive.start = 0;
          directive.end = directive.time;
          directive.keyframes.push(directive);
        }
        else {
          directive.time = totalTime;
        }

        directives.push(directive);
      });
    });
  }
  return epilogue;
}

/**
 * Parses an old screen-based epilogue and converts it into directive format
 */
function parseLegacyEpilogue(player, epilogue, $xml) {
  var scenes = epilogue.scenes;
  $xml.find("screen").each(function () {
    var $this = $(this);

    var image = $this.attr("img").trim();
    if (image.length > 0) {
      image = player.base_folder + image;
    }

    //create a scene for each screen
    var scene = {
      directives: [],
      background: image,
    };
    scenes.push(scene);
    parseSceneContent(player, scene, $this);
  });
}

/**
 * Parses an epilogue that came in the format background > scene > sprite/text and converts it into directive format
 */
function parseNotQuiteLegacyEpilogue(player, epilogue, $xml, sceneWidth, sceneHeight) {
  var scenes = epilogue.scenes;
  $xml.find('background').each(function () {
    var $this = $(this);
    var image = $this.attr('img').trim();
    if (image.length == 0) {
      image = '';
    } else {
      image = image.charAt(0) === '/' ? image : player.base_folder + image;
    }

    //create a directive-based scene for each scene in the background
    $this.find('scene').each(function () {
      var scene = {
        directives: [],
        background: image,
        width: sceneWidth,
        height: sceneHeight,
        aspectRatio: sceneWidth / sceneHeight,
      };
      scenes.push(scene);
      parseSceneContent(player, scene, $(this)); //this is intentionally $(this) instead of $this like in parseLegacyEpilogue
    });
  });
}

function parseSceneContent(player, scene, $scene) {
  var directive;
  var backgroundTransform = [$scene.attr('background-position-x'), $scene.attr('background-position-y'), $scene.attr('background-zoom') || 100];
  var addedPause = false;
  try {
    scene.x = toSceneX(backgroundTransform[0], scene);
    scene.y = toSceneY(backgroundTransform[1], scene);
    scene.zoom = parseFloat(backgroundTransform[2]) / 100;
  } catch (e) { }

  // Find the image data for this shot
  $scene.find('sprite').each(function () {
    var x = $(this).find("x").html().trim();
    var y = $(this).find("y").html().trim();
    var width = $(this).find("width").html().trim();
    var src = $(this).find('src').html().trim();

    src = src.charAt(0) === '/' ? src : player.base_folder + src;

    var css = $(this).attr('css');

    directive = {
      type: "sprite",
      id: "obj" + (epilogueSuffix++),
      x: toSceneX(x, scene),
      y: toSceneY(y, scene),
      width: width,
      src: src,
      css: css,
    }
    scene.directives.push(directive);

  });

  //get the information for all the text boxes
  $scene.find("text").each(function () {

    //the text box's position and width
    var x = $(this).find("x").html().trim();
    var y = $(this).find("y").html().trim();
    var w = $(this).find("width").html();
    var a = $(this).find("arrow").html();

    //the width component is optional. Use a default of 20%.
    if (w) {
      w = w.trim();
    }
    if (!w || (w.length <= 0)) {
      w = "20%"; //default to text boxes having a width of 20%
    }

    //dialogue bubble arrow
    if (a) {
      a = a.trim().toLowerCase();
      if (a.length >= 1) {
        a = "arrow-" + a; //class name for the different arrows. Only use if the writer specified something.
      }
    } else {
      a = "";
    }

    //automatically centre the text box, if the writer wants that.
    if (x && x.toLowerCase() == "centered") {
      x = getCenteredPosition(w);
    }

    var text = fixupDialogue($(this).find("content").html().trim()); //the actual content of the text box

    var css = $(this).attr('css');

    directive = {
      type: "text",
      id: "obj" + (epilogueSuffix++),
      x: x,
      y: y,
      arrow: a,
      width: w,
      text: text,
      css: css,
    }
    scene.directives.push(directive);
    scene.directives.push({ type: "pause" });
    addedPause = true;
  });
  if (!addedPause) {
      scene.directives.push({ type: "pause" });
  }
}

 /************************************************************
 * Read attributes from a source XML object and put them into properties of a JS object
 ************************************************************/
function readProperties(sourceObj, scene) {
  var targetObj = {};
  var $obj = $(sourceObj);
  $.each(sourceObj.attributes, function (i, attr) {
    var name = attr.name.toLowerCase();
    var value = attr.value;
    targetObj[name] = value;
  });

  //properties needing special handling

  if (targetObj.type !== "text") {
    // scene directives
    targetObj.time = parseFloat(targetObj.time, 10) * 1000;
    targetObj.alpha = parseFloat(targetObj.alpha, 10);
    targetObj.zoom = parseFloat(targetObj.zoom, 10);
    targetObj.rotation = parseFloat(targetObj.rotation, 10);
    targetObj.scale = parseFloat(targetObj.scale, 10);
    if (targetObj.x) { targetObj.x = toSceneX(targetObj.x, scene); }
    if (targetObj.y) { targetObj.y = toSceneY(targetObj.y, scene); }
  }
  else {
    // textboxes

    // ensure an ID
    var id = targetObj.id;
    if (!id) {
      targetObj.id = "obj" + (epilogueSuffix++);
    }

    // text (not from an attribute, so not populated automatically)
    targetObj.text = fixupDialogue($obj.html().trim());

    var w = targetObj.width;
    //the width component is optional. Use a default of 20%.
    if (w) {
      w = w.trim();
    }
    if (!w || (w.length <= 0)) {
      w = "20%"; //default to text boxes having a width of 20%
    }
    targetObj.width = w;

    //dialogue bubble arrow
    var a = targetObj.arrow; if (a) {
      a = a.trim().toLowerCase();
      if (a.length >= 1) {
        a = "arrow-" + a; //class name for the different arrows. Only use if the writer specified something.
      }
    } else {
      a = "";
    }
    targetObj.arrow = a;

    //automatically centre the text box, if the writer wants that.
    var x = targetObj.x;
    if (x && x.toLowerCase() == "centered") {
      targetObj.x = getCenteredPosition(w);
    }
  }
  return targetObj;
}

/************************************************************
 * Add the epilogue to the Epilogue modal
 ************************************************************/

function addEpilogueEntry(epilogue){
	var num = epilogues.length; //index number of the new epilogue
	epilogues.push(epilogue);
	var player = epilogue.player

	var nameStr = player.first+" "+player.last;
	if (player.first.length <= 0 || player.last.length <= 0){
		nameStr = player.first+player.last; //only use a space if they have both first and last names
	}

	var epilogueTitle = nameStr+": "+epilogue.title;
	var idName = 'epilogue-option-'+num;
	var clickAction = "selectEpilogue("+num+")";
	var unlocked = save.hasEnding(player.id, epilogue.title) ? " unlocked" : "";

	var htmlStr = '<li id="'+idName+'" class="epilogue-entry'+unlocked+'"><button onclick="'+clickAction+'">'+epilogueTitle+'</button></li>';

	$epilogueList.append(htmlStr);
	epilogueSelections.push($('#'+idName));
}

/************************************************************
 * Clear the Epilogue modal
 ************************************************************/

function clearEpilogueList(){
	$epilogueHeader.html('');
	$epilogueList.html('');
	epilogues = [];
	epilogueSelections = [];
}

/************************************************************
 * Cleans up epilogue data
 ************************************************************/
function clearEpilogue() {
  if (epiloguePlayer) {
    epiloguePlayer.destroy();
    epiloguePlayer = null;
  }
}

/************************************************************
 * The user has clicked on a button to choose a particular Epilogue
 ************************************************************/

function selectEpilogue(epNumber){
	chosenEpilogue = epilogues[epNumber]; //select the chosen epilogues

	for (var i = 0; i < epilogues.length; i++){
		epilogueSelections[i].removeClass("active"); //make sure no other epilogue is selected
	}
	epilogueSelections[epNumber].addClass("active"); //mark the selected epilogue as selected
	$epilogueAcceptButton.prop("disabled", false); //allow the player to accept the epilogue
}

/************************************************************
 * Show the modal for the player to choose an Epilogue, or restart the game.
 ************************************************************/
function doEpilogueModal(){

	clearEpilogueList(); //remove any already loaded epilogues
	chosenEpilogue = null; //reset any currently-chosen epilogue
	$epilogueAcceptButton.prop("disabled", true); //don't let the player accept an epilogue until they've chosen one

	//whether or not the human player won
	var playerWon = !players[HUMAN_PLAYER].out;

	if (EPILOGUES_ENABLED && playerWon) { //all the epilogues are for when the player wins, so don't allow them to choose one if they lost
		//load the epilogue data for each player
		players.forEach(function(p) {
			loadEpilogueData(p).forEach(addEpilogueEntry);
		});
	}

	//are there any epilogues available for the player to see?
	var haveEpilogues = (epilogues.length >= 1); //whether or not there are any epilogues available
	$epilogueAcceptButton.css("visibility", haveEpilogues ? "visible" : "hidden");

    if (EPILOGUES_ENABLED) {
        //decide which header string to show the player. This describes the situation.
    	var headerStr = '';
    	if (playerWon){
    		headerStr = winStr; //player won, and there are epilogues available
    		if (!haveEpilogues){
    			headerStr = winStrNone; //player won, but none of the NPCs have epilogues
    		}
    	} else {
    		headerStr = lossStr; //player lost
    	}
    } else {
        if (playerWon) {
            headerStr = winEpiloguesDisabledStr;
        } else {
            headerStr = lossEpiloguesDisabledStr;
        }
    }

	$epilogueHeader.html(headerStr); //set the header string
	$epilogueSelectionModal.modal("show");//show the epilogue selection modal
}

/************************************************************
 * Start the Epilogue
 ************************************************************/
function doEpilogue(){
	save.addEnding(chosenEpilogue.player.id, chosenEpilogue.title);

	if (USAGE_TRACKING) {
		var usage_tracking_report = {
			'date': (new Date()).toISOString(),
            'commit': VERSION_COMMIT,
			'type': 'epilogue',
			'session': sessionID,
			'game': gameID,
			'userAgent': navigator.userAgent,
			'origin': getReportedOrigin(),
			'table': {},
			'chosen': {
				'id': chosenEpilogue.player.id,
				'title': chosenEpilogue.title
			}
		};

		for (let i=1;i<5;i++) {
			if (players[i]) {
				usage_tracking_report.table[i] = players[i].id;
			}
		}

		$.ajax({
			url: USAGE_TRACKING_ENDPOINT,
			method: 'POST',
			data: JSON.stringify(usage_tracking_report),
			contentType: 'application/json',
			error: function (jqXHR, status, err) {
				console.error("Could not send usage tracking report - error "+status+": "+err);
			},
		});
	}

	//just in case, clear any leftover epilogue elements
  $(epilogueContent).children(':not(.epilogue-background)').remove();
  epilogueContainer.dataset.background = -1;
  epilogueContainer.dataset.scene = -1;

  loadEpilogue(chosenEpilogue);

	screenTransition($titleScreen, $epilogueScreen); //currently transitioning from title screen, because this is for testing
	$epilogueSelectionModal.modal("hide");
}

 /************************************************************
 * Starts up an epilogue, pre-fetching all its images before displaying anything in order to handle certain computations that rely on the image sizes
 ************************************************************/
function loadEpilogue(epilogue) {
  epiloguePlayer = new EpiloguePlayer(epilogue);
  epiloguePlayer.load();
  updateEpilogueButtons();
}

function moveEpilogueForward() {
  if (epiloguePlayer) {
    epiloguePlayer.advanceDirective();
    updateEpilogueButtons();
  }
}

function moveEpilogueBack() {
  if (epiloguePlayer) {
    epiloguePlayer.revertDirective();
    updateEpilogueButtons();
  }
}

/************************************************************
 * Updates enabled state of buttons
 ************************************************************/

function updateEpilogueButtons() {
  if (!epiloguePlayer) {
    return;
  }

  var $epiloguePrevButton = $('#epilogue-buttons > #epilogue-previous');
  var $epilogueNextButton = $('#epilogue-buttons > #epilogue-next');
  var $epilogueRestartButton = $('#epilogue-buttons > #epilogue-restart');
  $epiloguePrevButton.prop("disabled", !epiloguePlayer.hasPreviousDirectives());
  $epilogueNextButton.prop("disabled", !epiloguePlayer.hasMoreDirectives());
  $epilogueRestartButton.prop("disabled", epiloguePlayer.hasMoreDirectives());
}

 /************************************************************
 * Class for playing through an epilogue
 ************************************************************/
function EpiloguePlayer(epilogue) {
  $(window).resize(createClosure(this, this.resizeViewport));
  this.anims = [];
  this.epilogue = epilogue;
  this.lastUpdate = 0;
  this.sceneIndex = -1;
  this.directiveIndex = -1;
  this.sceneObjects = {};
  this.assetMap = {};
  this.camera = null;
  this.viewportWidth = 0;
  this.viewportHeight = 0;
  this.loadingImages = 0;
  this.waitingForAnims = false;
  this.overlay = { rgb: [0, 0, 0], a: 0 };
  this.epilogueContent = document.getElementById('epilogue-content');
  this.$viewport = $("#epilogue-viewport");
  this.$canvas = $("#epilogue-canvas");
  this.$overlay = $("#epilogue-overlay");
  this.$overlay.css("background-color", "#000000");
  this.$overlay.css("opacity", "0");
  this.sceneIndex = -1;
}

EpiloguePlayer.prototype.load = function () {
  for (var i = 0; i < this.epilogue.scenes.length; i++) {
    var scene = this.epilogue.scenes[i];
    if (scene.background) {
      scene.background = scene.background.charAt(0) === '/' ? scene.background : this.epilogue.player.base_folder + scene.background;
      this.fetchImage(scene.background);
    }
    for (var j = 0; j < scene.directives.length; j++) {
      var directive = scene.directives[j];
      if (directive.src) {
        directive.src = directive.src.charAt(0) === '/' ? directive.src : this.epilogue.player.base_folder + directive.src;
        this.fetchImage(directive.src);
      }
    }
  }
  this.readyToLoad = true;
  this.onLoadComplete();
}

/**
 * Called whenever all images being pre-fetched have been returned (which isn't necessarily when the total number of images that will be pre-fetched have been requested)
 * This is a workaround for IE11 not supporting promises
 */
EpiloguePlayer.prototype.onLoadComplete = function () {
  if (this.loadingImages > 0) { return; }

  if (this.readyToLoad) {
    this.$overlay.show();
    this.advanceScene();
    window.requestAnimationFrame(createClosure(this, this.loop));
  }
}

/**
 * Fetches an image asset ahead of time so it's ready before we need it
 * @param {string} path URL for image
 */
EpiloguePlayer.prototype.fetchImage = function (path) {
  var img = new Image();
  this.loadingImages++;
  var $this = this;
  img.onload = img.onerror = function () {
    $this.assetMap[path] = img;
    $this.loadingImages--;
    $this.onLoadComplete();
  };
  img.src = path;
}

EpiloguePlayer.prototype.destroy = function () {
  this.haltAnimations(true);

  //clear old textboxes
  $(this.epilogueContent).empty();

  //clear old images
  for (var obj in this.sceneObjects) {
    $(this.sceneObjects[obj].element).remove();
  }
  this.sceneObjects = {};

  this.$overlay.hide();
}

EpiloguePlayer.prototype.hasMoreDirectives = function () {
  return this.sceneIndex < this.epilogue.scenes.length - 1 || this.directiveIndex < this.activeScene.directives.length - 1;
}

EpiloguePlayer.prototype.hasPreviousDirectives = function () {
  return this.sceneIndex > 0|| this.directiveIndex > 0;
}

EpiloguePlayer.prototype.loop = function (timestamp) {
  var elapsed = timestamp - this.lastUpdate;

  if (this.anims.length > 0) {
    this.update(elapsed);
    this.draw();
  }

  this.lastUpdate = timestamp;
  window.requestAnimationFrame(createClosure(this, this.loop));
}

EpiloguePlayer.prototype.update = function (elapsed) {
  var nonLoopingCount = 0;
  for (var i = this.anims.length - 1; i >= 0; i--) {
    var anim = this.anims[i];
    anim.update(elapsed);
    if (!anim.looped) {
      if (anim.elapsed >= anim.duration) {
        this.anims.splice(i, 1);
      }
      else {
        nonLoopingCount++;
      }
    }
  }
  if (nonLoopingCount === 0 && this.waitingForAnims) {
    this.advanceDirective();
  }
}

EpiloguePlayer.prototype.draw = function () {
  for (var obj in this.sceneObjects) {
    this.drawObject(this.sceneObjects[obj]);
  }
}

EpiloguePlayer.prototype.drawObject = function (sprite) {
  var properties = [
    "scale(" + this.viewportWidth / this.activeScene.width * this.camera.zoom + ")",
    "translate(" + this.toViewX(sprite.x) + ", " + this.toViewY(sprite.y) + ")"
  ];
  var transform = properties.join(" ");

  $(sprite.element).css({
    "transform": transform,
    "transform-origin": "top left",
    "opacity": sprite.alpha / 100,
  });
  $(sprite.rotElement).css({
    "transform": "rotate(" + sprite.rotation + "deg) scale(" + sprite.scale + ")",
  });
}

EpiloguePlayer.prototype.toViewX = function (x) {
  var sceneWidth = this.camera.width;
  var offset = sceneWidth / this.camera.zoom / 2 - sceneWidth / 2 + x - this.camera.x;
  return offset + "px";
}

EpiloguePlayer.prototype.toViewY = function (y) {
  var sceneHeight = this.camera.height;
  var offset = sceneHeight / this.camera.zoom / 2 - sceneHeight / 2 + y - this.camera.y;
  return offset + "px";
}


/** Advances to the next scene if there is one */
EpiloguePlayer.prototype.advanceScene = function () {
  this.sceneIndex++;
  if (this.sceneIndex < this.epilogue.scenes.length) {
    this.setupScene(this.sceneIndex);
  }
}

EpiloguePlayer.prototype.setupScene = function (index) {
  var context = {};

  this.haltAnimations(true);

  //clear old textboxes
  this.clearAllText(context);

  //clear old images
  for (var obj in this.sceneObjects) {
    $(this.sceneObjects[obj].element).remove();
  }
  this.sceneObjects = {};

  this.activeScene = this.epilogue.scenes[index];
  this.directiveIndex = -1;

  if (!this.activeScene.width) {
    //if no scene dimensions were provided, use the background image's dimensions
    var backgroundImg = this.assetMap[this.activeScene.background];
    if (backgroundImg) {
      this.activeScene.width = backgroundImg.naturalWidth;
      this.activeScene.height = backgroundImg.naturalHeight;
      this.activeScene.aspectRatio = backgroundImg.naturalWidth / backgroundImg.naturalHeight;

      //backwards compatibility: if the new aspect ratio is flipped, we probably don't want to use it. Use the previous scene size instead
      if (this.sceneIndex > 0) {
        var previousScene = this.epilogue.scenes[this.sceneIndex - 1];
        if (previousScene.aspectRatio >= 1 && this.activeScene.aspectRatio < 1 || previousScene.aspectRatio < 1 && this.activeScene.aspectRatio >= 1) {
          this.activeScene.width = previousScene.width;
          this.activeScene.height = previousScene.height;
          this.activeScene.aspectRatio = previousScene.aspectRatio;
        }
      }      
    }
  }

  this.camera = {
    x: isNaN(this.activeScene.x) ? 0 : toSceneX(this.activeScene.x, this.activeScene),
    y: isNaN(this.activeScene.y) ? 0 : toSceneY(this.activeScene.y, this.activeScene),
    width: this.activeScene.width,
    height: this.activeScene.height,
    zoom: this.activeScene.zoom || 1,
  }

  var overlayColor;
  var overlayAlpha = 0;
  if (this.activeScene.overlayAlpha) {
    overlayAlpha = parseInt(this.activeScene.overlayAlpha, 10);
  }
  if (this.activeScene.overlayColor) {
    this.setOverlay(this.fromHex(this.activeScene.overlayColor), overlayAlpha);
  }

  //fit the viewport based on the scene's aspect ratio and the window size
  this.resizeViewport();

  if (this.activeScene.background) {
    this.addBackground(this.activeScene.background);
  }
  this.$viewport.css("background-color", this.activeScene.color);

  this.performDirective();
}

EpiloguePlayer.prototype.resizeViewport = function () {
  if (!this.activeScene) {
    return;
  }

  var windowHeight = $(window).height();
  var windowWidth = $(window).width();

  var viewWidth = this.activeScene.aspectRatio * windowHeight;
  if (viewWidth > windowWidth) {
    //take full width of window
    this.viewportWidth = windowWidth;
    this.viewportHeight = windowWidth / this.activeScene.aspectRatio;
    this.$viewport.width(windowWidth);
    this.$viewport.height(this.viewportHeight);
  }
  else {
    //take full height of window
    this.viewportWidth = viewWidth;
    this.viewportHeight = windowHeight;
    this.$viewport.width(this.viewportWidth);
    this.$viewport.height(this.viewportHeight);
  }

  this.draw();
}

EpiloguePlayer.prototype.haltAnimations = function (haltLooping) {
  this.waitingForAnims = false;
  var animloop = this.anims.slice();
  var j = 0;
  for (var i = 0; i < animloop.length; i++) {
    if (haltLooping || !animloop[i].looped) {
      animloop[i].halt();
      this.anims.splice(j, 1);
    }
    else {
      j++;
    }
  }
  this.draw();
}

EpiloguePlayer.prototype.advanceDirective = function () {
  this.haltAnimations(false);
  this.performDirective();
}

EpiloguePlayer.prototype.performDirective = function () {
  this.directiveIndex++;
  if (this.directiveIndex < this.activeScene.directives.length) {
    var directive = this.activeScene.directives[this.directiveIndex];
    switch (directive.type) {
      case "sprite":
        this.addAction(directive, this.addSprite, this.removeSprite);
        break;
      case "text":
        this.addAction(directive, this.addText, this.removeText);
        break;
      case "clear":
        this.addAction(directive, this.clearText, this.restoreText);
        break;
      case "move":
        this.addAction(directive, this.moveSprite, this.returnSprite);
        break;
      case "camera":
        this.addAction(directive, this.moveCamera, this.returnCamera);
        break;
      case "fade":
        this.addAction(directive, this.fade, this.restoreOverlay);
        break;
      case "stop":
        this.addAction(directive, this.stopAnimation, this.restoreAnimation);
        break;
      case "wait":
        this.addAction(directive, this.awaitAnims, function () { });
        return;
      case "pause":
        return;
    }

    this.performDirective();
  }
  else {
    this.advanceScene();
  }
}

/**
 * Adds an undoable action to the history
 * @param {any} context Context to pass to do and undo functions
 * @param {Function} doFunc Function to perform the directive
 * @param {Function} undoFunc Function to undo the directive
 */
EpiloguePlayer.prototype.addAction = function (directive, doFunc, undoFunc) {
  var context = {}; //contextual information for the do action to store off that the revert action can refer to
  var action = { directive: directive, context: context, perform: createClosure(this, doFunc), revert: createClosure(this, undoFunc) };
  directive.action = action;
  action.perform(directive, context);
}

/**
 * Reverts all changes up until the last "pause" directive
 */
EpiloguePlayer.prototype.revertDirective = function () {
  this.haltAnimations(true);

  var canRevert = (this.sceneIndex > 0);
  if (!canRevert) {
    //on the initial scene, make sure there is a pause directive to revert to. Otherwise we can't rewind any further
    for (var i = this.directiveIndex - 1; i >= 0; i--) {
      if (this.activeScene.directives[i].type === "pause") {
        canRevert = true;
        break;
      }
    }
  }

  if (!canRevert) { return; }

  for (var i = this.directiveIndex - 1; i >= 0; i--) {
    this.directiveIndex = i;
    var directive = this.activeScene.directives[i];
    if (directive.action) {
      directive.action.revert(directive, directive.action.context);
    }
    if (directive.type === "pause") {
      return;
    }
  }

  //reached the start of the scene, so time to back up an entire scene

  //it would be better to make scene setup/teardown an undoable action, but for a quick and dirty method for now, just fast forward the whole scene to its last pause
  this.sceneIndex--;
  this.setupScene(this.sceneIndex);
  while (this.directiveIndex < this.activeScene.directives.length - 1) {
    this.advanceDirective();
  }
}

EpiloguePlayer.prototype.addBackground = function (background) {
  var img = this.assetMap[background];
  this.addImage("background", background, { x: 0, y: 0, width: img.naturalWidth + "px", height: img.naturalHeight + "px" });
}

EpiloguePlayer.prototype.addImage = function (id, src, args) {
  var vehicle = document.createElement("div");
  vehicle.id = id;
  var img = document.createElement("img");
  img.src = this.assetMap[src].src;
  vehicle.appendChild(img);

  var x = args.x;
  var y = args.y;
  var width = args.width;
  var height = args.height;
  var scale = args.scale;
  var rotation = args.rotation;
  var alpha = args.alpha;

  var obj = {
    element: vehicle,
    rotElement: img,
    x: x,
    y: y,
    scale: scale || 1,
    rotation: rotation || 0,
    alpha: alpha || 100,
  };
  if (width) {
    if (width.endsWith("%")) {
      obj.widthPct = parseInt(width, 10) / 100;
    }
    else {
      obj.widthPct = parseInt(width, 10) / this.activeScene.width;
    }
    if (!height) {
      obj.heightPct = img.naturalHeight / img.naturalWidth * obj.widthPct * this.activeScene.aspectRatio;
    }
  }
  else {
    obj.widthPct = img.naturalWidth / this.activeScene.width;
  }
  if (height) {
    if (height.endsWith("%")) {
      obj.heightPct = parseInt(height, 10) / 100;
    }
    else {
      obj.heightPct = parseInt(height, 10) / this.activeScene.height;
    }
    if (!width) {
      obj.widthPct = img.naturalWidth / img.naturalHeight * obj.heightPct / this.activeScene.aspectRatio;
    }
  }
  else if (!obj.heightPct) {
    obj.heightPct = img.naturalHeight / this.activeScene.height;
  }

  $(vehicle).css(
    {
      position: "absolute",
      left: 0,
      top: 0,
      width: obj.widthPct * this.activeScene.width,
      height: obj.heightPct * this.activeScene.height,
    });
  this.$canvas.append(vehicle);

  this.sceneObjects[id] = obj;
  this.draw();
  return obj;
}

EpiloguePlayer.prototype.addSprite = function (directive) {
  this.addImage(directive.id, directive.src, {
    x: directive.x,
    y: directive.y,
    width: directive.width,
    height: directive.height,
    scale: directive.scale,
    rotation: directive.rotation,
    alpha: directive.alpha,
  });
}

EpiloguePlayer.prototype.removeSprite = function (directive) {
  $(this.sceneObjects[directive.id].element).remove();
  delete this.sceneObjects[directive.id];
}

EpiloguePlayer.prototype.addText = function (directive, context) {
  var id = directive.id;
  context.id = id;
  this.lastTextId = id;

  var box = document.getElementById(id);
  if (box) {
    //resuse the box and just replace the text
    context.oldContent = $(box.firstChild).html();
    if (box.style.display === "none") {
      box.style.display = "";
      context.wasHidden = true;
    }
    $(box.firstChild).html(directive.text);
    return;
  }

  var newEpilogueDiv = $(document.createElement('div')).attr('id', id).addClass('epilogue-text');
  var content = expandDialogue(directive.text, null, players[HUMAN_PLAYER]);

  newEpilogueDiv.html('<span class="dialogue-bubble ' + directive.arrow + '">' + content + '</span>');
  newEpilogueDiv.attr('style', directive.css);

  //use css to position the box
  newEpilogueDiv.css('position', "absolute");
  newEpilogueDiv.css('left', directive.x);
  newEpilogueDiv.css('top', directive.y);
  newEpilogueDiv.css('width', directive.width);

  newEpilogueDiv.data("directive", directive);

  //attach new div element to the content div
  this.epilogueContent.appendChild(newEpilogueDiv[0]);
}

EpiloguePlayer.prototype.removeText = function (directive, context) {
  this.lastTextId = context.id;
  var box = document.getElementById(this.lastTextId);
  if (context.oldContent) {
    $(box.firstChild).html(context.oldContent);
    if (context.wasHidden) {
      box.style.display = "none";
    }
  }
  else {
    this.epilogueContent.removeChild(document.getElementById(this.lastTextId));
  }
}

EpiloguePlayer.prototype.clearAllText = function (context) {
  context = context || {};
  var textBoxes = context.textBoxes = [];
  var $this = this;
  $(this.epilogueContent).children().each(function () {
    var boxContext = {};
    textBoxes.push(boxContext);
    $this.clearText({ id: this.id }, boxContext);
  });
}

EpiloguePlayer.prototype.clearText = function (directive, context) {
  var id = directive.id || this.lastTextId;
  context.id = lastTextId = id;

  //hide the box rather than remove it completely so that it can easily be restored when rewinding
  var box = document.getElementById(id);
  if (box) {
    box.style.display = "none";
  }
}

EpiloguePlayer.prototype.restoreText = function (directive, context) {
  var id = this.lastTextId = context.id;
  var box = document.getElementById(context.id);
  if (box) {
    box.style.display = "";
  }
}

EpiloguePlayer.prototype.interpolate = function (obj, prop, last, next, t) {
  var current = obj[prop];
  var start = last[prop];
  var end = next[prop];
  if (typeof start === "undefined" || isNaN(start) || typeof end === "undefined" || isNaN(end)) {
    return;
  }
  var mode = next.interpolation || "linear";
  obj[prop] = interpolationModes[mode](prop, start, end, t, last.keyframes, last.index)
}

EpiloguePlayer.prototype.updateSprite = function (id, last, next, t) {
  var sprite = this.sceneObjects[id];
  this.interpolate(sprite, "x", last, next, t);
  this.interpolate(sprite, "y", last, next, t);
  this.interpolate(sprite, "rotation", last, next, t);
  this.interpolate(sprite, "scale", last, next, t);
  this.interpolate(sprite, "alpha", last, next, t);
}

EpiloguePlayer.prototype.moveSprite = function (directive, context) {
  var sprite = this.sceneObjects[directive.id];
  if (sprite) {
    var frames = directive.keyframes.slice();
    context.x = sprite.x;
    context.y = sprite.y;
    context.rotation = sprite.rotation;
    context.scale = sprite.scale;
    context.alpha = sprite.alpha;
    frames.unshift(context);
    this.anims.push(new Animation(directive.id, frames, createClosure(this, this.updateSprite), directive.loop));
  }
}

EpiloguePlayer.prototype.returnSprite = function (directive, context) {
  var sprite = this.sceneObjects[directive.id];
  if (sprite) {
    if (typeof context.x !== "undefined") {
      sprite.x = context.x;
    }
    if (typeof context.y !== "undefined") {
      sprite.y = context.y;
    }
    if (typeof context.rotation !== "undefined") {
      sprite.rotation = context.rotation;
    }
    if (typeof context.scale !== "undefined") {
      sprite.scale = context.scale;
    }
    if (typeof context.alpha !== "undefined") {
      sprite.alpha = context.alpha;
    }
    this.draw();
  }
}

EpiloguePlayer.prototype.updateCamera = function (id, last, next, t) {
  this.interpolate(this.camera, "x", last, next, t);
  this.interpolate(this.camera, "y", last, next, t);
  if (last.zoom && next.zoom) {
    this.camera.zoom = lerp(last.zoom, next.zoom, t);
  }
}

EpiloguePlayer.prototype.moveCamera = function (directive, context) {
  var frames = directive.keyframes.slice();
  context.x = this.camera.x;
  context.y = this.camera.y;
  context.zoom = this.camera.zoom;
  frames.unshift(context);
  this.anims.push(new Animation("camera", frames, createClosure(this, this.updateCamera), directive.loop));
}

EpiloguePlayer.prototype.returnCamera = function (directive, context) {
  if (typeof context.x !== "undefined") {
    this.camera.x = context.x;
  }
  if (typeof context.y !== "undefined") {
    this.camera.y = context.y;
  }
  if (context.zoom) {
    this.camera.zoom = context.zoom;
  }
  this.draw();
}

EpiloguePlayer.prototype.fromHex = function (hex) {
  var value = parseInt(hex.substring(1), 16);
  var r = (value & 0xff0000) >> 16;
  var g = (value & 0x00ff00) >> 8;
  var b = (value & 0x0000ff);
  return [r, g, b];
}

EpiloguePlayer.prototype.toHexPiece = function (v) {
  var hex = Math.round(v).toString(16);
  if (hex.length < 2) {
    hex = "0" + hex;
  }
  return hex;
}

EpiloguePlayer.prototype.toHex = function (rgb) {
  return "#" + this.toHexPiece(rgb[0]) + this.toHexPiece(rgb[1]) + this.toHexPiece(rgb[2]);
}

EpiloguePlayer.prototype.updateOverlay = function (id, last, next, t) {
  if (typeof next.color !== "undefined") {
    var rgb1 = this.fromHex(last.color);
    var rgb2 = this.fromHex(next.color);

    var rgb = [0, 0, 0];
    for (var i = 0; i < rgb.length; i++) {
      rgb[i] = lerp(rgb1[i], rgb2[i], t);
    }
  }
  var alpha = lerp(last.alpha, next.alpha, t);

  this.setOverlay(rgb, alpha);
}

EpiloguePlayer.prototype.fade = function (directive, context) {
  var color = this.toHex(this.overlay.rgb);
  var frames = directive.keyframes.slice();
  context.color = color;
  context.alpha = this.overlay.a;
  frames.unshift(context);
  this.anims.push(new Animation("fade", frames, createClosure(this, this.updateOverlay), directive.loop));
}

EpiloguePlayer.prototype.setOverlay = function (color, alpha) {
  if (typeof color !== "undefined") {
    this.overlay.rgb = color;
  }
  this.overlay.a = alpha;
  this.$overlay.css({
    "opacity": alpha / 100,
    "background-color": this.toHex(this.overlay.rgb)
  });
}

EpiloguePlayer.prototype.restoreOverlay = function (directive, context) {
  this.setOverlay(context.color, context.alpha);
}

EpiloguePlayer.prototype.awaitAnims = function (directive, context) {
  for (var i = 0; i < this.anims.length; i++) {
    if (!this.anims[i].looped) {
      this.waitingForAnims = true;
      return;
    }
  }
  this.advanceDirective();
}

EpiloguePlayer.prototype.stopAnimation = function (directive, context) {
  var anim;
  var id = directive.id;
  context.haltedAnims = [];
  for (var i = this.anims.length - 1; i >= 0; i--) {
    anim = this.anims[i];
    if (anim.id === id) {
      anim.halt();
      this.anims.splice(i, 1);
      context.haltedAnims.push(anim);
      this.draw();
    }
  }
}

EpiloguePlayer.prototype.restoreAnimation = function (directive, context) {
  var haltedAnims = context.haltedAnims;
  for (var i = 0; i < haltedAnims.length; i++) {
    var anim = haltedAnims[i];
    anim.elapsed = 0;
    this.anims.push(anim);
  }
}