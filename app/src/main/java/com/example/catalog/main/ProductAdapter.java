package com.example.catalog.main;

import android.annotation.SuppressLint;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.catalog.R;

import java.util.ArrayList;
import java.util.List;

public class ProductAdapter extends RecyclerView.Adapter<ProductAdapter.ProductViewHolder> {

    public interface OnProductCheckedChangeListener {
        void onProductChecked(Product product, boolean isChecked);
    }

    private final List<Product> productList = new ArrayList<>();
    private OnProductCheckedChangeListener listener;

    public ProductAdapter(List<Product> productList) {
        if (productList != null) {
            this.productList.addAll(productList);
        }
    }

    public void setOnProductCheckedChangeListener(OnProductCheckedChangeListener listener) {
        this.listener = listener;
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

        holder.checkBoxProduct.setOnCheckedChangeListener(null);
        holder.checkBoxProduct.setChecked(product.isChecked());

        holder.checkBoxProduct.setOnCheckedChangeListener((buttonView, isChecked) -> {
            product.setChecked(isChecked);
            if (listener != null) {
                listener.onProductChecked(product, isChecked);
            }
        });
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

    @SuppressLint("NotifyDataSetChanged")
    public void setProductList(List<Product> productList) {
        this.productList.clear();
        if (productList != null) {
            this.productList.addAll(new ArrayList<>(productList));
        }
        notifyDataSetChanged();
    }
}
