monika.effects_flag = true;

monika.effects_enabled = function() {
    if(monika.find_slot() === undefined || !monika.effects_flag) {
        return false;
    }
    return true;
}

monika.active_effects = {
    // each entry in these arrays corresponds to an opponent slot,
    // and stores information for undoing an active effect.
    'overflow_text': [false, false, false, false],
    'edited_style': [false, false, false, false],
    'corrupted_dialogue': [null, null, null, null],
    'character_glitch': [null, null, null, null],    // oneshot glitches
    'character_glitching': [null, null, null, null], // repeating glitches
    'modified_body_style': null,
    'image_relocation': [null, null, null, null],
    'label_corruption': [null, null, null, null],
    
    'glitch_masturbation': null,
    'glitch_heavy_masturbation': false,
    
    'round_targeted_glitching': null,   // for the continuous-glitching round effect; set to targeted character's slot
    'round_dialogue_glitching': null,
    'round_edit_glitching': null,       // for the dialogue-editing round effect
    'round_delete_glitching': null,
};

/* Change up styles on the dialogue boxes for a slot so that text overflows out of the box properly. */
monika.setOverflowTextStyles = function(slot) {
    if(!monika.active_effects.modified_body_style) {
        /* If we can, try to prevent overflowing contents from causing scrollbars to appear */
        var original_body_style = $('body').attr('style');
        
        $('body').attr('style', original_body_style+';overflow-x:hidden;');
        
        monika.active_effects.modified_body_style = original_body_style;
    }
    
    $gameBubbles[slot-1].attr('style', 'display: table; z-index: 1;');
    $('game-bubble-'+slot.toString()+'.dialogue-bubble').attr('style', 'width:90%; overflow:visible');
    $gameDialogues[slot-1].attr('style', 'white-space: nowrap; position: absolute; left: 5%; top: 50%; transform: translate(0, -50%);');

    monika.active_effects.overflow_text[slot-1] = true;    
}

monika.resetBodyStyle = function() {
    if(monika.active_effects.modified_body_style) {
        $('body').attr('style', monika.active_effects.modified_body_style);
        monika.active_effects.modified_body_style = null;
    }
}

/* Reset any modified styles on a given dialogue box */
monika.resetDialogueBoxStyles = function(slot) {
    if(monika.active_effects.overflow_text[slot-1]) {
        $gameBubbles[slot-1].attr('style', 'display: table;');
        $('game-bubble-'+slot.toString()+'.dialogue-bubble').removeAttr('style');
        $gameDialogues[slot-1].removeAttr('style');
    }

    monika.active_effects.overflow_text[slot-1] = false;
    
    // check to see if any more overflow text effects are active
    var n_active_overflow = 0;
    for(var i=0;i<monika.active_effects.overflow_text.length;i++) {
        if(monika.active_effects.overflow_text[i]) {
            n_active_overflow++;
        }
    }
    
    if(n_active_overflow <= 0) {
        monika.resetBodyStyle();
    }
}

monika.applyEditedDialogueStyle = function(slot) {
    $gameDialogues[slot-1].addClass('monika-edited-dialogue');
    monika.active_effects.edited_style[slot-1] = true;
}

monika.removeEditedDialogueStyle = function(slot) {
    $gameDialogues[slot-1].removeClass('monika-edited-dialogue');
    monika.active_effects.edited_style[slot-1] = false;
}

monika.corruptCharacterLabel = function(slot) {
    var original = players[slot].label;
    
    players[slot].label = monika.generate_glitch_text(original.length + getRandomNumber(0, 5));
    monika.active_effects.label_corruption[slot-1] = original;
}

monika.undoLabelCorruption = function(slot) {
    if(monika.active_effects.label_corruption[slot-1]) {
        players[slot].label = monika.active_effects.label_corruption[slot-1];
    }
    
    monika.active_effects.label_corruption[slot-1] = null;
}

/* Corrupts a character's dialogue by zalgo-texting it. */
monika.zalgoCorruptDialogue = function(slot) {
    var original = $gameDialogues[slot-1].html();
    
    $gameDialogues[slot-1].html(monika.zalgo_text(original));
    
    monika.active_effects.corrupted_dialogue[slot-1] = original;
}

/* Corrupts a character's dialogue by picking a random letter and repeating it (cutting off the rest of the text).
 * For example: "Hi, what's your name?" -> "Hi, what's youuuuuuuuuuuu--"
 * We also apply the overflow text style for maximum effect.
 */
