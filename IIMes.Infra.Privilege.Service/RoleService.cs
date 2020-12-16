using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Infrastructure.Exception;

namespace IIMes.Infra.Privilege.Services
{
    public partial class RoleService : BaseMaintainService<Role, RoleDTO>, IRoleService
    {
        public RoleService(
                                IRepository<Role> rep,
                                IMapper mapper)
        : base(rep, mapper)
        {
        }

        protected override void CheckDuplication(RoleDTO obj)
        {
            var bizKeyName = "Name";
            var t = typeof(Role);
            var keyProperty = t.GetProperty(bizKeyName);
            if (keyProperty != null)
            {
                var domain = Mapper.Map<RoleDTO, Role>(obj);
                var keyValue = keyProperty.GetValue(domain);
                var duplications = Rep.Query(new QueryParameter() { Name = bizKeyName, Value = keyValue, Fuzzy = false });
                if (duplications != null && duplications.Count > 0)
                {
                    throw new BizException("MTN001", keyValue);
                }
            }
        }
    }
}
