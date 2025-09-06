package com.example.catalog.auth;

import android.os.Bundle;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

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
import com.example.catalog.database.ApiClient;
import com.example.catalog.database.AuthApi;
import com.example.catalog.database.AuthRequest;
import com.example.catalog.database.AuthResponse;

import com.example.catalog.main.MainFragment;

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

        loginButton.setOnClickListener(v -> OnButtonClick());

        Log.d("AuthFragment", "onViewCreated");
    }

    private void OnButtonClick() {
        String login = loginEditText.getText().toString();
        String password = passwordEditText.getText().toString();

        if (login.isEmpty() || password.isEmpty()) {
            Log.d("AuthFragment", "Ошибка входа");
            return;
        }

        AuthApi api = ApiClient.getAuthApi();
        AuthRequest request = new AuthRequest(login, password);

        Call<AuthResponse> call = api.login(request);

        call.enqueue(new Callback<>() {
            @Override
            public void onResponse(Call<AuthResponse> call, Response<AuthResponse> response) {
                if (response.isSuccessful() && response.body() != null) {
                    AuthResponse authResponse = response.body();

                    if (authResponse.isSuccess()) {
                        Log.d("AuthFragment", "Успешный вход, token = " + authResponse.getCode());

                        requireActivity().getSupportFragmentManager()
                                .beginTransaction()
                                .replace(R.id.nav_host_fragment, new MainFragment())
                                .addToBackStack(null)
                                .commit();
                    } else {
                        Log.d("AuthFragment", "Ошибка: " + authResponse.getMessage());
                    }
                } else {
                    Log.d("AuthFragment", "Ошибка ответа сервера");
                }
            }

            @Override
            public void onFailure(Call<AuthResponse> call, Throwable t) {
                Log.e("AuthFragment", "Ошибка сети: " + t.getMessage());
            }
        });
    }
}
