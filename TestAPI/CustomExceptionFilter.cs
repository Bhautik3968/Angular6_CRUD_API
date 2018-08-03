using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using TestAPI.DataContext;
using TestAPI.Models;
namespace TestAPI
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            string exceptionMessage = string.Empty;
            string exceptionStackTrace = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
                exceptionStackTrace = actionExecutedContext.Exception.StackTrace;
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
                exceptionStackTrace = actionExecutedContext.Exception.InnerException.StackTrace;
            }
            //----Save error to database-----
            ErrorDataContext _objContext = new ErrorDataContext();
            Error _objError = new Error();
            _objError.ErrorMessage = exceptionMessage;
            _objError.ErrorDescription = exceptionStackTrace;
            _objContext.SaveError(_objError);
            //----------------------------------
            var response = new HttpResponseMessage()
            {
                StatusCode= HttpStatusCode.InternalServerError,
                Content = new StringContent("An unexpected fault happened."),
                ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            };
            actionExecutedContext.Response = response;
        }
    }
}