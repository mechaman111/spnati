# -*- coding: utf-8 -*-

import sys
import os
import imp
if sys.version_info[0] == 2:
	imp.reload(sys)
	sys.setdefaultencoding('utf8')
import xml.etree.ElementTree as ET
import xml.dom.minidom as minidom
import datetime
import re
from collections import OrderedDict
from ordered_xml import OrderedXMLElement as Element, Comment
try:
     # Python 2.6-2.7
     from HTMLParser import HTMLParser
except ImportError:
     # Python 3
     from html.parser import HTMLParser

unescapeHTML = HTMLParser().unescape

#tags that relate to ending sequences
ending_tag = "ending" #name for the ending
ending_gender_tag = "ending_gender" #player gender the ending is shown to
ending_preview_tag = "gallery_image" # image to use for the preview in the gallery
ending_conditions_tag = "ending_conditions" # All other conditions
ending_hint_tag = "hint" #unlock hint
screen_tag = "screen"
scene_tag = "scene"
text_tag = "text"
directive_tag = "directive"
keyframe_tag = "keyframe"
x_tag = "x"
y_tag = "y"
width_tag = "width"
arrow_tag = "arrow"
ending_tags = [ending_tag, ending_gender_tag, ending_preview_tag, screen_tag, text_tag, x_tag, y_tag, width_tag, arrow_tag, ending_conditions_tag, ending_hint_tag, scene_tag, directive_tag, keyframe_tag]
screen_tags = [screen_tag, text_tag, x_tag, y_tag, width_tag, arrow_tag]
scene_tags = [scene_tag, directive_tag, keyframe_tag, text_tag]
scene_attributes = ['name', 'background', 'color', 'x', 'y',  'width', 'height', 'zoom', 'overlay', 'overlay-alpha']
directive_attributes = ['type', 'id', 'time', 'src', 'x', 'y', 'width', 'height', 'scale', 'scalex', 'scaley', 'pivotx', 'pivoty', 'color', 'alpha', 'rotation', 'zoom', 'arrow', 'delay', 'loop', 'ease', 'interpolation']
directive_types = ['sprite', 'text', 'clear', 'clear-all', 'move', 'camera', 'fade', 'stop', 'wait', 'pause']
ending_condition_types = ['alsoPlaying', 'playerStartingLayers',
			  'markers', 'not-markers', 'any-markers',
			  'alsoplaying-markers', 'alsoplaying-not-markers', 'alsoplaying-any-markers']
situations = []

#sets of possible targets for lines
one_word_targets = ["target", "filter", "hidden"]
multi_word_targets = ["targetStage", "targetLayers", "targetStatus", "alsoPlaying", "alsoPlayingStage", "alsoPlayingHand", "oppHand", "hasHand", "totalMales", "totalFemales", "targetTimeInStage", "alsoPlayingTimeInStage", "timeInStage", "consecutiveLosses", "totalAlive", "totalExposed", "totalNaked", "totalMasturbating", "totalFinished", "totalRounds", "saidMarker", "notSaidMarker", "alsoPlayingSaidMarker", "alsoPlayingNotSaidMarker", "alsoPlayingSayingMarker", "targetSaidMarker", "targetNotSaidMarker", "targetSayingMarker", "priority"] #these will need to be re-capitalised when writing the xml
lower_multi_targets = [t.lower() for t in multi_word_targets]
all_targets = one_word_targets + lower_multi_targets

def capitalizeDialogue(s):
	# Convert the first character of the string, or of a variable that starts the string, to uppercase
	return re.sub('(?<=^~)[a-z](?=\w+~)|^\w', lambda m: m.group(0).upper(), s)

def get_situations_from_xml():
	filename = os.path.join(os.path.dirname(sys.argv[0]), 'dialogue_tags.xml')
	dialogue_tags = ET.parse(filename)
	for el in dialogue_tags.iterfind('./triggers/trigger'):
		if el.attrib['tag'] == '-':
			continue
		situations.append({
			'key': el.get('tag'),
			'start': int(el.get('start', '0')),
			'end': int(el.get('end', '10')),
			'group': int(el.get('group', '0')),
			'order': int(el.get('order', '0')),
			'image': el.findtext('defaultImage'),
			'text': el.findtext('defaultText'),
			'optional': el.get('optional') == 'true',
		})
	situations.sort(key=lambda x: (x['group'], x['order']))

