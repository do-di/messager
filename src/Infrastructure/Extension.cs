using Application.Interface.Service;
using Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        {
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddSingleton<IHubClientService, HubClientService>();
            return services; 
        }
    }
}
