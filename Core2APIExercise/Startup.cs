using AutoMapper;
using Core2APIExercise.Common;
using Core2APIExercise.Common.Initializer;
using Core2APIExercise.Common.Interfaces;
using Core2APIExercise.Common.Resolver;
using Core2APIExercise.Data.DbContexts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Core2APIExercise
{
    /// <summary>
    /// Application Startup class and configuration
    /// </summary>
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            // In case use the App secret here is the configuration
            //if (env.IsDevelopment() || env.IsEnvironment("Testing"))
            //{
            //    builder.AddUserSecrets<Startup>();
            //}
            if (env.IsDevelopment() || env.IsEnvironment("Testing"))
            {
                builder.AddJsonFile($"sampleData.json", optional: true, reloadOnChange: false);
                //builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Fluent Validator Registraton
             services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                //.AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
                .AddJsonOptions(a => {
                        a.SerializerSettings.ContractResolver = new LowercaseContractResolver()
                        ; a.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    }
                );

            services.AddCustomizedContext(Configuration, CurrentEnvironment)
              .AddAutoMapper(cfg =>
              {
                  cfg.AddProfile<ApiProfile>();
              })
              //.AddCustomizedMvc() // Its for https attributes
              .AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new Info { Title = ".Net Core2 Api exercise", Version = "v1" });
              });

            services.AddScoped<UnitOfWork<ApiContext>, UnitOfWork<ApiContext>>();
            services.AddScoped<IDbInitializer, ApiInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseMvc();
        //}
        public void Configure(IApplicationBuilder app,  ILoggerFactory loggerFactory, IDbInitializer dbInitializer)
        {
            if (CurrentEnvironment.IsDevelopment() || CurrentEnvironment.IsEnvironment("Testing"))
            {
                app.UseDeveloperExceptionPage();
                dbInitializer.Initialize();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger()
                .UseDefaultFiles()
                .UseStaticFiles()

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core2 API Exercise V1");
            });

            app.UseMvc();
            

            //app.UseRewriter(new RewriteOptions().AddRedirectToHttps())
            //app.UseRewriter(new RewriteOptions())
            //    .UseDefaultFiles()
            //    .UseStaticFiles()
            //    .UseSwagger()
            //    .UseSwaggerUI(c =>
            //    {
            //        c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Net Core2 Api V1");
            //    })
            //    .UseMvcWithDefaultRoute();

        }
    }
}
