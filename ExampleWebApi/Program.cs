using ExampleWebApi.Cartoons;
using ExampleWebApi.Domain.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExampleWebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            // Ensure the database was created
            using (var scope = host.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                                
                var generator = scope.ServiceProvider.GetService<UserGenerator>();

                generator.GenerateBikiniBottom();

            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
