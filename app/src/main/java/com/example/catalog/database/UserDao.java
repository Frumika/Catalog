package com.example.catalog.database;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

public class UserDao {
    private final UsersDbHelper dbHelper;

    public UserDao(UsersDbHelper dbHelper) {
        this.dbHelper = dbHelper;
    }

    public void addUser(String email, String login, String password) throws Exception {
        if (checkUser(email, login)) {
            throw new Exception("Пользователь с таким логином или почтой уже существует");
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();

        values.put(UsersDbHelper.COLUMN_EMAIL, email);
        values.put(UsersDbHelper.COLUMN_LOGIN, login);
        values.put(UsersDbHelper.COLUMN_PASSWORD, password);

        long result = db.insert(UsersDbHelper.TABLE_NAME, null, values);

        if (result == -1) {
            throw new Exception("Ошибка при добавлении пользователя в базу данных");
        }
    }

    public boolean checkUser(String email, String login) {
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        String sql = "SELECT EXISTS(SELECT 1 FROM " + UsersDbHelper.TABLE_NAME +
                " WHERE " + UsersDbHelper.COLUMN_LOGIN + " = ? )";
        Cursor cursor = db.rawQuery(sql, new String[]{login, email});

        boolean exist = false;
        if (cursor.moveToFirst()) {
            exist = cursor.getInt(0) == 1;
        }

        cursor.close();
        return exist;
    }

    public void clearDatabase() {
        dbHelper.clearDatabase();
    }

}
