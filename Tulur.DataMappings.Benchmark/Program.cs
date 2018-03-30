using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace Tulur.DataMappings.Benchmark
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("FsMapper vs. Tulur.DataMapper");
			Console.WriteLine(new string('-', 29));
			Console.WriteLine();

			#region Register mapping rules for DataMapper

			DataMapper dataMapper = new DataMapper();

			dataMapper.Register<object, TypeA>();
			dataMapper.Register<object, TypeB>();
			dataMapper.Register<object, TypeC>();
			dataMapper.Register<object, TypeD>();

			dataMapper.Register<TypeA, object>();
			dataMapper.Register<TypeA, TypeB>();
			dataMapper.Register<TypeA, TypeC>();
			dataMapper.Register<TypeA, TypeD>();

			dataMapper.Register<TypeB, object>();
			dataMapper.Register<TypeB, TypeA>();
			dataMapper.Register<TypeB, TypeC>();
			dataMapper.Register<TypeB, TypeD>();

			dataMapper.Register<TypeC, object>();
			dataMapper.Register<TypeC, TypeA>();
			dataMapper.Register<TypeC, TypeB>();
			dataMapper.Register<TypeC, TypeD>();

			dataMapper.Register<TypeD, object>();
			dataMapper.Register<TypeD, TypeA>();
			dataMapper.Register<TypeD, TypeB>();
			dataMapper.Register<TypeD, TypeC>();

			#endregion

			#region Register mapping rules for FsMapper

			FsMapper fsMapper = new FsMapper();

			fsMapper.Register<object, TypeA>();
			fsMapper.Register<object, TypeB>();
			fsMapper.Register<object, TypeC>();
			fsMapper.Register<object, TypeD>();

			fsMapper.Register<TypeA, object>();
			fsMapper.Register<TypeA, TypeB>();
			fsMapper.Register<TypeA, TypeC>();
			fsMapper.Register<TypeA, TypeD>();

			fsMapper.Register<TypeB, object>();
			fsMapper.Register<TypeB, TypeA>();
			fsMapper.Register<TypeB, TypeC>();
			fsMapper.Register<TypeB, TypeD>();

			fsMapper.Register<TypeC, object>();
			fsMapper.Register<TypeC, TypeA>();
			fsMapper.Register<TypeC, TypeB>();
			fsMapper.Register<TypeC, TypeD>();

			fsMapper.Register<TypeD, object>();
			fsMapper.Register<TypeD, TypeA>();
			fsMapper.Register<TypeD, TypeB>();
			fsMapper.Register<TypeD, TypeC>();

			#endregion

			const int LIMIT = 10000000;
			Stopwatch sw;
			TypeA typeA = new TypeA {Title = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString()};

			long dataMapperCounter = 0;
			long fsMapperCounter = 0;
			for (int n = 0; n < 5; n++)
			{
				Console.Write(nameof(DataMapper) + ": ");
				sw = new Stopwatch();
				sw.Start();
				for (int i = 0; i < LIMIT; i++)
				{
					TypeB typeB = dataMapper.Map<TypeA, TypeB>(typeA);
					TypeC typeC = dataMapper.Map<TypeB, TypeC>(typeB);
					TypeD typeD = dataMapper.Map<TypeC, TypeD>(typeC);
					Object obj = dataMapper.Map<TypeD, Object>(typeD);
				}
				sw.Stop();
				dataMapperCounter += sw.ElapsedMilliseconds;
				Console.WriteLine(sw.ElapsedMilliseconds + " ms.");

				Console.Write("  " + nameof(FsMapper) + ": ");
				sw = new Stopwatch();
				sw.Start();
				for (int i = 0; i < LIMIT; i++)
				{
					TypeB typeB = fsMapper.Map<TypeA, TypeB>(typeA);
					TypeC typeC = fsMapper.Map<TypeB, TypeC>(typeB);
					TypeD typeD = fsMapper.Map<TypeC, TypeD>(typeC);
					Object obj = fsMapper.Map<TypeD, Object>(typeD);
				}
				sw.Stop();
				fsMapperCounter += sw.ElapsedMilliseconds;
				Console.WriteLine(sw.ElapsedMilliseconds + " ms.");
			}

			Console.WriteLine();
			if (dataMapperCounter < fsMapperCounter)
			{
				Console.WriteLine("{0} win! {1} ms.", nameof(DataMapper), fsMapperCounter - dataMapperCounter);
			}
			else
			{
				Console.WriteLine("{0} win! {1} ms.", nameof(FsMapper), dataMapperCounter - fsMapperCounter);
			}
			Console.WriteLine();

			Console.WriteLine();
			Console.WriteLine("Press ENTER...");
			Console.ReadLine();
		}
	}
}