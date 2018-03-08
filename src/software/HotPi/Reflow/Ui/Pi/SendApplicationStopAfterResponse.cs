using Nancy;

namespace Restall.HotPi.Reflow.Ui.Pi
{
	[DoesNotParticipateInBindingConventions]
	public class SendApplicationStopAfterResponse : IStoppable
	{
		private readonly NancyContext context;
		private readonly IStoppable application;

		public SendApplicationStopAfterResponse(NancyContext context, [RunningApplication] IStoppable application)
		{
			this.context = context;
			this.application = application;
		}

		public void Stop()
		{
			this.context.Items.Add("Shutdown", new OnDispose(() =>
			{
				// TODO: NEED TO FIRE OFF THE PROCESS TO SHUT DOWN THE PI...UNLESS WE CAN TRIGGER THAT ON APPLICATION EXIT...
				this.application.Stop();
			}));
		}
	}
}
