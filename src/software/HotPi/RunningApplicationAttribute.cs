using System;

namespace Restall.HotPi
{
	[AttributeUsage(validOn: AttributeTargets.Parameter)]
	public class RunningApplicationAttribute : Attribute
	{
	}
}
