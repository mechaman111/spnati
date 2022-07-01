/********************************************************************************
 * Bug reporting (automatic and manual) and feedback
 ********************************************************************************/

var USAGE_TRACKING_ENDPOINT = 'https://spnati.faraway-vision.io/usage/report';
var BUG_REPORTING_ENDPOINT = 'https://spnati.faraway-vision.io/usage/bug_report';
var FEEDBACK_ROUTE = "https://spnati.faraway-vision.io/usage/feedback";

/*
 * Usage tracking types:
 * Basic info: table composition, game state, UI info, game ID
 * Persistent/Session info: session IDs, epilogues + collectibles unlocked, number of unique characters played
 * Demographics info: player tags and clothing
 */

$bugReportModal = $('#bug-report-modal');
$feedbackReportModal = $('#feedback-report-modal');

function getReportedOrigin () {
    var origin = window.location.origin;

    if (origin.toLowerCase().startsWith('file:')) {
        return '<local filesystem origin>';
    } else {
        return origin;
    }
}

/* Gathers most of the generic information for an error report. */
function compileBaseErrorReport(userDesc, bugType) {
    var bugCharacter = null;
    if (bugType.startsWith('character')) {
        bugCharacter = bugType.split(':', 2)[1];
        bugType = 'character';
    }

    var circumstances = {
        'userAgent': navigator.userAgent,
        'origin': getReportedOrigin(),
        'visibleScreens': [],
        'uiTheme': UI_THEME,
    };

    var data = {
        'date': (new Date()).toISOString(),
        'commit': VERSION_COMMIT,
        'session': sessionID,
        'game': gameID,
        'type': bugType,
        'character': bugCharacter,
        'description': userDesc,
        'circumstances': circumstances,
        'player': {
            'gender': humanPlayer.gender,
            'size': humanPlayer.size,
        },
        'jsErrors': jsErrors,
    };

    if (epiloguePlayer) {
        data.epilogue = {
            epilogue: epiloguePlayer.epilogue.title,
            player: epiloguePlayer.epilogue.player.id,
            gender: epiloguePlayer.epilogue.gender,
            scene: epiloguePlayer.sceneIndex,
            view: epiloguePlayer.viewIndex,
            directive: epiloguePlayer.directiveIndex,
        };

        if (epiloguePlayer.activeScene) {
            data.epilogue.sceneName = epiloguePlayer.activeScene.name;
            for (let i = epiloguePlayer.directiveIndex; i >= 0; i--) {
                let directive = epiloguePlayer.activeScene.directives[i];
                if (directive && directive.type == "text") {
                    data.epilogue.lastText = directive.text;
                    break;
                }
            }
        }
    } else {
        var gameState = {
            'currentRound': currentRound,
            'currentTurn': currentTurn,
            'previousLoser': previousLoser,
            'recentLoser': recentLoser,
            'gameOver': gameOver,
            'rollback': inRollback()
        };
        mergeObjects(circumstances, gameState);
        if (gamePhase) {
            if (inRollback()) {
                circumstances.gamePhase = rolledBackGamePhase[0];
            } else {
                circumstances.gamePhase = gamePhase[0];
            }
        }

        var tableReports = [];
        for (let i = 0; i < players.length; i++) {
            if (players[i]) {
                playerData = {
                    'id': players[i].id,
                    'slot': i,
                    'stage': players[i].stage,
                    'timeInStage': players[i].timeInStage,
                    'markers': players[i].markers,
                    'oneShotCases': players[i].oneShotCases,
                    'oneShotStates': players[i].oneShotStates,
                }

                if (players[i].chosenState) {
                    playerData.currentLine    = players[i].chosenState.rawDialogue;
                    if (players[i].chosenState.image) {
                        playerData.currentImage   = players[i].folder + players[i].chosenState.image.replace('#', players[i].stage);
                    }
                    if (players[i].chosenState.parentCase && players[i].chosenState.parentCase.variableBindings) {
                        playerData.variableBindings = {};
                        for (let v in players[i].chosenState.parentCase.variableBindings) {
                            playerData.variableBindings[v] = players[i].chosenState.parentCase.variableBindings[v].slot;
                        }
                    }
                }

                tableReports[i] = playerData;
            } else {
                tableReports[i] = null;
            }
        }
        data.table = tableReports;
    }

    for (let i=0;i<allScreens.length;i++) {
        if (allScreens[i].css('display') !== 'none') {
            circumstances.visibleScreens.push(allScreens[i].attr('id'));
        }
    }

    return data;
}

