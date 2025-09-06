package com.example.catalog.database;

import androidx.annotation.NonNull;

public class AuthResponse {
    private boolean isSuccess;
    private String message;
    private String code;


    public boolean isSuccess() {
        return isSuccess;
    }

    public String getMessage() {
        return message;
    }

    public String getCode() {
        return code;
    }
}
