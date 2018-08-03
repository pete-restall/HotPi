#!/usr/bin/python
from ctypes import *
import os

class Max31850Scratchpad(Structure):
    _fields_ = [("buffer", c_ubyte * 9)]

class HotPi(object):
    ''' Python driver for HotPi
    '''
    def __init__(self, units = "c"):
        ''' Initialise HotPi

        Parameters:
        - units:     (optional) unit of measurement to return. ("c" (default) | "k" | "f")
        '''
        self.lib = CDLL(os.path.abspath("libhotpi-max31850.so.1.0.0"))
        self.units = units
        self.scratchpad = Max31850Scratchpad()
        self.valid_scratchpad = None

    def initialise_for_this_thread(self):
        result = self.lib.max31850Initialise()
        if (result != 0):
            raise HotPiError("Unable to initialise MAX31850; result=0x{0:8x}, errno={1}".format(result & 0xffffffff, get_errno()))

    def get(self):
        '''Reads the MAX31850 and returns the current value of the thermocouple.'''
        self.read()
        self.checkErrors()
        self.valid_scratchpad = self.scratchpad
        self.scratchpad = Max31850Scratchpad()
        return getattr(self, "to_" + self.units)(self.data_to_tc_temperature())

    def get_rj(self):
        '''Reads the MAX31850 and returns the current value of the reference junction.'''
        self.read()
        self.checkErrors()
        self.valid_scratchpad = self.scratchpad
        self.scratchpad = Max31850Scratchpad()
        return getattr(self, "to_" + self.units)(self.data_to_rj_temperature())

    def read(self):
        '''Samples the thermocouple and reads the scratchpad to store the result in self.scratchpad.'''
        result = self.lib.max31850SampleThermocouple()
        if (result != 0):
            raise HotPiError("Unable to sample thermocouple; result=0x{0:8x}, errno={1}".format(result & 0xffffffff, get_errno()))

        result = self.lib.max31850ReadScratchpad(byref(self.scratchpad))
        if (result != 0):
            raise HotPiError("Unable to read MAX31850 scratchpad; result=0x{0:8x}, errno={1}".format(result & 0xffffffff, get_errno()))

    def checkErrors(self):
        '''Checks error bits to see if there are any faults.'''
        self.ensure_crc_is_valid()
        if (self.scratchpad.buffer[2] & 0x04 != 0):
            raise HotPiError("MAX31850 scratchpad - short to VDD")
        if (self.scratchpad.buffer[2] & 0x02 != 0):
            raise HotPiError("MAX31850 scratchpad - short to GND")
        if (self.scratchpad.buffer[2] & 0x01 != 0):
            raise HotPiError("MAX31850 scratchpad - open circuit")
        if (self.scratchpad.buffer[0] & 0x01 != 0):
            raise HotPiError("MAX31850 scratchpad - 'Fault' bit set")

    def ensure_crc_is_valid(self):
        shift_register = [0] * 8
        for bit in self.scratchpad_to_bits():
            new_entry = bit ^ shift_register[7]
            shift_register[7] = shift_register[6]
            shift_register[6] = shift_register[5]
            shift_register[5] = shift_register[4] ^ new_entry
            shift_register[4] = shift_register[3] ^ new_entry
            shift_register[3] = shift_register[2]
            shift_register[2] = shift_register[1]
            shift_register[1] = shift_register[0]
            shift_register[0] = new_entry

        if (sum(shift_register) != 0):
            raise HotPiError("MAX31850 scratchpad CRC is invalid")

    def scratchpad_to_bits(self):
        bits = [0] * (len(self.scratchpad.buffer) * 8)
        for i in range(len(self.scratchpad.buffer)):
            for n in range(8):
                if ((self.scratchpad.buffer[i] & (1 << n)) != 0):
                    bits[(i << 3) + n] = 1

        return bits

    def data_to_tc_temperature(self):
        '''Returns the thermocouple temperature in celsius.'''
        if (self.valid_scratchpad == None):
            return 0

        fixed_point = (self.valid_scratchpad.buffer[1] << 24) | ((self.valid_scratchpad.buffer[0] & 0xfc) << 16)
        return float(fixed_point) / (1 << 20)

    def data_to_rj_temperature(self, data_32 = None):
        '''Returns the reference junction temperature in celsius.'''
        if (self.valid_scratchpad == None):
            return 0

        fixed_point = (self.valid_scratchpad.buffer[3] << 24) | ((self.valid_scratchpad.buffer[2] & 0xf0) << 16)
        return float(fixed_point) / (1 << 24)

    def to_c(self, celsius):
        '''Celsius passthrough for generic to_* method.'''
        return celsius

    def to_k(self, celsius):
        '''Convert celsius to kelvin.'''
        return celsius + 273.15

    def to_f(self, celsius):
        '''Convert celsius to fahrenheit.'''
        return celsius * 9.0/5.0 + 32

    def cleanup(self):
        self.lib.max31850Shutdown()
        return


class HotPiError(Exception):
     def __init__(self, value):
         self.value = value

     def __str__(self):
         return repr(self.value)
