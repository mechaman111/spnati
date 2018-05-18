/* NOTE: This file must be accessable from the root of the domain! */

const CACHE_NAME = 'SPNATI-v1';
const CACHE_KEEPALIVE = 3600; // refetch cached content older than this many seconds, if online

const URLS_PER_PRELOAD_BATCH = 100;      // How many URLs to preload in an interval
const PRELOAD_BATCH_INTERVAL = 2000;    // How often to pull URLs from the preload queue

/* This list holds the URLs of resources that should always be cached
 * All URLs in this list and in `static_content` will be cached when the
 * ServiceWorker is installed (i.e. when SPNATI is loaded for the first time)
 * This _will_ block load until all resources have been loaded.
 */
const static_content = [
    'js/bootstrap.min.js',
    'js/jquery-1.11.3.min.js',
    'js/js.cookie.js',
    'js/player.js',
    'js/save.js',
    'js/spniAI.js',
    'js/spniBehaviour.js',
    'js/spniClothing.js',
    'js/spniCore.js',
    'js/spniEpilogue.js',
    'js/spniForfeit.js',
    'js/spniGallery.js',
    'js/spniGame.js',
    'js/spniOption.js',
    'js/spniPoker.js',
    'js/spniSelect.js',
    'js/spniTitle.js',
    'js/svc_worker.js',
    'js/table.js',
    'fonts/glyphicons-halflings-regular.eot',
    'fonts/glyphicons-halflings-regular.svg',
    'fonts/glyphicons-halflings-regular.ttf',
    'fonts/glyphicons-halflings-regular.woff',
    'fonts/glyphicons-halflings-regular.woff2',
    'css/spni.css',
    'css/bootstrap.min.css',
    'css/bootstrap-theme.css',
    'css/spni.css',
];

/* When debugging is active, we always fetch and recache non-image data. */
var debug_active = false;

/* Set this to true to enable detailed logging of request handling. */
var verbose = false;

var current_preload_queue = [];

/* Makes a mutable clone of immutable headers
 * (for example, those from event Requests or network Responses) */
function clone_immutable_headers(headers) {
    var new_headers = new Headers();
    for(var kv of headers.entries()) {
        new_headers.append(kv[0], kv[1]);
    }

    return new_headers
}

self.addEventListener('fetch', function(event) {
    /* Don't bother caching non-GET requests */
    if(event.request.method !== 'GET') {
        return event.respondWith(fetch(event.request));
    }

    /* Ensure we can quickly reload scripts and etc. when developing;
     * Cache only images when debugging is enabled.
     */
    if(debug_active) {
        let file_ext = event.request.url.split('.').pop();
        if(file_ext !== 'png' && file_ext !== 'jpg' && file_ext !== 'svg') {
            return event.respondWith(
                fetch(event.request).then(
                    async function (net_response) {
                        var cache = await caches.open(CACHE_NAME);
                        cache.put(event.request, net_response.clone());

                        return net_response;
                    }
                )
            );
        }
    }


    /* Look for content in the cache first.
     * If we don't find it, or if the cached version is too old, attempt to
     *  retrieve it from the network, and cache the retrieved version.
     * Otherwise, return the cached version.
     */
    event.respondWith(
        caches.open(CACHE_NAME).then(
            async function(cache) {
                var cached_response = await cache.match(event.request);

                if(cached_response) {
                    var resp_time = new Date(cached_response.headers.get('Date'));
                    var current_cache_age = Date.now() - resp_time.getTime(); // in milliseconds

                    if(debug_active && verbose) console.log("[SW] Cache age of "+event.request.url+": "+(current_cache_age/1000).toPrecision(3).toString()+" seconds");

                    if(current_cache_age < CACHE_KEEPALIVE * 1000) {
                        /* We have fresh content cached. Return it. */
                        if(debug_active && verbose) console.log("[SW] Cache age of "+event.request.url+": "+(current_cache_age/1000).toPrecision(3).toString()+" seconds");
                        return cached_response;
                    } else if(debug_active && verbose) {
                        console.log("[SW] Refreshing stale file: "+event.request.url);
                    }
                }

                /* Otherwise, get it from the network, cache a copy, and return it. */

                /* When we make the network request, also make sure to set the `If-Modified-Since` header if we can */
                if(cached_response) {
                    /* We can't modify event.request directly-- copy it */
                    var new_headers = clone_immutable_headers(event.request.headers);
                    new_headers.set('If-Modified-Since', cached_response.headers.get('Date'));

                    var new_request = new Request(event.request.url, {
                        method: event.request.method,
                        headers: new_headers,
                        mode: 'same-origin',
                        credentials: event.request.credentials
                    });

                    var net_response = await fetch(new_request);
                    if(net_response.ok) {
                        /* Content was modified on the server, re-cache the response data and return it */
                        cache.put(event.request, net_response.clone());
                        return net_response;
                    } else if(net_response.status == 304) {
                        // Update the Date on the cached response and return it
                        if(debug_active && verbose) console.log("[SW] Got 304 Not Modified response for "+event.request.url+", updating cache date");

                        var new_response_headers = clone_immutable_headers(cached_response.headers);
                        new_response_headers.set('Date', net_response.headers.get('Date'));

                        var new_response = new Response(
                            cached_response.body,
                            {
                                status: cached_response.status,
                                statusText: cached_response.statusText,
                                headers: new_response_headers
                            }
                        );

                        cache.put(event.request, new_response.clone());
                        return new_response;
                    } else {
                        throw Error("Network request returned with error "+net_response.status.toString()+' '+net_response.statusText);
                    }
                } else {
                    var net_response = await fetch(event.request);

                    if(net_response.ok) {
                        var cloned_response = net_response.clone();
                        if('blob' in cloned_response) {
                            /* If we can, verify that the response actually has body data associated with it
                             * Sometimes we get zero-length responses on Firefox, but not on Chrome.
                             * I don't know why that happens.
                             */
                            var data = await cloned_response.blob();
                            var expected_content_length = parseInt(cloned_response.headers.get('Content-Length'), 10);
                            if(data.size === expected_content_length) {
                                if(debug_active && verbose) console.log("[SW] Verified response content length ("+cloned_response.headers.get('Content-Length')+" bytes)");
                                cache.put(event.request, new Response(
                                    data, {
                                        status: cloned_response.status,
                                        statusText: cloned_response.statusText,
                                        headers: cloned_response.headers
                                    }
                                ));
                            } else {
                                console.error("[SW] Got invalid response for "+event.request.url+": expected "+cloned_response.headers.get('Content-Length')+" bytes, got "+data.size.toString()+" bytes instead");
                                return Response.error();
                            }
                        } else {
                            if(debug_active && verbose) console.log("[SW] Not verifying response data-- cannot use Response.blob");
                            cache.put(event.request, cloned_response);
                        }
                    }

                    return net_response;
                }
            }
        ).catch(function (err) {
            console.error("Caught error when responding to request: "+err.toString());
            console.error(err.stack);
        })
    );
});

