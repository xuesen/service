using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;
using IIMes.Infra.Privilege.Repository;
using IIMes.Infrastructure.Exception;
using System.Linq;
using System.Collections.Generic;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Query;

namespace IIMes.Infra.Privilege.Services
{
    public partial class EmployeeService : BaseMaintainService<Employee, EmployeeDTO>, IEmployeeService
    {
        private readonly IRepository<Employee> _repEmployee;
        private readonly IRepository<Department> _repDepartment;
        private readonly IRepository<RoleEmployee> _repRoleEmployee; 
        private readonly IRepository<RoleResourcePermission> _repRoleResourcePermission;
        private readonly IRepository<ResourcePermission> _repResourcePermission;
        private readonly IRepository<Resource> _repResource;
        private readonly IRepository<Permission> _repPermission;

        public IList<PermissionsDTO> LstRootPermissions { get; set; }        
        public IList<PermissionsDTO> LstChildPermissions { get; set; } 
        public IList<TreeNode> LstTreeNode { get; set; } 
        public EmployeeService(
                                IRepository<Employee> repEmployee,
                                IRepository<Department> repDepartment,
                                IRepository<RoleEmployee> repRoleEmployee, 
                                IRepository<RoleResourcePermission> repRoleResourcePermission, 
                                IRepository<ResourcePermission> repResourcePermission,
                                IRepository<Resource> repResource,
                                IRepository<Permission> repPermission,
                                IMapper mapper)
        : base(repEmployee, mapper)
        {
            _repEmployee = repEmployee;
            _repDepartment = repDepartment;
            _repRoleEmployee = repRoleEmployee;
            _repRoleResourcePermission = repRoleResourcePermission;
            _repResourcePermission = repResourcePermission;
            _repResource = repResource;
            _repPermission = repPermission;
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

        public LoginDTO GetEmployee(LoginRequestDTO request)
        {
            LoginDTO logindto = new LoginDTO();
            // 判断用户名，密码是否正确
            var employee =  _repEmployee.GetEmployee(request.UserName, request.Password);
            if (employee != null)
            {
                // 获取用户和角色的对应关系
                var roleemployeelst = _repRoleEmployee.GetRoleEmployee(employee.Id, request.Application).ToList();
                if (roleemployeelst.Count == 0)
                {
                    throw new BizException("RAP002");
                }
                else
                {
                    logindto.User = new EmployeeDTO
                    {
                        Id = employee.Id,
                        Code = employee.Code,
                        Name = employee.Name,
                        Description = employee.Description,
                        Domain = employee.Domain,
                        Duty = employee.Duty,
                        Email = employee.Email,
                        // LeaveTime = employee.LeaveTime,
                        PhoneNo = employee.PhoneNo,
                        Type = employee.Type,
                        // Editor = employee.Editor,
                        // Cdt = employee.Cdt,
                        // Udt = employee.Udt
                    };
                    logindto.FlatPermissions = new List<FlatPermissionsDTO>();

                                      
                    foreach (var item in roleemployeelst)
                    {
                        // 根据角色id获取资源和权限的对应关系
                        var rrplst = _repRoleResourcePermission.GetRoleResourcePermission(item.SRole.Id).ToList();
                        if (rrplst.Count > 0)
                        {
                            foreach (var items in rrplst)
                            {
                                FlatPermissionsDTO flatpermission = new FlatPermissionsDTO
                                {
                                    Permissions = new List<PermissionDTO>(),
                                    resources = new ResourceDTO()
                                };                                
                                var resourcepermission =  _repResourcePermission.Get(items.SResourcePermission.Id);
                                var resource = _repResource.GetResourceById(request.Application, request.SubApplication, resourcepermission.SResource.Id);
                                if (resource == null)
                                {
                                    continue;
                                }
                                flatpermission.resources.Id = resource.Id;
                                flatpermission.resources.Application = resource.Application;
                                flatpermission.resources.Code = resource.Application;
                                // flatpermission.resources.Cdt = resource.Cdt;
                                flatpermission.resources.Description = resource.Description;
                                // flatpermission.resources.Editor = resource.Editor;
                                flatpermission.resources.Icon = resource.Icon;
                                flatpermission.resources.IsLeaf = resource.IsLeaf;
                                flatpermission.resources.Name = resource.Name;
                                flatpermission.resources.Sequence = resource.Sequence;
                                // flatpermission.resources.Udt = resource.Udt;
                                flatpermission.resources.Url = resource.Url;
                                var permission = _repPermission.Get(resourcepermission.SPermission.Id);
                                PermissionDTO permissionDTO = new PermissionDTO();
                                permissionDTO.Id = permission.Id;                                
                                permissionDTO.Code = permission.Code;
                                permissionDTO.Name = permission.Name;
                                flatpermission.Permissions.Add(permissionDTO);
                                logindto.FlatPermissions.Add(flatpermission);                                                        
                            }
                        }

                    }
                    logindto.Permissions = new List<PermissionsDTO>();
                    logindto.Permissions = GetPermissions(request.Application, request.SubApplication, logindto.FlatPermissions);                   
                }
            }
            else
            {
                throw new BizException("RAP001");   
            }
            return logindto;
        }

        private IList<PermissionsDTO> GetPermissions(string application, string subapplication, IList<FlatPermissionsDTO> flatPermissions)
        {
            // 全部根节点
            var rootlst = _repResource.GetRootResource4Login(application, subapplication).ToList();
            
            IList<PermissionsDTO> permissions = new List<PermissionsDTO>();

            for (int i = 0; i < rootlst.Count; i++)
            {
                PermissionsDTO permissionsDTO = new PermissionsDTO();
                permissionsDTO.resources = new ResourceDTO();
                permissionsDTO.resources.Id = rootlst[i].Id;
                // permissionsDTO.resources.ParentId = rootlst[i].SResource.Id;
                permissionsDTO.resources.Application = rootlst[i].Application;
                permissionsDTO.resources.Code = rootlst[i].Code;
                // flatpermission.resources.Cdt = resource.Cdt;
                permissionsDTO.resources.Description = rootlst[i].Description;
                // flatpermission.resources.Editor = resource.Editor;
                permissionsDTO.resources.Icon = rootlst[i].Icon;
                permissionsDTO.resources.IsLeaf = rootlst[i].IsLeaf;
                permissionsDTO.resources.Name = rootlst[i].Name;
                permissionsDTO.resources.Sequence = rootlst[i].Sequence;
                // flatpermission.resources.Udt = resource.Udt;
                permissionsDTO.resources.Url = rootlst[i].Url;                
                permissions.Add(permissionsDTO);
                // LstTreeNode = new List<TreeNode>();
                DealTreeNodes(permissionsDTO, flatPermissions);
                // BindRoot(permissionsDTO);
            }
            // // 根节点list
            // LstRootPermissions = permissions;
            // for (int i = 0; i < LstRootPermissions.Count; i++)
            // {
            //     PermissionsDTO node = new PermissionsDTO();
            //     node.resources = new Resource();
            //     node.resources = LstRootPermissions[i].resources;
            //     if (node.resources.IsLeaf == false)
            //     {
            //         var treenodes = _repResource.GetResourceByParentId(node.resources.Id).ToList();
            //         LstRootPermissions[i].Children = new List<PermissionsDTO>();
            //         PermissionsDTO tmp = new PermissionsDTO();
            //         for (int j = 0; j < treenodes.Count; j++)
            //         {
            //             tmp.resources = treenodes[j];
            //         }                  
            //         LstRootPermissions[i].Children.Add(tmp);
            //     }                
            // }
            // DealTreeNodes(permissions, flatPermissions);
            return permissions;
        }

        private void DealTreeNodes(PermissionsDTO treenode, IList<FlatPermissionsDTO> flatpermissions)
        {
            // LstPermissions = treenodes;
            if (treenode.resources.IsLeaf == true)
            {
                for (int j = 0; j < flatpermissions.Count; j++)
                {                        
                    if (treenode.resources.Url == flatpermissions[j].resources.Url)
                    {
                        // treenodes[i].Children = new List<FlatPermissionsDTO>();
                        treenode.Permissions = new List<PermissionDTO>();
                        for (int m = 0; m < flatpermissions[j].Permissions.Count; m++)
                        {
                            PermissionDTO per = new PermissionDTO();
                            per = flatpermissions[j].Permissions[m];
                            treenode.Permissions.Add(per);
                        }
                    }
                    else
                    {
                        // treenodes.RemoveAt(i);
                        // i--;
                    }
                }
            }
            else
            {
                GetTreeNodes(treenode, flatpermissions);
            }
        }
        private void GetTreeNodes(PermissionsDTO parent, IList<FlatPermissionsDTO> flatPermissions)
        {
            var treenodes = _repResource.GetResourceByParentId(parent.resources.Id).ToList();
            parent.Children = new List<PermissionsDTO>();
            
            IList<PermissionsDTO> permissions = new List<PermissionsDTO>();
            for (int i = 0; i < treenodes.Count; i++)
            {
                PermissionsDTO permissionsDTO = new PermissionsDTO();
                permissionsDTO.resources = new ResourceDTO();
                permissionsDTO.resources.Id = treenodes[i].Id;
                // permissionsDTO.resources.ParentId = treenodes[i].SResource.Id;
                permissionsDTO.resources.Application = treenodes[i].Application;
                permissionsDTO.resources.SubApplication = treenodes[i].SubApplication;
                permissionsDTO.resources.Code = treenodes[i].Code;
                // flatpermission.resources.Cdt = resource.Cdt;
                permissionsDTO.resources.Description = treenodes[i].Description;
                // flatpermission.resources.Editor = resource.Editor;
                permissionsDTO.resources.Icon = treenodes[i].Icon;
                permissionsDTO.resources.IsLeaf = treenodes[i].IsLeaf;
                permissionsDTO.resources.Name = treenodes[i].Name;
                permissionsDTO.resources.Sequence = treenodes[i].Sequence;
                // flatpermission.resources.Udt = resource.Udt;
                permissionsDTO.resources.Url = treenodes[i].Url;  
                
                parent.Children.Add(permissionsDTO);
                DealTreeNodes(permissionsDTO, flatPermissions);        
            }
            
        }

        // // 获取根节点
        // private void BindRoot(PermissionsDTO permissionsDTO)
        // {
        //     var treenodes = _repResource.GetResourceByParentId(permissionsDTO.resources.Id).ToList();
        //     //从dt表中查询出parentId='ABC'的行

        //     foreach (var dr in treenodes)
        //     {
        //         //if (dr["parentId"].ToString() == "ABC")
        //         //{
        //         //PermissionsDTO node = new PermissionsDTO();
        //         //node.resources = dr;
        //         // node.Text = dr["deptName"].ToString();
        //         TreeNode node = new TreeNode();
        //         node.resources = dr;
        //         node.Nodes = new List<TreeNode>();
        //         node.Nodes.Add(node);
        //         AddChild(node);
        //         //}
        //     }

        // }
        // // 绑定子节点
        // private void AddChild(TreeNode node)
        // {
        //     Resource dr2 = node.resources;//获取与根节点关联的数据行
        //     int pId = dr2.Id;//获取根节点的deptId
        
        //     //从dt表中查询出parentId=deptId的节点
        //     var rows = _repResource.GetResourceByParentId(pId).ToList();
        //     if (rows.Count == 0)
        //         return;
        //     foreach (var drow in rows) //再次遍历，添加子节点
        //     {
        //         TreeNode node3 = new TreeNode();
        //         node3.resources = drow;
        //         // node3 = drow["deptName"].ToString();
        //         node.Nodes = new List<TreeNode>();
        //         node.Nodes.Add(node3);
        //         //递归
        //         AddChild(node3);
        //     }
        // }
        [Transactional]
        public void ModifyPassword(int id, EmployeeRequestDTO request)
        {
            var employee = _repEmployee.Get(id);

            employee.Password = request.Password;
            employee.UpdateEditor = request.UpdateEditor;
            employee.Udt = DateTime.Now;
        }

        public IList<EmployeeDTO> GetEmployeeByManager(string manager)
        {
            IList<EmployeeDTO> empdtolst = new List<EmployeeDTO>();
            var employeelst = new List<Employee>();
            if (manager == "")
            {
                employeelst = _repEmployee.FindAll().ToList();
            }
            else
            {
                var emplst = _repEmployee.GetEmployeeByName(manager).ToList();                
                if (emplst.Count > 0)
                {
                    IList<int> empidlst = new List<int>();
                    foreach(var item in emplst)
                    {
                        empidlst.Add(item.Id);
                    }
                    employeelst = _repEmployee.GetEmployeeByEmpId(empidlst).ToList();
                }
            }

            foreach(var items in employeelst)
            {
                EmployeeDTO employeeDTO = new EmployeeDTO();
                employeeDTO.Id = items.Id;
                employeeDTO.Code = items.Code;
                employeeDTO.Name = items.Name;
                employeeDTO.Password = items.Password;
                employeeDTO.Domain = items.Domain;
                employeeDTO.Duty = items.Duty;
                employeeDTO.Email = items.Email;
                employeeDTO.LeaveTime = items.LeaveTime;
                employeeDTO.PhoneNo = items.PhoneNo;
                employeeDTO.Type = items.Type;
                employeeDTO.Description = items.Description;
                employeeDTO.SDepartment = new LayerDTO();
                if (items.SDepartment != null)
                {
                    var dep = _repDepartment.Get(items.SDepartment.Id);
                    employeeDTO.SDepartment.Id = dep.Id;
                    employeeDTO.SDepartment.Name = dep.Name;
                }
                employeeDTO.SEmployee = new LayerDTO();
                if (items.SEmployee != null)
                {
                    var emp = _repEmployee.Get(items.SEmployee.Id);
                    employeeDTO.SEmployee.Id = emp.Id;
                    employeeDTO.SEmployee.Name = emp.Name;
                }
                employeeDTO.Editor = new LayerEditorDTO();
                employeeDTO.Editor.Name = items.Editor;
                employeeDTO.UpdateEditor = items.UpdateEditor;
                employeeDTO.Update_time = items.Udt;
                
                empdtolst.Add(employeeDTO);
            }
            return empdtolst;
        }

        public override void Delete(int id)
        {
            Check(id);
            base.Delete(id);
        }
        protected void Check(int id)
        {
            IList<int> empidlst = new List<int>
            {
                id
            };
            var emplst = _repEmployee.GetEmployeeByEmpId(empidlst).ToList();
            if (emplst.Count > 0)
            {
                throw new BizException("MTN002");
            }
        }
    }
}