monika.repeatCorruptDialogue = function(slot) {
    var original = $gameDialogues[slot-1].html();
    var text = $gameDialogues[slot-1].text();
    
    /* Find all words in the dialogue. */
    var re = /\w+(\w)/gi;
    var all_words = [];
    var current_match = null;
    while((current_match = re.exec(text)) != null) {
        all_words.push(current_match);
    }
    
    // don't corrupt dialogue that's very short.
    if(all_words.length >= 7) {
        var min = Math.floor(all_words.length * 0.4);
        var max = Math.floor(all_words.length * 0.6);
        var selected = all_words[getRandomNumber(min, max+1)];
        
        /* Grab everything up to (but excluding) the targeted word */
        var modified_text = text.substr(0, selected.index);
        var corrupted_word = selected[0].substr(0, selected[0].length-1) + selected[1].repeat(getRandomNumber(10, 25));
        
        /* Put the corrupted word in the edited style: */
        modified_text = modified_text + '<span class="monika-edited-dialogue">' + corrupted_word + '</span>';
        
        $gameDialogues[slot-1].html(modified_text);
        
        monika.active_effects.corrupted_dialogue[slot-1] = original;
        
        monika.setOverflowTextStyles(slot);
    }
}

monika.undoDialogueCorruption = function(slot) {
    if(monika.active_effects.corrupted_dialogue[slot-1]) {
        $gameDialogues[slot-1].html(monika.active_effects.corrupted_dialogue[slot-1]);
        monika.active_effects.corrupted_dialogue[slot-1] = null;
    }
    
    if(monika.active_effects.overflow_text[slot-1]) {
        monika.resetDialogueBoxStyles(slot);
    }
    
    if(monika.active_effects.edited_style[slot-1]) {
        monika.removeEditedDialogueStyle(slot);
    }
}

monika.slot_to_css_class = function(slot) {
    switch (slot) {
        case 0: return "veryFarLeft";
        case 1: return "farLeft";
        case 2: return "almostLeft";
        case 3: return "almostRight";
        case 4: return "farRight";
        case 5: return "veryFarRight";
        default: return "centered";
    }
    
    return "centered";
}

/* dest_pos is either a slot number (0-5) or null/undefined for relocation to the center of the screen */
monika.moveCharacterOverUI = function(slot, dest_pos) {
    var dest_css = monika.slot_to_css_class(dest_pos);
    var classes_to_add = 'monika-over-ui '+dest_css;
    
    $gameImages[slot-1].appendTo('#game-screen > .screen').addClass(classes_to_add);
    monika.active_effects.image_relocation[slot-1] = classes_to_add;
}

monika.undoCharacterRelocation = function(slot) {
    if(monika.active_effects.image_relocation[slot-1]) {
        var dest_css = monika.slot_to_css_class(slot);
        var classes_to_remove = monika.active_effects.image_relocation[slot-1];
        
        $gameImages[slot-1].appendTo('#game-screen .image-row .'+dest_css).removeClass(classes_to_remove);
        monika.active_effects.image_relocation[slot-1] = null;
    }
}

monika.cleanUpEffects = function() {
    for(var i=0;i<4;i++) {
        if(monika.active_effects.corrupted_dialogue[i]) monika.undoDialogueCorruption(i+1);
        if(monika.active_effects.overflow_text[i]) monika.resetDialogueBoxStyles(i+1);
        if(monika.active_effects.edited_style[i]) monika.removeEditedDialogueStyle(i+1);
        if(monika.active_effects.character_glitch[i]) monika.undoCharacterGlitch(i+1);
        if(monika.active_effects.character_glitching[i]) monika.stopContinuousGlitching(i+1);
        if(monika.active_effects.image_relocation[i]) monika.undoCharacterRelocation(i+1);
    }
}

monika.random_character_replacement = function(allowed_chars, in_str, replace_prob) {
    var out_str = "";
    for(var i=0;i<in_str.length;i++) {
        if(Math.random() < replace_prob) {
            out_str += allowed_chars[getRandomNumber(0, allowed_chars.length)];
        } else {
            out_str += in_str.charAt(i);
        }
    }
    
    return out_str;
}

