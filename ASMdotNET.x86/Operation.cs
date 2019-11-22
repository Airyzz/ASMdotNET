using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86
{
    public static class util
    {
        public static bool isByte(int value)
        {
            return (value <= byte.MaxValue && value >= byte.MinValue);
        }
    }

    public abstract class Operation
    {
        public abstract byte[] compile(IntPtr address);
    }
}
