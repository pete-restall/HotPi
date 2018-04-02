namespace Restall.HotPi.Reflow.Controller
{
	public interface IHavePidControllerSettings
	{
		double Kp { get; }

		double Ki { get; }

		double Kd { get; }
	}
}
