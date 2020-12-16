using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Query;
using IIMes.Infra.Privilege.Repository;

namespace IIMes.Infra.Privilege.Services
{
    public partial class RoleEmployeeService : BaseMaintainService<RoleEmployee, RoleEmployeeDTO>, IRoleEmployeeService
    {
        private readonly IRepository<RoleEmployee> _repRoleEmployee;
        public RoleEmployeeService(
                                IRepository<RoleEmployee> repRoleEmployee,
                                IMapper mapper)
        : base(repRoleEmployee, mapper)
        {
            _repRoleEmployee = repRoleEmployee;
        }
        protected override void CheckDuplication(RoleEmployeeDTO obj)
        {
            var roleemployee = _repRoleEmployee.GetRoleEmployeeByRoleId(obj.EmployeeId, obj.RoleId);
            if (roleemployee != null)
            {
                throw new BizException("MTN001", roleemployee.SEmployee.Code);
            }
        }
    }
}
