using ExampleWebApi.Core.ActionFilters;
using ExampleWebApi.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

// Use a Hosting Startup Attribute to identify the IHostingStartup implementation.
[assembly: HostingStartup(typeof(ExampleWebApi.Core.CoreStartup))]
namespace ExampleWebApi.Core
{
    public class CoreStartup : IHostingStartup
    {

        public void Configure(IWebHostBuilder builder)
        {
            builder.ThrowExceptionIfNull();
            builder.ConfigureServices(services =>
            {
                
                //String Localizations
                services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
                //Configuring globally ActionFilters            
                services.AddScoped<ValidateModelAttribute>();
                services.AddScoped<ExceptionFilter>();
                //
                services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ValidateModelAttribute));
                    options.Filters.Add(typeof(ExceptionFilter));
                });
                //
                services.AddControllers();

                //String Localizations
                services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

                //
                // Register the Swagger generator
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
                    //
                    c.SwaggerDoc("v2", new OpenApiInfo
                    {
                        Version = "v2",
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
            });
        }
    }
}
