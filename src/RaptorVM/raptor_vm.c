#include <stdlib.h>
#include <stdio.h>
#include "opcodes.h"
#include "raptor_context.h"
#include "raptor_instruction.h"
#include "raptor_vm.h"

static void read_file_to_ram(struct raptor_context *context, char *file_path);
static void decode_instruction(struct raptor_context *context, struct raptor_instruction *instruction);

void init_raptor_vm(struct raptor_context *context) {
	// Initialize RAM with some blank values.
	context->ram = (char*)malloc(0xFFFF);
}

void destroy_raptor_vm(struct raptor_context *context) {
	// Free the RAM.
	free(context->ram);
}

void run_raptor_vm(struct raptor_context *context, char *file_path) {
	read_file_to_ram(context, file_path);
	
	context->registers[15] = 0x0000;
	struct raptor_instruction *instruction;
	while (instruction->opcode != 0) {
		instruction = (struct raptor_instruction*)(&context->ram[context->registers[15]]);
		context->registers[15] += 4;
		decode_instruction(context, instruction);
	}
}

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
			context->registers[15] = instruction->immediate;
			break;
	}
}