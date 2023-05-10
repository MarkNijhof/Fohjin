using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Bus;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Common;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.SQLite;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.BankApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("appsettings.ini", optional: true)
                .AddJsonFile("appsettings.json", optional: true)
                .AddXmlFile("appsettings.xml", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                ;

            var services = new ServiceCollection()
                .AddLogging(opt=>opt.AddConsole().AddDebug()
#if DEBUG
                    .SetMinimumLevel(LogLevel.Debug)
#else
                    .SetMinimumLevel(LogLevel.Information)
#endif
                    )
                .AddTransient<IConfiguration>(_ => configBuilder.Build())
                .AddBusServices()
                .AddCommandHandlersServices()
                .AddCommandsServices()
                .AddCommonServices()
                .AddConfigurationServices()
                .AddEventStoreServices()
                .AddEventStoreSqliteServices()
                .AddReportingServices()
                .AddDddServices()
                .AddBankApplicationServices()
                ;
            var service = services.BuildServiceProvider()
                .BootStrapApplication()
                ;

            var clientSearchFormPresenter = service.GetRequiredService<IClientSearchFormPresenter>();
            Application.EnableVisualStyles();
            clientSearchFormPresenter.Display();
        }
    }
}
