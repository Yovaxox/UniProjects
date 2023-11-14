from django.db import models

from generales.models import ClaseModelo
# Create your models here.

class Producto(ClaseModelo):
    fotografia = models.ImageField(upload_to="img")
    nombre = models.CharField(max_length=20)
    valor = models.IntegerField()
    descripcion = models.TextField(max_length=100)
    stock = models.IntegerField()

    def __str__(self):
        return self.nombre

    class Meta:
        verbose_name_plural = "Productos"