export type ServerResponseModel<T> = {
    data?: T;
    message: string;
    success: string;
};
