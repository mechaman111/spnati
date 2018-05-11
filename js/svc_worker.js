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
        for(let queued_request of preload_queue) {
            send_msg_to_sw({ 'type': 'cache', 'urls': queued_request.urls }).then(
                (v) => queued_request.resolve(v),
                (err) => queued_request.reject(err),
            );
        }

        /* Also set the debug status. */
        set_sw_debug(DEBUG);
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

function sw_is_active() {
    /* check to see if we can load SWs in the first place
     * then make sure that the Controller object is not null/undefined/etc.
     */
    return ('serviceWorker' in navigator) && (navigator.serviceWorker.controller != null);
}

function send_msg_to_sw(msg) {
    return new Promise(
        function(resolve, reject) {
            var reply_channel = new MessageChannel();

            reply_channel.port1.onmessage = function(ev) {
                if(ev.data.error) {
                    return reject(ev.data.error);
                } else {
                    return resolve(ev.data);
                }
            }

            navigator.serviceWorker.controller.postMessage(msg, [reply_channel.port2]);
        }
    );
}

function request_url_caching(urls) {
    if(!sw_is_active()) {
        return new Promise(
            function (resolve, reject) {
                preload_queue.push({
                    'resolve': resolve,
                    'reject': reject,
                    'urls': urls.slice(),
                });
            }
        );
    } else {
        return send_msg_to_sw({ 'type': 'cache', 'urls': urls });
    }

}

/* Set debug mode true/false in the ServiceWorker */
function set_sw_debug(debug_status) {
    return send_msg_to_sw({ 'type': 'set-debug', 'debug': debug_status}).then(function (ok) {
        if(!ok) {
            console.error("Attempt to set ServiceWorker debug status failed.")
        }

        return ok;
    });
}
