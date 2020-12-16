using System;
using System.Collections.Generic;
using System.Linq;
using ComposeService;
// using IIMes.Services.SFC.Interface;
// using IIMes.Services.SFC.Services;
using IIMes.Infrastructure.RestClient;
using IIMes.Services.Maintain.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using ProtoBuf;
using ProtoBuf.Grpc.Server;

namespace IIMes.Services.Maintain.Api
{
    public partial class Startup
    {
        public void MapGrpcService(IEndpointRouteBuilder endpoints)
        {
            // expose service interface as grpc servcie
            endpoints.MapGrpcService<IProductLineService>();
            endpoints.MapGrpcService<IFactoryService>();
            endpoints.MapGrpcService<IWorkShopService>();
            endpoints.MapGrpcService<ITerminalService>();
            endpoints.MapGrpcService<IModelService>();
            endpoints.MapGrpcService<IProcessService>();
            endpoints.MapGrpcService<IProcessTypeService>();
            endpoints.MapGrpcService<IStageService>();
            endpoints.MapGrpcService<IEquipmentService>();
            endpoints.MapGrpcService<IEquipmentTypeService>();
            endpoints.MapGrpcService<IEquipmentCatagoryService>();
            endpoints.MapGrpcService<IShiftService>();
            endpoints.MapGrpcService<IShiftGroupService>();
            endpoints.MapGrpcService<IDepartmentService>();
            endpoints.MapGrpcService<IEmployeeService>();
            endpoints.MapGrpcService<IMaintainService>();
            endpoints.MapGrpcService<IPartFsService>();
            endpoints.MapGrpcService<IWoFsService>();
            endpoints.MapGrpcService<IImportService>();
            endpoints.MapGrpcService<ICommonService>();
            endpoints.MapGrpcService<IFamilyService>();
            endpoints.MapGrpcService<IWoBomService>();
            endpoints.MapGrpcService<IScrapReasonService>();
            endpoints.MapGrpcService<IWoBaseService>();
            endpoints.MapGrpcService<IWoService>();
            endpoints.MapGrpcService<IWoStatusService>();
            endpoints.MapGrpcService<ITemporaryTableService>();
            // endpoints.MapGrpcService<ISnoService>();
            // endpoints.MapGrpcService<IRepService>();

            // endpoints.MapGrpcService<ISamplingPlanService>();
            // endpoints.MapGrpcService<ISamplingPlanDetailService>();
            // endpoints.MapGrpcService<ISettingService>();
            // endpoints.MapGrpcService<ISamplingRuleService>();
            // endpoints.MapGrpcService<ISamplingRuleDetailService>();
            // endpoints.MapGrpcService<ITestItemTypeService>();
        }
    }
}