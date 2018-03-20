CC=gcc
CCARGS=-c -Wall -pedantic -fPIC -O3
LD=ld
LDARGS=-soname libhotpi-max31850.so.1 -shared
LIBS=-lbcm2835

SO_FILE=libhotpi-max31850.so.1.0.0
OBJ_FILES=max31850.o onewire.o errno.o

all: $(OBJ_FILES)
	$(LD) $(LDARGS) -o $(SO_FILE) $(OBJ_FILES) $(LIBS)

clean:
	rm -Rf $(OBJ_FILES) $(SO_FILE)

%.o: %.c
	$(CC) $(CCARGS) -o $@ $<