def merge_intervals(items):
	ret = []
	i = 0
	while i < len(items):
		j = i + 1
		while j < len(items) and items[j] == items[j - 1] + 1:
			j = j + 1
		if j > i + 1:
			ret.append('%d-%d' % (items[i], items[j - 1]))
		else:
			ret.append(str(items[i]))
		i = j
	return ' '.join(ret)

#get a set of cases from the dictionaries. First try stage-specific from the character's data, then general entries from the character's data, then stage-specific from the default data, then general cases from the default data.
def get_cases(player_dictionary, situation):
	image_formats = ["png", "jpg", "jpeg", "gif", "gifv"] #image file format extensions
	out_list = []
	key = situation['key']

	result_list = list()
	clothes_count = len(player_dictionary["clothes"])
	def adjust_stage(stage):
		if stage > 4:
			return stage - 8 + clothes_count
		else:
			return stage

	first_stage = adjust_stage(situation['start'])
	last_stage = adjust_stage(situation['end'])

	def is_generic_line(line_data):
		for target_type in all_targets + ['tests', 'conditions']:
			if target_type in line_data:
				return False
		return True

	def have_generic_line(lines):
		for line_data in lines:
			if is_generic_line(line_data):
				return True
		return False
	
	need_default = False
	stages_left = list(range(first_stage, last_stage + 1))

	for stage in range(first_stage, last_stage + 1):
		full_key = "%d-%s" % (stage, key)

		#check character's data
		if full_key in player_dictionary:
			result_list += player_dictionary[full_key]

			#check if whe have a line that doesn't have any targets or filters
			#because we need at least one line that doesn't have one
			if have_generic_line(player_dictionary[full_key]):
				stages_left.remove(stage)
			else:
				need_default = True
		else:
			need_default = True

	if key in player_dictionary:
		result_list += player_dictionary[key]
		if have_generic_line(player_dictionary[key]):
			need_default = False
	
	#use the default data if there are no player-specific lines available
	if need_default and not situation['optional']:
		result_list.append({'key': situation['key'], 'text': situation['text'], 'image': situation['image']})
		print("Warning: Using default line for key %s, stage %d" % (key, stage))
	
	#debug
	#convert image formats
	#print "result list", result_list #for debug purposes
	for i, line_data in enumerate(result_list):
		line_data = line_data.copy() #use a copy of the line_data entry
		#because if we copy it then changing the stage number for images (below) for lines that don't have stage numbers
		#will use the first stage number that doesn't have a stage-specific version for all the stages where the generic line is used

		image = line_data["image"]
		text = line_data["text"]
		if len(image) <= 0:
			#if the character entry doesn't include an image, use default image
			image = situation["image"]
		
		#if the image name doesn't include a stage, prepend the current stage
		if not image[0].isdigit():
			# Try not to use # as placeholder unnecessarily
			if "stage" in line_data:
				image = "%s-%s" % (line_data["stage"], image)
			elif first_stage == last_stage:
				image = "%d-%s" % (first_stage, image)
			else:
				image = "#-%s" % (image)

		if not "stage" in line_data:
			if is_generic_line(line_data):
				line_data["stage"] = merge_intervals(stages_left)
				if line_data["stage"] == "":
					print("Warning: Unused generic %s line" % key)
			else:
				line_data["stage"] = merge_intervals(list(range(first_stage, last_stage + 1)))

		#if no file extension, assume .png
		if "." not in image:
			image += ".png"
		else:
			name, extention = image.rsplit(".", 1)
			if extention not in image_formats:
				#if the image name doesn't end with a known image format, assume it's a .png file that just happens to have a . in its name
				image += ".png"
		
		line_data["image"] = image
		
		#out_list.append( (image+".png", text) ) don't use this
		out_list.append( line_data ) #because we switched to using dictionaries
	
	return out_list

