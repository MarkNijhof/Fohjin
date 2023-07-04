using Microsoft.Extensions.Configuration;
using System.Collections;

namespace Test.Fohjin.DDD.TestUtilities
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddTupleConfiguration(
            this IConfigurationBuilder builder,
            params (string key, string? value)[] settings) =>
            builder.Add(new TupleConfigurationSource(settings));

        public static IConfigurationBuilder AddTupleConfiguration(
            this IConfigurationBuilder builder,
            IEnumerable<(string key, string? value)> settings) =>
            builder.Add(new TupleConfigurationSource(settings));

        public static IConfigurationBuilder AddTupleConfiguration(
            this IConfigurationBuilder builder,
            IEnumerable<KeyValuePair<string, string?>> settings) =>
            builder.Add(new TupleConfigurationSource(settings.Select(k => (k.Key, k.Value))));

        public static IConfigurationBuilder AddTupleConfiguration(
            this IConfigurationBuilder builder,
            IDictionary settings) =>
            builder.Add(new TupleConfigurationSource(
                settings.OfType<KeyValuePair<object, object?>>()
                        .Where(k => k.Key != null && k.Value != null)
                        .Select(k => (k.Key.ToString() ?? "", k.Value?.ToString()))
                ));
    }
}