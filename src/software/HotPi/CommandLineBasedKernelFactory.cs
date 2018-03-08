using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Modules;

namespace Restall.HotPi
{
	public static class CommandLineBasedKernelFactory
	{
		public static IKernel Create<TArguments>(TArguments args)
		{
			var kernel = new StandardKernel(AllNinjectModulesInCurrentAssemblyWithConventionsModuleFirst);
			try
			{
				kernel.Rebind<TArguments>().ToConstant(args);
				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		private static INinjectModule[] AllNinjectModulesInCurrentAssemblyWithConventionsModuleFirst => AllNinjectModulesInCurrentAssembly
			.OrderBy(module => module.GetType() == typeof(ConventionsNinjectModule) ? 0 : 1)
			.ToArray();

		private static IEnumerable<INinjectModule> AllNinjectModulesInCurrentAssembly => AllNinjectModuleTypesInCurrentAssembly
			.Select(Activator.CreateInstance)
			.Cast<INinjectModule>();

		private static IEnumerable<Type> AllNinjectModuleTypesInCurrentAssembly => AllTypes.InAssembly.Where(typeof(INinjectModule).IsAssignableFrom);
	}
}
