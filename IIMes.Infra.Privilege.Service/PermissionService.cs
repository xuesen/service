using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;
using System.Collections.Generic;
using IIMes.Infra.Privilege.Repository;
using System.Linq;

namespace IIMes.Infra.Privilege.Services
{
    public partial class PermissionService : BaseMaintainService<Permission, PermissionDTO>, IPermissionService
    {
        private readonly IRepository<RoleResourcePermission> _repRoleResourcePermission;
        private readonly IRepository<ResourcePermission> _repResourcePermission;
        private readonly IRepository<Resource> _repResource;
        private readonly IRepository<Permission> _repPermission;
        public PermissionService(
                                IRepository<RoleResourcePermission> repRoleResourcePermission, 
                                IRepository<ResourcePermission> repResourcePermission,
                                IRepository<Resource> repResource,
                                IRepository<Permission> repPermission,
                                IMapper mapper)
        : base(repPermission, mapper)
        {
            _repRoleResourcePermission = repRoleResourcePermission;
            _repResourcePermission = repResourcePermission;
            _repResource = repResource;
            _repPermission = repPermission;
        }

        // 获取权限树
        public IList<PermissionTreeDTO> GetPermissionTree(string application)
        {
            IList<PermissionTreeDTO> permissionlst = new List<PermissionTreeDTO>();
            permissionlst = GetPermissionTrees(application);
            return permissionlst;
        }

        private IList<PermissionTreeDTO> GetPermissionTrees(string application)
        {
            // 全部根节点
            var rootlst = _repResource.GetRootResource(application).ToList();
            
            IList<PermissionTreeDTO> permissions = new List<PermissionTreeDTO>();

            for (int i = 0; i < rootlst.Count; i++)
            {
                PermissionTreeDTO permissionsDTO = new PermissionTreeDTO();
                permissionsDTO.Resources = new ResourceDTO();
                permissionsDTO.Resources.Id = rootlst[i].Id;
                // permissionsDTO.resources.ParentId = rootlst[i].SResource.Id;
                permissionsDTO.Resources.Application = rootlst[i].Application;
                permissionsDTO.Resources.Code = rootlst[i].Code;
                // flatpermission.resources.Cdt = resource.Cdt;
                permissionsDTO.Resources.Description = rootlst[i].Description;
                // flatpermission.resources.Editor = resource.Editor;
                permissionsDTO.Resources.Icon = rootlst[i].Icon;
                permissionsDTO.Resources.IsLeaf = rootlst[i].IsLeaf;
                permissionsDTO.Resources.Name = rootlst[i].Name;
                permissionsDTO.Resources.Sequence = rootlst[i].Sequence;
                // flatpermission.resources.Udt = resource.Udt;
                permissionsDTO.Resources.Url = rootlst[i].Url;                
                permissions.Add(permissionsDTO);
                // LstTreeNode = new List<TreeNode>();
                permissionsDTO.ResourcePermissions = new List<ResourcePermissionsDTO>();
                
                var rplst = _repResourcePermission.GetResourcePermission(rootlst[i].Id);
                foreach (var item in rplst)
                {
                    ResourcePermissionsDTO rpdto = new ResourcePermissionsDTO();
                    rpdto.ResourcePermissionId = item.Id;
                    PermissionDTO permissionDTO = new PermissionDTO();
                    permissionDTO.Id = item.SPermission.Id;                                
                    permissionDTO.Code = item.SPermission.Code;
                    permissionDTO.Name = item.SPermission.Name;                    
                    rpdto.Permission = permissionDTO;
                    permissionsDTO.ResourcePermissions.Add(rpdto);
                }

                DealTreeNodes(permissionsDTO);
                // BindRoot(permissionsDTO);
            }
            return permissions;
        }

