using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultStatusCode(statusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You have made a Bad Request",
                401 => "Authorized, you have not ",
                404 => "Have Not Found",
                500 => "Have Error on server side",
                _ => null
            };
        }

    }
}