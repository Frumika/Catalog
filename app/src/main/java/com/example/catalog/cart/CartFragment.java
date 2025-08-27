package com.example.catalog.cart;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.RecyclerView;

import com.example.catalog.R;
import com.example.catalog.core.Product;

import java.util.List;

public class CartFragment extends Fragment {

    private TextView textViewCount;
    private RecyclerView recyclerView;
    private TextView textViewTotalSum;
    private Button buttonToPay;
    private List<Product> cartProducts;


    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_cart, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        textViewCount = view.findViewById(R.id.cart_textView__count);
        recyclerView = view.findViewById(R.id.cart_recyclerView);
        textViewTotalSum = view.findViewById(R.id.cart_textView__totalSum);
        buttonToPay = view.findViewById(R.id.cart_button__toPay);

    }

    private void setupRecyclerView() {
        // Todo: Написать реализацию установки RecyclerView
    }
}
