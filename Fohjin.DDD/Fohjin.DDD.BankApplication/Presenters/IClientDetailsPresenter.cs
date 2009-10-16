using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Presenters
{
    public interface IClientDetailsPresenter : IPresenter
    {
        void SetClient(Client client);
        void OpenSelectedAccount();
        void CreateNewAccount();
        void FormElementGotChanged();
        void Cancel();
        void SaveNewClientName();
        void SaveNewPhoneNumber();
        void SaveNewAddress();
        void InitialeClientHasMoved();
        void InitialeClientNameChange();
        void InitialeClientPhoneNumberChanged();
    }
}