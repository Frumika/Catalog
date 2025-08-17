package com.example.catalog.core;

import java.util.regex.Pattern;

public class ValidationUtils {

    private static final String EMAIL_REGEX = "^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,6}$";
    private static final Pattern EMAIL_PATTERN = Pattern.compile(EMAIL_REGEX);

    private static final String LOGIN_REGEX = "^[A-Za-z0-9_.!@#$%^&*(){}-]+$";
    private static final Pattern LOGIN_PATTERN = Pattern.compile(LOGIN_REGEX);

    private static final String PASSWORD_REGEX = "^[A-Za-z0-9_.!@#$%^&*(){}-]+$";
    private static final Pattern PASSWORD_PATTERN = Pattern.compile(PASSWORD_REGEX);


    public static boolean isValidEmail(String email) {
        return EMAIL_PATTERN.matcher(email).matches();
    }

    public static boolean isValidLogin(String login) {
        return LOGIN_PATTERN.matcher(login).matches();
    }

    public static boolean isValidPassword(String password) {
        return PASSWORD_PATTERN.matcher(password).matches();
    }
}
