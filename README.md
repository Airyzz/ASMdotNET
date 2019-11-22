# ASMdotNET
A class to compile assembly using a C++ like inline assembly syntax

## Getting Started
Add a reference to ASMdotNET to your project and add the following using statements:
```csharp
using ASMdotNET.x86;
using static ASMdotNET.x86.Opcodes;
using static ASMdotNET.x86.Registers;
```

Initialise the class with the address the code will start at:
```csharp
AssemblyCompiler asm = new AssemblyCompiler(0x04410010);
```

## Compiling
Writing your code is very similar to C++ inline assembly, or the auto assembler in Cheat Engine, with a few minor differences due to C# Syntax limitations


1. All operations are written following standard C# function syntax, and most operations have proper xml documentation comments
```
Assembly:
    mov eax,1024
ASMdotNET:
    mod(eax, 1024)
```

2. Accessing pointers stored in registers is dont using the Bitwise Complement operator (~)
```
Assembly:
    mov [eax],ecx
ASMdotNet:
    mov(~eax,ecx)
```

3. Operations are added to the compile list using 'params' and as a result, every statement must end with a comma, except for the final statement
```
Assembly:
    push ebp
    mov ebp, esp
    sub esp 08
    call 0x1FF01F
    mov [ebp],eax
    add esp,08
    mov esp, ebp
    pop ebp
    ret
    
ASMdotNET:
asm.Compile(
    push(ebp)
    mov(ebp, esp)
    sub(esp 08)
    call(0x1FF01F)
    mov(~ebp,eax)
    add(esp,08)
    mov(esp, ebp)
    pop(ebp)
    ret()
    );
```
    

