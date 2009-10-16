using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public interface IClientDetailsView : IView<IClientDetailsPresenter>
    {
        string ClientName { get; set; }
        string Street { get; set; }
        string StreetNumber { get; set; }
        string PostalCode { get; set; }
        string City { get; set; }
        string PhoneNumber { get; set; }
        
        string ClientNameLabel { set; }
        string AddressLine1Label { set; }
        string AddressLine2Label { set; }
        string PhoneNumberLabel { set; }

        IEnumerable<Account> Accounts { get; set; }
        Account GetSelectedAccount();
        
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
    }
}