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
Add           |   Adds two registers together and stores result in first register.     |   Add <reg1>, <reg2>          |
Sub           |   Subtracts two registers and stores result in first register.         |   Sub <reg1>, <reg2>          |
Mul           |   Multriplies two registers and stores result in first register.       |   Mul <reg1>, <reg2>          |
Div           |   Divides two registers and stores result in first register.           |   Div <reg1>, <reg2>          |
Mod           |   Modulus two registers and stores result in first register.           |   Mod <reg1>, <reg2>          |
Load_Immediate|   Loads an immediate (constant) value into the register,               |   Load_Immediate <reg1>, <const> |
Load_Byte     |   Loads a byte from mem address in second register into first register.|   Load_Byte <reg1>, <reg2>    |
Load_Word     |   Load_Byte except with two bytes (one word).                          |   Load_Word <reg1>, <reg2>    |
Store_Byte    |   Writes a byte from the second register to the mem address in the first register  |  Store_Byte <reg1>, <reg2> |
Store_Word    |   Store_Byte except with two bytes (one word).                         |   Store_Word <reg1>, <reg2>   |
