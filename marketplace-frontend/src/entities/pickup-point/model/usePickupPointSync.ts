import {useClearPickupPointsStore, useSetPickupPoints} from "@/entities/pickup-point";
import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {pickupPointApi} from "@/entities/pickup-point/api/pickupPointApi.ts";
import {useNotify} from "@/shared/lib";


export const usePickupPointSync = (isAuthenticated: boolean) => {
    const setPickupPoints = useSetPickupPoints();
    const clearPickupPointsStore = useClearPickupPointsStore();
    const [isLoading, setIsLoading] = useState(false);
    const notify = useNotify();


    useEffect(() => {
        if (!isAuthenticated) {
            clearPickupPointsStore();
            return;
        }

        setIsLoading(true);
        pickupPointApi.getAll()
            .then(pickupPoints => setPickupPoints(pickupPoints))
            .catch(error => {
                const apiError: ApiError = toApiError(error);
                notify("error", apiError.message || "Не удалось получить информацию о пунктах выдачи заказов");
            })
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    return {isLoading}
}