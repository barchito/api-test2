using System;
using AdvancedStudioExercise.Infrastructure;
using AdvancedStudioExercise.IoC;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AdvancedStudioExercise.Web {
    public class Startup {
        public Startup ( IConfiguration configuration ) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices ( IServiceCollection services ) {
            var config = new MapperConfiguration( cfg => { cfg.AddProfile( new WebMappingsProfileConfiguration() ); } );
            var mapper = config.CreateMapper();

            services.AddSingleton( mapper );
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Advanced Studio Exercise API", Version = "v1" });
            });

            services.Configure<AppConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            return new AutofacServiceProvider( ContainerManager.BuildContainer( services ) );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app, IHostingEnvironment env ) {
            if ( env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Advanced Studio Exercise API v1");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}