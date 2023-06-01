using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.TestUtilities
{
    public class ContextualTestMethodAttribute : TestMethodAttribute
    {
        public const string CurrentTestMethod = nameof(CurrentTestMethod);
        public const string CurrentTestInstance = nameof(CurrentTestInstance);

        private readonly static AsyncLocal<ITestMethod?> _current = new();
        private readonly static AsyncLocal<object?> _instance = new();

        public static ITestMethod? Current => _current.Value;
        public static object? Instance
        {
            get => _instance.Value;
            set => _instance.Value = value;
        }

        public ContextualTestMethodAttribute()
        {
        }

        public ContextualTestMethodAttribute(string? displayName) : base(displayName)
        {
        }

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            _current.Value = testMethod;
            var ret = base.Execute(testMethod);
            _current.Value = null;
            return ret;
        }
    }
}