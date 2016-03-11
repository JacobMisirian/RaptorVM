#include <stdio.h>
#include "opcodes.h"
#include "raptor_context.h"
#include "raptor_vm.h"

int main (int argc, char *argv[]) {
	struct raptor_context context;
	init_raptor_vm(&context);
	run_raptor_vm(&context, argv[1]);
	
	return 0;
}