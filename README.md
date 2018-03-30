## Tulur.DataMappings
Lightweight high-performanced data mapper, based on .NET expressions. This project will be helpful if you need to convert objects of different types a lot of times. For example, conversion DTO-object to business-object, and vice-versa.

This project is fork of [FsMapper](https://github.com/FSou1/FsMapper). FsMapper is good implementation of perfect idea - before mapping, system generate special function of conversion, this function compile dynamically and save in memory, when system should convert object, system use this function. In this case mapping process works without using Reflection. As a result, conversion works very fast.

I decided to improve this project, but in this case I had to do complete refactoring FsMapper, as a result I decided to create my fork of FsMapper.

### New futures 
1.	Main feature: you can create custom rules for your mappings. Because by default, system convert only properties with equal names / types. But in "Real world application" it is necessary to have possibility to use custom rules for your mappings. 
2.	Checking existence default constructor for result type was moved from "Run-time" to "Compile-time". So, if your result type does not have default constructor, application will not compile.
3.	Checking existence of convert-function before using was added.
4.	Parameter "capacity" of registrations was added. Something like this used for .NET collections.  It will helpful if you exactly know amount of your kinds of mappings.
5.	A lot of minor performance improvements / fixes.

### Usage
Here is a simple example of using:

```C#
// Initialization of DataMapper
DataMapper mapper = new DataMapper(); 

// Default mapping registration: TypeA -> TypeB
mapper.Register<TypeA, TypeB>();

// Mapping
TypeB objB = mapper.Map<TypeA, TypeB>(objA);
```

If you need to use custom mapping rules, you can use following ways:

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

Last variant is more preferable if you use a lot of mapping rules. In this case you can use separate class (or several classes) for storing all mapping rules.

Custom mapping rule is ordinary method with signature `void MethodName(TypeA source, TypeB dest)`. This method will run after normal mapping.

### Performance
If you run benchmark (project "Tulur.DataMappings.Benchmark"), you can compare performance of Tulur.DataMapper and FsMapper. Conditions: 20 registered mapping functions, 4 * 10 millions calls of mapping functions.

Conclusions: if you use DEBUG mode Tulur.DataMapper faster 15% approx, but if you use RELEASE mode, superiority is almost imperceptible. Most cases what mapper started secondary - that mapper will win (reason: "first code" always works slower, special behavior of CLR). If I understand correctly this results, CLR optimize code of FsMapeer dynamically something like was optimized Tulur.DataMapper. In general, I satisfied about this performance comparison, because my fork of FsMapper works not slower original (maybe little faster), but my fork has several new features. My code optimizations complete cover loading of new features.

### Remarks
1. Solution was created in VS2015 / .NET v4.6.1. Original version of FsMapper was created only for .NET Core. So, for benchmark used version of source code FsMapper adapted for .NET v4.6.1.
2. I purposefully declined idea with lambda-style of custom mapping rules ([Automapper]( https://github.com/AutoMapper/AutoMapper) style). lambda-style is beauty for simple conversions, but in "Real world application" it is big problem, I think. Because it is more difficult to debug/diagnostics, more difficult to use multi-steps code. For example, try to write `MappingRules.CreateMap()` method from example project in lambda-style :-)
I really like lambda-style, but not for tasks like this.


### Donation
If my project help you, you can support my motivation to continue working on this project :-)

Webmoney: Z410376614329 or R181376873839

Yandex.Money: 410012007533568

Ethereum: 0xFcaD676Dc74ea60c2fF9fb623ff7903AC898a32d
