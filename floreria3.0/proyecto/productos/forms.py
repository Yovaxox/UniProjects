from django import forms

from productos.models import Producto

class ProductoForm(forms.ModelForm):
    class Meta:
        model = Producto
        fields = ['fotografia','nombre','valor','descripcion','activo','stock']
        labels = {'fotografia': "Fotografía del producto",
                  'nombre': "Nombre del producto", 
                  'valor' : "Valor del producto", 
                  'descripcion' : "Descripción del producto", 
                  'activo' : "Estado del producto", 
                  'stock' : "Stock del producto"}
        widget = {'fotografia': forms.FileInput(), 
                  'nombre': forms.TextInput(), 
                  'valor': forms.NumberInput(),
                  'descripcion': forms.TextInput(), 
                  'activo': forms.CheckboxInput(), 
                  'stock': forms.NumberInput()}

        def __init__(self, *args, **kwargs):
            super().__init__(*args, **kwargs)
            for field in iter(self.fields):
                self.fields[field].widget.attrs.update({
                    'class': 'form-control'
                })