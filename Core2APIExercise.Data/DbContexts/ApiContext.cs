using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Core2APIExercise.Data.DbContexts
{
    public class ApiContext : DbContext
    { 
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string schema = "dbo";
            if (OS.IsMacOs())
            {
                schema = null;
            }

            var config = new  ContextConfig();
            config.ApplicationContextConfig(modelBuilder, schema);
        }
    }
   
}
