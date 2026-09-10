import {ApiError, toApiError} from "@/shared/api";
import {orderApi} from "@/entities/order/api/orderApi.ts";
import {useNotify} from "@/shared/lib";


export const useOrderActions = () => {
    const notify = useNotify();

    const getOrderById = async (orderId: number) => {
        try {
            return await orderApi.getById(orderId);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось получить информацию о заказе");
        }
    };

    const makeOrder = async (productIds: number[], pickupPointId: number) => {
        try {
            return await orderApi.makeOrder(productIds, pickupPointId);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось создать заказ");
        }
    };

    const payOrder = async (orderId: number) => {
        try {
            return await orderApi.payOrder(orderId);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось оплатить заказ");
        }
    }

    const cancelOrder = async (orderId: number) => {
        try {
            await orderApi.cancelOrder(orderId);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось отменить заказ");
        }
    }

    return {
        getOrderById,
        makeOrder,
        payOrder,
        cancelOrder,
    }
}