/* Fires on install (i.e. when there's a new version of the SW or when first visiting the page) */
self.addEventListener('install', function(event) {
    event.waitUntil(
        caches.open(CACHE_NAME).then(function(cache) {
            /* Map each url to its own cache.add call so that one failed request doesn't invalidate the entire thing */
            return Promise.all(static_content.map(
                (url) => cache.add(url)
            )).then(
                () => true,
                (err) => console.error("Error while caching static content: "+err.toString()+'\n'+err.stack),
            )
        }).then(function () {
            /* Make sure we're active immediately */
            return self.skipWaiting();
        })
    );
});

self.addEventListener('activate', function (event) {
    /* Force SW to become available to all clients-- this ensures serviceWorker.controller is available to everyone */
    return event.waitUntil(self.clients.claim());
})

function batch_preload(urls) {
    if(debug_active && verbose) console.log("[SW] Beginning batch preload for "+urls.length.toString()+" URLs");
    return caches.open(CACHE_NAME).then(
        function (cache) {
            return Promise.all(urls.map(
                (url) => cache.add(url)
            )).then(
                () => true,
                (err) => console.error("Error while preloading content: "+err.toString()+'\n'+err.stack),
            )
        }
    ).then(
        function () {
            if(debug_active && verbose) console.log("[SW] Batch preload complete.");
        },
        function (err) {
            console.error("[SW] Batch preload failed: "+err.toString());
        }
    )
}

/* Called periodically, to avoid overloading browsers with 100s of preload requests. */
function process_preload_queue() {
    if(current_preload_queue.length > 0) {
        /* pop some elements off the front of the preload queue */
        var urls = current_preload_queue.splice(0, URLS_PER_PRELOAD_BATCH);

        /* Begin preloading this batch */
        batch_preload(urls);

        if(debug_active && verbose) console.log("[SW] "+current_preload_queue.length.toString()+" URLs left to preload");
    }
}

setInterval(process_preload_queue, PRELOAD_BATCH_INTERVAL);

self.addEventListener('message', function(event) {
    var msg = event.data;

    if (msg.type === 'cache') {
        if(debug_active && verbose) console.log("[SW] Scheduling preload for "+msg.urls.length.toString()+" URLs");
        current_preload_queue.push.apply(current_preload_queue, msg.urls);
    } else if(msg.type === 'set-debug') {
        debug_active = msg.debug;

        if(debug_active) {
            console.log("[SW] Debugging enabled -- bypassing cache for non-image files");
        }
    } else if(msg.type === 'set-verbose') {
        verbose = msg.verbose;

        if(verbose) {
            console.log("[SW] Verbose logging of request handling enabled");
        }
    }
});
