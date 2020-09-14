using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.BL
{
    public static class BlDependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}