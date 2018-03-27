using System;

namespace Tulur.DataMapping.Example
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
}