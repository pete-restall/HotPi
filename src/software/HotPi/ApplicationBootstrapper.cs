using Ninject;

namespace Restall.HotPi
{
	public static class ApplicationBootstrapper
	{
		public static int Run<TApplication, TArguments>(TArguments args) where TApplication : IStartable, IStoppable
		{
			using (var kernel = CommandLineBasedKernelFactory.Create(args))
			{
				var application = kernel.Get<TApplication>();
				kernel.Bind<IStartable, IStoppable, object>().ToConstant(application).WhenTargetHas<RunningApplicationAttribute>();
				application.Start();
				return 0;
			}
		}
	}
}
