using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x64
{
    class Util
    {
        public static bool isByte(int value)
        {
            return (value <= byte.MaxValue && value >= byte.MinValue);
        }
    }
}
