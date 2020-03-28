using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMdotNET.x64;
using static ASMdotNET.x64.Registers;
using static ASMdotNET.x64.Opcodes;

namespace x64Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCompiler asm = new AssemblyCompiler(0x20AEDBD0000);

            byte[] code = asm.Compile(nop());

            Console.WriteLine(BytesToString(code));
            Console.ReadLine();


        }

        static string BytesToString(byte[] bytes)
        {
            string s = "";
            foreach (byte b in bytes)
            {
                s += b.ToString("X") + " ";
            }
            return s;
        }
    }
}
