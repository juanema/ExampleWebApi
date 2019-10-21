using ExampleWebApi.Core;
using ExampleWebApi.Core.Communications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;

namespace ExampleWebApi.ActionFilters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Guard.Against<ArgumentNullException>(context == null, $"Parameter {nameof(context)} is null");

            HttpStatusCode status = HttpStatusCode.InternalServerError;
            IList<string> messages = new List<string>
            {
                "Server error occurred.",
                context.Exception.Message
            };
            //
            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(new ApiBaseResponse(false, messages)); ;
        }
    }
}
