using System;

namespace Tulur.DataMappings
{
	internal readonly struct ComplexKey : IEquatable<ComplexKey>
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
			return (_first.GetHashCode() << 16) ^ (_second.GetHashCode() & 65535);
		}

		private readonly Type _first;

		private readonly Type _second;
	}
}