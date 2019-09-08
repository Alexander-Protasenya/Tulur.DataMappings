using System;

namespace Tulur.DataMappings.Tests
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

	public class TypeB // Complete copy of TypeA
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string[] Tags { get; set; }
		public DateTime Created { get; set; }
		public DateTime Timestamp { get; set; }
		public bool IsNew { get; set; }
		public bool IsArchive { get; set; }
	}

	public class TypeC
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string[] Tags { get; set; }
		public DateTime Timestamp { get; set; }
		public Status Status { get; set; }
	}

	public enum Status
	{
		IsNew,
		NotValid,
		Ready,
		Old
	}
}