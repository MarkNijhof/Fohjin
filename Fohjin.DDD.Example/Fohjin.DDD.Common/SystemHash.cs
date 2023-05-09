using System.Security.Cryptography;
using System.Text;

namespace Fohjin.DDD.Common
{
    public class SystemHash : ISystemHash
    {
        public string Hash(string input)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return new Guid(hash).ToString();
        }
    }
}
