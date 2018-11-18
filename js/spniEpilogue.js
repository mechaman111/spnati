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

// Attach some event listeners
var previousButton = document.getElementById('epilogue-previous');
var nextButton = document.getElementById('epilogue-next');
previousButton.addEventListener('click', function(e) {
  e.preventDefault();
  e.stopPropagation();
  progressEpilogue(-1);
});
nextButton.addEventListener('click', function(e) {
  e.preventDefault();
  e.stopPropagation();
  progressEpilogue(1);
});
document.getElementById('epilogue-restart').addEventListener('click', function(e) {
  e.preventDefault();
  e.stopPropagation();
  showRestartModal();
});
document.getElementById('epilogue-buttons').addEventListener('click', function() {
  if (!previousButton.disabled) {
    progressEpilogue(-1);
  }
});
epilogueContent.addEventListener('click', function() {
  if (!nextButton.disabled) {
    progressEpilogue(1);
  }
});

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
  }).each(parseEpilogue.bind(null, player));

	return epilogues;
}

function parseEpilogue(player, rawEpilogue) {
  //use parseXML() so that <image> tags come through properly
  //not using parseXML() because internet explorer doesn't like it

  var title = $(rawEpilogue).find("title").html().trim();
  var ratio = [4, 3];
  try {
    var rawRatio = $(rawEpilogue).attr('ratio');
    if (rawRatio) {
      rawRatio = rawRatio.split(':');
      ratio = [parseFloat(rawRatio[0]), parseFloat(rawRatio[1])];
    }
  } catch(e) {
    console.error('Failed reading epilogue ratio: ', $(rawEpilogue).attr('ratio'))
  }

  var screens = []; //the list of screens for the epilogue

  // Leaving this for backwards compatibility, screens are hereby depreciated
  $(rawEpilogue).find("screen").each(function() {
    var image = player.base_folder + $(this).attr("img").trim(); //get the full path for the screen's image
    //use an attribute rather than a tag because IE doesn't like parsing XML

    var textBoxes = parseSceneContent(player, $(this)).textBoxes;

    screens.push({image, textBoxes}); //add a screen object to the list of screens
  });


  var backgrounds = [];
  $(rawEpilogue).find('background').each(function() {
    var image = $(this).attr('img').trim();
    image = image.charAt(0) === '/' ? image : player.base_folder + image;

    var scenes = [];
    $(this).find('scene').each(function() {
      scenes.push(parseSceneContent(player, $(this)));
    });

    backgrounds.push({image, scenes});
  });

  var epilogue = {player, title, ratio, screens, backgrounds}; //epilogue object

  if (!epilogue.backgrounds.length && !epilogue.screens.length) {
    return;
  }

  return epilogue;
}

function parseSceneContent(player, scene) {

  var images = [];
  var textBoxes = [];

  var backgroundTransform = [scene.attr('background-position-x') || 0, scene.attr('background-position-y') || 0, scene.attr('background-zoom') || 0];
  try {
    backgroundTransform[0] = parseFloat(backgroundTransform[0]) * -1;
    backgroundTransform[1] = parseFloat(backgroundTransform[1]) * -1;
    backgroundTransform[2] = parseFloat(backgroundTransform[2]) / 100 + 1;
  } catch(e) {}
  backgroundTransform = 'translate(' + backgroundTransform[0] + '%,' + backgroundTransform[1] + '%) scale(' + backgroundTransform[2] + ');';
  // Find the image data for this shot
  scene.find('sprite').each(function() {
    var x = $(this).find("x").html().trim();
    var y = $(this).find("y").html().trim();
    var width = $(this).find("width").html().trim();
    var src = $(this).find('src').html().trim();

    src = src.charAt(0) === '/' ? src : player.base_folder + src;

    images.push({x, y, width, src});
  });

  //get the information for all the text boxes
  scene.find("text").each(function() {

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
    if (x.toLowerCase() == "centered") {
      x = getCenteredPosition(w);
    }

    var text = $(this).find("content").html().trim(); //the actual content of the text box

    textBoxes.push({x, y, width:w, arrow:a, text}); //add a textBox object to the list of textBoxes
  });

  return {images, textBoxes, backgroundTransform};
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

	progressEpilogue(1); //initialise buttons and text boxes
	screenTransition($titleScreen, $epilogueScreen); //currently transitioning from title screen, because this is for testing
	$epilogueSelectionModal.modal("hide");
}

