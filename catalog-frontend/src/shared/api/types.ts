export type RequestBody = Record<string, unknown>;

export interface ApiResponse<TData = null> {
    code: string;
    message: string | null;
    data: TData;
    ok: boolean;
}

export interface PagedResult<T> {
    items: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
}

export interface RefreshResponse {
    accessToken: string;
    refreshToken: string;
}