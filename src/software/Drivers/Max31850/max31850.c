#include <bcm2835.h>
#include <sched.h>
#include <sys/mman.h>

#include "onewire.h"
#include "max31850.h"

static int setCurrentThreadToNearRealtime(void);
static void ensureThreadHasLittleChanceOfBeingInterrupted();

int EXPORT max31850Initialise(void)
{
	if (!setCurrentThreadToNearRealtime())
		return ERROR_INIT_SCHEDULER;

	return onewireInitialise();
}

static int setCurrentThreadToNearRealtime(void)
{
	static struct sched_param schedulerParameters = {0};
	schedulerParameters.sched_priority = sched_get_priority_max(SCHED_FIFO) - 1;
	if (sched_setscheduler(0, SCHED_FIFO, &schedulerParameters) != 0)
		return 0;

	return mlockall(MCL_CURRENT | MCL_FUTURE) == 0;
}

void EXPORT max31850Shutdown(void)
{
	onewireShutdown();
}

int EXPORT max31850SampleThermocouple(void)
{
	int result;

	ensureThreadHasLittleChanceOfBeingInterrupted();
	result = onewireReset();
	if (result != 0)
		return result;

	onewireWriteSkipRom();
	onewireWriteByte(0x44);
	bcm2835_delay(65);
	for (int i = 0; i < 4; i++)
	{
		bcm2835_delay(10);
		if (onewireReadBit())
			return ERROR_NONE;
	}

	return ERROR_SAMPLE_TIME_TOO_LONG;
}

static void ensureThreadHasLittleChanceOfBeingInterrupted()
{
	sched_yield();
}

int EXPORT max31850ReadScratchpad(Scratchpad *const scratchpad)
{
	int result;

	if (!scratchpad)
		return ERROR_SCRATCHPAD_CANNOT_BE_NULL;

	ensureThreadHasLittleChanceOfBeingInterrupted();
	result = onewireReset();
	if (result != 0)
		return result;

	onewireWriteSkipRom();
	onewireWriteByte(0xbe);
	for (size_t i = 0; i < sizeof(scratchpad->buffer); i++)
		scratchpad->buffer[i] = onewireReadByte();

	return ERROR_NONE;
}
