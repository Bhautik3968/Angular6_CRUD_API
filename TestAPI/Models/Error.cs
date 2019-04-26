using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPI.Models
{
    public class Error
    {
        public int ID { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDescription { get; set; }        
    }
}