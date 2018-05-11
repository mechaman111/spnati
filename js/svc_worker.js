var preloader_worker = null;

if ('serviceWorker' in navigator) {
  window.addEventListener('load', function() {
    navigator.serviceWorker.register('/service_worker.js').then(function(registration) {
      // Registration was successful
      console.log('ServiceWorker registration successful with scope: ', registration.scope);
    }, function(err) {
      // registration failed
      console.log('ServiceWorker registration failed: ', err);
    });

    if(sw_is_active()) {
        set_sw_debug(true);
    }
  });

  if(window.Worker) {
      console.log("Starting Preloader Worker...");
      preloader_worker = new Worker('js/preloader_worker.js');
  }
}

function sw_is_active() {
    /* check to see if we can load SWs in the first place
     * then make sure that the Controller object is not null/undefined/etc.
     */
    return ('serviceWorker' in navigator) && (navigator.serviceWorker.controller != null);
}

function preloader_active() {
    return preloader_worker != null;
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
    if(preloader_active()) {
        /* Preload resources in the background using the preloader worker if we can. */
        preloader_worker.postMessage(urls);
    } else {
        /* Otherwise fall back to having the ServiceWorker do the loading. */
        return send_msg_to_sw({ 'type': 'cache', 'urls': urls }).then(function (ok) {
            if(!ok) {
                console.error("Attempt to cache urls failed: "+urls.toString())
            }

            return ok;
        });
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
