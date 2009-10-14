namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IClientSearchFormPresenter : IPresenter
    {
        void CreateNewClient();
        void OpenSelectedClient();
        void Refresh();
    }
}