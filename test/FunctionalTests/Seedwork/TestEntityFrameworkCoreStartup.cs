﻿using Acheve.AspNetCore.TestHost.Security;
using Acheve.TestHost;
using Balea;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FunctionalTests.Seedwork
{
    public class TestEntityFrameworkCoreStartup
    {
        private readonly IConfiguration configuration;

        public TestEntityFrameworkCoreStartup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddBalea(options =>
                {
                    options.Common.ClaimTypeMap.AllowedSubjectClaimTypes.Add(JwtClaimTypes.Subject);
                    options.Common.ClaimTypeMap.AllowedSubjectClaimTypes.Add(ClaimTypes.Upn);
                })
                .AddEntityFrameworkCoreGrantor(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(configuration.GetConnectionString(ConnectionStrings.Default), sqlServerOptions =>
                        {
                            sqlServerOptions.MigrationsAssembly(typeof(TestEntityFrameworkCoreStartup).Assembly.FullName);
                        })
                        .UseLoggerFactory(LoggerFactory.Create(builder =>
                        {
                            builder.SetMinimumLevel(LogLevel.Information).AddConsole();
                        }));
                    };

                })
                .Services
                .AddAuthentication(setup =>
                {
                    setup.DefaultAuthenticateScheme = TestServerDefaults.AuthenticationScheme;
                    setup.DefaultChallengeScheme = TestServerDefaults.AuthenticationScheme;
                })
                .AddTestServer(options =>
                {
                    options.RoleClaimType = "sourceRole";
                })
                .Services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(Policies.Custom, builder =>
                    {
                        builder.RequireAuthenticatedUser();
                    });
                })
                .AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                });
        }
    }
}
