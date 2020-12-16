using IIMes.Infrastructure.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IIMes.Infrastructure.BaseController
{
    public class CustExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            if (ex is BizException exception)
            {
                if (string.IsNullOrEmpty(exception.Message))
                {
                    var message = GetMessage(exception, context);
                    exception.SetMessage(message);
                }

                context.Result = new OkObjectResult(BaseResult<BizException>.CreateFailResult(exception, ex.Message));
            }
            else
            {
                context.Result = new OkObjectResult(BaseResult<System.Exception>.CreateFailResult(ex, ex.Message));
            }

            context.ExceptionHandled = true;
        }

        private string GetMessage(BizException exception, ExceptionContext context)
        {
            var provider = (IMessageProvider)context.HttpContext.RequestServices.GetService(typeof(IMessageProvider));
            var message = provider.GetMessage(exception.ErrCode);
            object[] args = exception.Args;
            if (args != null)
            {
                message = string.Format(message, args);
            }

            return message;
        }
    }
}