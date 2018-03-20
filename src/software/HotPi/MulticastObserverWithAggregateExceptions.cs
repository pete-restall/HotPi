using System;
using System.Collections.Generic;
using System.Linq;

namespace Restall.HotPi
{
	[DoesNotParticipateInBindingConventions]
	public class MulticastObserverWithAggregateExceptions<T> : IObserve<T>
	{
		private readonly IEnumerable<IObserve<T>> observers;

		public MulticastObserverWithAggregateExceptions(IEnumerable<IObserve<T>> observers)
		{
			this.observers = observers.ToArray();
		}

		public void Observe(T observed)
		{
			var exceptions = this.observers
				.Select(observer => ContinueIfException(observer, observed))
				.Where(exception => exception != null)
				.ToArray();

			if (exceptions.Length > 0)
				throw new AggregateException($"Exception(s) occurred when observing {observed}", exceptions);
		}

		private static Exception ContinueIfException(IObserve<T> observer, T observed)
		{
			try
			{
				observer.Observe(observed);
				return null;
			}
			catch (Exception exception)
			{
				return exception;
			}
		}
	}
}
