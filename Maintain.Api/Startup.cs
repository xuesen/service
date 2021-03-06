using System;
using System.Collections.Generic;
using System.Linq;
using AspectCore.Extensions.DependencyInjection;
using AutoMapper;
using Grpc.Net.Client;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Hibernate.Data;
using IIMes.Infrastructure.Options;
using IIMes.Services.Core.Resource;
using IIMes.Services.Maintain.Api.Options;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Services;
using Maintain.Interface.AutoMapper;
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
    public partial class Startup
    {
        private const string MqUriString = "rabbitmq://localhost/";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the Maintain. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
            services.Configure<GrpcServiceOptions>(Configuration.GetSection("GrpcServices"));
            services.Configure<AppOption>(Configuration.GetSection("App"));
            services
            .AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                x.SerializerSettings.MaxDepth = 5;
                // 不使用驼峰
                x.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            });

            services.AddNHibernate();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddCodeFirstGrpc(config =>
            {
                config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
            });

            services.AddAutoMapper(typeof(MapperConfig));

            // var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            // {
            //     cfg.Host(new Uri(MqUriString), h => { });
            // });
            // services.AddSingleton<IPublishEndpoint>(bus);
            // services.AddSingleton<IBus>(bus);
            // services.AddSingleton<IBusControl>(bus);
            // services.AddMassTransitHostedService();

            services.AddSingleton<IMessageProvider>(sp =>
            {
                var assembly = typeof(IResource).Assembly;
                var option = sp.GetService<IOptions<AppOption>>();
                return new ResourceMessageProvider(assembly, option);
            });

            services.AddRestClient();

            services.AddBizService();

            services.ConfigureDynamicProxy(c => { c.ThrowAspectException = false; });
        }

        // This method gets called by the Maintain. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                MapGrpcService(endpoints);
            });
        }

        public static T GetGrpcService<T>(IServiceProvider provider)
            where T : class
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            var serviceName = typeof(T).Name;
            var option = provider.GetService<IOptions<GrpcServiceOptions>>();
            var address = option.Value.Address[serviceName];
            var channel = GrpcChannel.ForAddress(address);
            var service = channel.CreateGrpcService<T>();
            return service;
        }
    }
}
