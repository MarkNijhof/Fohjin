using System;
using System.Linq;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class AccountDetailsPresenter : Presenter<IAccountDetailsView>, IAccountDetailsPresenter
    {
        private int _editStep;
        private AccountReport _accountReport;
        private AccountDetailsReport _accountDetailsReport;
        private readonly IAccountDetailsView _accountDetailsView;
        private readonly IPopupPresenter _popupPresenter;
        private readonly IBus _bus;
        private readonly IReportingRepository _reportingRepository;

        public AccountDetailsPresenter(IAccountDetailsView accountDetailsView, IPopupPresenter popupPresenter, IBus bus, IReportingRepository reportingRepository)
            : base(accountDetailsView)
        {
            _editStep = 0;
            _accountDetailsView = accountDetailsView;
            _popupPresenter = popupPresenter;
            _bus = bus;
            _reportingRepository = reportingRepository;
        }

        public void Display()
        {
            _accountDetailsView.DisableSaveButton();
            _accountDetailsView.EnableMenuButtons();
            _accountDetailsView.EnableDetailsPanel();

            LoadData();
            _accountDetailsView.ShowDialog();
        }

        private void LoadData()
        {
            _accountDetailsReport = _reportingRepository.GetByExample<AccountDetailsReport>(new { _accountReport.Id }).FirstOrDefault();
            _accountDetailsView.AccountName = _accountDetailsReport.AccountName;
            _accountDetailsView.AccountNameLabel = _accountDetailsReport.AccountName;
            _accountDetailsView.AccountNumberLabel = _accountDetailsReport.AccountNumber;
            _accountDetailsView.BalanceLabel = _accountDetailsReport.Balance;
            _accountDetailsView.Ledgers = _accountDetailsReport.Ledgers;
            _accountDetailsView.TransferAccounts = _reportingRepository.GetByExample<AccountReport>(null).ToList().Where(x => x.Id != _accountDetailsReport.Id).ToList();
        }

        public void SetAccount(AccountReport accountReport)
        {
            _accountReport = accountReport;
        }

        public void CloseTheAccount()
        {
            _popupPresenter.CatchPossibleException(() =>
            {
//                if (_accountDetailsReport == null)
//                    return;

                _bus.Publish(new CloseAccountCommand(_accountReport.Id));

                _accountDetailsView.Close();
            });
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
            _accountDetailsView.AccountName = _accountDetailsReport.AccountName;
            _accountDetailsView.DisableMenuButtons();
            _accountDetailsView.EnableAccountNameChangePanel();
        }

        public void ChangeAccountName()
        {
            _popupPresenter.CatchPossibleException(() =>
            {
                _bus.Publish(new ChangeAccountNameCommand(
                                 _accountDetailsReport.Id,
                                 _accountDetailsView.AccountName));

                _accountDetailsReport = new AccountDetailsReport(
                    _accountDetailsReport.Id,
                    _accountDetailsReport.ClientReportId,
                    _accountDetailsView.AccountName,
                    _accountDetailsReport.Balance,
                    _accountDetailsReport.AccountNumber);

                _accountDetailsView.EnableMenuButtons();
                _accountDetailsView.EnableDetailsPanel();
                _bus.Commit();
                SystemTimer.Trigger(LoadData).In(2000);
            });
        }

        public void DepositeMoney()
        {
            _popupPresenter.CatchPossibleException(() =>
            {
                _bus.Publish(new DepositeCashCommand(
                                 _accountDetailsReport.Id,
                                 _accountDetailsView.DepositeAmount));

                _accountDetailsView.EnableMenuButtons();
                _accountDetailsView.EnableDetailsPanel();
                _bus.Commit();
                SystemTimer.Trigger(LoadData).In(2000);
            });
        }

        public void WithdrawlMoney()
        {
            _popupPresenter.CatchPossibleException(() =>
            {
                _bus.Publish(new WithdrawlCashCommand(
                                 _accountDetailsReport.Id,
                                 _accountDetailsView.WithdrawlAmount));

                _accountDetailsView.EnableMenuButtons();
                _accountDetailsView.EnableDetailsPanel();
                _bus.Commit();
                SystemTimer.Trigger(LoadData).In(2000);
            });
        }

        public void TransferMoney()
        {
            _popupPresenter.CatchPossibleException(() =>
            {
                _bus.Publish(new SendMoneyTransferCommand(
                                 _accountDetailsReport.Id,
                                 _accountDetailsView.TransferAmount,
                                 _accountDetailsView.GetSelectedTransferAccount().AccountNumber));

                _accountDetailsView.EnableMenuButtons();
                _accountDetailsView.EnableDetailsPanel();
                _bus.Commit();
                SystemTimer.Trigger(LoadData).In(2000);
                SystemTimer.Trigger(LoadData).In(4000); // This one is because there is also a delay in the transfer service :)
            });
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
            return _accountDetailsView.AccountName != _accountDetailsReport.AccountName;
        }
    }
}