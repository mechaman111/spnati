/********************************************************************************
 This file contains the variables and functions that form the select screens of
 the game. The parsing functions for the opponent.xml file.
 ********************************************************************************/

/**********************************************************************
 *****               Opponent & Group Specification               *****
 **********************************************************************/

/**************************************************
 * Stores meta information about opponents.
 **************************************************/
function createNewOpponent (id, enabled, first, last, label, image, gender,
                            height, source, artist, writer, description,
                            ending, layers, release, tags) {
	var newOpponentObject = {id:id,
							 folder:'opponents/'+id+'/',
							 enabled:enabled,
                             first:first,
							 last:last,
							 label:label,
							 image:image,
                             gender:gender,
							 height:height,
							 source:source,
                             artist:artist,
                             writer:writer,
							 description:description,
                             ending:ending,
                             layers:layers,
							 tags:tags,
                             release:parseInt(release)};

	return newOpponentObject;
}

/**************************************************
 * Stores meta information about groups.
 **************************************************/
function createNewGroup (title, opponents) {
	var newGroupObject = {title:title,
						  opponents:opponents};

	return newGroupObject;
}

/**********************************************************************
 *****                  Select Screen UI Elements                 *****
 **********************************************************************/

/* main select screen */
$selectTable = $("#select-table");
$selectBubbles = [$("#select-bubble-1"),
                  $("#select-bubble-2"),
                  $("#select-bubble-3"),
                  $("#select-bubble-4")];
$selectDialogues = [$("#select-dialogue-1"),
                    $("#select-dialogue-2"),
                    $("#select-dialogue-3"),
                    $("#select-dialogue-4")];
$selectAdvanceButtons = [$("#select-advance-button-1"),
                         $("#select-advance-button-2"),
                         $("#select-advance-button-3"),
                         $("#select-advance-button-4")];
$selectImages = [$("#select-image-1"),
                 $("#select-image-2"),
                 $("#select-image-3"),
                 $("#select-image-4")];
$selectLabels = [$("#select-name-label-1"),
                 $("#select-name-label-2"),
                 $("#select-name-label-3"),
                 $("#select-name-label-4")];
$selectButtons = [$("#select-slot-button-1"),
                  $("#select-slot-button-2"),
                  $("#select-slot-button-3"),
                  $("#select-slot-button-4")];
$selectMainButton = $("#main-select-button");
$selectRandomButtons = [$("#select-random-button"), $("#select-random-female-button"), $("#select-random-male-button")];
$selectRemoveAllButton = $("#select-remove-all-button");

/* individual select screen */
$individualSelectTable = $("#individual-select-table");
$individualNameLabels = [$("#individual-name-label-1"), $("#individual-name-label-2"), $("#individual-name-label-3"), $("#individual-name-label-4")];
$individualPrefersLabels = [$("#individual-prefers-label-1"), $("#individual-prefers-label-2"), $("#individual-prefers-label-3"), $("#individual-prefers-label-4")];
$individualSexLabels = [$("#individual-sex-label-1"), $("#individual-sex-label-2"), $("#individual-sex-label-3"), $("#individual-sex-label-4")];
$individualHeightLabels = [$("#individual-height-label-1"), $("#individual-height-label-2"), $("#individual-height-label-3"), $("#individual-height-label-4")];
$individualSourceLabels = [$("#individual-source-label-1"), $("#individual-source-label-2"), $("#individual-source-label-3"), $("#individual-source-label-4")];
$individualWriterLabels = [$("#individual-writer-label-1"), $("#individual-writer-label-2"), $("#individual-writer-label-3"), $("#individual-writer-label-4")];
$individualArtistLabels = [$("#individual-artist-label-1"), $("#individual-artist-label-2"), $("#individual-artist-label-3"), $("#individual-artist-label-4")];
$individualCountBoxes = [$("#individual-counts-1"), $("#individual-counts-2"), $("#individual-counts-3"), $("#individual-counts-4")];
$individualLineCountLabels = [$("#individual-line-count-label-1"), $("#individual-line-count-label-2"), $("#individual-line-count-label-3"), $("#individual-line-count-label-4")];
$individualPoseCountLabels = [$("#individual-pose-count-label-1"), $("#individual-pose-count-label-2"), $("#individual-pose-count-label-3"), $("#individual-pose-count-label-4")];
$individualDescriptionLabels = [$("#individual-description-label-1"), $("#individual-description-label-2"), $("#individual-description-label-3"), $("#individual-description-label-4")];
$individualBadges = [$("#individual-badge-1"), $("#individual-badge-2"), $("#individual-badge-3"), $("#individual-badge-4")];
$individualLayers = [$("#individual-layer-1"), $("#individual-layer-2"), $("#individual-layer-3"), $("#individual-layer-4")];

$individualImages = [$("#individual-image-1"), $("#individual-image-2"), $("#individual-image-3"), $("#individual-image-4")];
$individualButtons = [$("#individual-button-1"), $("#individual-button-2"), $("#individual-button-3"), $("#individual-button-4")];

$individualPageIndicator = $("#individual-page-indicator");
$individualMaxPageIndicator = $("#individual-max-page-indicator");

$individualCreditsButton = $('.individual-credits-btn');

