using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IClientSearchFormView : IView
    {
        IEnumerable<ClientReport>? Clients { get; set; }
        ClientReport? GetSelectedClient();
        event EventAction? OnCreateNewClient;
        event EventAction? OnOpenSelectedClient;
    }
}