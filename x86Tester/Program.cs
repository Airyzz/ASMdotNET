using ASMdotNET.x86;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ASMdotNET.x86.Opcodes;
using static ASMdotNET.x86.Registers;

namespace x86Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCompiler asm = new AssemblyCompiler(0x02DD0000);


            asm.Compile();

            asm.Add(
                mov(eax, 0x02DD002D),
                mov(edx, ~eax),
                mov(eax, 0x02DD0030),
                mov(~eax, edx),
                nop(),
                nop(),
                mov(~(ecx + 0x1024), esi),
                mov(~ecx + 0x10, esi),
                mov(edx, ~eax + 0x16),
                mov(esi, ~edx + 0x01024),
                nop(),
                nop(),
                push(~eax),
                push(eax),
                push(~esi),
                push(~edx + 0x10),
                push(~esi + 0x1024),
                call(0x02DD00E0),
                call(esi),
                call(~edx),
                call(~esi + 0x10),
                call(~esi + 0x1000),
                call(0x02DD0000),
                jmp(0x02dd0000),
                jmp(edx),
                jmp(~esi),
                jmp(~esi + 0x10),
                jmp(~esi + 0x1024),
                pop(eax),
                pop(ecx),
                pop(~eax + 0x10),
                pop(~ecx + 0x1024),
                pop(0x123456),
                sub(eax, ecx),
                sub(edx, 0x10),
                sub(esi, 0x1000),
                sub(eax, ~edi),
                sub(~ecx, eax),
                sub(~edi, 0x08),
                sub(~esi, 0x1000),
                ret(),
                add(eax,ecx),
                add(~esi, edx),
                add(edx, 0x1000),
                add(~esi, 0x256),
                add(edx, ~esi)

                //pop(eax + 10)
                );

            byte[] code = asm.Compile();

            Console.WriteLine(BytesToString(code));
            Console.ReadLine();
        }

        static string BytesToString(byte[] bytes)
        {
            string s = "";
            foreach(byte b in bytes)
            {
                s += b.ToString("X") + " ";
            }
            return s;
        }
    }
}
