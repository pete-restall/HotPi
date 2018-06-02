namespace Restall.HotPi.Reflow.Profiles
{
	public interface IHaveSetpoint<out T>
	{
		T Setpoint { get; }
	}
}