#add a single element (initially used so I can add a tag named "tag")
#now it also handles targets, which are optional
#now it takes a series of lines for a particular stage, and adds all the <case> and <state> elements for the given list of lines
def create_case_xml(base_element, lines):
	#one_word_targets = ["target", "filter"]
	#targets = one_word_targets + ["targetstage"]
	
	#step 1: sort the lines by case (situation, and any targets)
	#this means that once the case changes, we know that the script won't see that case again
	#give them a key to define an order
	for line_data in lines:
		if "stage" in line_data:
			sort_key = line_data["stage"]
		else:
			sort_key = '-'

		if "conditions" in line_data:
			for condition in line_data["conditions"]:
				sort_key += "," + "count-" + condition[0]
		if "tests" in line_data:
			for test in line_data["tests"]:
				sort_key += "," + "test:" + test[0]
		for target_type in all_targets:
			if target_type in line_data:
				sort_key += "," + target_type + ":" +line_data[target_type]
		line_data["sort_key"] = sort_key

	#now do the sorting
	lines.sort(key=lambda l: l["sort_key"])
	
	#step 2: iterate through the list of lines
	current_sort = None #which case combination we're currently looking at. initially nothing
	case_xml_element = None #current XML element, add states to this

	possible_statuses = [ 'alive', 'lost_some', 'mostly_clothed', 'decent', 'exposed',
			      'chest_visible', 'crotch_visible', 'topless', 'bottomless',
			      'naked', 'lost_all', 'masturbating', 'finished' ]
	
	for line_data in lines:
		if line_data["sort_key"] != current_sort:
			#this is a new key
			current_sort = line_data["sort_key"]
			
			#make a new <case> element in the xml
			tag_list = OrderedDict()
			if "stage" in line_data:
				tag_list["stage"] = str(line_data["stage"])
			
			for target_type in one_word_targets:
				if target_type in line_data:
					tag_list[target_type] = line_data[target_type]
			
			#need to re-capitalise multi-word target names
			for ind, lower_case_target in enumerate(lower_multi_targets):
				if lower_case_target in line_data:
					capital_word = multi_word_targets[ind]
					tag_list[capital_word] = line_data[lower_case_target]
	
			case_xml_element = base_element.subElement("case", None, tag_list) #create the <case> element in the xml

			if "conditions" in line_data:
				for condition in line_data["conditions"]:
					conddict = OrderedDict(count=condition[1])
					condparts = condition[0].split('&') if condition[0] != '' else []
					for cond in condparts:
						if cond in [ 'male', 'female' ]:
							conddict['gender'] = cond
						elif cond in possible_statuses or (cond[0:4] == 'not_' and cond[4:] in possible_statuses):
							conddict['status'] = cond
						else:
							conddict['filter'] = cond

					case_xml_element.subElement("condition", None, conddict)

			if "tests" in line_data:
				for test in line_data["tests"]:
					case_xml_element.subElement("test", None, [('expr', test[0]), ('value', test[1])])


		#now add the individual line
		#remember that this happens regardless of if the <case> is new
		attrib = OrderedDict(img=line_data["image"])
		if "marker" in line_data:
			attrib["marker"] = line_data["marker"]
		if "weight" in line_data:
			attrib["weight"] = line_data["weight"]
		if "direction" in line_data:
			attrib["direction"] = line_data["direction"]
		if "location" in line_data:
			attrib["location"] = line_data["location"]
		case_xml_element.subElement("state", line_data["text"], attrib) #add the image and text

