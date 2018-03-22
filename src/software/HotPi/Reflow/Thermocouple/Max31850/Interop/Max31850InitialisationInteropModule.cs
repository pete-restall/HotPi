using Ninject.Modules;
using Raspberry;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class Max31850InitialisationInteropModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<IMax31850InitialisationInterop>()
				.To<Max31850InitialisationInterop>()
				.When(_ => Board.Current.IsRaspberryPi);

			this.Kernel?
				.Bind<IMax31850InitialisationInterop>()
				.To<NonPiMax31850InitialisationInterop>()
				.When(_ => !Board.Current.IsRaspberryPi);
		}
	}
}
