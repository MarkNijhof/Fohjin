using System;
using System.Collections.Generic;
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
            _accountDetailsView.DisableSaveButton();
            _accountDetailsView.DisableDepositeButton();
            _accountDetailsView.DisableWithdrawlButton();
            _accountDetailsView.DisableTransferButton();

            if (_account == null)
            {
                _accountDetails = null;
                _accountDetailsView.AccountName = string.Empty;
                _accountDetailsView.AccountNumber = string.Empty;
                _accountDetailsView.Balance = 0;
                _accountDetailsView.Ledgers = null;
                _accountDetailsView.TransferAccounts = null;
                _accountDetailsView.SetIsNewAccount();
                _accountDetailsView.ShowDialog();
                return;
            }

            _accountDetails = _repository.GetByExample<AccountDetails>(new { _account.Id }).FirstOrDefault();
            _accountDetailsView.AccountName = _accountDetails.AccountName;
            _accountDetailsView.AccountNumber = _accountDetails.AccountNumber;
            _accountDetailsView.Balance = _accountDetails.Balance;
            _accountDetailsView.Ledgers = _accountDetails.Ledgers;
            _accountDetailsView.TransferAccounts = _repository.GetByExample<Account>(null).ToList().Where(x => x.Id != _accountDetails.Id).ToList();
            _accountDetailsView.SetIsExistingAccount();
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

            _bus.Publish(new CloseAccountCommand(_account.Id));
        }

        public void SaveAccountDetails()
        {
            if (_accountDetails == null)
            {
                _bus.Publish(new AddNewAccountToClientCommand(
                    Guid.NewGuid(),
                    _account.ClientDetailsId, 
                    _accountDetailsView.AccountName));
                _accountDetailsView.Close();
                return;
            }

            if (AccountNameHasChanged())
                _bus.Publish(new AccountNameGotChangedCommand(
                    _accountDetails.Id,
                    _accountDetailsView.AccountName));
        }

        public void PreformCashDeposite()
        {
            if (_accountDetails == null)
                return;

            if (AmountIsValid())
                _bus.Publish(new CashDepositeCommand(
                    _accountDetails.Id,
                    _accountDetailsView.Amount));
        }

        public void PreformCashWithdrawl()
        {
            if (_accountDetails == null)
                return;

            if (AmountIsValid())
                _bus.Publish(new CashWithdrawlCommand(
                    _accountDetails.Id,
                    _accountDetailsView.Amount));
        }

        public void PreformTransfer()
        {
            if (_accountDetails == null)
                return;

            if (TransferAmountIsValid())
                _bus.Publish(new TransferMoneyToAnOtherAccountCommand(
                    _accountDetails.Id,
                    _accountDetailsView.TransferAmount,
                    _accountDetailsView.GetSelectedTransferAccount().AccountNumber));
        }

        public void FormElementGotChanged()
        {
            ManageSaveButton();
            ManageTransferButton();
            ManageCashTransferButtons();
        }

        private void ManageCashTransferButtons()
        {
            _accountDetailsView.DisableDepositeButton();
            _accountDetailsView.DisableWithdrawlButton();

            if (!AmountIsValid())
                return;

            _accountDetailsView.EnableDepositeButton();
            _accountDetailsView.EnableWithdrawlButton();
        }

        private void ManageTransferButton()
        {
            _accountDetailsView.DisableTransferButton();

            if (TransferAmountIsValid())
                _accountDetailsView.EnableTransferButton();
        }

        private void ManageSaveButton()
        {
            _accountDetailsView.DisableSaveButton();

            if (string.IsNullOrEmpty(_accountDetailsView.AccountName))
                return;

            if (_accountDetails == null)
            {
                _accountDetailsView.EnableSaveButton();
                return;
            }

            if (AccountNameHasChanged())
            {
                _accountDetailsView.EnableSaveButton();
                return;
            }
        }

        private bool AccountNameHasChanged()
        {
            return _accountDetailsView.AccountName != _accountDetails.AccountName;
        }

        private bool AmountIsValid()
        {
            return _accountDetailsView.Amount > decimal.Zero;
        }

        private bool TransferAmountIsValid()
        {
            return _accountDetailsView.TransferAmount > decimal.Zero;
        }
    }
}