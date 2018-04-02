namespace Restall.HotPi.Reflow.Controller
{
	public interface IReflowProfileSegment
	{
		bool CanProvideNextSetpoint(ReflowProcessContext context);

		Temperature GetNextSetpoint(ReflowProcessContext context);
	}
}
