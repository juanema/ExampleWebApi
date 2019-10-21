using ExampleWebApi.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleWebApi.Cartoons
{
    public class CartoonsAutoRegister : AutoRegister
    {
        protected override void Register(IServiceCollection services)
        {
            services.AddScoped<UserGenerator>();
        }
    }
}
