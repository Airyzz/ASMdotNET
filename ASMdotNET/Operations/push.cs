using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86.Operations
{
    public class push : Operation
    {
        bool isInteger = false;
        Register register;
        int value;

        public override byte[] compile(IntPtr address)
        {
            if (isInteger)
            {
                //push 1024
                byte[] code = new byte[5];
                code[0] = OpcodeBytes.pushdw;
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, code, 1, 4);
                return code;
            }
            else
            {
                if (register.pointer)
                {
                    if (register.usesOffset)
                    {
                        if (util.isByte(register.appliedOffset))
                        {
                            //push [eax+10]
                            return new byte[] { 0xff, (byte)(0x70 + register.register), (byte)register.appliedOffset };
                        }
                        else
                        {
                            //push [eax,1024]
                            byte[] code = new byte[6];
                            code[0] = 0xFF;
                            code[1] = (byte)(0xB0 + register.register);
                            Buffer.BlockCopy(BitConverter.GetBytes(register.appliedOffset), 0, code, 2, 4);
                            return code;
                        }

                    }
                    else
                    {
                        //push [eax]
                        return new byte[] { 0xFF, (byte)(0x30 + register.register) };
                    }
                }
                //push eax
                return new byte[] { (byte)(OpcodeBytes.push + register.register) };
            }
        }

        public push(Register Register)
        {
            register = Register;
        }

        public push(int Value)
        {
            value = Value;
            isInteger = true;
        }
    }
}
