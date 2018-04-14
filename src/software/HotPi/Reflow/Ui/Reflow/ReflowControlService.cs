using System.Diagnostics.CodeAnalysis;
using Nancy;
using Nancy.Responses.Negotiation;
using Restall.HotPi.Nancy;
using Restall.HotPi.Reflow.Profiles;

namespace Restall.HotPi.Reflow.Ui.Reflow
{
	public class ReflowControlService
	{
		private readonly IReflowProfileRepository profiles;
		private readonly IControlReflowProcess control;
		private readonly ResourceLinker resources;
		private readonly Negotiator contentNegotiator;

		public ReflowControlService(IReflowProfileRepository profiles, IControlReflowProcess control, ResourceLinker resources, Negotiator contentNegotiator)
		{
			this.profiles = profiles;
			this.control = control;
			this.resources = resources;
			this.contentNegotiator = contentNegotiator;
		}

		[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = CodeAnalysisJustification.Rpc)]
		public object Start(StartProcessRunRequest request)
		{
			if (!this.profiles.TryGetById(request.ProfileId, out var profile))
				return HttpStatusCode.NotFound;

			var run = profile.TryStartProcessRun(this.control);
			var response = new StartProcessRunResponse(
				profileId: run.ProfileId,
				processRunId: run.Id,
				abortLink: this.resources.RelativeUriFor("reflow:abort", new { profileId = profile.Id, processRunId = run.Id }));

			return run.WasAlreadyRunning
				? this.Conflict(response)
				: this.Ok(response);
		}

		private object Conflict(object body)
		{
			return this.contentNegotiator
				.WithModel(body)
				.WithStatusCode(HttpStatusCode.Conflict);
		}

		private object Ok(object body)
		{
			return this.contentNegotiator
				.WithModel(body)
				.WithStatusCode(HttpStatusCode.OK);
		}

		[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = CodeAnalysisJustification.Rpc)]
		public object Abort(AbortProcessRunRequest request)
		{
			if (!this.profiles.TryGetById(request.ProfileId, out var profile))
				return HttpStatusCode.NotFound;

			var hasBeenAborted = profile.AbortProcessRun(request.ProcessRunId, this.control);
			var response = new AbortProcessRunResponse();

			return !hasBeenAborted
				? HttpStatusCode.NotFound
				: this.Ok(response);
		}
	}
}
