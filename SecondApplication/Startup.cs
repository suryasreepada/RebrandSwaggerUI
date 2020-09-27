using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RebrandSwaggerUI.Core;

namespace SecondApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            HostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioningSupport();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSwaggerGenSupport(HostEnvironment, "Second API", "1.0.0.0", new List<string> { "1.0", "2.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomPrefix("api/secondapp");

            app.UseCustomSwaggerUI("Second API", "api/secondapp", new List<string> { "1.0", "2.0" });

            app.UseMvc();
        }
    }
}
