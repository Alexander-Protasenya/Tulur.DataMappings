using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tulur.DataMappings.Tests
{
	[TestClass]
	public class MainTests
	{
		[TestMethod]
		public void DefaultMappingTest()
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

			mapper.Register<TypeA, TypeB>();

			TypeB objB = mapper.Map<TypeA, TypeB>(objA);

			Assert.AreEqual(objA.Title, objB.Title);
			Assert.AreEqual(objA.Description, objB.Description);
			Assert.AreEqual(objA.Tags.Length, objB.Tags.Length);
			Assert.AreEqual(objA.Timestamp, objB.Timestamp);
			Assert.AreEqual(objA.IsArchive, objB.IsArchive);
		}

		[TestMethod]
		public void CustomMappingTest()
		{
			TypeA objA = new TypeA
			{
				Title = "My title",
				Description = "My description",
				Timestamp = DateTime.Now,
				IsArchive = true
			};

			DataMapper mapper = new DataMapper();

			mapper.Register<TypeA, TypeC>(CreateMap);

			TypeC objC = mapper.Map<TypeA, TypeC>(objA);

			Assert.AreEqual(objA.Title, objC.Name);
			Assert.AreEqual(objA.Description, objC.Description);
			Assert.AreEqual(objA.Timestamp.ToUniversalTime(), objC.Timestamp);
			Assert.AreEqual(Status.Old, objC.Status);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void NotFoundMappingTest()
		{
			TypeA objA = new TypeA
			{
				Title = "My title",
				Description = "My description",
				Timestamp = DateTime.Now,
				IsArchive = true
			};

			DataMapper mapper = new DataMapper();

			mapper.Register<TypeB, TypeC>();

			mapper.Map<TypeA, TypeC>(objA);
		}

		private static void CreateMap(TypeA source, TypeC dest)
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