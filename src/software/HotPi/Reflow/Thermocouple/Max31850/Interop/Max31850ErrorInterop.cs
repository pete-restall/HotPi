using System;
using System.Runtime.InteropServices;

namespace Restall.HotPi.Reflow.Thermocouple.Max31850.Interop
{
	public class Max31850ErrorInterop
	{
		public static void ThrowExceptionIfResultCodeIsError(int result, string methodName)
		{
			if (!IsError(result))
				return;

			var errorMessage = $"MAX31850 interop result 0x{result:x8} calling {methodName}() indicates an error";
			if (IsErrorWithErrno(result))
			{
				int errno = Max31850GetLastError();
				errorMessage += $"; errno=0x{errno:x8}";
			}

			throw new InvalidOperationException(errorMessage);
		}

		public static bool IsError(int result)
		{
			return result != 0;
		}

		private static bool IsErrorWithErrno(int result)
		{
			return (result & 0x4000000) != 0;
		}

		[DllImport(Max31850Interop.NativeLibraryName, EntryPoint = "max31850GetLastError")]
		private static extern int Max31850GetLastError();

		public static bool IsPresencePulseError(int result)
		{
			return IsError(result, 16) || IsError(result, 17);
		}

		public static bool IsError(int result, int mask)
		{
			return (result & ~0xc000000) == mask;
		}
	}
}
