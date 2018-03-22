using System;
using Raspberry.IO.GeneralPurpose;

namespace Restall.HotPi.Reflow.Gpio
{
	public class DummyGpioConnectionDriver : IGpioConnectionDriver
	{
		public GpioConnectionDriverCapabilities GetCapabilities()
		{
			return GpioConnectionDriverCapabilities.None;
		}

		public void Allocate(ProcessorPin pin, PinDirection direction)
		{
		}

		public void SetPinResistor(ProcessorPin pin, PinResistor resistor)
		{
		}

		public void SetPinDetectedEdges(ProcessorPin pin, PinDetectedEdges edges)
		{
		}

		public void Wait(ProcessorPin pin, bool waitForUp = true, TimeSpan timeout = new TimeSpan())
		{
		}

		public void Release(ProcessorPin pin)
		{
		}

		public void Write(ProcessorPin pin, bool value)
		{
		}

		public bool Read(ProcessorPin pin)
		{
			return false;
		}

		public ProcessorPins Read(ProcessorPins pins)
		{
			return pins;
		}
	}
}
