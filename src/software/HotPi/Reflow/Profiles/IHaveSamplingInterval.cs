using System;

namespace Restall.HotPi.Reflow.Profiles
{
	public interface IHaveSamplingInterval
	{
		TimeSpan SamplingInterval { get; }
	}
}
