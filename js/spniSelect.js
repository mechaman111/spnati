/********************************************************************************
 This file contains the variables and functions that form the select screens of
 the game. The parsing functions for the opponent.xml file.
 ********************************************************************************/


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
$selectRandomButtons = $("#select-random-button, #select-random-female-button, #select-random-male-button");
$selectRandomTableButton = $("#select-random-group-button");
$selectRemoveAllButton = $("#select-remove-all-button");

mainSelectDisplays = [
	new MainSelectScreenDisplay(1),
	new MainSelectScreenDisplay(2),
	new MainSelectScreenDisplay(3),
	new MainSelectScreenDisplay(4)
];

var individualDetailDisplay = new OpponentDetailsDisplay();


/* group select screen */
$groupSelectTable = $("#group-select-table");
$groupSwitchTestingButton = $("#group-switch-testing-button");
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
$groupNewBadges = [$("#group-new-badge-1"), $("#group-new-badge-2"), $("#group-new-badge-3"), $("#group-new-badge-4")];
$groupCostumeBadges = [$("#group-costume-badge-1"), $("#group-costume-badge-2"), $("#group-costume-badge-3"), $("#group-costume-badge-4")];
$groupStatuses = [$("#group-status-1"), $("#group-status-2"), $("#group-status-3"), $("#group-status-4")];
$groupLayers = [$("#group-layer-1"), $("#group-layer-2"), $("#group-layer-3"), $("#group-layer-4")];
$groupGenders = [$("#group-gender-1"), $("#group-gender-2"), $("#group-gender-3"), $("#group-gender-4")];
$groupCostumeSelectors = [$("#group-costume-select-1"), $("#group-costume-select-2"), $("#group-costume-select-3"), $("#group-costume-select-4")];

$groupImages = [$("#group-image-1"), $("#group-image-2"), $("#group-image-3"), $("#group-image-4")];
$groupNameLabel = $("#group-name-label");
$groupButton = $("#group-button");

$groupBackgroundToggle = $('#group-enable-preset-backgrounds');

$groupPageIndicator = $("#group-page-indicator");
$groupMaxPageIndicator = $("#group-max-page-indicator");

$groupCreditsButton = $('#group-credits-button');

$searchModal = $('#search-modal');
$searchName = $("#search-name");
$searchSource = $("#search-source");
$searchTag = $("#search-tag");
$searchCreator = $("#search-creator");

$tagList = $("#tagList");
$sourceList = $("#sourceList");
$creatorList = $("#creatorList");
$searchGenderOptions = [$("#search-gender-1"), $("#search-gender-2"), $("#search-gender-3")];

$searchModal.on('shown.bs.modal', function() {
	$searchName.focus();
});

$sortingOptionsItems = $(".sort-dropdown-options li");

$groupSearchModal = $('#group-search-modal');
$groupSearchGroupName = $("#group-search-group-name");
$groupSearchName = $("#group-search-name");
$groupSearchSource = $("#group-search-source");
$groupSearchTag = $("#group-search-tag");
$groupSearchGenderOptions = [$("#group-search-gender-1"), $("#group-search-gender-2"), $("#group-search-gender-3"), $("#group-search-gender-4")];

$groupSearchModal.on('shown.bs.modal', function() {
	$groupSearchGroupName.focus();
});

var $indivSelectionCardContainer = $('#individual-select-screen .selection-cards-container');

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
var loadedGroups = [];
var selectableGroups = loadedGroups;

/* indiv. select view variables */

/** Should the individual selection view be in "Testing" mode? */
var individualSelectTesting = false;

/** Are the default fill suggestions using Testing opponents? */
var suggestedTestingOpponents = undefined;

/* page variables */
var individualPage = 0;
var groupPage = 0;
var chosenGender = -1;
var chosenGroupGender = -1;
var sortingMode = "Featured";
var sortingOptionsMap = {
    "Recently Updated": sortOpponentsByMultipleFields("-lastUpdated"),
    "Newest" : sortOpponentsByMultipleFields("-release"),
    "Oldest" : sortOpponentsByMultipleFields("release"),
    "Most Layers" : sortOpponentsByMultipleFields("-layers"),
    "Fewest Layers" : sortOpponentsByMultipleFields("layers"),
    "Name (A-Z)" : sortOpponentsByMultipleFields("first", "last"),
    "Name (Z-A)" : sortOpponentsByMultipleFields("-first", "-last"),
    "Talks to selected" : sortOpponentsByMostTargeted(),
};
var groupCreditsShown = false;

/* consistence variables */
var selectedSlot = 0;
var shownIndividuals = Array(4);
var shownGroup = Array(4);
var randomLock = false;

/* Status indicators */
var statusIndicators = {
    testing: {
        icon: "badge-testing.png",
        tooltip: "This opponent is currently in testing.",
    },
    offline: {
        icon: "badge-offline.png",
        tooltip: "This opponent has been retired from the official version of the game.",
    },
    incomplete: {
        icon: "badge-incomplete.png",
        tooltip: "This opponent is incomplete and currently not in development."
    },
    duplicate: {
        icon: "badge-duplicate.png",
        tooltip: "This opponent has been retired from the game and replaced with a newer version."
    },
    event: {
        icon: "badge-event.png",
        tooltip: "This opponent only returns to the official version of the game for special events."
    }
}

/**********************************************************************
 *****               Opponent & Group Specification               *****
 **********************************************************************/

