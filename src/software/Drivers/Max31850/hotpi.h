#ifndef __HOTPI_HOTPI_H
#define __HOTPI_HOTPI_H
#include <limits.h>

#define EXPORT __attribute__((visibility("default")))
#define NOEXPORT __attribute__((visibility("hidden")))

#define ERROR (((unsigned int) -1) & ~0x7fffffffu)
#define ERROR_WITH_ERRNO (ERROR | 0x40000000)

#define ERROR_NONE 0
#define ERROR_INIT_SCHEDULER (ERROR_WITH_ERRNO | 1)
#define ERROR_INIT_BCM2835 (ERROR_WITH_ERRNO | 2)
#define ERROR_NO_PRESENCE_PULSE (ERROR | 16)
#define ERROR_PRESENCE_PULSE_TOO_LONG (ERROR | 17)
#define ERROR_SAMPLE_TIME_TOO_LONG (ERROR | 32)
#define ERROR_SCRATCHPAD_CANNOT_BE_NULL (ERROR | 64)

#endif
