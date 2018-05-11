/* NOTE: This file must be accessable from the root of the domain! */

const CACHE_NAME = 'SPNATI-v1';
const CACHE_KEEPALIVE = 600; // refetch cached content older than this many seconds, if online

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
                    var current_cache_age = resp_time.getTime() - Date.now(); // in milliseconds

                    if(current_cache_age < CACHE_KEEPALIVE * 1000) {
                        /* We have fresh content cached. Return it. */
                        return cached_response;
                    }
                }

                /* Otherwise, get it from the network, cache a copy, and return it. */
                var net_response = await fetch(event.request);
                cache.put(event.request, net_response.clone());
                return net_response;
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
            return cache.addAll(static_content);
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

self.addEventListener('message', function(event) {
    var msg = event.data;

    if (msg.type === 'cache') {
        if(debug_active) console.log("[SW] Preloading "+msg.urls.length.toString()+" URLs");

        event.waitUntil(
            caches.open(CACHE_NAME).then(
                (cache) => cache.addAll(msg.urls)
            ).then(
                function () {
                    if(debug_active) console.log("[SW] Preload successful.");
                    event.ports[0].postMessage(true);
                },
                function (err) {
                    console.error("[SW] Preload failed: "+err.toString());
                    event.ports[0].postMessage(false);
                }
            )
        );
    } else if(msg.type === 'set-debug') {
        debug_active = msg.debug;

        if(debug_active) {
            console.log("[SW] Debugging enabled -- bypassing cache for non-image files");
        }
    }
});
