using System;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class AccountDetailsPresenter : IAccountDetailsPresenter
    {
        private AccountDto _accountDto;
        private AccountDetailsDto _accountDetailsDto;
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

        public void SetAccount(AccountDto accountDto)
        {
            _accountDto = accountDto;
        }

        public void CloseTheAccount()
        {
            if (_accountDetailsDto == null)
                return;

            _bus.Publish(new CloseAnAccountCommand(_accountDto.Id));
        }

        public void SaveAccountDetails()
        {
            if (_accountDetailsDto == null)
            {
                _bus.Publish(new AddNewAccountToClientCommand(
                    _accountDto.ClientId, 
                    Guid.NewGuid(), 
                    _accountDetailsView.AccountName));
                return;
            }
        }

        public void InitiateDeposite()
        {
            if (_accountDetailsDto == null)
                return;
        }

        public void InitiateWithdrawl()
        {
            if (_accountDetailsDto == null)
                return;
        }
    }
}