/**************************************************
 * Stores meta information about groups.
 **************************************************/
function Group(title, background) {
    this.title = title;
    this.background = background;
    this.opponents = Array(4);
    this.costumes = Array(4);
}

/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Loads all of the content required to display the title
 * screen.
 ************************************************************/

function loadSelectScreen () {
    var deferred = loadListingFile();
    updateSelectionVisuals();
    
    return deferred;
}

function splitCreatorField (field) {
    // First, remove any parenthetical info in the field.
    // Then, split on observed creator separators.
    return field
            .replace(/\([^\)]+\)|\[[^\]]+\]/gm, '')
            .split(/\s*(?:,|&|\:|and|\+|\/|\\|<(?:\/\\)?\s*br\s*(?:\/\\)?>)\s*/gm)
            .map(function (s) {
                return s.trim();
            });
}

/************************************************************
 * Loads and parses the main opponent listing file.
 ************************************************************/
function loadListingFile () {
	/* clear the previous meta information */
	var outstandingLoads = 0;
    var totalLoads = 0;
	var opponentGroupMap = {};
	var opponentMap = {};
    var tagSet = {};
    var sourceSet = {};
    var creatorSet = {};

	var onComplete = function(opp, index) {
		if (opp) {
			if (opp.id in opponentMap) {
				loadedOpponents[opponentMap[opp.id]] = opp;
                opp.searchTags.forEach(function(tag) {
                    tagSet[tag] = true;
                });
                sourceSet[opp.source] = true;
                
                splitCreatorField(opp.artist).forEach(function (creator) {
                    creatorSet[creator] = true;
                });
                
                splitCreatorField(opp.writer).forEach(function (creator) {
                    creatorSet[creator] = true;
                });
                
                var disp = new OpponentSelectionCard(opp);
                opp.selectionCard = disp;
                disp.statusIcon.tooltip({ delay: { show: 200 }, placement: 'bottom',
                                          container: '#individual-select-screen .selection-cards-container' });
			}
			if (opp.id in opponentGroupMap) {
				opponentGroupMap[opp.id].forEach(function(groupPos) {
					groupPos.group.opponents[groupPos.idx] = opp;
                    groupPos.group.costumes[groupPos.idx] = groupPos.costume;
				});
			}
		}
        
        if (--outstandingLoads == 0) {
            $(".title-menu-buttons-container>div").removeAttr("hidden");
            $("#title-load-container").hide();
            
            $tagList.append(Object.keys(TAG_ALIASES).concat(Object.keys(tagSet)).sort().map(function(tag) {
                return new Option(tag);
            }));
            $sourceList.append(Object.keys(sourceSet).sort().map(function(source) {
                return new Option(source);
            }));
            $creatorList.append(Object.keys(creatorSet).sort().map(function(source) {
                return new Option(source);
            }));
            loadedOpponents.forEach(function(p) { p.selectionCard.updateEpilogueBadge() });
            updateIndividualSelectSort();
        } else {
            var progress = Math.floor(100 * (totalLoads - outstandingLoads) / totalLoads);
            $(".game-load-progress").text(progress.toString(10));
        }
	}

	/* grab and parse the opponent listing file */
	return $.ajax({
        type: "GET",
		url: listingFile,
		dataType: "text",
		success: function(xml) {
            var $xml = $(xml);
			var available = {};

            /* start by checking which characters will be loaded and available */
            $xml.find('>individuals>opponent').each(function () {
                var oppStatus = $(this).attr('status');
                var id = $(this).text();
                if (oppStatus === undefined || oppStatus === 'testing' || includedOpponentStatuses[oppStatus]) {
                    available[id] = true;
                }
            });

			$xml.find('>groups>group').each(function () {
                var title = $(this).attr('title');
                var background = $(this).attr('background') || undefined;
				var opp1 = $(this).attr('opp1');
				var opp2 = $(this).attr('opp2');
				var opp3 = $(this).attr('opp3');
				var opp4 = $(this).attr('opp4');
                var costume1 = $(this).attr('costume1');
				var costume2 = $(this).attr('costume2');
				var costume3 = $(this).attr('costume3');
				var costume4 = $(this).attr('costume4');

                var ids = [opp1, opp2, opp3, opp4];
                var costumes = [costume1, costume2, costume3, costume4];
                if (!ids.every(function(id) { return available[id]; })) return;

				var newGroup = new Group(title, background);
				ids.forEach(function(id, idx) {
					if (!(id in opponentGroupMap)) {
						opponentGroupMap[id] = [];
					}
					opponentGroupMap[id].push({ group: newGroup, idx: idx, costume: costumes[idx] });
				});
				loadedGroups.push(newGroup);
			});

            /* now actually load the characters */
            var oppDefaultIndex = 0; // keep track of an opponent's default placement

            $xml.find('>individuals>opponent').each(function () {
                var oppStatus = $(this).attr('status');
                var id = $(this).text();
                var releaseNumber = $(this).attr('release');
                var highlightStatus = $(this).attr('highlight');

                if (available[id]) {
                    outstandingLoads++;
                    totalLoads++;
                    opponentMap[id] = oppDefaultIndex++;
                    loadOpponentMeta(id, oppStatus, releaseNumber, highlightStatus, onComplete);
                }
            });

		}
	});
}

/************************************************************
 * Loads and parses the meta XML file of an opponent.
 ************************************************************/