function logError(err, message, fileName, lineNum) {
    var errData = { 'date': (new Date()).toISOString() };
    if (err) {
        errData.type = err.name;
        errData.stack = err.stack;

        if (!message) message = err.message;
        if (!fileName) fileName = err.fileName;
        if (!lineNum) lineNum = err.lineNumber;

        errData.message = message;
        errData.filename = fileName;
        errData.lineno = lineNum;
    }

    jsErrors.push(errData);

    var report = compileBaseErrorReport('Automatically generated after Javascript error.', 'auto');
        
    // swallow errors here so we don't call this function recursively
    return postJSON(BUG_REPORTING_ENDPOINT, report).catch(function() {});
}

/** Helper function for manually logging / capturing errors. */
function captureError(err) {
    console.error(err);
    
    if (!(err instanceof Error)) {
        return;
    }

    logError(err);
    Sentry.captureException(err);
}


window.addEventListener('error', function (ev) {
    /* Sentry has its own listener for capturing exceptions, so we don't need
     * captureException here
     */
    logError(ev.error, ev.message, ev.filename, ev.lineno);
});

window.addEventListener('onunhandledrejection', function (ev) {
    if (!(ev.reason instanceof Error)) {
        return;
    }

    logError(ev.reason);
});


/*
 * Bug Report Modal functions
 */

function getBugReport() {
    var desc = $('#bug-report-desc').val();
    var type = $('#bug-report-type').val();

    return compileBaseErrorReport(desc, type);
}

function updateBugReportSendButton() {
    if($('#bug-report-desc').val().trim().length > 0) {
        $("#bug-report-modal-send-button").removeAttr('disabled');
    } else {
        $("#bug-report-modal-send-button").attr('disabled', 'true');
    }
}

$('#bug-report-desc').keyup(updateBugReportSendButton);

/* Update the bug report text dump. */
function updateBugReportOutput() {
    $('#bug-report-output').val(JSON.stringify(getBugReport()));
    $('#bug-report-status').text("");

    updateBugReportSendButton();
}

function copyBugReportOutput() {
    var elem = $('#bug-report-output')[0];
    elem.select();
    document.execCommand("copy");
}

function sendBugReport() {
    if($('#bug-report-desc').val().trim().length == 0) {
        $('#bug-report-status').text("Please enter a description first!");
        return false;
    }

    return postJSON(BUG_REPORTING_ENDPOINT, getBugReport()).then(function () {
        $('#bug-report-status').text("Bug report sent!");
        $('#bug-report-desc').val("");
        $('#bug-report-type').empty();
        closeBugReportModal();
    }).catch(function (err) {
        captureError(err);
        var msg = "";
        if (err instanceof Error) {
            msg = err.message;
        } else {
            msg = err.toString();
        }
        $('#bug-report-status').text("Failed to send bug report (" + msg + ")");
    });
}

$('#bug-report-type').change(updateBugReportOutput);
$('#bug-report-desc').change(updateBugReportOutput);
$('#bug-report-copy-btn').click(copyBugReportOutput);

 /************************************************************
  * The player clicked a bug-report button. Shows the bug reports modal.
  ************************************************************/
function showBugReportModal () {
    var prevVal = $('#bug-report-type').val();
    /* Set up possible bug report types. */
    var bugReportTypes = [
        ['freeze', 'Game Freeze or Crash'],
        ['display', 'Game Graphical Problem'],
        ['other', 'Other Game Issue'],
    ].concat((epiloguePlayer ? [ epiloguePlayer.epilogue.player ] : players.opponents).map(function(p) {
        return [ 'character:'+p.id, (epiloguePlayer ? 'Epilogue' : 'Character') + ' Defect ('+p.id.initCap()+')'];
    }));

    $('#bug-report-type').empty().append(bugReportTypes.map(function(item) {
        return $('<option>', { value: item[0], text: item[1] });
    }));
    if (prevVal && bugReportTypes.some(function(t) { return t[0] === prevVal; })) {
        $('#bug-report-type').val(prevVal);
    } else if (epiloguePlayer) {
        $('#bug-report-type').val('character:'+epiloguePlayer.epilogue.player.id);
    }

    updateBugReportOutput();

    $bugReportModal.modal('show');
}

$bugReportModal.on('shown.bs.modal', function() {
    $('#bug-report-type').focus();
});

