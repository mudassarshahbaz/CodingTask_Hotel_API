
using Microsoft.Extensions.DependencyInjection;
using Service.Implementations;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Service.Implementations.Unit;
using Service.Interfaces.Unit;

namespace Service
{
    public static class ServiceRegistration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddRepository();
            //services.AddHandler();
            services.AddScoped<IServiceUnit, ServiceUnit>();

        }

    }
}
