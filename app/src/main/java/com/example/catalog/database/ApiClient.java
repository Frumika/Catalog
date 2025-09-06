package com.example.catalog.database;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ApiClient {
    private static final String SERVER_IP = "192.168.0.101";
    private static final String SERVER_PORT = "5277";
    private static final String BASE_URL = "http://" + SERVER_IP + ":" + SERVER_PORT;
    private static Retrofit retrofit;

    public static Retrofit getRetrofitInstance() {
        if (retrofit == null) {
            retrofit = new Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }
        return retrofit;
    }

    public static AuthApi getAuthApi() {
        return getRetrofitInstance().create(AuthApi.class);
    }
}
