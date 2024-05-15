using Microsoft.Extensions.DependencyInjection;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Application
{
    public static class DepencyInjection
    {
        public static IServiceCollection DepencyInjectionApplication(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, ICurrentUserService>();
            return services;
        }
    }
}