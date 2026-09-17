namespace onineproject.Errors
{
    public class ApiResponse
    {
        public int? Statuscode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int? statuscode, string? message = null)
        
        {
            Statuscode = statuscode;
            Message = message ?? GetDefaultMessageForStatusCode(statuscode);

        }
        private string? GetDefaultMessageForStatusCode(int? statuscode)
        {
            return statuscode switch
            {
                400 => "Bad Request",
                401 => "unauthorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null


            };
          
        }

    }
}
