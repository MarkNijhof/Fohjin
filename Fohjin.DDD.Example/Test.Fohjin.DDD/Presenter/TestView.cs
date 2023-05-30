namespace Test.Fohjin.DDD.Presenter
{
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