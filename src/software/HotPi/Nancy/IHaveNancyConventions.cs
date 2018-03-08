using Nancy.Conventions;

namespace Restall.HotPi.Nancy
{
	public interface IHaveNancyConventions
	{
		void ApplyConventionsTo(NancyConventions conventions);
	}
}
