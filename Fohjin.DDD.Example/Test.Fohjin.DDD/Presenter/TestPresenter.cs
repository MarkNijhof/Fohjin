namespace Test.Fohjin.DDD.Presenter
{
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
}