using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x64
{
    public abstract class Operation
    {
        public abstract byte[] compile(IntPtr address);
    }
}
