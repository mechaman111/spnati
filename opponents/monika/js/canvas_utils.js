monika.get_canvas = function(jquery_elem) {
    var canvas = document.createElement('canvas');
    var dom_elem = jquery_elem[0];

    canvas.width = dom_elem.naturalWidth;
    canvas.height = dom_elem.naturalHeight;

    var ctx = canvas.getContext('2d');
    ctx.drawImage(dom_elem, 0, 0);

    return {canvas, ctx, 'original': jquery_elem.attr('src'), 'elem': jquery_elem};
}

monika.get_empty_canvas = function(jquery_elem) {
    var canvas = document.createElement('canvas');
    var dom_elem = jquery_elem[0];

    canvas.width = dom_elem.naturalWidth;
    canvas.height = dom_elem.naturalHeight;

    var ctx = canvas.getContext('2d');

    return {canvas, ctx, 'original': jquery_elem.attr('src'), 'elem': jquery_elem};
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