function loadOpponentMeta (id, status, releaseNumber, highlightStatus, onComplete) {
	/* grab and parse the opponent meta file */
    console.log("Loading metadata for \""+id+"\"");
	$.ajax({
        type: "GET",
		url: 'opponents/' + id + '/' + metaFile,
		dataType: "text",
		success: function(xml) {
            var $xml = $(xml);

			var opponent = new Opponent(id, $xml, status, releaseNumber, highlightStatus);

			/* add the opponent to the list */
            onComplete(opponent);
		},
		error: function(err) {
			console.log("Failed reading \""+id+"\"");
			onComplete();
		}
	});
}

function updateStatusIcon(elem, opp) {
    var status = opp.status;
    if (!opp.status) {
        status = opp.highlightStatus;
    }

    if (status && statusIndicators[status]) {
        elem.attr({
            'src': 'img/' + statusIndicators[status].icon,
            'alt': status.initCap(),
            'data-original-title': statusIndicators[status].tooltip || '',
        }).show();
    } else {
        elem.removeAttr('data-original-title').hide();
    }
}

function updateGenderIcon(elem, opp) {
    elem.attr({
        src: opp.selectGender === 'male' ? MALE_SYMBOL : FEMALE_SYMBOL,
        alt: opp.selectGender.initCap(),
    }).show();
}

/* Creates an <option> element in a jQuery object for an alternate costume.
 * `alt_costume` in this case has only `id` and `label` attributes.
 */
function getCostumeOption(alt_costume, selected_costume) {
    return $('<option>', {val: alt_costume.folder, text: 'Costume: '+alt_costume.name,
                          selected: alt_costume.folder == selected_costume, data: alt_costume});
}

function fillCostumeSelector($selector, costumes, selected_costume) {
    $selector.empty().append($('<option>', {
        val: '',
        text: 'Default Costume'
    }), costumes.map(function(c) {
        return $('<option>', {
            val: c.folder, text: '\u{1f455} '+c.name,
            selected: c.folder == selected_costume
        }).data('costumeDescriptor', c);
    }));
    return $selector;
}

/************************************************************
 * Loads opponents onto the group select screen based on the
 * currently selected page.
 * 
 * ignore_bg {boolean}: If true, skips setting up group backgrounds.
 * This is really only necessary during initial load, when we need to
 * update this screen despite it not actually being visible.
 ************************************************************/
function updateGroupSelectScreen (ignore_bg) {
	/* safety wrap around */
    if (groupPage < 0) {
		/* wrap to last page */
		groupPage = (selectableGroups.length)-1;
	} else if (groupPage > selectableGroups.length-1) {
		/* wrap to the first page */
		groupPage = 0;
	}
	$groupPageIndicator.val(groupPage+1);
    $groupMaxPageIndicator.html("of "+selectableGroups.length);

    /* create and load all of the individual opponents */
    $groupButton.attr('disabled', false);
    
    var group = selectableGroups[groupPage];
    
    if (group) {
        $groupNameLabel.html(group.title);

        if (!ignore_bg) {
            if (group.background && backgrounds[group.background]) {
                var bg = backgrounds[group.background];

                $('.group-preset-background-row').show();
                $('#group-preset-background-label').text(bg.name);

                $groupBackgroundToggle.prop('checked', useGroupBackgrounds).off('change');
                $groupBackgroundToggle.on('change', function () {
                    /* The user toggled the preset background checkbox. */
                    useGroupBackgrounds = $groupBackgroundToggle.is(':checked');

                    if (useGroupBackgrounds) {
                        bg.activateBackground();
                    } else {
                        optionsBackground.activateBackground();
                    }

                    save.saveSettings();
                });

                if (useGroupBackgrounds) {
                    bg.activateBackground();
                }
            } else {
                $('.group-preset-background-row').hide();

                if (useGroupBackgrounds && activeBackground.id !== optionsBackground.id) {
                    optionsBackground.activateBackground();
                }
            }
        }
    } else {
        $groupNameLabel.html("(No matches)");
        $groupButton.attr('disabled', true);
    }

    for (var i = 0; i < 4; i++) {
        var opponent = group ? group.opponents[i] : null;
        var costume = group ? group.costumes[i] : null;

        if (opponent && typeof opponent == "object") {
            shownGroup[i] = opponent;

            if (costume) {
                if (costume.toLowerCase() == "default") {
                    opponent.selectAlternateCostume(null);
                } else {
                    costume = "opponents/reskins/" + costume;
                    
                    for (let j = 0; j < opponent.alternate_costumes.length; j++) {
                        if (opponent.alternate_costumes[j].folder === costume) {
                            opponent.selectAlternateCostume(opponent.alternate_costumes[j]);
                            break;
                        }
                    }
                }
            }

            $groupNameLabels[i].html(opponent.first + " " + opponent.last);
            $groupPrefersLabels[i].html(opponent.label);
            $groupSexLabels[i].html(opponent.gender);
            $groupSourceLabels[i].html(opponent.source);
            $groupWriterLabels[i].html(opponent.writer);
            $groupArtistLabels[i].html(opponent.artist);
            $groupDescriptionLabels[i].html(opponent.description);
            var epilogueStatus = opponent.getEpilogueStatus();

            if (opponent.endings) {
                $groupBadges[i].show();
                $groupBadges[i].attr({'src': epilogueStatus.badge,
                                      'data-original-title': epilogueStatus.tooltip || ''});
            } else {
                $groupBadges[i].hide();
            }
            /*
            if (opponent.highlightStatus === 'new') {
                $groupNewBadges[i].show();
            } else {
                $groupNewBadges[i].hide();
            }
            */
            $groupCostumeSelectors[i].hide();
            if (opponent.alternate_costumes.length > 0) {
                /*
                $groupCostumeBadges[i].show();
                */
                fillCostumeSelector($groupCostumeSelectors[i], opponent.alternate_costumes,
                                    opponent.selected_costume).show();
            } else {
                //$groupCostumeBadges[i].hide();
            }

            updateStatusIcon($groupStatuses[i], opponent);

            $groupLayers[i].attr({
                src: "img/layers" + opponent.layers + ".png",
                alt: opponent.layers + ' layers',
            }).show();
            updateGenderIcon($groupGenders[i], opponent);

            $groupImages[i].attr('src', opponent.selection_image);
            $groupImages[i].css('height', opponent.scale + '%');
            $groupImages[i].show();
        } else {
            delete shownGroup[i];

            $groupNameLabels[i].html("");
            $groupPrefersLabels[i].html("");
            $groupSexLabels[i].html("");
            $groupSourceLabels[i].html("");
            $groupWriterLabels[i].html("");
            $groupArtistLabels[i].html("");
            $groupDescriptionLabels[i].html("");
            $groupBadges[i].hide();
            $groupNewBadges[i].hide();
            $groupCostumeBadges[i].hide();
            $groupStatuses[i].hide();
            $groupLayers[i].hide();
            $groupGenders[i].hide();
            $groupImages[i].hide();
            $groupCostumeSelectors[i].hide();
            $groupButton.attr('disabled', true);
        }
    }
}

