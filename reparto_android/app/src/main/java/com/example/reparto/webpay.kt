package com.example.reparto

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.StrictMode
import android.util.Log
import android.view.View
import android.widget.TextView
import android.widget.Toast
import java.sql.Connection
import java.sql.DriverManager
import java.sql.ResultSet
import java.sql.Statement

class webpay : AppCompatActivity() {

    //private var connectSql = ConnectSql()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.webpay)
    }

    fun btnPagarClick(view: View){
        var numTarjeta: TextView
        var MM: TextView
        var YY: TextView
        var codSeguridad: TextView

        numTarjeta = findViewById(R.id.txt_numTarjeta)
        MM = findViewById(R.id.txt_MM)
        YY = findViewById(R.id.txt_YY)
        codSeguridad = findViewById(R.id.txt_CodSeguridad)

        if(numTarjeta.text.toString().length == 0 || MM.text.toString().length == 0 || YY.text.toString().length == 0 || codSeguridad.text.toString().length == 0){
            Toast.makeText(this,"Todos los campos son obligatorios.",Toast.LENGTH_LONG).show();
        }else if(numTarjeta.text.toString().length > 16 || numTarjeta.text.toString().length < 16){
            Toast.makeText(this,"Número de tarjeta incorrecto.",Toast.LENGTH_LONG).show();
        }else if(MM.text.toString().length > 2 || MM.text.toString().length < 2){
            Toast.makeText(this,"Mes de vencimiento incorrecto.",Toast.LENGTH_LONG).show();
        }else if(YY.text.toString().length > 2 || YY.text.toString().length < 2){
            Toast.makeText(this,"Año de vencimiento incorrecto.",Toast.LENGTH_LONG).show();
        }else if(codSeguridad.text.toString().length > 3 || codSeguridad.text.toString().length < 3){
            Toast.makeText(this,"Código de seguridad incorrecto.",Toast.LENGTH_LONG).show();
        }else{
            setContentView(R.layout.webpay_ok)
        }
    }

    fun irLocales(view: View){
        setContentView(R.layout.locales)
    }

}