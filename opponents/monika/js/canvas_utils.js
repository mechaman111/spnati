monika.get_canvas_async = function (jquery_elem, callback, keep_empty) {
    var img = new Image();
    var src = jquery_elem.attr('src');
    
    img.onload = function () {
        var canvas = document.createElement('canvas');
        
        canvas.width  = img.naturalWidth;
        canvas.height = img.naturalHeight;
        
        var ctx = canvas.getContext('2d');
        
        if (!keep_empty) {
            ctx.drawImage(img, 0, 0);
        }
        
        var cv = {canvas, ctx, 'original': src, 'elem': jquery_elem};
        cv.undo = function() { monika.restore_image(cv); }
        
        callback(cv);
    }
    
    img.src = src;
}

monika.set_image_from_canvas = function(jquery_elem, cv) {
    cv.data_url = cv.canvas.toDataURL();
    jquery_elem[0].src = cv.data_url;
}

monika.restore_image = function(cv) {
    var jquery_elem = cv.elem;
    if(cv.elem && cv.data_url && cv.original) {
        // the element's displayed image might have changed in the meantime (game advancing, pose changes, etc.)
        // don't mess with images that weren't set by us.
        if(jquery_elem[0].src === cv.data_url) {
            jquery_elem[0].src = cv.original;
            cv.data_url = undefined;
            return true;
        }

        cv.data_url = undefined;
        return false;
    }
}

monika.split_pose_filename = function(src) {
    var splitIdx = src.lastIndexOf('/')+1;
    var base = src.substring(0, splitIdx);
    var filename = src.substring(splitIdx);
    
    // Won't work with weird filenames, but it'll work for most cases.
    // Just gotta be careful...
    var stem = filename.substring(0, filename.lastIndexOf('.'));
    var ext = filename.substring(filename.lastIndexOf('.')+1);
    
    var sp1 = stem.split('-', 2);
    var stage = undefined;
    var pose = stem;
    if (sp1.length === 2) {
        stage = parseInt(sp1[0], 10);
        pose = sp1[1];
    }
    
    return {
        'base': base,
        'stage': stage,
        'pose': pose,
        'ext': ext
    }
}

monika.assemble_pose_filename = function(pose) {
    return pose.base+pose.stage+'-'+pose.pose+'.'+pose.ext;
}
