using System;
using System.Windows.Forms;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using NUnit.Framework;


namespace Test.Fohjin.DDD.Presenter
{
    [TestFixture]
    public class PresenterTest
    {
        [Test]
        public void The_presenter_base_class_will_hook_up_the_view_events_with_the_presenter_event_handlers()
        {
            var testView = new TestView();
            var testPresenter = new TestPresenter(testView);

            testView.Test();

            Assert.That(testPresenter.TestValue, Is.True);
        }
    }

    public class TestPresenter : Presenter<ITestView>
    {
        public bool TestValue { get; set; }
        public TestPresenter(ITestView view) : base(view)
        {
            TestValue = false;
        }

        public void Test()
        {
            TestValue = true;
        }
    }

    public interface ITestView : IView
    {
        event EventAction OnTest;
    }

    public class TestView : ITestView
    {
        public event EventAction OnTest;

        public void Test()
        {
            OnTest();
        }

        // IView Interface plumbing
        public void Dispose()
        {
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
        }
    }
}