function closeBugReportModal() {
    $bugReportModal.modal('hide');
}

 /************************************************************
  * Functions for the feedback reporting modal.
  ************************************************************/

function sendFeedbackReport() {
    if ($('#feedback-report-desc').val().trim().length == 0) {
        $('#feedback-report-status').text("Please enter a description first!");
        return false;
    }

    var desc = $('#feedback-report-desc').val();
    var character = $('#feedback-report-character').val();
    var report = compileBaseErrorReport(desc, "feedback");
    var endpoint = FEEDBACK_ROUTE + (character ? '/' + character : '');

    return postJSON(endpoint, report).then(function () {
        $('#feedback-report-status').text("Feedback sent!");
        $('#feedback-report-desc').val("");
        $('#feedback-report-character').empty()
        closeFeedbackReportModal();
    }).catch(function (err) {
        captureError(err);
        var msg = "";
        if (err instanceof Error) {
            msg = err.message;
        } else {
            msg = err.toString();
        }

        console.error("Could not send feedback report - " + msg);
        $('#feedback-report-status').text("Failed to send feedback report (" + msg + ")");
    });
}

function updateFeedbackSendButton() {
    if ($('#feedback-report-desc').val().trim().length > 0) {
        $("#feedback-report-modal-send-button").removeAttr('disabled');
    } else {
        $("#feedback-report-modal-send-button").attr('disabled', 'true');
    }
}

$('#feedback-report-desc').keyup(updateFeedbackSendButton).change(updateFeedbackSendButton);

function updateFeedbackMessage() {
    var player = $('#feedback-report-character option:selected').data('character');

    $(".feedback-message-container").hide();
    $("#feedback-disabled-warning").hide();

    if (player && player.feedbackData) {
        if (player.feedbackData.enabled && player.feedbackData.message) {
            $(".feedback-message-container").show();
            $(".feedback-character-name").text(player.label);
            $(".feedback-message").text(player.feedbackData.message);
        } else if (!player.feedbackData.enabled) {
            $("#feedback-disabled-warning").show();
        }
    }
}

$("#feedback-report-character").change(updateFeedbackMessage);

function showFeedbackReportModal($fromModal) {
    var prevVal = $('#feedback-report-character').val();
    $('#feedback-report-character').empty().append(
        $('<option disabled data-load-indicator="">Loading...</option>'),
        $('<option value="">General Game Feedback</option>')
    );

    var feedbackCharacters = epiloguePlayer && !inGame ? [ epiloguePlayer.epilogue.player ] : players.opponents;


    Promise.all(feedbackCharacters.map(function(p) {
        $("#feedback-report-character").append($('<option>', { text: p.id.initCap(), value: p.id }).data('character', p));
        if (p.feedbackData) {
            return Promise.resolve();
        } else {
            return fetch(FEEDBACK_ROUTE + '/' + p.id, { method: "GET" }).then(function (resp) {
                if (resp.status < 200 || resp.status > 299) {
                    throw new Error("Fetching " + FEEDBACK_ROUTE + p.id + " failed with error " + resp.status + ": " + resp.statusText);
                } else {
                    return resp.json();
                }
            }).then(function(data) {
                p.feedbackData = data;
            }).catch(function (err) {
                console.error("Failed to get feedback message data for " + p.id);
                captureError(err);
            });
        }
    })).then(function() {
        $("#feedback-report-character option[data-load-indicator]").remove();
        if (prevVal && feedbackCharacters.indexOf(prevVal) >= 0) {
            $('#feedback-report-character').val(prevVal);
        } else if (epiloguePlayer) {
            $('#feedback-report-character').val(epiloguePlayer.epilogue.player.id);
        }
        updateFeedbackMessage();
    });

    if ($fromModal) {
        $fromModal.modal('hide');
        $feedbackReportModal.one('hide.bs.modal', function() {
            $fromModal.modal('show');
        });
    }
    $feedbackReportModal.modal('show');
}

function closeFeedbackReportModal() {
    $feedbackReportModal.modal('hide');
}

$feedbackReportModal.on('shown.bs.modal', function () {
    $('#feedback-report-character').focus();
});

