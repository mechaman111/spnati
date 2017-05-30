
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
		console.log(this.data.endings);
		this.loadSave();
	};

	this.loadSave = function(){
		players[HUMAN_PLAYER].timer = this.data['masturbationTimer'];
		$nameField.val(this.data['name']);
		changePlayerGender(this.data['gender']);
		changePlayerSize(this.data['size']);
		setBackground(this.data['background']);
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
	this.savePlayer = function(){
		this.data['name'] = $nameField.val();
		this.data['gender'] = players[HUMAN_PLAYER].gender;
		this.data['size'] = players[HUMAN_PLAYER].size;
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
	}
}

var save = new Save();

$('#options-modal-button').click(function(){
	save.saveOptions();
});
