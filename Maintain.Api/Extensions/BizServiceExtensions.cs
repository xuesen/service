// using IIMes.Services.SFC.Interface;
// using IIMes.Services.SFC.Services;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IIMes.Services.Maintain.Api
{
    public static class BizServiceExtensions
    {
        public static IServiceCollection AddBizService(this IServiceCollection services)
        {
            // add local service
            services.AddScoped<IProductLineService, ProductLineService>();
            services.AddScoped<IFactoryService, FactoryService>();
            services.AddScoped<IWorkShopService, WorkShopService>();
            services.AddScoped<ITerminalService, TerminalService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IProcessService, ProcessService>();
            services.AddScoped<IProcessTypeService, ProcessTypeService>();
            services.AddScoped<IStageService, StageService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();
            services.AddScoped<IEquipmentCatagoryService, EquipmentCatagoryService>();
            services.AddScoped<IShiftGroupService, ShiftGroupService>();
            services.AddScoped<IShiftService, ShiftService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMaintainService, MaintainService>();
            services.AddScoped<IPartFsService, PartFsService>();
            services.AddScoped<IWoFsService, WoFsService>();
            services.AddScoped<IImportService, ImportService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IScrapReasonService, ScrapReasonService>();
            services.AddScoped<ITemporaryTableService, TemporaryTableService>();
            // services.AddScoped<ISnoService, SnoService>();
            // services.AddScoped<IRepService, RepService>();
            // services.AddScoped<ISFCServcie, SFCService>();

            // services.AddSingleton(provider =>
            // {
            //     add a grpc service
            //     return Startup.GetGrpcService<ISFCService>(provider);
            // });

            services.AddScoped<ISamplingPlanService, SamplingPlanService>();
            services.AddScoped<ISamplingPlanDetailService, SamplingPlanDetailService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISamplingRuleService, SamplingRuleService>();
            services.AddScoped<ISamplingRuleDetailService, SamplingRuleDetailService>();
            services.AddScoped<ITestItemTypeService, TestItemTypeService>();
            services.AddScoped<ITestItemService, TestItemService>();
            services.AddScoped<IBarcodeRuleService, BarcodeRuleService>();
            services.AddScoped<IPrintTemplateService, PrintTemplateService>();
            services.AddScoped<ISymptomTypeService, SymptomTypeService>();
            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<ICauseService, CauseService>();
            services.AddScoped<IDutyService, DutyService>();
            services.AddScoped<IRepairTypeService, RepairTypeService>();
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IBomService, BomService>();
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddScoped<IBopBaseService, BopBaseService>();
            services.AddScoped<IBopProcessTestitemService, BopProcessTestitemService>();
            services.AddScoped<IBopProcessSamplingService, BopProcessSamplingService>();
            services.AddScoped<IBopProcessLabelService, BopProcessLabelService>();
            services.AddScoped<IBopSpecService, BopSpecService>();
            services.AddScoped<IPartPackService, PartPackService>();
            services.AddScoped<IBopProcessBomService, BopProcessBomService>();
            services.AddScoped<IWoBomService, WoBomService>();
            services.AddScoped<IScrapReasonService, ScrapReasonService>();
            services.AddScoped<IWoBaseService, WoBaseService>();
            services.AddScoped<IWoService, WoService>();
            services.AddScoped<IWoStatusService, WoStatusService>();
            return services;
        }
    }
}
