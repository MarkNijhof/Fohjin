using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace Test.Fohjin.DDD.TestUtilities;

public static class TestContextExtensions
{
    public const string TestWorkingDirectory = nameof(TestWorkingDirectory);
    public static TestContext SetupWorkingDirectory(this TestContext context)
    {
        var path = context.GetPathForTest();
        if (!Directory.Exists(path) && path != null)
            Directory.CreateDirectory(path);
        Environment.CurrentDirectory = path ?? ".";
        context.Properties.Add(TestWorkingDirectory, path);

        return context;
    }

    public static string? GetPathForTest(this TestContext context)
    {
        if (context.FullyQualifiedTestClassName == null ||
            context.DeploymentDirectory == null)
        {
            return null;
        }

        var testClass = Type.GetType(context.FullyQualifiedTestClassName);
        var testName = context.GetFileNameForTest();

        if (testClass?.Name == null)
        {
            return null;
        }

        var path = Path.Combine(
            context.DeploymentDirectory,
            testClass.Name,
            testName
            );
        return path;
    }

    public static string GetFileNameForTest(this TestContext context, int maxLenght = 60) =>
         context.TestName?.Length <= maxLenght ? context.TestName :
            context.TestName?[..(maxLenght / 2 - 1)] + "-" + context.TestName?[^(maxLenght / 2 - 1)..];

    public static T? GetTestProperty<T>(this TestContext context, string key) =>
         (T?)context.Properties[key];

    public static TestContext AddResults(this TestContext context, string name, object? results)
    {
        if (results == null)
            return context;

        var path = context.GetTestProperty<string>(TestWorkingDirectory) ?? context.GetPathForTest();

        if (context.Properties["RUN_ID"] != null && path != null)
            path = Path.Combine(path, context.Properties["RUN_ID"]?.ToString() ?? "");

        if (!Directory.Exists(path) && path != null)
            Directory.CreateDirectory(path);

        var target = Path.Combine(path ?? ".", name + ".json");

        using var file = File.Create(target);
        JsonSerializer.Serialize(file, results, new JsonSerializerOptions
        {
            WriteIndented = true,
        });
        file.Flush();
        context.AddResultFile(target);
        context.WriteLine($"Added: {target}");

        return context;
    }

    public static TestContext GetResults<T>(this TestContext context, string name, out T result) =>
        GetResults(context, name, out result);

    public static TestContext GetResults(this TestContext context, string name, Type type, out object? result)
    {

        var path = context.GetTestProperty<string>(TestWorkingDirectory) ?? context.GetPathForTest();
        if (!Directory.Exists(path) && path != null)
            Directory.CreateDirectory(path);

        var target = Path.Combine(path ?? ".", name + ".json");
        using var file = File.OpenRead(target);

        result = JsonSerializer.Deserialize(file, type, new JsonSerializerOptions
        {
            WriteIndented = true,
        });
        file.Flush();
        context.AddResultFile(target);
        context.WriteLine($"Read: {target}");

        return context;
    }
}