#write the xml file to the specified filename
def write_xml(data, filename):
	#f = open(filename)
	o = Element("opponent")
	mydate = datetime.datetime.now()
	o.append(Comment("This file was machine generated by make_xml.py version 2.2 in " + mydate.strftime("%B") + " " + mydate.strftime("%Y") +". Please discontinue use of this tool, as it has been replaced by the Character Editor."))
	o.subElement("first", data["first"])
	o.subElement("last", data["last"])

	#label
	for stage in data["label"]:
		if stage == 0:
			o.subElement("label", data["label"][stage])
		else:
			o.subElement("label", data["label"][stage], {stage: stage})

	for tag in ("gender", "size", "timer"):
		o.subElement(tag, data[tag])

	#intelligence
	for stage in data["intelligence"]:
		if stage == 0:
			o.subElement("intelligence", data["intelligence"][stage])
		else:
			o.subElement("intelligence", data["intelligence"][stage], {'stage': stage})

	#tags
	tags_elem = o.subElement("tags", blank_before=True, blank_after=True)
	character_tags = data["character_tags"]
	for tag in character_tags:
		tags_elem.subElement("tag", tag)

	#start image
	start = o.subElement("start", blank_before=True, blank_after=True)
	start_data = data["start"] if "start" in data else ["0-calm,So we'll be playing strip poker... I hope we have fun."]
	start_count = len(start_data)
	for i in range(0, start_count):
		start_image, start_text = start_data[i].split(",", 1)
		start.subElement("state", start_text, {'img': start_image+".png"})
	
	#wardrobe
	clth = o.subElement("wardrobe", blank_after=True)
	clothes = data["clothes"]
	clothes_count = len(clothes)
	for i in range(clothes_count - 1, -1, -1):
		pname, lname, tp, pos, num = (clothes[i] + ",").split(",")[:5]
		generic = None
		if pname[0].lower() == pname[0]:
			generic = lname
			lname = pname
		clothesattr = OrderedDict([("name", lname), ("generic", generic), ("position", pos), ("type", tp)])
		if num=="plural":
			clothesattr["plural"] = "true"
		clth.subElement("clothing", None, clothesattr)
	
	#behaviour
	bh = o.subElement("behaviour", blank_after=True)
	for situation in situations:
		contents = get_cases(data, situation)
		if len(contents):
			trig = bh.subElement("trigger", None, {'id': situation['key']})
			#add the target values, if any
			create_case_xml(trig, contents) #add the case element to the XML tree

	#endings
	if "endings" in data:
		#for each ending
		for ending in data["endings"]:
			ending_xml = o.subElement("epilogue", None, {'gender': ending["gender"]}, blank_after=True)
			
			if 'img' in ending:
				ending_xml.set('img', ending['img'])
			for cond_type in ending_condition_types:
				if cond_type in ending:
					ending_xml.set(cond_type, ending[cond_type])
			
			ending_xml.subElement("title", ending["title"])
			
			#for each screen in an ending
			for screen in ending["screens"]:
				screen_xml = ending_xml.subElement("screen", None, {'img': screen["image"]}, blank_after=True)
				
				#for each text box on a screen
				for text_box in screen["text_boxes"]:
					text_box_xml = screen_xml.subElement("text", blank_after=True)
					text_box_xml.subElement(x_tag, text_box[x_tag])
					text_box_xml.subElement(y_tag, text_box[y_tag])
					#width and arrow are optional
					if width_tag in text_box:
						text_box_xml.subElement(width_tag, text_box[width_tag])
					if arrow_tag in text_box:
						text_box_xml.subElement(arrow_tag, text_box[arrow_tag])
					text_box_xml.subElement("content", capitalizeDialogue(text_box[text_tag]))

			if not 'scenes' in ending:
				continue
			for scene in ending["scenes"]:
				scene_xml = ending_xml.subElement("scene", None, None, blank_after=True)
				for cond_type in scene_attributes:
					if cond_type in scene:
						scene_xml.set(cond_type, scene[cond_type])

				for directive in scene["directives"]:
					directive_text = None
					if text_tag in directive:
						directive_text = directive[text_tag]
					directive_xml = scene_xml.subElement("directive", directive_text)
					for directive_attr in directive_attributes:
						if directive_attr in directive:
							directive_xml.set(directive_attr, directive[directive_attr])

					for keyframe in directive["keyframes"]:
						keyframe_xml = directive_xml.subElement("keyframe")
						for keyframe_attr in directive_attributes:
							if keyframe_attr in keyframe:
								keyframe_xml.set(keyframe_attr, keyframe[keyframe_attr])
	
	#done
	
	open(filename, 'w').write(o.serialize())


#add an ending to the 
def add_ending(ending, d):
	ending = dict(ending)

	if len(list(ending.keys())) <= 0:
		#this is an empty ending, so don't add anything
		return
	
	#check for required values
	if "title" not in ending:
		print("Error - ending \"%s\" does not have a title." % (str(ending)))
		return
		
	if "gender" not in ending:
		print("Error - ending \"%s\" does not have a gender specified." % (str(ending)))
		return
		
	if "screens" not in ending:
		print("Error - ending \"%s\" does not have any screens." % (str(ending)))
		return
	
	#either get the endings data from the dictionary, or make a new endings variable and add that to the dictionary
	endings = None
	if "endings" in d:
		endings = d["endings"]
	else:
		endings = list()
		d["endings"] = endings
		
	endings.append(ending)
	
