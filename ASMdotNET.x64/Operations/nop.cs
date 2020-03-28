using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x64.Operations
{
    public class nop : Operation
    {
        public override byte[] compile(IntPtr address)
        {
            return new byte[] { (byte)0x90 };
        }
    }
}