/* group select screen */
$groupSelectTable = $("#group-select-table");
$groupNameLabels = [$("#group-name-label-1"), $("#group-name-label-2"), $("#group-name-label-3"), $("#group-name-label-4")];
$groupPrefersLabels = [$("#group-prefers-label-1"), $("#group-prefers-label-2"), $("#group-prefers-label-3"), $("#group-prefers-label-4")];
$groupSexLabels = [$("#group-sex-label-1"), $("#group-sex-label-2"), $("#group-sex-label-3"), $("#group-sex-label-4")];
$groupHeightLabels = [$("#group-height-label-1"), $("#group-height-label-2"), $("#group-height-label-3"), $("#group-height-label-4")];
$groupSourceLabels = [$("#group-source-label-1"), $("#group-source-label-2"), $("#group-source-label-3"), $("#group-source-label-4")];
$groupWriterLabels = [$("#group-writer-label-1"), $("#group-writer-label-2"), $("#group-writer-label-3"), $("#group-writer-label-4")];
$groupArtistLabels = [$("#group-artist-label-1"), $("#group-artist-label-2"), $("#group-artist-label-3"), $("#group-artist-label-4")];
$groupCountBoxes = [$("#group-counts-1"), $("#group-counts-2"), $("#group-counts-3"), $("#group-counts-4")];
$groupLineCountLabels = [$("#group-line-count-label-1"), $("#group-line-count-label-2"), $("#group-line-count-label-3"), $("#group-line-count-label-4")];
$groupPoseCountLabels = [$("#group-pose-count-label-1"), $("#group-pose-count-label-2"), $("#group-pose-count-label-3"), $("#group-pose-count-label-4")];
$groupDescriptionLabels = [$("#group-description-label-1"), $("#group-description-label-2"), $("#group-description-label-3"), $("#group-description-label-4")];
$groupBadges = [$("#group-badge-1"), $("#group-badge-2"), $("#group-badge-3"), $("#group-badge-4")];
$groupLayers = [$("#group-layer-1"), $("#group-layer-2"), $("#group-layer-3"), $("#group-layer-4")];

$groupImages = [$("#group-image-1"), $("#group-image-2"), $("#group-image-3"), $("#group-image-4")];
$groupNameLabel = $("#group-name-label");
$groupButton = $("#group-button");

$groupPageIndicator = $("#group-page-indicator");
$groupMaxPageIndicator = $("#group-max-page-indicator");

$groupCreditsButton = $('.group-credits-btn');

$searchName = $("#search-name");
$searchSource = $("#search-source");
$searchTag = $("#search-tag");
$searchGenderOptions = [$("#search-gender-1"), $("#search-gender-2"), $("#search-gender-3")];

$sortingOptionsItems = $(".sort-dropdown-options li");

/**********************************************************************
 *****                  Select Screen Variables                   *****
 **********************************************************************/

/* hidden variables */
var mainSelectHidden = false;
var singleSelectHidden = false;
var groupSelectHidden = false;

/* opponent listing file */
var listingFile = "opponents/listing.xml";
var metaFile = "meta.xml";

/* opponent information storage */
var loadedOpponents = [];
var selectableOpponents = loadedOpponents;
var hiddenOpponents = [];
var loadedGroups;

/* page variables */
var groupSelectScreen = 0;
var individualPage = 0;
var groupPage = [0, 0];
var chosenGender = -1;
var sortingMode = "Featured";
var sortingOptionsMap = {
    "Newest" : sortOpponentsByMultipleFields("-release"),
    "Oldest" : sortOpponentsByMultipleFields("release"),
    "Most Layers" : sortOpponentsByMultipleFields("-layers"),
    "Fewest Layers" : sortOpponentsByMultipleFields("layers"),
    "Name (A-Z)" : sortOpponentsByMultipleFields("first", "last"),
    "Name (Z-A)" : sortOpponentsByMultipleFields("-first", "-last"),
};
var individualCreditsShown = false;
var groupCreditsShown = false;

/* consistence variables */
var selectedSlot = 0;
var individualSlot = 0;
var shownIndividuals = [null, null, null, null];
var shownGroup = [null, null, null, null];
var randomLock = false;

/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Loads all of the content required to display the title
 * screen.
 ************************************************************/
function loadSelectScreen () {
    loadListingFile();

	updateSelectionVisuals();
}

/************************************************************
 * Loads and parses the main opponent listing file.
 ************************************************************/
function loadListingFile () {
	/* clear the previous meta information */
	loadedOpponents = [];
	loadedGroups = loadedGroups = [[], []];
	var outstandingLoads = 0;
	var onComplete = function() {
		if (--outstandingLoads == 0) {
			/* Remove any slots that failed to load */
			loadedOpponents = loadedOpponents.filter(function(x) { return x !== null; });
			selectableOpponents = loadedOpponents.slice();
		}
	}

	/* grab and parse the opponent listing file */
	$.ajax({
        type: "GET",
		url: listingFile,
		dataType: "text",
		success: function(xml) {
			/* start by parsing and loading the individual listings */
            var oppDefaultIndex = 0; // keep track of an opponent's default placement

			$individualListings = $(xml).find('individuals');
			$individualListings.find('opponent').each(function () {
                if ($(this).attr('status') === undefined || includedOpponentStatuses[$(this).attr('status')]) {
                    var id = $(this).text();
                    console.log("Reading \""+id+"\" from listing file");
                    outstandingLoads++;
                    loadOpponentMeta(id, loadedOpponents, oppDefaultIndex++, onComplete);
                }
			});

			/* end by parsing and loading the group listings */
			$groupListings = $(xml).find('groups');
			$groupListings.find('group').each(function () {
				var title = $(this).attr('title');
				var opp1 = $(this).attr('opp1');
				var opp2 = $(this).attr('opp2');
				var opp3 = $(this).attr('opp3');
				var opp4 = $(this).attr('opp4');

				var newGroup = createNewGroup(title, [opp1, opp2, opp3, opp4]);
				outstandingLoads += 4;
				loadGroupMeta($(this).attr('testing') ? 1 : 0, newGroup, onComplete);
			});
		}
	});
}

