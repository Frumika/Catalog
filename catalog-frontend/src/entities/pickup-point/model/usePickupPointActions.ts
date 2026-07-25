import {
    type PickupPoint,
    usePickupPoints,
    useRemovePickupPoint,
    useSelectPickupPoint,
    useSetPickupPoints
} from "@/entities/pickup-point";
import {useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {pickupPointApi} from "@/entities/pickup-point/api/pickupPointApi.ts";


export const usePickupPointActions = () => {
    const setPickupPoints = useSetPickupPoints();
    const selectPickupPointInStore = useSelectPickupPoint();
    const removePickupPointInStore = useRemovePickupPoint();
    const currentPickupPoints = usePickupPoints();

    const [error, setError] = useState<ApiError | null>(null);

    const selectPickupPoint = async (pickupPoint: PickupPoint) => {
        const previous = currentPickupPoints;
        selectPickupPointInStore(pickupPoint);

        try {
            const updated = await pickupPointApi.select(pickupPoint.id);
            selectPickupPointInStore(updated);
        } catch (error) {
            setError(toApiError(error));
            setPickupPoints(previous);
        }
    };

    const removePickupPoint = async (pickupPoint: PickupPoint) => {
        const previous = currentPickupPoints;
        removePickupPointInStore(pickupPoint);

        try {
            await pickupPointApi.remove(pickupPoint.id);
        } catch (error) {
            setError(toApiError(error));
            setPickupPoints(previous);
        }
    };

    return {selectPickupPoint, removePickupPoint, error};
};