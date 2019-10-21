using ExampleWebApi.Core.Domain.Services.Communication;
using ExampleWebApi.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Net;

namespace ExampleWebApi.Core.ActionFilters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ThrowExceptionIfNull();
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
