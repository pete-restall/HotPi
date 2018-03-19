using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow
{
	public struct Temperature
	{
		public Temperature(decimal kelvin)
		{
			this.Kelvin = kelvin;
		}

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public decimal Kelvin { get; private set; }
	}
}
