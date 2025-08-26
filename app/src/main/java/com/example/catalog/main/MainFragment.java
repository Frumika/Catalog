package com.example.catalog.main;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.catalog.R;

import java.util.ArrayList;
import java.util.List;

public class MainFragment extends Fragment {

    private Spinner spinner;
    private RecyclerView recyclerView;
    private TextView textViewCount;
    private ProductAdapter adapter;
    private List<Product> productList;
    private List<Product> cartProducts;


    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_main, container, false);

        productList = createProducts();

        recyclerView = view.findViewById(R.id.main_recyclerView);
        spinner = view.findViewById(R.id.main_spinner__categories);
        textViewCount = view.findViewById(R.id.main_textView__count);

        setupRecyclerView();
        setupSpinner();
        setupListener();

        return view;
    }


    private List<Product> createProducts() {
        List<Product> list = new ArrayList<>();

        list.add(new Product(ProductType.MOBILE, "Samsung Galaxy S21", 120_000, R.drawable.ic_launcher_foreground));
        list.add(new Product(ProductType.MOBILE, "iPhone 13", 100_000, R.drawable.ic_launcher_foreground));
        list.add(new Product(ProductType.MOBILE, "Xiaomi 14", 80_000, R.drawable.ic_launcher_foreground));
        list.add(new Product(ProductType.MOBILE, "OnePlus 9", 35_000, R.drawable.ic_launcher_foreground));

        list.add(new Product(ProductType.LAPTOP, "MacBook Pro", 150_000, R.drawable.ic_launcher_foreground));
        list.add(new Product(ProductType.LAPTOP, "Huawei MateBool D16", 71_000, R.drawable.ic_launcher_foreground));
        list.add(new Product(ProductType.LAPTOP, "Lenovo ThinkPad X1 Carbon", 120_000, R.drawable.ic_launcher_foreground));

        list.add(new Product(ProductType.COMPUTER_EQUIPMENTS, "Nvidia RTX 5090", 250_000, R.drawable.ic_launcher_foreground));
        list.add(new Product(ProductType.COMPUTER_EQUIPMENTS, "Intel Core i9-14900K", 30_000, R.drawable.ic_launcher_foreground));

        return list;
    }

    private void setupRecyclerView() {
        adapter = new ProductAdapter(productList);
        recyclerView.setLayoutManager(new LinearLayoutManager(requireContext()));
        recyclerView.setAdapter(adapter);
    }

    private void setupSpinner() {
        String[] categories = ProductType.getProductTypes();

        ArrayAdapter<String> spinnerAdapter = new ArrayAdapter<>(
                requireContext(),
                android.R.layout.simple_spinner_item,
                categories
        );
        spinnerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(spinnerAdapter);

        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                filterProducts(categories[position]);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
            }
        });
    }

    private void setupListener() {
        cartProducts = new ArrayList<>();

        adapter.setOnProductCheckedChangeListener((product, isChecked) -> {
            if (isChecked) {
                cartProducts.add(product);
            } else {
                cartProducts.remove(product);
            }

            updateCount();
        });
    }

    private void filterProducts(String selectedCategory) {
        ProductType type = ProductType.fromDisplayName(selectedCategory);

        if (type == ProductType.ALL) {
            adapter.setProductList(productList);
        } else {
            List<Product> filtered = new ArrayList<>();
            for (Product p : productList) {
                if (p.getProductType() == type) {
                    filtered.add(p);
                }
            }
            adapter.setProductList(filtered);
        }
    }

    private void updateCount() {
        textViewCount.setText(String.valueOf(cartProducts.size()));
    }
}
