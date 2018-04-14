using System;

namespace Restall.HotPi.Reflow
{
	public interface IHaveReflowTimebaseSettings
	{
		TimeSpan SamplingInterval { get; }
	}
}
