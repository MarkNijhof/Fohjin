using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IAccountDetailsView : IView<IAccountDetailsPresenter>
    {
        string AccountName { get; set; }
        IEnumerable<Ledger> Ledgers { get; set; }
        string Amount { get; set; }
    }
}