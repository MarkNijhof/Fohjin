using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.TestUtilities
{
    public static class TestContextExtensions
    {
        public const string TestWorkingDirectory = nameof(TestWorkingDirectory);
        public static TestContext SetupWorkingDirectory(this TestContext context)
        {
            var testClass = Type.GetType(context.FullyQualifiedTestClassName);

            var testName = context.TestName.Length <= 60 ? context.TestName :
                context.TestName[..29] + "-" + context.TestName[^29..];

            var path = Path.Combine(
                context.DeploymentDirectory,
                testClass.Name,
                testName
                );

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Environment.CurrentDirectory = path;
            context.Properties.Add(TestWorkingDirectory, path);

            return context;
        }
    }
}
