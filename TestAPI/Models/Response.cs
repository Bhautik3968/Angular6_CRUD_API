using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPI.Models
{
    public class Response
    {
        public bool Success { get; set; } = true;
        public string ResponseString { get; set; } = string.Empty;

        public int StatusCode { get; set; } = 0;

        public Response()
        {           
        }
        public Response(bool Success, string ResponseString)
        {
            this.Success = Success;
            this.ResponseString = ResponseString;
        }

        public Response(bool Success, int StatusCode, string ResponseString)
        {
            this.Success = Success;
            this.StatusCode = StatusCode;
            this.ResponseString = ResponseString;
        }
    }
}