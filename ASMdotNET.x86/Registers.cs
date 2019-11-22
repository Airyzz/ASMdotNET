using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMdotNET.x86;

namespace ASMdotNET.x86
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
    }

    public class Register
    {
        public RegisterName register;
        public bool pointer = false;
        public int appliedOffset = 0;
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
            return Reg;
        }

        public static Register operator -(Register register, int Offset)
        {
            Register Reg = new Register(register.register);
            Reg.pointer = register.pointer;
            Reg.appliedOffset = -Offset;
            Reg.usesOffset = true;
            return Reg;
        }

        public static Register operator ~(Register register)
        {
            Register Reg = new Register(register.register);
            Reg.appliedOffset = register.appliedOffset;
            Reg.usesOffset = register.usesOffset;
            Reg.pointer = true;
            return Reg;
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
    }

}
