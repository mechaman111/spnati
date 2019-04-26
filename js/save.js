//Class for saving user's progress and preferences

function mergeObjects(a, b){
	if(b === undefined || b === null){
		return a;
	}
	else if(a === undefined || a === null){
		return b;
	}
	for(var v in b){
		if (typeof a[v] === 'object') {
			a[v] = mergeObjects(a[v], b[v])
		} else {
			a[v] = b[v];
		}
	}
	return a;
}

function Save() {
    var prefix = 'SPNatI.';
    var endings;

    this.convertCookie = function() {
        var legacyOptionIndices = {
            autoFade: [ true, false ],
            cardSuggest: [ true, false ],
            gameDelay: [ 0, 400, 800, 1200, 1600 ],
            dealAnimation: [ 0, 200, 500, 1000 ],
            autoForfeit: [ 4000, 7500, 10000, null ],
            autoEnding: [ 4000, 7500, 10000, null ],
        };
        var legacyBackgroundIndices = [
            'inventory', 'beach', 'classroom', 'brick', 'night', 'roof',
            'seasonal', 'library', 'bathhouse', 'poolside', 'hot spring',
            'mansion', 'purple room', 'showers', 'street', 'green screen',
            'arcade', 'club', 'bedroom', 'hall', 'locker room',
            'haunted forest', 'romantic', 'classic'
        ];

        var data = Cookies.getJSON('save');
        if (data && typeof(data) == 'object') {
            var options = {};
            for (var key in legacyOptionIndices) {
                if (key in data) {
                    try {
                        var value = legacyOptionIndices[key][data[key] - 1];
                        options[key] = value;
                    } catch (ex) { }
                }
            }
            localStorage.setItem(prefix + 'options', JSON.stringify(options));

            var settings = {};
            if ('masturbationTimer' in data) {
                settings.stamina = data.masturbationTimer;
            }
            if (data.background) {
                settings.background = legacyBackgroundIndices[data.background - 1];
            }
            localStorage.setItem(prefix + 'settings', JSON.stringify(settings));
            if (data.gender) {
                localStorage.setItem(prefix + 'gender', data.gender);
            }
            ['male', 'female'].forEach(function(gender) {
                if (gender in data) {
                    var profile = data[gender];
                    profile.clothing = clothingChoices[gender].filter(function(item, ix) {
                        return profile.clothing[ix];
                    }).map(function(item) { return item.generic; });
                    localStorage.setItem(prefix + gender, JSON.stringify(profile));
                }
            });
            if (typeof data.endings == 'object') {
                for (var c in data.endings) {
                    data.endings[c] = Object.keys(data.endings[c]);
                }
                localStorage.setItem(prefix + 'endings', JSON.stringify(data.endings));
            }
            if (data.askedUsageTracking) {
                localStorage.setItem(prefix + 'usageTracking', data.usageTracking ? 'yes' : 'no');
            }
            Cookies.remove('save');
        }
    };

    this.load = function() {
        this.convertCookie();
        this.loadOptions();
        this.loadPlayer();
    };

    var defaultWardrobes = {
        'male': [ 'jacket', 't-shirt', 'belt', 'pants', 'boxers', 'gloves', 'socks', 'boots' ],
        'female': [ 'jacket', 'tank top', 'bra', 'belt', 'pants', 'panties', 'stockings', 'shoes' ],
    };
    this.loadPlayer = function() {
        var gender = players[HUMAN_PLAYER].gender;
        var profile = {};
        try {
            profile = JSON.parse(localStorage.getItem(prefix + gender)) || { };
        } catch (ex) {
            console.error('Failed parsing', gender, 'player profile from localStorage');
        }
        $nameField.val(profile.name || '');
        changePlayerSize(profile.size || eSize.MEDIUM);
        selectedChoices = clothingChoices[gender].map(function(item) {
            return (profile.clothing || defaultWardrobes[gender]).indexOf(item.generic) >= 0;
        });
        playerTagSelections = profile.tags || {};
    };
    this.savePlayer = function(){
        localStorage.setItem(prefix + 'gender', players[HUMAN_PLAYER].gender);
    /*  var tags = {};
        for (var key in playerTagSelections) {
            tags[key] = playerTagSelections[key];
        }*/
        var profile = {
            name: $nameField.val(),
            size: players[HUMAN_PLAYER].size,
            tags: playerTagSelections,
            clothing: clothingChoices[players[HUMAN_PLAYER].gender].filter(function(item, ix) {
                return selectedChoices[ix];
            }).map(function(item) { return item.generic; }),
        };
        localStorage.setItem(prefix + players[HUMAN_PLAYER].gender, JSON.stringify(profile));
    };
    this.loadOptions = function(){
        try {
            var options = JSON.parse(localStorage.getItem(prefix + 'options')) || { };
            if ('autoFade' in options && typeof options.autoFade == 'boolean') AUTO_FADE = options.autoFade;
            if ('cardSuggest' in options && typeof options.cardSuggest == 'boolean') CARD_SUGGEST = options.cardSuggest;
            if ('gameDelay' in options && typeof options.gameDelay == 'number') GAME_DELAY = options.gameDelay;
            if ('dealAnimation' in options && typeof options.dealAnimation == 'number') {
                ANIM_TIME = options.dealAnimation;
                ANIM_DELAY = 0.16 * ANIM_TIME;
            }
            if ('autoForfeit' in options
                && (typeof options.autoForfeit == 'number' || options.autoForfeit === null))
                FORFEIT_DELAY = options.autoForfeit;
            if ('autoEnding' in options
                && (typeof options.autoEnding == 'number' || options.autoEnding === null))
                ENDING_DELAY = options.autoEnding;
        } catch (ex) {
            console.error('Failed parsing options from localStorage');
        }
        try {
            var settings = JSON.parse(localStorage.getItem(prefix + 'settings')) || {};
            if ('stamina' in settings) players[HUMAN_PLAYER].stamina = settings.stamina;
            if ('background' in settings) setBackground(settings.background);
        } catch (ex) {
            console.error('Failed parsing settings from localStorage');
        }
        var usageTracking = localStorage.getItem(prefix + 'usageTracking');
        if (usageTracking) {
            USAGE_TRACKING = (usageTracking == 'yes');
        }
        var gender = localStorage.getItem(prefix + 'gender');
        if (gender) {
            players[HUMAN_PLAYER].gender = gender;
        }
    };
    this.saveUsageTracking = function() {
        if (USAGE_TRACKING !== undefined) {
            localStorage.setItem(prefix + 'usageTracking', USAGE_TRACKING ? 'yes' : 'no');
        }
    };
    this.saveOptions = function() {
        var options = {
            autoFade: AUTO_FADE,
            cardSuggest: CARD_SUGGEST,
            gameDelay: GAME_DELAY,
            dealAnimation: ANIM_TIME,
            autoForfeit: FORFEIT_DELAY,
            autoEnding: ENDING_DELAY,
        };
        localStorage.setItem(prefix + 'options', JSON.stringify(options));
    };
    this.saveSettings = function() {
        var settings = { stamina: players[HUMAN_PLAYER].stamina };
        if (selectedBackground != defaultBackground) {
            settings.background = selectedBackground;
        }
        localStorage.setItem(prefix + 'settings', JSON.stringify(settings));
    };
    this.loadEndings = function() {
        if (endings === undefined) {
            try {
                endings = JSON.parse(localStorage.getItem(prefix + 'endings')) || { };
            } catch (e) {
                console.error('Failed parsing endings from localStorage');
                endings = {};
            }
        }
    };
    this.hasEnding = function(character, title) {
        this.loadEndings();
        if (character in endings && Array.isArray(endings[character])) {
            return endings[character].indexOf(title) >= 0;
        }
        return false;
    };
    this.addEnding = function(character, title){
        this.loadEndings();
        if (!(character in endings)) {
            endings[character] = [];
        }
        endings[character].push(title);
        localStorage.setItem(prefix + 'endings', JSON.stringify(endings));
        //Clear table of endings, so they are loaded agin when player visits gallery
        allEndings = [];
        anyEndings = [];
        maleEndings = [];
        femaleEndings = [];
    }
}

var save = new Save();

function saveSettings(){
    save.saveSettings();
};

function saveOptions(){
    save.saveOptions();
}
