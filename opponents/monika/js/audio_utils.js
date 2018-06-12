monika.audio_available = function() {
    if(window.AudioContext || window.webkitAudioContext) {
        return true;
    }
    return false;
}

monika.get_audio_context = function() {
    if(monika.audio_available()) {
        return new (window.AudioContext || window.webkitAudioContext)();
    }
    
    return undefined;
}

/* create an empty audio buffer with length `dur` (seconds) */
monika.get_empty_buffer = function(ctx, dur) {
    var frameCount = ctx.sampleRate * dur;
    return ctx.createBuffer(2, frameCount, ctx.sampleRate);
}

/* generate white noise in with values from [-range, +range] */
monika.generate_white_noise = function(ctx, dur, range) {
    var frameCount = ctx.sampleRate * dur;
    var buf = ctx.createBuffer(2, frameCount, ctx.sampleRate);
    
    for(var ch=0;ch<2;ch++) {
        var ch_data = buf.getChannelData(ch);
        for(var i=0;i<frameCount;i++) {
            ch_data[i] = (Math.random() * 2 * range) - range;
        }
    }
    
    return buf;
}

monika.play_buffer = function (ctx, buf) {
    var src = ctx.createBufferSource();
    src.buffer = buf;
    src.connect(ctx.destination);
    src.start();
}

/* End-effect code. Fails silently, in both senses of the term. */
monika.play_noise = function(dur, range) {
    try {
        if(monika.audio_available()) {
            var ctx = monika.get_audio_context()
            var buf = monika.generate_white_noise(ctx, dur, range);
            monika.play_buffer(ctx, buf);
            
            return true;
        }
    } catch (e) {
        console.error("[Monika] error when playing noise: "+e.toString());
    }
    
    
    return false;
}
