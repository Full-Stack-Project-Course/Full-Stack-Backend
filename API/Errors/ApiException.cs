namespace API.Errors
{
    public class ApiException:ApiResponse
    {
        public string Details { get; set; } 
        public ApiException(int statusCode ,  string message=null , string stacktrace = null):base(statusCode, message) 
        {

            Details = stacktrace;

        }
    }
}
