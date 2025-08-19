package com.example.catalog;

import android.app.Application;

import com.example.catalog.database.UserDao;
import com.example.catalog.database.UsersDbHelper;


public class MyApp extends Application {
    private UsersDbHelper dbHelper;
    private UserDao userDao;

    @Override
    public void onCreate() {
        super.onCreate();

        dbHelper = new UsersDbHelper(this);
        userDao = new UserDao(dbHelper);
    }

    public UsersDbHelper getDbHelper() {
        return dbHelper;
    }

    public UserDao getUserDao() {
        return userDao;
    }
}
