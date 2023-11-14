from django.shortcuts import render
from django.urls import reverse_lazy
from django.views import generic
from django.contrib.messages.views import SuccessMessageMixin
from django.contrib.auth.mixins import LoginRequiredMixin, PermissionRequiredMixin

from productos.models import Producto
from productos.forms import ProductoForm
from generales.views import SinPrivilegios

#Rest Framework
from rest_framework import viewsets
from .serializers import ProductoSerializer

#PUSH
from django.views.decorators.http import require_http_methods
from django.views.decorators.csrf import csrf_exempt

from django.http import HttpResponse, HttpResponseBadRequest

from django.core import serializers
import json

from fcm_django.models import FCMDevice
# Create your views here.
@csrf_exempt
@require_http_methods(['POST'])
def guardar_token(request):
    body = request.body.decode('utf-8')
    bodyDict = json.loads(body)

    token = bodyDict['token']

    existe = FCMDevice.objects.filter(registration_id = token, active=True)

    if len(existe) > 0:
        return HttpResponseBadRequest(json.dumps({'mensaje':'el token ya existe'}))

    dispositivo = FCMDevice()
    dispositivo.registration_id = token
    dispositivo.active = True

    #solo si el usuario esta autenticado procederemos a enlazarlo
    if request.user.is_authenticated:
        dispositivo.user = request.user

    try:
        dispositivo.save()
        return HttpResponse(json.dumps({'mensaje':'token guardado'}))
    except:
        return HttpResponseBadRequest(json.dumps({'mensaje':'no se ha podido guardar'}))

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
    #primero obtenemos todos los dispositivos
    if request.method == 'POST':
        formulario = ProductoForm(request.POST, files=request.FILES)
        if formulario.is_valid():
            formulario.save()
            try:
                dispositivos = FCMDevice.objects.filter(active=True)
                dispositivos.send_message(
                    title="Producto agregada",
                    body="Se ha agregado: "+ formulario.cleaned_data['nombre'],
                    icon="/static/base/img/rosas.jpg"
                )
                print("***************** envio")
            except Exception as error:
                print("*******************************************")
                print(error)

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

#Serializer
class ProductoViewSet(viewsets.ModelViewSet):
    queryset = Producto.objects.all()
    serializer_class = ProductoSerializer