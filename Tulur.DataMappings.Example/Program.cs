using System;

namespace Tulur.DataMappings.Example
{
	public class Program
	{
		public static void Main(string[] args)
		{
			TypeA objA = new TypeA
			{
				Title = "My title",
				Description = "My description",
				Tags = new[] {"Tag1", "Tag2", "Tag3"},
				Timestamp = DateTime.Now,
				IsArchive = true
			};

			DataMapper mapper = new DataMapper();

			// Default mapping
			mapper.Register<TypeA, TypeB>();

			// If it is necessary to add specific mapping rules
			//mapper.Register<TypeA, TypeB>((from, to) =>
			//{
			//	to.Name = from.Title;
			//});

			// If it is necessary to add specific mapping rules from separate "profile" class, sometimes it is very useful,
			// for example if you have a lot of specific mapping rules
			//mapper.Register<TypeA, TypeB>(MappingRules.CreateMap);

			TypeB result = mapper.Map<TypeA, TypeB>(objA);

			Console.WriteLine(result.Name);
			Console.WriteLine(result.Description);
			Console.WriteLine(string.Join(", ", result.Tags));
			Console.WriteLine(result.Status);
			Console.WriteLine(result.Timestamp);
			Console.ReadLine();
		}
	}
}