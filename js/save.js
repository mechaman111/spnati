
//Class for saving user's progress and preferences

function mergeObjects(a, b){
	if(b===undefined){
		return a;
	}
	else if(a===undefined){
		return b;
	}
	for(var v in b){
		a[v] = b[v];
	}
	return a;
}

function Save(){
	this.data = {
		'gender' : '',
		'name' : '',
		'background' : 1,
		'masturbationTimer' : 20,
		'name' : '',
		'gender' : "male",
		'size' : "medium",
		'autoFade' : 1,
		'cardSuggest' : 2,
		'gameDelay' : 3,
		'dealAnimation' : 3,
		'autoForfeit' : 4,
		'clothing' : [false, false, true, false, true,
			false, true, true, false, true,
			false, false, true, false, true],
		'endings' : {}
	};

	this.saveCookie = function(){
		Cookies.set('save', this.data, {expires: 3652});
	};

	this.loadCookie = function(){
		var cookie = Cookies.get('save');
		if(cookie !== undefined){
			this.data = mergeObjects(this.data, JSON.parse(cookie));
		}
		this.loadSave();
	};

	this.loadSave = function(){
		players[HUMAN_PLAYER].timer = this.data['masturbationTimer'];
		$nameField.val(this.data['name']);
		changePlayerGender(this.data['gender']);
		changePlayerSize(this.data['size']);
		setBackground(this.data['background']);
		selectedChoices = this.data['clothing'].slice(0);
		updateTitleClothing();

		setAutoFade(this.data['autoFade']);
		setCardSuggest(this.data['cardSuggest']);
		setAITurnTime(this.data['gameDelay']);
		setDealSpeed(this.data['dealAnimation']);
		setAutoForfeit(this.data['autoForfeit']);
	};

	this.saveOptions = function(){
		this.data['masturbationTimer'] = parseInt($masturbationTimerBox.val());
		var back = $("body").css('background-image');
		var ind = back.indexOf('background')+10;
		back = back.substr(ind);
		ind = back.indexOf('.');
		back = parseInt(back.substr(0,ind));
		this.data['background'] = back;

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

		this.saveCookie();
	};
	this.savePlayer = function(){
		this.data['name'] = $nameField.val();
		this.data['gender'] = players[HUMAN_PLAYER].gender;
		this.data['size'] = players[HUMAN_PLAYER].size;
		this.data['clothing'] = selectedChoices;
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
