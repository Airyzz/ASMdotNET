using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMdotNET;

namespace ASMdotNET
{
    public enum RegisterName
    {
        eax,
        ecx,
        edx,
        ebx,
        esp,
        ebp,
        esi,
        edi,
        rax,
        rcx,
        rdx,
        rbx,
        rsp,
        rbp,
        rsi,
        rdi,
        ax,
        cx,
        dx,
        bx,
    }

    public class Register
    {
        public RegisterName register;
        public bool pointer = false;
        public int appliedOffset = 0;
        public int multiplier;
        public bool usesMultiplier;
        public bool usesOffset;

        public Register(RegisterName _register)
        {
            register = _register;
        }

        public static Register operator +(Register register, int Offset)
        {
            Register Reg = new Register(register.register);
            Reg.pointer = register.pointer;
            Reg.appliedOffset = Offset;
            Reg.usesOffset = true;
            Reg.multiplier = register.multiplier;
            Reg.usesMultiplier = register.usesMultiplier;
            return Reg;
        }

        public static Register operator -(Register register, int Offset)
        {
            Register Reg = new Register(register.register);
            Reg.pointer = register.pointer;
            Reg.appliedOffset = -Offset;
            Reg.usesOffset = true;
            Reg.multiplier = register.multiplier;
            Reg.usesMultiplier = register.usesMultiplier;
            return Reg;
        }

        public static Register operator ~(Register register)
        {
            Register Reg = new Register(register.register);
            Reg.appliedOffset = register.appliedOffset;
            Reg.usesOffset = register.usesOffset;
            Reg.pointer = true;
            Reg.multiplier = register.multiplier;
            Reg.usesMultiplier = register.usesMultiplier;
            return Reg;
        }

        public static Register operator *(Register register, int multiplier)
        {
            Register Reg = new Register(register.register);
            Reg.appliedOffset = register.appliedOffset;
            Reg.usesOffset = true;
            Reg.pointer = true;
            Reg.multiplier = multiplier;
            Reg.usesMultiplier = true;
            return Reg;
        }

        public override string ToString()
        {
            return register.ToString();
        }

    }

    public static class Registers
    {
        public static Register eax = new Register(RegisterName.eax);
        public static Register ecx = new Register(RegisterName.ecx);
        public static Register edx = new Register(RegisterName.edx);
        public static Register ebx = new Register(RegisterName.ebx);
        public static Register esp = new Register(RegisterName.esp);
        public static Register ebp = new Register(RegisterName.ebp);
        public static Register esi = new Register(RegisterName.esi);
        public static Register edi = new Register(RegisterName.edi);
        public static Register rax = new Register(RegisterName.rax);
        public static Register rcx = new Register(RegisterName.rcx);
        public static Register rdx = new Register(RegisterName.dx);
        public static Register rbx = new Register(RegisterName.rbx);
        public static Register rsp = new Register(RegisterName.rsp);
        public static Register rbp = new Register(RegisterName.rbp);
        public static Register rsi = new Register(RegisterName.rsi);
        public static Register rdi = new Register(RegisterName.rdi);
        public static Register ax  = new Register(RegisterName.ax);
        public static Register cx  = new Register(RegisterName.cx);
        public static Register dx  = new Register(RegisterName.dx);
        public static Register bx  = new Register(RegisterName.bx); 
    }

}
