using Api.Data;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Api.Data.SeedData;
using Api.Data.Entities.Identity;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                Log.Information("Application Starting");
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await DefaultGovernrates.SeedGovernratesAsync(context);
                await DefaultCities.SeedCitiesAsync(context);
                await DefaultRoles.SeedAsync(roleManager);
                await DefaultUsers.SeedUserAsync(userManager, context);
                await DefaultBanks.SeedBanksAsync(userManager, context);


                Log.Information("Data seed done");
            }
            catch (Exception ex)
            {

                Log.Fatal(ex, ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }

            try
            {
                host.Run();
            }
            catch (Exception ex)
            {

                Log.Fatal(ex, "The application failed to start!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}
