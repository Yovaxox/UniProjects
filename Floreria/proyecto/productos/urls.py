from django.urls import path

from productos.views import ProductoView, ProductoNew, ProductoEdit, ProductoDel, Galeria

urlpatterns = [
    path('productos', ProductoView.as_view(), name='producto_list'),
    path('productos/new', ProductoNew.as_view(), name='producto_new'),
    path('productos/edit/<int:pk>', ProductoEdit.as_view(), name='producto_edit'),
    path('productos/delete/<int:pk>', ProductoDel.as_view(), name='producto_delete'),
    path('productos/galeria', Galeria.as_view(), name='galeria'),
]