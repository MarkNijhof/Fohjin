using System;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;

namespace Test.Fohjin.DDD.Scenarios.Displaying_the_error_popup
{
    public class When_in_the_GUI_displaying_the_error_popup : PresenterTestFixture<PopupPresenter>
    {
        protected override void When()
        {
            Presenter.CatchPossibleException(() =>
                {
                    throw new Exception("Message");
                });
        }

        [Then]
        public void Then_the_name_of_the_exception_is_loaded_in_the_view()
        {
            On<IPopupView>().VerifyThat.ValueIsSetFor(x => x.Exception = "Exception");
        }

        [Then]
        public void Then_the_message_of_the_exception_is_loaded_in_the_view()
        {
            On<IPopupView>().VerifyThat.ValueIsSetFor(x => x.Message = "Message");
        }

        [Then]
        public void Then_display_is_called()
        {
            On<IPopupView>().VerifyThat.Method(x => x.ShowDialog()).WasCalled();
        }
    }
}