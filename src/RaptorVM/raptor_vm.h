#ifndef raptor_vm_h
#define raptor_vm_h

#include "raptor_context.h"

// Constructor.
void init_raptor_vm(struct raptor_context *context, size_t size);
// Deconstructor.
void run_raptor_vm(struct raptor_context *context, char *file_path);

#endif