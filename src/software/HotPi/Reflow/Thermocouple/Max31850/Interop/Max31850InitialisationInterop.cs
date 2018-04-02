using System.Runtime.InteropServices;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class Max31850InitialisationInterop : IMax31850InitialisationInterop
	{
		public void Initialise()
		{
			int result = Max31850Initialise();
			Max31850ErrorInterop.ThrowExceptionIfResultCodeIsError(result, nameof(Max31850Initialise));
		}

		[DllImport(Max31850Interop.NativeLibraryName, EntryPoint = "max31850Initialise", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Max31850Initialise();

		public void Shutdown()
		{
			Max31850Shutdown();
		}

		[DllImport(Max31850Interop.NativeLibraryName, EntryPoint = "max31850Shutdown", CallingConvention = CallingConvention.Cdecl)]
		private static extern int Max31850Shutdown();
	}
}
