using System;
using System.Collections.Generic;
using System.Linq;
using ComposeService;
// using IIMes.Services.SFC.Interface;
// using IIMes.Services.SFC.Services;
using IIMes.Infrastructure.RestClient;
using IIMes.Infra.Privilege.Interface;
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

namespace IIMes.Infra.Privilege.Api
{
    public partial class Startup
    {
        public void MapGrpcService(IEndpointRouteBuilder endpoints)
        {
            // expose service interface as grpc servcie
            endpoints.MapGrpcService<IDepartmentService>();
            endpoints.MapGrpcService<IEmployeeService>();
        }
    }
}