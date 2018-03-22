using Ninject;

namespace Restall.HotPi.Reflow.Gpio
{
	public class GpioPinAttribute : NamedAttribute
	{
		public GpioPinAttribute(string name) : base("gpio:pin/" + name)
		{
		}
	}
}
