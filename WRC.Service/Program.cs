using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WRC.Service.Interfaces;
using WRC.Service.Services;

namespace WRC.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IOptionsProvider, RegistryOptionsProvider>();
                    services.AddSingleton<ISystemCommandExecutor, CommandExecutor>();
                    services.AddSingleton<IServerListener, ServerListener>();
                    services.AddHostedService<Worker>();
                });
    }
}
