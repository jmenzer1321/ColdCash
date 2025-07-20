using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using ColdCash.Data;
using System;

namespace ColdCash.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBudgetServices(this IServiceCollection services)
        {
            services.AddDbContextFactory<BudgetDbContext>(options =>
            {
                var dbPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "ColdCash",
                    "ColdCash.ccbudget");

                Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
                options.UseSqlite($"Data Source={dbPath}");
            });

            return services;
        }
    }
}