using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Tulur.DataMappings.Benchmark
{
	public class FsMapper : IMapper
	{
		public void Register<TSource, TDest>()
		{
			var activator = _objectBuilder.GetActivator<TSource, TDest>();
			var key = new TypeTuple(typeof(TSource), typeof(TDest));
			AddMap(key, activator);
		}

		public TDest Map<TSource, TDest>(TSource source)
		{
			var key = new TypeTuple(typeof(TSource), typeof(TDest));
			var activator = GetMap(key);
			return ((Func<TSource, TDest>) activator)(source);
		}

		internal void AddMap<TSource, TDest>(TypeTuple key, Func<TSource, TDest> activator)
		{
			_source[key] = activator;
		}

		internal Delegate GetMap(TypeTuple key)
		{
			return _source[key];
		}

		private readonly Dictionary<TypeTuple, Delegate> _source = new Dictionary<TypeTuple, Delegate>();

		private readonly IObjectBuilder _objectBuilder = new ExpressionNewObjectBuilder();
	}

	public interface IMapper
	{
		void Register<TSource, TDest>();

		TDest Map<TSource, TDest>(TSource source);
	}

	public class TypeTuple : IEquatable<TypeTuple>
	{
		public bool Equals(TypeTuple other)
		{
			if (ReferenceEquals(null, other)) return false;
			return Source == other.Source && Destination == other.Destination;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TypeTuple);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Source?.GetHashCode() ?? 0) << 16) ^ ((Destination?.GetHashCode() ?? 0) & 65535);
			}
		}

		public static bool operator ==(TypeTuple left, TypeTuple right)
		{
			if (ReferenceEquals(null, left)) return ReferenceEquals(null, right);
			return left.Equals(right);
		}

		public static bool operator !=(TypeTuple left, TypeTuple right)
		{
			if (ReferenceEquals(null, left)) return !ReferenceEquals(null, right);
			return !left.Equals(right);
		}

		public Type Source { get; set; }
		public Type Destination { get; set; }

		public TypeTuple(Type source, Type destination)
		{
			this.Source = source;
			this.Destination = destination;
		}
	}

	public class ExpressionNewObjectBuilder : IObjectBuilder
	{
		public Func<TSource, TDest> GetActivator<TSource, TDest>()
		{
			var ctor = typeof(TDest).GetConstructor(Type.EmptyTypes);
			if (ctor == null) throw new MissingMemberException(string.Format("The default constructor of {0} type is missing", typeof(TDest).Name));

			var orig = Expression.Parameter(typeof(TSource), "orig");

			var getProps = typeof(TSource).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var setProps = typeof(TDest).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var intersect = getProps.Join(setProps, gp => gp.Name, sp => sp.Name, (gp, sp) => new Tuple<PropertyInfo, PropertyInfo>(gp, sp));

			var props = intersect
				.Select(kv => (MemberBinding) Expression.Bind(kv.Item2, Expression.Property(orig, kv.Item1)));

			var body = Expression.MemberInit(
				Expression.New(typeof(TDest)), props
			);

			return Expression.Lambda<Func<TSource, TDest>>(body, orig).Compile();
		}
	}

	public interface IObjectBuilder
	{
		Func<TSource, TDest> GetActivator<TSource, TDest>();
	}
}