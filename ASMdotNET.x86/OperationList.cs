using ASMdotNET.x86.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x86
{
    public static class Opcodes
    {
        /// <summary>
        /// No Operation
        /// </summary>
        /// <returns></returns>
        public static Operation nop()
        {
            return new nop();
        }

        /// <summary>
        /// Move DWORD into register
        /// </summary>
        /// <param name="register"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Operation mov(Register register, int value)
        {
            return new mov(register, value);
        }

        /// <summary>
        /// Copy memory
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static Operation mov(Register r1, Register r2)
        {
            return new mov(r1, r2);
        }



        /// <summary>
        /// Push register onto the stack
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation push(Register register)
        {
            return new push(register);
        }

        /// <summary>
        /// Push DWORD onto the stack
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Operation push(int Value)
        {
            return new push(Value);
        }


        /// <summary>
        /// Call a function at an address
        /// </summary>
        /// <param name="functionAddress">Address of the function</param>
        /// <returns></returns>
        public static Operation call(int functionAddress)
        {
            return new call(functionAddress);
        }
        
        /// <summary>
        /// Call a function at an address stored in a register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation call(Register register)
        {
            return new call(register);
        }

        /// <summary>
        /// Jump execution to address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jmp(int Address)
        {
            return new jmp(Address);
        }

        /// <summary>
        /// Jump execution to address stored in register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation jmp(Register register)
        {
            return new jmp(register);
        }

        /// <summary>
        /// Pop a value from the stack to a register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation pop(Register register)
        {
            return new pop(register);
        }

        /// <summary>
        /// Pop a value from the stack to an address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation pop(int Address)
        {
            return new pop(Address);
        }

        public static Operation sub(Register R1, Register R2)
        {
            return new sub(R1, R2);
        }

        public static Operation sub(Register R1, int Value)
        {
            return new sub(R1, Value);
        }

        public static Operation add(Register R1, Register R2)
        {
            return new add(R1, R2);
        }

        public static Operation add(Register R1, int Value)
        {
            return new add(R1, Value);
        }

        public static Operation ret()
        {
            return new ret();
        }


    }
}
