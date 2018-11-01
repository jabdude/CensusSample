using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Census.Services
{
    public class ServiceFactory
    {
        private static readonly Lazy<ServiceFactory> factory = new Lazy<ServiceFactory>(() => new ServiceFactory());

        private static ServiceFactory Factory { get { return factory.Value; } }
        private static ServiceCollection ServiceCollection { get; set; }
        private static ServiceProvider _provider { get; set; }
        
        public static ServiceProvider Provider { get {
                if(ServiceCollection == null)
                {
                    ConfigureServices();
                    _provider = ServiceCollection.BuildServiceProvider();
                }
                return _provider;
            }
        }
          
        private ServiceFactory()
        {
            ConfigureServices();
        }

        private static void ConfigureServices()
        {
            ServiceCollection = new ServiceCollection();
            var assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly.GetTypes().Where(x => x.IsInterface))
            {
                if (type.Namespace.Contains("Census.Services"))
                {
                    var services = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(s => s.GetTypes())
                       .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);
                   
                    if(services.Any())
                        ServiceCollection.AddTransient(type, services.First()); // only one implementation anyway
                }                
            }          
        }
    }
}