#scene-based endings
def handle_scene_ending_string(key, content, ending, d):
	#get the scenes variable
	scenes = None
	if "scenes" in ending:
		scenes = ending["scenes"]
	else:
		#or make one, if it doesn't already exist
		scenes = list()
		ending["scenes"] = scenes
		
	#get the current screen
	scene = None
	if len(scenes) >= 1:
		scene = scenes[-1]

	#make a new scene
	if key == scene_tag:
		scene = dict()
		scenes.append(scene)
		scene["directives"] = list()
		parts = content.split(',')
		for c in parts:
			try:
				cond_type, cond_value = c.rsplit(':', 1)
				cond_type = cond_type.strip()
				cond_value = cond_value.strip()
				if cond_type in scene_attributes:
					if cond_value != '':
						scene[cond_type] = cond_value
					else:
						print("Scene attribute without value for \"%s\": \"%s\". Skipping." % (ending['title'], cond_type))
				else:
					print("Unknown scene attribute %s" % cond_type)

			except ValueError:
				print("Scene attribute with empty value for \"%s\": \"%s\" Skipping." % (ending['title'], c))
		return
	
	#make sure we have a screen ready, because the other tags are specific to a screen
	if scene is None:
		print("Error - using tag \"%s\" with value \"%s\", without a scene variable - use the \"%s\" tag first to put this information on that scene." % (key, content, scene_tag))
		return

	#new directive
	if key == directive_tag:
		directive = dict()
		directive["keyframes"] = list()
		scene["directives"].append(directive)
		parts = content.split(',')
		for c in parts:
			try:
				cond_type, cond_value = c.rsplit(':', 1)
				cond_type = cond_type.strip()
				cond_value = cond_value.strip()
				if cond_type in directive_attributes:
					if cond_value != '':
						if cond_type == "type" and not cond_value in directive_types:
							print("Unknown directive type %s. Skipping." % cond_value)
						else:
							directive[cond_type] = cond_value
					else:
						print("Directive attribute without value for \"%s\": \"%s\". Skipping." % (directive['type'], cond_type))
				else:
					print("Unknown directive attribute %s" % cond_type)

			except ValueError:
				print("Directive attribute with empty value for \"%s\": \"%s\" Skipping." % (directive['type'], c))
		return

	#new keyframe
	if key == keyframe_tag:
		directive = None
		if len(scene["directives"]) >= 1:
			directive = scene["directives"][-1]

		if directive is None:
			print("Error - using tag \"%s\" with value \"%s\", without a directive variable - use the \"%s\" tag first to put this information on that directive." % (key, content, directive_tag))
			return

		keyframe = dict()
		directive["keyframes"].append(keyframe)
		parts = content.split(',')
		for c in parts:
			try:
				cond_type, cond_value = c.rsplit(':', 1)
				cond_type = cond_type.strip()
				cond_value = cond_value.strip()
				if cond_type in directive_attributes:
					if cond_value != '':
						keyframe[cond_type] = cond_value
					else:
						print("Keyframe attribute without value for \"%s\": \"%s\". Skipping." % (directive['type'], cond_type))
				else:
					print("Unknown directive attribute %s" % cond_type)

			except ValueError:
				print("Keyframe attribute with empty value for \"%s\": \"%s\" Skipping." % (directive['type'], c))
		return

	#text
	if key == text_tag:
		directive = None
		if len(scene["directives"]) >= 1:
			directive = scene["directives"][-1]
		if directive is None:
			print("Error - using tag \"%s\" with value \"%s\", without a directive variable - use the \"%s\" tag first to put this information on that directive." % (key, content, directive_tag))
			return

		directive["text"] = content

