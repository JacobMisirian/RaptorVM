#ifndef raptor_context_h
#define raptor_context_h

#include <stdint.h>

// Holds the RAM and Registers.
struct raptor_context {
	size_t *ramSize;
	char *ram;
	uint16_t registers[16];
};

#endif