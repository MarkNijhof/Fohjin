using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IClientDetailsPresenter : IPresenter
    {
        void SetClient(Client client);
        void OpenSelectedAccount();
        void CreateNewAccountAndAttachToClient();
        void SaveClientChanges();
        void FormElementGotChanged();
    }
}