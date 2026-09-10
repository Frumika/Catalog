import {Observer} from "@/shared/ui/observer";
import type {ReactNode} from "react";

type InfiniteScrollProps = {
    hasMore: boolean;
    onLoadMore: () => void;
    children: ReactNode;
};

export const InfiniteScroll = (
    {
        hasMore,
        onLoadMore,
        children
    }: InfiniteScrollProps) => (
    <>
        {children}
        {hasMore && <Observer onIntersect={onLoadMore}/>}
    </>
);