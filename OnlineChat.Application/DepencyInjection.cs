using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.Mappings;
using OnlineChat.Application.Services;
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
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DepencyInjection).Assembly);
            });
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmailService, EmailService>();
            var mappingconfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingconfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}