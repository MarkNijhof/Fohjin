using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IClientDetailsView : IView<IClientDetailsPresenter>
    {
        string ClientName { get; set; }
        string Street { get; set; }
        string StreetNumber { get; set; }
        string PostalCode { get; set; }
        string City { get; set; }
        IEnumerable<AccountDto> Accounts { get; set; }
        string PhoneNumber { get; set; }
        AccountDto GetSelectedAccount();
    }
}