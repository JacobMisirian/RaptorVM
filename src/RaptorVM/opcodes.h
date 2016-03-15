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
#define OP_PUSH 0x1E
#define OP_POP 0x1F
#define OP_CALL 0x20
#define OP_RET 0x21
#define OP_ADD_IMMEDIATE 0x22
#define OP_SUB_IMMEDIATE 0x23
#define OP_MUL_IMMEDIATE 0x24
#define OP_DIV_IMMEDIATE 0x25
#define OP_MOD_IMMEDIATE 0x26
#define OP_MOV_IMMEDIATE 0x27
#define OP_PRINT_IMMEDIATE 0x28
#define OP_PRINT_CHAR_IMMEDIATE 0x29
#define OP_SHIFT_LEFT_IMMEDIATE 0x2A
#define OP_SHIFT_RIGHT_IMMEDIATE 0x2B
#define OP_AND_IMMEDIATE 0x2C
#define OP_OR_IMMEDIATE 0x2D
#define OP_XOR_IMMEDIATE 0x2E
#define OP_CMP_IMMEDIATE 0x2F
#define OP_PUSH_IMMEDIATE 0x30

#endif