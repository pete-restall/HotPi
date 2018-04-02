using System.Collections.Generic;
using System.Linq;

namespace Restall.HotPi.Reflow.Controller
{
	public class ReflowProcessController : IControlReflowProcess, IProvideReflowTemperatureSetpoints, IObserve<PidControllerAdjusted>
	{
		private readonly object sync = new object();
		private ReflowZone[] zones = new ReflowZone[0];
		private ReflowProcessContext context;

		bool IControlReflowProcess.Start(params ReflowZone[] zones)
		{
			return ((IControlReflowProcess) this).Start((IEnumerable<ReflowZone>) zones);
		}

		bool IControlReflowProcess.Start(IEnumerable<ReflowZone> zones)
		{
			lock (this.sync)
			{
				if (this.zones.Length > 0)
					return false;

				this.zones = zones.ToArray();
				return this.zones.Length > 0;
			}
		}

		void IControlReflowProcess.Stop()
		{
			this.zones = new ReflowZone[0];
		}

		Temperature IProvideReflowTemperatureSetpoints.GetNextSetpoint()
		{
			if (this.context == null)
				return Temperature.Undefined;

			var zone = this.zones.FirstOrDefault(z => z.CanProvideNextSetpoint(this.context));
			return zone?.GetNextSetpoint(this.context) ?? Temperature.Undefined;
		}

		void IObserve<PidControllerAdjusted>.Observe(PidControllerAdjusted observed)
		{
			this.context = new ReflowProcessContext(observed);
		}
	}
}
