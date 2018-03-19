#include <errno.h>

#include "hotpi.h"

int EXPORT max31850GetLastErrorNumber(void)
{
	return errno;
}
