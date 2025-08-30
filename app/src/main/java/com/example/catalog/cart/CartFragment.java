package com.example.catalog.cart;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.catalog.R;
import com.example.catalog.core.Product;

import java.io.Serializable;
import java.util.List;

public class CartFragment extends Fragment {

    public static final String ARG_CART_PRODUCTS = "cart_products";
    private TextView textViewCount;
    private RecyclerView recyclerView;
    private TextView textViewTotalSum;
    private Button buttonToPay;
    private List<Product> cartList;
    private CartProductAdapter adapter;


    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if (getArguments() != null) {
            cartList = (List<Product>) getArguments().getSerializable(ARG_CART_PRODUCTS);
        }
    }

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

        setupRecyclerView();
        setupButtonToPay();

        updateCount();
        updateTotalProductsPrice();
    }

    public static CartFragment newInstance(List<Product> cartProducts) {
        CartFragment fragment = new CartFragment();
        Bundle args = new Bundle();

        args.putSerializable(ARG_CART_PRODUCTS, (Serializable) cartProducts);
        fragment.setArguments(args);

        return fragment;
    }

    private void setupRecyclerView() {
        adapter = new CartProductAdapter(cartList);
        adapter.setOnProductRemoveListener(this::onProductRemove);

        recyclerView.setLayoutManager(new LinearLayoutManager(requireContext()));
        recyclerView.setAdapter(adapter);
    }

    private void setupButtonToPay() {
        buttonToPay.setOnClickListener(v -> onButtonToPayClick());
    }

    private void onButtonToPayClick() {
        // Здесь можно добавить логику для перехода к оплате

        Log.d("CartFragment", "onButtonToPayClick");
    }



    @SuppressLint("NotifyDataSetChanged")
    private void onProductRemove(int position) {
        cartList.remove(position);
        adapter.notifyItemRemoved(position);

        updateCount();
        updateTotalProductsPrice();
    }

    private void updateCount() {
        textViewCount.setText(String.valueOf(cartList.size()));
    }

    private void updateTotalProductsPrice() {
        double totalSum = 0;
        for (Product product : cartList) {
            totalSum += product.getPrice();
        }
        textViewTotalSum.setText(totalSum + " ₽");
    }
}
