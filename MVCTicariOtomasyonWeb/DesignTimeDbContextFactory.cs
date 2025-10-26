// DesignTimeDbContextFactory.cs
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MVCTicariOtomasyonWeb.Models.sınıflar;

namespace MVCTicariOtomasyonWeb
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var conn = config.GetConnectionString("DefaultConnection")
                       ?? "Server=localhost,1433;Database=MvcTicariOtomasyon;User Id=sa;Password=Ahsennur#2025;Encrypt=False;TrustServerCertificate=True;";

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(conn);

            return new Context(optionsBuilder.Options);
        }
    }
}
