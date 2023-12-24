namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        //status code
        public int StatusCode { get; set; }
        //üzenet
        public string Message { get; set; }
        //részletek
        public string Details { get; set; }
    }
}
