/********************************************************************************
 * Bug reporting (automatic and manual) and feedback
 ********************************************************************************/

$bugReportModal = $('#bug-report-modal');
$feedbackReportModal = $('#feedback-report-modal');
$usageTrackingModal = $('#usage-reporting-modal');

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
        for (let i=1;i<players.length;i++) {
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
                }

                tableReports[i-1] = playerData;
            } else {
                tableReports[i-1] = null;
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

window.addEventListener('error', function (ev) {
    var errData = {
        'date': (new Date()).toISOString(),
        'message': ev.message,
        'filename': ev.filename,
        'lineno': ev.lineno,

    }

    if (ev.error) {
        errData.type = ev.error.name;
        errData.stack = ev.error.stack;
    }

    jsErrors.push(errData);

    if (USAGE_TRACKING) {
        var report = compileBaseErrorReport('Automatically generated after Javascript error.', 'auto');

        $.ajax({
            url: BUG_REPORTING_ENDPOINT,
            method: 'POST',
            data: JSON.stringify(report),
            contentType: 'application/json',
            error: function (jqXHR, status, err) {
                console.error("Could not send bug report - error "+status+": "+err);
            },
        });
    }
});

/*
 * Bug Report Modal functions
 */

function getBugReportJSON() {
    var desc = $('#bug-report-desc').val();
    var type = $('#bug-report-type').val();
    var character = undefined;

    var report = compileBaseErrorReport(desc, type);
    return JSON.stringify(report);
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
    $('#bug-report-output').val(getBugReportJSON());
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

    $.ajax({
        url: BUG_REPORTING_ENDPOINT,
        method: 'POST',
        data: getBugReportJSON(),
        contentType: 'application/json',
        error: function (jqXHR, status, err) {
            console.error("Could not send bug report - error "+status+": "+err);
            $('#bug-report-status').text("Failed to send bug report (error "+status+")");
        },
        success: function () {
            $('#bug-report-status').text("Bug report sent!");
            $('#bug-report-desc').val("");
            $('#bug-report-type').empty();
            closeBugReportModal();
        }
    });
}

$('#bug-report-type').change(updateBugReportOutput);
$('#bug-report-desc').change(updateBugReportOutput);
$('#bug-report-copy-btn').click(copyBugReportOutput);

if (!document.fullscreenEnabled) {
    $('.title-fullscreen-button, .game-menu-dropup li:has(#game-fullscreen-button), #epilogue-fullscreen-button').hide();
}

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

    $.ajax({
        url: FEEDBACK_ROUTE + (character || ""),
        method: 'POST',
        data: JSON.stringify(report),
        contentType: 'application/json',
        error: function (jqXHR, status, err) {
            console.error("Could not send feedback report - error " + status + ": " + err);
            $('#feedback-report-status').text("Failed to send feedback report (error " + err + ")");
        },
        success: function () {
            $('#feedback-report-status').text("Feedback sent!");
            $('#feedback-report-desc').val("");
            $('#feedback-report-character').empty()
            closeFeedbackReportModal();
        }
    });
}

function updateFeedbackSendButton() {
    if (
        !!$("#feedback-report-character").val() &&
        $('#feedback-report-desc').val().trim().length > 0
    ) {
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

    $.when.apply($, feedbackCharacters.map(function(p) {
        $("#feedback-report-character").append($('<option>', { text: p.id.initCap(), value: p.id }).data('character', p));
        if (p.feedbackData) {
            return true;
        } else {
            return $.ajax({
                url: FEEDBACK_ROUTE + p.id,
                type: "GET",
                dataType: "json",
            }).then(function(data) {
                p.feedbackData = data;
            }, function() {
                console.error("Failed to get feedback message data for " + p.id);
                return $.Deferred().resolve().promise(); /* This is meant to avoid hiding the "Loading..." 
                                                            entry right away if one GET fails. */
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

/*
 * Show the usage tracking consent modal.
 */

function showUsageTrackingModal() {
    $usageTrackingModal.modal('show');
}

function enableUsageTracking() {
    USAGE_TRACKING = true;
    save.saveUsageTracking();
    sentryInit();
}

function disableUsageTracking() {
    USAGE_TRACKING = false;
    save.saveUsageTracking();
}

function sentryInit() {
    if (USAGE_TRACKING && !SENTRY_INITIALIZED) {
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

        SENTRY_INITIALIZED = true;
    }
}
