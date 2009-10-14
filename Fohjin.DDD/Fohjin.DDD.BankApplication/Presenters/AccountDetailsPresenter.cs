using System;
using System.Linq;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using AccountDetails=Fohjin.DDD.Reporting.Dto.AccountDetails;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class AccountDetailsPresenter : IAccountDetailsPresenter
    {
        private Account _account;
        private AccountDetails _accountDetails;
        private readonly IAccountDetailsView _accountDetailsView;
        private readonly ICommandBus _bus;
        private readonly IRepository _repository;

        public AccountDetailsPresenter(IAccountDetailsView accountDetailsView, ICommandBus bus, IRepository repository)
        {
            _accountDetailsView = accountDetailsView;
            _bus = bus;
            _repository = repository;
            _accountDetailsView.SetPresenter(this);
        }

        public void Display()
        {
            if (_account == null)
                return;

            _accountDetails = _repository.GetByExample<AccountDetails>(new { _account.Id }).FirstOrDefault();

            _accountDetailsView.AccountName = _accountDetails.AccountName;
            _accountDetailsView.AccountNumber = _accountDetails.AccountNumber;
            _accountDetailsView.Balance = _accountDetails.Balance;
            _accountDetailsView.Ledgers = _accountDetails.Ledgers;
            _accountDetailsView.TransferAccounts = _repository.GetByExample<Account>(null).ToList().Where(x => x.Id != _accountDetails.Id).ToList();
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