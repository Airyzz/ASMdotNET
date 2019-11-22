using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASMdotNET.x86.Registers;

namespace ASMdotNET.x86
{
    public class AssemblyCompiler
    {
        byte[] code = new byte[] { };
        IntPtr Address;

        public AssemblyCompiler(IntPtr address)
        {
            Address = address;
        }

        public AssemblyCompiler(int address)
        {
            Address = (IntPtr)address;
        }

        public byte[] Compile(params Operation[] statements)
        {
            byte[] assembly = new byte[] { };
            foreach(Operation statement in statements)
            {
                byte[] operation = statement.compile(Address);
                Address = IntPtr.Add(Address, operation.Length);
                assembly = Combine(assembly, operation );
                //resetRegisterFlags();
            }
            return assembly;
        }

        private byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        private void resetRegisterFlags()
        {
            eax.pointer = false;
            ecx.pointer = false;
            edx.pointer = false;
            ebx.pointer = false;
            esp.pointer = false;
            ebp.pointer = false;
            esi.pointer = false;
            edi.pointer = false;
        }
    }
}
