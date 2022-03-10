using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyConsoleApp.Models.Database;

namespace MyConsoleApp.Models
{
    internal class ConsoleSchoolContext : SchoolContext
    {
        private readonly string _connectionString;

        public ConsoleSchoolContext()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            _connectionString = config.GetConnectionString("SchoolDb");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = optionsBuilder
                    .UseSqlServer(_connectionString);
            }
        }
    }
}
