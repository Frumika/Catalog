package com.example.catalog.main;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.catalog.R;

import java.util.ArrayList;
import java.util.List;

public class MainFragment extends Fragment {

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {

        View view = inflater.inflate(R.layout.fragment_main, container, false);

        RecyclerView recyclerView = view.findViewById(R.id.main_recyclerView);

        List<Product> products = new ArrayList<>();
        products.add(new Product(ProductType.MOBILE, "Samsung Galaxy S21", 120_000, R.drawable.ic_launcher_foreground));
        products.add(new Product(ProductType.MOBILE, "iPhone 13", 100_000, R.drawable.ic_launcher_foreground));
        products.add(new Product(ProductType.MOBILE, "Xiaomi 14", 80_000, R.drawable.ic_launcher_foreground));
        products.add(new Product(ProductType.MOBILE, "OnePlus 9", 35_000, R.drawable.ic_launcher_foreground));

        products.add(new Product(ProductType.LAPTOP, "MacBook Pro", 150_000, R.drawable.ic_launcher_foreground));
        products.add(new Product(ProductType.LAPTOP, "Huawei MateBool D16", 71_000, R.drawable.ic_launcher_foreground));
        products.add(new Product(ProductType.LAPTOP, "Lenovo ThinkPad X1 Carbon", 120_000, R.drawable.ic_launcher_foreground));

        products.add(new Product(ProductType.COMPUTER_EQUIPMENTS, "Nvidia RTX 5090", 250_000, R.drawable.ic_launcher_foreground));
        products.add(new Product(ProductType.COMPUTER_EQUIPMENTS, "Intel Core i9-14900K", 30_000, R.drawable.ic_launcher_foreground));

        ProductAdapter adapter = new ProductAdapter(products);
        recyclerView.setAdapter(adapter);

        recyclerView.setLayoutManager(new LinearLayoutManager(requireContext()));

        return view;
    }
}