/************************************************************
* Loads the meta information for an entire group.
************************************************************/
function loadGroupMeta (groupSelectScreen, group, onComplete) {
 /* parse the individual information of each group member */
 loadedGroups[groupSelectScreen].push(group);

 for (var i = 0; i < 4; i++) {
   loadOpponentMeta(group.opponents[i], group.opponents, i, onComplete);
 }
}

/************************************************************
 * Loads and parses the meta XML file of an opponent.
 ************************************************************/
function loadOpponentMeta (id, targetArray, index, onComplete) {
	/* grab and parse the opponent meta file */
	$.ajax({
        type: "GET",
		url: 'opponents/' + id + '/' + metaFile,
		dataType: "text",
		success: function(xml) {
			/* grab all the info for this listing */
			var enabled = $(xml).find('enabled').text();
			var first = $(xml).find('first').text();
			var last = $(xml).find('last').text();
			var label = $(xml).find('label').text();
			var pic = $(xml).find('pic').text();
			var gender = $(xml).find('gender').text();
			var height = $(xml).find('height').text();
			var from = $(xml).find('from').text();
			var artist = $(xml).find('artist').text();
			var writer = $(xml).find('writer').text();
			var description = $(xml).find('description').text();
            var ending = $(xml).find('has_ending').text() === "true";
            var layers = $(xml).find('layers').text();
            var release = $(xml).find('release').text();
			var tags = $(xml).find('tags').children().map(function() { return $(this).text(); }).get();

			var opponent = createNewOpponent(id, enabled, first, last,
                                             label, pic, gender, height, from,
                                             artist, writer, description,
                                             ending, layers, release, tags);

			/* add the opponent to the list */
            if (index !== undefined) {
                // enforces opponent default order according to listing file
                // (instead of order being determined by when the AJAX call completes)
                targetArray[index] = opponent;
            }
            else {
                targetArray.push(opponent);
            }
            onComplete();
      		},
      		error: function(err) {
				console.log("Failed reading \""+id+"\"");
      			if (index !== undefined) {
      				targetArray[index] = null;
      			}
      			onComplete();
		}
	});
}

/************************************************************
 * Loads opponents onto the individual select screen based
 * on the currently selected page.
 ************************************************************/
function updateIndividualSelectScreen () {
	/* safety wrap around */
	if (individualPage < 0) {
		/* wrap to last page */
		individualPage = Math.ceil(selectableOpponents.length/4)-1;
	}
	$individualPageIndicator.val(individualPage+1);

	/* keep track of how many opponents were on this screen */
	var empty = 0;

    /* create and load all of the individual opponents */
	for (var i = individualPage*4; i < (individualPage+1)*4; i++) {
		var index = i - individualPage*4;

		if (i < selectableOpponents.length) {
			shownIndividuals[index] = selectableOpponents[i];

			$individualNameLabels[index].html(selectableOpponents[i].first + " " + selectableOpponents[i].last);
			$individualPrefersLabels[index].html(selectableOpponents[i].label);
			$individualSexLabels[index].html(selectableOpponents[i].gender);
			$individualSourceLabels[index].html(selectableOpponents[i].source);
			$individualWriterLabels[index].html(wordWrapHtml(selectableOpponents[i].writer));
			$individualArtistLabels[index].html(wordWrapHtml(selectableOpponents[i].artist));
			$individualDescriptionLabels[index].html(selectableOpponents[i].description);

            if (selectableOpponents[i].ending) {
                $individualBadges[index].show();
            }
            else {
                $individualBadges[index].hide();
            }

            $individualLayers[index].show();
            $individualLayers[index].attr("src", "img/layers" + selectableOpponents[i].layers + ".png");

			$individualImages[index].attr('src', selectableOpponents[i].folder + selectableOpponents[i].image);
			$individualImages[index].show();
			if (selectableOpponents[i].enabled == "true") {
				$individualButtons[index].html('Select Opponent');
				$individualButtons[index].attr('disabled', false);
			} else {
				$individualButtons[index].html('Coming Soon');
				$individualButtons[index].attr('disabled', true);
			}
		} else {
			shownIndividuals[index] = null;

			$individualNameLabels[index].html("");
			$individualPrefersLabels[index].html("");
			$individualSexLabels[index].html("");
			$individualSourceLabels[index].html("");
			$individualWriterLabels[index].html("");
			$individualArtistLabels[index].html("");
            $individualCountBoxes[index].css("visibility", "hidden");
			$individualDescriptionLabels[index].html("");
            $individualBadges[index].hide();
            $individualLayers[index].hide();

			$individualImages[index].hide();
			$individualButtons[index].attr('disabled', true);

			empty++;
		}
    }

	/* reload if the page is empty */
	if (empty == 4 && individualPage != 0) {
		individualPage = 0;
		updateIndividualSelectScreen();
	}
}