/**********************************************************************
 *****                   Interaction Functions                    *****
 **********************************************************************/

/* A filter predicate encompassing the filter options on the individual select
 * screen.
 */
function filterOpponent(opp, name, source, creator, tag) {
    // filter by name
    if (name
        && opp.selectLabel.toLowerCase().indexOf(name) < 0
        && opp.first.toLowerCase().indexOf(name) < 0
        && opp.last.toLowerCase().indexOf(name) < 0) {
        return false;
    }

    // filter by source
    if (source && opp.source.toLowerCase().indexOf(source) < 0) {
        return false;
    }

    // filter by tag
    if (tag && !(opp.searchTags && opp.searchTags.indexOf(tag) >= 0)) {
        return false;
    }
    
    // filter by creator
    if (creator && opp.artist.toLowerCase().indexOf(creator) < 0 && opp.writer.toLowerCase().indexOf(creator) < 0) {
        return false;
    }

    // filter by gender
    if ((chosenGender == 2 && opp.selectGender !== eGender.MALE)
        || (chosenGender == 3 && opp.selectGender !== eGender.FEMALE)) {
        return false;
    }
    
    return true;
}

/************************************************************
 * Filters the list of selectable opponents based on those
 * already selected and performs search logic.
 ************************************************************/
function updateIndividualSelectFilters(autoclear) {
    var name = $searchName.val().toLowerCase();
    var source = $searchSource.val().toLowerCase();
    var creator = $searchCreator.val().toLowerCase();
    var tag = canonicalizeTag($searchTag.val());

    // Array.prototype.filter automatically skips empty slots
    loadedOpponents.forEach(function (opp) {
        opp.selectionCard.setFiltered(!filterOpponent(opp, name, source, creator, tag));

        if (opp.selectionCard.isVisible(individualSelectTesting, false)) {
            $(opp.selectionCard.mainElem).show();
        } else {
            $(opp.selectionCard.mainElem).hide();
        }
    });

    // If a unique match was made, automatically clear the search so
    // another opponent can be found more quickly.
    if (autoclear && (name != null || source != null) && selectableOpponents.length == 0) {
        clearSearch();
        return;
    }
}

/** Updates the sort order of opponents on the individual select screen. */
function updateIndividualSelectSort() {
    var ordered = loadedOpponents.slice();

    /* sort opponents */
    // Since ordered is always initialized here with featured order,
    // check if a different sorting mode is selected, and if yes, sort it.
    if (sortingOptionsMap.hasOwnProperty(sortingMode)) {
        ordered.sort(sortingOptionsMap[sortingMode]);
    }
    
    if (individualSelectTesting && (sortingMode === "Featured" || sortingMode === "Recently Updated")) {
        /*
         * As special cases, when using these sort modes in the Testing view,
         * additionally sort all Testing opponents before main-roster opponents.
         */
        ordered.sort(sortTestingOpponents);
    }

    ordered.forEach(function (opp) {
        $(opp.selectionCard.mainElem).appendTo($indivSelectionCardContainer);
    });
}

$('#individual-select-screen .sort-filter-field').on('input', function () {
    updateIndividualSelectFilters();
});

function updateIndividualSelectVisibility() {
    loadedOpponents.forEach(function (opp) {
        if (opp.selectionCard.isVisible(individualSelectTesting, false)) {
            $(opp.selectionCard.mainElem).show();
        } else {
            $(opp.selectionCard.mainElem).hide();
        }
    });
}

/** Is the individual select screen locked to Testing or Main Roster mode? */
function isIndividualSelectViewTypeLocked() {
    return players.some(function (opp) { return opp && opp !== humanPlayer; });
}

/** 
 * Update displayed epilogue badges for opponents on the individual
 * selection screen.
 */
function updateIndividualEpilogueBadges (autoclear) {
    loadedOpponents.forEach(function(opp) {
        if (opp.endings) {
            opp.selectionCard.updateEpilogueBadge();
        }
    });
}


