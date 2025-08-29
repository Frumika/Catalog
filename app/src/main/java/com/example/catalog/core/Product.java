package com.example.catalog.core;

import java.io.Serializable;

public class Product implements Serializable {
    private final int imageResId;
    private final String name;
    private final double price;
    private final ProductType productType;
    private boolean IsChecked;


    public Product(ProductType productType, String name, double price, int imageResId) {
        this.productType = productType;
        this.name = name;
        this.price = price;
        this.imageResId = imageResId;
    }

    public ProductType getProductType() {
        return productType;
    }

    public int getImageResId() {
        return imageResId;
    }

    public String getName() {
        return name;
    }

    public double getPrice() {
        return price;
    }

    public boolean isChecked() {
        return IsChecked;
    }

    public void setChecked(boolean isChecked) {
        this.IsChecked = isChecked;
    }
}
