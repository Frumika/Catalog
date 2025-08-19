package com.example.catalog.register;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import com.example.catalog.MyApp;
import com.example.catalog.R;
import com.example.catalog.core.ValidationUtils;
import com.example.catalog.database.UserDao;

public class RegFragment extends Fragment {

    private EditText emailEditText;
    private EditText loginEditText;
    private EditText passwordEditText;
    private EditText confirmPasswordEditText;
    private UserDao userDao;


    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {

        userDao = ((MyApp) requireActivity().getApplication()).getUserDao();

        return inflater.inflate(R.layout.fragment_register, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        emailEditText = view.findViewById(R.id.register_editText__email);
        loginEditText = view.findViewById(R.id.register_editText__login);
        passwordEditText = view.findViewById(R.id.register_editText__password);
        confirmPasswordEditText = view.findViewById(R.id.register_editText__confirmPassword);

        Button registerButton = view.findViewById(R.id.register_button__register);
        registerButton.setOnClickListener(v -> OnButtonClick());

        Log.d("RegFragment", "onViewCreated");
    }

    private void OnButtonClick() {
        String email = emailEditText.getText().toString();
        String login = loginEditText.getText().toString();
        String password = passwordEditText.getText().toString();
        String confirmPassword = confirmPasswordEditText.getText().toString();

        boolean isPasswordEquals = password.equals(confirmPassword);
        boolean isUserDataValid = VerifyUserData(email, login, password);

        if (isUserDataValid && isPasswordEquals) {
            try {
                userDao.addUser(email, login, password);
            } catch (Exception e) {
                Log.d("RegFragment", "Error: " + e.getMessage());
            }
        } else {
            Log.d("RegFragment", "User data is not valid");
        }


    }

    private boolean VerifyUserData(String email, String login, String password) {
        boolean isValidEmail = ValidationUtils.isValidEmail(email);
        boolean isValidLogin = ValidationUtils.isValidLogin(login);
        boolean isValidPassword = ValidationUtils.isValidPassword(password);

        return isValidEmail && isValidLogin && isValidPassword;
    }
}
