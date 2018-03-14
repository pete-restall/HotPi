using System;
using System.Linq;
using Ninject.Infrastructure.Language;

namespace Restall.HotPi
{
	public static class TypeInheritanceExtensions
	{
		public static bool Inherits(this Type type, Func<Type, bool> predicate)
		{
			return type.GetAllBaseTypes().Any(predicate);
		}
	}
}
