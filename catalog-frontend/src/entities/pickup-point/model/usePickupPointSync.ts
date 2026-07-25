import {useClearPickupPointsStore, useSetPickupPoints} from "@/entities/pickup-point";
import {useEffect, useState} from "react";
import {ApiError, toApiError} from "@/shared/api";
import {pickupPointApi} from "@/entities/pickup-point/api/pickupPointApi.ts";


export const usePickupPointSync = (isAuthenticated: boolean) => {
    const setPickupPoints = useSetPickupPoints();
    const clearPickupPointsStore = useClearPickupPointsStore();

    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<ApiError | null>(null);

    useEffect(() => {
        if (!isAuthenticated) {
            clearPickupPointsStore();
            return;
        }

        setIsLoading(true);
        pickupPointApi.getAll()
            .then(pickupPoints => setPickupPoints(pickupPoints))
            .catch(error => setError(toApiError(error)))
            .finally(() => setIsLoading(false));
    }, [isAuthenticated]);

    return {
        isLoading,
        error,
    }
}