#handle the ending data
def handle_ending_string(key, content, ending, d):
	if key == ending_tag:
		#this is a new ending, so store the previous ending (if any)
		add_ending(ending, d)
		#reset the ending data
		ending.clear()
		#and add the title of the new ending
		ending["title"] = content
		return
	elif key == ending_gender_tag:
		if len(content) <= 0: #if the gender wasn't specified, use "any"
			content = "any"
		ending["gender"] = content
		return
	elif key == ending_hint_tag:
		ending["hint"] = content
		return
	elif key == ending_preview_tag:
		if len(content) > 0:
			ending['img'] = content
		return
	elif key == ending_conditions_tag:
		condition_parts = content.split(',')
		for c in condition_parts:
			try:
				cond_type, cond_value = c.rsplit(':', 1)
				cond_type = cond_type.strip()
				cond_value = cond_value.strip()
				if cond_type in ending_condition_types:
					if cond_value != '':
						ending[cond_type] = cond_value
					else:
						print("Epilogue condition without value for \"%s\": \"%s\". Skipping." % (ending['title'], cond_type))
				else:
					print("Unknown epilogue condition %s" % cond_type)

			except ValueError:
				print("Epilogue condition with empty value for \"%s\": \"%s\" Skipping." % (ending['title'], c))
		return
		
	#get the screens variable
	screens = None
	if "screens" in ending:
		screens = ending["screens"]
	else:
		#or make one, if it doesn't already exist
		screens = list()
		ending["screens"] = screens
		
	#get the current screen
	screen = None
	if len(screens) >= 1:
		screen = screens[-1]

	if key in scene_tags and screen is None:
		handle_scene_ending_string(key, content, ending, d)
	else:
		#background image for a screen - makes a new screen
		if key == screen_tag:
			screen = dict()
			screens.append(screen)
			screen["image"] = content
			screen["text_boxes"] = list()
			return
	
		#make sure we have a screen ready, because the other tags are specific to a screen
		if screen is None:
			print("Error - using tag \"%s\" with value \"%s\", without a screen variable - use the \"%s\" tag first to put this information on that screen." % (key, content, screen_tag))
			return
	
		text_boxes = screen["text_boxes"]
	
		#the actual text of the text box. this makes a new text box
		if key == text_tag:
			text_box = dict()
			text_box[text_tag] = content
			text_boxes.append(text_box)
			return
		
		#get the current text box for the current screen
		text_box = None
		if len(text_boxes) >= 1:
			text_box = text_boxes[-1]
		else:
			print("Error - trying to use tag \"%s\" with value \"%s\", without making a text box. Use the \"%s\" tag first." % (key, content, text_tag))
			return
	
		#x position. Can be a css value, or "centered"
		if key == x_tag:
			text_box[x_tag] = content
			return
	
		#y position. Is a css value.
		elif key == y_tag:
			text_box[y_tag] = content
			return
	
		#width of a text box. defaults to 20%
		elif key == width_tag:
			text_box[width_tag] = content
			return
		
		#direction of the dialogue box arrow (if anything)
		elif key == arrow_tag:
			text_box[arrow_tag] = content
			return
		
	
