using System.Collections.Generic;
using Restall.HotPi.Reflow.Controller;

namespace Restall.HotPi.Reflow
{
	public interface IControlReflowProcess
	{
		bool Start(params ReflowZone[] zones);

		bool Start(IEnumerable<ReflowZone> zones);

		void Stop();
	}
}
