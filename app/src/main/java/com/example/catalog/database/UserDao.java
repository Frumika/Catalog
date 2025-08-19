package com.example.catalog.database;

public class UserDao {
    private final UsersDbHelper databaseHelper;

    public UserDao(UsersDbHelper databaseHelper) {
        this.databaseHelper = databaseHelper;
    }

    public void addUser(User user) {
        /**/
    }

    public User getUser(String login) {
        return new User(1, "john.mclean@examplepetstore.com", "login", "password");
    }

    public boolean checkUser(String login, String password) {
        return true;
    }

}
