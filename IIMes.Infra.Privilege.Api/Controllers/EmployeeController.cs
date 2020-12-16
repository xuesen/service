using System;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Security.Cryptography;

namespace IIMes.Infra.Privilege.Controllers
{
    public class EmployeeController : BaseController<Employee, EmployeeDTO>
    {      
        private readonly IEmployeeService _employeeService;
        public EmployeeController(ILogger<Employee> logger, IEmployeeService employeeService)
        : base(logger, employeeService)
        {
            _employeeService = employeeService;            
        }

        // 登录
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody]LoginRequestDTO request)
        {
            var pwd = Base64Encode(request.Password);
            request.Password = pwd;
            var result = _employeeService.GetEmployee(request);
            return Ok(result);
        }

        /// <summary>
        /// 将字符串转换成base64格式,使用UTF8字符集
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <returns></returns>
        private static string Base64Encode(string content)
        {
             // 初始化MD5对象
             MD5CryptoServiceProvider serviceProvider = new MD5CryptoServiceProvider();
             // 把要加密的内容装换成字节数组
            byte[] inputBytes = Encoding.UTF8.GetBytes(content);
             // 转换指定字节的哈希值
             byte[] outPutBytes = serviceProvider.ComputeHash(inputBytes);
             // 转换成64位的字符串
            var resultStr = Convert.ToBase64String(outPutBytes);
            return resultStr;
        }

        [Route("password/{id}")]
        [HttpPut]
        public ActionResult ModifyPassword([FromRoute] int id, [FromBody]EmployeeRequestDTO request)
        {
            var pwd = Base64Encode(request.Password);
            request.Password = pwd;
            _employeeService.ModifyPassword(id, request);
            return Ok();
        }

        // 查询条件是经理的查询接口
        [Route("manager")]
        [HttpPost]
        public ActionResult GetEmployeeByManager([FromBody]EmployeeManagerRequestDTO request)
        {
            var employeeObj = _employeeService.GetEmployeeByManager(request.Manager);
            return Ok(employeeObj);
        }
    }
}