using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BL.Service.Abstracts;
using Training.BL.Service.Implements;

namespace Training.BL
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services) {
        services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITrainerService, TrainerService>();
            return services;
        }


    }
    
}
