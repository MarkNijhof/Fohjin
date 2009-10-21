using System.Collections.Generic;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IClientSearchFormView : IView
    {
        IEnumerable<Client> Clients { get; set; }
        Client GetSelectedClient();
        event Action OnCreateNewClient;
        event Action OnOpenSelectedClient;
        event Action OnRefresh;
    }
}