(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(['monika'], factory);
    } else if (typeof exports === 'object') {
        // Node, CommonJS-like
        module.exports = factory(require('monika'));
    } else {
        // Browser globals (root is window)
        root.monika_effects = factory(root, root.monika);
        root.monika.effects = root.monika_effects;
    }
}(this, function (root, monika) {

    var exports = {};

    /* Effect - base class for all effects. */
    function Effect(id) {
        this.active = false;
    }

    Effect.prototype.execute = function (on_finished) {
        this.active = true;
        if (monika.active_effects.indexOf(this) < 0) {
            monika.active_effects.push(this);
        }

        if (on_finished) on_finished();
    };

    Effect.prototype.revert = function (on_finished) {
        this.active = false;

        var idx = monika.active_effects.indexOf(this);
        if (idx >= 0) {
            monika.active_effects.splice(idx, 1);
        }

        if (on_finished) on_finished();
    };

    Effect.prototype.constructor = Effect;
    exports.Effect = Effect;

    /* CanvasEffect - an Effect that uses a Canvas for image processing.
     *  - source_elem: jQuery object containing the source <img> element to modify.
     *  - processing_cb: a callback that takes this CanvasEffect object and performs out the needed image modifications.
     *  - keep_empty: if true, the internal canvas will not be pre-filled with the source image.
     */
    function CanvasEffect(source_elem, processing_cb, keep_empty) {
        Effect.call(this);

        this.dest_elem = source_elem;
        this.image = new Image();
        this.canvas = document.createElement('canvas');
        this.context = this.canvas.getContext('2d');

        this.source = null;
        this.data_url = null;

        this.processing_cb = processing_cb;
        this.from_empty_canvas = keep_empty;

        this.on_finished = null;

        this.image.onload = function () {
            try {
                this.canvas.width = this.image.naturalWidth;
                this.canvas.height = this.image.naturalHeight;

                if (this.canvas.width <= 0 || this.canvas.height <= 0) {
                    return;
                }

                if (!this.from_empty_canvas) {
                    this.context.drawImage(this.image, 0, 0);
                }

                this.processing_cb(this);

                this.data_url = this.canvas.toDataURL();
                this.dest_elem[0].src = this.data_url;
            } catch (e) {
                console.error(e);
            } finally {
                if (this.on_finished) this.on_finished();
            }
        }.bind(this);
    }

    CanvasEffect.prototype = Object.create(Effect.prototype);
    CanvasEffect.prototype.constructor = CanvasEffect;
    exports.CanvasEffect = CanvasEffect;

    CanvasEffect.prototype.execute = function (on_finished) {
        Effect.prototype.execute.call(this);

        this.source = this.dest_elem.attr('src');

        this.on_finished = on_finished;
        this.image.src = this.source;
    }

    CanvasEffect.prototype.revert = function (on_finished) {
        if (this.active) {
            Effect.prototype.revert.call(this);

            if (this.dest_elem && this.data_url && this.source) {
                // the element's displayed image might have changed in the meantime (game advancing, pose changes, etc.)
                // don't mess with images that weren't set by us.
                if (this.dest_elem[0].src === this.data_url) {
                    this.dest_elem[0].src = this.source;
                }

                this.data_url = null;
            }
        }

        if (on_finished) on_finished();
    }

    /* Canvas effect filters... */

    /* generate `n_divisions` random numbers that all sum to `total_sum`
     * not perfect, statistically speaking-- but good enough for our purposes. */
    function random_divide(n_divisions, total_sum) {
        var divisions = [];
        var div_sum = 0;

        for (var i = 0; i < n_divisions; i++) {
            var r = Math.random();

            divisions.push(r);
            div_sum += r;
        }

        return divisions.map(function (v) {
            return Math.floor((v / div_sum) * total_sum);
        });
    }


    CanvasEffect.prototype.tile_filter = function (tile_src_images) {
        /* Replace the entirety of the input image with random tiles taken from the images in tile_src_images */
        this.context.save();

        var tile_size = 32; // px square
        var width_tiles = Math.ceil(this.canvas.width / tile_size);
        var height_tiles = Math.ceil(this.canvas.height / tile_size);

        for (var iter = 0; iter < 4; iter++) {
            for (var col = 0; col < width_tiles; col++) {
                for (var row = 0; row < height_tiles; row++) {
                    var src = getRandomNumber(0, tile_src_images.length);

                    var x_offset = getRandomNumber(-9, 9);
                    var y_offset = getRandomNumber(-9, 9);

                    // source tile coordinates
                    var sx_tile = col + x_offset;
                    var sy_tile = row + y_offset;

                    // draw the tile into the image
                    this.context.drawImage(
                        tile_src_images[src][0],
                        sx_tile * tile_size, sy_tile * tile_size, // source pixel coords
                        tile_size, tile_size, // source rect size
                        col * tile_size, row * tile_size, // dest pixel coords
                        tile_size, tile_size // dest rect size
                    );
                }
            }
        }

        this.context.restore();
    }

    CanvasEffect.prototype.waver_filter = function (start_y, affected_height) {
        /* Divide the image into horizontal slices and offset them randomly */
        this.context.save();

        if (!start_y) {
            start_y = 0;
        }

        if (!affected_height) {
            affected_height = this.canvas.height;
        }

        // create a copy of the image data to use for offsetting:
        var original = this.context.getImageData(0, 0, this.canvas.width, this.canvas.height);
        var cp = this.context.createImageData(original);
        cp.data.set(original.data);

        // divide image into random slices
        var divisions = random_divide(getRandomNumber(7, 15), affected_height);

        // draw slices from the copy onto the destination canvas w/ random offset:
        var current_y = start_y;
        for (var i = 0; i < divisions.length; i++) {
            var random_offset = getRandomNumber(0, Math.floor(cp.width / 5));
            if (Math.random() < 0.5) {
                random_offset *= -1;
            }

            // clear destination canvas segment:
            this.context.clearRect(0, current_y, cp.width, divisions[i]);
            this.context.putImageData(cp, random_offset, 0, 0, current_y, cp.width, divisions[i]);

            current_y += divisions[i];
        }

        this.context.restore();
    }

    CanvasEffect.prototype.channel_split_filter = function (start_y, affected_height) {
        this.context.save();

        /* Randomly offset one channel in each strip */
        if (!start_y) {
            start_y = 0;
        }

        if (!affected_height) {
            affected_height = this.canvas.height;
        }

        // divide image into random slices
        var divisions = random_divide(getRandomNumber(15, 25), affected_height);

        var original = this.context.getImageData(0, 0, this.canvas.width, this.canvas.height);
        var outData = this.context.createImageData(this.canvas.width, this.canvas.height);

        /* Copy over unaffected areas */
        outData.data.set(original.data);

        // draw slices from random channels onto the destination canvas w/ random offset:
        var current_y = start_y;
        for (var i = 0; i < divisions.length; i++) {
            var random_x_offset = getRandomNumber(0, Math.floor(this.canvas.width * 0.25));
            if (Math.random() < 0.5) {
                random_x_offset *= -1;
            }

            var random_y_offset = getRandomNumber(0, Math.floor(this.canvas.height * 0.01));
            if (Math.random() < 0.5) {
                random_y_offset *= -1;
            }

            /* Randomly select a channel */
            var offset_channel = Math.floor(Math.random() * 3);
            var pixel_index_offset = ((random_y_offset * this.canvas.width) + random_x_offset) * 4;

            for (
                var dstIdx = current_y * this.canvas.width * 4; dstIdx < (current_y + divisions[i]) * this.canvas.width * 4; dstIdx += 4
            ) {
                var srcIdx = dstIdx + pixel_index_offset;

                if (srcIdx < 0) continue;
                if (srcIdx >= original.data.length - 4) break;

                outData.data[dstIdx + offset_channel] = original.data[srcIdx + offset_channel];
            }

            current_y += divisions[i];
        }

        this.context.putImageData(outData, 0, 0);
        this.context.restore();
    }

    CanvasEffect.prototype.color_bars_filter = function () {
        /* Apply randomly-colored overlays to horizontal strips of the image */
        this.context.save();

        this.context.globalCompositeOperation = 'source-atop';

        var divisions = random_divide(getRandomNumber(10, 20), this.canvas.height);
        var current_y = 0;

        for (var i = 0; i < divisions.length; i++) {
            if (Math.random() >= 0.4) { // don't colorize 40% of the strips
                // generate a random fill color:
                var hue = getRandomNumber(0, 360);
                this.context.fillStyle = "hsl(" + hue.toString() + ", 100%, 50%)";

                // randomize alpha within 25%-50% range
                this.context.globalAlpha = (Math.random() * 0.25) + 0.25;

                // draw the strip:
                this.context.fillRect(0, current_y, this.canvas.width, divisions[i]);

                current_y += divisions[i];
            }
        }

        this.context.restore();
    }

    /* End canvas effect code */


    function CollectionEffect() {
        Effect.call(this);
        this.subeffects = [];
    }

    CollectionEffect.prototype = Object.create(Effect.prototype);
    CollectionEffect.prototype.constructor = CollectionEffect;
    exports.CollectionEffect = CollectionEffect;

    CollectionEffect.prototype.push = function (effect) {
        this.subeffects.push(effect);
    }

    CollectionEffect.prototype.remove = function (effect) {
        var idx = this.subeffects.indexOf(effect);
        if (idx >= 0) this.subeffects.splice(idx, 1);
    }

    CollectionEffect.prototype.execute = function (on_finished) {
        Effect.prototype.execute.call(this);

        var n_completed = 0;
        var complete = false;

        this.subeffects.forEach(function (effect) {
            effect.execute(function () {
                n_completed += 1;

                if (!complete && n_completed >= this.subeffects.length && on_finished) {
                    complete = true;
                    on_finished();
                }
            }.bind(this));
        }.bind(this));
    }

    CollectionEffect.prototype.revert = function (on_finished) {
        if (this.active) {
            Effect.prototype.revert.call(this);

            var n_completed = 0;
            var complete = false;

            this.subeffects.forEach(function (effect) {
                effect.revert(function () {
                    n_completed += 1;

                    if (!complete && n_completed >= this.subeffects.length && on_finished) {
                        complete = true;
                        on_finished();
                    }
                }.bind(this));
            }.bind(this));
        } else if (on_finished) {
            return on_finished();
        }
    }


    function VisualGlitchEffect(target_slot, start_y, affected_height) {
        CollectionEffect.call(this);

        this.target_slot = target_slot;
        this.target_display = gameDisplays[target_slot - 1];

        this.start_y = start_y;
        this.affected_height = affected_height;

        if (this.target_display.pose instanceof Pose) {
            var target_pose = this.target_display.pose;

            for (var id in target_pose.sprites) {
                if (target_pose.sprites.hasOwnProperty(id)) {
                    this.subeffects.push(new CanvasEffect(
                        $(target_pose.sprites[id].img),
                        function (canvas_effect) {
                            canvas_effect.channel_split_filter();
                            canvas_effect.waver_filter();
                        }.bind(this),
                        false
                    ));
                }
            }
        } else {
            this.subeffects.push(new CanvasEffect(
                this.target_display.simpleImage,
                function (canvas_effect) {
                    canvas_effect.channel_split_filter(this.start_y, this.affected_height);
                    canvas_effect.waver_filter(this.start_y, this.affected_height);
                }.bind(this),
                false
            ));
        }
    }

    VisualGlitchEffect.prototype = Object.create(CollectionEffect.prototype);
    VisualGlitchEffect.prototype.constructor = VisualGlitchEffect;
    exports.VisualGlitchEffect = VisualGlitchEffect;


    /* VisualSuggestedOppGlitchEffect - a variant on the above VisualGlitchEffect
     * that is used for the Suggested Opponents instead.
     */
    function VisualSuggestedOppGlitchEffect(target_slot, target_quad, start_y, affected_height) {
        CollectionEffect.call(this);

        this.target_slot = target_slot;
        this.target_quad = target_quad;
        this.target_image = mainSelectDisplays[target_slot].suggestionQuad[target_quad].children('.opponent-suggestion-image');

        this.start_y = start_y;
        this.affected_height = affected_height;
        
        this.subeffects.push(new CanvasEffect(
            this.target_image,
            function (canvas_effect) {
                canvas_effect.channel_split_filter(this.start_y, this.affected_height);
                canvas_effect.waver_filter(this.start_y, this.affected_height);
            }.bind(this),
            false
        ));
    }

    VisualSuggestedOppGlitchEffect.prototype = Object.create(CollectionEffect.prototype);
    VisualSuggestedOppGlitchEffect.prototype.constructor = VisualSuggestedOppGlitchEffect;
    exports.VisualSuggestedOppGlitchEffect = VisualSuggestedOppGlitchEffect;


    /* RepeatingVisualGlitchEffect - a variant on the above VisualGlitchEffect
     * that repeatedly applies a visual glitch.
     */
    function RepeatingVisualGlitchEffect(target_slot, glitchTime, normalTime) {
        Effect.call(this);
        this.visual_glitch_effect = new VisualGlitchEffect(target_slot);

        this.glitchTime = glitchTime;
        this.normalTime = normalTime;

        this.cancelled = false;
        this.currentTimerID = null;
    }

    RepeatingVisualGlitchEffect.prototype = Object.create(Effect.prototype);
    RepeatingVisualGlitchEffect.prototype.constructor = RepeatingVisualGlitchEffect;
    exports.RepeatingVisualGlitchEffect = RepeatingVisualGlitchEffect;

    RepeatingVisualGlitchEffect.prototype.glitchOn = function () {
        if (this.cancelled) {
            return;
        }
        this.visual_glitch_effect.execute(function () {
            this.currentTimerID = setTimeout(this.glitchOff.bind(this), this.glitchTime);
        }.bind(this));
    }

    RepeatingVisualGlitchEffect.prototype.glitchOff = function () {
        if (this.cancelled) {
            return;
        }
        this.visual_glitch_effect.revert(function () {
            this.currentTimerID = setTimeout(this.glitchOn.bind(this), this.normalTime);
        }.bind(this));
    }

    RepeatingVisualGlitchEffect.prototype.execute = function (on_finished) {
        Effect.prototype.execute.call(this);

        this.cancelled = false;
        this.glitchOn();

        if (on_finished) on_finished();
    }

    RepeatingVisualGlitchEffect.prototype.revert = function (on_finished) {
        if (this.active) {
            Effect.prototype.revert.call(this);

            this.cancelled = true;
            clearTimeout(this.currentTimerID);
            this.visual_glitch_effect.revert(on_finished);
        } else if (on_finished) {
            return on_finished();
        }
    }


    function GlitchPoseChange(target_slot, dest_pose, glitch_time, start_y, affected_height) {
        VisualGlitchEffect.call(this, target_slot, start_y, affected_height);

        this.target_player = players[target_slot];
        this.glitchTime = glitch_time;
        this.dest_pose_name = dest_pose;
        this.currentTimerID = null;

        if (!dest_pose) {
            this.dest_pose = null;
        } else if (dest_pose.startsWith('custom:')) {
            var key = dest_pose.split(':', 2)[1];
            var poseDef = this.target_player.poses[key];

            if (poseDef) {
                this.dest_pose = new Pose(poseDef, this);
            } else {
                this.dest_pose = null;
            }
        } else {
            this.dest_pose = this.target_player.folder + dest_pose;
        }
    }

    GlitchPoseChange.prototype = Object.create(VisualGlitchEffect.prototype);
    GlitchPoseChange.prototype.constructor = GlitchPoseChange;
    exports.GlitchPoseChange = GlitchPoseChange;

    GlitchPoseChange.prototype.doTransition = function (on_finished) {
        try {
            if (this.original_pose && this.original_pose === this.target_display.pose) {
                if (!this.dest_pose) {
                    this.target_display.clearPose();
                } else {
                    this.target_display.drawPose(this.dest_pose);
                }
            }
        } catch (e) {
            monika.reportException('in glitch pose transition', e);
        } finally {
            if (on_finished) return on_finished();
        }
    }

    GlitchPoseChange.prototype.execute = function (on_finished) {
        this.original_pose = this.target_display.pose;

        VisualGlitchEffect.prototype.execute.call(this, function () {
            this.currentTimerID = setTimeout(this.doTransition.bind(this, on_finished), this.glitchTime);
        }.bind(this));
    }

    GlitchPoseChange.prototype.revert = function (on_finished) {
        if (this.active) {
            try {
                if (this.original_pose) {
                    this.target_display.drawPose(this.original_pose);
                } else {
                    this.target_display.clearPose();
                }
            } catch (e) {
                monika.reportException('in glitch pose reverting', e);
            } finally {
                Effect.prototype.revert.call(this);
            }
        }

        if (on_finished) return on_finished();
    }


    /* DialogueContentEffect - an Effect that modifies the _content_ of a
     * character's dialogue.
     */
    function DialogueContentEffect(target_slot, dialogueReplacement) {
        Effect.call(this);

        this.target_slot = target_slot;
        this.target_display = gameDisplays[target_slot - 1];
        this.replacement = dialogueReplacement;
    }

    DialogueContentEffect.prototype = Object.create(Effect.prototype);
    DialogueContentEffect.prototype.constructor = DialogueContentEffect;
    exports.DialogueContentEffect = DialogueContentEffect;

    DialogueContentEffect.prototype.execute = function (on_finished) {
        Effect.prototype.execute.call(this);

        this.original = this.target_display.dialogue.html();

        /* Avoid accidentally causing multiple script invocations due to replacements. */
        this.original = this.original.replace(/<script>.+<\/script>/gi, '');

        if (typeof this.replacement === 'function') {
            var modified = this.replacement(this.original, this.target_display);
        } else {
            var modified = this.replacement
        }

        this.target_display.dialogue.html(modified);

        if (on_finished) on_finished();
    }

    DialogueContentEffect.prototype.revert = function (on_finished) {
        if (this.active) {
            Effect.prototype.revert.call(this);

            this.target_display.dialogue.html(this.original);
        }

        if (on_finished) on_finished();
    }


    /* DialogueStyleEffect - an Effect that applies a CSS style to a character's
     * dialogue or bubble.
     */
    function DialogueStyleEffect(target_slot, textStyle, bubbleStyle) {
        Effect.call(this);

        this.target_display = gameDisplays[target_slot - 1];
        this.textStyle = textStyle;
        this.bubbleStyle = bubbleStyle;
    }

    DialogueStyleEffect.prototype = Object.create(Effect.prototype);
    DialogueStyleEffect.prototype.constructor = DialogueStyleEffect;
    exports.DialogueStyleEffect = DialogueStyleEffect;

    DialogueStyleEffect.prototype.execute = function (on_finished) {
        Effect.prototype.execute.call(this);

        if (this.textStyle) this.target_display.dialogue.addClass(this.textStyle);
        if (this.bubbleStyle) this.target_display.bubble.addClass(this.bubbleStyle);

        if (on_finished) on_finished();
    }

    DialogueStyleEffect.prototype.revert = function (on_finished) {
        if (this.active) {
            Effect.prototype.revert.call(this);

            if (this.textStyle) this.target_display.dialogue.removeClass(this.textStyle);
            if (this.bubbleStyle) this.target_display.bubble.removeClass(this.bubbleStyle);
        }

        if (on_finished) on_finished();
    }


    /* DialogueOverflowEffect - an Effect that causes a character's dialogue to
     * overflow the box. Also applies some styling.
     */
    function DialogueOverflowEffect(target_slot) {
        Effect.call(this);

        this.content_effect = new DialogueContentEffect(target_slot, this.doReplacement.bind(this));
        this.style_effect = new DialogueStyleEffect(target_slot, null, 'monika-overflow-bubble');
    }

    DialogueOverflowEffect.prototype = Object.create(Effect.prototype);
    DialogueOverflowEffect.prototype.constructor = DialogueOverflowEffect;
    exports.DialogueOverflowEffect = DialogueOverflowEffect;

    DialogueOverflowEffect.prototype.doReplacement = function (original, target_display) {
        var text = target_display.dialogue.text();

        /* Find all words in the dialogue. */
        var re = /\w+(\w)/gi;
        var all_words = [];
        var current_match = null;
        while ((current_match = re.exec(text)) != null) {
            all_words.push(current_match);
        }

        // don't corrupt dialogue that's very short.
        if (all_words.length >= 4) {
            var min = Math.floor(all_words.length * 0.4);
            var max = Math.floor(all_words.length * 0.6);
            var selected = all_words[getRandomNumber(min, max + 1)];

            /* Grab everything up to (but excluding) the targeted word */
            var modified_text = text.substr(0, selected.index);
            var corrupted_word = selected[0].substr(0, selected[0].length - 1) + selected[1].repeat(getRandomNumber(10, 25));

            /* Put the corrupted word in the edited style: */
            modified_text = modified_text + '<span class="monika-edited-dialogue">' + corrupted_word + '</span>';

            return modified_text;
        }

        return original;
    }

    DialogueOverflowEffect.prototype.execute = function (on_finished) {
        Effect.prototype.execute.call(this);

        this.content_effect.execute();
        this.style_effect.execute();

        if (on_finished) on_finished();
    }

    DialogueOverflowEffect.prototype.revert = function (on_finished) {
        if (this.active) {
            Effect.prototype.revert.call(this);

            this.content_effect.revert();
            this.style_effect.revert();
        }

        if (on_finished) on_finished();
    }

    /* Effects for doing various kinds of glitch text */
    var inline_glitch_chars = [
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
    ];

    /* DialogueCharacterReplacement - replaces some characters in dialogue with glitch characters */
    function DialogueCharacterReplacement(target_slot, replace_freq) {
        DialogueContentEffect.call(this, target_slot, this.inlineCharacterReplacement.bind(this));
        this.replace_freq = replace_freq;
    }

    DialogueCharacterReplacement.prototype = Object.create(DialogueContentEffect.prototype);
    DialogueCharacterReplacement.prototype.constructor = DialogueCharacterReplacement;
    exports.DialogueCharacterReplacement = DialogueCharacterReplacement;

    DialogueCharacterReplacement.prototype.inlineCharacterReplacement = function (original, target_display) {
        var out_str = "";

        for (var i = 0; i < original.length; i++) {
            if (Math.random() < this.replace_freq) {
                out_str += inline_glitch_chars[getRandomNumber(0, inline_glitch_chars.length)];
            } else {
                out_str += original.charAt(i);
            }
        }

        return out_str;
    }


    function generate_character_sequence(allowed_chars, length) {
        var ret = '';
        for (var i = 0; i < length; i++) {
            ret += allowed_chars[getRandomNumber(0, allowed_chars.length)];
        }

        return ret;
    }


    function generate_glitch_text(length) {
        return generate_character_sequence(inline_glitch_chars, length);
    }

    exports.generate_glitch_text = generate_glitch_text;


    // taken from tchouky's zalgo text site: http://www.eeemo.net/

    //those go UP
    var zalgo_up = [
        '\u030d', /*     ̍     */ '\u030e', /*     ̎     */ '\u0304', /*     ̄     */ '\u0305', /*     ̅     */
        '\u033f', /*     ̿     */ '\u0311', /*     ̑     */ '\u0306', /*     ̆     */ '\u0310', /*     ̐     */
        '\u0352', /*     ͒     */ '\u0357', /*     ͗     */ '\u0351', /*     ͑     */ '\u0307', /*     ̇     */
        '\u0308', /*     ̈     */ '\u030a', /*     ̊     */ '\u0342', /*     ͂     */ '\u0343', /*     ̓     */
        '\u0344', /*     ̈́     */ '\u034a', /*     ͊     */ '\u034b', /*     ͋     */ '\u034c', /*     ͌     */
        '\u0303', /*     ̃     */ '\u0302', /*     ̂     */ '\u030c', /*     ̌     */ '\u0350', /*     ͐     */
        '\u0300', /*     ̀     */ '\u0301', /*     ́     */ '\u030b', /*     ̋     */ '\u030f', /*     ̏     */
        '\u0312', /*     ̒     */ '\u0313', /*     ̓     */ '\u0314', /*     ̔     */ '\u033d', /*     ̽     */
        '\u0309', /*     ̉     */ '\u0363', /*     ͣ     */ '\u0364', /*     ͤ     */ '\u0365', /*     ͥ     */
        '\u0366', /*     ͦ     */ '\u0367', /*     ͧ     */ '\u0368', /*     ͨ     */ '\u0369', /*     ͩ     */
        '\u036a', /*     ͪ     */ '\u036b', /*     ͫ     */ '\u036c', /*     ͬ     */ '\u036d', /*     ͭ     */
        '\u036e', /*     ͮ     */ '\u036f', /*     ͯ     */ '\u033e', /*     ̾     */ '\u035b', /*     ͛     */
        '\u0346', /*     ͆     */ '\u031a' /*     ̚     */
    ];

    //those go DOWN
    var zalgo_down = [
        '\u0316', /*     ̖     */ '\u0317', /*     ̗     */ '\u0318', /*     ̘     */ '\u0319', /*     ̙     */
        '\u031c', /*     ̜     */ '\u031d', /*     ̝     */ '\u031e', /*     ̞     */ '\u031f', /*     ̟     */
        '\u0320', /*     ̠     */ '\u0324', /*     ̤     */ '\u0325', /*     ̥     */ '\u0326', /*     ̦     */
        '\u0329', /*     ̩     */ '\u032a', /*     ̪     */ '\u032b', /*     ̫     */ '\u032c', /*     ̬     */
        '\u032d', /*     ̭     */ '\u032e', /*     ̮     */ '\u032f', /*     ̯     */ '\u0330', /*     ̰     */
        '\u0331', /*     ̱     */ '\u0332', /*     ̲     */ '\u0333', /*     ̳     */ '\u0339', /*     ̹     */
        '\u033a', /*     ̺     */ '\u033b', /*     ̻     */ '\u033c', /*     ̼     */ '\u0345', /*     ͅ     */
        '\u0347', /*     ͇     */ '\u0348', /*     ͈     */ '\u0349', /*     ͉     */ '\u034d', /*     ͍     */
        '\u034e', /*     ͎     */ '\u0353', /*     ͓     */ '\u0354', /*     ͔     */ '\u0355', /*     ͕     */
        '\u0356', /*     ͖     */ '\u0359', /*     ͙     */ '\u035a', /*     ͚     */ '\u0323' /*     ̣     */
    ];

    //those always stay in the middle
    var zalgo_mid = [
        '\u0315', /*     ̕     */ '\u031b', /*     ̛     */ '\u0340', /*     ̀     */ '\u0341', /*     ́     */
        '\u0358', /*     ͘     */ '\u0321', /*     ̡     */ '\u0322', /*     ̢     */ '\u0327', /*     ̧     */
        '\u0328', /*     ̨     */ '\u0334', /*     ̴     */ '\u0335', /*     ̵     */ '\u0336', /*     ̶     */
        '\u034f', /*     ͏     */ '\u035c', /*     ͜     */ '\u035d', /*     ͝     */ '\u035e', /*     ͞     */
        '\u035f', /*     ͟     */ '\u0360', /*     ͠     */ '\u0362', /*     ͢     */ '\u0338', /*     ̸     */
        '\u0337', /*     ̷     */ '\u0361', /*     ͡     */ '\u0489' /*     ҉_     */
    ];

    /* DialogueZalgoText - inserts zalgo text into a character's dialogue */
    function DialogueZalgoText(target_slot) {
        DialogueContentEffect.call(this, target_slot, this.insertZalgoText.bind(this));
    }

    DialogueZalgoText.prototype = Object.create(DialogueContentEffect.prototype);
    DialogueZalgoText.prototype.constructor = DialogueZalgoText;
    exports.DialogueZalgoText = DialogueZalgoText;

    DialogueZalgoText.prototype.insertZalgoText = function (original, target_display) {
        var out_str = '';
        for (var i = 0; i < original.length; i++) {
            out_str += original.charAt(i);

            var n_up = getRandomNumber(0, 3);
            var n_mid = getRandomNumber(0, 2);
            var n_down = getRandomNumber(0, 3);

            out_str += generate_character_sequence(zalgo_up, n_up);
            out_str += generate_character_sequence(zalgo_mid, n_mid);
            out_str += generate_character_sequence(zalgo_down, n_down);
        }

        return out_str;
    }


    function FakeDeleteCharacter(target_slot) {
        CollectionEffect.call(this);

        this.target_slot = target_slot;

        this.image_sources = [];
        this.target_display = gameDisplays[target_slot - 1];

        this.original_label = this.target_display.label.html();
        this.target_player = players[target_slot];

        /* Get all displayed sprites as image sources */
        gameDisplays.forEach(function (disp) {
            if (!players[disp.slot]) return;

            if (disp.slot !== target_slot) {
                this.push(new DialogueContentEffect(
                    disp.slot,
                    this.replaceCharacterReferences.bind(this)
                ));
            } else {
                this.push(new DialogueCharacterReplacement(
                    disp.slot,
                    0.2
                ));
            }

            if (disp.pose instanceof Pose) {
                var target_pose = disp.pose;

                for (var id in target_pose.sprites) {
                    if (target_pose.sprites.hasOwnProperty(id)) {
                        this.image_sources.push($(target_pose.sprites[id].img));
                    }
                }
            } else {
                this.image_sources.push($(disp.simpleImage));
            }
        }.bind(this));

        /* Set up the visual effects */
        if (this.target_display.pose instanceof Pose) {
            var target_pose = this.target_display.pose;

            for (var id in target_pose.sprites) {
                if (target_pose.sprites.hasOwnProperty(id)) {
                    this.push(new CanvasEffect(
                        $(target_pose.sprites[id].img),
                        function (canvas_effect) {
                            canvas_effect.tile_filter(this.image_sources);
                        }.bind(this),
                        true
                    ));
                }
            }
        } else {
            this.push(new CanvasEffect(
                this.target_display.simpleImage,
                function (canvas_effect) {
                    canvas_effect.tile_filter(this.image_sources);
                }.bind(this),
                true
            ));
        }
    }

    FakeDeleteCharacter.prototype = Object.create(CollectionEffect.prototype);
    FakeDeleteCharacter.prototype.constructor = FakeDeleteCharacter;
    exports.FakeDeleteCharacter = FakeDeleteCharacter;

    FakeDeleteCharacter.prototype.replaceCharacterReferences = function (in_text) {
        var re = new RegExp(this.original_label, 'gi');
        var re2 = new RegExp(this.target_player.label, 'gi');

        var modified_dialogue = in_text.replace(re, function (match) {
            return generate_glitch_text(match.length + getRandomNumber(0, 5))
        });

        modified_dialogue = modified_dialogue.replace(re2, function (match) {
            return generate_glitch_text(match.length)
        });

        return modified_dialogue;
    }

    FakeDeleteCharacter.prototype.execute = function (on_finished) {
        this.target_display.label.html(generate_glitch_text(this.original_label.length));
        CollectionEffect.prototype.execute.call(this, on_finished);
    }

    FakeDeleteCharacter.prototype.revert = function (on_finished) {
        if (this.active) {
            this.target_display.label.html(this.original_label);
            CollectionEffect.prototype.revert.call(this, on_finished);
        } else if (on_finished) {
            return on_finished();
        }
    }

    function CharacterDisplacementEffect(slot, dest_slot) {
        Effect.call(this);

        this.target_slot = slot;
        this.target_display = gameDisplays[slot - 1];
        this.position_css = 'monika-over-ui ';

        if (dest_slot !== undefined) {
            this.position_css = this.position_css + ['veryFarLeft', 'farLeft', 'almostLeft', 'almostRight', 'farRight', 'veryFarRight'][dest_slot];
        } else {
            this.position_css = this.position_css + 'centered';
        }
    }

    CharacterDisplacementEffect.prototype = Object.create(Effect.prototype);
    CharacterDisplacementEffect.prototype.constructor = CharacterDisplacementEffect;
    exports.CharacterDisplacementEffect = CharacterDisplacementEffect;

    CharacterDisplacementEffect.prototype.execute = function (on_finished) {
        Effect.prototype.execute.call(this);

        this.clone_img = this.target_display.imageArea.clone();
        this.prev_pose = this.target_display.pose;

        this.clone_img.appendTo('#game-screen > .screen').addClass(this.position_css).css({
            'height': '80%',
            'bottom': '0',
            'top': ''
        });

        setTimeout(function () {
            this.target_display.clearPose();
            this.revertHook = monika.registerHook('updateGameVisual', 'pre', this.revert.bind(this, null));
        }.bind(this), 0);


        if (on_finished) on_finished();
    }

    CharacterDisplacementEffect.prototype.revert = function (on_finished) {
        if (this.active) {
            Effect.prototype.revert.call(this);

            if (this.clone_img) this.clone_img.remove();
            if (this.prev_pose) this.target_display.drawPose(this.prev_pose);
            if (this.revertHook) monika.unregisterHook('updateGameVisual', 'pre', this.revertHook);
        }

        if (on_finished) on_finished();
    }


    function GlitchMasturbationEffect(target_slot) {
        Effect.call(this);

        this.glitchEffect = new VisualGlitchEffect(target_slot);
        this.target_display = gameDisplays[target_slot - 1];
        this.target_slot = target_slot;
        this.heavy = false;
        this.intervalID = null;
        this.hookHandle = null;
        this.images = [];
    }

    GlitchMasturbationEffect.prototype = Object.create(Effect.prototype);
    GlitchMasturbationEffect.prototype.constructor = GlitchMasturbationEffect;
    exports.GlitchMasturbationEffect = GlitchMasturbationEffect;

    GlitchMasturbationEffect.prototype.shuffleImages = function () {
        this.images = [];
        
        var folderPath = 'opponents/monika/';
        var monika_pl = monika.utils.get_monika_player();
        
        if (monika_pl.alt_costume && monika_pl.alt_costume.id === 'monika_love_bug') {
            folderPath = 'opponents/reskins/monika_love_bug/';
        }

        for (var i = 1; i <= 9; i++) {
            if (this.heavy) {
                this.images.push(folderPath + '9-heavy-' + i + '.png');
            } else {
                this.images.push(folderPath + '9-mast-' + i + '.png');
            }
        }

        /* fisher-yates shuffle */
        for (var i = 0; i < this.images.length; i++) {
            var j = getRandomNumber(i, this.images.length);

            // swap images[i] and images[j]
            var t = this.images[i];
            this.images[i] = this.images[j];
            this.images[j] = t;
        }
    }

    GlitchMasturbationEffect.prototype.setHeavyMode = function (v) {
        this.heavy = v;
        this.shuffleImages();
    }

    GlitchMasturbationEffect.prototype.doTransition = function (after) {
        if (this.images.length <= 0) this.shuffleImages();
        var nextImage = this.images.pop();

        //console.log("doing transition from " + this.target_display.pose);
        if (monika.JOINT_FORFEIT_ACTIVE || !this.active) return;

        setTimeout(function () {
            if (monika.JOINT_FORFEIT_ACTIVE || !this.active) return;

            this.glitchEffect.execute(function () {
                setTimeout(function () {
                    if (monika.JOINT_FORFEIT_ACTIVE || !this.active) return;

                    this.target_display.simpleImage.attr('src', nextImage);
                    if (after) after();
                }.bind(this), 500);
            }.bind(this));
        }.bind(this), 0);
    }

    GlitchMasturbationEffect.prototype.updateGameVisualHook = function (player) {
        if (this.target_slot !== player || monika.JOINT_FORFEIT_ACTIVE) return;

        if (
            (players[this.target_slot].forfeit[0] === PLAYER_FINISHING_MASTURBATING) ||
            (players[this.target_slot].forfeit[0] === PLAYER_FINISHED_MASTURBATING)
        ) {
            this.revert();
        } else {
            if (players[this.target_slot].forfeit[0] === PLAYER_HEAVY_MASTURBATING && !this.heavy) {
                this.setHeavyMode(true);
            }
            this.doTransition();
        }
    }

    GlitchMasturbationEffect.prototype.execute = function (on_finished) {
        Effect.prototype.execute.call(this);

        this.doTransition();
        this.intervalID = setInterval(this.doTransition.bind(this), 5000);
        this.hookHandle = monika.registerHook('updateGameVisual', 'post', this.updateGameVisualHook.bind(this));

        if (on_finished) on_finished();
    }

    GlitchMasturbationEffect.prototype.revert = function (on_finished) {
        if (this.active) {
            Effect.prototype.revert.call(this);

            clearInterval(this.intervalID);
            this.glitchEffect.revert(on_finished);
            monika.unregisterHook('updateGameVisual', 'post', this.hookHandle);
        }

        if (on_finished) on_finished();
    }

    return exports;
}));