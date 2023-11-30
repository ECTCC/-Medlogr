using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace µMedlogr.core;
public class µMedlogrContextFactory : IDesignTimeDbContextFactory<µMedlogrContext> {
    public µMedlogrContext CreateDbContext(string[] args) {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../µMedlogr"))
            .AddJsonFile("appsettings.Development.json").Build();

        var connstring = config.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<µMedlogrContext>();
        optionsBuilder.UseSqlServer(connstring);

        return new µMedlogrContext(optionsBuilder.Options);
    }
}
