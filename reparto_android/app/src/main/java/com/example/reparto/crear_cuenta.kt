package com.example.reparto

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.StrictMode
import android.util.Log
import android.view.View
import android.widget.CheckBox
import android.widget.TextView
import android.widget.Toast
import java.sql.Connection
import java.sql.DriverManager
import java.sql.ResultSet
import java.sql.SQLException
import java.sql.Statement

class crear_cuenta : AppCompatActivity() {

    private var connectSql = ConnectSql()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.crear_cuenta)
    }

    fun btnConfirmarClick(view: View){
        var nombre: TextView
        var num_cel: TextView
        var dir_dom: TextView
        var pass: TextView
        var dir_ofi: TextView
        var check_terms: CheckBox

        nombre = findViewById(R.id.txt_nombre)
        num_cel = findViewById(R.id.txt_num)
        pass = findViewById(R.id.txt_pass)
        dir_dom = findViewById(R.id.txt_dom)
        dir_ofi = findViewById(R.id.txt_ofi) //opcional
        check_terms = findViewById(R.id.check_terms)

        if(nombre.text.toString().length == 0 || num_cel.text.toString().length == 0 || pass.text.toString().length == 0 || dir_dom.text.toString().length == 0 || check_terms.isChecked == false){
            Toast.makeText(this,"Revise los campos obligatorios (*)",Toast.LENGTH_LONG).show();
        }else if(num_cel.text.toString().length > 8 || num_cel.text.toString().length < 8){
            Toast.makeText(this,"Número de celular no es válido",Toast.LENGTH_LONG).show();
        }else if(existeNumBD(num_cel)){
            Toast.makeText(this,"Número de celular ya existe",Toast.LENGTH_LONG).show();
        }
        else{
            try {
                val sqlstatement =
                    "INSERT USUARIOS (NOMBRE, NUMERO_CELULAR, CONTRASENA, DIRECCION_DOMICILIO, DIRECCION_OFICINA)" +
                            " VALUES ('"+nombre.text.toString()+"' ,'" + num_cel.text.toString() + "','" + pass.text.toString() + "' ,'" + dir_dom.text.toString() + "' ,'" + dir_ofi.text.toString() + "')"
                Log.d("SQLSTATEMENT", sqlstatement)
                val stm: Statement = connectSql.dbConn()?.createStatement()!!
                stm.executeUpdate(sqlstatement)

                setContentView(R.layout.cuenta_ok)
            } catch (ex: SQLException){
                Toast.makeText(this,"Ha ocurrido un error. Contacte al administrador.",Toast.LENGTH_LONG).show();
            }
        }
    }

    fun existeNumBD(num_cel: TextView): Boolean {
        val sqlstatement ="SELECT COUNT(*) AS CONTEO FROM USUARIOS WHERE NUMERO_CELULAR='"+num_cel.text.toString()+"'"
        val stm: Statement = connectSql.dbConn()?.createStatement()!!
        val rs: ResultSet = stm.executeQuery(sqlstatement)

        if(rs.next()){
            return rs.getString(1) == "1"
        }
        return false
    }

    fun irLogin(view: View){
        val intent = Intent(this, login::class.java)
        startActivity(intent)
    }
}