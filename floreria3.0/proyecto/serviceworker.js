var CACHE_NAME = 'my-site-cache-v1';
var urlsToCache = [
    '/',
    '/productos/productos/galeria',
    '/login',
    '/productos/productos',
    '/logout',
    // css
    '/static/base/vendor/bootstrap/css/bootstrap.min.css',
    '/static/base/css/one-page-wonder.min.css',
    'https://fonts.googleapis.com/css?family=Catamaran:100,200,300,400,500,600,700,800,900',
    'https://fonts.googleapis.com/css?family=Lato:100,100i,300,300i,400,400i,700,700i,900,900i',
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
        console.log('Cache activado, Javier');
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
    apiKey: "AIzaSyAeJnFtw_CqFkH5ZbY_0tpizeVtVRO9pls",
    authDomain: "floreria-82351.firebaseapp.com",
    databaseURL: "https://floreria-82351.firebaseio.com",
    projectId: "floreria-82351",
    storageBucket: "floreria-82351.appspot.com",
    messagingSenderId: "297973945218",
    appId: "1:297973945218:web:e8abae1f3135b5461fb59b"
  };
  // Initialize Firebase
  firebase.initializeApp(firebaseConfig);

  let messaging = firebase.messaging();

messaging.setBackgroundMessageHandler(function (payload) {
      console.log("Ha llegado notificacion");

      let title = 'titulo de la notificacion';

      let options = {
          body:'Este es el mensaje',
          icon:'/static/base/img/rosas.jpg'
      }

    self.registration.showNotification(title, options);

});
