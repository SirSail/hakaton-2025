namespace Core.API
{
    public class ApiResponse<T> where T:class
    {
        public T Data { get; set; }
        public ApiError Error { get; set; }

        public bool IsSuccess => Error is null && Data is not null;

    }
}
