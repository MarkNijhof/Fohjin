using System;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting.Dto;
using AccountDetails=Fohjin.DDD.Reporting.Dto.AccountDetails;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class AccountDetailsPresenter : IAccountDetailsPresenter
    {
        private Account _account;
        private AccountDetails _accountDetails;
        private readonly IAccountDetailsView _accountDetailsView;
        private readonly ICommandBus _bus;

        public AccountDetailsPresenter(IAccountDetailsView accountDetailsView, ICommandBus bus)
        {
            _accountDetailsView = accountDetailsView;
            _bus = bus;
            _accountDetailsView.SetPresenter(this);
        }

        public void Display()
        {
            _accountDetailsView.ShowDialog();
        }

        public void SetAccount(Account account)
        {
            _account = account;
        }

        public void CloseTheAccount()
        {
            if (_accountDetails == null)
                return;

            _bus.Publish(new CloseAnAccountCommand(_account.Id));
        }

        public void SaveAccountDetails()
        {
            if (_accountDetails == null)
            {
                _bus.Publish(new AddNewAccountToClientCommand(
                    _account.ClientDetailsId, 
                    Guid.NewGuid(), 
                    _accountDetailsView.AccountName));
                return;
            }
        }

        public void InitiateDeposite()
        {
            if (_accountDetails == null)
                return;
        }

        public void InitiateWithdrawl()
        {
            if (_accountDetails == null)
                return;
        }
    }
}