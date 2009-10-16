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
        private bool _createNewProcess;
        private int _editStep;
        private Client _client;
        private ClientDetails _clientDetails;
        private readonly IClientDetailsView _clientDetailsView;
        private readonly IAccountDetailsPresenter _accountDetailsPresenter;
        private readonly ICommandBus _bus;
        private readonly IRepository _repository;

        public ClientDetailsPresenter(IClientDetailsView clientDetailsView, IAccountDetailsPresenter accountDetailsPresenter, ICommandBus bus, IRepository repository)
        {
            _editStep = 0;
            _createNewProcess = false;
            _clientDetailsView = clientDetailsView;
            _accountDetailsPresenter = accountDetailsPresenter;
            _bus = bus;
            _repository = repository;
            _clientDetailsView.SetPresenter(this);
        }

        public void Display()
        {
            _createNewProcess = false;
            _clientDetailsView.DisableSaveButton();
            _clientDetailsView.EnableOverviewPanel();
            DisableAllMenuButtons();

            if (_client == null)
            {
                _editStep = 1;
                _createNewProcess = true;
                _clientDetails = new ClientDetails(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                ResetForm();
                _clientDetailsView.EnableClientNamePanel();
                _clientDetailsView.ShowDialog();
                return;
            }

            _clientDetails = _repository.GetByExample<ClientDetails>(new { _client.Id }).FirstOrDefault();

            SetClientDetailsData();
            SetReadOnlyData();

            EnableAllMenuButtons();
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

        public void FormElementGotChanged()
        {
            _clientDetailsView.DisableSaveButton();

            if (!FormIsValid())
                return;

            if (_createNewProcess)
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
            _clientDetailsView.DisableSaveButton();
            if (_createNewProcess)
            {
                _editStep = 2;
                _clientDetails = new ClientDetails(
                    _clientDetails.Id,
                    _clientDetailsView.ClientName,
                    _clientDetails.Street,
                    _clientDetails.StreetNumber,
                    _clientDetails.PostalCode,
                    _clientDetails.City,
                    _clientDetails.PhoneNumber);

                _clientDetailsView.EnableAddressPanel();
                return;
            }

            _bus.Publish(new ClientChangedTheirNameCommand(
                _clientDetails.Id,
                _clientDetailsView.ClientName));

            _clientDetails = new ClientDetails(
                _clientDetails.Id,
                _clientDetailsView.ClientName,
                _clientDetails.Street,
                _clientDetails.StreetNumber,
                _clientDetails.PostalCode,
                _clientDetails.City,
                _clientDetails.PhoneNumber);

            EnableAllMenuButtons();
            _clientDetailsView.EnableOverviewPanel();
        }

        public void SaveNewAddress()
        {
            _clientDetailsView.DisableSaveButton();
            if (_createNewProcess)
            {
                _editStep = 3;
                _clientDetails = new ClientDetails(
                    _clientDetails.Id,
                    _clientDetails.ClientName,
                    _clientDetailsView.Street,
                    _clientDetailsView.StreetNumber,
                    _clientDetailsView.PostalCode,
                    _clientDetailsView.City,
                    _clientDetails.PhoneNumber);

                _clientDetailsView.EnablePhoneNumberPanel();
                return;
            }

            _bus.Publish(new ClientHasMovedCommand(
                _clientDetails.Id,
                _clientDetailsView.Street,
                _clientDetailsView.StreetNumber,
                _clientDetailsView.PostalCode,
                _clientDetailsView.City));

            _clientDetails = new ClientDetails(
                _clientDetails.Id,
                _clientDetails.ClientName,
                _clientDetailsView.Street,
                _clientDetailsView.StreetNumber,
                _clientDetailsView.PostalCode,
                _clientDetailsView.City,
                _clientDetails.PhoneNumber);

            EnableAllMenuButtons();
            _clientDetailsView.EnableOverviewPanel();
        }

        public void SaveNewPhoneNumber()
        {
            _clientDetailsView.DisableSaveButton();
            if (_createNewProcess)
            {
                _bus.Publish(new ClientCreatedCommand(
                    Guid.NewGuid(),
                    _clientDetails.ClientName,
                    _clientDetails.Street,
                    _clientDetails.StreetNumber,
                    _clientDetails.PostalCode,
                    _clientDetails.City,
                    _clientDetailsView.PhoneNumber));

                _clientDetailsView.Close();
                return;
            }

            _bus.Publish(new ClientPhoneNumberIsChangedCommand(
                _clientDetails.Id,
                _clientDetailsView.PhoneNumber));

            _clientDetails = new ClientDetails(
                _clientDetails.Id,
                _clientDetails.ClientName,
                _clientDetails.Street,
                _clientDetails.StreetNumber,
                _clientDetails.PostalCode,
                _clientDetails.City,
                _clientDetailsView.PhoneNumber);

            EnableAllMenuButtons();
            _clientDetailsView.EnableOverviewPanel();
        }

        public void Cancel()
        {
            if (_createNewProcess)
            {
                _clientDetailsView.Close();
                return;
            }

            EnableAllMenuButtons();
            _clientDetailsView.EnableOverviewPanel();
            SetClientDetailsData();
        }

        public void InitialeClientHasMoved()
        {
            _editStep = 1;
            DisableAllMenuButtons();
            _clientDetailsView.EnableAddressPanel();
        }

        public void InitialeClientNameChange()
        {
            _editStep = 3;
            DisableAllMenuButtons();
            _clientDetailsView.EnableClientNamePanel();
        }

        public void InitialeClientPhoneNumberChanged()
        {
            _editStep = 2;
            DisableAllMenuButtons();
            _clientDetailsView.EnablePhoneNumberPanel();
        }

        private void SetReadOnlyData() 
        {
            _clientDetailsView.ClientNameLabel = _clientDetails.ClientName;
            _clientDetailsView.PhoneNumberLabel = _clientDetails.PhoneNumber;
            _clientDetailsView.AddressLine1Label = string.Format("{0} {1}", _clientDetails.Street, _clientDetails.StreetNumber);
            _clientDetailsView.AddressLine2Label = string.Format("{0} {1}", _clientDetails.PostalCode, _clientDetails.City);
        }

        private void ResetForm() 
        {
            _clientDetailsView.ClientName = string.Empty;
            _clientDetailsView.Street = string.Empty;
            _clientDetailsView.StreetNumber = string.Empty;
            _clientDetailsView.PostalCode = string.Empty;
            _clientDetailsView.City = string.Empty;
            _clientDetailsView.PhoneNumber = string.Empty;
            _clientDetailsView.Accounts = null;
        }

        private void DisableAllMenuButtons() 
        {
            _clientDetailsView.DisableAddNewAccountMenu();
            _clientDetailsView.DisableClientHasMovedMenu();
            _clientDetailsView.DisableNameChangedMenu();
            _clientDetailsView.DisablePhoneNumberChangedMenu();
        }

        private void SetClientDetailsData()
        {
            _clientDetailsView.ClientName = _clientDetails.ClientName;
            _clientDetailsView.Street = _clientDetails.Street;
            _clientDetailsView.StreetNumber = _clientDetails.StreetNumber;
            _clientDetailsView.PostalCode = _clientDetails.PostalCode;
            _clientDetailsView.City = _clientDetails.City;
            _clientDetailsView.PhoneNumber = _clientDetails.PhoneNumber;
            _clientDetailsView.Accounts = _clientDetails.Accounts;
        }

        private void EnableAllMenuButtons() 
        {
            _clientDetailsView.EnableAddNewAccountMenu();
            _clientDetailsView.EnableClientHasMovedMenu();
            _clientDetailsView.EnableNameChangedMenu();
            _clientDetailsView.EnablePhoneNumberChangedMenu();
        }

        private bool FormIsValid()
        {
            if (_editStep == 0)
                return true;

            if (_editStep == 1)
                return !string.IsNullOrEmpty(_clientDetailsView.ClientName);

            if (_editStep == 2)
                return
                    !string.IsNullOrEmpty(_clientDetailsView.Street) &&
                    !string.IsNullOrEmpty(_clientDetailsView.StreetNumber) &&
                    !string.IsNullOrEmpty(_clientDetailsView.PostalCode) &&
                    !string.IsNullOrEmpty(_clientDetailsView.City);

            if (_editStep == 3)
                return !string.IsNullOrEmpty(_clientDetailsView.PhoneNumber);

            throw new Exception("Edit step was not properly initialized!");
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