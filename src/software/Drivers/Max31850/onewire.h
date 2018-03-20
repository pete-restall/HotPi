#ifndef __HOTPI_ONEWIRE_H
#define __HOTPI_ONEWIRE_H
#include "hotpi.h"

int onewireInitialise(void);
void onewireShutdown(void);
int onewireReset(void);
void onewireWriteByte(unsigned char byte);
unsigned char onewireReadByte(void);
unsigned char onewireReadBit(void);

#endif
