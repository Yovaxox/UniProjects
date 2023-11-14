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
        return HttpResponseBadRequest(json.dumps({'mensaje':'El token ya existe'}))

    dispositivo = FCMDevice()
    dispositivo.registration_id = token
    dispositivo.active = True

    #Solo si el usuario está autenticado procederemos a enlazarlo
    if request.user.is_authenticated:
        dispositivo.user = request.user

    try:
        dispositivo.save()
        return HttpResponse(json.dumps({'mensaje':'Token guardado'}))
    except:
        return HttpResponseBadRequest(json.dumps({'mensaje':'No se ha podido guardar'}))

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

#Serializer
class ProductoViewSet(viewsets.ModelViewSet):
    queryset = Producto.objects.all()
    serializer_class = ProductoSerializer

def producto_print(self, pk=None):
    import io
    from reportlab.platypus import SimpleDocTemplate, Paragraph, TableStyle
    from reportlab.lib.styles import getSampleStyleSheet
    from reportlab.lib import colors
    from reportlab.lib.pagesizes import letter
    from reportlab.platypus import Table

    response = HttpResponse(content_type='application/pdf')
    buff=io.BytesIO()
    doc=SimpleDocTemplate(buff,
            pagesize=letter,
            rightMargin=40,
            leftMargin=40,
            topMargin=60,
            bottomMargin=18,)

    productos=[]
    styles=getSampleStyleSheet()
    header1=Paragraph("Florería - PDF Hecho por Javier Paredes", styles['Heading1'])
    header2=Paragraph("Listado de Productos", styles['Heading2'])
    productos.append(header1)
    productos.append(header2)
    headings=('Id Producto','Nombre Producto','Valor (CLP)','Stock Disponible')
    if not pk:
        registros= [ (p.id, p.nombre, p.valor, p.stock)
                    for p in Producto.objects.all().order_by('pk')
                    ]
    else:
        registros= [ (p.id, p.nombre, p.valor, p.stock)
                    for p in Producto.objects.filter(id=pk).order_by('pk')
                    ]



    t= Table([headings] + registros)
    t.setStyle(TableStyle(
        [
            ('GRID', (0,0), (3,-1), 1, colors.dodgerblue),
            ('LINEBELOW', (0,0), (-1,0), 2, colors.darkblue),
            ('BACKGROUND', (0,0), (-1,0), colors.dodgerblue)
        ]
    ))

    productos.append(t)
    doc.build(productos)
    response.write(buff.getvalue())
    buff.close()
    return response