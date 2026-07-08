import {useCallback} from "react";
import {cartApi} from "../api/cartApi.ts";
import {usePaginatedList} from "@/shared/lib/usePaginatedList.ts";



export const useCartList = () => {
    const fetchPage = useCallback(
        () => cartApi.getCartPositions(),
        []
    );

    // return usePaginatedList(fetchPage,);
};