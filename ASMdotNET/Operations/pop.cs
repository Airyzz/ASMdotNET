using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86.Operations
{
    public class pop : Operation
    {
        bool isInteger = false;
        Register register;
        int _address;

        public override byte[] compile(IntPtr address)
        {
            if (isInteger)
            {
                //pop [0x100000]
                byte[] code = new byte[6];
                code[0] = 0x8F;
                code[1] = 0x05;
                Buffer.BlockCopy(BitConverter.GetBytes(_address), 0, code, 2, 4);
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
                            //pop [eax+10]
                            return new byte[] { 0x8f, (byte)(0x40 + register.register), (byte)register.appliedOffset };
                        }
                        else
                        {
                            //pop [eax+1024]
                            byte[] code = new byte[6];
                            code[0] = 0x8F;
                            code[1] = (byte)(0x80 + register.register);
                            Buffer.BlockCopy(BitConverter.GetBytes(register.appliedOffset), 0, code, 2, 4);
                            return code;
                        }

                    }
                    else
                    {
                        //pop [eax]
                        return new byte[] { 0x8F, (byte)register.register };
                    }
                }
                else
                {
                    if (register.usesOffset)
                        throw new ArithmeticException("Invalid ASM. pop can only pop to a pointer.");
                    //pop eax
                    return new byte[] { (byte)(0x58 + register.register) };
                }
            }
        }

        public pop(Register Register)
        {
            register = Register;
        }

        public pop(int Address)
        {
            _address = Address;
            isInteger = true;
        }
    }
}
