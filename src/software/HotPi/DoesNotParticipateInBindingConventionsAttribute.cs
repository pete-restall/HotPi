using System;

namespace Restall.HotPi
{
	[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public class DoesNotParticipateInBindingConventionsAttribute : Attribute
	{
	}
}
