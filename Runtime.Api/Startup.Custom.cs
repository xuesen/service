using IIMes.Services.Runtime.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace IIMes.Services.Runtime.Api
{
    public partial class Startup
    {
        public void MapGrpcService(IEndpointRouteBuilder endpoints)
        {
            // expose service interface as grpc servcie
            endpoints.MapGrpcService<IEquipmentService>();
            endpoints.MapGrpcService<ITerminalService>();
            endpoints.MapGrpcService<IWoService>();
            endpoints.MapGrpcService<IMaterialService>();
            endpoints.MapGrpcService<IWORService>();
            endpoints.MapGrpcService<IWORScrapService>();
            endpoints.MapGrpcService<IFeedingService>();
            endpoints.MapGrpcService<IWORSymptomService>();
            endpoints.MapGrpcService<IWORRepairService>();
            endpoints.MapGrpcService<IWORDefectService>();
            endpoints.MapGrpcService<IWORRepairLogDetailService>();
        }
    }
}