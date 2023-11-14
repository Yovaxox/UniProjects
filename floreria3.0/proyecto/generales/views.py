from django.shortcuts import render
from django.http import HttpResponse, HttpResponseRedirect
from django.urls import reverse_lazy

from django.contrib.auth.mixins import LoginRequiredMixin, PermissionRequiredMixin

from django.views import generic

# Create your views here.
class SinPrivilegios(PermissionRequiredMixin):
    login_url = 'generales:sin_privilegios'
    raise_exception = False
    redirect_field_name = "redirecto_to"

    def handle_no_permission(self):
        return HttpResponseRedirect(reverse_lazy(self.login_url))

class Home(generic.TemplateView):
    template_name='generales/home.html'

class HomeSinPrivilegios(generic.TemplateView):
    template_name="generales/sin_privilegios.html"