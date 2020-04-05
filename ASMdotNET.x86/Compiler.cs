using Binarysharp.Assemblers.Fasm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASMdotNET.Registers;

namespace ASMdotNET
{
    public enum TargetFramework
    {
        x86,
        x64
    }

    public class AssemblyCompiler
    {
        byte[] code = new byte[] { };
        List<object> operations = new List<object> { };
        IntPtr Address = IntPtr.Zero;
        TargetFramework framework;

        public AssemblyCompiler(TargetFramework targetFramework, IntPtr address)
        {
            framework = targetFramework; 
            Address = address;
        }

        public AssemblyCompiler(TargetFramework targetFramework, int address)
        {
            framework = targetFramework;
            Address = (IntPtr)address;
        }

        public AssemblyCompiler(TargetFramework targetFramework, long address)
        {
            framework = targetFramework;
            Address = (IntPtr)address;
        }

        public AssemblyCompiler(TargetFramework targetFramework)
        {
            framework = targetFramework;
        }

        public byte[] Compile(params object[] statements)
        {
            var asm = new FasmNet(100000, 100);
            if (framework == TargetFramework.x86)
            {
                asm.AddLine("use32");
            }
            else
            {
                asm.AddLine("use64");
            }


            if(statements.Length == 0)
            {
                foreach (object op in operations)
                {
                    if (op.GetType() == typeof(Operation))
                    {
                        asm.AddLine(((Operation)op).op);
                    }
                    else if (op.GetType() == typeof(string))
                    {
                        asm.AddLine((string)op);
                    }
                    else
                    {
                        throw new InvalidOperationException("Unsupported object in Compile");
                    }
                }
                return asm.Assemble(Address);
            }

            foreach (object op in statements)
            {
                if (op.GetType() == typeof(Operation))
                {
                    asm.AddLine(((Operation)op).op);
                }
                else if (op.GetType() == typeof(string))
                {
                    asm.AddLine((string)op);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported object in Compile");
                }
            }
            return asm.Assemble(Address);
        }

        public byte[] Compile(IntPtr OverrideAddress, params object[] statements)
        {
            var asm = new FasmNet();
            if (framework == TargetFramework.x86)
            {
                asm.AddLine("use32");
            }
            else
            {
                asm.AddLine("use64");
            }

            foreach (object op in statements)
            {
                if (op.GetType() == typeof(Operation))
                {
                    asm.AddLine(((Operation)op).op);
                    Console.WriteLine(((Operation)op).op);
                }
                else if(op.GetType() == typeof(string))
                {
                    asm.AddLine((string)op);
                    Console.WriteLine((string)op);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported object in Compile");
                }
            }
            return asm.Assemble(OverrideAddress);
        }

        public void Add(params object[] opcodes)
        {
            foreach(object operation in opcodes)
            {
                operations.Add(operation);
            }
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