#read in a character's data
def read_player_file(filename):
	case_names = [s['key'] for s in situations]
	
	d = {}
	
	ending = dict()
	
	stage = -1
	
	f = open(filename, 'r')
	for line_number, line in enumerate(f):
		line = line.strip()
		
		line_data = dict() #all of the lines data:
		#key is the stage and situation in which the line should be used. includes a stage number for stage-specific lines
		#image = the image filename (if no extension, assumed to be png)
		#target = if the line targets a particular other character
		#targetStage = if the line targets a particular stage for a particular character
		#filter = if the line targets a particular tag
		
		if len(line) <= 0 or line[0]=='#': #use # as a comment character, and skip empty lines
			continue
		
		#check for characters that can't be used
		if sys.version_info[0] == 2:
			skip_line = False
			try:
				# In utf-8, characters using umlauts are actually encoded as two separate characters
				# so we need to try to decode the entire line instead of individual characters
				line.decode('utf-8')
			except UnicodeDecodeError:
				# Find out which character
				problem_character = ""
				for c in line:
					try:
						c.decode('utf-8')
					except UnicodeDecodeError:
						problem_character = c
						break

				if (len(problem_character) > 0):
					print("Unable to decode character %s in line %d: \"%s\"" % (problem_character, line_number, line))
				else:
					print("Unable to decode line \"%s\" in line %d: " % (line, line_number))

				skip_line = True
				break

			if skip_line:
				continue
		
		#split the lines, then check for malformed entries
		try:
			key, text = line.split("=", 1)
		except ValueError:
			#this helps to find lines that are misformed 
			print("Unable to split line %d: \"%s\"" % (line_number, line))
			continue
		
		key = key.strip().lower()
		
		text = unescapeHTML(text.strip())
		
		
		#now deal with any possible targets and filters
		target_type = "skip" #reset any previous target type. this should only be used if there's a target present, but setting it here just in case
		if ',' in key:
			target_parts = key.split(',')
			key = target_parts[0]
			targets = target_parts[1:]
			for t in targets:
			
				try:
					target_type, target_value = t.rsplit(":", 1)
				except ValueError:
					#make sure the target has a format we can understand
					print("Invalid targeting for line %d - \"%s\". Skipping line." % (line_number, line))
					target_type = "skip"
					text = ""
					target_value = "N/A"
				
				target_type = target_type.strip()
				target_value = target_value.strip()
				
				#make sure there's a target. Can I check the data here to make sure that a target is valid?
				if len(target_value) <= 0:
					print("No target value specified for line %d - \"%s\". Skipping line." % (line_number, line))
					target_type = skip
					text = ""
				
				#now actually process valid targets
				#valid targets
				if target_type in all_targets:
					line_data[target_type] = target_value
					
				elif target_type == "skip":
					#skip this target type
					pass

				elif target_type in ["marker", "direction", "location", "weight"]:
					line_data[target_type] = target_value
					pass

				elif target_type.startswith("count-") or target_type == "count":
					condition_filter = target_type[6::]
					if "conditions" not in line_data:
						line_data["conditions"] = [[condition_filter, target_value]]
					else: line_data["conditions"].append([condition_filter, target_value])
					
				elif target_type.startswith("test:"):
					test_expr = target_type[5::]
					if "tests" not in line_data:
						line_data["tests"] = [[test_expr, target_value]]
					else: line_data["tests"].append([test_expr, target_value])

				else:
					#unknown target type
					print("Error - unknown target type \"%s\" for line %d - \"%s\". Skipping line." % (target_type, line_number, line))
					text = "" #make the script skip this line
					
				if target_type == "targetstage":
					#print a warning if they used a targetStage without a target
					have_target = False
					for other_target_data in targets:
						if "target:" in other_target_data:
							have_target = True
							break
					if not have_target:
						print("Warning - using a targetStage for line %d - \"%s\" without using a target value" % (line_number, line))
		
		
		#if the key contains a -, it belongs to a specific stage
		if '-' in key:
			stg, part_key = key.rsplit('-', 1)
			
			#if it starts with a * use the current stage
			if stg[0] == '*':
				key = "%d-%s" % (stage, part_key)
				line_data["stage"] = str(stage)

			#negative numbers count from the end. -1 is finished, -2 is masturbating, -3 is nude. -4 is the last layer of clothing, and so on.
			#using negative numbers assumes that they are after all the clothes entries
			elif stg[0] == '-' and stg[1:].isdigit():
				key = "%d-%s" % (stage + 4 + int(stg), part_key)
				line_data["stage"] = str(stage + 4 + int(stg))
			else:
				line_data["stage"] = str(stg)

		else:
			part_key = key
		
		#cases, these can be repeated
		if part_key in case_names:
		
			line_data["key"] = part_key
		
			if text == "" or text == ",":
				#if there's no entry, skip it.
				continue
				
			if ',' not in text:
				#img, desc = "", text
				line_data["image"] = ""
				line_data["text"] = text
			else:
				img,desc = text.split(",", 1) #split into (image, text) pairs
				line_data["image"] = img
				line_data["text"] = desc.strip()

			if line_data["text"].find('~silent~') == 0:
				line_data["text"] = ""
			else:
				line_data["text"] = capitalizeDialogue(line_data["text"])

			#print "adding line", line	
			
			if key in d:
				d[key].append(line_data) #add it to existing list of responses
			else:
				d[key] = [line_data] #make a new list of responses
				
		#clothes is a list
		elif key == "clothes":
			stage += 1
			if "clothes" in d:
				d["clothes"].append(text)
			else:
				d["clothes"] = [text]

	#intelligence is written as
	#   intelligence=bad
	#   intelligence=good,3
	#this means to start at bad intelligence and switch to good starting at stage 3
	#   The label can be changed in the same manner
		elif key in ("intelligence", "label"):
			parts = text.split(",", 1)
			(from_stage, value) = (0 if len(parts) == 1 else parts[1], parts[0])
			if key in d:
				d[key][from_stage] = value
			else:
				d[key] = {from_stage: value}

		#tags for the character i.e. blonde, athletic, cute
		#tags can be written as either:
		#	tag=blonde
		#	tag=athletic
		#or as
		#	tags=blond, athletic
		elif key == "tag":
			if "character_tags" in d:
				if not text in d["character_tags"]:
					d["character_tags"].append(text)
				else:
					print("Warning - duplicated tag: '%s'" % text)
			else:
				d["character_tags"] = [text]

		elif key == "tags":
			character_tags = [tag.strip() for tag in text.split(',')]
			if "character_tags" in d:
				d["character_tags"] = d["character_tags"] + character_tags
			else:
				d["character_tags"] = character_tags

		elif key == "marker":
			if "markers" in d:
				d["markers"].append(text)
			else:
				d["markers"] = [text]

		#write start lines last to first
		elif key == "start":
			if key in d:
				d[key].append(capitalizeDialogue(text))
			else:
				d[key] = [capitalizeDialogue(text)]

		#this tag relates to an ending sequence
		#use a different function, because it's quite complicated
		elif key in ending_tags:
			handle_ending_string(key, text, ending, d)
		
		#other values are single lines. These need to be in the data, even if the value is empty
		else:
			d[key] = text
	
	#add the final ending (if it exists)
	add_ending(ending, d)
	
    #set default intelligence, if the writer doesn't set it
	if "intelligence" not in d:
		d["intelligence"] = [["0", "average"]]

	return d

