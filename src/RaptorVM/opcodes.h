#ifndef opcodes_h
#define opcodes_h

// The opcodes for instructions.
#define OP_ADD 0x01
#define OP_SUB 0x02
#define OP_MUL 0x03
#define OP_DIV 0x04
#define OP_MOD 0x05
#define OP_MOV 0x06
#define OP_LOAD_IMMEDIATE 0x07
#define OP_PRINT 0x08
#define OP_JMP 0x09
#define OP_SHIFT_LEFT 0x0A
#define OP_SHIFT_RIGHT 0x0B
#define OP_AND 0x0C
#define OP_OR 0x0D
#define OP_XOR 0x0E
#define OP_NOT 0x0F
#define OP_CMP 0x10
#define OP_JE 0x11
#define OP_JNE 0x12
#define OP_JG 0x13
#define OP_JGE 0x14
#define OP_JL 0x15
#define OP_JLE 0x16
#define OP_PRINT_CHAR 0x17
#define OP_LOAD_BYTE 0x18
#define OP_LOAD_WORD 0x19
#define OP_STORE_BYTE 0x1A
#define OP_STORE_WORD 0x1B
#define OP_INC 0x1C
#define OP_DEC 0x1D

#endif