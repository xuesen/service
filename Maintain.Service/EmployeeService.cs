using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;

namespace IIMes.Services.Maintain.Services
{
    public partial class EmployeeService : BaseMaintainService<SEmployee, EmployeeDTO>, IEmployeeService
    {
        public EmployeeService(IRepository<SEmployee> rep, IMapper mapper)
        : base(rep, mapper)
        {
        }

        public override void Add(EmployeeDTO obj)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(obj.Code);
            string result = Convert.ToBase64String(md5.ComputeHash(bytes));
            obj.Password = result;

            CheckDuplication(obj);
            base.Add(obj);
        }
    }
}
