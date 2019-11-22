using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86
{
    public class OpcodeBytes
    {
        public static byte nop = 0x90;
        public static byte push = 0x50;
        public static byte pushdw = 0x68;
        public static byte movdw = 0xb8;
        public static byte ret = 0xc3;
    }
}
