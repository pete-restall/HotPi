using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850
{
	[DoesNotParticipateInBindingConventions]
	public class Max31850NearRealtimeCommandExecutor : IReflowPlantProcess, IExecuteMax31850CommandsInNearRealtime, IDisposable
	{
		private class Command : IDisposable
		{
			private readonly ManualResetEventSlim wait;

			public Command(Func<object> action)
			{
				this.Action = action;
				this.wait = new ManualResetEventSlim();
			}

			public Func<object> Action { get; }

			public object Result { get; set; }

			public Exception Exception { get; set; }

			public void Done()
			{
				this.wait.Set();
			}

			public bool Wait(TimeSpan timeout)
			{
				return this.wait.Wait(timeout);
			}

			public void Dispose()
			{
				this.wait.Dispose();
				GC.SuppressFinalize(this);
			}
		}

		private readonly IMax31850InitialisationInterop interop;
		private readonly BlockingCollection<Command> commands;
		private readonly CancellationTokenSource cancel;
		private readonly Thread priorityThread;

		public Max31850NearRealtimeCommandExecutor(IMax31850InitialisationInterop interop)
		{
			this.interop = interop;
			this.commands = new BlockingCollection<Command>();
			this.cancel = new CancellationTokenSource();
			this.priorityThread = new Thread(this.CommandLoop);
		}

		public void Start()
		{
			this.priorityThread.Start();
		}

		public void Stop()
		{
			this.cancel.Cancel();
		}

		private void CommandLoop()
		{
			this.interop.Initialise();

			while (!this.cancel.IsCancellationRequested)
			{
				try
				{
					this.commands
						.GetConsumingEnumerable(this.cancel.Token)
						.ForEach(ExecuteCommandOnNearRealtimeThread);
				}
				catch
				{
					// Carry on regardless...
				}
			}

			this.interop.Shutdown();
		}

		private static void ExecuteCommandOnNearRealtimeThread(Command command)
		{
			try
			{
				command.Result = command.Action();
			}
			catch (Exception exception)
			{
				command.Exception = exception;
			}

			command.Done();
		}

		public T Execute<T>(Func<T> action, TimeSpan timeout)
		{
			if (!this.priorityThread.IsAlive)
				throw new InvalidOperationException("The near-realtime thread for executing MAX31850 commands is not running !");

			using (var command = new Command(() => action()))
			{
				this.commands.Add(command);
				if (!command.Wait(timeout))
					throw new TimeoutException($"Timed out after {timeout} while waiting for MAX31850 command to complete.");

				if (command.Exception != null)
					throw command.Exception;

				return (T) command.Result;
			}
		}

		public void Dispose()
		{
			this.cancel.Dispose();
			this.commands.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
