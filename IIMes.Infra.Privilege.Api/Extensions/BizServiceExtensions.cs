// using IIMes.Services.SFC.Interface;
// using IIMes.Services.SFC.Services;
using IIMes.Infra.Privilege.Interface;
using IIMes.Infra.Privilege.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IIMes.Infra.Privilege.Api
{
    public static class BizServiceExtensions
    {
        public static IServiceCollection AddBizService(this IServiceCollection services)
        {
            // add local service
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleEmployeeService, RoleEmployeeService>();
            services.AddScoped<IPermissionService, PermissionService>(); 
            services.AddScoped<IRoleResourcePermissionService, RoleResourcePermissionService>();
            services.AddScoped<ICompanyService, CompanyService>();           
            return services;
        }
    }
}
