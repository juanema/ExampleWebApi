using ExampleWebApi.ActionFilters;
using ExampleWebApi.Cartoons;
using ExampleWebApi.Core.DependencyInjection;
using ExampleWebApi.Core.Persistence.Repositories;
using ExampleWebApi.Domain.Models;
using ExampleWebApi.Domain.Persistence.Context;
using ExampleWebApi.Domain.Persistence.Repositories;
using ExampleWebApi.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ExampleWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(new SqliteConnection(Configuration.GetConnectionString("ExampleWebApi"))));

            services.AddControllers();
            //String Localizations
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            //
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User API",
                    Description = "A simple example ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Juanma Berlanga",
                        Email = "juanma.berlanga@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/juanma-berlanga-84710645/"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //Configuring globally ActionFilters            
            services.AddScoped<ValidateModelAttribute>();
            services.AddScoped<ExceptionFilter>();
            //
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
                options.Filters.Add(typeof(ExceptionFilter));
            });
            // register and scope other classes in every assembly
            services.ScanAssemblies();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API V1");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
