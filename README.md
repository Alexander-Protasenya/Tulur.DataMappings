## Tulur.DataMappings
Lightweight, high-performanced data mapper, based on .NET expressions. This project will be helpful if you need to convert objects of different types a lot of times. For example, conversion of data-transfer-object to business-object, and vice-versa.

This project is a fork of [FsMapper](https://github.com/FSou1/FsMapper). FsMapper is a good implementation of a perfect idea - the system generates special functions of conversions. These functions compile dynamically only once and save in memory. The system uses these functions when it is necessary. In this case mapping process works without using Reflection. As a result, mapping works very fast.

### New futures 
1.	Main feature: you can create custom rules for your mappings. Because by default, the system converts only properties with equal names / types. But in "Real world applications" it is necessary to have a possibility to use custom rules for your mappings. 
2.	Checking existence of a default constructor for result type was moved from "Run-time" to "Compile-time". So, if result type does not have a default constructor, application will not be compiled.
3.	Checking existence of a mapping-function before using was added.
4.	Parameter "capacity" for mapping rules was added. It is common feature for .NET collections. It will be helpful if you exactly know number of your kinds of mappings.

### Usage

This library is available on NuGet:

`PM> Install-Package Tulur.DataMappings`

Simple example:

```C#
// Initialization of DataMapper
DataMapper mapper = new DataMapper(); 

// Default mapping registration: TypeA -> TypeB
mapper.Register<TypeA, TypeB>();

// Mapping
TypeB objB = mapper.Map<TypeA, TypeB>(objA);
```

If you need to use custom mapping rules, you can use the following ways:

```C#
mapper.Register<TypeA, TypeB>((from, to) =>
{
    to.Name = from.Title;
});
```

Or

```C#
// CreateMap() is method of class MappingRules
mapper.Register<TypeA, TypeB>(MappingRules.CreateMap);
```

The last variant is more preferable if you use a lot of mapping rules. In this case you can create separate class (or several classes) for storing all mapping rules.

Custom mapping rule is an ordinary method with signature `void MethodName(TypeA source, TypeB dest)`.

### Performance
I had to completely refactor of FsMapper, because I had a lot of ideas to improve it. As a result I had manage to make code shorter and increase performance! I happy about it because FsMapper is fastest mapper which I saw. It was fastest mapper :-)

Original benchmark project from FsMapper is used. But version of .NET Core was updated to stable latest version; All NuGet dependencies were updated to latest. An updated benchmark is exist in this source code.

``` ini

.NET Core SDK=2.2.108
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
```
|                 Method |         Mean |     Error |    StdDev |
|----------------------- |-------------:|----------:|----------:|
|          CtorBenchmark |     4.133 ns | 0.0401 ns | 0.0356 ns |
|      FsMapperBenchmark |    43.375 ns | 0.1930 ns | 0.1805 ns |
|    **DataMapperBenchmark** |    **37.639 ns** | **0.0782 ns** | **0.0731 ns** |
|    AutoMapperBenchmark |   113.011 ns | 0.5304 ns | 0.4429 ns |
| ExpressMapperBenchmark |   139.935 ns | 2.8134 ns | 2.6316 ns |
|       MapsterBenchmark |    54.562 ns | 0.1719 ns | 0.1435 ns |
|   AgileMapperBenchmark |   169.747 ns | 0.3111 ns | 0.2758 ns |
| ValueInjecterBenchmark | 1,600.073 ns | 3.0689 ns | 2.8706 ns |

### Remarks
I purposefully declined the idea with lambda-style of custom mapping rules ([Automapper]( https://github.com/AutoMapper/AutoMapper) style). Lambda-style is beauty for simple mapping, but in "Real world applications" it is a big problem, I think. Because it is more difficult to debug/diagnostics, and using it in complex code. If you don't believe me, you can try to write `CreateMap()` method from test project in lambda-style :-)

I really like lambda-style, but not for tasks like this.


### Donation
If my project help you, you can support my motivation to continue working on this project :-)

Webmoney: Z410376614329 or R181376873839

Yandex.Money: 410012007533568

Ethereum: 0xFcaD676Dc74ea60c2fF9fb623ff7903AC898a32d
