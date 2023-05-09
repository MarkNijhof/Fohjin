using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Configuration;
using Fohjin.DDD.Common;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fohjin.DDD.BankApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //ApplicationBootStrapper.BootStrap();

            var services = new ServiceCollection()
                .AddBusServices()
                .AddConfigurationServices()
                .AddCommonServices()
                .AddReportingServices()
                .AddDddServices()
                ;
            var service = services.BuildServiceProvider();

            var clientSearchFormPresenter = service.GetRequiredService<IClientSearchFormPresenter>();
            Application.EnableVisualStyles();
            clientSearchFormPresenter.Display();
        }
    }
}
