namespace Restall.HotPi.Reflow.Profiles
{
	public interface IHaveProcessVariable<out T>
	{
		T ProcessVariable { get; }
	}
}
