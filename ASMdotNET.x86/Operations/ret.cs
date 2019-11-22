using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86.Operations
{
    public class ret : Operation
    {
        public override byte[] compile(IntPtr address)
        {
            return new byte[] { OpcodeBytes.ret };
        }
    }
}
