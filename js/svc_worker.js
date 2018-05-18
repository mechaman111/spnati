var preload_queue = []; /* Used to store preload requests before the SW becomes available */

/* Register the SW as soon as possible so we can take advantage of the cache as much as we can */
if ('serviceWorker' in navigator) {
    navigator.serviceWorker.register('/service_worker.js').then(function(registration) {
      // Registration was successful
      console.log('ServiceWorker registration successful with scope: ', registration.scope);
    }, function(err) {
      // registration failed
      console.log('ServiceWorker registration failed: ', err);
    });

    navigator.serviceWorker.addEventListener('controllerchange', function () {
        /* Now that we can send messages to the SW, start preloading queued URLs. */
        console.log("Sending "+preload_queue.length.toString()+" queued preload requests to SW...");
        for(var i=0;i<preload_queue.length;i++) {
            var queued_request = preload_queue[i];
            send_msg_to_sw({ 'type': 'cache', 'urls': queued_request });
        }

        /* Also set the debug status. */
        set_sw_debug(DEBUG);
        set_sw_verbose(DEBUG);
    });

    /* Array of images to cache in the background */
    const static_images = [
        'img/all.png',
        'img/any.png',
        'img/bisexual.jpg',
        'img/blankcard.jpg',
        'img/enter.png',
        'img/female.png',
        'img/female_large.png',
        'img/female_medium.png',
        'img/female_small.png',
        'img/gallery.svg',
        'img/icon.ico',
        'img/icon.jpg',
        'img/male.png',
        'img/male_large.png',
        'img/male_medium.png',
        'img/male_small.png',
        'img/reddit.png',
        'img/title.png',
        'img/unknown_s.jpg',
        'img/unknown.jpg',
        'img/unknown.svg',
    ];

    /* autogenerate lists of cards to save */
    var cards = [];
    for(suit of ['spade', 'heart', 'clubs', 'diamo']) { // filename prefixes
        cards.push('img/'+suit+'.jpg');
        for(let i=1;i<=13;i++) {
            cards.push('img/'+suit+i.toString()+'.jpg');
        }
    }
    request_url_caching(cards.concat(static_images));
}

/************************************************************
 * Test if Service Worker features are available.
 *
 * If this is true, then request_url_caching()-- if the
 * SW is unavailable at call time (i.e. if it hasn't been loaded yet)
 * then the caching request will be queued until the SW is available.
 *************************************************************/
function sw_is_available() {
    return ('serviceWorker' in navigator);
}


/************************************************************
 * Test if the Service Worker is active for this page.
 *
 * This probably will not be the case until a bit after the
 * Service Worker is loaded and active-- if you're preloading
 * URLs, you might want to use `sw_is_available()` instead,
 * since we can queue URLs to be preloaded even before the
 * Service Worker is available.
 ************************************************************/
function sw_is_active() {
    /* check to see if we can load SWs in the first place
     * then make sure that the Controller object is not null/undefined/etc.
     */
    return ('serviceWorker' in navigator) && (navigator.serviceWorker.controller != null);
}
/************************************************************
 * Sends a message to the service worker.
 * See the 'message' event listener in service_worker.js
 * for more details.
 *
 * Messages should be objects with 'type' properties at the
 * very least, and possibly other properties depending on the
 * message type.
 ************************************************************/
function send_msg_to_sw(msg) {
    navigator.serviceWorker.controller.postMessage(msg);
}

/************************************************************
 * Schedule a list of URLs to be preloaded and cached in the background.
 *
 * Use this function when you expect to be loading a lot of heavy files
 * later on; for example, this is called when a character is selected
 * (to preload said character's gameplay images).
 *
 * Preloading is done in the background-- it shouldn't block the main flow
 * of gameplay (rendering, etc.) but there may be a few pauses / lag-spikes
 * when beginning to load lots of heavy files.
 ************************************************************/
function request_url_caching(urls) {
    if(!sw_is_active()) {
        preload_queue.push(urls.slice());
    } else {
        return send_msg_to_sw({ 'type': 'cache', 'urls': urls });
    }

}


/************************************************************
 * Turn debug mode on and off in the Service Worker.
 * When debug mode is enabled, all non-image content (scripts, dialogue, etc.) will be
 * fetched immediately instead of using the cache, regardless of cache age.
 *
 * Note that the browser may still decide to use its own disk caching system
 * regardless of the debug mode setting!
 ************************************************************/
function set_sw_debug(debug_status) {
    return send_msg_to_sw({ 'type': 'set-debug', 'debug': debug_status}).then(function (ok) {
        if(!ok) {
            console.error("Attempt to set ServiceWorker debug status failed.")
        }

        return ok;
    });
}

/************************************************************
 * Turn verbose request logging on and off in the Service Worker.
 ************************************************************/
function set_sw_verbose(verbose_status) {
    return send_msg_to_sw({ 'type': 'set-verbose', 'verbose': verbose_status}).then(function (ok) {
        if(!ok) {
            console.error("Attempt to set ServiceWorker verbose status failed.")
        }

        return ok;
    });
}
