using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMdotNET.x64.Operations;

namespace ASMdotNET.x64
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
        /// Copy memory
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation mov(Register R1, Register R2)
        {
            return new mov(R1, R2);
        }

        /// <summary>
        /// Set value of register
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Operation mov(Register R1, int Value)
        {
            return new mov(R1, Value);
        }

        /// <summary>
        /// Set value of register
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Operation mov(Register R1, long Value)
        {
            return new mov(R1, Value);
        }
        
        /// <summary>
        /// Store register in address
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="R1"></param>
        /// <returns></returns>
        public static Operation mov(long Address, Register R1)
        {
            return new mov(Address, R1);
        }
    }
}
