using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Net.Client;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Hibernate.Data;
using IIMes.Services.Maintain.Api.Options;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Services;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProtoBuf;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Server;

namespace IIMes.Services.Maintain.Api
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services)
        {
            var dbContextManager = new DBContextManager();
            services.AddSingleton<IDBContextManager>(dbContextManager);
            services.AddScoped<IDBContext>(factory => dbContextManager.OpenDBContext());
            return services;
        }
    }
}
