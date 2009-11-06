using System;
using System.Windows.Forms;
using Fohjin.DDD.BankApplication.Presenters;
using StructureMap;

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
            ApplicationBootStrapper.BootStrap();

            var clientSearchFormPresenter = ObjectFactory.GetInstance<IClientSearchFormPresenter>();

            Application.EnableVisualStyles();

            clientSearchFormPresenter.Display();
        }
    }
}
