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
    public class AccountDetailsPresenter : Presenter<IAccountDetailsView>, IAccountDetailsPresenter
    {
        private int _editStep;
        private Account _account;
        private AccountDetails _accountDetails;
        private readonly IAccountDetailsView _accountDetailsView;
        private readonly ICommandBus _bus;
        private readonly IRepository _repository;

        public AccountDetailsPresenter(IAccountDetailsView accountDetailsView, ICommandBus bus, IRepository repository) : base(accountDetailsView)
        {
            _editStep = 0;
            _accountDetailsView = accountDetailsView;
            _bus = bus;
            _repository = repository;
        }

        public void Display()
        {
            _accountDetailsView.DisableSaveButton();
            _accountDetailsView.EnableMenuButtons();
            _accountDetailsView.EnableDetailsPanel();

            _accountDetails = _repository.GetByExample<AccountDetails>(new { _account.Id }).FirstOrDefault();
            _accountDetailsView.AccountName = _accountDetails.AccountName;
            _accountDetailsView.AccountNameLabel = _accountDetails.AccountName;
            _accountDetailsView.AccountNumberLabel = _accountDetails.AccountNumber;
            _accountDetailsView.BalanceLabel = _accountDetails.Balance;
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

            _bus.Publish(new CloseAccountCommand(_account.Id));
        }

        public void Cancel()
        {
            _editStep = 0;
            _accountDetailsView.EnableDetailsPanel();
            _accountDetailsView.DisableSaveButton();
            _accountDetailsView.EnableMenuButtons();
        }

        public void InitiateMoneyDeposite()
        {
            _editStep = 1;
            _accountDetailsView.DepositeAmount = 0M;
            _accountDetailsView.DisableMenuButtons();
            _accountDetailsView.EnableDepositePanel();
        }

        public void InitiateMoneyWithdrawl()
        {
            _editStep = 2;
            _accountDetailsView.WithdrawlAmount = 0M;
            _accountDetailsView.DisableMenuButtons();
            _accountDetailsView.EnableWithdrawlPanel();
        }

        public void InitiateMoneyTransfer()
        {
            _editStep = 3;
            _accountDetailsView.TransferAmount = 0M;
            _accountDetailsView.DisableMenuButtons();
            _accountDetailsView.EnableTransferPanel();
        }

        public void InitiateAccountNameChange()
        {
            _editStep = 4;
            _accountDetailsView.AccountName = _accountDetails.AccountName;
            _accountDetailsView.DisableMenuButtons();
            _accountDetailsView.EnableAccountNameChangePanel();
        }

        public void ChangeAccountName()
        {
            _bus.Publish(new AccountNameGotChangedCommand(
                _accountDetails.Id,
                _accountDetailsView.AccountName));

            _accountDetails = new AccountDetails(
                _accountDetails.Id,
                _accountDetails.ClientId,
                _accountDetailsView.AccountName,
                _accountDetails.Balance,
                _accountDetails.AccountNumber);

            _accountDetailsView.EnableMenuButtons();
            _accountDetailsView.EnableDetailsPanel();
        }

        public void DepositeMoney()
        {
            _bus.Publish(new CashDepositeCommand(
                _accountDetails.Id,
                _accountDetailsView.DepositeAmount));

            _accountDetailsView.DepositeAmount = 0;
            _accountDetailsView.EnableMenuButtons();
            _accountDetailsView.EnableDetailsPanel();
        }

        public void WithdrawlMoney()
        {
            _bus.Publish(new CashWithdrawlCommand(
                _accountDetails.Id,
                _accountDetailsView.WithdrawlAmount));

            _accountDetailsView.WithdrawlAmount = 0;
            _accountDetailsView.EnableMenuButtons();
            _accountDetailsView.EnableDetailsPanel();
        }

        public void TransferMoney()
        {
            _bus.Publish(new TransferMoneyToAnOtherAccountCommand(
                _accountDetails.Id,
                _accountDetailsView.TransferAmount,
                _accountDetailsView.GetSelectedTransferAccount().AccountNumber));

            _accountDetailsView.TransferAmount = 0;
            _accountDetailsView.EnableMenuButtons();
            _accountDetailsView.EnableDetailsPanel();
        }

        public void FormElementGotChanged()
        {
            _accountDetailsView.DisableSaveButton();

            if (!FormIsValid())
                return;

            if (FormHasChanged())
            {
                _accountDetailsView.EnableSaveButton();
                return;
            }
        }

        private bool FormIsValid()
        {
            if (_editStep == 0 || 
                _editStep == 1 || 
                _editStep == 2 || 
                _editStep == 3)
                return true;

            if (_editStep == 4)
                return !string.IsNullOrEmpty(_accountDetailsView.AccountName);

            throw new Exception("Edit step was not properly initialized!");
        }

        private bool FormHasChanged()
        {
            return
                AccountNameHasChanged() ||
                DepositeAmountHasChanged() ||
                WithdrawlAmountHasChanged() ||
                TransferAmountHasChanged();
        }

        private bool TransferAmountHasChanged()
        {
            return _accountDetailsView.TransferAmount > decimal.Zero;
        }

        private bool WithdrawlAmountHasChanged()
        {
            return _accountDetailsView.WithdrawlAmount > decimal.Zero;
        }

        private bool DepositeAmountHasChanged()
        {
            return _accountDetailsView.DepositeAmount > decimal.Zero;
        }

        private bool AccountNameHasChanged()
        {
            return _accountDetailsView.AccountName != _accountDetails.AccountName;
        }
    }
}