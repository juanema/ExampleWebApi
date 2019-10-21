using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleWebApi.Core.DependencyInjection
{
    public abstract class AutoRegister
    {
        /// <summary>
		/// Registration entry point
		/// </summary>
		public void RegisterServices(IServiceCollection services)
        {
            Guard.Against<ArgumentException>(services == null, $"Parameter {nameof(services)} is null");
                       
            Register(services);
        }

        /// <summary>
        /// Override this method to perform configuration
        /// operations
        /// </summary>
        protected virtual void Register(IServiceCollection services)
        {

        }
    }
}
