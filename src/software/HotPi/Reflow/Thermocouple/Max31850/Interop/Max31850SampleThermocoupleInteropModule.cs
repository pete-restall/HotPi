using Ninject.Modules;
using Raspberry;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class Max31850SampleThermocoupleInteropModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<IMax31850SampleThermocoupleInterop>()
				.To<Max31850SampleThermocoupleInterop>()
				.When(_ => Board.Current.IsRaspberryPi);

			this.Kernel?
				.Bind<IMax31850SampleThermocoupleInterop>()
				.To<NonPiMax31850SampleThermocoupleInterop>()
				.When(_ => !Board.Current.IsRaspberryPi);
		}
	}
}
