using Microsoft.Extensions.Configuration;

namespace Test.Fohjin.DDD.TestUtilities
{
    public class TupleConfigurationSource : IConfigurationSource
    {
        private readonly IReadOnlyList<(string key, string? value)> _config;

        public TupleConfigurationSource(params (string key, string? value)[] settings) => _config = settings;
        public TupleConfigurationSource(IEnumerable<(string key, string? value)> settings) => _config = settings.ToArray();

        public IConfigurationProvider Build(IConfigurationBuilder builder) => new TupleConfigurationProvider(_config);

    }
}