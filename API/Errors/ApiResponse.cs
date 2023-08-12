namespace API.Errors
{
    public class ApiResponse
    {
        public int statusCode {  get; set; }
        public string message { get; set; }
        public ApiResponse(int statusCode , string message=null)
        {
            this.statusCode = statusCode;
            this.message = message ?? GetResponseMessageFromCode(statusCode) ;
        }

        private string GetResponseMessageFromCode(int statuscode)
        {
            return statuscode switch {
                200 => "Successfull Response",
                401 => "Bad User Request",
                404 => "Not Found asset",
                500 => "Internal Server error", //will not work because the framework will handle the exception
                _ => ""
            };
        }

        public void methodTobeInvoked(int code)
        {
            Console.WriteLine("This method has been invoked with code " + code);
        }
    }
}
