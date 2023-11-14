package com.example.reparto

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.StrictMode
import android.util.Log
import android.view.View
import android.widget.RadioButton
import android.widget.TextView
import android.widget.Toast
import androidx.datastore.core.DataStore
import androidx.datastore.preferences.core.Preferences
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.datastore.preferences.preferencesDataStore
import androidx.lifecycle.lifecycleScope
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.launch
import java.sql.Connection
import java.sql.DriverManager
import java.sql.ResultSet
import java.sql.Statement

val Context.dataStore: DataStore<Preferences> by preferencesDataStore(name = "user_preference")
class login : AppCompatActivity() {

    private var connectSql = ConnectSql()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.login)
    }

    fun btnIngresarClick(view: View){
        var cel: TextView;
        var password: TextView;

        cel = findViewById(R.id.txt_cel)
        password = findViewById(R.id.txt_password)

        val sqlstatement ="SELECT * FROM USUARIOS WHERE NUMERO_CELULAR='"+cel.text.toString()+"' AND CONTRASENA = '"+password.text.toString()+"'"
        val stm: Statement = connectSql.dbConn()?.createStatement()!!
        val rs: ResultSet = stm.executeQuery(sqlstatement)

        if(cel.text.toString().length == 0 && password.text.toString().length == 0){
            Toast.makeText(this,"Número de celular y contraseña no deben estar vacíos",Toast.LENGTH_LONG).show();
        }else if(cel.text.toString().length == 0){
            Toast.makeText(this,"Número de celular no debe estar vacío",Toast.LENGTH_LONG).show();
        }else if(password.text.toString().length == 0){
            Toast.makeText(this,"Contraseña no debe estar vacía",Toast.LENGTH_LONG).show();
        }else{
            if(rs.next()){
                //setContentView(R.layout.locales)
                saveText(valueToSave = cel.text.toString())
                val intent = Intent(this, locales::class.java)
                startActivity(intent)
            }else{
                Toast.makeText(this,"Credenciales incorrectas",Toast.LENGTH_LONG).show();
            }
        }
    }

    fun irCrearCuenta(view: View){
        val intent = Intent(this, crear_cuenta::class.java)
        startActivity(intent)
    }

    fun irLocal1(view: View){
        setContentView(R.layout.local1)
    }

    fun irWebpay(view: View){
        val intent = Intent(this, webpay::class.java)
        startActivity(intent)
    }

    fun btnLocal(view: View){
        var newProducto1 : TextView;
        var sqlstatement = "";

        Log.d("VAL", findViewById(R.id.combo1));

        newProducto1 = findViewById(R.id.combo1);

        when(view.id) {
                R.id.tienda1 -> sqlstatement = "SELECT * FROM PRODUCTOS WHERE ID_EMPRESA=1"
                R.id.tienda2 -> sqlstatement = "SELECT * FROM PRODUCTOS WHERE ID_EMPRESA=2"
                R.id.tienda3 -> sqlstatement = "SELECT * FROM PRODUCTOS WHERE ID_EMPRESA=3"
                R.id.tienda4 -> sqlstatement = "SELECT * FROM PRODUCTOS WHERE ID_EMPRESA=4"
        }

        Log.d("SQL", sqlstatement)

        val stm: Statement = connectSql.dbConn()?.createStatement()!!
        val rs: ResultSet = stm.executeQuery(sqlstatement)

        if(rs.next()){
            var prod1 : String
            var prod2 : String
            Log.d("TAG1", rs.getString(3))
            Log.d("TAG2", rs.getString(4))
            Log.d("TAG3", rs.getString(5))
            prod1 = rs.getString(3);

            Log.d("TAG3", prod1)

            newProducto1.setText(prod1);




            setContentView(R.layout.local1)


        }
    }

    private fun saveText(valueToSave: String) = lifecycleScope.launch {
        val prefsKey = stringPreferencesKey("name_key")
        dataStore.edit { preferences ->
            preferences[prefsKey] = valueToSave
        }
    }
}