using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class ClientSearchFormPresenter : IClientSearchFormPresenter
    {
        private readonly IClientSearchFormView _clientSearchFormView;
        private readonly IClientDetailsPresenter _clientDetailsPresenter;
        private readonly IRepository _repository;

        public ClientSearchFormPresenter(IClientSearchFormView clientSearchFormView, IClientDetailsPresenter clientDetailsPresenter, IRepository repository)
        {
            _clientSearchFormView = clientSearchFormView;
            _clientDetailsPresenter = clientDetailsPresenter;
            _repository = repository;
            _clientSearchFormView.SetPresenter(this);
        }

        public void CreateNewClient()
        {
            _clientDetailsPresenter.SetClient(null);
            _clientDetailsPresenter.Display();
        }

        public void OpenSelectedClient()
        {
            var client = _clientSearchFormView.GetSelectedClient();
            _clientDetailsPresenter.SetClient(client);
            _clientDetailsPresenter.Display();
        }

        public void Refresh()
        {
            LoadData();
        }

        public void Display()
        {
            LoadData();
            try
            {
                _clientSearchFormView.ShowDialog();
            }
            finally
            {
                _clientSearchFormView.Dispose();
            }
        }

        private void LoadData()
        {
            _clientSearchFormView.Clients = _repository.GetByExample<Client>(null);
        }
    }
}