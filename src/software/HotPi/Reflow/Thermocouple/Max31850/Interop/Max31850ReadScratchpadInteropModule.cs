using Ninject.Modules;
using Raspberry;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class Max31850ReadScratchpadInteropModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<IMax31850ReadScratchpadInterop>()
				.To<Max31850ReadScratchpadInterop>()
				.When(_ => Board.Current.IsRaspberryPi);

			this.Kernel?
				.Bind<IMax31850ReadScratchpadInterop>()
				.To<NonPiMax31850ReadScratchpadInterop>()
				.When(_ => !Board.Current.IsRaspberryPi);
		}
	}
}
