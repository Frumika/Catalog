package com.example.catalog.cart;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.catalog.R;
import com.example.catalog.core.Product;

import java.util.ArrayList;
import java.util.List;

public class CartProductAdapter extends RecyclerView.Adapter<CartProductAdapter.CartProducViewHolder> {

    public interface onProductRemoveListener {
        void onProductRemove(int position);
    }

    public void setOnProductRemoveListener(onProductRemoveListener listener) {
        this.listener = listener;
    }

    private List<Product> cartList;
    private onProductRemoveListener listener;

    public CartProductAdapter(List<Product> cartList) {
        if (cartList != null) {
            this.cartList = cartList;
        }
    }

    @NonNull
    @Override
    public CartProductAdapter.CartProducViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View productView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.item_cart_product, parent, false);
        return new CartProductAdapter.CartProducViewHolder(productView);
    }

    @Override
    public void onBindViewHolder(@NonNull CartProducViewHolder holder, int position) {
        Product product = cartList.get(position);

        holder.textProductName.setText(product.getName());
        holder.textProductPrice.setText(String.valueOf(product.getPrice()));
        holder.imageProduct.setImageResource(product.getImageResId());

        holder.imageTrash.setOnClickListener(v -> {
            if (listener != null) {
                int currentPosition = holder.getAdapterPosition();

                if (currentPosition != RecyclerView.NO_POSITION) {
                    listener.onProductRemove(currentPosition);
                }
            }
        });
    }

    @Override
    public int getItemCount() {
        return cartList.size();
    }


    public static class CartProducViewHolder extends RecyclerView.ViewHolder {

        ImageView imageProduct;
        ImageView imageTrash;
        TextView textProductName;
        TextView textProductPrice;

        public CartProducViewHolder(@NonNull View itemView) {
            super(itemView);
            imageProduct = itemView.findViewById(R.id.cartItem_imageProduct__image);
            imageTrash = itemView.findViewById(R.id.cartItem_imageView__trashImage);
            textProductName = itemView.findViewById(R.id.cartItem_textView__productName);
            textProductPrice = itemView.findViewById(R.id.cartItem_textView__productPrice);
        }
    }
}
