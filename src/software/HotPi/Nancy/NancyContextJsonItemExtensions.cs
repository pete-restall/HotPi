using System;
using System.Collections.Generic;
using Nancy;

namespace Restall.HotPi.Nancy
{
	public static class NancyContextJsonItemExtensions
	{
		public static void SetLazyItem<T>(this NancyContext context, Func<T> obj)
		{
			context.SetItem(obj);
		}

		private static void SetItem<T>(this NancyContext context, T obj)
		{
			context.Items[ItemKeyFor<T>()] = obj;
		}

		private static string ItemKeyFor<T>()
		{
			return "__" + typeof(T).AssemblyQualifiedName;
		}

		public static T GetItem<T>(this NancyContext context)
		{
			object item;
			if (!context.Items.TryGetValue(ItemKeyFor<T>(), out item))
			{
				object lazyEvaluator;
				if (!context.Items.TryGetValue(ItemKeyFor<Func<T>>(), out lazyEvaluator))
					throw new KeyNotFoundException($"No item for type {typeof(T)} was found in the NancyContext.Items collection");

				item = ((Func<T>) lazyEvaluator)();
				context.SetItem(item);
			}

			return (T) item;
		}
	}
}
