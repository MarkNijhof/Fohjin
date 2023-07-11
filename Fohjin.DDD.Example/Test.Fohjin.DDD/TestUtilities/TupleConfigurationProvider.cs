using Microsoft.Extensions.Configuration;

namespace Test.Fohjin.DDD.TestUtilities
{
    public class TupleConfigurationProvider : ConfigurationProvider
    {
        public TupleConfigurationProvider(params (string key, string value)[] settings)
            : this(settings.AsEnumerable())
        {
        }
        public TupleConfigurationProvider(IEnumerable<(string key, string value)> settings)
        {
            foreach (var (key, value) in settings)
            {
                if (Data.ContainsKey(key))
                    Data[key] = value;
                else
                    Data.Add(key, value);
            }
        }
    }
}