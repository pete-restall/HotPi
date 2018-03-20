using System;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	public interface IExecuteMax31850CommandsInNearRealtime
	{
		T Execute<T>(Func<T> action, TimeSpan timeout);
	}
}
