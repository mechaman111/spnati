monika.glitchElementAsync = function(jquery_elem, start_y, affected_height, cb) {
    return monika.get_canvas_async(jquery_elem, function (cv) {
        if (cv.canvas.width > 0 && cv.canvas.height > 0) {
            monika.channel_split_filter(cv, start_y, affected_height);
            monika.waver_filter(cv, start_y, affected_height);
            monika.set_image_from_canvas(jquery_elem, cv);
        }
        
        return cb(cv);
    });
}

monika.startElementGlitching = function (jquery_elem, glitchTime, normalTime, start_y, affected_height) {
    if(!glitchTime) glitchTime = 500;
    if(!normalTime) normalTime = 500;

    var glitchCanceled = false;
    var cv = null;
    var current_timer_id = undefined;
    
    var glitch_off = function () {
        if (glitchCanceled) {
            return;
        }
        
        cv = null;
        current_timer_id = setTimeout(glitchTime, glitch_on);
    }
    
    var glitch_on = function () {        
        if (glitchCanceled) {
            return;
        }
        
        monika.glitchElementAsync(jquery_elem, start_y, affected_height, function (cur_cv) {
            if (glitchCanceled) {
                return cv.undo();
            }
            
            cv = cur_cv;
            current_timer_id = setTimeout(glitchTime, glitch_off);
        });
    }
    
    glitchOn();
    
    return function () { // for stopping glitching
        glitchCanceled = true;
        clearInterval(current_timer_id);
        if (cv) {
            cv.undo();
        }
    }
}

/* Cause visual glitching in a given player's image. Will be reset as soon as they change poses.  */
monika.glitchCharacter = function(slot, start_y, affected_height, cb) {
    monika.glitchElementAsync($gameImages[slot-1], start_y, affected_height, function (cv) {
        monika.active_effects.character_glitch[slot-1] = cv;
        cb(cv);
    });
}

monika.undoCharacterGlitch = function(slot) {
    var cv = monika.active_effects.character_glitch[slot-1];
    if(cv) {
        monika.active_effects.character_glitch[slot-1] = null;
        return cv.undo();
    }
}

monika.startCharacterGlitching = function (slot, glitchTime, normalTime, start_y, affected_height) {
    monika.active_effects.character_glitching[slot-1] = monika.startElementGlitching($gameImages[slot-1], glitchTime, normalTime, start_y, affected_height);
}

monika.stopCharacterGlitching = function (slot) {
    var stopCallback = monika.active_effects.character_glitching[slot-1];
    if(stopCallback) {
        stopCallback();
    }
    monika.active_effects.character_glitching[slot-1] = null;
}

monika.temporaryCharacterGlitch = function(slot, delay, glitchTime, start_y, affected_height) {
    setTimeout(function () {
        monika.glitchCharacter(slot, start_y, affected_height, function (cv) {
            setTimeout(monika.undoCharacterGlitch.bind(null, slot), glitchTime);
        });
    }, delay);
}

monika.glitch_pose_transition = function(slot, next_img, delay, glitchTime, start_y, affected_height) {
    var do_glitch = function () {
        monika.glitchCharacter(slot, start_y, affected_height, function (cv) {
            setTimeout(function () {
                $gameImages[slot-1].attr('src', players[slot].folder+next_img);
            }, glitchTime);
        });
    }
    
    if(!delay || delay <= 0) {
        do_glitch();
    } else {
        setTimeout(do_glitch, delay);
    }
}

// For the blazer stripping animation
monika.glitch_strip_upper = function(slot, next_img) {
    return monika.glitch_pose_transition(slot, next_img, 500, 1000, 400, 776-400);
}

monika.generate_mast_images = function() {
    // generate a shuffled set of images to use
    // masturbation images go from 9-mast-1[-alt].png to 9-mast-9[-alt].png.

    monika.mast_images = [];

    for(var i=1;i<=9;i++) {
        if(monika.active_effects.glitch_heavy_masturbation) {
            monika.mast_images.push('opponents/monika/9-mast-'+i.toString()+'-alt.png');
        } else {
            monika.mast_images.push('opponents/monika/9-mast-'+i.toString()+'.png');
        }
    }

    /* fisher-yates shuffle */
    for(var i=0;i<monika.mast_images.length;i++) {
        var j = getRandomNumber(i, monika.mast_images.length);

        // swap images[i] and images[j]
        var t = monika.mast_images[i];
        monika.mast_images[i] = monika.mast_images[j];
        monika.mast_images[j] = t;
    }
}

monika.glitch_mast_heavy = function(st) {
    monika.active_effects.glitch_heavy_masturbation = st;
    monika.generate_mast_images();
}

monika.get_masturbation_image = function() {
    if(!monika.mast_images) {
        monika.mast_images = [];
    }


    if(monika.mast_images.length === 0) {
        monika.generate_mast_images();
    }

    return monika.mast_images.pop();
}