/************************************************************
 * Loads opponents onto the group select screen based on the
 * currently selected page.
 ************************************************************/
function updateGroupSelectScreen () {
	/* safety wrap around */
  if (groupPage[groupSelectScreen] < 0) {
		/* wrap to last page */
		groupPage[groupSelectScreen] = (loadedGroups[groupSelectScreen].length)-1;
	} else if (groupPage[groupSelectScreen] > loadedGroups[groupSelectScreen].length-1) {
		/* wrap to the first page */
		groupPage[groupSelectScreen] = 0;
	}
	$groupPageIndicator.val(groupPage[groupSelectScreen]+1);

    /* create and load all of the individual opponents */
	for (var i = 0; i < 4; i++) {
		var opponent = loadedGroups[groupSelectScreen][groupPage[groupSelectScreen]].opponents[i];

		if (opponent) {
			shownGroup[i] = opponent;

			$groupNameLabels[i].html(opponent.first + " " + opponent.last);
			$groupPrefersLabels[i].html(opponent.label);
			$groupSexLabels[i].html(opponent.gender);
			$groupSourceLabels[i].html(opponent.source);
			$groupWriterLabels[i].html(wordWrapHtml(opponent.writer));
			$groupArtistLabels[i].html(wordWrapHtml(opponent.artist));
			$groupDescriptionLabels[i].html(opponent.description);

            if (opponent.ending) {
                $groupBadges[i].show();
            }
            else {
                $groupBadges[i].hide();
            }

            $groupLayers[i].show();
            $groupLayers[i].attr("src", "img/layers" + opponent.layers + ".png");

			$groupImages[i].attr('src', opponent.folder + opponent.image);
			$groupImages[i].show();
			$groupNameLabel.html(loadedGroups[groupSelectScreen][groupPage[groupSelectScreen]].title);
			if (opponent.enabled == "true") {
				$groupButton.html('Select Group');
				$groupButton.attr('disabled', false);
			} else {
				$groupButton.html('Unavailable');
				$groupButton.attr('disabled', true);
			}
		} else {
			shownGroup[i] = null;

			$groupNameLabels[i].html("");
			$groupPrefersLabels[i].html("");
			$groupSexLabels[i].html("");
			$groupSourceLabels[i].html("");
			$groupWriterLabels[i].html("");
			$groupArtistLabels[i].html("");
			$groupDescriptionLabels[i].html("");
            $groupBadges[i].hide();
            $groupLayers[i].hide();
			$groupImages[i].hide();
		}
    }
}

/**********************************************************************
 *****                   Interaction Functions                    *****
 **********************************************************************/

/************************************************************
 * The player clicked the advance dialogue button on the main
 * select screen.
 ************************************************************/
function advanceSelectDialogue (slot) {
    players[slot].current++;

    /* update dialogue */
    $selectDialogues[slot-1].html(players[slot].state[players[slot].current].dialogue);

    /* determine if the advance dialogue button should be shown */
    if (players[slot].state.length > players[slot].current+1) {
        $selectAdvanceButtons[slot-1].css({opacity : 1});
    } else {
        $selectAdvanceButtons[slot-1].css({opacity : 0});
    }

    /* direct the dialogue bubble */
    if (players[slot].state[players[slot].current].direction) {
        $selectBubbles[slot-1].removeClass();

		$selectBubbles[slot-1].addClass("dialogue-bubble dialogue-"+players[slot].state[players[slot].current].direction);
	} else {
		$selectBubbles[slot-1].removeClass();
		$selectBubbles[slot-1].addClass("dialogue-bubble dialogue-centre");
	}

    /* update image */
    $selectImages[slot-1].attr('src', players[slot].folder + players[slot].state[players[slot].current].image);
}

/************************************************************
 * Filters the list of selectable opponents based on those
 * already selected and performs search and sort logic.
 ************************************************************/
function updateSelectableOpponents(autoclear) {
    var name = $searchName.val().toLowerCase();
    var source = $searchSource.val().toLowerCase();
    var tag = $searchTag.val().toLowerCase();

    // reset filters
    selectableOpponents = [];

    // search for matches
    for (var i = 0; i < loadedOpponents.length; i++) {
        if (!loadedOpponents[i]) {
            continue;
        }

        // filter by name
        if (name
            && loadedOpponents[i].label.toLowerCase().indexOf(name) < 0
            && loadedOpponents[i].first.toLowerCase().indexOf(name) < 0
            && loadedOpponents[i].last.toLowerCase().indexOf(name) < 0) {
            continue;
        }

        // filter by source
        if (source && !loadedOpponents[i].source.toLowerCase().includes(source)) {
            continue;
        }

        // filter by tag
        if (tag) {
            if (!loadedOpponents[i].tags || !loadedOpponents[i].tags.some(function(t) {
                return t.toLowerCase().indexOf(tag) >= 0;
            })) {
                continue;
            }
        }

        // filter by gender
        if (chosenGender == 2 && loadedOpponents[i].gender !== eGender.MALE) {
            continue;
        }
        else if (chosenGender == 3 && loadedOpponents[i].gender !== eGender.FEMALE) {
            continue;
        }

        selectableOpponents.push(loadedOpponents[i]); // opponents will be in featured order
    }

    /* hide selected opponents */
    for (var i = 1; i < players.length; i++) {
        if (players[i]) {
            /* find this opponent's placement in the selectable opponents */
            for (var j = 0; j < selectableOpponents.length; j++) {
                if (selectableOpponents[j].folder == players[i].folder) {
                    /* this is a selected player */
                    selectableOpponents.splice(j, 1);
                }
            }
        }
    }

    // If a unique match was made, automatically clear the search so
    // another opponent can be found more quickly.
    if (autoclear && (name != null || source != null) && selectableOpponents.length == 0) {
        clearSearch();
        return;
    }

    /* sort opponents */
    // Since selectableOpponents is always reloaded here with featured order,
    // check if a different sorting mode is selected, and if yes, sort it.
    if (sortingOptionsMap.hasOwnProperty(sortingMode)) {
        selectableOpponents.sort(sortingOptionsMap[sortingMode]);
    }

    /* update max page indicator */
    $individualMaxPageIndicator.html("of "+Math.ceil(selectableOpponents.length/4));
}

