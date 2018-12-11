/********************************************************************************
 This file contains the variables and functions that form the gallery screens of
 the game.
 ********************************************************************************/

function GEnding(player, ending){
	this.player = player;
	this.gender = $(ending).attr('gender');

	var previewImage = $(ending).attr('img');
	if (previewImage) {
		previewImage = previewImage.charAt(0) === '/' ? previewImage : player.base_folder + previewImage;
	} else {
		console.log("No preview image found for: "+player.id+" ending: "+$(ending).html());
	}

	this.image = previewImage;
	this.title = $(ending).html();
	this.unlockHint = $(ending).attr('hint');
	this.unlocked = function() { return EPILOGUES_UNLOCKED || save.hasEnding(player.id, this.title); };
}

 /**********************************************************************
  *****                   Gallery Screen UI Elements               *****
  **********************************************************************/

$genderTypeButtons = [$('#gallery-gender-all'), $('#gallery-gender-any'), $('#gallery-gender-male'), $('#gallery-gender-female')];
$galleryEndings = $('#gallery-endings-block').children();
$galleryPrevButton = $('#gallery-prev-page-button');
$galleryNextButton = $('#gallery-next-page-button');
$galleryStartButton = $('#gallery-start-ending-button');
$selectedEndingPreview = $('#selected-ending-previev');
$selectedEndingLabels = [$('#selected-ending-title'), $('#selected-ending-character'), $('#selected-ending-gender')];
$selectedEndingHint = [$('#selected-ending-hint-container'), $('#selected-ending-hint')];

function loadGalleryScreen(){
	screenTransition($titleScreen, $galleryScreen);
	loadGalleryEndings();
	galleryGender(GALLERY_GENDER);
}

function backGalleryScreen(){
	screenTransition($galleryScreen, $titleScreen);
}

/* opponent listing file */
var galleryEndings = [];
var allEndings = [];
var anyEndings = [];
var maleEndings = [];
var femaleEndings = [];
var galleryPage = 0;
var galleryPages = -1;
var epp = 20;
var selectedEnding = -1;
var GALLERY_GENDER = 'all';

function loadGalleryEndings(){
	if(allEndings.length > 0){
		return;
	}
	
	for(var i=0; i<loadedOpponents.length; i++){
		if (loadedOpponents[i].ending) {
			loadedOpponents[i].endings.each(function () {
				var gending = new GEnding(loadedOpponents[i], this);
				
				allEndings.push( gending );
				switch(gending.gender){
					case 'male': maleEndings.push(gending);
					break;
					case 'female': femaleEndings.push(gending);
					break;
					default: anyEndings.push(gending);
					break;
				}
			});
		}
	}
}

function loadEndingThunbnail(element, ending){
	element.removeClass('empty-thumbnail');
	if (ending.unlocked()) {
		element.removeClass('unlocked-thumbnail');
		element.css('background-image','url(\''+ending.image+'\')');
	} else {
		element.css('background-image', '');
		element.addClass('unlocked-thumbnail');
	}
}

function loadThumbnails(){
	var i=0;
	for(; i<epp && epp*galleryPage+i<galleryEndings.length; i++){
		loadEndingThunbnail($galleryEndings.eq(i), galleryEndings[epp*galleryPage+i]);
	}
	for( ; i<epp; i++){
		$galleryEndings.eq(i).removeClass('unlocked-thumbnail');
		$galleryEndings.eq(i).addClass('empty-thumbnail');
		$galleryEndings.eq(i).css('background-image', 'none');
	}
}

function galleryGender(gender){
	GALLERY_GENDER = gender;
	$('.gallery-gender-button').css('opacity', 0.4);
	switch(gender){
		case 'male':
			galleryEndings = maleEndings;
			$('#gallery-gender-male').css('opacity', 1);
			break;
		case 'female':
			galleryEndings = femaleEndings;
			$('#gallery-gender-female').css('opacity', 1);
			break;
		case 'any':
			galleryEndings = anyEndings;
			$('#gallery-gender-any').css('opacity', 1);
			break;
		default:
			galleryEndings = allEndings;
			$('#gallery-gender-all').css('opacity', 1);
			break;
	}
	galleryPages = Math.ceil(galleryEndings.length/parseFloat(epp));
	galleryPage = 0;
	$galleryPrevButton.attr('disabled', true);
	if(galleryPages==1){
		$galleryNextButton.attr('disabled', true);
	}
	else{
		$galleryNextButton.attr('disabled', false);
	}
	loadThumbnails();
}

