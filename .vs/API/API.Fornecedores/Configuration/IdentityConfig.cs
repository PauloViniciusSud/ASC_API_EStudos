using API.Fornecedores.Date;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Fornecedores.Configuration
{
    public static class IdentityConfig
    {

        public static IServiceCollection AddIdentityConfiguration
            (this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<AplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IRouteCollection>().AddEntityFrameworkStores<AplicationDbContext>()
              .AddDefaultTokenProviders();


            return services;
        }
    }
}
