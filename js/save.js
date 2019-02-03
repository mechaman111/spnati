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

function Save(){
	this.data = {
		'background' : null,
		'masturbationTimer' : 20,
		'gender' : "male",
		'autoFade' : 1,
		'cardSuggest' : 2,
		'gameDelay' : 3,
		'dealAnimation' : 3,
		'autoForfeit' : 4,
		'autoEnding' : 4,
		'male' : {
			'name' : '',
			'clothing' : [false, false, true, false, true, false,
				      false, true, true, false, false, true,
				      false, false, true, true, false, true],
			'size' : 'medium',
			'tags' : {},
		},
		'female' : {
			'name' : '',
			'clothing' : [false, false, true, false, true, true,
				      false, true, true, false, false, true,
				      false, false, false, true, false, true],
			'size' : 'medium',
			'tags' : {},
		},
		'endings' : {},
		'askedUsageTracking': false,
		'usageTracking': false,
	};
	
	this.saveCookie = function(){
		Cookies.set('save', this.data, {expires: 3652});
	};

	this.loadCookie = function(){
		var cookie = Cookies.get('save');
		console.log(this.data);
		if(cookie !== undefined){
			this.data = mergeObjects(this.data, JSON.parse(cookie));
		}
		console.log(this.data);
		this.loadOptions();
		this.loadPlayer();
	};

	this.loadPlayer = function() {
		$nameField.val(this.data[players[HUMAN_PLAYER].gender]['name']);
		changePlayerSize(this.data[players[HUMAN_PLAYER].gender]['size']);
		selectedChoices = this.data[players[HUMAN_PLAYER].gender]['clothing'];
		playerTagSelections = this.data[players[HUMAN_PLAYER].gender]['tags'];
		updateTitleGender();
	};
	this.loadOptions = function(){
		USAGE_TRACKING = this.data['usageTracking'];
		players[HUMAN_PLAYER].stamina = this.data['masturbationTimer'] || 20;
		players[HUMAN_PLAYER].gender = this.data['gender'];
		
		if (!this.data['background'] || this.data['background'] == 1) {
			setBackground(defaultBackground);
		} else {
			setBackground(this.data['background']);
		}
		

		setAutoFade(this.data['autoFade']);
		setCardSuggest(this.data['cardSuggest']);
		setAITurnTime(this.data['gameDelay']);
		setDealSpeed(this.data['dealAnimation']);
		setAutoForfeit(this.data['autoForfeit']);
		setAutoEnding(this.data['autoEnding']);
	};

	this.saveOptions = function(){
		this.data['usageTracking'] = USAGE_TRACKING;
		this.data['masturbationTimer'] = players[HUMAN_PLAYER].stamina;
		
		if (selectedBackground != defaultBackground-1) {
			this.data['background'] = selectedBackground+1;
		} else {
			this.data['background'] = null;
		}

		this.saveCookie();
	};
	this.saveIngameOptions = function(){
		this.data['autoFade'] = AUTO_FADE?1:2;
		this.data['cardSuggest'] = CARD_SUGGEST?1:2;
		switch(GAME_DELAY){
			case 0: this.data['gameDelay'] = 1; break;
			case 300: this.data['gameDelay'] = 2; break;
			default:
			case 600: this.data['gameDelay'] = 3; break;
			case 800: this.data['gameDelay'] =  4; break;
			case 1200: this.data['gameDelay'] = 5;
		}
		switch(ANIM_DELAY){
			case 0: this.data['dealAnimation'] = 1; break;
			case 150: this.data['dealAnimation'] = 2; break;
			default:
			case 350: this.data['dealAnimation'] = 3; break;
			case 800: this.data['dealAnimation'] = 4; break;
		}
		if(!AUTO_FORFEIT){
			this.data['autoForfeit'] = 4;
		}
		else{
			switch(FORFEIT_DELAY){
				case 4000: this.data['autoForfeit'] = 1; break;
				default:
				case 7500: this.data['autoForfeit'] = 2; break;
				case 10000: this.data['autoForfeit'] = 3; break;
			}
		}
		if(!AUTO_ENDING){
			this.data['autoEnding'] = 4;
		}
		else{
			switch(ENDING_DELAY){
				case 4000: this.data['autoEnding'] = 1; break;
				default:
				case 7500: this.data['autoEnding'] = 2; break;
				case 10000: this.data['autoEnding'] = 3; break;
			}
		}

		this.saveCookie();
	};
	this.savePlayer = function(){
		this.data['gender'] = players[HUMAN_PLAYER].gender;
		this.data[this.data['gender']]['name'] = $nameField.val();
		this.data[this.data['gender']]['size'] = players[HUMAN_PLAYER].size;
		this.data[this.data['gender']]['clothing'] = selectedChoices.slice();
		var tags = {};
		for (var key in playerTagSelections) {
			tags[key] = playerTagSelections[key];
		}
		this.data[this.data['gender']]['tags'] = tags;
		this.saveCookie();
	};

	this.hasEnding = function(character, title){
		if(this.data.endings[character] !== undefined){
			if(this.data.endings[character][title] !== undefined){
				return this.data.endings[character][title];
			}
		}
		return false;
	}
	this.addEnding = function(character, title){
		if(this.data.endings[character]===undefined){
			this.data.endings[character] = {};
		}
		this.data.endings[character][title] = true;
		this.saveCookie();
		//Clear table of endings, so they are loaded agin when player visits gallery
		allEndings = [];
		anyEndings = [];
		maleEndings = [];
		femaleEndings = [];
	}
}

var save = new Save();

function saveOptions(){
	save.saveOptions();
};

function saveIngameOptions(){
	save.saveIngameOptions();
}
