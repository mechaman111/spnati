/* Preloader Worker script.
 * Loads files in the background and caches them.
 * Send it lists of URLs to preload.
 */

const CACHE_NAME = 'SPNATI-v1'; // this isn't shared between this context and the ServiceWorker context...

onmessage = function (event) {
    console.log("[PL] Preloading "+event.data.length.toString()+" URLs...");
    caches.open(CACHE_NAME).then(
        function (cache) {
            return Promise.all(event.data.map(async function (url) {
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
