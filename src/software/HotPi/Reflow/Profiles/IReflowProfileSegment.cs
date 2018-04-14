namespace Restall.HotPi.Reflow.Profiles
{
	public interface IReflowProfileSegment
	{
		bool CanProvideNextSetpoint(IReflowProcessContext context);

		Temperature GetNextSetpoint(IReflowProcessContext context);
	}
}
