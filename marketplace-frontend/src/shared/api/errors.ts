export class ApiError extends Error {
    code: string;

    constructor(code: string, message: string | null) {
        super(message ?? code);
        this.code = code;
        this.name = 'ApiError';
    }
}

export function toApiError(error: unknown): ApiError {
    if (error instanceof ApiError) return error;
    return new ApiError('unknown_error', 'Что-то пошло не так');
}