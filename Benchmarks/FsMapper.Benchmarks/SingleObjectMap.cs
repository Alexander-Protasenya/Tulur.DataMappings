using System;
using AutoMapper;
using BenchmarkDotNet.Attributes;
using FsMapper.Benchmarks.Models;
using Mapster;

namespace FsMapper.Benchmarks
{
	public class SingleObjectMappingFunc
	{
		private FsMapper.Mapper _fsMapper;
		private Tulur.DataMappings.DataMapper _dataMapper;
		private AutoMapper.Mapper _autoMapper;

		private CustomerDto _dto;

		[GlobalSetup]
		public void GlobalSetup()
		{
			_dto = GetCustomerDto();

			_fsMapper = ConfigureFsMapper();
			_dataMapper = ConfigureTulurDataMapper();
			_autoMapper = ConfigureAutoMapper();

			ConfigureExpressMapper();
			ConfigureAutoMapper();
		}

		[Benchmark]
		public Customer CtorBenchmark() => new Customer
		{
			Id = _dto.Id,
			CreatedAtUtc = _dto.CreatedAtUtc,
			IsDeleted = _dto.IsDeleted,
			Title = _dto.Title
		};

		[Benchmark]
		public Customer FsMapperBenchmark() => _fsMapper.Map<CustomerDto, Customer>(_dto);

		[Benchmark]
		public Customer DataMapperBenchmark() => _dataMapper.Map<CustomerDto, Customer>(_dto);

		[Benchmark]
		public Customer AutoMapperBenchmark() => _autoMapper.Map<CustomerDto, Customer>(_dto);

		[Benchmark]
		public Customer ExpressMapperBenchmark() => ExpressMapper.Mapper.Map<CustomerDto, Customer>(_dto);

		[Benchmark]
		public Customer MapsterBenchmark() => _dto.Adapt<Customer>();

		[Benchmark]
		public Customer AgileMapperBenchmark() => AgileObjects.AgileMapper.Mapper.Map(_dto).ToANew<Customer>();

		[Benchmark]
		public Customer ValueInjecterBenchmark() => Omu.ValueInjecter.Mapper.Map<Customer>(_dto);


		#region Configure

		internal FsMapper.Mapper ConfigureFsMapper()
		{
			FsMapper.Mapper mapper = new FsMapper.Mapper();
			mapper.Register<CustomerDto, Customer>();
			return mapper;
		}

		internal Tulur.DataMappings.DataMapper ConfigureTulurDataMapper()
		{
			Tulur.DataMappings.DataMapper mapper = new Tulur.DataMappings.DataMapper();
			mapper.Register<CustomerDto, Customer>();
			return mapper;
		}

		internal AutoMapper.Mapper ConfigureAutoMapper()
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<CustomerDto, Customer>();
			});

			AutoMapper.Mapper mapper = new AutoMapper.Mapper(config);
			return mapper;
		}

		internal void ConfigureExpressMapper()
		{
			ExpressMapper.Mapper.Register<CustomerDto, Customer>();
		}

		#endregion

		#region DTO

		internal CustomerDto GetCustomerDto() => new CustomerDto
		{
			Id = 42,
			Title = "Test",
			CreatedAtUtc = new DateTime(2017, 9, 3),
			IsDeleted = true
		};

		#endregion
	}
}