/************************************************************
 * The player clicked on an opponent slot.
 ************************************************************/
function selectOpponentSlot (slot) {
    if (!players[slot]) {
        /* add a new opponent */
        selectedSlot = slot;

		/* update the list of selectable opponents based on those that are already selected, search, and sort options */
		updateSelectableOpponents(true);

		/* reload selection screen */
		updateIndividualSelectScreen();
        updateIndividualCountStats();

        /* switch screens */
		screenTransition($selectScreen, $individualSelectScreen);
    } else {
        /* remove the opponent that's there */
        players[slot] = null;
        updateSelectionVisuals();
    }
}

/************************************************************
 * The player clicked on the select group slot.
 ************************************************************/
function clickedSelectGroupButton () {
	selectedSlot = 1;
  groupSelectScreen = 0;
	updateGroupSelectScreen();

    $groupMaxPageIndicator.html("of "+loadedGroups[0].length);

	/* switch screens */
	screenTransition($selectScreen, $groupSelectScreen);
}

/************************************************************
* The player clicked on the Testing Tables button
************************************************************/
function clickedSelectGroupTestingButton () {
 selectedSlot = 1;
 groupSelectScreen = 1;
 updateGroupSelectScreen();

   $groupMaxPageIndicator.html("of "+loadedGroups[1].length);

 /* switch screens */
	screenTransition($selectScreen, $groupSelectScreen);
}


/************************************************************
 * The player clicked on the select random group slot.
 ************************************************************/
function clickedRandomGroupButton () {
	selectedSlot = 1;

    for (var i = 1; i < players.length; i++) {
        players[i] = null;
    }

	/* get a random number for the group listings */
  var randomGroupNumber = getRandomNumber(0, loadedGroups[0].length);
    console.log(loadedGroups[0][randomGroupNumber].opponents[0]);

	/* load the corresponding group */
	loadBehaviour(loadedGroups[0][randomGroupNumber].opponents[0].id, updateRandomSelection);
	loadBehaviour(loadedGroups[0][randomGroupNumber].opponents[1].id, updateRandomSelection);
	loadBehaviour(loadedGroups[0][randomGroupNumber].opponents[2].id, updateRandomSelection);
	loadBehaviour(loadedGroups[0][randomGroupNumber].opponents[3].id, updateRandomSelection);
}

/************************************************************
 * The player clicked on the all random button.
 ************************************************************/
function clickedRandomFillButton (predicate) {
	/* compose a copy of the loaded opponents list */
	var loadedOpponentsCopy = [];

	/* only add non-selected opponents from the list */
	for (var i = 0; i < loadedOpponents.length; i++) {
		/* check to see if this opponent is selected */
		var position = -1;
		for (var j = 1; j < players.length; j++) {
			if (players[j] && loadedOpponents[i].folder == players[j].folder) {
				/* this opponent is loaded */
				position = j;
			}
		}
		if (position == -1) {
			if(predicate) {
				if(predicate(loadedOpponents[i])) {
					loadedOpponentsCopy.push(loadedOpponents[i]);
				}
			} else {
				loadedOpponentsCopy.push(loadedOpponents[i]);
			}
		}
	}

	/* select random opponents */
	for (var i = 1; i < players.length; i++) {
		/* if slot is empty */
		if (!players[i]) {
			/* select random opponent */
			var randomOpponent = getRandomNumber(0, loadedOpponentsCopy.length);

			/* load opponent */
			loadBehaviour(loadedOpponentsCopy[randomOpponent].id, updateRandomSelection);

			/* remove random opponent from copy list */
			loadedOpponentsCopy.splice(randomOpponent, 1);
		}
	}
}

/************************************************************
 * The player clicked on the remove all button.
 ************************************************************/
function clickedRemoveAllButton ()
{
    for (var i = 1; i < 5; i++) {
        players[i] = null;
    }
    updateSelectionVisuals();
}

/************************************************************
 * The player clicked on a change stats card button on the
 * individual select screen.
 ************************************************************/
function changeIndividualStats (target) {
    for (var i = 1; i < 5; i++) {
        for (var j = 1; j < 4; j++) {
            if (j != target) {
                $('#individual-stats-page-'+i+'-'+j).hide();
            }
            else {
                $('#individual-stats-page-'+i+'-'+j).show();
            }
        }
    }

    individualCreditsShown = (target == 2); // true when Credits button is clicked
}

