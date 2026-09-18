using E_Commerce.Application.Contracts;
using E_Commerce.Application.Profiles;
using E_Commerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile(new ProductProfile()), typeof(ApplicationServicesRegistration).Assembly);

            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IBasketServices, BasketServices>();
            services.AddSingleton<ICacheServices, CacheService>();
            services.AddScoped<IIdentityService, IdentityServices>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenServices>();
            return services;
        }

    }
}
