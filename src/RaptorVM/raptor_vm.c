#include <stdlib.h>
#include <stdio.h>
#include "opcodes.h"
#include "raptor_context.h"
#include "raptor_instruction.h"
#include "raptor_vm.h"

#define ZERO_FLAG 0x01
#define SIGN_FLAG 0x02
#define OVERFLOW_FLAG 0x03

#define IP context->registers[15]
#define FLAGS context->registers[14]

// Local method declarations.
static void read_file_to_ram(struct raptor_context *context, char *file_path);
static void decode_instruction(struct raptor_context *context, struct raptor_instruction *instruction);
static uint16_t set_flags(struct raptor_context *context, uint16_t value);

// Constructor.
void init_raptor_vm(struct raptor_context *context, size_t size) {
	// Initialize RAM with some blank values.
	context->ram = (char*)malloc(size);
	context->ramSize = &size;
}
// Deconstructor.
void destroy_raptor_vm(struct raptor_context *context) {
	// Free the RAM.
	free(context->ram);
}
// The main method that takes in the context and path.
void run_raptor_vm(struct raptor_context *context, char *file_path) {
	// Put the file in context->ram.
	read_file_to_ram(context, file_path);
	// Set the IP to 0.
	context->registers[15] = 0;
	
	struct raptor_instruction *instruction = (struct raptor_instruction*)(&context->ram[context->registers[15]]);
	// While the program still exists.
	while (instruction->opcode != 0) {
		// Increase the value of the IP by 4 (length of an instruction).
		context->registers[15] += 4;
		// Actually execute the instruction.
		decode_instruction(context, instruction);
		// Read from the virtual RAM into an instruction struct.
		instruction = (struct raptor_instruction*)(&context->ram[context->registers[15]]);
	}
}
// Reads the file into the virtual RAM at context->ram.
static void read_file_to_ram(struct raptor_context *context, char *file_path) {
	FILE *file;
	long filelen;
	file = fopen(file_path, "rb");
	fseek(file, 0, SEEK_END);
	filelen = ftell(file);
	rewind(file);
	fread(context->ram, filelen, 1, file);
	fclose(file);
}

// Interprets an instruction.
static void decode_instruction(struct raptor_context *context, struct raptor_instruction *instruction) {
	//printf("OperandOne:%d\tOperandTwo:%d\tImmediate:%d\n", instruction->operandOne, instruction->operandTwo, instruction->immediate);
	switch (instruction->opcode) {
		case OP_ADD:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] + context->registers[instruction->operandTwo]);
			break;
		case OP_SUB:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] - context->registers[instruction->operandTwo]);
			break;
		case OP_MUL:
			context->registers[instruction->operandOne] *= context->registers[instruction->operandTwo];
			break;
		case OP_DIV:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] / context->registers[instruction->operandTwo]);
			break;
		case OP_MOD:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] % context->registers[instruction->operandTwo]);
			break;
		case OP_MOV:
			context->registers[instruction->operandOne] = context->registers[instruction->operandTwo];
			break;
		case OP_LOAD_IMMEDIATE:
			context->registers[instruction->operandOne] = instruction->immediate;
			break;
		case OP_PRINT:
			printf("%d", context->registers[instruction->operandOne]);
			break;
		case OP_PRINT_CHAR:
			printf("%c", context->registers[instruction->operandOne]);
			break;
		case OP_JMP:
			// Set the IP to the immediate (label).
			IP = instruction->immediate;
			break;
		case OP_SHIFT_LEFT:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] << context->registers[instruction->operandTwo]);
			break;
		case OP_SHIFT_RIGHT:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] >> context->registers[instruction->operandTwo]);
			break;
		case OP_AND:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] & context->registers[instruction->operandTwo]);
			break;
		case OP_OR:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] | context->registers[instruction->operandTwo]);
			break;
		case OP_XOR:
			context->registers[instruction->operandOne] = set_flags(context, context->registers[instruction->operandOne] ^ context->registers[instruction->operandTwo]);
			break;
		case OP_NOT:
			context->registers[instruction->operandOne] = set_flags(context, ~context->registers[instruction->operandOne]);
			break;
		case OP_CMP:
			set_flags(context, context->registers[instruction->operandOne] - context->registers[instruction->operandTwo]);
			break;
		case OP_JE:
			if (FLAGS & ZERO_FLAG)
				IP = instruction->immediate;
			break;
		case OP_JNE:
			if (!(FLAGS & ZERO_FLAG))
				IP = instruction->immediate;
			break;
		case OP_JL:
			if (FLAGS & SIGN_FLAG)
				IP = instruction->immediate;
			break;
		case OP_JLE:
			if ((FLAGS & SIGN_FLAG) || (FLAGS & ZERO_FLAG))
				IP = instruction->immediate;
			break;
		case OP_LOAD_BYTE:
			context->registers[instruction->operandOne] = context->ram[context->registers[instruction->operandTwo]];
			break;
		case OP_LOAD_WORD:
			context->registers[instruction->operandOne] = *((uint16_t*)(&context->ram[context->registers[instruction->operandTwo]]));
			break;
		case OP_STORE_BYTE:
			context->ram[context->registers[instruction->operandOne]] = context->registers[instruction->operandTwo];
			break;
		case OP_STORE_WORD:
			*((uint16_t*)(&context->ram[context->registers[instruction->operandOne]])) = context->registers[instruction->operandTwo];
			break;
		case OP_INC:
			context->registers[instruction->operandOne]++;
			break;
		case OP_DEC:
			context->registers[instruction->operandOne]--;
			break;
	}
}

static uint16_t set_flags(struct raptor_context *context, uint16_t value) {
	if (value == 0)
		FLAGS |= ZERO_FLAG;
	else
		FLAGS &= ~ZERO_FLAG;
		
	if (value < 0)
		FLAGS |= SIGN_FLAG;
	else if (value > 0)
		FLAGS &= ~SIGN_FLAG;
	return value;
}