/************************************************************
 * The player clicked the select opponent button on the
 * individual select screen.
 ************************************************************/
function selectIndividualOpponent (slot) {
    /* move the stored player into the selected slot and update visuals */
	individualSlot = slot;
	loadBehaviour(shownIndividuals[slot-1].id, individualScreenCallback, 0);
}

/************************************************************
 * This is the callback for the individual select screen.
 ************************************************************/
function individualScreenCallback (playerObject, slot) {
    players[selectedSlot] = playerObject;
    players[selectedSlot].current = 0;

	/* switch screens */
	screenTransition($individualSelectScreen, $selectScreen);
	updateSelectionVisuals();
}

/************************************************************
 * The player is changing the page on the individual screen.
 ************************************************************/
function changeIndividualPage (skip, page) {
    console.log("resigtered");
    if (skip) {
        if (page == -1) {
            /* go to first page */
            individualPage = 0;
        } else if (page == 1) {
            /* go to last page */
            individualPage = Math.ceil(selectableOpponents.length/4)-1;
        } else {
            /* go to selected page */
            individualPage = Number($individualPageIndicator.val()) - 1;
        }
    } else {
        individualPage += page;
    }

    updateIndividualSelectScreen();
    updateIndividualCountStats();
}

/************************************************************
 * The player clicked on a change stats card button on the
 * group select screen.
 ************************************************************/
function changeGroupStats (target) {
    for (var i = 1; i < 5; i++) {
        for (var j = 1; j < 4; j++) {
            if (j != target) {
                $('#group-stats-page-'+i+'-'+j).hide();
            }
            else {
                $('#group-stats-page-'+i+'-'+j).show();
            }
        }
    }

    groupCreditsShown = (target == 2); // true when Credits button is clicked
}

/************************************************************
 * The player clicked the select opponent button on the
 * group select screen.
 ************************************************************/
function selectGroup () {
    /* clear the selection screen */
	for (var i = 1; i < 5; i++) {
		players[i] = null;
	}
	updateSelectionVisuals();

	/* load the group members */
	for (var i = 0; i < 4; i++) {
    if (loadedGroups[groupSelectScreen][groupPage[groupSelectScreen]].opponents[i]) {
			loadBehaviour(loadedGroups[groupSelectScreen][groupPage[groupSelectScreen]].opponents[i].id, groupScreenCallback, i+1);
		}
	}
}

/************************************************************
 * This is the callback for the group select screen.
 ************************************************************/
function groupScreenCallback (playerObject, slot) {
	console.log(slot +" "+playerObject);
    players[slot] = playerObject;
    players[slot].current = 0;

	updateSelectionVisuals();

    /* switch screens */
	screenTransition($groupSelectScreen, $selectScreen);
}

/************************************************************
 * The player is changing the page on the group screen.
 ************************************************************/
function changeGroupPage (skip, page) {
	if (skip) {
		if (page == -1) {
			/* go to first page */
      groupPage[groupSelectScreen] = 0;
		} else if (page == 1) {
			/* go to last page */
			groupPage[groupSelectScreen] = loadedGroups[groupSelectScreen].length-1;
		} else {
			/* go to selected page */
			groupPage[groupSelectScreen] = Number($groupPageIndicator.val()) - 1;
		}
	} else {
		groupPage[groupSelectScreen] += page;
	}
	updateGroupSelectScreen();
    updateGroupCountStats();
}

/************************************************************
 * The player clicked on the back button on the individual or
 * group select screen.
 ************************************************************/
function backToSelect () {
    /* switch screens */
	screenTransition($individualSelectScreen, $selectScreen);
	screenTransition($groupSelectScreen, $selectScreen);
}

/************************************************************
 * The player clicked on the start game button on the main
 * select screen.
 ************************************************************/
function advanceSelectScreen () {
    advanceToNextScreen($selectScreen);
}

/************************************************************
 * The player clicked on the back button on the main select
 * screen.
 ************************************************************/
function backSelectScreen () {
	screenTransition($selectScreen, $titleScreen);
}

/**********************************************************************
 *****                     Display Functions                      *****
 **********************************************************************/

/************************************************************
 * Displays all of the current players on the main select
 * screen.
 ************************************************************/
