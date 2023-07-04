using Microsoft.Extensions.Configuration;

namespace Test.Fohjin.DDD.TestUtilities
{
    public class TupleConfigurationProvider : ConfigurationProvider
    {
        public TupleConfigurationProvider(params (string key, string? value)[] settings)
            : this(settings.AsEnumerable())
        {
        }
        public TupleConfigurationProvider(IEnumerable<(string key, string? value)> settings)
        {
            foreach (var item in settings)
            {
                if (Data.ContainsKey(item.key))
                    Data[item.key] = item.value;
                else
                    Data.Add(item.key, item.value);
            }
        }
    }
}