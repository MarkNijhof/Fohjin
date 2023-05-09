using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fohjin.DDD.Common
{
    public interface ISystemRandom
    {
        int Next(int start, int end);
    }
}
