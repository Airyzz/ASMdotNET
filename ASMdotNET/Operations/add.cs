using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86.Operations
{
    public class add : Operation
    {
        bool isInteger = false;
        Register R1;
        Register R2;
        int value;

        public override byte[] compile(IntPtr address)
        {
            if (R2 != null)
            {
                if (R1.pointer && R2.pointer)
                    throw new ArithmeticException("Invalid ASM. sub cannot be used with two pointers");
            }

            if (isInteger)
            {
                if(util.isByte(value))
                {
                    if (R1.pointer)
                    {
                        //add [eax],08
                        return new byte[] { 0x83, (byte)(0x88 + R1.register), (byte)value };
                    }
                    else
                    {
                        //add eax,08
                        return new byte[] { 0x83, (byte)(0xc0 + R1.register), (byte)value };
                    }
                }
                else
                {
                    if (R1.pointer)
                    {
                        //add [eax],0x1000
                        byte[] code = new byte[6];
                        code[0] = 0x81;
                        code[1] = (byte)(0x00 + R1.register);
                        Buffer.BlockCopy(BitConverter.GetBytes(value), 0, code, 2, 4);
                        return code;
                    }
                    else
                    {
                        //add eax,0x1000
                        byte[] code = new byte[6];
                        code[0] = 0x81;
                        code[1] = (byte)(0xc0 + R1.register);
                        Buffer.BlockCopy(BitConverter.GetBytes(value), 0, code, 2, 4);
                        return code;
                    }
                }
            }
            else
            {
                if (R1.pointer)
                {
                    //add [eax],eax
                    byte registerCode = (byte)(R1.register + 0x8 * (byte)R2.register);
                    return new byte[] { 0x01, registerCode };
                }
                else if (R2.pointer)
                {
                    //sub eax,[eax]
                    byte registerCode = (byte)(R2.register + 0x8 * (byte)R1.register);
                    return new byte[] { 0x03, registerCode };
                }
                else
                {
                    //sub eax,eax
                    byte registerCode = (byte)( 0xc0 + R1.register + 0x8 * (byte)R2.register);
                    return new byte[] { 01, registerCode };
                }

            }
        }

        public add(Register r1, Register r2)
        {
            R1 = r1;
            R2 = r2;
        }

        public add(Register r1, int Value)
        {
            R1 = r1;
            value = Value;
            isInteger = true;
        }
    }
}