function updateSelectionVisuals () {
    /* update all opponents */
    for (var i = 1; i < players.length; i++) {
        if (players[i]) {
            /* update dialogue */
            $selectDialogues[i-1].html(players[i].state[players[i].current].dialogue);

            /* determine if the advance dialogue button should be shown */
            if (players[i].state.length > players[i].current+1) {
                $selectAdvanceButtons[i-1].css({opacity : 1});
            } else {
                $selectAdvanceButtons[i-1].css({opacity : 0});
            }

			/* show the bubble */
			$selectBubbles[i-1].show();

            /* update image */
            $selectImages[i-1].attr('src', players[i].folder + players[i].state[players[i].current].image);
			$selectImages[i-1].show();

            /* update label */
            $selectLabels[i-1].html(players[i].label.initCap());

            /* change the button */
            $selectButtons[i-1].html("Remove Opponent");
            $selectButtons[i-1].removeClass("smooth-button-green");
            $selectButtons[i-1].addClass("smooth-button-red");
        } else {
            /* clear the view */
            $selectDialogues[i-1].html("");
            $selectAdvanceButtons[i-1].css({opacity : 0});
			$selectBubbles[i-1].hide();
			$selectImages[i-1].hide();
            $selectLabels[i-1].html("Opponent "+i);

            /* change the button */
            $selectButtons[i-1].html("Select Opponent");
            $selectButtons[i-1].removeClass("smooth-button-red");
            $selectButtons[i-1].addClass("smooth-button-green");
        }
    }

    /* check to see if all opponents are loaded */
    var loaded = 0;
    for (var i = 1; i < players.length; i++) {
        if (players[i]) {
            loaded++;
        }
    }

    /* if enough opponents are loaded, then enable progression */
    if (loaded >= 2) {
        $selectMainButton.attr('disabled', false);
    } else {
        $selectMainButton.attr('disabled', true);
    }

    /* if all opponents are loaded, disable fill buttons */
    if (loaded >= 4) {
        for (var i = 0; i < $selectRandomButtons.length; i++) {
            $selectRandomButtons[i].attr('disabled', true);
        }
    }
    else {
        for (var i = 0; i < $selectRandomButtons.length; i++) {
            $selectRandomButtons[i].attr('disabled', false);
        }
    }

    /* if no opponents are loaded, disable remove all button */
    if (loaded <= 0) {
        $selectRemoveAllButton.attr('disabled', true);
    } else {
        $selectRemoveAllButton.attr('disabled', false);
    }
}



/************************************************************
 * This is the callback for the group clicked rows, it
 * updates information on the group screen.
 ************************************************************/
function updateGroupScreen (playerObject) {
    /* find a spot to store this player */
    for (var i = 0; i < storedGroup.length; i++) {
        if (!storedGroup[i]) {
            storedGroup[i] = playerObject;
            $groupLabels[i+1].html(playerObject.label);
            break;
        }
    }

	/* enable the button */
	$groupButton.attr('disabled', false);
}

/************************************************************
 * This is the callback for the random buttons.
 ************************************************************/
function updateRandomSelection (playerObject) {
    /* find a spot to store this player */
    for (var i = 0; i < players.length; i++) {
        if (!players[i]) {
            players[i] = playerObject;
            break;
        }
    }

	updateSelectionVisuals();
}

/************************************************************
 * Hides the table on the single selection screen.
 ************************************************************/
function hideSelectionTable() {
    mainSelectHidden = !mainSelectHidden;
    if (mainSelectHidden) {
        $selectTable.hide();
    }
    else {
        $selectTable.show();
    }
}

/************************************************************
 * Hides the table on the single selection screen.
 ************************************************************/
function hideSingleSelectionTable() {
    singleSelectHidden = !singleSelectHidden;
    if (singleSelectHidden) {
        $individualSelectTable.hide();
    }
    else {
        $individualSelectTable.show();
    }
}

/************************************************************
 * Hides the table on the single group screen.
 ************************************************************/
function hideGroupSelectionTable() {
    groupSelectHidden = !groupSelectHidden;
    if (groupSelectHidden) {
        $groupSelectTable.hide();
    }
    else {
        $groupSelectTable.show();
    }
}

function openSearchModal() {
    $searchModal.modal('show');
}


function closeSearchModal() {
    // perform the search and sort logic
    updateSelectableOpponents();

    // update
    updateIndividualSelectScreen();
    updateIndividualCountStats();
}

function clearSearch() {
    $searchName.val(null);
    $searchTag.val(null);
    $searchSource.val(null);
    closeSearchModal();
}

function changeSearchGender(gender) {
    chosenGender = gender;
    setActiveOption($searchGenderOptions, gender);
}

/************************************************************
 * Sorting Functions
 ************************************************************/

/**
 * Callback for Arrays.sort to sort an array of objects by the given field.
 * Prefixing "-" to a field will cause the sort to be done in reverse.
 * Examples:
 *   // sorts myArr by each element's first name (A-Z)
 *   myArr.sort(sortOpponentsByField("first"));
 *   // sorts myArr by each element's last name (Z-A)
 *   myArr.sort(sortOpponentsByField("-last"));
 */
function sortOpponentsByField(field) {
    // check for prefix
    var order = 1; // 1 = forward, -1 = reversed
    if (field[0] === "-") {
        order = -1;
        field = field.substr(1);
    }

    return function(opp1, opp2) {
        var compare = 0;
        if (opp1[field] < opp2[field]) {
            compare = -1;
        }
        else if (opp1[field] > opp2[field]) {
            compare = 1;
        }
        return order * compare;
    }
}

/**
 * Callback for Arrays.sort to sort an array of objects over multiple given fields.
 * Prefixing "-" to a field will cause the sort to be done in reverse.
 * This should allow more flexibility in the sorting order.
 * Example:
 *   // sorts myArr by each element's number of layers (low to high),
 *   // and for elements whose layers are equivalent, sort them by first name (Z-A)
 *   myArr.sort(sortOpponentsByMultipleFields("layers", "-first"));
 */
function sortOpponentsByMultipleFields() {
    var fields = arguments; // retrieve the args passed in
    return function(opp1, opp2) {
        var i = 0;
        var compare = 0;
        // if both elements have the same field, check the next ones
        while (compare === 0 && i < fields.length) {
            compare = sortOpponentsByField(fields[i])(opp1, opp2);
            i++;
        }
        return compare;
    }
}

