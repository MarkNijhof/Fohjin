using Fohjin.DDD.BankApplication.Views;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public class ClientSearchFormPresenter : IClientSearchFormPresenter
    {
        private readonly IClientSearchFormView _clientSearchFormView;
        private readonly IClientDetailsPresenter _clientDetailsPresenter;

        public ClientSearchFormPresenter(IClientSearchFormView clientSearchFormView, IClientDetailsPresenter clientDetailsPresenter)
        {
            _clientSearchFormView = clientSearchFormView;
            _clientDetailsPresenter = clientDetailsPresenter;
            _clientSearchFormView.SetPresenter(this);
        }

        public void CreateNewClient()
        {
            _clientDetailsPresenter.Display();
        }

        public void OpenSelectedClient()
        {
            var client = _clientSearchFormView.GetSelectedClient();
            _clientDetailsPresenter.SetClient(client);
            _clientDetailsPresenter.Display();
        }

        public void Display()
        {
            _clientSearchFormView.ShowDialog();
        }
    }
}