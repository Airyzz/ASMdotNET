using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86.Operations
{
    class jmp : Operation
    {
        private Register reg;
        private IntPtr FunctionAddress = IntPtr.Zero;

        public override byte[] compile(IntPtr address)
        {
            if(FunctionAddress != IntPtr.Zero)
            {
                //call 0x100000
                byte[] code = new byte[5];
                code[0] = 0xe9;
                int relativeAddress = ((int)IntPtr.Subtract(FunctionAddress, (int)address)) - code.Length;
                Buffer.BlockCopy(BitConverter.GetBytes(relativeAddress), 0, code, 1, 4);
                return code;
            }
            else
            {
                if (reg.pointer)
                {
                    if (reg.usesOffset)
                    {
                        if (util.isByte(reg.appliedOffset))
                        {
                            //call [eax+10]
                            return new byte[] { 0xff, (byte)(0x60 + reg.register), (byte)reg.appliedOffset };
                        }
                        else
                        {
                            //call [eax+1024]
                            byte[] code = new byte[6];
                            code[0] = 0xff;
                            code[1] = (byte)(0xA0 + reg.register);
                            Buffer.BlockCopy(BitConverter.GetBytes(reg.appliedOffset), 0, code, 2, 4);
                            return code;
                        }
                    }
                    else
                    {
                        //call [eax]
                        return new byte[] { 0xff, (byte)(0x20 + reg.register) };
                    }
                }
                else
                {
                    //call eax
                    return new byte[] { 0xff, (byte)(0xE0 + reg.register) };
                }
            }
        }


        public jmp(Register register)
        {
            reg = register;
        }

        public jmp(int functionAddress)
        {
            FunctionAddress = (IntPtr)functionAddress;
        }

        public jmp(IntPtr functionAddress)
        {
            FunctionAddress = functionAddress;
        }
    }
}
