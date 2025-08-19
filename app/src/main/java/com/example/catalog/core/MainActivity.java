package com.example.catalog.core;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;


import com.example.catalog.R;
import com.example.catalog.database.UserDao;
import com.example.catalog.database.UsersDbHelper;

public class MainActivity extends AppCompatActivity {
    private final UsersDbHelper dbHelper = new UsersDbHelper(this);
    private final UserDao userDao = new UserDao(dbHelper);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_main);
    }
}