using System;

namespace Tulur.DataMapping.Benchmark
{
	public class TypeA
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public string[] Tags { get; set; }

		public DateTime Created { get; set; }

		public DateTime Timestamp { get; set; }

		public bool IsNew { get; set; }

		public bool IsArchive { get; set; }
	}

	public class TypeB
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string[] Tags { get; set; }

		public DateTime Timestamp { get; set; }
	}

	public class TypeC
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public DateTime CreatedAtUtc { get; set; }

		public bool IsDeleted { get; set; }
	}

	public class TypeD
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public DateTime CreatedAtUtc { get; set; }

		public bool IsDeleted { get; set; }
	}
}