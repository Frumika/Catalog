export interface ApiResponse<TData = null> {
    code: string;
    message: string | null;
    data: TData;
    ok: boolean;
}

export type RequestBody = Record<string, unknown>;