using System;
using System.Diagnostics.CodeAnalysis;

namespace Restall.HotPi.Reflow
{
	public class Temperature : IEquatable<Temperature>
	{
		public static readonly Temperature Undefined = new Temperature();

		private Temperature()
		{
			this.Kelvin = -1;
		}

		public Temperature(decimal kelvin)
		{
			if (kelvin < 0)
				throw new ArgumentOutOfRangeException(nameof(kelvin), kelvin, "Temperature on the Kelvin scale starts at Absolute Zero");

			this.Kelvin = kelvin;
		}

		[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local", Justification = CodeAnalysisJustification.ForSerialisation)]
		public decimal Kelvin { get; private set; }

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Temperature);
		}

		public static bool operator !=(Temperature a, Temperature b)
		{
			return !(a == b);
		}

		public static bool operator ==(Temperature a, Temperature b)
		{
			return a?.Equals(b) == true;
		}

		public bool Equals(Temperature other)
		{
			if (ReferenceEquals(null, other))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			return this.Kelvin == other.Kelvin;
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = CodeAnalysisJustification.ImmutableSemantics)]
		public override int GetHashCode()
		{
			return this.Kelvin.GetHashCode();
		}

		public static Temperature operator +(Temperature a, Temperature b)
		{
			return new Temperature(a.Kelvin + b.Kelvin);
		}
	}
}
