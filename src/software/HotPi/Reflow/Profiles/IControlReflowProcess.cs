namespace Restall.HotPi.Reflow.Profiles
{
	public interface IControlReflowProcess
	{
		bool TryStart(StartProcessRunCommand profile, out StartProcessRunCommand running);

		void Abort();
	}
}
