package com.example.catalog.database;

import android.content.ContentValues;
import android.database.sqlite.SQLiteDatabase;

public class UserDao {
    private final UsersDbHelper dbHelper;

    public UserDao(UsersDbHelper dbHelper) {
        this.dbHelper = dbHelper;
    }

    public void addUser(String email, String login, String password) {
        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();

        values.put(UsersDbHelper.COLUMN_EMAIL, email);
        values.put(UsersDbHelper.COLUMN_LOGIN, login);
        values.put(UsersDbHelper.COLUMN_PASSWORD, password);

        long result = db.insert(UsersDbHelper.TABLE_NAME, null, values);

        if (result == -1) {
            throw new Error("Ошибка при добавлении пользователя в базу данных");
        }
    }

}
