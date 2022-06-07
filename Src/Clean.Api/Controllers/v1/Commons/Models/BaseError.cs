namespace Clean.Api.Controllers.v1.Commons.Models
{
    public class BaseError
    {
        public BaseError(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
