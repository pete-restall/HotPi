namespace Restall.HotPi
{
	public interface IObserve<in TEvent>
	{
		void Observe(TEvent observed);
	}
}
