using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Tulur.DataMappings
{
	public class DataMapper
	{
		public DataMapper(int capacity = 8)
		{
			_convertFunctions = new Dictionary<ComplexKey, Delegate>(capacity);
		}

		public void Register<TSource, TResult>(Action<TSource, TResult> postAction = null) where TResult : new()
		{
			Type typeSource = typeof(TSource);
			Type typeResult = typeof(TResult);
			ComplexKey key = new ComplexKey(typeSource, typeResult);

			Func<TSource, TResult> convertFunction = CreateConvertFunction<TSource, TResult>();
			if (postAction != null)
			{
				Func<TSource, TResult> specialConvertFunction = delegate(TSource source)
				{
					TResult result = convertFunction(source);
					postAction(source, result);
					return result;
				};

				_convertFunctions[key] = specialConvertFunction;
			}
			else
			{
				_convertFunctions[key] = convertFunction;
			}
		}

		public TResult Map<TSource, TResult>(TSource source) where TResult : new()
		{
			Type typeSource = typeof(TSource);
			Type typeResult = typeof(TResult);
			ComplexKey key = new ComplexKey(typeSource, typeResult);

			Delegate convertFunction;
			if (!_convertFunctions.TryGetValue(key, out convertFunction))
			{
				const string ERROR = "Pair of types {0}, {1} not registered. You should register this pair of types using method {2}().";
				throw new Exception(string.Format(ERROR, typeSource.FullName, typeResult.FullName, nameof(Register)));
			}

			return ((Func<TSource, TResult>) convertFunction)(source);
		}

		private readonly Dictionary<ComplexKey, Delegate> _convertFunctions;

		private static Func<TSource, TResult> CreateConvertFunction<TSource, TResult>()
		{
			Type typeSource = typeof(TSource);
			Type typeResult = typeof(TResult);

			const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public;
			PropertyInfo[] getProps = typeSource.GetProperties(BINDING_FLAGS);
			PropertyInfo[] setProps = typeResult.GetProperties(BINDING_FLAGS);

			ParameterExpression instance = Expression.Parameter(typeSource, null);

			IEnumerable<MemberAssignment> props = getProps
				.Join(setProps, g => g.Name, s => s.Name, (g, s) => new {PropertyGet = g, PropertySet = s})
				.Select(x => Expression.Bind(x.PropertySet, Expression.Property(instance, x.PropertyGet)));

			MemberInitExpression body = Expression.MemberInit(Expression.New(typeResult), props);

			return Expression.Lambda<Func<TSource, TResult>>(body, instance).Compile();
		}
	}
}