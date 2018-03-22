using Ninject.Extensions.ContextPreservation;
using Ninject.Modules;
using Raspberry;
using Raspberry.IO.GeneralPurpose;

namespace Restall.HotPi.Reflow.Gpio
{
	public class GpioConnectionModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<GpioConnection>()
				.ToSelf()
				.InSingletonScope()
				.OnActivation(x => x.Open())
				.OnDeactivation(x => x.Close());

			this.Kernel?
				.Bind<GpioConnectionSettings>()
				.ToMethod(ctx => new GpioConnectionSettings
				{
					Driver = ctx.ContextPreservingGet<IGpioConnectionDriver>(),
					Opened = false
				});

			this.Kernel?
				.Bind<IGpioConnectionDriver>()
				.To<FileGpioConnectionDriver>()
				.When(_ => Board.Current.IsRaspberryPi);

			this.Kernel?
				.Bind<IGpioConnectionDriver>()
				.To<DummyGpioConnectionDriver>()
				.When(_ => !Board.Current.IsRaspberryPi);
		}
	}
}
