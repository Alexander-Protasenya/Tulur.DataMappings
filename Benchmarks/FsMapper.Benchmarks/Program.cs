using System;

using BenchmarkDotNet.Running;

namespace FsMapper.Benchmarks
{
	public class Program
	{
		public static void Main()
		{
			BenchmarkRunner.Run<SingleObjectMappingFunc>();
			Console.ReadLine();
		}
	}
}