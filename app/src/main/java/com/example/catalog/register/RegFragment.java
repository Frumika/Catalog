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

import com.example.catalog.R;

public class RegFragment extends Fragment {

    private EditText emailEditText;
    private EditText loginEditText;
    private EditText passwordEditText;
    private EditText confirmPasswordEditText;
    private Button registerButton;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {

        return inflater.inflate(R.layout.fragment_register, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        emailEditText = view.findViewById(R.id.register_editText__email);
        loginEditText = view.findViewById(R.id.register_editText__login);
        passwordEditText = view.findViewById(R.id.register_editText__password);
        registerButton = view.findViewById(R.id.register_button__register);
        confirmPasswordEditText = view.findViewById(R.id.register_editText__confirmPassword);

        registerButton.setOnClickListener(v -> {
            String email = emailEditText.getText().toString();
            String login = loginEditText.getText().toString();
            String password = passwordEditText.getText().toString();
            String confirmPassword = confirmPasswordEditText.getText().toString();

            Log.d("RegFragment", "Логин: " + login + ", Пароль: " + password);
        });

        Log.d("RegFragment", "onViewCreated");
    }
}
