using System;
using System.Diagnostics.CodeAnalysis;
using NullGuard;

namespace Restall.HotPi.Reflow
{
	public class Temperature : IComparable<Temperature>, IEquatable<Temperature>
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

		public override bool Equals([AllowNull] object obj)
		{
			return this.Equals(obj as Temperature);
		}

		public static bool operator !=([AllowNull] Temperature a, [AllowNull] Temperature b)
		{
			return !(a == b);
		}

		public static bool operator ==([AllowNull] Temperature a, [AllowNull] Temperature b)
		{
			return a?.Equals(b) == true;
		}

		public bool Equals([AllowNull] Temperature other)
		{
			return this.CompareTo(other) == 0;
		}

		public int CompareTo([AllowNull] Temperature other)
		{
			if (ReferenceEquals(this, other))
				return 0;

			if (ReferenceEquals(null, other))
				return 1;

			return this.Kelvin.CompareTo(other.Kelvin);
		}

		public static bool operator <([AllowNull] Temperature a, [AllowNull] Temperature b)
		{
			if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
				return false;

			return ReferenceEquals(a, null) || a.CompareTo(b) < 0;
		}

		public static bool operator <=([AllowNull] Temperature a, [AllowNull] Temperature b)
		{
			return ReferenceEquals(a, null) || a.CompareTo(b) <= 0;
		}

		public static bool operator >([AllowNull] Temperature a, [AllowNull] Temperature b)
		{
			return a?.CompareTo(b) > 0;
		}

		public static bool operator >=([AllowNull] Temperature a, [AllowNull] Temperature b)
		{
			if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
				return true;

			return a?.CompareTo(b) >= 0;
		}

		[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = CodeAnalysisJustification.ImmutableSemantics)]
		public override int GetHashCode()
		{
			return this.Kelvin.GetHashCode();
		}
	}
}
