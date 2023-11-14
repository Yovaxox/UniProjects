package com.example.reparto

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.ListView

class list_pedidos : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_list_pedidos)

        val arrayAdapter:ArrayAdapter<*>

        val pedidos = mutableListOf("Pedido1","Pedido2","Pedido3")
        val lvPedidos = findViewById<ListView>(R.id.lvPedidos)

        arrayAdapter = ArrayAdapter(this,android.R.layout.simple_list_item_1,pedidos)
        lvPedidos.adapter = arrayAdapter
    }
}