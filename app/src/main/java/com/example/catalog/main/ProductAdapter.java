package com.example.catalog.main;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.catalog.R;

import java.util.List;

public class ProductAdapter extends RecyclerView.Adapter<ProductAdapter.ProductViewHolder> {

    private final List<Product> productList;

    public ProductAdapter(List<Product> productList) {
        this.productList = productList;
    }

    @NonNull
    @Override
    public ProductViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.item_product, parent, false);
        return new ProductViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ProductViewHolder holder, int position) {
        Product product = productList.get(position);
        holder.imageProduct.setImageResource(product.getImageResId());
        holder.textProductName.setText(product.getName());
        holder.textProductPrice.setText(product.getPrice() + " ₽");
    }

    @Override
    public int getItemCount() {
        return productList.size();
    }

    public static class ProductViewHolder extends RecyclerView.ViewHolder {
        ImageView imageProduct;
        CheckBox checkBoxProduct;
        TextView textProductName;
        TextView textProductPrice;
        TextView textProductPriceLabel;

        public ProductViewHolder(@NonNull View itemView) {
            super(itemView);
            imageProduct = itemView.findViewById(R.id.imageProduct);
            checkBoxProduct = itemView.findViewById(R.id.checkBoxProduct);
            textProductName = itemView.findViewById(R.id.textProductName);
            textProductPriceLabel = itemView.findViewById(R.id.textProductPrice__label);
            textProductPrice = itemView.findViewById(R.id.textProductPrice);
        }
    }
}
