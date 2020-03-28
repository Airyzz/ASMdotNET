using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET
{
    public static class Opcodes
    {
        private static Operation assembleString(string name, Register R1, bool canUsePointer = true)
        {
            if (R1.pointer)
            {
                if (canUsePointer)
                {
                    if (R1.usesOffset)
                    {
                        if (R1.usesMultiplier)
                        {
                            if (R1.appliedOffset != 0)
                                return new Operation($"{name} [{R1}*{R1.multiplier}{R1.appliedOffset.ToString("+#;-#;0")}]");
                            return new Operation($"{name} [{R1}*{R1.multiplier}]");
                        }
                        return new Operation($"{name} [{R1}{R1.appliedOffset.ToString("+#;-#;0")}]");
                    }
                    return new Operation($"{name} [{R1}]");
                }
                else{
                    throw new InvalidOperationException($"Cannot use pointer with {name}");
                }
            }
            return new Operation($"{name} {R1}");
        }

        private static Operation assembleString(string name, Register R1, Register R2, bool canUsePointer = true)
        {
            if (R1.pointer)
            {
                if (canUsePointer)
                {
                    if (R1.usesOffset)
                    {
                        if (R1.usesMultiplier)
                        {
                            if(R1.appliedOffset != 0)
                                return new Operation($"{name} [{R1}*{R1.multiplier}{R1.appliedOffset.ToString("+#;-#;0")}],{R2}");
                            return new Operation($"{name} [{R1}*{R1.multiplier}],{R2}");
                        }
                        return new Operation($"{name} [{R1}{R1.appliedOffset.ToString("+#;-#;0")}],{R2}");
                    }
                    return new Operation($"{name} [{R1}],{R2}");
                }
                else
                {
                    throw new InvalidOperationException($"Cannot use pointer with {name}");
                }
            }
            else if (R2.pointer)
            {
                
                if (canUsePointer)
                {

                    if (R2.usesOffset)
                    {

                        if (R2.usesMultiplier)
                        {
                            if(R2.appliedOffset != 0)
                                return new Operation($"{name} {R1},[{R2}*{R2.multiplier}{R2.appliedOffset.ToString("+#;-#;0")}]");
                            return new Operation($"{name} {R1},[{R2}*{R2.multiplier}]");
                        }
                        return new Operation($"{name} {R1},[{R2}{R2.appliedOffset.ToString("+#;-#;0")}]");
                    }
                    return new Operation($"{name} {R1},[{R2}]");
                }
                else
                {
                    throw new InvalidOperationException($"Cannot use pointer with {name}");
                }
            }
            else
            {
                return new Operation($"{name} {R1},{R2}");
            }
        }

        private static Operation assembleString(string name, Register R1, int Value, bool canUsePointer = true)
        {
            if (R1.pointer)
            {
                if (canUsePointer)
                {

                    if (R1.usesOffset)
                    {
                        if (R1.usesMultiplier)
                        {
                            if (R1.appliedOffset != 0)
                                return new Operation($"{name} [{R1}*{R1.multiplier}{R1.appliedOffset.ToString(" +#;-#;0")}],{Value}");
                            return new Operation($"{name} [{R1}*{R1.multiplier}],{Value}");
                        }
                        return new Operation($"{name} [{R1}{R1.appliedOffset.ToString(" +#;-#;0")}],{Value}");
                    }
                    return new Operation($"{name} [{R1}],{Value}");
                }
                else
                {
                    throw new InvalidOperationException($"Cannot use pointer with {name}");
                }
            }
            else
            {
                return new Operation($"{name} {R1},{Value}");
            }
        }

        private static Operation assembleString(string name, int Value)
        {
            return new Operation($"{name} {Value}");
        }

        /// <summary>
        /// No Operation
        /// </summary>
        /// <returns></returns>
        public static Operation nop()
        {
            return new Operation("nop");
        }

        /// <summary>
        /// Move DWORD into register
        /// </summary>
        /// <param name="register"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Operation mov(Register R1, int value)
        {
            return assembleString("mov", R1, value);
        }
       
        /// <summary>
        /// Copy memory
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static Operation mov(Register R1, Register R2)
        {
            return assembleString("mov", R1, R2);
        }

        /// <summary>
        /// Push register onto the stack
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation push(Register register)
        {
            return assembleString("push", register);
        }
       
        /// <summary>
        /// Push DWORD onto the stack
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Operation push(int Value)
        {
            return assembleString("push", Value);
        }

        /// <summary>
        /// Pop a value from the stack to a register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation pop(Register register)
        {
            return assembleString("pop", register, false);
        }

        #region Control Flow
        /// <summary>
        /// Call a function at an address
        /// </summary>
        /// <param name="functionAddress">Address of the function</param>
        /// <returns></returns>
        public static Operation call(int functionAddress)
        {
            return assembleString("call", functionAddress);
        }
        
        /// <summary>
        /// Call a function at an address stored in a register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation call(Register R1)
        {
            return assembleString("call", R1);
        }

        /// <summary>
        /// Call a function at an address stored in a register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation ret()
        {
            return new Operation($"ret");
        }

        /// <summary>
        /// Compare Two Registers
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation cmp(Register R1, Register R2)
        {
            return assembleString("cmp", R1, R2);
        }

        /// <summary>
        /// Compare register to value
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Operation cmp(Register R1, int Value)
        {
            return assembleString("cmp", R1, Value);
        }

        #region Jumps
        /// <summary>
        /// Jump execution to address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jmp(int Address)
        {
            return assembleString("jmp", Address);
        }

        /// <summary>
        /// Jump execution to address stored in register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Operation jmp(Register register)
        {
            return assembleString("jmp", register, false);
        }

        /// <summary>
        /// Jump to address if equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation je(int Address)
        {
            return assembleString("je", Address);
        }

        /// <summary>
        /// Jump to address if not equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jne(int Address)
        {
            return assembleString("jne", Address);
        }

        /// <summary>
        /// Jump to address if greater than
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jg(int Address)
        {
            return assembleString("jg", Address);
        }

        /// <summary>
        /// Jump to address if greater or equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jge(int Address)
        {
            return assembleString("jge", Address);
        }

        /// <summary>
        /// Jump to address if above
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation ja(int Address)
        {
            return assembleString("ja", Address);
        }

        /// <summary>
        /// Jump to address if above or qual
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jae(int Address)
        {
            return assembleString("jae", Address);
        }

        /// <summary>
        /// Jump to address if lesser
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jl(int Address)
        {
            return assembleString("jl", Address);
        }

        /// <summary>
        /// Jump to address if lesser or equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jle(int Address)
        {
            return assembleString("jle", Address);
        }

        /// <summary>
        /// Jump to address if below
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jb(int Address)
        {
            return assembleString("jb", Address);
        }

        /// <summary>
        /// Jump to address if below or equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jbe(int Address)
        {
            return assembleString("jbe", Address);
        }

        /// <summary>
        /// Jump to address if overflow
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jo(int Address)
        {
            return assembleString("jo", Address);
        }

        /// <summary>
        /// Jump to address if not overflow
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jno(int Address)
        {
            return assembleString("jno", Address);
        }

        /// <summary>
        /// Jump to address if zero
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jz(int Address)
        {
            return assembleString("jz", Address);
        }

        /// <summary>
        /// Jump to address if not zero
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jnz(int Address)
        {
            return assembleString("jnz", Address);
        }

        /// <summary>
        /// Jump to address if signed
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation js(int Address)
        {
            return assembleString("js", Address);
        }

        /// <summary>
        /// Jump to address if not signed
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jns(int Address)
        {
            return assembleString("jns", Address);
        }

        /// <summary>
        /// Jump to label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Operation jmp(string label)
        {
            return new Operation($"jmp {label}");
        }

        /// <summary>
        /// jump to label if equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation je(string label)
        {
            return new Operation($"je {label}");
        }

        /// <summary>
        /// jump to label if not equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jne(string label)
        {
            return new Operation($"jne {label}");
        }

        /// <summary>
        /// jump to label if greater than
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jg(string label)
        {
            return new Operation($"jg {label}");
        }

        /// <summary>
        /// jump to label if greater or equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jge(string label)
        {
            return new Operation($"jge {label}");
        }

        /// <summary>
        /// jump to label if above
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation ja(string label)
        {
            return new Operation($"ja {label}");
        }

        /// <summary>
        /// jump to label if above or qual
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jae(string label)
        {
            return new Operation($"jae {label}");
        }

        /// <summary>
        /// jump to label if lesser
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jl(string label)
        {
            return new Operation($"jl {label}");
        }

        /// <summary>
        /// jump to label if lesser or equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jle(string label)
        {
            return new Operation($"jle {label}");
        }

        /// <summary>
        /// jump to label if below
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jb(string label)
        {
            return new Operation($"jb {label}");
        }

        /// <summary>
        /// jump to label if below or equal
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jbe(string label)
        {
            return new Operation($"jbe {label}");
        }

        /// <summary>
        /// jump to label if overflow
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jo(string label)
        {
            return new Operation($"jo {label}");
        }

        /// <summary>
        /// jump to label if not overflow
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jno(string label)
        {
            return new Operation($"jno {label}");
        }

        /// <summary>
        /// jump to label if zero
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jz(string label)
        {
            return new Operation($"jz {label}");
        }

        /// <summary>
        /// jump to label if not zero
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jnz(string label)
        {
            return new Operation($"jnz {label}");
        }

        /// <summary>
        /// jump to label if signed
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation js(string label)
        {
            return new Operation($"js {label}");
        }

        /// <summary>
        /// jump to label if not signed
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public static Operation jns(string label)
        {
            return new Operation($"jns {label}");
        }

        #endregion
        #endregion

        #region Arithmetic

        /// <summary>
        /// Subtract R2 from R1
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation sub(Register R1, Register R2)
        {
            return assembleString("sub", R1, R2);
        }

        /// <summary>
        /// Subtract value from r1
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Operation sub(Register R1, int Value)
        {
            return assembleString("sub", R1, Value);
        }

        /// <summary>
        /// Add R2 to R1
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation add(Register R1, Register R2)
        {
            return assembleString("add", R1, R2);
        }

        /// <summary>
        /// Add value to R1
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Operation add(Register R1, int Value)
        {
            return assembleString("add", R1, Value);
        }

        /// <summary>
        /// Signed Multiply R1 by R2. Result is stored in R1
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation imul(Register R1, Register R2)
        {
            return assembleString("imul", R1, R2, false);
        }

        /// <summary>
        /// Multiply R1 by value in the AX register
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation mul(Register R1)
        {
            return assembleString("mul", R1, false);
        }

        /// <summary>
        /// Divide R1 by value in the dividend register
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation div(Register R1)
        {
            return assembleString("div", R1, false);
        }

        /// <summary>
        /// Signed Divide R1 by value in the dividend register
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation idiv(Register R1)
        {
            return assembleString("idiv", R1, false);
        }

        /// <summary>
        /// Increment register by 1
        /// </summary>
        /// <param name="R1"></param>
        /// <returns></returns>
        public static Operation inc(Register R1)
        {
            return assembleString("inc", R1, false);
        }

        /// <summary>
        /// Decrement register by 1
        /// </summary>
        /// <param name="R1"></param>
        /// <returns></returns>
        public static Operation dec(Register R1)
        {
            return assembleString("dec", R1, false);
        }

        /// <summary>
        /// Load effective address
        /// </summary>
        /// <param name="R1"></param>
        /// <param name="R2"></param>
        /// <returns></returns>
        public static Operation lea(Register R1, Register R2)
        {
            return assembleString("lea", R1, R2);
        }

        public static Operation xor(Register R1, Register R2)
        {
            return assembleString("xor", R1, R2);
        }

        public static Operation xor(Register R1, int Value)
        {
            return assembleString("xor", R1, Value);
        }

        #endregion
    }
}
