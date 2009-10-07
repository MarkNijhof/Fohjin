using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IClientSearchFormView : IView<IClientSearchFormPresenter>
    {
        IEnumerable<ClientDto> Clients { get; set; }
        ClientDto GetSelectedClient();
    }
}