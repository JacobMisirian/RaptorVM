#include <stdlib.h>
#include <stdio.h>
#include "raptor_vm.h"
#include "raptor_context.h"
#include "raptor_instruction.h"

static void read_file_to_ram(struct raptor_context *context, char *file_path);

void init_raptor_vm(struct raptor_context *context) {
	// Initialize RAM with some blank values.
	context->ram = (char*)malloc(0xFFFF);
}

void destroy_raptor_vm(struct raptor_context *context) {
	// Free the RAM.
	free(context->ram);
}

void run_raptor_vm(struct raptor_context *context, char *file_path) {
	printf("%s\n", file_path);
	read_file_to_ram(context, file_path);
	
	context->registers[15] = 0x0000;
	struct raptor_instruction *instruction;
	while (instruction->opcode != 0) {
		instruction = (struct raptor_instruction*)(&context->ram[context->registers[15]]);
		context->registers[15] += 4;
		printf("Opcode %d ", instruction->opcode);
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
