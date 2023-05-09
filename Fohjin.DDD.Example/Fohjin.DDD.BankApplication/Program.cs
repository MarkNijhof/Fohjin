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
            var services = new ServiceCollection()
                .AddBusServices()
                .AddConfigurationServices()
                .AddCommonServices()
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
