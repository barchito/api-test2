using Core2APIExercise.Common.Initializer;
using Core2APIExercise.Common.Interfaces;
using Core2APIExercise.Common.Repositories;
using Core2APIExercise.Data.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Core2APIExercise.Data.Models;
using Core2APIExercise.Data.Entities;

namespace Core2APIExercise.Common
{
    /// <summary>
    /// Override service Collection extension method to update customization
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomizedContext(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
        {
            if (env.IsEnvironment("Testing"))
            {
                services.AddDbContext<ApiContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase("APIdb"));
            }
            else
            {
                if (OS.IsMacOs())
                {
                    //store sqlite data base output directory
                    var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                    var directoryName = System.IO.Path.GetDirectoryName(location);
                    var dataSource = $"Data Source={directoryName}//APIdb.sqlite";
                    services.AddDbContext<ApiContext>(options => options.UseSqlite(dataSource));
                }
                else
                {
                    services.AddDbContext<ApiContext>(
                        options => options.UseSqlServer(
                            configuration.GetConnectionString("DefaultConnection")));

                    // In case there is migration files then uncomment it and add migration file
                    //services.AddDbContext<ApiContext>(
                    //    options => options.UseSqlServer(
                    //        configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsHistoryTable("Migration", "APIdb")));

                }
            }

            services.AddScoped<UnitOfWork<ApiContext>, UnitOfWork<ApiContext>>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<,>));
             services.Configure<SampleData>(option => configuration.GetSection("SampleData"));

            //// Additional tryouts:
            //services.Configure<AppSettings>(opt => Configuration.GetSection("AppSettings"));
            //services.Configure<JsonSettings>(opt => Configuration.GetSection("AppSettings"));
            return services;
        }

        //public static IServiceCollection AddCustomizedMvc(this IServiceCollection services)
        //{
        //    services.AddMvc()
        //        .AddMvcOptions(options =>
        //        {
        //            options.Filters.Add(new RequireHttpsAttribute());
        //        });

        //    return services;
        //}

        public static IServiceCollection AddCustomizedAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<PersonModel,Person>().ReverseMap();
            });

            return services;
        }
    }
}
