using ExampleWebApi.Core.DependencyInjection;
using ExampleWebApi.Core.Persistence.Repositories;
using ExampleWebApi.Domain.Models;
using ExampleWebApi.Domain.Persistence.Repositories;
using ExampleWebApi.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleWebApi.Domain
{
    public class DomainAutoRegister : AutoRegister
    {
        protected override void Register(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<User, int>, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
