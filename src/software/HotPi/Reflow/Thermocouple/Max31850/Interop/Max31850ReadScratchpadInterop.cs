using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class Max31850ReadScratchpadInterop : IMax31850ReadScratchpadInterop
	{
		[StructLayout(LayoutKind.Sequential)]
		[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local", Justification = "Struct existing on the stack and consisting of only bytes (not arrays !) can skip marshalling and be passed as a pointer, so readonly is not appropriate")]
		private struct Scratchpad
		{
			public byte buffer0;
			public byte buffer1;
			public byte buffer2;
			public byte buffer3;
			public byte buffer4;
			public byte buffer5;
			public byte buffer6;
			public byte buffer7;
			public byte buffer8;
		}

		public Max31850ReadScratchpadResponse ReadScratchpad()
		{
			var scratchpad = new Scratchpad();
			int result = Max31850ReadScratchpad(scratchpad);
			return new Max31850ReadScratchpadResponse(
				presencePulse: !Max31850ErrorInterop.IsError(result),
				raw: new[]
				{
					scratchpad.buffer0, scratchpad.buffer1, scratchpad.buffer2,
					scratchpad.buffer3, scratchpad.buffer4, scratchpad.buffer5,
					scratchpad.buffer6, scratchpad.buffer7, scratchpad.buffer8
				});
		}

		[DllImport(Max31850Interop.NativeLibraryName, EntryPoint = "max31850ReadScratchpad")]
		private static extern int Max31850ReadScratchpad(Scratchpad scratchpad);
	}
}