/************************************************************
 * Draw Epilogue Text Box num for the current screen
 ************************************************************/
function drawEpilogueBox(data) {
  if (!data) {
    return;
  }

  var imgOrTxt = data.src ? 'image' : 'text';
  var idNum = document.getElementsByClassName('epilogue-' + imgOrTxt).length;

  //make new div element
  var newEpilogueDiv = $(document.createElement('div')).attr('id', 'epilogue-' + imgOrTxt + '-' + idNum).addClass('epilogue-' + imgOrTxt);

  switch (imgOrTxt) {
    case 'image':
      newEpilogueDiv.html('<img src="' + data.src + '">')
      break;
    case 'text':
      var content = expandDialogue(data.text, null, players[HUMAN_PLAYER]);

      newEpilogueDiv.html('<span class="dialogue-bubble ' + data.arrow + '">' + content + '</span>');
      break;
  }

  //use css to position the box
  newEpilogueDiv.css('position', "absolute");
  newEpilogueDiv.css('left', data.x);
  newEpilogueDiv.css('top', data.y);
  newEpilogueDiv.css('width', data.width);

  //attach new div element to the content div
  epilogueContent.appendChild(newEpilogueDiv[0]);
}

/************************************************************
 * Move the Epilogue forwards and backwards.
 ************************************************************/

