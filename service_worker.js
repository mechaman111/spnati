/* NOTE: This file must be accessable from the root of the domain! */

const CACHE_NAME = 'SPNATI-v1';
const CACHE_KEEPALIVE = 3600; // refetch cached content older than this many seconds, if online

/* this list holds images that should always be cached
 * (player clothes, the logo, etc.)
 * We autogenerate the lists to cache cards and backgrounds.
 *
 * All URLs in this list and in `static_content` will be cached when the
 * ServiceWorker is installed (i.e. when SPNATI is loaded for the first time)
 */
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

/* this list holds other things that should always be cached
 * (JS, CSS, etc.) */
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

self.addEventListener('fetch', function(event) {
    /* Don't bother caching non-GET requests */
    if(event.request.method !== 'GET') {
        return fetch(event.request);
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
        )
    );
});

self.addEventListener('install', function(event) {
    event.waitUntil(
        caches.open(CACHE_NAME).then(function(cache) {
            /* autogenerate lists of cards to save */
            var cards = [];
            for(suit of ['clubs', 'diamo', 'heart', 'spade']) { // filename prefixes
                cards.push('img/'+suit+'.jpg');
                for(let i=1;i<=13;i++) {
                    cards.push('img/'+suit+i.toString()+'.jpg');
                }
            }

            var bgs = [];
            for(let bgIdx=1;bgIdx<=23;bgIdx++) {
                bgs.push('img/background'+bgIdx.toString()+'.png');
            }

            /* Combine all lists of stuff to cache on install */
            return cache.addAll(static_content.concat(static_images, cards, bgs));
        })
    );
});

self.addEventListener('message', function(event) {
    var msg = event.data;

    /* NOTE: I'm not sure if we'll ever need to handle multiple types of messages,
     * but I'll play it safe here... */
    if (msg.type === 'cache') {
        event.waitUntil(
            caches.open(CACHE_NAME).then(
                (cache) => cache.addAll(msg.urls)
            ).then(
                (value) => ev.ports[0].postMessage(true),
                (error) => ev.ports[0].postMessage(false)
            )
        );
    }
});
