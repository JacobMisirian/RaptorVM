#ifndef raptor_vm_h
#define raptor_vm_h

#include "raptor_context.h"

void init_raptor_vm(struct raptor_context *context);
void run_raptor_vm(struct raptor_context *context, char *file_path);

#endif