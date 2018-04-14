namespace Restall.HotPi.Reflow.Profiles
{
	public interface IReflowProcessContext : IHaveSamplingInterval, IHaveProcessVariable<Temperature>, IHaveSetpoint<Temperature>
	{
	}
}
