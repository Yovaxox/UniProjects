package com.example.reparto

import android.annotation.SuppressLint
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.lifecycle.lifecycleScope
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.launch
import org.w3c.dom.Text
import java.sql.ResultSet
import java.sql.Statement
import java.text.SimpleDateFormat
import java.util.*

class locales : AppCompatActivity() {

    private var connectSql = ConnectSql()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.locales)
        setUserName()
    }

    private suspend fun loadText(): String? {
        val prefsKey = stringPreferencesKey("name_key")
        val prefs = dataStore.data.first()
        return prefs[prefsKey]
    }


    fun btnSuma(view: View){
        val newTotal: TextView = findViewById(R.id.total)
        val newCantidad1: TextView = findViewById(R.id.cantidad1)
        val numero1:Double =newCantidad1.text.toString().toDouble()
        val newCantidad2: TextView = findViewById(R.id.cantidad2)
        val numero2:Double =newCantidad2.text.toString().toDouble()
        val newCantidad3: TextView = findViewById(R.id.cantidad3)
        val numero3:Double =newCantidad3.text.toString().toDouble()
        val newCantidad4: TextView = findViewById(R.id.cantidad4)
        val numero4:Double =newCantidad4.text.toString().toDouble()

        when(view.id) {
            R.id.mas1 -> newCantidad1.setText((numero1 + 1).toInt().toString());
            R.id.mas2 -> newCantidad2.setText((numero2 + 1).toInt().toString());
            R.id.mas3 -> newCantidad3.setText((numero3 + 1).toInt().toString());
            R.id.mas4 -> newCantidad4.setText((numero4 + 1).toInt().toString());
        }

        calculartotal();
    }

    fun btnResta(view: View){
        val newCantidad1: TextView = findViewById(R.id.cantidad1)
        val numero1:Double =newCantidad1.text.toString().toDouble()
        val newCantidad2: TextView = findViewById(R.id.cantidad2)
        val numero2:Double =newCantidad2.text.toString().toDouble()
        val newCantidad3: TextView = findViewById(R.id.cantidad3)
        val numero3:Double =newCantidad3.text.toString().toDouble()
        val newCantidad4: TextView = findViewById(R.id.cantidad4)
        val numero4:Double =newCantidad4.text.toString().toDouble()

        when(view.id) {
            R.id.menos1 -> newCantidad1.setText((numero1 - 1).toInt().toString());
            R.id.menos2 -> newCantidad2.setText((numero2 - 1).toInt().toString());
            R.id.menos3 -> newCantidad3.setText((numero3 - 1).toInt().toString());
            R.id.menos4 -> newCantidad4.setText((numero4 - 1).toInt().toString());
        }

        calculartotal();

    }

    fun calculartotal(){
        val newTotal: TextView = findViewById(R.id.total);
        val txtCant1: TextView = findViewById(R.id.cantidad1);
        val valCant1:Int = txtCant1.text.toString().toInt();
        val txtPrecio1: TextView = findViewById(R.id.precio1);
        val valPrecio1:Int = txtPrecio1.text.toString().toInt();

        val txtCant2: TextView = findViewById(R.id.cantidad2);
        val valCant2:Int = txtCant2.text.toString().toInt();
        val txtPrecio2: TextView = findViewById(R.id.precio2);
        val valPrecio2:Int = txtPrecio2.text.toString().toInt();

        val txtCant3: TextView = findViewById(R.id.cantidad3);
        val valCant3:Int = txtCant3.text.toString().toInt();
        val txtPrecio3: TextView = findViewById(R.id.precio3);
        val valPrecio3:Int = txtPrecio3.text.toString().toInt();

        val txtCant4: TextView = findViewById(R.id.cantidad4);
        val valCant4:Int = txtCant4.text.toString().toInt();
        val txtPrecio4: TextView = findViewById(R.id.precio4);
        val valPrecio4:Int = txtPrecio4.text.toString().toInt();

        val subTotal:String = ((valCant1 * valPrecio1)+(valCant2 * valPrecio2)+(valCant3 * valPrecio3)+(valCant4 * valPrecio4)).toString()

        newTotal.setText(subTotal)
    }

    fun btnLocal(view: View){
        when(view.id) {
            R.id.tienda1 -> setContentView(R.layout.local1);
            R.id.tienda2 -> setContentView(R.layout.local2);
            R.id.tienda3 -> setContentView(R.layout.local3);
            R.id.tienda4 -> setContentView(R.layout.local4);
        }
        //setContentView(R.layout.local1)
        var newProducto1 : TextView;
        var newProducto2 : TextView;
        var newProducto3 : TextView;
        var newProducto4 : TextView;
        var newPrecio1 : TextView;
        var newPrecio2 : TextView;
        var newPrecio3 : TextView;
        var newPrecio4 : TextView;
        var sqlstatement = "";

        newProducto1 = findViewById(R.id.combo1);
        newProducto2 = findViewById(R.id.combo2);
        newProducto3 = findViewById(R.id.combo3);
        newProducto4 = findViewById(R.id.combo4);

        newPrecio1 = findViewById(R.id.precio1);
        newPrecio2 = findViewById(R.id.precio2);
        newPrecio3 = findViewById(R.id.precio3);
        newPrecio4 = findViewById(R.id.precio4);

        when(view.id) {
            R.id.tienda1 -> sqlstatement = "SELECT * FROM PRODUCTOS WHERE ID_EMPRESA=1"
            R.id.tienda2 -> sqlstatement = "SELECT * FROM PRODUCTOS WHERE ID_EMPRESA=2"
            R.id.tienda3 -> sqlstatement = "SELECT * FROM PRODUCTOS WHERE ID_EMPRESA=3"
            R.id.tienda4 -> sqlstatement = "SELECT * FROM PRODUCTOS WHERE ID_EMPRESA=4"
        }

        Log.d("SQL", sqlstatement)

        val stm: Statement = connectSql.dbConn()?.createStatement()!!
        val rs: ResultSet = stm.executeQuery(sqlstatement)

        //if(rs.next()){
            var prod1 : String
            var val1 : String
            while (rs.next() == true){
                prod1 = rs.getString(3);
                val1 = rs.getString(4);
                print(newProducto1.text.toString())
                Log.d("TAG2", newProducto1.text.toString());

                if(newProducto1.text.toString() == "") {
                    newProducto1.setText(prod1);
                    newPrecio1.setText(val1)
                }else if(newProducto2.text.toString() == "") {
                    newProducto2.setText(prod1);
                    newPrecio2.setText(val1)
                }else if(newProducto3.text.toString() == ""){
                    newProducto3.setText(prod1);
                    newPrecio3.setText(val1)
                }else if(newProducto4.text.toString() == ""){
                    newProducto4.setText(prod1);
                    newPrecio4.setText(val1)
                }
            }


        //}
    }

    @SuppressLint("MissingInflatedId")
    fun irWebpay(view: View){
        val newTotal: TextView = findViewById(R.id.total);

        var comision: String = "0"
        if (newTotal.text.toString() == "") newTotal.text = "0"
        if(newTotal.text.toString().toInt() > 0) {
            comision = (3 * (newTotal.text.toString().toInt()) / 100).toString()
        }

        val totalFinal:String = (newTotal.text.toString().toInt() + comision.toString().toInt()).toString()

        if(totalFinal.toInt() > 0) {
            setContentView(R.layout.webpay)
            val newTotal2: TextView = findViewById(R.id.total2);
            newTotal2.setText("$" + totalFinal)
            Log.d("TOTAL2", totalFinal)
        }else{
            Toast.makeText(this,"El monto total debe ser mayor a 0.",Toast.LENGTH_LONG).show();
        }
    }

    fun irLocales(view: View){
        setContentView(R.layout.locales)
        setUserName()
    }

    fun btnPagarClick(view: View){
        var total: TextView = findViewById(R.id.total2)
        //M/dd/yyyy SQLSERVER
        val sdf = SimpleDateFormat("M/dd/yyyy")
        val currentDate = sdf.format(Date())
        //obtener ID usuario
        var ID_usuario: String = getUserID()

        Log.d("TOTAL$$$", total.text.toString().substring(1))
        Log.d("FECHA ACTUAL", currentDate)
        Log.d("ID USUARIO", ID_usuario)

        var numTarjeta: TextView
        var MM: TextView
        var YY: TextView
        var codSeguridad: TextView

        numTarjeta = findViewById(R.id.txt_numTarjeta)
        MM = findViewById(R.id.txt_MM)
        YY = findViewById(R.id.txt_YY)
        codSeguridad = findViewById(R.id.txt_CodSeguridad)

        if(numTarjeta.text.toString().length == 0 || MM.text.toString().length == 0 || YY.text.toString().length == 0 || codSeguridad.text.toString().length == 0){
            Toast.makeText(this,"Todos los campos son obligatorios.", Toast.LENGTH_LONG).show();
        }else if(numTarjeta.text.toString().length > 16 || numTarjeta.text.toString().length < 16){
            Toast.makeText(this,"Número de tarjeta incorrecto.", Toast.LENGTH_LONG).show();
        }else if(MM.text.toString().length > 2 || MM.text.toString().length < 2){
            Toast.makeText(this,"Mes de vencimiento incorrecto.", Toast.LENGTH_LONG).show();
        }else if(YY.text.toString().length > 2 || YY.text.toString().length < 2){
            Toast.makeText(this,"Año de vencimiento incorrecto.", Toast.LENGTH_LONG).show();
        }else if(codSeguridad.text.toString().length > 3 || codSeguridad.text.toString().length < 3){
            Toast.makeText(this,"Código de seguridad incorrecto.", Toast.LENGTH_LONG).show();
        }else{
            val sqlstatement =
                "INSERT PEDIDOS (TOTAL, FECHA, ID_USUARIO)" +
                        " VALUES ('"+total.text.toString().substring(1).toInt()+"' ,'" +currentDate+ "','" + ID_usuario.toInt() + "')"
            Log.d("SQLSTATEMENT", sqlstatement)
            val stm: Statement = connectSql.dbConn()?.createStatement()!!
            stm.executeUpdate(sqlstatement)
            setContentView(R.layout.webpay_ok)
        }
    }

    fun setUserName(){
        lifecycleScope.launch {
            var dataCel: String?
            val userName: TextView = findViewById(R.id.txtUserName)
            dataCel = loadText()
            Log.d("DATASTORE", dataCel.toString())

            val sqlstatement = "SELECT * FROM USUARIOS WHERE NUMERO_CELULAR='$dataCel'"
            val stm: Statement = connectSql.dbConn()?.createStatement()!!
            val rs: ResultSet = stm.executeQuery(sqlstatement)
            if(rs.next()) {
                userName.setText("Bienvenid@, " + rs.getString(2) + "!")
            }
        }
    }

    fun getUserID(): String{
        var data: String = ""

        lifecycleScope.launch {
            var cel: String? = loadText()
            val sqlstatement = "SELECT * FROM USUARIOS WHERE NUMERO_CELULAR='$cel'"
            val stm: Statement = connectSql.dbConn()?.createStatement()!!
            val rs: ResultSet = stm.executeQuery(sqlstatement)
            if(rs.next()) {
                data = rs.getString(1)
            }
        }
        return data
    }
}