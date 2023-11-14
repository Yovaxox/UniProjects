package com.example.reparto

import android.os.StrictMode
import android.util.Log
import java.sql.Connection
import java.sql.DriverManager
import java.sql.SQLException

class ConnectSql {

    private val ip="yovaxdb.database.windows.net:1433"//"169.254.90.33:1433"
    private val db="yovaxdb"
    private val username="yovaxdb"
    private val password="[DUOC_PWD]"

    fun dbConn(): Connection? {
        val policy = StrictMode.ThreadPolicy.Builder().permitAll().build()
        StrictMode.setThreadPolicy(policy)
        var conn : Connection? = null
        val connString : String
        try{
            Class.forName("net.sourceforge.jtds.jdbc.Driver").newInstance()
            connString = "jdbc:jtds:sqlserver://$ip;databaseName=$db;user=$username;password=$password;ssl=request"
            conn = DriverManager.getConnection(connString)
        }catch (ex: SQLException){

            Log.e("Error: ", ex.message!!)
        }catch (ex1: ClassNotFoundException){
            Log.e( "Error: ", ex1.message!!)
        }catch (ex2: Exception){
            Log.e( "Error: ", ex2.message!!)
        }
        return conn
    }
}
