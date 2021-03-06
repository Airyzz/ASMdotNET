﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMdotNET;
using static ASMdotNET.Opcodes;
using static ASMdotNET.Registers;

namespace x86Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCompiler asm = new AssemblyCompiler(TargetFramework.x86, 0x001C0000);


            byte[] code = asm.Compile(
                "start:",
                "mov eax,[eax*4+100]",
                "mov eax,[eax*4]",
                mov(eax, ~eax * 4 + 100)
                );
            
            

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
