package com.example.catalog.core;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class MainViewModel extends ViewModel {

    private final MutableLiveData<Boolean> isLoggedIn = new MutableLiveData<>(false);

    public LiveData<Boolean> getIsLoggedIn() {

        return isLoggedIn;
    }

    public void setLoggedIn(boolean value) {
        isLoggedIn.setValue(value);
    }
}
