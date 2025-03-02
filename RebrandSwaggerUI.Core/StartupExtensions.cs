﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace RebrandSwaggerUI.Core
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApiVersioningSupport(this IServiceCollection services)
        {
            services
                .AddApiVersioning(options => options.ReportApiVersions = true)
                .AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVVV");

            return services;
        }

        public static SwaggerGenOptions AddSwaggerGenSupport(
            this SwaggerGenOptions swaggerGenOptions, IHostEnvironment hostEnv, string title, string release, List<string> apiVersions = null)
        {
            if (apiVersions == null || apiVersions.Count == 0)
                apiVersions = new List<string>() { "1.0" }; // This code is to make sure we have a version for each API

            string description = $"Environment: {hostEnv.EnvironmentName} {Environment.NewLine}Release: {release}";

            apiVersions.ForEach(version =>
            {
                swaggerGenOptions.SwaggerDoc($"v{version}", new OpenApiInfo
                {
                    Title = title,
                    Version = $"v{version}",
                    Description = description,
                    TermsOfService = new Uri("https://www.google.com/"),
                    Contact = new OpenApiContact
                    {
                        Email = "support@mycompany.com",
                        Name = "My Company Support Team"
                    },
                    License = new OpenApiLicense
                    {
                        Name = $"Copyright {DateTime.Now.Year}, My Company Inc. All rights reserved."
                    }
                });
            });

            swaggerGenOptions.EnableAnnotations();

            return swaggerGenOptions;
        }

        public static IApplicationBuilder UseCustomSwaggerUI(
            this IApplicationBuilder applicationBuilder, string applicatonName, string endpointPrefix, List<string> apiVersions = null)
        {
            if (apiVersions == null || apiVersions.Count == 0)
                apiVersions = new List<string>() { "1.0" }; // This code is to make sure we have a version for each API

            applicationBuilder.UseSwagger();

            applicationBuilder.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "My Company Inc.";
                c.RoutePrefix = string.Empty;
                apiVersions.ForEach(x => c.SwaggerEndpoint($"/{endpointPrefix}/swagger/v{x}/swagger.json", $"{applicatonName} v{x}"));

                // For UI Rebranding
                c.InjectStylesheet(@"/resources/css/custom-swagger.css");
                c.InjectJavascript(@"/resources/scripts/custom-script.js");
            });

            return applicationBuilder;
        }

        public static IApplicationBuilder UseCustomPrefix(this IApplicationBuilder app, string prefix)
        {
            app.UsePathBase($"/{prefix}");

            app.Use((context, next) =>
            {
                context.Request.PathBase = $"/{prefix}";
                return next();
            });

            return app;
        }
    }
}
