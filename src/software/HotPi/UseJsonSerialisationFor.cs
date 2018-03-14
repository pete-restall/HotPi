using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Planning.Targets;
using NullGuard;

namespace Restall.HotPi
{
	public class UseJsonSerialisationFor : IParameter
	{
		public UseJsonSerialisationFor(string name)
		{
			this.Name = name;
		}

		public string Name { get; }

		public bool Equals([AllowNull] IParameter other)
		{
			var castOther = other as UseJsonSerialisationFor;
			return castOther?.Name == this.Name;
		}

		public object GetValue(IContext context, ITarget target)
		{
			return this.Name;
		}

		public bool ShouldInherit => true;
	}
}
