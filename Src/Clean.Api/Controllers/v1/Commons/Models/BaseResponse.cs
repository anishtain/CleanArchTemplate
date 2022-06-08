namespace Clean.Api.Controllers.v1.Commons.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse(bool isSuccess, int statusCode, T data, BaseError error = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Data = data;
            Error = error;
        }

        public bool IsSuccess { get; set; }

        public int StatusCode { get; set; }

        public T Data { get; set; }

        public BaseError Error { get; set; }
    }
}
