using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IIMes.Infrastructure.BaseController
{
    public class CustActionFilterAttribute : ActionFilterAttribute
    {
        // private readonly IWebHostEnvironment _hostingEnvironment;
        // private readonly IModelMetadataProvider _modelMetadataProvider;

        // public CustomExceptionFilter(
        //     IWebHostEnvironment hostingEnvironment,
        //     IModelMetadataProvider modelMetadataProvider)
        // {
        //     _hostingEnvironment = hostingEnvironment;
        //     _modelMetadataProvider = modelMetadataProvider;
        // }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is OkObjectResult result)
            {
                context.Result = new OkObjectResult(BaseResult<object>.CreateOkResult(result.Value));
            }
            else if (context.Result is OkResult)
            {
                context.Result = new OkObjectResult(BaseResult<object>.CreateOkResult(null));
            }
        }

        // public override void OnException(ExceptionContext context)
        // {
        //     var ex = context.Exception;
        //     context.Result = new OkObjectResult(BaseResult<System.Exception>.CreateFailResult(ex, ex.Message));
        //     context.ExceptionHandled = true;
        // }
    }
}