/** Event handler for the sort dropdown options. Fires when user clicks on a dropdown item. */
$sortingOptionsItems.on("click", function(e) {
    sortingMode = $(this).find('a').html();
    $("#sort-dropdown-selection").html(sortingMode); // change the dropdown text to the selected option
});

/************************************************************
 * Word wrapping Functions
 ************************************************************/

/**
 * Inserts a fixed-size HTML element with the specified text to allow the content
 * to be either word-wrapped (if the text is long and spaces are present)
 * or word-broken (if text is long and no spaces are present).
 */
function wordWrapHtml(text) {
    text = text || "&nbsp;";
    return "<table class=\"wrap-text\"><tr><td>" + text + "</td></tr></table>";
}

/************************************************************
 * Dynamic dialogue and image counting functions
 ************************************************************/

/** Event handler for the individual selection screen credits button. */
$individualCreditsButton.on('click', function(e) {
    updateIndividualCountStats()
});

/** Event handler for the group selection screen credits button. */
$groupCreditsButton.on('click', function(e) {
    updateGroupCountStats();
});

/**
 * Loads and displays the number of unique dialogue lines and the number of pose images
 * into the character's player object for those currently on the selection screen.
 * Only loads if the unique line count or image count is not known.
 */
function updateOpponentCountStats(opponentArr, uiElements) {
    opponentArr.forEach(function(opp, idx) {
        // load behaviour file if line/image count is not known
        if (opp && (opp.uniqueLineCount === undefined || opp.posesImageCount === undefined)) {
            uiElements.countBoxes[idx].css("visibility", "visible");

            // retrieve line and image counts
            if (DEBUG) {
                console.log("[LineImageCount] Fetching counts for " + opp.label + " in slot " + idx);
            }
            var countsPromise = Promise.resolve(fetchBehaviour(opp.folder));
            countsPromise.then(countLinesImages).then(function(response) {
                opp.uniqueLineCount = response.numUniqueLines;
                opp.posesImageCount = response.numPoses;

                // show line and image counts
                if (DEBUG) {
                    console.log("[LineImageCount] Loaded " + opp.label + " from behaviour: " +
                      opp.uniqueLineCount + " lines, " + opp.posesImageCount + " images");
                }
                uiElements.lineLabels[idx].html(opp.uniqueLineCount);
                uiElements.poseLabels[idx].html(opp.posesImageCount);
            });
        }
        else {
            // this character's counts were previously loaded
            if (opp) {
                if (DEBUG) {
                    console.log("[LineImageCount] Loaded previous count for " + opp.label + ": " +
                      opp.uniqueLineCount + " lines, " + opp.posesImageCount + " images)");
                }
                uiElements.countBoxes[idx].css("visibility", "visible");
                uiElements.lineLabels[idx].html(opp.uniqueLineCount);
                uiElements.poseLabels[idx].html(opp.posesImageCount);
            }
            else {
                // there is no character in the slot
                uiElements.countBoxes[idx].css("visibility", "hidden");
                uiElements.lineLabels[idx].html("");
                uiElements.poseLabels[idx].html("");
            }
        }
    });
}

/** Dialogue/image count update function for the individual selection screen. */
function updateIndividualCountStats() {
    if (individualCreditsShown) {
        var individualUIElements = {
            countBoxes : $individualCountBoxes,
            lineLabels : $individualLineCountLabels,
            poseLabels : $individualPoseCountLabels
        };
        updateOpponentCountStats(shownIndividuals, individualUIElements);
    }
}

/** Dialogue/image count update function for the group selection screen. */
function updateGroupCountStats() {
    if (groupCreditsShown) {
        var groupUIElements = {
            countBoxes : $groupCountBoxes,
            lineLabels : $groupLineCountLabels,
            poseLabels : $groupPoseCountLabels
        };
        updateOpponentCountStats(shownGroup, groupUIElements);
    }
}

/**
 * Fetches the behaviour.xml file of the specified opponent directory.
 */
function fetchBehaviour(path) {
    return $.ajax({
        type: "GET",
        url: path + "behaviour.xml",
        dataType: "text"
    });
}

/**
 * Callback to parse the number of lines of dialogue and number of images
 * given a character's behaviour XML. Returns the counts as an object with
 * properties numTotalLines, numUniqueLines, and numPoses.
 */
function countLinesImages(xml) {
    // parse all lines of dialogue and all images
	var numTotalLines = 0;
	var numUniqueDialogueLines = 0;
	var numUniqueUsedPoses = 0;
    var lines = {};
    var poses = {};
    $(xml).find('state').each(function(idx, data) {
		numTotalLines++;
		// count only unique lines of dialogue
		if (lines[data.textContent.trim()] === undefined) numUniqueDialogueLines++;
        lines[data.textContent.trim()] = 1;
		// count unique number of poses used in dialogue
		// note that this number may differ from actual image count if some images
		// are never used, or if images that don't exist are used in the dialogue
		if (poses[data.getAttribute("img")] === undefined) numUniqueUsedPoses++;
        poses[data.getAttribute("img")] = 1;
    });

    return {
        numTotalLines : numTotalLines,
        numUniqueLines : numUniqueDialogueLines,
        numPoses : numUniqueUsedPoses
    };
}
