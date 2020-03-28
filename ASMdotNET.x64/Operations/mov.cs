using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x64.Operations
{
    public class mov : Operation
    {
        private Register r1, r2 = null;
        private int Value;
        private long LongValue;
        public bool useDword;
        public bool valueIsLong;
        public bool movToPointer;

        public override byte[] compile(IntPtr address)
        {
            //mov eax,1024
            if (useDword)
            {
                //mov rax,0x20AEDBD0000
                if (valueIsLong)
                {
                    return new byte[] { };
                }
                //mov eax,1024
                else
                {
                    return new byte[] { };
                }
            }
            //mov 0x20AEDBD0000,eax
            if (movToPointer)
            {
                if(r1.type == RegisterType.Full)
                {
                    return new byte[] { };
                }
                if(r1.type == RegisterType.Numbered)
                {
                    return new byte[] { };
                }
                if(r1.type == RegisterType.Lower4)
                {
                    return new byte[] { };
                }
                throw new Exception("Error: Invalid Register Type");
            }
            //mov eax,ecx
            if (r1 != null && r2 != null)
            {
                return new byte[] { };
            }

            return new byte[] { };
        }

        public mov(Register register, int value)
        {
            useDword = true;
            r1 = register;
            r2 = null;
            Value = value;
            valueIsLong = false;
        }

        public mov(Register register, long value)
        {
            useDword = true;
            r1 = register;
            r2 = null;
            LongValue = value;
            valueIsLong = true;
        }


        public mov(Register R1, Register R2)
        {
            r1 = R1;
            r2 = R2;
            useDword = false;
        }

        public mov(long address, Register R1)
        {
            movToPointer = true;
            useDword = false;
            r1 = R1;
            Value = address;
        }
    }
}
