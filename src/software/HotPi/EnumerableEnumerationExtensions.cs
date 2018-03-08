using System;
using System.Collections.Generic;

namespace Restall.HotPi
{
	public static class EnumerableEnumerationExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var item in source)
				action(item);
		}
	}
}