/************************************************************
 * The player clicked on an opponent slot.
 ************************************************************/
function selectOpponentSlot (slot) {
    if (!(slot in players)) {
        /* add a new opponent */
        selectedSlot = slot;
        showIndividualSelectionScreen();
    } else {
        /* remove the opponent that's there */
        $selectImages[slot-1].off('load');
		
        players[slot].unloadOpponent();
        delete players[slot];

        updateSelectionVisuals();
    }
}

function showIndividualSelectionScreen() {
    /* We don't need to update filtering when moving from the main select screen
     * to the indiv. select screen, since the filters cannot actually change
     * unless the user is already on said screen.
     * 
     * We also don't need to update sorting, since any change to the sort mode
     * (anywhere) automatically updates the display order.
     * 
     * The visibility of characters might change, however, depending on the
     * view type and what characters have already been selected.
     */
    updateIndividualSelectVisibility();

    /* Make sure the user doesn't have target-count sorting set if
     * the amount of loaded opponents drops to 0. */
    if (sortingMode === "Talks to selected") {
        if (players.countTrue() <= 1) {
            setSortingMode("Featured");
        }
    }

    updateIndividualEpilogueBadges();

    /* switch screens */
    if (SENTRY_INITIALIZED) Sentry.setTag("screen", "select-individual");
    screenTransition($selectScreen, $individualSelectScreen);
}

function toggleIndividualSelectView() {
    individualSelectTesting = !individualSelectTesting;

    /* Switch to the default sort mode for the selected view. */
    if (individualSelectTesting) {
        setSortingMode("Recently Updated");
    } else {
        setSortingMode("Featured");
    }

    updateSelectionVisuals();

    $("#select-group-testing-button").text(
        individualSelectTesting ? "Main Roster" : "Testing Roster"
    );
}

/************************************************************
 * The player clicked on the Preset Tables button.
 ************************************************************/
function showPresetTables () {
    $groupSwitchTestingButton.html("Testing Tables");
    updateSelectableGroups();
    updateGroupSelectScreen();

    if (SENTRY_INITIALIZED) Sentry.setTag("screen", "select-group");

	/* switch screens */
	screenTransition($selectScreen, $groupSelectScreen);
}

/************************************************************
 * Filters the list of selectable opponents based on those
 * already selected and performs search and sort logic.
 ************************************************************/
function updateSelectableGroups() {
    var groupname = $groupSearchGroupName.val().toLowerCase();
    var name = $groupSearchName.val().toLowerCase();
    var source = $groupSearchSource.val().toLowerCase();
    var tag = canonicalizeTag($groupSearchTag.val());

    // reset filters
    selectableGroups = loadedGroups.filter(function(group) {
        if (!group.opponents.every(function(opp) { return opp; })) return false;

        if (groupname && group.title.toLowerCase().indexOf(groupname) < 0) return false;

        if (name && !group.opponents.some(function(opp) {
            return opp.selectLabel.toLowerCase().indexOf(name) >= 0
                || opp.first.toLowerCase().indexOf(name) >= 0
                || opp.last.toLowerCase().indexOf(name) >= 0;
        })) return false;

        if (source && !group.opponents.some(function(opp) {
            return opp.source.toLowerCase().indexOf(source) >= 0;
        })) return false;

        if (tag && !group.opponents.some(function(opp) {
            return opp.searchTags && opp.searchTags.indexOf(canonicalizeTag(tag)) >= 0;
        })) return false;

        if ((chosenGroupGender == 2 || chosenGroupGender == 3)
            && !group.opponents.every(function(opp) {
                return opp.selectGender == (chosenGroupGender == 2 ? eGender.MALE : eGender.FEMALE);
            })) return false;

        if (chosenGroupGender == 4
            && !(group.opponents.some(function(opp) { return opp.selectGender == eGender.MALE; })
                 && group.opponents.some(function(opp) { return opp.selectGender == eGender.FEMALE; })))
            return false;

        return true;
    })
}

/************************************************************
 * Common function to selectGroup and clickedRandomGroupButton
 * to load the members of a group (preset table)
 ************************************************************/
function loadGroup (chosenGroup) {
    if (!chosenGroup) return;

	clickedRemoveAllButton();
    console.log(chosenGroup.title);
    
    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            category: 'select',
            message: 'Loading group '+chosenGroup.title,
            level: 'info'
        });
    }

    if (useGroupBackgrounds) {
        if (chosenGroup.background && backgrounds[chosenGroup.background]) {
            backgrounds[chosenGroup.background].activateBackground();
        } else {
            optionsBackground.activateBackground();
        }
    }

    /* load the group members */
	for (var i = 1; i < 5; i++) {
        var member = chosenGroup.opponents[i-1];
        if (member) {
            if (players.some(function(p, j) { return i != j && p == member; })) {
                member = member.clone();
            }
            
            var costumeDesc = $groupCostumeSelectors[i-1].children(':selected').data('costumeDescriptor');
            var selectedCostume = costumeDesc ? costumeDesc.folder : null;

            if ((member.selected_costume && selectedCostume != member.selected_costume)
                || (!member.selected_costume && selectedCostume != null)) {
                member.selectAlternateCostume(costumeDesc);
            }

            if (SENTRY_INITIALIZED) {
                Sentry.addBreadcrumb({
                    category: 'select',
                    message: 'Loading group opponent ' + member.id,
                    level: 'info'
                });
            }

            member.loadBehaviour(i);
            players[i] = member;
        }
	}

	updateSelectionVisuals();
}

