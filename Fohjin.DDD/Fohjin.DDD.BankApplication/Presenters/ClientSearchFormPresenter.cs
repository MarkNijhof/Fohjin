using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class ClientSearchFormPresenter : Presenter<IClientSearchFormView>, IClientSearchFormPresenter
    {
        private readonly IClientSearchFormView _clientSearchFormView;
        private readonly IClientDetailsPresenter _clientDetailsPresenter;
        private readonly IReportingRepository _reportingRepository;

        public ClientSearchFormPresenter(IClientSearchFormView clientSearchFormView, IClientDetailsPresenter clientDetailsPresenter, IReportingRepository reportingRepository) : base(clientSearchFormView)
        {
            _clientSearchFormView = clientSearchFormView;
            _clientDetailsPresenter = clientDetailsPresenter;
            _reportingRepository = reportingRepository;
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
            _clientSearchFormView.Clients = _reportingRepository.GetByExample<Client>(null);
        }
    }
}