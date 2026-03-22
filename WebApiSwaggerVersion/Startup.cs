using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using WebApiSwaggerVersion.Conventions;

namespace WebApiSwaggerVersion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());

            }).AddJsonOptions( options =>
            {
                //convert enum values to string instead of numbers.
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerGen(config =>
            {
              var titleBase = "Movies API";
              var description = "This is a Web API for Movies operations";
              
              config.SwaggerDoc("v1", new OpenApiInfo
              {
                Version = "v1",
                Title = titleBase + " v1",
                Description = description
              });

              config.SwaggerDoc("v2", new OpenApiInfo
              {
                Version = "v2",
                Title = titleBase + " v2",
                Description = description
              });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger( options =>
            {
              options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
            });

            app.UseSwaggerUI(config =>
            {
                
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesAPI v1");
                config.SwaggerEndpoint("/swagger/v2/swagger.json", "MoviesAPI v2");
                
                //Change me like this:
                //config.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesAPI v1 a");
                //config.SwaggerEndpoint("/swagger/v2/swagger.json", "MoviesAPI v2 a");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