/************************************************************
 * The player clicked on the select random group slot.
 ************************************************************/
function clickedRandomGroupButton () {
	selectedSlot = 1;
	/* get a random number for the group listings */
	var randomGroupNumber = getRandomNumber(0, loadedGroups.length);
	var chosenGroup = loadedGroups[randomGroupNumber];

    /* workaround for preset costumes */
	for (var i = 0; i < 4; i++) {
	    var costume = chosenGroup.costumes[i];
	    
        if (costume) {
            var costumeFolder = (costume.toLowerCase() == "default") ? '' : "opponents/reskins/" + costume;
            
            fillCostumeSelector($groupCostumeSelectors[i], chosenGroup.opponents[i].alternate_costumes, costumeFolder);
        } else {
            $groupCostumeSelectors[i].empty();
        }
	}

	loadGroup(chosenGroup);
}

/************************************************************
 * The player clicked on the all random button.
 ************************************************************/
function clickedRandomFillButton (predicate) {
	/* compose a copy of the loaded opponents list */
	var loadedOpponentsCopy = loadedOpponents.filter(function(opp) {
        // Filter out characters that can't be selected via the regular view
        return (opp.selectionCard.isVisible(individualSelectTesting, true) && 
                (!predicate || predicate(opp)));
    });

	/* select random opponents */
	for (var i = 1; i < players.length; i++) {
		/* if slot is empty */
		if (!(i in players)) {
			/* select random opponent */
			var randomOpponent = getRandomNumber(0, loadedOpponentsCopy.length);

            if (SENTRY_INITIALIZED) {
                Sentry.addBreadcrumb({
                    category: 'select',
                    message: 'Loading random opponent ' + loadedOpponentsCopy[randomOpponent].id,
                    level: 'info'
                });
            }

			/* load opponent */
			players[i] = loadedOpponentsCopy[randomOpponent];
			players[i].loadBehaviour(i);

			/* remove random opponent from copy list */
			loadedOpponentsCopy.splice(randomOpponent, 1);
		}
	}

	updateSelectionVisuals();
}

function loadDefaultFillSuggestions () {
    /* get a copy of the loaded opponents list, same as above */
    var possiblePicks = loadedOpponents.filter(function (opp) {
        if (players.some(function (p) { return p && p.id === opp.id; })) {
            return false;
        }

        if (!individualSelectTesting) {
            return opp.highlightStatus === DEFAULT_FILL;
        } else {
            return opp.status === "testing";
        }
    });
    
    var fillPlayers = [];
    if (DEFAULT_FILL === 'new') {
        /* Special case: for the 'new' fill mode, always suggest the most
         * recently-added or recently-updated character.
         *
         * For the testing view, this requires sorting the list of prefills by
         * increasing chronological order.
         *
         * In both cases, the character to suggest first is always at the back
         * of the list.
         */
        if (individualSelectTesting) {
            possiblePicks.sort(sortOpponentsByMultipleFields("lastUpdated"));
        }

        fillPlayers.push(possiblePicks.pop());
    }

    for (var i = fillPlayers.length; i < players.length-1; i++) {
        if (possiblePicks.length === 0) break;
        /* select random opponent */
        var idx = getRandomNumber(0, possiblePicks.length);
        var randomOpponent = possiblePicks[idx];
        possiblePicks.splice(idx, 1);

        fillPlayers.push(randomOpponent);
    }

    for (var i = 0; i < mainSelectDisplays.length; i++) {
        mainSelectDisplays[i].setPrefillSuggestion(fillPlayers[i]);
    }

    suggestedTestingOpponents = individualSelectTesting;
}

function updateDefaultFillView() {
    if (suggestedTestingOpponents !== individualSelectTesting) {
        loadDefaultFillSuggestions();
    }
}

/************************************************************
 * The player clicked on the remove all button.
 ************************************************************/
function clickedRemoveAllButton ()
{
    for (var i = 1; i < 5; i++) {
        if (players[i]) {
            players[i].unloadOpponent();
            delete players[i];
            $selectImages[i-1].off('load');
        }
    }
    updateSelectionVisuals();
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
    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            'category': 'select',
            'message': 'Loading group at page '+groupPage,
            'level': 'info'
        });
    }

    loadGroup(selectableGroups[groupPage]);

    if (SENTRY_INITIALIZED) Sentry.setTag("screen", "select-main");

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
            groupPage = 0;
		} else if (page == 1) {
			/* go to last page */
			groupPage = selectableGroups.length-1;
		} else {
			/* go to selected page */
			groupPage = Number($groupPageIndicator.val()) - 1;
		}
	} else {
		groupPage += page;
    }
    
    if (SENTRY_INITIALIZED) {
        Sentry.addBreadcrumb({
            'category': 'select',
            'level': 'info',
            'message': 'Going to preset table page ' + groupPage + ' / ' + (selectableGroups.length-1),
            'data': {
                'skip': String(skip),
                'page': String(page)
            }
        });
    }

	updateGroupSelectScreen();
    updateGroupCountStats();
}


/************************************************************
 * Adds hotkey functionality to the group selection screen.
 ************************************************************/


