using System;
using System.Linq;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using ClientDetails=Fohjin.DDD.Reporting.Dto.ClientDetails;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class ClientDetailsPresenter : IClientDetailsPresenter
    {
        private Client _client;
        private ClientDetails _clientDetails;
        private readonly IClientDetailsView _clientDetailsView;
        private readonly IAccountDetailsPresenter _accountDetailsPresenter;
        private readonly ICommandBus _bus;
        private readonly IRepository _repository;

        public ClientDetailsPresenter(IClientDetailsView clientDetailsView, IAccountDetailsPresenter accountDetailsPresenter, ICommandBus bus, IRepository repository)
        {
            _clientDetailsView = clientDetailsView;
            _accountDetailsPresenter = accountDetailsPresenter;
            _bus = bus;
            _repository = repository;
            _clientDetailsView.SetPresenter(this);
        }

        public void Display()
        {
            if (_client == null)
                return;

            _clientDetails = _repository.GetByExample<ClientDetails>(new { _client.Id }).FirstOrDefault();

            _clientDetailsView.ClientName = _clientDetails.ClientName;
            _clientDetailsView.Street = _clientDetails.Street;
            _clientDetailsView.StreetNumber = _clientDetails.StreetNumber;
            _clientDetailsView.PostalCode = _clientDetails.PostalCode;
            _clientDetailsView.City = _clientDetails.City;
            _clientDetailsView.PhoneNumber = _clientDetails.PhoneNumber;
            _clientDetailsView.Accounts = _clientDetails.Accounts;
            _clientDetailsView.ShowDialog();
        }

        public void SetClient(Client client)
        {
            if (client == null)
                return;

            _client = client;
        }

        public void OpenSelectedAccount()
        {
            var client = _clientDetailsView.GetSelectedAccount();
            _accountDetailsPresenter.SetAccount(client);
            _accountDetailsPresenter.Display();
        }

        public void CreateNewAccountAndAttachToClient()
        {
            _accountDetailsPresenter.SetAccount(null);
            _accountDetailsPresenter.Display();
        }

        public void SaveClientChanges()
        {
            if (_clientDetails == null)
            {
                _bus.Publish(new ClientCreatedCommand(
                    Guid.NewGuid(), 
                    _clientDetailsView.ClientName, 
                    _clientDetailsView.Street, 
                    _clientDetailsView.StreetNumber, 
                    _clientDetailsView.PostalCode, 
                    _clientDetailsView.City, 
                    _clientDetailsView.PhoneNumber));
                return;
            }
        }
    }
}