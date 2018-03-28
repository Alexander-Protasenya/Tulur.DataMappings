using System;

namespace Tulur.DataMappings.Example
{
	public static class MappingRules
	{
		/// <summary>
		/// Example of complex mapping rule
		/// </summary>
		public static void CreateMap(TypeA source, TypeB dest)
		{
			dest.Name = source.Title;
			dest.Timestamp = source.Timestamp.ToUniversalTime();

			if ((string.IsNullOrEmpty(source.Title)) || (string.IsNullOrEmpty(source.Description)))
			{
				dest.Status = Status.NotValid;
			}
			else
			{
				if (source.IsNew)
				{
					dest.Status = Status.IsNew;
				}
				else if (source.IsArchive)
				{
					dest.Status = Status.Old;
				}
				else
				{
					dest.Status = Status.Ready;
				}
			}
		}
	}
}