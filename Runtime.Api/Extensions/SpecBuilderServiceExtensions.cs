// using IIMes.Services.SFC.Interface;
// using IIMes.Services.SFC.Services;
using System;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Services;
using IIMes.Services.Runtime.Services.Spec;
using Microsoft.Extensions.DependencyInjection;

namespace IIMes.Services.Runtime.Api
{
    public static class SpecBuilderExtensions
    {
        public static IServiceCollection AddSpecBuilder(this IServiceCollection services)
        {
            services.AddScoped<ICollectionSpecBuilder, EquipmentMaterialPreparingCollectionBuilder>();
            services.AddScoped<ICollectionSpecBuilder, BomMaterialPreparingCollectionBuilder>();
            services.AddScoped<SpecManager>();
            return services;
        }
    }
}