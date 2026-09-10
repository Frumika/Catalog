import {
    type PickupPoint,
    usePickupPoints,
    useRemovePickupPoint,
    useSelectPickupPoint,
    useSetPickupPoints
} from "@/entities/pickup-point";
import {ApiError, toApiError} from "@/shared/api";
import {pickupPointApi} from "@/entities/pickup-point/api/pickupPointApi.ts";
import {useNotify} from "@/shared/lib";


export const usePickupPointActions = () => {
    const setPickupPoints = useSetPickupPoints();
    const selectPickupPointInStore = useSelectPickupPoint();
    const removePickupPointInStore = useRemovePickupPoint();
    const currentPickupPoints = usePickupPoints();
    const notify = useNotify();


    const selectPickupPoint = async (pickupPoint: PickupPoint) => {
        const previous = currentPickupPoints;
        selectPickupPointInStore(pickupPoint);

        try {
            const updated = await pickupPointApi.select(pickupPoint.id);
            selectPickupPointInStore(updated);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось выбрать ПВЗ");
            setPickupPoints(previous);
        }
    };

    const removePickupPoint = async (pickupPoint: PickupPoint) => {
        const previous = currentPickupPoints;
        removePickupPointInStore(pickupPoint);

        try {
            await pickupPointApi.remove(pickupPoint.id);
        } catch (error) {
            const apiError: ApiError = toApiError(error);
            notify("error", apiError.message || "Не удалось удалить ПВЗ");
            setPickupPoints(previous);
        }
    };

    return {selectPickupPoint, removePickupPoint};
};