#make the meta.xml file
def make_meta_xml(data, filename):
	o = Element("opponent")
	
	enabled = "true" if "enabled" not in data or data["enabled"] == "true" else "false"
	o.subElement("enabled", enabled)
	
	values = ["first","last","label","pic","gender","height","from","writer","artist","description","endings","layers","character_tags"]
	
	for value in values:
		if value == "pic":
			pic = data["pic"]
			if pic == "":
				pic = "0-calm"
			o.subElement("pic", pic + ".png")

		elif value == "layers":
			#the number of layers of clothing is taken directly from the clothing data
			o.subElement("layers", str(len(data["clothes"])))

		elif value == "label":
			o.subElement("label", data["label"][0])

		elif value == "character_tags":
			tags_elem = o.subElement("tags")
			character_tags = data["character_tags"]
			for tag in character_tags:
			       tags_elem.subElement("tag", tag)

		elif value == "endings":
			if "endings" in data:
				#for each ending
				for ending in data["endings"]:
					ending_xml = o.subElement("epilogue", ending["title"], {'gender': ending["gender"]})

					if 'img' in ending:
						ending_xml.set('img', ending['img'])
					else:
						ending_xml.set('img', ending["screens"][0]["image"])

					for cond_type in ending_condition_types:
						if cond_type in ending:
							if 'markers' in cond_type:
								ending_xml.set('markers', 'true')
							else:
								ending_xml.set(cond_type, ending[cond_type])

		elif value in data:
			o.subElement(value, data[value])

	open(filename, 'w').write(o.serialize())

#make the marker.xml file
def make_markers_xml(data, filename):
	if "markers" in data:
		o = Element("markers")
		markers = data["markers"]
		for marker_data in markers:
			name, scope, desc = marker_data.split(",", 2)
			if scope == "public":
				scope = "Public"
			elif scope == "private":
				scope = "Private"
			o.subElement("marker", desc, [("name", name), ("scope",scope)])
		
		open(filename, 'w').write(o.serialize())

#read the input data, the write the xml files
def make_xml(player_filename, out_filename, meta_filename=None, marker_filename=None):
	get_situations_from_xml()
	player_dictionary = read_player_file(player_filename)
	write_xml(player_dictionary, out_filename)
	if meta_filename is not None:
		make_meta_xml(player_dictionary, meta_filename)
	if marker_filename is not None:
		make_markers_xml(player_dictionary, marker_filename)

#make the xml files using the given arguments
#python make_xml <character data file> <behaviour.xml output file> <meta.xml output file>
if __name__ == "__main__":
	if len(sys.argv) <= 1:
		print("Please give the name of the dialogue file to process into XML files")
		exit()
	behaviour_name = "behaviour.xml"
	meta_name = "meta.xml"
	marker_name = "markers.xml"
	if len(sys.argv) > 2:
		behaviour_name = sys.argv[2]
	if len(sys.argv) > 3:
		meta_name = sys.argv[3]
	if len(sys.argv) > 4:
		marker_name = sys.argv[4]
		
	make_xml(sys.argv[1], behaviour_name, meta_name, marker_name)


