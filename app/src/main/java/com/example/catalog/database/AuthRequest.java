package com.example.catalog.database;

public class AuthRequest {
    private String email;
    private String login;
    private String password;

    public AuthRequest(String email, String login, String password) {
        this(login, password);
        this.email = email;
    }

    public AuthRequest(String login, String password) {
        this.login = login;
        this.password = password;
    }


    public String getEmail() {
        return email;
    }

    public String getLogin() {
        return login;
    }

    public String getPassword() {
        return password;
    }
}
