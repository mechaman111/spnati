/* Metadata for opponents to use in testing.
 *
 * I'm using my own characters here so that others don't have to worry
 * about unintentionally breaking the tests with character updates.
 * (- FarawayVision)
 */
 
var opponent_metadata = {};
var opponent_fixtures = {};

function load_opponent_fixture (id) {
    return function (done) {
        fetchCompressedURL('opponents/'+id+'/meta.xml').then(function (data) {
            opponent_metadata[id] = $(data);
            opponent_fixtures[id] = new Opponent (id, opponent_metadata[id], undefined, 0);

            opponent_fixtures[id].loadBehaviour(0, true, function () {
                opponent_fixtures[id].resetState();
                done();
            }, function () {
                return done(new Error("Error loading "+id));
            });
        }).fail(done);
    }
}

before('loading test dummy', load_opponent_fixture('test_dummy'));
before('loading Chihiro', load_opponent_fixture('chihiro'));
before('loading HK416', load_opponent_fixture('hk416'));

beforeEach(function () {
    DEBUG = true;
});

function getOpponent (id, slot) {
    var opp = opponent_fixtures[id];
    opp.slot = slot;
    opp.resetState(true);

    players[slot] = opp;
    return opp;
}

function setOpponentStage (opp, stage) {
    var prevStage = opp.stage;
    
    opp.stage = stage;
    for (var i=0;i<(stage-prevStage);i++) {
        opp.clothing.pop();
    }
}

function setupTableFixture (slot_ids) {
    slot_ids.forEach(function (id, idx) {
        if (!id) return;
        
        loadOpponent(id, idx+1);
    });
}

function cleanUpPlayers () {
    players.forEach(function (pl) {
        pl.resetState(true);
    })

    players = Array(5);
}

function dummyState (attrs, parentCase) {
    var caseObj = new State($('<state></state>'), parentCase);
    
    Object.keys(attrs).forEach(function (k) {
        caseObj[k] = attrs[k];
    });
    
    return caseObj;
}

function dummyCase (attrs) {
    var caseObj = new Case($('<case></case>'), null);
    
    Object.keys(attrs).forEach(function (k) {
        caseObj[k] = attrs[k];
    });
    
    if (caseObj.states.length === 0) {
        /* ensure cases always have at least one child State */
        caseObj.states.push(dummyState({}, caseObj));
    }
    
    return caseObj;
}

function dummyCollectible (player, attrs) {
    var collectible = new Collectible($('<collectible></collectible>'), player);
    
    Object.keys(attrs).forEach(function (k) {
        collectible[k] = attrs[k];
    });
    
    return collectible;
}
