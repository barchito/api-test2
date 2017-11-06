using Core2APIExercise.Common.Interfaces;
using Core2APIExercise.Data.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core2APIExercise.Common.Initializer
{
   public class ApiInitializer: IDbInitializer
    {
        private readonly ApiContext _context;
        private readonly ILogger _logger;
        //private readonly SampleData _data;
        private readonly IHostingEnvironment _environment;
        
        public ApiInitializer(ApiContext context,
          ILoggerFactory loggerFactory,
          IOptions<SampleData> dataOptions,
          IHostingEnvironment env)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ApiInitializer");
            //_data = dataOptions.Value;
            _environment = env;
        }
        public void Initialize()
        {
            InitializeContext();
        }
        private void InitializeContext()
        {
            // create database schema if it does not exist
            if (_context.Database.EnsureCreated())
            {
                _logger.LogInformation("Creating API Database schema...");
            }

            var unitOfWork = new UnitOfWork<ApiContext>(_context);
            var seedData = new SeedData<ApiContext>(_logger, unitOfWork);
            seedData.Initialize();
        }
    }
}
