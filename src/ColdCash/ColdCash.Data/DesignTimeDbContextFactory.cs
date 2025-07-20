using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;

namespace ColdCash.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BudgetDbContext>
    {
        public BudgetDbContext CreateDbContext(string[] args)
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ColdCash",
                "dev.ccbudget");

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            var options = new DbContextOptionsBuilder<BudgetDbContext>()
                .UseSqlite($"Data Source={path}")
                .Options;

            return new BudgetDbContext(options);
        }
    }
}