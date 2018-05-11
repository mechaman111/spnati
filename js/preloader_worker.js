/* Preloader Worker script.
 * Loads files in the background and caches them.
 * Send it lists of URLs to preload.
 */

const CACHE_NAME = 'SPNATI-v1'; // this isn't shared between this context and the ServiceWorker context...
const CACHE_KEEPALIVE = 600;

onmessage = function (event) {
    console.log("[PL] Preloading "+event.data.length.toString()+" URLs...");
    caches.open(CACHE_NAME).then(
        function (cache) {
            return Promise.all(event.data.map(async function (url) {
                var cached_response = await cache.match(url);

                if(cached_response) {
                    var resp_time = new Date(cached_response.headers.get('Date'));
                    var current_cache_age = resp_time.getTime() - Date.now(); // in milliseconds

                    if(current_cache_age < CACHE_KEEPALIVE * 1000) {
                        /* We already have fresh content cached.
                         * Don't fetch it again. */
                        return;
                    }
                }

                /* Otherwise, go and fetch it. */
                var resp = await fetch("../"+url, { headers: { 'X-Worker-Initiated': 'true' } });
                return cache.put(url, resp);
            }));
        }
    ).then(
        function () {
            console.log("[PL] Preload successful.");
        },
        function (err) {
            console.log("[PL] Preload failed: "+err.toString());
        }
    );
}
