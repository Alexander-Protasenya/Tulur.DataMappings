using System;

namespace Tulur.DataMapping
{
	internal sealed class ComplexKey : IEquatable<ComplexKey>
	{
		public ComplexKey(Type first, Type second)
		{
			_first = first;
			_second = second;
		}

		public bool Equals(ComplexKey other)
		{
			return ReferenceEquals(_first, other._first) && ReferenceEquals(_second, other._second);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((_first?.GetHashCode() ?? 0) << 16) ^ ((_second?.GetHashCode() ?? 0) & 65535);
			}
		}

		private readonly Type _first;
		private readonly Type _second;
	}
}