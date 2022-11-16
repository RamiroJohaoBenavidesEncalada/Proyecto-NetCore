using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Curso.ComercioElectronico.Infraestructure;

public class ComercioElectronicoContextFactory : IDesignTimeDbContextFactory<ComercioElectronicoDbContext>
{
    public ComercioElectronicoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ComercioElectronicoDbContext>();
        
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Curso.ComercioElectronico.HttpApi"))
            .AddJsonFile("appsettings.json")
            .Build();

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, configuration.GetConnectionString("ComercioElectronico"));
        Debug.WriteLine($"dbPath: {dbPath}");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new ComercioElectronicoDbContext(optionsBuilder.Options);
    }
} 