using System.Security.Cryptography;
using System.Text;

namespace Fohjin.DDD.Common;

public class SystemHash : ISystemHash
{
    public string? Hash(string? input)
    {
        if (input == null)
            return null;
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return new Guid(hash).ToString();
    }
}
