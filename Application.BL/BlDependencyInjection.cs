using System;
using Application.BL.Common.Interfaces;
using Application.BL.Common.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.BL
{
    public static class BlDependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IAppRepository, InMemoryDbRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}