monika.glitch_masturbation = function(slot) {
    var glitchTime = 500;
    var normalTime = 8000;
    var yuriTime = 1500;

    var glitch_count = 0;
    var yuri_glitch_at = getRandomNumber(5, 215);
    var yuri_original_dialogue = null;

    monika.mast_images = [];

    console.log("[Monika] Scheduled Yuri glitch effect at "+yuri_glitch_at.toString());
    
    for(var i=0;i<players.length;i++) {
        // make sure Yuri isn't actually in the game if we do the surprise
        // TODO: update these values just in case when Yuri is actually added to the game
        if(players[i]) {
            if(players[i].label === "Yuri" || players[i].folder === "opponents/yuri/") {
                yuri_glitch_at = -1;
                break;
            }
        }
    }

    var do_glitch = function() {
        if(!monika.active_effects.glitch_masturbation) return;

        glitch_count += 1;
        
        monika.glitchCharacter(slot, null, null, function (cv) {
            monika.active_effects.glitch_masturbation = setTimeout(set_next_image, glitchTime);
        });
    }

    var set_next_image = function () {
        if(!monika.active_effects.glitch_masturbation) return;
        monika.active_effects.character_glitch[slot-1] = null;

        if(yuri_original_dialogue) {
            $gameDialogues[slot-1].html(yuri_original_dialogue);
            yuri_original_dialogue = null;
        }

        if(glitch_count === yuri_glitch_at) {
            yuri_original_dialogue = $gameDialogues[slot-1].html();

            $gameImages[slot-1].attr('src', 'opponents/monika/9-yuri-surprised.png');
            $gameDialogues[slot-1].html([
                "W-what? W-w-where am I? What is this? Are you--",
                "Wait, w-where am I? What just happened? Wait... are you--",
                "W-w-what just happened? Who are you people? W-where am I?",
            ][getRandomNumber(0, 3)]);
            
            monika.active_effects.glitch_masturbation = setTimeout(do_glitch, yuriTime);
        } else {
            var next_img = monika.get_masturbation_image();
            $gameImages[slot-1].attr('src', next_img);
            
            monika.active_effects.glitch_masturbation = setTimeout(do_glitch, normalTime);
        }
    }
    
    monika.active_effects.glitch_masturbation = setTimeout(do_glitch, normalTime / 2);
}

monika.stop_glitch_masturbation = function() {
    if(monika.active_effects.glitch_masturbation) {
        clearInterval(monika.active_effects.glitch_masturbation);
        monika.active_effects.glitch_masturbation = null;
    }
}

/* generate `n_divisions` random numbers that all sum to `total_sum`
 * not perfect, statistically speaking-- but good enough for our purposes. */
function random_divide(n_divisions, total_sum) {
    var divisions = [];
    var div_sum = 0;

    for(var i=0;i<n_divisions;i++) {
        var r = Math.random();

        divisions.push(r);
        div_sum += r;
    }

    return divisions.map(function(v) { return Math.floor((v / div_sum) * total_sum); });
}

monika.tile_filter = function(cv, tile_src_images) {
    /* Replace the entirety of the input image with random tiles taken from the images in tile_src_images */
    cv.ctx.save();
    
    var tile_size = 32; // px square
    var width_tiles = Math.ceil(cv.canvas.width / tile_size);
    var height_tiles = Math.ceil(cv.canvas.height / tile_size);
    
    var height_ranges = [];
    var width_ranges = [];
    
    for(var i=0;i<tile_src_images.length;i++) {
        var actual_width = tile_src_images[i][0].naturalWidth;
        var actual_height = tile_src_images[i][0].naturalHeight;
        
        width_ranges.push(Math.ceil(actual_width / tile_size));
        height_ranges.push(Math.ceil(actual_height / tile_size));
    }
    
    for(var col=0; col<width_tiles; col++) {
        for(var row=0; row<height_tiles; row++) {
            var src = getRandomNumber(0, tile_src_images.length);
            
            var x_offset = getRandomNumber(-7, 7);
            var y_offset = getRandomNumber(-7, 7);
            
            // source tile coordinates
            var sx_tile = col + x_offset; //getRandomNumber(Math.floor(0.20 * width_ranges[src]), Math.floor(0.80 * width_ranges[src]));
            var sy_tile = row + y_offset; //getRandomNumber(Math.floor(0.20 * height_ranges[src]), Math.floor(0.80 * height_ranges[src]));
            
            // draw the tile into the image
            cv.ctx.drawImage(
                tile_src_images[src][0],
                sx_tile * tile_size, sy_tile * tile_size, // source pixel coords
                tile_size, tile_size, // source rect size
                col * tile_size, row * tile_size, // dest pixel coords
                tile_size, tile_size, // dest rect size
            );
        }
    }
    
    cv.ctx.restore();
}

