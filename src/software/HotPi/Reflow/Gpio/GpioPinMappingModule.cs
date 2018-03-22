using Ninject.Modules;
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
			this.Kernel?
				.Bind<PinConfiguration>()
				.ToConstant(new OutputPinConfiguration(pin.ToProcessor()) { Name = name, Reversed = isActiveLow })
				.Named("gpio:pin/" + name);

			return this;
		}

		private GpioPinMappingModule BindInputPin(ConnectorPin pin, string name, bool isActiveLow = false, PinResistor pull = PinResistor.None)
		{
			this.Kernel?
				.Bind<PinConfiguration>()
				.ToConstant(new InputPinConfiguration(pin.ToProcessor()) { Name = name, Reversed = isActiveLow, Resistor = pull })
				.Named("gpio:pin/" + name);

			return this;
		}
	}
}
