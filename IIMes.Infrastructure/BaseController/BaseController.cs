using Microsoft.AspNetCore.Mvc;

namespace IIMes.Infrastructure.BaseController
{
    [ApiController]
    [Route("api/[controller]")]
    [CustExceptionFilter]
    [CustActionFilter]
    public class BaseController : ControllerBase
    {
    }
}