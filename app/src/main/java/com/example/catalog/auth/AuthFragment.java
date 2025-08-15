package com.example.catalog.auth;

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

public class AuthFragment extends Fragment {

    private EditText loginEditText;
    private EditText passwordEditText;
    private Button loginButton;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {

        return inflater.inflate(R.layout.fragment_auth, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        loginEditText = view.findViewById(R.id.auth_editText__login);
        passwordEditText = view.findViewById(R.id.auth_editText__password);
        loginButton = view.findViewById(R.id.auth_button__login);

        loginButton.setOnClickListener(v -> {
            String login = loginEditText.getText().toString();
            String password = passwordEditText.getText().toString();

            Log.d("AuthFragment", "Логин: " + login + ", Пароль: " + password);
        });

        Log.d("AuthFragment", "onViewCreated");
    }
}