function sentryInit() {
    console.log("Initializing Sentry...");

    var sentry_opts = {
        dsn: 'https://df511167a4fa4a35956a8653ff154960@sentry.io/1508488',
        release: VERSION_TAG,
        maxBreadcrumbs: 100,
        integrations: [new Sentry.Integrations.Breadcrumbs({
            console: false,
            dom: false
        })],
        beforeSend: function (event, hint) {
            /* Inject additional game state data into event tags: */
            if (!event.extra) event.extra = {};

            event.tags.commit = VERSION_COMMIT;

            if (inGame && !epiloguePlayer) {
                event.extra.recentLoser = recentLoser;
                event.extra.previousLoser = previousLoser;
                event.extra.gameOver = gameOver;
                event.extra.currentTurn = currentTurn;
                event.extra.currentRound = currentRound;

                event.tags.rollback = inRollback();
                event.tags.gamePhase = getGamePhaseString(gamePhase);
                event.tags.uiTheme = UI_THEME;
            }

            if (epiloguePlayer) {
                event.tags.epilogue = epiloguePlayer.epilogue.title;
                event.tags.epilogue_player = epiloguePlayer.epilogue.player.id;
                event.tags.epilogue_gender = epiloguePlayer.epilogue.gender;

                event.extra.loaded = epiloguePlayer.loaded;
                event.extra.directiveIndex = epiloguePlayer.directiveIndex;
                event.extra.sceneIndex = epiloguePlayer.sceneIndex;
                event.extra.viewIndex = epiloguePlayer.viewIndex;
            }

            var n_players = 0;
            for (var i=1;i<players.length;i++) {
                if (players[i]) {
                    n_players += 1;
                    event.tags["character:" + players[i].id] = true;
                    event.tags["slot-" + i] = players[i].id;

                    if (players[i].alt_costume) {
                        event.tags[players[i].id+":alt-costume"] = players[i].alt_costume.id;
                    }
                } else {
                    event.tags["slot-" + i] = undefined;
                }
            }

            event.tags.n_players = n_players;

            return event;
        }
    };

    if (window.location.origin.indexOf('spnati.net') >= 0) {
        sentry_opts.environment = 'production';
    }

    Sentry.init(sentry_opts);

    Sentry.setUser({
        'id': sessionID,
    });

    Sentry.setTag("game_version", CURRENT_VERSION);
}

function collectBaseUsageInfo(type, includeGameState) {
    var report = {
        'date': (new Date()).toISOString(),
        'commit': VERSION_COMMIT,
        'type': type,
        'origin': getReportedOrigin(),
    };

    if (save.getUsageTrackingInfo().persistent) {
        report.session = sessionID;
    }

    if (includeGameState) {
        let table = {};
        let tableState = {};

        for (let i = 0; i < 5; i++) {
            if (players[i]) {
                let oppInfo = {
                    'stage': players[i].stage,
                    'seenDialogue': 0,
                    'outOrder': players[i].outOrder,
                    'costume': players[i].selected_costume
                };

                if (players[i].repeatLog) oppInfo.seenDialogue = Object.keys(players[i].repeatLog).length;

                tableState[i] = oppInfo;
                if (i > 0) table[i] = players[i].id;
            }
        }

        report.game = gameID;
        report.table = table;
        report.tableState = tableState;
        report.ui = {
            'theme': UI_THEME,
            'minimal': MINIMAL_UI,
            'background': activeBackground.id,
        };
    }

    return report;
}

function collectCommonUsageInfo(type, includeGameState) {
    var report = collectBaseUsageInfo(type, includeGameState);

    if (save.getUsageTrackingInfo().persistent) {
        let unlockedCollectibles = {};
        let unlockedEpilogues = {};
        loadedOpponents.forEach(function (opp) {
            if (opp.collectibles) {
                let unlocked = opp.collectibles.flatMap(function (c) {
                    return c.isUnlocked(true) ? [c.id] : [];
                });
    
                if (unlocked.length > 0) unlockedCollectibles[opp.id] = unlocked;
            }
    
            if (opp.endings) {
                let unlocked = opp.endings.map(function () {
                    /* jQuery's .map actually works like filter-map: returning null/undefined means that no item is inserted into the set. */
                    var rawTitle = $(this).html();
                    if (save.hasEnding(opp.id, rawTitle)) return rawTitle;
                }).get();
    
                if (unlocked.length > 0) unlockedEpilogues[opp.id] = unlocked;
            }
        });
    
        let unlockedGeneralCollectibles = generalCollectibles.flatMap(function (c) {
            return c.isUnlocked(true) ? [c.id] : [];
        });
    
        if (unlockedGeneralCollectibles.length > 0) unlockedCollectibles["__general"] = unlockedGeneralCollectibles;

        report.persistent = {
            'played_characters': save.getPlayedCharacterSet().length,
            'collectibles': unlockedCollectibles,
            'epilogues': unlockedEpilogues,
        };
    }

    if (save.getUsageTrackingInfo().demographics) {
        report.player = {
            'tags': humanPlayer.tags,
        };

        if (includeGameState) {
            report.player.clothing = save.selectedClothing().map(function (c) { return c.id; });
        }
    }

    return report;
}

