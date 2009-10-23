using System.Collections.Generic;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IClientDetailsView : IView
    {
        string ClientName { get; set; }
        string Street { get; set; }
        string StreetNumber { get; set; }
        string PostalCode { get; set; }
        string City { get; set; }
        string PhoneNumber { get; set; }
        string NewAccountName { get; set; }
        
        string ClientNameLabel { set; }
        string AddressLine1Label { set; }
        string AddressLine2Label { set; }
        string PhoneNumberLabel { set; }

        IEnumerable<Account> Accounts { get; set; }
        Account GetSelectedAccount();

        IEnumerable<ClosedAccount> ClosedAccounts { get; set; }
        ClosedAccount GetSelectedClosedAccount();
        
        void EnableAddNewAccountMenu();
        void DisableAddNewAccountMenu();
        void EnableClientHasMovedMenu();
        void DisableClientHasMovedMenu();
        void EnableNameChangedMenu();
        void DisableNameChangedMenu();
        void EnablePhoneNumberChangedMenu();
        void DisablePhoneNumberChangedMenu();

        void EnableSaveButton();
        void DisableSaveButton();
        void EnableOverviewPanel();
        void EnableAddressPanel();
        void EnableClientNamePanel();
        void EnablePhoneNumberPanel();
        void EnableAddNewAccountPanel();

        event Action OnOpenSelectedAccount;
        event Action OnOpenSelectedClosedAccount;
        event Action OnFormElementGotChanged;
        event Action OnCancel;
        event Action OnSaveNewClientName;
        event Action OnSaveNewPhoneNumber;
        event Action OnSaveNewAddress;
        event Action OnInitiateClientHasMoved;
        event Action OnInitiateClientNameChange;
        event Action OnInitiateClientPhoneNumberChanged;
        event Action OnInitiateAddNewAccount;
        event Action OnCreateNewAccount;
        event Action OnRefresh;
    }
}