        private void DealTreeNodes(PermissionTreeDTO parent)
        {
            var treenodes = _repResource.GetResourceByParentId(parent.Resources.Id).ToList();
            parent.Children = new List<PermissionTreeDTO>();
            
            IList<PermissionTreeDTO> permissions = new List<PermissionTreeDTO>();
            for (int i = 0; i < treenodes.Count; i++)
            {
                PermissionTreeDTO permissionsDTO = new PermissionTreeDTO();
                permissionsDTO.Resources = new ResourceDTO();
                permissionsDTO.Resources.Id = treenodes[i].Id;
                // permissionsDTO.resources.ParentId = treenodes[i].SResource.Id;
                permissionsDTO.Resources.Application = treenodes[i].Application;
                permissionsDTO.Resources.Code = treenodes[i].Code;
                // flatpermission.resources.Cdt = resource.Cdt;
                permissionsDTO.Resources.Description = treenodes[i].Description;
                // flatpermission.resources.Editor = resource.Editor;
                permissionsDTO.Resources.Icon = treenodes[i].Icon;
                permissionsDTO.Resources.IsLeaf = treenodes[i].IsLeaf;
                permissionsDTO.Resources.Name = treenodes[i].Name;
                permissionsDTO.Resources.Sequence = treenodes[i].Sequence;
                // flatpermission.resources.Udt = resource.Udt;
                permissionsDTO.Resources.Url = treenodes[i].Url;  
                
                parent.Children.Add(permissionsDTO);
                permissionsDTO.ResourcePermissions = new List<ResourcePermissionsDTO>();
                
                var rplst = _repResourcePermission.GetResourcePermission(treenodes[i].Id);
                foreach (var item in rplst)
                {
                    ResourcePermissionsDTO rpdto = new ResourcePermissionsDTO();
                    rpdto.ResourcePermissionId = item.Id;
                    PermissionDTO permissionDTO = new PermissionDTO();
                    permissionDTO.Id = item.SPermission.Id;                                
                    permissionDTO.Code = item.SPermission.Code;
                    permissionDTO.Name = item.SPermission.Name;                    
                    rpdto.Permission = permissionDTO;                    
                    permissionsDTO.ResourcePermissions.Add(rpdto);
                }
                DealTreeNodes(permissionsDTO);        
            }
            
        }

        // 获取该角色的权限树
        public IList<PermissionTreeByRoleDTO> GetPermissionTreeByRole(string application, int roleid)
        {
            IList<PermissionTreeByRoleDTO> permissionlst = new List<PermissionTreeByRoleDTO>();

            var rrplst = _repRoleResourcePermission.GetRoleResourcePermission(roleid).ToList();
            // resource_id list
            IList<ResourceDTO> rlst = new List<ResourceDTO>();
            foreach (var item in rrplst)
            {
                var rp = _repResourcePermission.Get(item.SResourcePermission.Id);
                var resource = _repResource.Get(rp.SResource.Id);
                ResourceDTO resourceDTO = new ResourceDTO();
                resourceDTO.Application = resource.Application;
                resourceDTO.Code = resource.Code;
                resourceDTO.Description = resource.Description;
                resourceDTO.UpdateEditor = resource.UpdateEditor;
                resourceDTO.Icon = resource.Icon;
                resourceDTO.Id = resource.Id;
                resourceDTO.IsLeaf = resource.IsLeaf;
                resourceDTO.Sequence = resource.Sequence;
                resourceDTO.Url = resource.Url;
                resourceDTO.SubApplication = resource.SubApplication;
                rlst.Add(resourceDTO);
            }
            // 去掉重复的resource_id
            HashSet<ResourceDTO> hs = new HashSet<ResourceDTO>(rlst);
            rlst = hs.ToList();
         
            for (int i = 0; i < rlst.Count; i++)
            { 
                PermissionTreeByRoleDTO ptrdto = new PermissionTreeByRoleDTO();
                ptrdto.Resource = new ResourceDTO();
                ptrdto.Resource = rlst[i];

                IList<ResourcePermissionsDTO> plst = new List<ResourcePermissionsDTO>();
                foreach (var item in rrplst)
                {
                    var rp = _repResourcePermission.Get(item.SResourcePermission.Id);

                    if (rp.SResource.Id == rlst[i].Id)
                    {
                        ResourcePermissionsDTO resourcePermissionDTO = new ResourcePermissionsDTO();                          
                        resourcePermissionDTO.ResourcePermissionId = rp.Id; 
                        PermissionDTO permissionDTO = new PermissionDTO();
                        permissionDTO.Id = rp.SPermission.Id;                                
                        permissionDTO.Code = rp.SPermission.Code;
                        permissionDTO.Name = rp.SPermission.Name;                    
                        resourcePermissionDTO.Permission = permissionDTO;                   
                        plst.Add(resourcePermissionDTO);
                    }
                }
                ptrdto.ResourcePermissions = new List<ResourcePermissionsDTO>();
                ptrdto.ResourcePermissions = plst;
                permissionlst.Add(ptrdto);
            }
            return permissionlst;
        }

    }
}
