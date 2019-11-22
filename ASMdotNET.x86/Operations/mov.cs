using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86.Operations
{
    public class mov : Operation
    {
        private Register r1, r2 = null;
        private int Value;
        public bool useDword;
        public bool movToPointer;

        public override byte[] compile(IntPtr address)
        {
            if (movToPointer)
            {
                byte[] operation = new byte[6];
                operation[0] = 0x89;
                operation[1] = (byte)(0x05 + (0x8 * (byte)r1.register));
                Buffer.BlockCopy(BitConverter.GetBytes(Value), 0, operation, 2, 4);
                return operation;
            }
            if (useDword)
            {
                byte[] operation = new byte[5];
                operation[0] = (byte)(OpcodeBytes.movdw + r1.register);
                Buffer.BlockCopy(BitConverter.GetBytes(Value), 0, operation, 1, 4);
                return operation;
            }
            else
            {
                if (r1.pointer == true)
                {
                    if (r1.usesOffset)
                    {
                        //If offset fits into one byte
                        if (r1.appliedOffset <= byte.MaxValue && r1.appliedOffset >= byte.MinValue)
                        {
                            //mov [eax+10],eax
                            byte[] operation = new byte[3];
                            operation[0] = 0x89;
                            byte registercode = (byte)(r1.register + 0x40 + 0x8 * (byte)r2.register);
                            operation[1] = registercode;
                            operation[2] = (byte)r1.appliedOffset;
                            return operation;
                        }
                        else
                        {
                            //mov [eax+1024],eax
                            byte[] operation = new byte[6];
                            operation[0] = 0x89;
                            byte registercode = (byte)(r1.register + 0x80 + 0x8 * (byte)r2.register);
                            operation[1] = registercode;
                            Buffer.BlockCopy(BitConverter.GetBytes(r1.appliedOffset), 0, operation, 2, 4);
                            return operation;
                        }
                    }
                    else
                    {
                        //mov [eax],eax
                        byte[] operation = new byte[2];
                        operation[0] = 0x89;
                        byte registercode = (byte)(r1.register + 0x8 * (byte)r2.register);
                        operation[1] = registercode;
                        return operation;
                    }
                }
                else if (r2.pointer == true)
                {
                    if (r2.usesOffset)
                    {
                        //If offset fits into one byte
                        if (r2.appliedOffset <= byte.MaxValue && r2.appliedOffset >= byte.MinValue)
                        {
                            //mov eax,[eax+10]
                            byte[] operation = new byte[3];
                            operation[0] = 0x8b;
                            byte registercode = (byte)(r2.register + 0x40 + 0x8 * (byte)r1.register);
                            operation[1] = registercode;
                            operation[2] = (byte)r2.appliedOffset;
                            return operation;
                        }
                        else
                        {
                            //mov eax,[eax+1024]
                            byte[] operation = new byte[6];
                            operation[0] = 0x8b;
                            byte registercode = (byte)(r2.register + 0x80 + 0x8 * (byte)r1.register);
                            operation[1] = registercode;
                            Buffer.BlockCopy(BitConverter.GetBytes(r2.appliedOffset), 0, operation, 2, 4);
                            return operation;
                        }
                    }
                    else
                    {
                        //mov eax,[eax]
                        byte[] operation = new byte[2];
                        operation[0] = 0x8b;
                        byte registercode = (byte)(r2.register + 0x8 * (byte)r1.register);
                        operation[1] = registercode;
                        return operation;
                    }
                }
                else
                {
                    //mov eax,eax
                    byte[] operation = new byte[2];
                    operation[0] = 0x8b;
                    byte registercode = (byte)(0xC0 + r2.register + 0x8 * (byte)r1.register);
                    operation[1] = registercode;
                    return operation;
                }
            }
        }

        public mov(Register register, int value)
        {
            useDword = true;
            r1 = register;
            r2 = null;
            Value = value;
        }

        public mov(Register R1, Register R2)
        {
            r1 = R1;
            r2 = R2;
            useDword = false;
        }

        public mov(int address, Register R1)
        {
            movToPointer = true;
            useDword = false;
            r1 = R1;
            Value = address;
        }
    }
}
