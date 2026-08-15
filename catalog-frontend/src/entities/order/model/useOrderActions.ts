import {useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {orderApi} from "@/entities/order/api/orderApi.ts";


export const useOrderActions = () => {

    const getOrderById = async (orderId: number) => {
        try {
            return await orderApi.getById(orderId);
        } catch (error) {
            throw toApiError(error);
        }
    };

    const makeOrder = async (productIds: number[], pickupPointId: number) => {
        try {
            return await orderApi.makeOrder(productIds, pickupPointId);
        } catch (error) {
            throw toApiError(error);
        }
    };

    const payOrder = async (orderId: number) => {
        try {
            return await orderApi.payOrder(orderId);
        } catch (error) {
            throw toApiError(error);
        }
    }

    const cancelOrder = async (orderId: number) => {
        try {
            await orderApi.cancelOrder(orderId);
        } catch (error) {
            throw toApiError(error);
        }
    }

    return {
        getOrderById,
        makeOrder,
        payOrder,
        cancelOrder,
    }
}