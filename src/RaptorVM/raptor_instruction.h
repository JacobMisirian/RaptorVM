#ifndef raptor_instruction_h
#define raptor_instruction_h

#include <stdint.h>

struct raptor_instruction {
	uint8_t opcode;
	uint8_t operandOne : 4;
	uint8_t operandTwo : 4;
	uint16_t immediate;
};

#endif