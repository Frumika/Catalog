import {useCallback, useEffect, useMemo, useRef, useState} from "react";
import type {CartPosition} from "@/entities/cart";
import {selectionStorage} from "@/pages/cart/model/selectionStorage.ts";


export const useCartSelection = (cartPositions: CartPosition[]) => {
    const [selectedIds, setSelectedIds] = useState<number[]>(() => selectionStorage.getSavedIds() ?? []);
    const isInitializedRef = useRef(false);


    useEffect(() => {
        if (cartPositions.length === 0) return;

        if (!isInitializedRef.current) {
            const syncedIds = selectionStorage.syncPositions(cartPositions);
            setSelectedIds(syncedIds);
            isInitializedRef.current = true;
        } else {
            const currentIds = cartPositions.map((p) => p.productId);
            setSelectedIds((prev) => {
                const valid = prev.filter((id) => currentIds.includes(id));
                selectionStorage.saveIds(valid);
                return valid;
            });
        }
    }, [cartPositions]);


    const togglePosition = useCallback((cartPosition: CartPosition) => {
        const updatedIds = selectionStorage.togglePosition(cartPosition.productId);
        setSelectedIds(updatedIds);
    }, []);

    const toggleAll = useCallback(() => {
        const allIds = cartPositions.map((p) => p.productId);
        const newSelected = selectedIds.length === cartPositions.length ? [] : allIds;

        selectionStorage.saveIds(newSelected);
        setSelectedIds(newSelected);
    }, [cartPositions, selectedIds.length]);

    const isPositionsSelected = useCallback(
        (cartPosition: CartPosition) => selectedIds.includes(cartPosition.productId),
        [selectedIds]
    );

    const selectedPositions = useMemo(() => {
        return cartPositions.filter((p) => selectedIds.includes(p.productId));
    }, [cartPositions, selectedIds]);

    const isAllSelected = cartPositions.length > 0 && selectedIds.length === cartPositions.length;

    return {
        selectedPositions,
        isPositionsSelected,
        togglePosition,
        toggleAll,
        isAllSelected,
    };
};