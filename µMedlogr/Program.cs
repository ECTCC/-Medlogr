using µMedlogr.core;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using µMedlogr.core.Models;
using µMedlogr.core.Interfaces;

namespace µMedlogr;
public class Program {
    public static void Main(string[] args) {

        //Only For manual testing data

        #region CustomSeeding
#if DEBUG
        //var µMedlogrContextFactory = new µMedlogrContextFactory();
        //using (var context = µMedlogrContextFactory.CreateDbContext([""])) {
        //    context.Database.EnsureDeleted();
        //    context.Database.EnsureCreated();
        //    context.SaveChanges();
        //}
#endif
        #endregion

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services
            .AddDatabaseDeveloperPageExceptionFilter();

        builder.Services
            .AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<µMedlogrContext>();

        SetupServices(builder, connectionString);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseMigrationsEndPoint();
        } else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        using (var scope = app.Services.CreateScope()) {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<µMedlogrContext>();
            context.Database.EnsureCreated();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();


        app.Run();
    }

    private static void SetupServices(WebApplicationBuilder builder, string connectionString) {
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<µMedlogrContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddScoped<µMedlogrContext>();
        builder.Services.AddScoped<EntityManager>();
        builder.Services.AddScoped<IHealthRecordService, HealthRecordService>();
        builder.Services.AddScoped<IDrugService, DrugService>();
        builder.Services.AddScoped<IPersonService, PersonService>();
        builder.Services.AddScoped<ISymptomService, SymptomService>();
        builder.Services.AddAuthentication().AddCookie();
        builder.Services.AddAuthorization();
    }
}
