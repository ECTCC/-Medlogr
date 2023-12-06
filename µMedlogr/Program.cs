using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using µMedlogr.core;
using µMedlogr.core.Services;

namespace µMedlogr;
public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services
            .AddDatabaseDeveloperPageExceptionFilter();

        builder.Services
            .AddDefaultIdentity<µMedlogr.core.Models.AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<µMedlogrContext>();

        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<µMedlogrContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddScoped<µMedlogrContext>();
        builder.Services.AddScoped<EntityManager>();
        builder.Services.AddAuthentication().AddCookie();
        builder.Services.AddAuthorization();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseMigrationsEndPoint();
        } else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.Run();
    }
}
