#include <stdlib.h>
#include <stdio.h>
#include "opcodes.h"
#include "raptor_context.h"
#include "raptor_instruction.h"
#include "raptor_vm.h"

// Local method declarations.
static void read_file_to_ram(struct raptor_context *context, char *file_path);
static void decode_instruction(struct raptor_context *context, struct raptor_instruction *instruction);

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
	struct raptor_instruction *instruction;
	// Set opcode to a default of 1 so that it doesn't default to 0 and not execute.
	instruction->opcode = 1;
	// While the program still exists.
	while (instruction->opcode != 0) {
		// Read from the virtual RAM into an instruction struct.
		instruction = (struct raptor_instruction*)(&context->ram[context->registers[15]]);
		// Increase the value of the IP by 4 (length of an instruction).
		context->registers[15] += 4;
		// Actually execute the instruction.
		decode_instruction(context, instruction);
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
			context->registers[instruction->operandOne] += context->registers[instruction->operandTwo];
			break;
		case OP_SUB:
			context->registers[instruction->operandOne] -= context->registers[instruction->operandTwo];
			break;
		case OP_MUL:
			context->registers[instruction->operandOne] *= context->registers[instruction->operandTwo];
			break;
		case OP_DIV:
			context->registers[instruction->operandOne] /= context->registers[instruction->operandTwo];
			break;
		case OP_MOD:
			context->registers[instruction->operandOne] %= context->registers[instruction->operandTwo];
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
		case OP_JMP:
			// Set the IP to the immediate (label).
			context->registers[15] = instruction->immediate;
			break;
		case OP_SHIFT_LEFT:
			context->registers[instruction->operandOne] <<= context->registers[instruction->operandTwo];
			break;
		case OP_SHIFT_RIGHT:
			context->registers[instruction->operandOne] >>= context->registers[instruction->operandTwo];
			break;
		case OP_AND:
			context->registers[instruction->operandOne] &= context->registers[instruction->operandTwo];
			break;
		case OP_OR:
			context->registers[instruction->operandOne] |= context->registers[instruction->operandTwo];
			break;
		case OP_XOR:
			context->registers[instruction->operandOne] ^= context->registers[instruction->operandTwo];
			break;
	}
}