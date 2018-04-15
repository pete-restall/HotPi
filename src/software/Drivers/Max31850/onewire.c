#include <bcm2835.h>

#include "onewire.h"

#define MAX_RISE_TIME_US 2
#define MIN_PULSE_US 2
#define RECOVERY_US (MAX_RISE_TIME_US + 2)

static const RPiGPIOPin onewire = RPI_GPIO_P1_26;

static void onewireTristate(void);
static void onewireDriveLow(void);
static int onewireIsLow(void);
static void onewireWriteBit(unsigned char bit);

int NOEXPORT onewireInitialise(void)
{
	if (!bcm2835_init())
		return ERROR_INIT_BCM2835;

	bcm2835_gpio_set_pud(onewire, BCM2835_GPIO_PUD_OFF);
	onewireTristate();

	return ERROR_NONE;
}

static void onewireTristate(void)
{
	bcm2835_gpio_fsel(onewire, BCM2835_GPIO_FSEL_INPT);
}

void NOEXPORT onewireShutdown(void)
{
	bcm2835_close();
}

int NOEXPORT onewireReset(void)
{
	onewireDriveLow();
	bcm2835_delayMicroseconds(490);
	onewireTristate();
	bcm2835_delayMicroseconds(70);
	if (!onewireIsLow())
		return ERROR_NO_PRESENCE_PULSE;

	bcm2835_delayMicroseconds(460);
	if (onewireIsLow())
		return ERROR_PRESENCE_PULSE_TOO_LONG;

	return ERROR_NONE;
}

static void onewireDriveLow(void)
{
	bcm2835_gpio_clr(onewire);
	bcm2835_gpio_fsel(onewire, BCM2835_GPIO_FSEL_OUTP);
}

static int onewireIsLow(void)
{
	return bcm2835_gpio_lev(onewire) == LOW;
}

void NOEXPORT onewireWriteSkipRom(void)
{
	onewireWriteByte(0xcc);
}

void NOEXPORT onewireWriteByte(unsigned char byte)
{
	for (int i = 0; i < 8; i++)
	{
		onewireWriteBit(byte & 0x01);
		byte >>= 1;
	}
}

static void onewireWriteBit(unsigned char bit)
{
	onewireDriveLow();
	if (bit == 0)
	{
		bcm2835_delayMicroseconds(61);
		onewireTristate();
		bcm2835_delayMicroseconds(MAX_RISE_TIME_US + RECOVERY_US);
	}
	else
	{
		bcm2835_delayMicroseconds(MIN_PULSE_US);
		onewireTristate();
		bcm2835_delayMicroseconds(
			61 - MIN_PULSE_US + MAX_RISE_TIME_US + RECOVERY_US);
	}
}

unsigned char NOEXPORT onewireReadByte(void)
{
	unsigned char byte = 0;
	for (unsigned char i = 0; i < 8; i++)
		byte |= (unsigned char) (onewireReadBit() << i);

	return byte;
}

unsigned char NOEXPORT onewireReadBit(void)
{
	unsigned char bit;
	onewireDriveLow();
	bcm2835_delayMicroseconds(MIN_PULSE_US);
	onewireTristate();
	bcm2835_delayMicroseconds(MAX_RISE_TIME_US + 8);
	bit = onewireIsLow() ? 0 : 1;
	bcm2835_delayMicroseconds(
		60 - MAX_RISE_TIME_US - 8 - MIN_PULSE_US + RECOVERY_US);

	return bit;
}
