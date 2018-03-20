using Ninject.Modules;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	public class Max31850NearRealtimeCommandExecutorModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?
				.Bind<Max31850NearRealtimeCommandExecutor, IReflowPlantProcess, IExecuteMax31850CommandsInNearRealtime>()
				.To<Max31850NearRealtimeCommandExecutor>()
				.InSingletonScope();
		}
	}
}
