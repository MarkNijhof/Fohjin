using System;
using System.Windows.Forms;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Presenter
{
    [TestClass]
    public class PresenterTest
    {
        [TestMethod]
        public void The_presenter_base_class_will_hook_up_the_view_events_with_the_presenter_event_handlers()
        {
            var testView = new TestView();
            var testPresenter = new TestPresenter(testView);

            testView.Test();

            Assert.AreEqual(testPresenter.TestValue, Is.True);
        }
    }
}