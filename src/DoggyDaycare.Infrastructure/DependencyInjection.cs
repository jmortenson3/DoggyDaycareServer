using DoggyDaycare.Core.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IOrganizationRepository), typeof(MockOrganizationRepository));
            services.AddScoped(typeof(ILocationRepository), typeof(MockLocationRepository));
            services.AddScoped(typeof(ICustomerRepository), typeof(MockCustomerRepository));
            services.AddScoped(typeof(IPetRepository), typeof(MockPetRepository));
            services.AddScoped(typeof(IBookingRepository), typeof(MockBookingRepository));
            
            return services;
        }
    }
}