function groupSelectScreen_keyUp(e)
{
    console.log(e)
    if ($('#group-select-screen').is(':visible')
        && !$groupButton.prop('disabled')) {
        if (e.keyCode == 37) { // left arrow
            changeGroupPage(false, -1);
        }
        else if (e.keyCode == 39) { // right arrow
            changeGroupPage(false, 1);
        }
        else if (e.keyCode == 13) { // enter key
            selectGroup();
        }
    }
}
$groupSelectScreen.data('keyhandler', groupSelectScreen_keyUp);

/************************************************************
 * The player clicked on the back button on the individual or
 * group select screen.
 ************************************************************/
function backToSelect () {
    /* switch screens */
    if (SENTRY_INITIALIZED) Sentry.setTag("screen", "select-main");

    if (useGroupBackgrounds) optionsBackground.activateBackground();

	screenTransition($individualSelectScreen, $selectScreen);
	screenTransition($groupSelectScreen, $selectScreen);
}

/************************************************************
 * The player clicked on the start game button on the main
 * select screen.
 ************************************************************/
function advanceSelectScreen () {
    console.log("Starting game...");

    gameID = generateRandomID();

    if (USAGE_TRACKING) {
        if (SENTRY_INITIALIZED) {
            Sentry.setTag("in_game", true);
            Sentry.setTag("screen", "game");

            Sentry.addBreadcrumb({
                category: 'game',
                message: 'Starting game.',
                level: 'info'
            });
        }

        var usage_tracking_report = {
            'date': (new Date()).toISOString(),
			'commit': VERSION_COMMIT,
            'type': 'start_game',
            'session': sessionID,
            'game': gameID,
            'userAgent': navigator.userAgent,
            'origin': getReportedOrigin(),
            'table': {},
			'tags': humanPlayer.tags
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

    var playedCharacters = save.getPlayedCharacterSet();
    players.forEach(function(player) {
        if (player.id !== 'human') {
            playedCharacters.push(player.id);
        }

        player.preloadStageImages(0);
    });

    save.savePlayedCharacterSet(playedCharacters);

	transcriptHistory = [];
    inGame = true;

    advanceToNextScreen($selectScreen);
}

/************************************************************
 * The player clicked on the back button on the main select
 * screen.
 ************************************************************/
function backSelectScreen () {
    if (SENTRY_INITIALIZED) Sentry.setTag("screen", "title");
	screenTransition($selectScreen, $titleScreen);
}

/* The player selected an alternate costume for an opponent.
 * `slot` is the 1-based opponent slot affected.
 * `inGroup` is true if the affected opponent is on the group selection screen.
 */
function altCostumeSelected(slot) {
    var costumeSelector = $groupCostumeSelectors[slot-1];
    var opponent = selectableGroups[groupPage].opponents[slot-1];

    var costumeDesc = costumeSelector.children(':selected').data('costumeDescriptor');
    opponent.selectAlternateCostume(costumeDesc);
    $groupImages[slot-1].attr('src', opponent.selection_image);
    updateGenderIcon($groupGenders[slot-1], opponent);
}

/**********************************************************************
 *****                     Display Functions                      *****
 **********************************************************************/

/************************************************************
 * Displays all of the current players on the main select
 * screen.
 ************************************************************/
function updateSelectionVisuals () {
    /* Check to see if all opponents are loaded. */
    var filled = 0, loaded = 0;
    players.forEach(function(p, idx) {
        if (idx > 0) {
            filled++;
            if (p.isLoaded()) {
                loaded++;
            }
        }
    });

    /* Update suggestions images. */
    updateDefaultFillView();

    if (loaded >= 2) {
        var suggested_opponents = loadedOpponents.filter(function(opp) {
            if (individualSelectTesting && opp.status !== "testing") return false;
            return opp.selectionCard.isVisible(individualSelectTesting, true);
        });

        /* sort opponents */
        suggested_opponents.sort(sortOpponentsByMostTargeted());

        var suggestion_idx = 0;
        for (var i = 1; i < players.length; i++) {
            if (players[i] === undefined) {
                for (var j = 0; j < 4; j++) {
                    mainSelectDisplays[i - 1].updateTargetSuggestionDisplay(
                        j, suggested_opponents[suggestion_idx++]
                    );
                }
                mainSelectDisplays[i - 1].displayTargetSuggestions(true);
            } else {
                mainSelectDisplays[i - 1].displayTargetSuggestions(false);
            }
        }
    } else {
        for (var i = 1; i < players.length; i++) {
            if (players[i] === undefined) {
                mainSelectDisplays[i - 1].displayTargetSuggestions(false);
            }
        }
    }

    /* update all opponents */
    for (var i = 1; i < players.length; i++) {
        mainSelectDisplays[i - 1].update(players[i]);
    }

    /* If the individual selection view type is locked, then disable the view
     * mode toggle button.
     */
    $("#select-group-testing-button").attr("disabled", isIndividualSelectViewTypeLocked());

    /* Hide the "Preset Tables" and "Random Table" buttons when viewing the
     * Testing roster. */
    if (individualSelectTesting) {
        $("#select-group-button").hide();
        $selectRandomTableButton.hide();
    } else {
        $("#select-group-button").show();
        $selectRandomTableButton.show();
    }

    /* if enough opponents are selected, and all those are loaded, then enable progression */
    $selectMainButton.attr('disabled', filled < 2 || loaded < filled);

    /* if all slots are taken, disable fill buttons */
    $selectRandomButtons.attr('disabled', filled >= 4 || loadedOpponents.length == 0);

    /* if no opponents are loaded, disable remove all button */
    $selectRemoveAllButton.attr('disabled', filled <= 0 || loaded < filled);

    /* Disable buttons while loading is going on */
    $selectRandomTableButton.attr('disabled', loaded < filled || loadedOpponents.length == 0);
    $groupButton.attr('disabled', loaded < filled);
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

function clearSearch() {
    $searchName.val(null);
    $searchTag.val(null);
    $searchSource.val(null);

    // perform the search and sort logic, then update
    updateIndividualSelectFilters();
}

function changeSearchGender(gender) {
    chosenGender = gender;
    setActiveOption("search-gender", gender);
    updateIndividualSelectFilters(true);
}

$('ul#search-gender').on('click', 'a', function() {
    changeSearchGender(parseInt($(this).attr('data-value'), 10));
});

function openGroupSearchModal() {
    $groupSearchModal.modal('show');
}

function closeGroupSearchModal() {
    // perform the search and sort logic
    updateSelectableGroups();

    // update
    updateGroupSelectScreen();
    updateGroupCountStats();
}

function clearGroupSearch() {
    $groupSearchName.val(null);
    $groupSearchGroupName.val(null);
    $groupSearchTag.val(null);
    $groupSearchSource.val(null);
    closeGroupSearchModal();
}

function changeGroupSearchGender(gender) {
    chosenGroupGender = gender;
    setActiveOption("group-search-gender", gender);
}

$('ul#group-search-gender').on('click', 'a', function() {
    changeGroupSearchGender(parseInt($(this).attr('data-value'), 10));
});

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

/**
 * Special Callback for Arrays.sort to sort an array of opponents on
 * the total number of lines targeting them the currently selected
 * opponents have.
 */
function sortOpponentsByMostTargeted() {
	return function(opp1, opp2) {
		counts = [opp1, opp2].map(function(opp) {
			return opp.inboundLinesFromSelected(individualSelectTesting ? "testing" : undefined);
		});
		if (counts[0] > counts[1]) return -1;
		if (counts[0] < counts[1]) return 1;
		return 0;
	}
}

/**
 * Special callback for Arrays.sort to sort an array of opponents using the
 * Testing-specific rules. The sort order produced by this callback is:
 * - highlight="sponsorship"
 * - status="testing"
 * - everything else
 */
function sortTestingOpponents(opp1, opp2) {
    var scores = [opp1, opp2].map(function (opp) {
        if (opp.highlightStatus === "sponsorship") {
            return 2;
        } else if (opp.status === "testing") {
            return 1;
        } else {
            return 0;
        }
    });

    return scores[1] - scores[0];
}

function setSortingMode(mode) {
    sortingMode = mode;
    $("#sort-dropdown-selection").html(sortingMode); // change the dropdown text to the selected option
    updateIndividualSelectSort();
}

/** Event handler for the sort dropdown options. Fires when user clicks on a dropdown item. */
$sortingOptionsItems.on("click", function(e) {
    setSortingMode($(this).find('a').html());
});

/************************************************************
 * Dynamic dialogue and image counting functions
 ************************************************************/

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

            opp.fetchBehavior().then(countLinesImages).then(function(response) {
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
 * Callback to parse the number of lines of dialogue and number of images
 * given a character's behaviour XML. Returns the counts as an object with
 * properties numTotalLines, numUniqueLines, and numPoses.
 */
function countLinesImages(xml) {
    // parse all lines of dialogue and all images
	var numTotalLines = 0;
    var lines = new Set();
    var poses = new Set();
    
    var matched = $(xml).find('state').get();
    var layers = $(xml).find('>wardrobe>clothing').length;
    var deferred = $.Deferred();
    
    /* Avoid blocking the UI by breaking the work into smaller chunks. */
    function process () {
        var startTs = Date.now();
            
        if (DEBUG) console.log("Processing: "+matched.length+" states to go");
        do {
            data = matched.shift();
            
            numTotalLines++;
            
        	// count only unique lines of dialogue
            if (data.textContent.trim() != "") lines.add(data.textContent.trim());
            if ($(data).children('text').length) lines.add($(data).children('text').html().trim());
            
        	// count unique number of poses used in dialogue
        	// note that this number may differ from actual image count if some images
        	// are never used, or if images that don't exist are used in the dialogue
            
            var $case = $(data).parent();
            var $trigger = $case.parent('trigger');
            var $stage = $case.parent('stage');
            var stageInterval = $trigger.length ? getRelevantStagesForTrigger($trigger.attr('id'), layers)
                : $stage.length ? { min: $case.parent('stage').attr('id'), max: $case.parent('stage').attr('id') }
                : { min: 0, max: 0 };

            for (var stage = stageInterval.min; stage <= stageInterval.max; stage++) {
                var images = $(data).children('alt-img').filter(function() {
                    return checkStage(stage, $(this).attr('stage'));
                }).map(function() { return $(this).text(); }).get();
                if (images.length == 0) images = [ $(data).attr('img') ];
                images.forEach(function(poseName) {
                    if (!poseName) return;
                    poses.add(poseName.replace('#', stage));
                });
            }
        } while (Date.now() - startTs < 50 && matched.length > 0);
        
        if (matched.length > 0) {
            setTimeout(process.bind(null), 50);
        } else {
            return deferred.resolve({
                numTotalLines : numTotalLines,
                numUniqueLines : lines.size,
                numPoses : poses.size
            });
        }
    }
    
    process();
    return deferred.promise();
}