monika.waver_filter = function(cv, start_y, affected_height) {
    /* Divide the image into horizontal slices and offset them randomly */
    cv.ctx.save();

    if(!start_y) {
        start_y = 0;
    }

    if(!affected_height) {
        affected_height = cv.canvas.height;
    }

    // create a copy of the image data to use for offsetting:
    var original = cv.ctx.getImageData(0, 0, cv.canvas.width, cv.canvas.height);
    var cp = cv.ctx.createImageData(original);
    cp.data.set(original.data);

    // divide image into random slices
    var divisions = random_divide(getRandomNumber(7, 15), affected_height);

    // draw slices from the copy onto the destination canvas w/ random offset:
    var current_y = start_y;
    for(var i=0;i<divisions.length;i++) {
        var random_offset = getRandomNumber(0, Math.floor(cp.width/5));
        if(Math.random() < 0.5) {
            random_offset *= -1;
        }

        // clear destination canvas segment:
        cv.ctx.clearRect(0, current_y, cp.width, divisions[i]);

        cv.ctx.putImageData(cp, random_offset, 0, 0, current_y, cp.width, divisions[i]);

        current_y += divisions[i];
    }

    cv.ctx.restore();
}

monika.channel_split_filter = function(cv, start_y, affected_height) {
    cv.ctx.save();

    /* Randomly offset one channel in each strip */

    if(!start_y) {
        start_y = 0;
    }

    if(!affected_height) {
        affected_height = cv.canvas.height;
    }

    // divide image into random slices
    var divisions = random_divide(getRandomNumber(15, 25), affected_height);

    var original = cv.ctx.getImageData(0, 0, cv.canvas.width, cv.canvas.height);
    var outData = cv.ctx.createImageData(cv.canvas.width, cv.canvas.height);

    /* Copy over unaffected areas */
    outData.data.set(original.data);

    // draw slices from random channels onto the destination canvas w/ random offset:
    var current_y = start_y;
    for(var i=0;i<divisions.length;i++) {
        var random_x_offset = getRandomNumber(0, Math.floor(cv.canvas.width*0.25));
        if(Math.random() < 0.5) {
            random_x_offset *= -1;
        }

        var random_y_offset = getRandomNumber(0, Math.floor(cv.canvas.height*0.01));
        if(Math.random() < 0.5) {
            random_y_offset *= -1;
        }

        /* Randomly select a channel */
        var src_selector = Math.random();

        for(var row = 0; row < divisions[i]; row++) {
            var offset_row = row + random_y_offset + current_y;

            for(var col=0; col < cv.canvas.width; col++) {
                var offset_col = col + random_x_offset;

                var valid_offset = offset_col > 0 && offset_col < cv.canvas.width && offset_row > 0 && offset_row < cv.canvas.height;

                var pixel_idx = (((row+current_y) * cv.canvas.width) + col) * 4;
                var offset_idx = (((offset_row) * cv.canvas.width) + offset_col) * 4;

                if(src_selector < 0.33) {
                    // offset red channel

                    outData.data[pixel_idx+1] = original.data[pixel_idx+1]
                    outData.data[pixel_idx+2] = original.data[pixel_idx+2]

                    if(valid_offset) {
                        outData.data[offset_idx+0] = original.data[pixel_idx+0]
                        outData.data[offset_idx+3] = Math.max(original.data[pixel_idx+3], original.data[offset_idx+3])
                    }
                } else if(src_selector < 0.66) {
                    // offset green channel

                    outData.data[pixel_idx+0] = original.data[pixel_idx+0]
                    outData.data[pixel_idx+2] = original.data[pixel_idx+2]

                    if(valid_offset) {
                        outData.data[offset_idx+1] = original.data[pixel_idx+1]
                        outData.data[offset_idx+3] = Math.max(original.data[pixel_idx+3], original.data[offset_idx+3])
                    }
                } else {
                    // offset blue channel

                    outData.data[pixel_idx+0] = original.data[pixel_idx+0]
                    outData.data[pixel_idx+1] = original.data[pixel_idx+1]

                    if(valid_offset) {
                        outData.data[offset_idx+2] = original.data[pixel_idx+2]
                        outData.data[offset_idx+3] = Math.max(original.data[pixel_idx+3], original.data[offset_idx+3])
                    }
                }

                outData.data[pixel_idx+3] = original.data[pixel_idx+3]
            }
        }

        current_y += divisions[i];
    }

    cv.ctx.putImageData(outData, 0, 0);

    cv.ctx.restore();
}

monika.color_bars_filter = function(cv) {
    /* Apply randomly-colored overlays to horizontal strips of the image */
    cv.ctx.save();

    cv.ctx.globalCompositeOperation = 'source-atop';

    var divisions = random_divide(getRandomNumber(10, 20), cv.canvas.height);
    var current_y = 0;

    for(var i=0;i<divisions.length;i++) {
        if(Math.random() >= 0.4) { // don't colorize 40% of the strips
            // generate a random fill color:
            var hue = getRandomNumber(0, 360);
            cv.ctx.fillStyle = "hsl("+hue.toString()+", 100%, 50%)";

            // randomize alpha within 25%-50% range
            cv.ctx.globalAlpha = (Math.random() * 0.25) + 0.25;

            // draw the strip:
            cv.ctx.fillRect(0, current_y, cv.canvas.width, divisions[i]);

            current_y += divisions[i];
        }
    }

    cv.ctx.restore();
}