function galleryNextPage(){
	galleryPage++;
	loadThumbnails();
	$galleryEndings.css('opacity', '');
	$galleryPrevButton.attr('disabled', false);
	if(galleryPage+1==galleryPages){
		$galleryNextButton.attr('disabled', true);
	}
}

function galleryPrevPage(){
	galleryPage--;
	loadThumbnails();
	$galleryEndings.css('opacity', '');
	$galleryNextButton.attr('disabled', false);
	if(galleryPage==0){
		$galleryPrevButton.attr('disabled', true);
	}
}

function selectEnding(i) {
	selectedEnding = epp*galleryPage+i;
	var ending = galleryEndings[selectedEnding];

	if (!ending) {
		return;
	}
	
	if (ending.unlockHint) {
		$selectedEndingHint[0].show();
		$selectedEndingHint[1].html(ending.unlockHint);
	} else {
		$selectedEndingHint[0].hide();
	}

	if (ending.unlocked()) {
		$galleryStartButton.attr('disabled', false);
		chosenEpilogue = ending;
		$selectedEndingLabels[0].html(ending.title);
	} else {
		$galleryStartButton.attr('disabled', true);
		chosenEpilogue = -1;
		$selectedEndingLabels[0].html('');
	}
	
	$galleryEndings.css('opacity', '');
	$galleryEndings.eq(i).css('opacity', 1);
	loadEndingThunbnail($selectedEndingPreview, ending);
	$selectedEndingLabels[1].html(ending.player.label);
	$selectedEndingLabels[2].html(ending.gender);
	switch(ending.gender){
		case 'male':
			$selectedEndingLabels[2].removeClass('female-style');
			$selectedEndingLabels[2].addClass('male-style');
			break;
		case 'female':
			$selectedEndingLabels[2].removeClass('male-style');
			$selectedEndingLabels[2].addClass('female-style');
			break;
		default:
			$selectedEndingLabels[2].removeClass('female-style');
			$selectedEndingLabels[2].removeClass('male-style');
	}
}

function doEpilogueFromGallery(){
	if (!chosenEpilogue) {
		return;
	}
	
	var player = chosenEpilogue.player;
	
	fetchCompressedURL(
		'opponents/' + player.id + "/behaviour.xml",
		/* Success callback.
		 * 'this' is bound to the Opponent object.
		 */
		function(xml) {
			var $xml = $(xml);
			
			var endingElem = null;
			
			$xml.find('epilogue').each(function () {
				if ($(this).find('title').html() === chosenEpilogue.title && $(this).attr('gender') === chosenEpilogue.gender) {
					endingElem = this;
				}
			});
			
			if($nameField.val()){
				players[HUMAN_PLAYER].label = $nameField.val();
			} else {
				switch(chosenEpilogue.gender){
					case "male": players[HUMAN_PLAYER].label = "Mister"; break;
					case "female" : players[HUMAN_PLAYER].label = "Missy"; break;
					default: players[HUMAN_PLAYER].label = (players[HUMAN_PLAYER].gender=="male")?"Mister":"Missy";
				}
			}
			
			// function definition in spniEpilogue.js
			chosenEpilogue = parseEpilogue(player, endingElem);
		
			if (USAGE_TRACKING) {
				var usage_tracking_report = {
					'date': (new Date()).toISOString(),
					'commit': VERSION_COMMIT,
					'type': 'gallery',
					'session': sessionID,
					'userAgent': navigator.userAgent,
					'origin': getReportedOrigin(),
					'chosen': {
						'id': chosenEpilogue.player.id,
						'title': chosenEpilogue.title
					}
				};
		
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
			screenTransition($galleryScreen, $epilogueScreen); //currently transitioning from title screen, because this is for testing
			$epilogueSelectionModal.modal("hide");
		}
	);
}
