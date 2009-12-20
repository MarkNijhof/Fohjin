using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class ClientSearchFormPresenter : Presenter<IClientSearchFormView>, IClientSearchFormPresenter
    {
        private readonly IClientSearchFormView _clientSearchFormView;
        private readonly IPopupPresenter _popupPresenter;
        private readonly IClientDetailsPresenter _clientDetailsPresenter;
        private readonly IReportingRepository _reportingRepository;

        public ClientSearchFormPresenter(IClientSearchFormView clientSearchFormView, IClientDetailsPresenter clientDetailsPresenter, IPopupPresenter popupPresenter, IReportingRepository reportingRepository) : base(clientSearchFormView)
        {
            _clientSearchFormView = clientSearchFormView;
            _popupPresenter = popupPresenter;
            _clientDetailsPresenter = clientDetailsPresenter;
            _reportingRepository = reportingRepository;
        }

        public void CreateNewClient()
        {
            _clientDetailsPresenter.SetClient(null);
            _clientDetailsPresenter.Display();
            SystemTimer.Trigger(LoadData).In(2000);
        }

        public void OpenSelectedClient()
        {
            _popupPresenter.CatchPossibleException(() =>
            {
                var client = _clientSearchFormView.GetSelectedClient();
                _clientDetailsPresenter.SetClient(client);
                _clientDetailsPresenter.Display();
            });
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
            _clientSearchFormView.Clients = _reportingRepository.GetByExample<ClientReport>(null);
        }
    }
}