using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

namespace Restall.HotPi
{
	public class ConventionsNinjectModule : NinjectModule
	{
		public override void Load()
		{
			this.Kernel?.Bind(cfg =>
			{
				AllClassesParticipatingInConventions(cfg).BindAllBaseClasses();
				AllClassesParticipatingInConventions(cfg).BindAllInterfaces();
			});
		}

		private static IJoinFilterWhereExcludeIncludeBindSyntax AllClassesParticipatingInConventions(IFromSyntax cfg)
		{
			return cfg
				.FromThisAssembly()
				.IncludingNonePublicTypes()
				.SelectAllClasses()
				.WithoutAttribute<DoesNotParticipateInBindingConventionsAttribute>();
		}
	}
}
