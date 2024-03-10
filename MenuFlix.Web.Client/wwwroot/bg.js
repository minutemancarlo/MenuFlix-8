const checkPermission = () => {
    if (!('serviceWorker' in navigator)) {
        throw new Error("Browser is not supported service worker!")
    }
}

const registerSW = async () => {
    const registration = await navigator.serviceWorker.register('service-worker.js');
    return registration;
}

const requestNotificationPermission = async () => {
    const permission = await Notification.requestPermission();

    if (permission !== 'granted') {
        throw new Error("Notification permission not granted")
    }
}

checkPermission()
registerSW()
requestNotificationPermission()

//let handler;

//window.Connection = {
//    Initialize: function (interop) {

//        handler = function () {
//            interop.invokeMethodAsync("Connection.StatusChanged", navigator.onLine);
//        }

//        window.addEventListener("online", handler);
//        window.addEventListener("offline", handler);

//        handler(navigator.onLine);
//    },
//    Dispose: function () {

//        if (handler != null) {

//            window.removeEventListener("online", handler);
//            window.removeEventListener("offline", handler);
//        }
//    }
};
