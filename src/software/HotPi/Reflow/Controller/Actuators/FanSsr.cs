using System;
using Raspberry.IO;
using Restall.HotPi.Reflow.Gpio;
using Restall.HotPi.Reflow.Profiles;

namespace Restall.HotPi.Reflow.Controller.Actuators
{
	public class FanSsr : IControlReflowProcess
	{
		private readonly IOutputBinaryPin ssr;
		private readonly IControlReflowProcess controller;

		public FanSsr([GpioPin("SSR2")] IOutputBinaryPin ssr, IControlReflowProcess controller)
		{
			this.ssr = ssr;
			this.controller = controller;
		}

		public bool TryStart(StartProcessRunCommand profile, out StartProcessRunCommand running)
		{
			if (!this.controller.TryStart(profile, out running))
				return false;

			this.EnsureFanIsAlwaysOnDuringReflow();
			return true;
		}

		private void EnsureFanIsAlwaysOnDuringReflow()
		{
			Console.WriteLine("FAN SSR ON");
			this.ssr.Write(true);
		}

		public void Abort()
		{
			this.controller.Abort();
		}
	}
}
