using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMdotNET.x64
{
    public enum RegisterName
    {
        rax,
        rcx,
        rdx,
        rbx,
        rsp,
        rbp,
        rsi,
        rdi,
        r8,
        r9,
        r10,
        r11,
        r12,
        r13,
        r14,
        r15,
        eax,
        ecx,
        edx,
        ebx,
        esp,
        ebp,
        esi,
        edi
    }

    public enum RegisterType
    {
        Full,
        Numbered,
        Lower4
    }

    public class Register
    {
        public RegisterName register;
        public RegisterType type;
        public bool pointer = false;
        public int appliedOffset = 0;
        public bool usesOffset;

        public Register(RegisterName _register, RegisterType _type)
        {
            register = _register;
            type = _type;
        }

        public static Register operator +(Register register, int Offset)
        {
            Register Reg = new Register(register.register, register.type);
            Reg.pointer = register.pointer;
            Reg.appliedOffset = Offset;
            Reg.usesOffset = true;
            return Reg;
        }

        public static Register operator -(Register register, int Offset)
        {
            Register Reg = new Register(register.register, register.type);
            Reg.pointer = register.pointer;
            Reg.appliedOffset = -Offset;
            Reg.usesOffset = true;
            return Reg;
        }

        public static Register operator ~(Register register)
        {
            Register Reg = new Register(register.register, register.type);
            Reg.appliedOffset = register.appliedOffset;
            Reg.usesOffset = register.usesOffset;
            Reg.pointer = true;
            return Reg;
        }
    }

    public static class Registers
    {
        public static Register rax = new Register(RegisterName.rax, RegisterType.Full);
        public static Register rcx = new Register(RegisterName.rcx, RegisterType.Full);
        public static Register rdx = new Register(RegisterName.rdx, RegisterType.Full);
        public static Register rbx = new Register(RegisterName.rbx, RegisterType.Full);
        public static Register rsp = new Register(RegisterName.rsp, RegisterType.Full);
        public static Register rbp = new Register(RegisterName.rbp, RegisterType.Full);
        public static Register rsi = new Register(RegisterName.rsi, RegisterType.Full);
        public static Register rdi = new Register(RegisterName.rdi, RegisterType.Full);

        public static Register r8 = new Register(RegisterName.r8, RegisterType.Numbered);
        public static Register r9 = new Register(RegisterName.r9, RegisterType.Numbered);
        public static Register r10 = new Register(RegisterName.r10, RegisterType.Numbered);
        public static Register r11 = new Register(RegisterName.r11, RegisterType.Numbered);
        public static Register r12 = new Register(RegisterName.r12, RegisterType.Numbered);
        public static Register r13 = new Register(RegisterName.r13, RegisterType.Numbered);
        public static Register r14 = new Register(RegisterName.r14, RegisterType.Numbered);
        public static Register r15 = new Register(RegisterName.r15, RegisterType.Numbered);

        public static Register eax = new Register(RegisterName.eax, RegisterType.Lower4);
        public static Register ecx = new Register(RegisterName.ecx, RegisterType.Lower4);
        public static Register edx = new Register(RegisterName.edx, RegisterType.Lower4);
        public static Register ebx = new Register(RegisterName.ebx, RegisterType.Lower4);
        public static Register esp = new Register(RegisterName.esp, RegisterType.Lower4);
        public static Register ebp = new Register(RegisterName.ebp, RegisterType.Lower4);
        public static Register esi = new Register(RegisterName.esi, RegisterType.Lower4);
        public static Register edi = new Register(RegisterName.edi, RegisterType.Lower4);
    }
}
