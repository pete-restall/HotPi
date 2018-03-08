using System;
using System.Collections.Generic;
using System.Reflection;

namespace Restall.HotPi
{
	public static class AllTypes
	{
		public static IEnumerable<Type> InAssembly => Assembly.GetExecutingAssembly().GetTypes();
	}
}
