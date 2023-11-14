from django.urls import path, include

from productos.views import ProductoView, ProductoNew, ProductoEdit, ProductoDel, Galeria, producto_print

#Imports DRF / enrutadores
from .views import ProductoViewSet, guardar_token
from rest_framework import routers

router = routers.DefaultRouter()
router.register('productos', ProductoViewSet)

urlpatterns = [
    path('productos', ProductoView.as_view(), name='producto_list'),
    path('productos/new', ProductoNew.as_view(), name='producto_new'),
    path('productos/edit/<int:pk>', ProductoEdit.as_view(), name='producto_edit'),
    path('productos/delete/<int:pk>', ProductoDel.as_view(), name='producto_delete'),
    path('productos/galeria', Galeria.as_view(), name='galeria'),
    path('productos/print', producto_print, name='producto_print'),
    path('productos/print/<int:pk>', producto_print, name='producto_print_one'),

    path('api/', include(router.urls)), #url/productos/api
    path('guardar-token/', guardar_token, name='guardar_token'),
]