using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;
using IIMes.Infrastructure.Aspect;
using IIMes.Infra.Privilege.Repository;

namespace IIMes.Infra.Privilege.Services
{
    public partial class RoleResourcePermissionService : IRoleResourcePermissionService
    {
        private readonly IRepository<RoleResourcePermission> _rep;        
        public RoleResourcePermissionService(
                                IRepository<RoleResourcePermission> rep)
        {
            _rep = rep;
        }

        [Transactional]
        public int Save(RoleSourcePermissionRequestDTO request)
        {
            var ret = 0;
            _rep.DeleteByRoleId(request.RoleId);

            for (int i = 0; i < request.NodesChecked.Count; i++)
            {
                for (int j = 0; j < request.ResourcePermissions.Count; j++)
                {
                   if (request.NodesChecked[i] == request.ResourcePermissions[j].ResourceId)
                   {
                       for (int m = 0; m < request.ResourcePermissions[j].ResourcePermissionIds.Count; m++)
                       {
                            RoleResourcePermission rrp = new RoleResourcePermission
                            {
                                SRole = new Role()
                            };
                            rrp.SRole.Id = request.RoleId;
                            rrp.SResourcePermission = new ResourcePermission
                            {
                                Id = request.ResourcePermissions[j].ResourcePermissionIds[m]
                            };
                            rrp.Editor = request.Editor;
                            rrp.UpdateEditor = request.Editor;

                            _rep.Add(rrp);
                       }
                   }
                }
            }
            return ret;
        }
    }
}
