package com.example.reparto

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.StrictMode
import android.util.Log
import android.view.View
import android.widget.ArrayAdapter
import android.widget.TextView
import android.widget.Toast
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.lifecycle.lifecycleScope
import kotlinx.coroutines.launch
import java.nio.file.attribute.AttributeView
import java.sql.Connection
import java.sql.DriverManager
import java.sql.ResultSet
import java.sql.Statement

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    fun irLogin(view: View){
        val intent = Intent(this, login::class.java)
        startActivity(intent)
    }

    private fun saveText(valueToSave: String) = lifecycleScope.launch {
        val prefsKey = stringPreferencesKey("name_key")
        dataStore.edit { preferences ->
            preferences[prefsKey] = valueToSave
        }
    }
}