using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using ExampleWebApi.Core.Extensions;

namespace ExampleWebApi.Core.ActionFilters
{

    /// <summary>
    /// Validate the ModelState on action executing
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ThrowExceptionIfNull();
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.ThrowExceptionIfNull();
            next.ThrowExceptionIfNull();
            // Do something before the action executes.
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            // next() calls the action method.            
            await next().ConfigureAwait(false);
            // resultContext.Result is set.
            // Do something after the action executes.
        }

    }
}
