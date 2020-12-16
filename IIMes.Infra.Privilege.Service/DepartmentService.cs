// using System.Net.Cache;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using IIMes.Services.Core.Model;
// using IIMes.Services.Maintain.Model;
// using MassTransit;
// using MassTransit.Util;
// using IIMes.Services.Core.Message;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;
using IIMes.Infra.Privilege.Repository;
using System.Linq;
using System.Collections.Generic;

namespace IIMes.Infra.Privilege.Services
{
    public partial class DepartmentService : BaseMaintainService<Department, DepartmentDTO>, IDepartmentService
    {
        private readonly IRepository<Department> _repDepartment;        
        public DepartmentService(IRepository<Department> repDepartment, IMapper mapper)
        : base(repDepartment, mapper)
        { 
            _repDepartment = repDepartment;
        }
        public IList<DepartmentDTO> GetDepartment(string departmentname)
        {
            IList<DepartmentDTO> depdtolst = new List<DepartmentDTO>();
            var departmentlst = new List<Department>();
            if (departmentname == "")
            {
                departmentlst = _repDepartment.FindAll().ToList();
            }
            else
            {
                var deplst = _repDepartment.GetDepartment(departmentname).ToList();                
                if (deplst.Count > 0)
                {
                    IList<int> depidlst = new List<int>();
                    foreach(var item in deplst)
                    {
                        depidlst.Add(item.Id);
                    }
                    departmentlst = _repDepartment.GetDepartmentByDepId(depidlst).ToList();
                }
            }

            foreach(var items in departmentlst)
            {
                DepartmentDTO departmentDTO = new DepartmentDTO();
                departmentDTO.Id = items.Id;
                departmentDTO.Code = items.Code;
                departmentDTO.Name = items.Name;
                departmentDTO.Company = items.Company;
                departmentDTO.Description = items.Description;
                departmentDTO.SDepartment = new LayerDTO();
                if (items.SDepartment != null)
                {
                    var dep = _repDepartment.Get(items.SDepartment.Id);
                    departmentDTO.SDepartment.Id = dep.Id;
                    departmentDTO.SDepartment.Name = dep.Name;
                }
                departmentDTO.Editor = new LayerEditorDTO();
                departmentDTO.Editor.Name = items.Editor;
                departmentDTO.UpdateEditor = items.UpdateEditor;
                departmentDTO.Update_time = items.Udt;
                
                depdtolst.Add(departmentDTO);
            }
            return depdtolst;
        }
        public override void Delete(int id)
        {
            Check(id);
            base.Delete(id);
        }
        protected void Check(int id)
        {
            IList<int> depidlst = new List<int>
            {
                id
            };
            var emplst = _repDepartment.GetDepartmentByDepId(depidlst).ToList();
            if (emplst.Count > 0)
            {
                throw new BizException("MTN002");
            }
        }
    }
}
