package com.example.catalog.database;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface AuthApi {
    @POST("/api/identity/register")
    Call<AuthResponse> register(@Body AuthRequest request);

    @POST("/api/identity/login")
    Call<AuthResponse> login(@Body AuthRequest request);
}
