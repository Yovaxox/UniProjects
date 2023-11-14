from django.shortcuts import render
from django.urls import reverse_lazy
from django.views import generic
from django.contrib.messages.views import SuccessMessageMixin
from django.contrib.auth.mixins import LoginRequiredMixin, PermissionRequiredMixin

from productos.models import Producto
from productos.forms import ProductoForm
from generales.views import SinPrivilegios

# Create your views here.

class ProductoView(LoginRequiredMixin,SinPrivilegios,generic.ListView):
    permission_required = "producto.view_producto"
    model = Producto
    template_name = "productos/producto_list.html"
    context_object_name = "obj"
    login_url = 'generales:login'

class ProductoNew(SuccessMessageMixin,LoginRequiredMixin,SinPrivilegios,generic.CreateView):
    permission_required = "producto.add_producto"
    model = Producto
    template_name = "productos/producto_form.html"
    context_object_name = 'obj'
    form_class = ProductoForm
    success_url = reverse_lazy("productos:producto_list")
    login_url = 'generales:login'
    success_message = "Producto creado correctamente"

class ProductoEdit(SuccessMessageMixin,LoginRequiredMixin,SinPrivilegios,generic.UpdateView):
    permission_required = "producto.change_producto"
    model = Producto
    template_name = "productos/producto_form.html"
    context_object_name = 'obj'
    form_class = ProductoForm
    success_url = reverse_lazy("productos:producto_list")
    login_url = 'generales:login'
    success_message = "Producto modificado correctamente"

class ProductoDel(LoginRequiredMixin,SinPrivilegios,generic.DeleteView):
    permission_required = "producto.delete_producto"
    model = Producto
    template_name = "productos/producto_del.html"
    context_object_name = 'obj'
    success_url = reverse_lazy("productos:producto_list")
    login_url = 'generales:login'

class Galeria(generic.ListView):
    model = Producto
    template_name = "productos/galeria.html"
    context_object_name = "obj"