function sendUsageReport(report) {
    return postJSON(USAGE_TRACKING_ENDPOINT, report).catch(function (err) {
        captureError(err);
    });
}

/**
 * Log the start of a game for bug reporting and analytics.
 */
function recordStartGameEvent() {
    Sentry.setTag("in_game", true);
    Sentry.setTag("screen", "game");

    Sentry.addBreadcrumb({
        category: 'game',
        message: 'Starting game.',
        level: 'info'
    });

    if (save.getUsageTrackingInfo().basic) {
        sendUsageReport(collectCommonUsageInfo('start_game', true));
    }
}

/**
 * Log the end of a game for bug reporting and analytics.
 * @param {string} winner The ID of the winning character.
 */
function recordEndGameEvent(winner) {
    Sentry.addBreadcrumb({
        category: 'game',
        message: 'Game ended with ' + winner + ' winning.',
        level: 'info'
    });

    var report = collectCommonUsageInfo('end_game', true);
    report.winner = winner;
    report.rounds = currentRound;

    if (save.getUsageTrackingInfo().basic) {
        sendUsageReport(report);
    }
}

/**
 * Log a game in progress that's been interrupted, for example because the player navigated to another page.
 * @param {boolean} isPageUnload If true, this event is being sent just before a page unload.
 */
function recordInterruptedGameEvent(isPageUnload) {
    var report = collectBaseUsageInfo("interrupted_game", true);
    report.gameState = {
        'currentRound': currentRound,
        'currentTurn': currentTurn,
        'previousLoser': previousLoser,
        'recentLoser': recentLoser,
        'rollback': inRollback()
    };

    if (gamePhase) {
        if (inRollback()) {
            report.gameState.gamePhase = rolledBackGamePhase[0];
        } else {
            report.gameState.gamePhase = gamePhase[0];
        }
    }

    /* Interruption reasons are related to game sessions. */
    if (save.getUsageTrackingInfo().persistent) report.isPageUnload = isPageUnload;

    if (save.getUsageTrackingInfo().basic) {
        if (isPageUnload) {
            /* It'd be better to use fetch() with the keepalive option here, but that doesn't work on all browsers, particularly Firefox.
            * Instead use synchronous XHR. This will delay page unload, but we don't really have many other options.
            */
            var xhr = new XMLHttpRequest();
            xhr.open("POST", USAGE_TRACKING_ENDPOINT, false);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.send(JSON.stringify(report));
        } else {
            sendUsageReport(report);
        }
    }
}

/**
 * Log the start of an epilogue for bug reporting and analytics.
 * @param {bool} gallery Whether the epilogue was played from the Gallery.
 * @param {Epilogue} epilogue The epilogue that was chosen.
 */
function recordEpilogueEvent(gallery, epilogue) {
    Sentry.addBreadcrumb({
        category: 'epilogue',
        message: 'Starting ' + epilogue.player.id + ' epilogue: ' + epilogue.title,
        level: 'info'
    });

    Sentry.setTag("epilogue_gallery", gallery);
    Sentry.setTag("screen", "epilogue");

    if (save.getUsageTrackingInfo().basic) {
        var report = collectCommonUsageInfo(gallery ? 'gallery' : 'epilogue', !gallery);
        report.chosen = { 'id': epilogue.player.id, 'title': epilogue.title };
        sendUsageReport(report);
    }
}

function showDataCollectionPrompt() {
    $("#selection-data-collection-banner-border").prop("hidden", false);

    $("#enable-data-collection-btn").on("click", hideDataCollectionPrompt.bind(null, true));
    $("#disable-data-collection-btn").on("click", hideDataCollectionPrompt.bind(null, false));
}

function hideDataCollectionPrompt(opt_in) {
    $("#selection-data-collection-banner-border").prop("hidden", true);
    if (opt_in !== null) {
        var curInfo = save.getUsageTrackingInfo();
        save.setUsageTrackingInfo(curInfo.basic, opt_in, opt_in);
    }
}
