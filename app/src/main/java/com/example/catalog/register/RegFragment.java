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
import com.example.catalog.core.FieldType;
import com.example.catalog.database.ApiClient;
import com.example.catalog.database.AuthApi;
import com.example.catalog.database.AuthRequest;
import com.example.catalog.database.AuthResponse;
import com.example.catalog.main.MainFragment;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;


public class RegFragment extends Fragment {

    private EditText emailEditText;
    private EditText loginEditText;
    private EditText passwordEditText;
    private EditText confirmPasswordEditText;


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

        if (!password.equals(confirmPassword)) {
            Log.d("RegFragment", "Пароли не совпадают");
            clearFields(FieldType.combine(FieldType.PASSWORD, FieldType.CONFIRM_PASSWORD));
            return;
        }

        AuthApi api = ApiClient.getAuthApi();
        AuthRequest request = new AuthRequest(email, login, password);

        Call<AuthResponse> call = api.register(request);

        call.enqueue(new Callback<>() {
            @Override
            public void onResponse(Call<AuthResponse> call, Response<AuthResponse> response) {
                if (response.isSuccessful() && response.body() != null) {
                    AuthResponse authResponse = response.body();

                    if (authResponse.isSuccess()) {
                        Log.d("RegFragment", "Успешная регистрация, token = " + authResponse.getCode());

                        requireActivity().getSupportFragmentManager()
                                .beginTransaction()
                                .replace(R.id.nav_host_fragment, new MainFragment())
                                .addToBackStack(null)
                                .commit();
                    } else {
                        Log.d("RegFragment", "Ошибка: " + authResponse.getMessage());
                    }
                } else {
                    Log.d("RegFragment", "Ошибка ответа сервера");
                }
            }

            @Override
            public void onFailure(Call<AuthResponse> call, Throwable t) {
                Log.e("AuthFragment", "Ошибка сети: " + t.getMessage());
            }
        });

    }


    private void clearFields(int fieldTypes) {
        if (FieldType.hasFlag(fieldTypes, FieldType.EMAIL)) {
            emailEditText.setText("");
        }
        if (FieldType.hasFlag(fieldTypes, FieldType.LOGIN)) {
            loginEditText.setText("");
        }
        if (FieldType.hasFlag(fieldTypes, FieldType.PASSWORD)) {
            passwordEditText.setText("");
        }
        if (FieldType.hasFlag(fieldTypes, FieldType.CONFIRM_PASSWORD)) {
            confirmPasswordEditText.setText("");
        }
    }
}
