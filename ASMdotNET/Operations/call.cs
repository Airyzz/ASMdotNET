using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86.Operations
{
    class call : Operation
    {
        private Register reg;
        private IntPtr FunctionAddress = IntPtr.Zero;

        public override byte[] compile(IntPtr address)
        {
            if(FunctionAddress != IntPtr.Zero)
            {
                //call 0x100000
                byte[] code = new byte[5];
                code[0] = 0xe8;
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
                            return new byte[] { 0xff, (byte)(0x50 + reg.register), (byte)reg.appliedOffset };
                        }
                        else
                        {
                            //call [eax+1024]
                            byte[] code = new byte[6];
                            code[0] = 0xff;
                            code[1] = (byte)(0x90 + reg.register);
                            Buffer.BlockCopy(BitConverter.GetBytes(reg.appliedOffset), 0, code, 2, 4);
                            return code;
                        }
                    }
                    else
                    {
                        //call [eax]
                        return new byte[] { 0xff, (byte)(0x10 + reg.register) };
                    }
                }
                else
                {
                    //call eax
                    return new byte[] { 0xff, (byte)(0xD0 + reg.register) };
                }
            }
        }


        public call(Register register)
        {
            reg = register;
        }

        public call(int functionAddress)
        {
            FunctionAddress = (IntPtr)functionAddress;
        }

        public call(IntPtr functionAddress)
        {
            FunctionAddress = functionAddress;
        }
    }
}
