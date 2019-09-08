## Tulur.DataMappings
Lightweight, high-performanced data mapper, based on .NET expressions. This project will be helpful if you need to convert objects of different types a lot of times. For example, conversion of data-transfer-object to business-object, and vice-versa.

This project is a fork of [FsMapper](https://github.com/FSou1/FsMapper). FsMapper is a good implementation of a perfect idea - the system generates special functions of conversions. These functions compile dynamically only once and save in memory. The system uses these functions when it is necessary. In this case mapping process works without using Reflection. As a result, mapping works very fast.

### New futures 
1.	Main feature: you can create custom rules for your mappings. Because by default, the system converts only properties with equal names / types. But in "Real world applications" it is necessary to have a possibility to use custom rules for your mappings. 
2.	Checking existence of a default constructor for result type was moved from "Run-time" to "Compile-time". So, if result type does not have a default constructor, application will not be compiled.
3.	Checking existence of a mapping-function before using was added.
4.	Parameter "capacity" for mapping rules was added. It is common feature for .NET collections. It will be helpful if you exactly know number of your kinds of mappings.
5.	I had to completely refactor of FsMapper, because I had a lot of ideas to improve it. As a result I had manage to make code shorter and increase performance! I happy about this because FsMapper is fastest mapper which I saw. It was fastest mapper :-)

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
If you run benchmark (project "Tulur.DataMappings.Benchmark"), you can compare performance of Tulur.DataMapper and FsMapper. Conditions: 20 registered mapping functions, 4 * 10 millions calls of mapping functions.

Conclusions: if you use DEBUG mode Tulur.DataMapper 15% approx faster. But if you use RELEASE mode, superiority is almost imperceptible. In most cases the mapper that is started secondary - wins (reason: "first code" always works slower, it is special behavior of CLR). If I understand this results correctly, CLR optimizes code of FsMapeer dynamically something like Tulur.DataMapper was optimized. In general, I'am satisfied with this performance comparison, because my fork of FsMapper works not slower original (maybe a bit faster), but my fork has several new features. My code optimizations completely covered loading of new features.

### Remarks
1. The solution was created in VS2015 / .NET v4.6.1. Original version of FsMapper was created for .NET Core. So, benchmark uses the version of source code FsMapper adapted for .NET v4.6.1.
2. I purposefully declined the idea with lambda-style of custom mapping rules ([Automapper]( https://github.com/AutoMapper/AutoMapper) style). Lambda-style is beauty for simple converting, but in "Real world applications" it is a big problem, I think. Because it is more difficult to debug/diagnostics, and using it in "multi-steps" code. For example, try to write `MappingRules.CreateMap()` method from example project in lambda-style :-)
I really like lambda-style, but not for tasks like this.


### Donation
If my project help you, you can support my motivation to continue working on this project :-)

Webmoney: Z410376614329 or R181376873839

Yandex.Money: 410012007533568

Ethereum: 0xFcaD676Dc74ea60c2fF9fb623ff7903AC898a32d
