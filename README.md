# RaptorVM
VM in C and C#

## Instruction Set
The format for a Raptor Assembly (RASM) instruction is as follows:
```
<opcode> <operandOne:operandTwo> <immediate>
```

The opcode is one byte, the operands are one nibble each, and the immediate is two bytes
for a total of four bytes per instruction.

Not all instructions use operands or the immediate, in which case the space they occupy
will be written with zeros. For instance, the Add instruction uses no immediate, so the
bytecode for Add a, b will look like:
```
00000001 0010 0010 0000000000000000
```

The first byte is opcode 1, the next two nibbles are the registers 1 and 2 (represented
by a and b), the final 16 bits are the zeros.

Instruction   |   Description                                                          |   Syntax                      |
-----------   |   ------------------------------------------------------------------   |   -------------------------   |
Add           |   Adds two registers together and stores result in first register.     |   ```Add <reg1>, <reg2>```          |
Sub           |   Subtracts two registers and stores result in first register.         |   ```Sub <reg1>, <reg2>```          |
Mul           |   Multriplies two registers and stores result in first register.       |   ```Mul <reg1>, <reg2>```          |
Div           |   Divides two registers and stores result in first register.           |   ```Div <reg1>, <reg2>```          |
Mod           |   Modulus two registers and stores result in first register.           |   ```Mod <reg1>, <reg2>```          |
Shift_Left    |   Shifts first register left by second register and stores result in first. |   ```Shift_Left <reg1>, <reg2>```   |
Shift_Right   |   Shifts first register right by second register and stores result in first. |  ```Shift_Right <reg1>, <reg2>```  |
And           |   Preforms and (&) on first and second registers and stores result in first. |   ```And <reg1>, <reg2>```    |
Or            |   Preforms or (```|```) on first and second registers and stores result in first.  |   ```Or <reg1>, <reg2>```     |
Xor           |   Preforms Xor (^) on first and second registers and stores result in first. |   ```Xor <reg1>, <reg2>```    |
Not           |   Preforms Not (~) on first and second registers and stores result in first. |   ```Not <reg1>, <reg2>```    |
Inc           |   Increments the number inside the register.                           |   ```Inc <reg>```                   |
Dec           |   Decrements the number inside the register.                           |   ```Dec <reg>```                   | 
Load_Immediate|   Loads an immediate (constant) value into the register,               |   ```Load_Immediate <reg1>, <const>``` |
Load_Byte     |   Loads a byte from mem address in second register into first register.|   ```Load_Byte <reg1>, <reg2>```    |
Load_Word     |   Load_Byte except with two bytes (one word).                          |   ```Load_Word <reg1>, <reg2>```    |
Store_Byte    |   Writes a byte from the second register to the mem address in the first register  |  ```Store_Byte <reg1>, <reg2>``` |
Store_Word    |   Store_Byte except with two bytes (one word).                         |   ```Store_Word <reg1>, <reg2>```   |
Print         |   Prints the number in register onto the terminal.                     |   ```Print <reg>```
Print_Char    |   Prints the number in register onto the terminal as an ASCII char.    |   ```Print_Char <reg>```            |
Push          |   Pushes the value in register to the top of Stack Pointer.            |   ```Push <reg>```                  |
Pop           |   Pops the value off of Stack Pointer and stores in register.          |   ```Pop <reg>```                   |
Jmp           |   Jumps by setting the Instruction Pointer to label in immediate.      |   ```Jmp <lbl>```                   |
Cmp           |   Preforms a comparison between two registers, setting value in FLAGS. |   ```Cmp <reg1>, <reg2>```          |
Je            |   Jumps if FLAGS is zero (jump if equal) to label in immediate.        |   ```Je <lbl>```                    |
Jne           |   Jumps if FLAGS is not zero (jump if not equal) to label in immediate.|   ```Jne <lbl>```                   |
Jl            |   Jumps if sign FLAG is on (jump if lesser) to label in immediate. |  ```Jl <lbl>```             |
Jle           |   Jumps if FLAGS is zero or sign FLAG is on (jump if lesser or equal) to label in immediate. |  ```Jle <lbl>``` |
Jg            |   Jumps if sign FLAG is off (jump is greater) to label in immediate. |  ```Jg <lbl>``` |
Jge           |   Jumps if FLAGS is zero or sign FLAG is off (jump if greater or equal) to label in immediate. |  ```Jge <lbl>``` |
Call          |   Preforms a function call to label in immediate and pushes return addr to stack. |  ```Call <lbl>``` |
Ret           |   In a function, returns back to the caller by setting Instruction Pointer to top of stack. | ```Ret``` |
