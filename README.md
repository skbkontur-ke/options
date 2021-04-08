# Yet another Option type implementation

## Content

* [License](#license)
* [Using option type](#using-option-type)
* [Features](#features)
    * [Killer-feature: Unrestricted do-notation with `Task<T>` support](#killer-feature-unrestricted-do-notation-with-taskt-support)
    * [Other features](#other-features)
    * [Drawbacks](#drawbacks)
* [Instantiation of Option type](#instantiation-of-option-type)
* [Safe extraction of data from Option instance](#safe-extraction-of-data-from-option-instance)
    * [GetOrElse](#getorelse)
    * [TryGet](#tryget)
    * [Match](#match)
    * [Switch](#switch)
    * [OnSome](#onsome)
    * [OnNone](#onnone)
    * [Switch/OnSome/OnNone methods chaining](#switchonsomeonnone-methods-chaining)
    * [HasSome](#hassome)
    * [IsNone](#isnone)
    * [Implicit conversion to bool](#implicit-conversion-to-bool)
    * [foreach](#foreach)
    * [LINQ query sytnax](#linq-query-sytnax)
    * [Conversion to IEnumerable](#conversion-to-ienumerable)
    * [LINQ method syntax](#linq-method-syntax)
    * [ToString](#tostring)
* [Unsafe extraction of data from Option instance](#unsafe-extraction-of-data-from-option-instance)
    * [GetOrThrow](#getorthrow)
    * [GetOrDefault](#getordefault)
    * [EnsureHasValue](#ensurehasvalue)
    * [EnsureNone](#ensurenone)
* [Conversion of `Option<TValue>` to another Option type](#conversion-of-optiontvalue-to-another-option-type)
    * [Map](#map)
    * [Select](#select)
    * [Upcast](#upcast)
    * [Or](#or)
    * [Do-notation without tasks](#do-notation-without-tasks)
    * [Do-notation with tasks](#do-notation-with-tasks)
* [Other](#other)
* [Contributing](#contributing)

## License
MIT

## Using option type

Use [cement](https://github.com/skbkontur/cement#get-started) to add reference to Option type assembly.

Execute that command in your cement module:
`cm ref add ke-options your-csproj.csproj`

If you are targeting .NET Framework 4.8 or lower, replace reference `..\ke-options\bin\Kontur.Options\netstandard2.1\Kontur.Options.dll` with reference `..\ke-options\bin\Kontur.Options\netstandard2.0\Kontur.Options.dll`.

## Features

### Killer-feature: Unrestricted do-notation with `Task<T>` support

Example:
```
abstract Option<Guid> GetCurrentUserId();
abstract Task<int> GetCurrentIndex();
abstract Task<Option<Product>> GetCurrentProduct();
abstract Task<Option<string>> GetMessage(Guid userId, int index, Product product));
abstract Task<Format> GetFormat(int index, string message);
abstract Option<ConvertResult> Convert(string message, Format format);
abstract Task<bool> IsValid(index);

Task<Option<ConvertResult>> result =
  from userId  in GetCurrentUserId() // A
  where userId != Guid.Empty       // B
  from index   in GetCurrentIndex() // C
  from product in GetCurrentProduct() // D
  let nextIndex = index + 1
  where IsValid(nextIndex) // E
  from message in GetMessage(userId, nextIndex, product) // F
  from format  in GetFormat(nextIndex, message) // G
  select Convert(message, format); // H

```
Where:
* `A` or `C` or both (first two `from`/`in` clauses) must return either `Option<T>` or `Task<Option<T>>`.
* `A`, `C`, `D`, `F` and `G` may return one of `Option<T>`, `Task<T>` or `Task<Option<T>>`. Subsequent expressions may depend on previous expressions (`F` and `G` for example) or may not depend on previous expressions (`C` and `D` for example). Number of `B`, `C`, `D`, `E`, `F` and `G`-like statements is efficiently unlimited.
* `B` and `E` may return either `bool` or `Task<bool>`.
* `H` should return either `TResult` or `Option<TResult>`.

### Other features

* Assembly contains only Option type implementation. There are no other stuff.
* Option type implementation is if-less and makes use of abstract classes polymorphism and VMT to maintain error-safety and simplifity. So there is no null-forgiving operator. Also there is no ternary operators that check `HasSome` flag.
* There is no specific handling of nulls. Use C# 8 nullable reference types to handle nulls.

### Drawbacks

* As implementation is based on abstract classes polymorphism, Option type is not `readonly` `struct`.


## Instantiation of Option type

Explicit variants:
```
var option = Option.Some("hello");
var option = Option<string>.Some("hello");
var option = Option.Some<string>("hello");

var option = Option.None<string>();
var option = Option<string>.None();
```

Implicit variants:
```
Option<string> option = "hello";
Option<string> option = Option.None();

var option = flag
  ? Option.Some("Hello")
  : Option.None();

var option = flag
  ? "Hello"
  : Option<string>.None();

Option<string> option = flag
  ? "Hello"
  : Option.None();
```

## Safe extraction of data from Option instance

### GetOrElse
```
Option<string> option = ...;

string result = option.GetOrElse(() => "defaultValue");
string result = option.GetOrElse("defaultValue");

object result = option.GetOrElse(() => new object());
object result = option.GetOrElse<object>(new Exception("no value"));
```


### TryGet
It works for nullable and non-nullable reference and value types.

Remark: On netstandard2.0 and below for reference types it returns nullable variant of generic type if nullable reference types are enabled.

```
Option<string> option = ...;

if (option.TryGet(out var value))
{
  // value is not null here (see remark above for reference type and old framework versions)
}

// value may be null here
```


### Match
```
Option<int> option = ...;

string result = option.Match(onNone: () => "valueOnNone", onSome: str => $"Number {i}");
string result = option.Match(onNoneValue: "valueOnNone", onSome: str => $"Number {i}");

object result = option.Match<object>(onNone: () => "valueOnNone", onSome: str => $"Number {i}");
object result = option.Match<object>(onNoneValue: "valueOnNone", onSome: str => $"Number {i}");
```

### Switch
```
Option<int> option = ...;

option.Switch(
  onNone: () => Console.WriteLine("There is no value"),
  onSome: value => Console.WriteLine($"Value is {value}")
);
```

### OnSome
```
Option<int> option = ...;

option.OnSome(value => Console.WriteLine($"Value is {value}"));
option.OnSome(() => Console.WriteLine("Value is present"));
```

### OnNone
```
Option<int> option = ...;

option.OnNone(() => Console.WriteLine("There is no value"));
```

### Switch/OnSome/OnNone methods chaining
```
Option<int> option = ...;

string result = option
  .Switch(
    onNone: () => Console.WriteLine("There is no value")),
    onSome: value => Console.WriteLine($"Value is {value}")
  .OnSome(value => log.Info($"Value is {value}"))
  .OnNone(() => log.Info("There is no value"))
  .Match(onSome: value => value.ToString(), onNoneValue: "valueOnNone");
```

### HasSome

```
Option<string> option = "has value";

// true
var result = opton.HasSome;
```

```
Option<string> option = Option.None();

// false
var result = opton.HasSome;
```

### IsNone

```
Option<string> option = "has value";

// false
var result = opton.IsNone;
```

```
Option<string> option = Option.None();

// true
var result = opton.IsNone;
```

### Implicit conversion to bool
```
Option<string> option = ...;

if (option)
{
  // On some
}
else
{
  // On none
}
```

### foreach
```
string FindValue(Option<int> option)
{
  foreach(string value in option)
  {
    return value + " is found!";
  }

  return "no value found";
}

```

### LINQ query sytnax
```
Option<int> option = Option.Some(10);

IEnumerable<int> result =
  from value in option
  from i1 in new [] { 1, 2 }
  from i2 in new [] { 100, 200 }
  select value + i1 + i2;

IEnumerable<int> result =
  from i1 in new [] { 1, 2 }
  from value in option
  from i2 in new [] { 100, 200 }
  select i1 + value + i2;

// result is [111, 112, 211, 212].
```

```
Option<int> option = Option.None();

IEnumerable<int> result =
  from value in option
  from i in new [] { 1, 2 }
  select value + i;

IEnumerable<int> result =
  from i in new [] { 1, 2 }
  from value in option
  select value + i;

// result is empty.

```

### Conversion to IEnumerable
```
Option<int> option = ...;

// `[]` if `None`
// `[value]` if `Some`
IEnumerable<int> converted = option.GetValues();
```

### LINQ method syntax
```
Option<string> option = ...;

// `[]` if `None`
// `[value]` if `Some`
string[] values = option.GetValues().ToArray();
```


### ToString
```
Option<string> option = ...;

// `None<string>` if `None`
// `Some<string> value={Value}` if `Some`
string str = option.ToString();
```

## Unsafe extraction of data from Option instance

### GetOrThrow
```
Option<string> option = ...;

string result = option.GetOrThrow(); // Throws `ValueMissingException` on None
string result = option.GetOrThrow(new Exception("There is no value"));
string result = option.GetOrThrow(() => new Exception("There is no value"));
```

### GetOrDefault
It works for nullable and non-nullable reference and value types.

```
Option<string> option = ...;

string? result = option.GetOrDefault();
```

### EnsureHasValue
```
Option<string> option = ...;

// Nothing if `Some`
// Exception is thrown if `None`
option.EnsureHasValue();
```

### EnsureNone
```
Option<string> option = ...;

// Nothing if `None`
// Exception is thrown if `Some`
option.EnsureNone();
```

## Conversion of `Option<TValue>` to another Option type

### Map

Map changes value or/and type of `TValue`s

```
Option<int> option = ...;

Option<int> result = option.Map(i => i + 1);
Option<string> result = option.Map(i => i.ToString());
Option<object> result = option.Map<object>(i => i);
```

### Select

`Select` is same as map but allows selecting an `Option` in addition to selecting a plain value.
That even allows to change `Some` to `None` for some values.

```
Option<int> option = ...;

Option<string> result = option.Select(i => i > 0 ? Option.Some(i) : Option.None());
Option<int> result = option.Select(i => i + 1);
Option<string> result = option.Select(i => i.ToString());
Option<object> result = option.Select<object>(i => i);
```

### Upcast

Only safe upcasts are allowed.

```
Option<string> option = ...;

// Compiles
Option<object> objectOption = option.Upcast<object>();

// Does not compile
var result = objectOption.Upcast<string>();
```

### Or
```
Option<string> option1 = ...;
Option<string> option2 = ...;

// If option1 is Some then option1 is returned.
// Otherwise option2 is returned.
Option<string> result = option.Or(option2); 
Option<string> result = option.Or(() => option2); 
```

Variants with upcast:
```
Option<string> option1 = ...;
Option<object> option2 = ...;

// If option1 is Some then option1 is returned.
// Otherwise option2 is returned.
Option<object> result = option.Or(option2); 
Option<object> result = option.Or(() => option2); 
```


### Do-notation without tasks
```
abstract Option<Guid> GetCurrentUserId();
abstract Option<int> GetCurrentIndex();
abstract Option<Product> GetCurrentProduct();
abstract Option<string> GetMessage(Guid userId, int index, Product product);
abstract Option<ConvertResult> Convert(string message, Product product);

Option<ConvertResult> result =
  from userId  in GetCurrentUserId()
  where userId != Guid.Empty
  from index   in GetCurrentIndex()
  from product in GetCurrentProduct()
  let nextIndex = index + 1
  from message in GetMessage(userId, nextIndex, product)
  select Convert(message, product);

```
The last `select` expression can return either `Option<TResult>` or just `TResult`.


### Do-notation with tasks
```
abstract Option<Guid> GetCurrentUserId();
abstract Task<int> GetCurrentIndex();
abstract Task<Option<Product>> GetCurrentProduct();
abstract Task<Option<string>> GetMessage(Guid userId, int index, Product product));
abstract Task<Format> GetFormat(int index, string message);
abstract Option<ConvertResult> Convert(string message, Format format);
abstract Task<bool> IsValid(index);

Task<Option<ConvertResult>> result =
  from userId  in GetCurrentUserId() // A
  where userId != Guid.Empty       // B
  from index   in GetCurrentIndex() // C
  from product in GetCurrentProduct() // D
  let nextIndex = index + 1
  where IsValid(nextIndex) // E
  from message in GetMessage(userId, nextIndex, product) // F
  from format  in GetFormat(nextIndex, message) // G
  select Convert(message, format); // H

```
Where:
* `A` or `C` or both (first two `from`/`in` clauses) must return either `Option<T>` or `Task<Option<T>>`.
* `A`, `C`, `D`, `F` and `G` may return one of `Option<T>`, `Task<T>` or `Task<Option<T>>`. Subsequent expressions may depend on previous expressions (`F` and `G` for example) or may not depend on previous expressions (`C` and `D` for example). Number of `B`, `C`, `D`, `E`, `F` and `G`-like statements is efficiently unlimited.
* `B` and `E` may return either `bool` or `Task<bool>`.
* `H` should return either `TResult` or `Option<TResult>`.


## Other

* `Equals` and `GetHahsCode` make use of Type argument and value (if present) for calculations.
* There is no `fold` method.


## Contributing

Add safe methods with one generic parameter used as return value directly into `Option.TValue.cs` file instead adding as extension method for simplier upcasts by specifing only one generic argument.
