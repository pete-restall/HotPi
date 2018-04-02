namespace Restall.HotPi.Reflow.Controller
{
	public interface IProvideReflowTemperatureSetpoints
	{
		Temperature GetNextSetpoint();
	}
}
