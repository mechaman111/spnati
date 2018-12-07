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
    var re = /\/?opponents\/(.+?)\/(\d+?)\-(.+?)\.(\w+)/mi
    var match = re.exec(src);
    
    return {
        'opponent': match[1],
        'stage': match[2],
        'pose': match[3],
        'ext': match[4],
    }
}

monika.assemble_pose_filename = function(pose) {
    return "opponents/"+pose.opponent+"/"+pose.stage+'-'+pose.pose+'.'+pose.ext;
}
