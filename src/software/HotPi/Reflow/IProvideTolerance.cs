using System;

namespace Restall.HotPi.Reflow
{
	public interface IProvideTolerance
	{
		ToleranceCheck<T> ForTarget<T>(T target, Func<T, double> getValue);
	}
}