function progressEpilogue(direction) {
  var activeText = document.getElementsByClassName('epilogue-text').length;
  var datastore = epilogueContainer.dataset;

  if (!epilogueContainer.getAttribute('style')) {
    var ratio = chosenEpilogue.ratio;
    epilogueContainer.setAttribute('style', 'max-width:' + ratio[0] / ratio[1] * 100 + 'vh; height:' + ratio[1] / ratio[0] * 100 + 'vw;');
  }

  // default all buttons and disable/enable conditionally later
  var $epiloguePrevButton = $('#epilogue-buttons > #epilogue-previous');
  var $epilogueNextButton = $('#epilogue-buttons > #epilogue-next');
  var $epilogueRestartButton = $('#epilogue-buttons > #epilogue-restart');
  $epiloguePrevButton.prop("disabled", false);
  $epilogueNextButton.prop("disabled",  false);
  $epilogueRestartButton.prop("disabled", true);

  /////////////////////////////////////////
  // This bit is only for backwards compatibility, please remove it once we're rid of all the old epilogues
  /////////////////////////////////////////
  if (chosenEpilogue.screens.length) {
    var currentScreen = chosenEpilogue.screens[datastore.scene];
    if (direction > 0) {
      if (currentScreen && currentScreen.textBoxes.length > activeText) {
        // Forward same screen
        drawEpilogueBox(currentScreen.textBoxes[activeText++]);
      } else {
        // Forward and changing screens
        $(epilogueContent).children('.epilogue-text').remove();
        datastore.scene++;
        currentScreen = chosenEpilogue.screens[datastore.scene];
        $(epilogueContent).children('.epilogue-background').attr('src', currentScreen.image)
        drawEpilogueBox(currentScreen.textBoxes && currentScreen.textBoxes[0]);
        activeText = 1;
      }
    } else if (direction < 0) {
      if (activeText > 1) {
        // Backwards same screen
        epilogueContent.removeChild(document.getElementById('epilogue-text-' + (--activeText)));
      } else {
        // Backwards and changing screens
        $(epilogueContent).children('.epilogue-text').remove();
        datastore.scene--;
        currentScreen = chosenEpilogue.screens[datastore.scene];
        $(epilogueContent).children('.epilogue-background').attr('src', currentScreen.image);
        currentScreen.textBoxes.forEach(drawEpilogueBox);
        activeText = document.getElementsByClassName('epilogue-text').length
      }
    }

    // disable buttons
    if (datastore.scene <= 0 && activeText <= 1) {
      $epiloguePrevButton.prop('disabled', true);
    }
    if (datastore.scene >= chosenEpilogue.screens.length - 1 && activeText >= currentScreen.textBoxes.length) {
      $epilogueNextButton.prop('disabled', true);
      $epilogueRestartButton.prop('disabled', false);
    }
  } else {
    /////////////////////////////////////////
    // End backwards compatibility
    /////////////////////////////////////////

    var currentBackground = chosenEpilogue.backgrounds[datastore.background];
    var currentScene = currentBackground && currentBackground.scenes[datastore.scene];
    if (direction > 0) {
      if (currentScene && currentScene.textBoxes.length > activeText) {
        // Forward same scene
        drawEpilogueBox(currentScene.textBoxes[activeText++]);
      } else {
        // Forward new scene
        datastore.scene++;
        if (!currentBackground || datastore.scene >= currentBackground.scenes.length) {
          // New background!
          datastore.background++;
          currentBackground = chosenEpilogue.backgrounds[datastore.background];
          datastore.scene = 0;
          currentScene = currentBackground.scenes[datastore.scene];
          $(epilogueContent).children('.epilogue-background').attr('src', currentBackground.image).siblings().remove();
          currentScene.images.forEach(drawEpilogueBox);
          drawEpilogueBox(currentScene.textBoxes[0]);
        } else {
          // New scene, same background
          datastore.scene++;
          currentScene = currentBackground.scenes[datastore.scene];
          $(epilogueContent).children('.epilogue-background').siblings().remove();
          currentScene.images.forEach(drawEpilogueBox);
          drawEpilogueBox(currentScene.textBoxes[0]);
        }
        activeText = 1;

        $(epilogueContent).children('.epilogue-background').attr('style', 'transform:' + currentScene.backgroundTransform);
      }
    } else if (direction < 0) {
      if (activeText > 1) { // TODO: figure out how to make this more flexible if writers want multiple texts to appear at once or some such
        // Backwards same scene
        epilogueContent.removeChild(document.getElementById('epilogue-text-' + (--activeText)));
      } else {
        if (datastore.scene) {
          // Backwards new scene same background
          datastore.scene--;
          currentScene = currentBackground.scenes[datastore.scene];
          $(epilogueContent).children('.epilogue-background').siblings().remove();
          currentScene.images.forEach(drawEpilogueBox);
          currentScene.textBoxes.forEach(drawEpilogueBox);
        } else {
          // Backwards new background! (old background?)
          datastore.background--;
          currentBackground = chosenEpilogue.backgrounds[datastore.background];
          datastore.scene = currentBackground.scenes.length - 1;
          currentScene = currentBackground.scenes[datastore.background];
          $(epilogueContent).children('.epilogue-background').attr('src', currentBackground.image).siblings().remove();
          currentScene.images.forEach(drawEpilogueBox);
          currentScene.textBoxes.forEach(drawEpilogueBox);
        }
        activeText = document.getElementsByClassName('epilogue-text').length;

        $(epilogueContent).children('.epilogue-background').attr('style', 'transform:' + currentScene.backgroundTransform);
      }
    }

    if (datastore.background <= 0 && datastore.scene <= 0 && activeText <= 1) {
      $epiloguePrevButton.prop('disabled', true);
    }
    if (datastore.background >= chosenEpilogue.backgrounds.length - 1 &&
      datastore.scene >= currentBackground.scenes.length - 1 &&
      activeText >= currentScene.textBoxes.length) {
      $epilogueNextButton.prop('disabled', true);
      $epilogueRestartButton.prop('disabled', false);
    }
  }
}
