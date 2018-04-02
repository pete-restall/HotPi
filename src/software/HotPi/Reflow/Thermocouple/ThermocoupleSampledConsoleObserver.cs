using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Restall.HotPi.Reflow.Thermocouple
{
	public class ThermocoupleSampledConsoleObserver : IObserve<ThermocoupleSampled>
	{
		private readonly JsonSerializer serialiser;

		public ThermocoupleSampledConsoleObserver(JsonSerializer serialiser)
		{
			this.serialiser = serialiser;
		}

		public void Observe(ThermocoupleSampled observed)
		{
			Debug.WriteLine(JObject.FromObject(observed, this.serialiser).ToString(Formatting.Indented));
		}
	}
}
