using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineChat.Application.Abstractions;
using OnlineChat.Application.ChatActions;
using OnlineChat.Application.Mappings;
using OnlineChat.Application.Services;
using System;

namespace OnlineChat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection DepencyInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<IEncryptionService>(provider => new EncryptionService(configuration));

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddScoped<IFileService, FileService>();
            services.AddSingleton<IEmailService, EmailService>();

            services.AddAutoMapper(cfg =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var encryptionService = serviceProvider.GetRequiredService<IEncryptionService>();
                cfg.AddProfile(new MappingProfile(encryptionService));
            });

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            return services;
        }
    }
}