monika.generate_character_sequence = function(allowed_chars, length) {
    var ret = '';
    for(var i=0;i<length;i++) {
        ret += allowed_chars[getRandomNumber(0, allowed_chars.length)];
    }
    
    return ret;
}

monika.inline_glitch_chars = [
    '¡', '¢', '£', '¤', '¥', '¦', '§', '¨', '©', 'ª',
    '«', '¬', '®', '¯', '°', '±', '²', '³', '´', 'µ',
    '¶', '·', '¸', '¹', 'º', '»', '¼', '½', '¾', '¿',
    'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç', 'È', 'É', 'Ê', 'Ë',
    'Ì', 'Í', 'Î', 'Ï', 'Ð', 'Ñ', 'Ò', 'Ó', 'Ô', 'Õ',
    'Ö', '×', 'Ø', 'Ù', 'Ú', 'Û', 'Ü', 'Ý', 'Þ', 'ß', 
    'â', 'ã', 'ä', 'å', 'æ', 'ç', 'è', 'é', 'ê', 'ë',
    'ì', 'í', 'î', 'ï', 'ð', 'ñ', 'ò', 'ó', 'ô', 'õ',
    'ö', '÷', 'ø', 'ù', 'ú', 'û', 'ü', 'ý', 'þ', 'ÿ', 
    'Ă', 'ă', 'Ą', 'ą', 'Ć', 'ć', 'Ĉ', 'ĉ', 'Ċ', 'ċ',
    'Č', 'č', 'Ď', 'ď', 'Đ', 'đ', 'Ē', 'ē', 'Ĕ', 'ĕ',
    'Ė', 'ė', 'Ę', 'ę', 'Ě', 'ě', 'Ĝ', 'ĝ', 'Ğ', 'ğ', 
    'Ģ', 'ģ', 'Ĥ', 'ĥ', 'Ħ', 'ħ', 'Ĩ', 'ĩ', 'Ī', 'ī',
    'Ĭ', 'ĭ', 'Į', 'į', 'İ', 'ı', 'Ĳ', 'ĳ', 'Ĵ', 'ĵ',
    'Ķ', 'ķ', 'ĸ', 'Ĺ', 'ĺ', 'Ļ', 'ļ', 'Ľ', 'ľ', 'Ŀ', 
    'ł', 'Ń', 'ń', 'Ņ', 'ņ', 'Ň', 'ň', 'ŉ', 'Ŋ', 'ŋ',
    'Ō', 'ō', 'Ŏ', 'ŏ', 'Ő', 'ő', 'Œ', 'œ', 'Ŕ', 'ŕ',
    'Ŗ', 'ŗ', 'Ř', 'ř', 'Ś', 'ś', 'Ŝ', 'ŝ', 'Ş', 'ş', 
    'Ţ', 'ţ', 'Ť', 'ť', 'Ŧ', 'ŧ', 'Ũ', 'ũ', 'Ū', 'ū',
    'Ŭ', 'ŭ', 'Ů', 'ů', 'Ű', 'ű', 'Ų', 'ų', 'Ŵ', 'ŵ',
    'Ŷ', 'ŷ', 'Ÿ', 'Ź', 'ź', 'Ż', 'ż', 'Ž', 'ž', 'À',
    'Á', 'à', 'á', 'Ā', 'ā', 'Ġ', 'ġ', 'ŀ', 'Ł', 'Š', 'š',
]

monika.generate_glitch_text = function(length) {
    return monika.generate_character_sequence(monika.inline_glitch_chars, length);
}


// taken from tchouky's zalgo text site: http://www.eeemo.net/

