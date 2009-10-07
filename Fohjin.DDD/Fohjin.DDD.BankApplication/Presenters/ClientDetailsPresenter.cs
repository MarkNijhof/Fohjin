using System;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class ClientDetailsPresenter : IClientDetailsPresenter
    {
        private ClientDetailsDto _clientDetailsDto;
        private readonly IClientDetailsView _clientDetailsView;
        private readonly ICommandBus _bus;

        public ClientDetailsPresenter(IClientDetailsView clientDetailsView, ICommandBus bus)
        {
            _clientDetailsView = clientDetailsView;
            _bus = bus;
            _clientDetailsView.SetPresenter(this);
        }

        public void Display()
        {
            _clientDetailsView.ShowDialog();
        }

        public void SetClient(ClientDto clientDto)
        {
            if (clientDto == null)
                return;

        }

        public void OpenSelectedAccount()
        {
            throw new NotImplementedException();
        }

        public void CreateNewAccountAndAttachToClient()
        {
            throw new NotImplementedException();
        }

        public void SaveClientChanges()
        {
            if (_clientDetailsDto == null)
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