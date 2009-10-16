using System;
using System.Collections.Generic;
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
            _clientDetailsView.DisableSaveButton();
            _clientDetailsView.EnableOverviewPanel();

            if (_client == null)
            {
                _clientDetails = null;
                _clientDetailsView.ClientName = string.Empty;
                _clientDetailsView.Street = string.Empty;
                _clientDetailsView.StreetNumber = string.Empty;
                _clientDetailsView.PostalCode = string.Empty;
                _clientDetailsView.City = string.Empty;
                _clientDetailsView.PhoneNumber = string.Empty;
                _clientDetailsView.Accounts = null;
                _clientDetailsView.EnableClientNamePanel();
                _clientDetailsView.ShowDialog();
                return;
            }

            _clientDetails = _repository.GetByExample<ClientDetails>(new { _client.Id }).FirstOrDefault();
            _clientDetailsView.ClientName = _clientDetails.ClientName;
            _clientDetailsView.Street = _clientDetails.Street;
            _clientDetailsView.StreetNumber = _clientDetails.StreetNumber;
            _clientDetailsView.PostalCode = _clientDetails.PostalCode;
            _clientDetailsView.City = _clientDetails.City;
            _clientDetailsView.PhoneNumber = _clientDetails.PhoneNumber;
            _clientDetailsView.Accounts = _clientDetails.Accounts;
            _clientDetailsView.SetIsExistingClient();

            _clientDetailsView.ClientNameLabel = _clientDetails.ClientName;
            _clientDetailsView.PhoneNumberLabel = _clientDetails.PhoneNumber;
            _clientDetailsView.AddressLine1Label = string.Format("{0} {1}", _clientDetails.Street, _clientDetails.StreetNumber);
            _clientDetailsView.AddressLine2Label = string.Format("{0} {1}", _clientDetails.PostalCode, _clientDetails.City);

            _clientDetailsView.ShowDialog();
        }

        public void SetClient(Client client)
        {
            _client = client;
        }

        public void OpenSelectedAccount()
        {
            var client = _clientDetailsView.GetSelectedAccount();
            _accountDetailsPresenter.SetAccount(client);
            _accountDetailsPresenter.Display();
        }

        public void CreateNewAccount()
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
                _clientDetailsView.Close();
                return;
            }

            var messages = new List<IMessage>();

            if (AddressHasChanged())
                messages.Add(new ClientHasMovedCommand(
                    _clientDetails.Id,
                    _clientDetailsView.Street,
                    _clientDetailsView.StreetNumber,
                    _clientDetailsView.PostalCode,
                    _clientDetailsView.City));

            if (PhoneNumberHasChanged())
                messages.Add(new ClientPhoneNumberIsChangedCommand(
                    _clientDetails.Id,
                    _clientDetailsView.PhoneNumber));

            if (ClientNameHasChanged())
                messages.Add(new ClientChangedTheirNameCommand(
                    _clientDetails.Id,
                    _clientDetailsView.ClientName));

            _bus.Publish(messages.AsEnumerable());
        }

        public void FormElementGotChanged()
        {
            _clientDetailsView.DisableSaveButton();

            if (!FormIsValid())
                return;

            if (_clientDetails == null)
            {
                _clientDetailsView.EnableSaveButton();
                return;
            }

            if (FormHasChanged())
            {
                _clientDetailsView.EnableSaveButton();
                return;
            }
        }

        public void SaveNewClientName()
        {
            _bus.Publish(new ClientChangedTheirNameCommand(
                _clientDetails.Id,
                _clientDetailsView.ClientName));

            _clientDetailsView.EnableOverviewPanel();
        }

        public void SaveNewPhoneNumber()
        {
            _bus.Publish(new ClientPhoneNumberIsChangedCommand(
                _clientDetails.Id,
                _clientDetailsView.PhoneNumber));

            _clientDetailsView.EnableOverviewPanel();
        }

        public void SaveNewAddress()
        {
            _bus.Publish(new ClientHasMovedCommand(
                _clientDetails.Id,
                _clientDetailsView.Street,
                _clientDetailsView.StreetNumber,
                _clientDetailsView.PostalCode,
                _clientDetailsView.City));

            _clientDetailsView.EnableOverviewPanel();
        }

        public void Cancel()
        {
            _clientDetailsView.EnableOverviewPanel();
        }

        public void InitialeClientHasMoved()
        {
            _clientDetailsView.EnableAddressPanel();
        }

        public void InitialeClientNameChange()
        {
            _clientDetailsView.EnableClientNamePanel();
        }

        public void InitialeClientPhoneNumberChanged()
        {
            _clientDetailsView.EnablePhoneNumberPanel();
        }

        private bool FormIsValid()
        {
            return
                !string.IsNullOrEmpty(_clientDetailsView.ClientName) &&
                !string.IsNullOrEmpty(_clientDetailsView.Street) &&
                !string.IsNullOrEmpty(_clientDetailsView.StreetNumber) &&
                !string.IsNullOrEmpty(_clientDetailsView.PostalCode) &&
                !string.IsNullOrEmpty(_clientDetailsView.City) &&
                !string.IsNullOrEmpty(_clientDetailsView.PhoneNumber);
        }

        private bool FormHasChanged()
        {
            return
                AddressHasChanged() ||
                PhoneNumberHasChanged() ||
                ClientNameHasChanged();
        }

        private bool AddressHasChanged()
        {
            return
                _clientDetailsView.Street != _clientDetails.Street ||
                _clientDetailsView.StreetNumber != _clientDetails.StreetNumber ||
                _clientDetailsView.PostalCode != _clientDetails.PostalCode ||
                _clientDetailsView.City != _clientDetails.City;
        }

        private bool PhoneNumberHasChanged()
        {
            return _clientDetailsView.PhoneNumber != _clientDetails.PhoneNumber;
        }

        private bool ClientNameHasChanged()
        {
            return _clientDetailsView.ClientName != _clientDetails.ClientName;
        }
    }
}