//those go UP
monika.zalgo_up = [
	'\u030d', /*     ̍     */		'\u030e', /*     ̎     */		'\u0304', /*     ̄     */		'\u0305', /*     ̅     */
	'\u033f', /*     ̿     */		'\u0311', /*     ̑     */		'\u0306', /*     ̆     */		'\u0310', /*     ̐     */
	'\u0352', /*     ͒     */		'\u0357', /*     ͗     */		'\u0351', /*     ͑     */		'\u0307', /*     ̇     */
	'\u0308', /*     ̈     */		'\u030a', /*     ̊     */		'\u0342', /*     ͂     */		'\u0343', /*     ̓     */
	'\u0344', /*     ̈́     */		'\u034a', /*     ͊     */		'\u034b', /*     ͋     */		'\u034c', /*     ͌     */
	'\u0303', /*     ̃     */		'\u0302', /*     ̂     */		'\u030c', /*     ̌     */		'\u0350', /*     ͐     */
	'\u0300', /*     ̀     */		'\u0301', /*     ́     */		'\u030b', /*     ̋     */		'\u030f', /*     ̏     */
	'\u0312', /*     ̒     */		'\u0313', /*     ̓     */		'\u0314', /*     ̔     */		'\u033d', /*     ̽     */
	'\u0309', /*     ̉     */		'\u0363', /*     ͣ     */		'\u0364', /*     ͤ     */		'\u0365', /*     ͥ     */
	'\u0366', /*     ͦ     */		'\u0367', /*     ͧ     */		'\u0368', /*     ͨ     */		'\u0369', /*     ͩ     */
	'\u036a', /*     ͪ     */		'\u036b', /*     ͫ     */		'\u036c', /*     ͬ     */		'\u036d', /*     ͭ     */
	'\u036e', /*     ͮ     */		'\u036f', /*     ͯ     */		'\u033e', /*     ̾     */		'\u035b', /*     ͛     */
	'\u0346', /*     ͆     */		'\u031a' /*     ̚     */
];

//those go DOWN
monika.zalgo_down = [
	'\u0316', /*     ̖     */		'\u0317', /*     ̗     */		'\u0318', /*     ̘     */		'\u0319', /*     ̙     */
	'\u031c', /*     ̜     */		'\u031d', /*     ̝     */		'\u031e', /*     ̞     */		'\u031f', /*     ̟     */
	'\u0320', /*     ̠     */		'\u0324', /*     ̤     */		'\u0325', /*     ̥     */		'\u0326', /*     ̦     */
	'\u0329', /*     ̩     */		'\u032a', /*     ̪     */		'\u032b', /*     ̫     */		'\u032c', /*     ̬     */
	'\u032d', /*     ̭     */		'\u032e', /*     ̮     */		'\u032f', /*     ̯     */		'\u0330', /*     ̰     */
	'\u0331', /*     ̱     */		'\u0332', /*     ̲     */		'\u0333', /*     ̳     */		'\u0339', /*     ̹     */
	'\u033a', /*     ̺     */		'\u033b', /*     ̻     */		'\u033c', /*     ̼     */		'\u0345', /*     ͅ     */
	'\u0347', /*     ͇     */		'\u0348', /*     ͈     */		'\u0349', /*     ͉     */		'\u034d', /*     ͍     */
	'\u034e', /*     ͎     */		'\u0353', /*     ͓     */		'\u0354', /*     ͔     */		'\u0355', /*     ͕     */
	'\u0356', /*     ͖     */		'\u0359', /*     ͙     */		'\u035a', /*     ͚     */		'\u0323' /*     ̣     */
];

//those always stay in the middle
monika.zalgo_mid = [
	'\u0315', /*     ̕     */		'\u031b', /*     ̛     */		'\u0340', /*     ̀     */		'\u0341', /*     ́     */
	'\u0358', /*     ͘     */		'\u0321', /*     ̡     */		'\u0322', /*     ̢     */		'\u0327', /*     ̧     */
	'\u0328', /*     ̨     */		'\u0334', /*     ̴     */		'\u0335', /*     ̵     */		'\u0336', /*     ̶     */
	'\u034f', /*     ͏     */		'\u035c', /*     ͜     */		'\u035d', /*     ͝     */		'\u035e', /*     ͞     */
	'\u035f', /*     ͟     */		'\u0360', /*     ͠     */		'\u0362', /*     ͢     */		'\u0338', /*     ̸     */
	'\u0337', /*     ̷     */		'\u0361', /*     ͡     */		'\u0489' /*     ҉_     */		
];

monika.zalgo_text = function(in_str) {
    var out_str = '';
    for(var i=0;i<in_str.length;i++) {
        out_str += in_str.charAt(i);
        
        var n_up = getRandomNumber(0, 3);
        var n_mid = getRandomNumber(0, 2);
        var n_down = getRandomNumber(0, 3);
        
        out_str += monika.generate_character_sequence(monika.zalgo_up, n_up);
        out_str += monika.generate_character_sequence(monika.zalgo_mid, n_mid);
        out_str += monika.generate_character_sequence(monika.zalgo_down, n_down);
    }
    
    return out_str;
}
