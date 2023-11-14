
var CACHE_NAME = 'my-site-cache-v1';
var urlsToCache = [
    '/',
    '/productos/productos/galeria',
    // css
    '/static/base/vendor/bootstrap/css/bootstrap.min.css',
    '/static/base/css/one-page-wonder.min.css',
    //js
    '/static/base/vendor/jquery/jquery.min.js',
    '/static/base/vendor/bootstrap/js/bootstrap.bundle.min.js',
    //img
    '/static/base/img/girasol.jpg',
    '/static/base/img/hortensias.jpg',
    '/static/base/img/rosas.jpg',
];

self.addEventListener('install', function(event) {
  // Perform install steps
  event.waitUntil(
    caches.open(CACHE_NAME)
      .then(function(cache) {
        console.log('Opened cache');
        return cache.addAll(urlsToCache);
      })
  );
});

self.addEventListener('fetch', function(event) {
    event.respondWith(
      fetch(event.request)
      .then((result)=>{
        return caches.open(CACHE_NAME)
        .then(function(c) {
          c.put(event.request.url, result.clone())
          return result;
        })
      })
      .catch(function(e){
          return caches.match(event.request)
      })

    );
});

//codigo para notificaciones push

importScripts('https://www.gstatic.com/firebasejs/3.9.0/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/3.9.0/firebase-messaging.js');

var firebaseConfig = {
  apiKey: "AIzaSyBlUeIj5TIs2gz836HBSFr0R_ZOuVGZdbo",
  authDomain: "floreria-558e9.firebaseapp.com",
  databaseURL: "https://floreria-558e9.firebaseio.com",
  projectId: "floreria-558e9",
  storageBucket: "floreria-558e9.appspot.com",
  messagingSenderId: "381627206105",
  appId: "1:381627206105:web:c96c61b386c052b2e7f1c8"
};
// Initialize Firebase
firebase.initializeApp(firebaseConfig);

let messaging = firebase.messaging();

messaging.setBackgroundMessageHandler(function(payload) {

  let title = 'titulo de la notificacion';
  
    let options = {
      body:'este es el mensaje',
      icon:'/static/base/img/hortensias.jpg'
    }

    self.registration.showNotification(title, options);
});
