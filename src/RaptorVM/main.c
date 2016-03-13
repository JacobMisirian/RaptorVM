#include <stdio.h>
#include <sys/stat.h>
#include "raptor_context.h"
#include "raptor_vm.h"

#define RAMSIZE 0xFFFF

int main (int argc, char *argv[]) {
	if (argc <= 1)
		printf("Error: No input file specified!\n");
	else if (!file_exists(argv[1]))
		printf("Error: File %s does not exist!\n", argv[1]);
	else {
		struct raptor_context context;
		// Initialize the VM.
		init_raptor_vm(&context, RAMSIZE);
		// Execute the VM with the file path argument.
		run_raptor_vm(&context, argv[1]);
		// Deconstruct.
		destroy_raptor_vm(&context);
		return 0;
	}
	return 1;
}

int file_exists(char *path) {
	struct stat buffer;
	return stat(path, &buffer) == 0;
}