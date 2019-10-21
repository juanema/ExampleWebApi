using ExampleWebApi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace ExampleWebApi.ActionFilters
{

    /// <summary>
    /// Validate the ModelState on action executing
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Guard.Against<ArgumentNullException>(context == null, $"Parameter {nameof(context)} is null");
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Guard.Against<ArgumentNullException>(context == null, $"Parameter {nameof(context)} is null");
            Guard.Against<ArgumentNullException>(next == null, $"Parameter {nameof(next)} is null");
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
