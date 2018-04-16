using Ninject.Modules;
using Raspberry.IO;
using Raspberry.IO.GeneralPurpose;

namespace Restall.HotPi.Reflow.Gpio
{
	public class GpioPinMappingModule : NinjectModule
	{
		public override void Load()
		{
			this
				.BindOutputPin(ConnectorPin.P1Pin11, "SSR2")
				.BindOutputPin(ConnectorPin.P1Pin13, "SSR1")
				.BindOutputPin(ConnectorPin.P1Pin15, "SSR0")
				.BindInputPin(ConnectorPin.P1Pin19, "WDTEXPIRED")
				.BindOutputPin(ConnectorPin.P1Pin21, "CLRWDT")
				.BindOutputPin(ConnectorPin.P1Pin35, "LED0")
				.BindOutputPin(ConnectorPin.P1Pin37, "LED1");
		}

		private GpioPinMappingModule BindOutputPin(ConnectorPin pin, string name, bool isActiveLow = false)
		{
			var processorPin = pin.ToProcessor();
			var pinName = "gpio:pin/" + name;

			this.Kernel?
				.Bind<PinConfiguration>()
				.ToConstant(new OutputPinConfiguration(processorPin) { Name = name, Reversed = isActiveLow })
				.Named(pinName);

			this.Kernel?
				.Bind<IOutputBinaryPin>()
				.To<GpioOutputBinaryPin>()
				.Named(pinName)
				.WithConstructorArgument(typeof(ProcessorPin), processorPin);

			return this;
		}

		private GpioPinMappingModule BindInputPin(ConnectorPin pin, string name, bool isActiveLow = false, PinResistor pull = PinResistor.None)
		{
			var processorPin = pin.ToProcessor();
			var pinName = "gpio:pin/" + name;

			this.Kernel?
				.Bind<PinConfiguration>()
				.ToConstant(new InputPinConfiguration(processorPin) { Name = name, Reversed = isActiveLow, Resistor = pull })
				.Named(pinName);

			this.Kernel?
				.Bind<IInputBinaryPin>()
				.To<GpioInputBinaryPin>()
				.Named(pinName)
				.WithConstructorArgument(typeof(ProcessorPin), processorPin)
				.WithConstructorArgument(typeof(PinResistor), pull);

			return this;
		}
	}
}
