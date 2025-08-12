package com.example.catalog.start;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import com.example.catalog.R;

public class StartFragment extends Fragment {
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {

        return inflater.inflate(R.layout.fragment_start, container, false);

    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        Button loginButton = view.findViewById(R.id.button_login);
        Button registerButton = view.findViewById(R.id.button_register);

        loginButton.setOnClickListener(v -> {
                    Log.d("StartFragment", "Нажата кнопка Авторизоваться");
                }
        );

        registerButton.setOnClickListener(v -> {
                    Log.d("StartFragment", "Нажата кнопка Зарегистрироваться");
                }
        );
    }
}