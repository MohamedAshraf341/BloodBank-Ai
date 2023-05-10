export interface apiResponse<T> {
  success: boolean;
  message: boolean;
  data: T;
}