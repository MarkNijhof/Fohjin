using System.Security.Cryptography;
using System.Text;

namespace Fohjin.DDD.Common
{
    public class SystemHash : ISystemHash
    {
        public string Hash(string input)
        {
            using var md5 = MD5.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = md5.ComputeHash(bytes);
            var guid = new Guid(hash);
            return guid.ToString();
        }
    }
}
