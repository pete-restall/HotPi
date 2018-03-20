using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow
{
	public class Voltage : IEquatable<Voltage>
	{
		public Voltage(decimal volts)
		{
			this.Volts = volts;
		}

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public decimal Volts { get; private set; }

		public decimal Millivolts => this.Volts * 1000;

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Voltage);
		}

		public bool Equals(Voltage other)
		{
			if (ReferenceEquals(null, other))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			return this.Volts == other.Volts;
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = CodeAnalysisJustification.ImmutableSemantics)]
		public override int GetHashCode()
		{
			return this.Volts.GetHashCode();
		}
	}
}
