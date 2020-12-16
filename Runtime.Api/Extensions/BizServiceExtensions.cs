// using IIMes.Services.SFC.Interface;
// using IIMes.Services.SFC.Services;
using System;
// using Generate.Service; // 生成序号时需要放开，2020.9.17
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IIMes.Services.Runtime.Api
{

    public static class BizServiceExtensions
    {
        public static IServiceCollection AddBizService(this IServiceCollection services)
        {
            // add local service
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<ITerminalService, TerminalService>();
            services.AddScoped<IWoService, WoService>();
            services.AddScoped<IMaterialService, MaterialService>();
            // services.AddScoped<GenerateBarcodeManager>(); // 生成序号时需要放开，2020.9.17
            services.AddScoped<IWORScrapService, WORScrapService>();
            services.AddScoped<IWORService, WORService>();
            services.AddScoped<IFeedingService, FeedingService>();
            services.AddScoped<IWORSymptomService, WORSymptomService>();
            services.AddScoped<IWORRepairService, WORRepairService>();
            services.AddScoped<IWORDefectService, WORDefectService>();
            services.AddScoped<IWORRepairLogDetailService, WORRepairLogDetailService>();     
            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<ISymptomTypeService, SymptomTypeService>();
            services.AddScoped<ICauseService, CauseService>();
            services.AddScoped<IDutyService, DutyService>();  
            services.AddScoped<IRepairTypeService, RepairTypeService>();
            services.AddScoped<IProcessService, ProcessService>();  
            services.AddScoped<IPartService, PartService>();                              
            return services;
        }
    }
}