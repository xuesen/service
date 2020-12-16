using AutoMapper;
using IIMes.Infra.Privilege.Model;
using IIMes.Infra.Privilege.Interface;

namespace Privilege.Interface.AutoMapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // 部门
            CreateMap<Department, DepartmentDTO>()
                        .ForPath(dto => dto.SDepartment.Id, (map) => map.MapFrom(m => m.SDepartment.Id))
                        .ForPath(dto => dto.SDepartment.Name, (map) => map.MapFrom(m => m.SDepartment.Name))
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<DepartmentDTO, Department>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code))
                        .ForMember(map => map.SDepartment, opt =>
                        {
                            opt.PreCondition(dto => (dto.SDepartment != null));
                            opt.MapFrom(dto => dto.SDepartment);
                        });
            CreateMap<LayerDTO, Department>();
            // 员工
            CreateMap<Employee, EmployeeDTO>()
                        .ForPath(dto => dto.SDepartment.Id, (map) => map.MapFrom(m => m.SDepartment.Id))
                        .ForPath(dto => dto.SDepartment.Name, (map) => map.MapFrom(m => m.SDepartment.Name))
                        .ForPath(dto => dto.SEmployee.Id, (map) => map.MapFrom(m => m.SEmployee.Id))
                        .ForPath(dto => dto.SEmployee.Name, (map) => map.MapFrom(m => m.SEmployee.Name))
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<EmployeeDTO, Employee>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code))
                        .ForMember(map => map.SEmployee, opt =>
                        {
                            opt.PreCondition(dto => (dto.SDepartment != null));
                            opt.MapFrom(dto => dto.SDepartment);
                        })                        
                        .ForMember(map => map.SEmployee, opt =>
                        {
                            opt.PreCondition(dto => (dto.SEmployee != null));
                            opt.MapFrom(dto => dto.SEmployee);
                        });
            CreateMap<LayerDTO, Employee>();
            // 角色
            CreateMap<Role, RoleDTO>()
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<RoleDTO, Role>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
            CreateMap<LayerDTO, Role>();
            // 角色绑定用户
            CreateMap<RoleEmployee, RoleEmployeeDTO>()
                        .ForPath(dto => dto.EmployeeId, (map) => map.MapFrom(m => m.SEmployee.Id))
                        .ForPath(dto => dto.EmployeeCode, (map) => map.MapFrom(m => m.SEmployee.Code))
                        .ForPath(dto => dto.EmployeeName, (map) => map.MapFrom(m => m.SEmployee.Name))
                        .ForPath(dto => dto.RoleId, (map) => map.MapFrom(m => m.SRole.Id))
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor))
                        .ForPath(dto => dto.Update_time, (map) => map.MapFrom(m => m.Udt));
            CreateMap<RoleEmployeeDTO, RoleEmployee>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code))
                        .ForPath(map => map.SEmployee.Id, (dto) => dto.MapFrom(m => m.EmployeeId))
                        .ForPath(map => map.SEmployee.Code, (dto) => dto.MapFrom(m => m.EmployeeCode))
                        .ForPath(map => map.SEmployee.Name, (dto) => dto.MapFrom(m => m.EmployeeName))
                        .ForPath(map => map.SRole.Id, (dto) => dto.MapFrom(m => m.RoleId));
            CreateMap<LayerDTO, RoleEmployee>();
            // 公司
            CreateMap<Company, CompanyDTO>()
                        .ForPath(dto => dto.Editor.Name, (map) => map.MapFrom(m => m.Editor));
            CreateMap<CompanyDTO, Company>()
                        .ForPath(map => map.Editor, (dto) => dto.MapFrom(m => m.Editor.Code));
        }
    }
}