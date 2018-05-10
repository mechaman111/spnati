if ('serviceWorker' in navigator) {
  window.addEventListener('load', function() {
    navigator.serviceWorker.register('/service_worker.js').then(function(registration) {
      // Registration was successful
      console.log('ServiceWorker registration successful with scope: ', registration.scope);
    }, function(err) {
      // registration failed
      console.log('ServiceWorker registration failed: ', err);
    });
  });
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
