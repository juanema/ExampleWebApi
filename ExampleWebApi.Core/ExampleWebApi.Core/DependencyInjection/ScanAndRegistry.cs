using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using System.Linq;

namespace ExampleWebApi.Core.DependencyInjection
{
    /// <summary>
    /// Search AutoRegister class <see cref="AutoRegister"/> and run method register in every assembly
    /// </summary>
    public static class ScanAndRegistry
    {
        private static IList<Assembly> _assemblies;

        /// <summary>
        /// Scan assemblies and search AutoRegister type <see cref="AutoRegister"/> and run method register in every assembly
        /// </summary>
        public static IServiceCollection ScanAssemblies(this IServiceCollection services)
        {
            Guard.Against<ArgumentNullException>(services == null, $"Parameter {nameof(services)} is null");
            //singleton pattern
            if (_assemblies == null)
            {
                _assemblies = new List<Assembly>();
            }
            LoadExecutingAssemby();
            LoadAssembliesFromApplicationBaseDirectory();
            ScanAll(services);
            return services;
        }

        /// <summary>
        /// Adds executing assembly to scan queue
        ///</summary>
        private static void LoadExecutingAssemby()
        {
            AddAssemblyToScan(Assembly.GetExecutingAssembly());
        }

        private static void LoadAssembliesFromApplicationBaseDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            AddAssembliesFromPath(baseDirectory);
            string binPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            if (Directory.Exists(binPath)) { AddAssembliesFromPath(binPath); }
        }

        /// <summary>
		/// Loads all assemblies from given path and try to
		/// add them to the aseembly to scan queue
		/// </summary>
		private static void AddAssembliesFromPath(string path)
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(path), $"Parameter {nameof(path)} is null");

            IEnumerable<string> assemblyPaths = Directory.GetFiles(path)
                .Where(file =>
                {
                    var name = Path.GetFileName(file);
                    // TODO JBL: for high performance, only scan the solution assemblies
                    return name.StartsWith("ExampleWebApi", StringComparison.OrdinalIgnoreCase);
                });

            foreach (string assemblyPath in assemblyPaths)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.LoadFrom(assemblyPath);

                }
                catch (Exception) { }
                if (assembly != null) { AddAssemblyToScan(assembly); }
            }
        }

        /// <summary>
		/// Adds given assembly to the assembly to scan queue
		/// </summary>
		private static void AddAssemblyToScan(Assembly assembly)
        {
            Guard.Against<ArgumentNullException>(assembly == null, $"Parameter {nameof(assembly)} is null");

            if (!_assemblies.Where(a => a.FullName == assembly.FullName).Any())
            {
                _assemblies.Add(assembly);
            }
        }

        /// <summary>
        /// Scan all assemblies in assembly list to perform autoregistration
        /// </summary>
        private static void ScanAll(IServiceCollection services)
        {
            foreach (var assembly in _assemblies) { ScanAssemby(assembly, services); }
        }

        /// <summary>
        /// Scans given assembly to perform autoregistration
        /// </summary>
        private static void ScanAssemby(Assembly assembly, IServiceCollection services)
        {
            Guard.Against<ArgumentNullException>(assembly == null, $"Parameter {nameof(assembly)} is null");
            IList<Type> registers = assembly
                .GetTypes()
                .Where(type => typeof(AutoRegister).IsAssignableFrom(type) && !type.IsAbstract)
                .ToList<Type>();

            foreach (Type type in registers)
            {
                AutoRegister autoRegister = (AutoRegister)Activator.CreateInstance(type);
                autoRegister.RegisterServices(services);
            }
        }

    }
}
