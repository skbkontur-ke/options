# Yet another Option type implementation

## Content

* [License](#license)
* [Installation](#installation)
* [Features](#features)
* [Drawbacks](#drawbacks)
* [Instantiation of Option type](#instantiation-of-option-type)
* [Extraction of data from Option instance](#extraction-of-data-from-option-instance)
    * [TryGet](#tryget)
    * [Match](#match)
    * [Switch](#switch)
    * [OnSome](#onsome)
    * [OnNone](#onnone)
    * [Switch/OnSome/OnNone methods chaining](#switchonsomeonnone-methods-chaining)
    * [Switch/OnSome/OnNone return value upcasting](#switchonsomeonnone-return-value-upcasting)
    * [GetOrElse](#getorelse)
    * [GetOrThrow](#getorthrow)
    * [GetOrDefault](#getordefault)
    * [EnsureHasValue](#ensurehasvalue)
    * [EnsureNone](#ensurenone)
    * [HasSome](#hassome)
    * [IsNone](#isnone)
    * [Implicit conversion to bool](#implicit-conversion-to-bool)
    * [Conversion to IEnumerable](#conversion-to-ienumerable)
    * [LINQ method syntax](#linq-method-syntax)
    * [LINQ query sytnax](#linq-query-sytnax)
    * [foreach](#foreach)
    * [ToString](#tostring)
* [Conversion of `Option<TValue>` to another Option type](#conversion-of-optiontvalue-to-another-option-type)
    * [Map](#map)
    * [And](#and)
    * [Or](#or)
    * [Upcast](#upcast)
    * [Select](#select)
    * [Do-notation without tasks](#do-notation-without-tasks)
    * [Do-notation with tasks](#do-notation-with-tasks)
* [Other](#other)
* [Contributing](#contributing)

## License

MIT

## Installation

Use [cement](https://github.com/skbkontur/cement#get-started) to add reference to `Option` type assembly.

Execute that command in your cement module:
`cm ref add ke-options your-csproj.csproj`

If you are targeting .NET Framework 4.8 or lower, replace reference `..\ke-options\bin\Kontur.Options\netstandard2.1\Kontur.Options.dll` with reference `..\ke-options\bin\Kontur.Options\netstandard2.0\Kontur.Options.dll`.

## Features

* [Unrestricted do-notation with `Task<T>` support](#do-notation-with-tasks)
* Great interface that allow checking and extraction of data with single method. See [TryGet](#tryget) and [Match](#match).
* `And` (`Then`, `ContinueOnSome`) and `Or` (`Else`, `ContinueOnNone`) combinators `Option` type. See [And](#and) for `And` combinator and [Or](#or) for `Or` combinator.
* Assembly contains only `Option` type implementation. There are no other stuff.
* `Option` type implementation is if-less and makes use of abstract classes polymorphism and VMT to maintain error-safety and simplifity. As a result there is no null-forgiving operator. Also there is no ternary operators that check `HasSome` flag or similar staff.
* There is no specific handling of nulls. Use C# 8 nullable reference types to handle nulls.

## Drawbacks

* Implementation is based on abstract classes polymorphism. So `Option` type is not marked `readonly` (but it is implemented as readonly) and is not `struct`.


## Instantiation of `Option` type

Explicit variants:
```
var option = Option.Some("hello");
var option = Option<string>.Some("hello");
var option = Option.Some<string>("hello");
```

```
var option = Option.None<string>();
var option = Option<string>.None();
```

Implicit variants:
```
Option<string> option = "hello";
Option<string> option = Option.None();
```

```
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

```
Option<int> GetResult(Random random)
{
  int randomValue = random.Next(0, 10);
  if (randomValue > 10)
  {
    return randomValue;
  }

  return Option.None();
}
```


## Extraction of data from `Option` instance

### TryGet
```
Option<string> option = ...;

if (option.TryGet(out var value))
{
  // value is not null here
}

// value may be null here
```

### Match
```
Option<int> option = ...;

string result = option.Match(onNone: () => "valueOnNone", onSome: i => $"Number {i}");
string result = option.Match(onNone: () => "valueOnNone", onSome: () => "Number is present");
string result = option.Match(onNone: () => "valueOnNone", onSomeValue: "Number is present");
string result = option.Match(onNoneValue: "valueOnNone", onSome: i => $"Number {i}");
string result = option.Match(onNoneValue: "valueOnNone", onSome: () => "Number is present");
string result = option.Match(onNoneValue: "valueOnNone", onSomeValue: "Number is present");

object upcasted = option.Match(onNone: () => new object(), onSome: i => $"Number {i}");
object upcasted = option.Match(onNone: () => new object(), onSome: () => "Number is present");
object upcasted = option.Match(onNone: () => new object(), onSomeValue: "Number is present");
object upcasted = option.Match(onNoneValue: new object(), onSome: i => $"Number {i}");
object upcasted = option.Match(onNoneValue: new object(), onSome: () => "Number is present");
object upcasted = option.Match(onNoneValue: new object(), onSomeValue: "Number is present");

object upcasted = option.Match(onNone: () => "valueOnNone", onSome: _ => new object());
object upcasted = option.Match(onNone: () => "valueOnNone", onSome: () => new object());
object upcasted = option.Match(onNone: () => "valueOnNone", onSomeValue: new object());
object upcasted = option.Match(onNoneValue: "valueOnNone", onSome: _ => new object());
object upcasted = option.Match(onNoneValue: "valueOnNone", onSome: () => new object());
object upcasted = option.Match(onNoneValue: "valueOnNone", onSomeValue: new object());

object upcasted = option.Match<object>(onNone: () => new Exception("There is no value"), onSome: i => $"Number {i}");
object upcasted = option.Match<object>(onNone: () => new Exception("There is no value"), onSome: () => "Number is present");
object upcasted = option.Match<object>(onNone: () => new Exception("There is no value"), onSomeValue: "Number is present");
object upcasted = option.Match<object>(onNoneValue: new Exception("There is no value"), onSome: i => $"Number {i}");
object upcasted = option.Match<object>(onNoneValue: new Exception("There is no value"), onSome: () => "Number is present");
object upcasted = option.Match<object>(onNoneValue: new Exception("There is no value"), onSomeValue: "Number is present");
```

### Switch
```
Option<int> option = ...;

option.Switch(
  onNone: () => Console.WriteLine("There is no value"),
  onSome: value => Console.WriteLine($"Value is {value}")
);

option.Switch(
  onNone: () => Console.WriteLine("There is no value"),
  onSome: () => Console.WriteLine("Value is present")
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
    onNone: () => Console.WriteLine("There is no value"),
    onSome: value => Console.WriteLine($"Value is {value}"))
  .OnSome(value => log.Info($"Value is {value}"))
  .OnNone(() => log.Info("There is no value"))
  .Match(onSome: value => value.ToString(), onNoneValue: "valueOnNone");
```

### Switch/OnSome/OnNone return value upcasting

```
Option<string> option = ...;

Option<object> upcasted = option.Switch<object>(
  onNone: () => Console.WriteLine("There is no value"),
  onSome: value => Console.WriteLine($"Value is {value}")
);
Option<object> upcasted = option.Switch<object>(
  onNone: () => Console.WriteLine("There is no value"),
  onSome: () => Console.WriteLine("Value is present")
);

Option<object> upcasted = option.OnSome<object>(value => Console.WriteLine($"Value is {value}"));
Option<object> upcasted = option.OnSome<object>(() => Console.WriteLine("Value is present"));

Option<object> upcasted = option.OnNone<object>(() => Console.WriteLine("There is no value"));
```

### GetOrElse
```
Option<string> option = ...;

string result = option.GetOrElse(() => "defaultValue");
string result = option.GetOrElse("defaultValue");

object upcasted = option.GetOrElse(() => new object());
object upcasted = option.GetOrElse(new object());

object upcasted = option.GetOrElse<object>(() => new Exception("There is no value"));
object upcasted = option.GetOrElse<object>(new Exception("There is no value"));

Option<object> objectOption = ...;
object upcasted = objectOption.GetOrElse(() => "defaultValue");
object upcasted = objectOption.GetOrElse("defaultValue");
```

### GetOrThrow
```
Option<string> option = ...;

string result = option.GetOrThrow(); // Throws `ValueMissingException` on None
string result = option.GetOrThrow(new Exception("There is no value"));
string result = option.GetOrThrow(() => new Exception("There is no value"));

object upcasted = option.GetOrThrow<object>();
object upcasted = option.GetOrThrow<object>(new Exception("There is no value"));
object upcasted = option.GetOrThrow<object>(() => new Exception("There is no value"));
```

To override default thrown exception for specific `TValue` you can implement extentsion method with specific `TValue` in namespace of specific `TValue`.
```
namespace Custom
{
  class CustomValue
  {
  }
  static class GetOrThrowExtensions
  {
    static CustomValue GetOrThrow(this Option<CustomValue> option)
    {
      return option.GetOrThrow(new Exception("Overiden!"));
    }
  }
}
```

### GetOrDefault
```
Option<string> option = ...;

string? result = option.GetOrDefault();

object? upcasted = option.GetOrDefault<object>();
```

### EnsureHasValue
```
Option<string> option = ...;

// Nothing if `Some`
option.EnsureHasValue(); // Throws `ValueMissingException` on None
option.EnsureHasValue(new Exception("There is no value"));
option.EnsureHasValue(() => new Exception("There is no value"))
```

To override default thrown exception for specific `TValue` you can implement extentsion method with specific `TValue` in namespace of specific `TValue`.
```
namespace Custom
{
  class CustomValue
  {
  }
  static class EnsureHasValueExtensions
  {
    static void EnsureHasValue(this Option<CustomValue> option)
    {
      return option.EnsureHasValue(new Exception("Overiden!"));
    }
  }
}
```

### EnsureNone
```
Option<string> option = ...;

// Nothing if `None`
option.EnsureNone(); // Throws `ValueExistsException` if `Some`
option.EnsureNone(new Exception("There is value"));
option.EnsureNone(() => new Exception("There is value"))
option.EnsureNone(value => new Exception($"There is value: {value}"))
```

To override default thrown exception for specific `TValue` you can implement extentsion method with specific `TValue` in namespace of specific `TValue`.
```
namespace Custom
{
  class CustomValue
  {
  }
  static class EnsureNoneExtensions
  {
    static void EnsureNone(this Option<CustomValue> option)
    {
      return option.EnsureNone(new Exception("Overiden!"));
    }
  }
}
```

### HasSome

```
Option<string> option = "has value";

// true
bool result = option.HasSome;
```

```
Option<string> option = Option.None();

// false
bool result = option.HasSome;
```

### IsNone

```
Option<string> option = "has value";

// false
bool result = option.IsNone;
```

```
Option<string> option = Option.None();

// true
bool result = option.IsNone;
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

bool result = option;
```

### Conversion to IEnumerable
```
Option<string> option = ...;

// `[]` if `None`
// `[value]` if `Some`
IEnumerable<string> values = option.GetValues();

IEnumerable<object> upcasted = option.GetValues<object>();
```

### LINQ method syntax
```
Option<string> option = ...;

// `[]` if `None`
// `[value]` if `Some`
string[] values = option.GetValues().ToArray();
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

### foreach
```
string FindValue(Option<int> option)
{
  foreach(var value in option)
  {
    return value + " is found!";
  }

  return "no value found";
}

```

### ToString
```
Option<string> option = ...;

// `None<string>` if `None`
// `Some<string> value={Value}` if `Some`
string str = option.ToString();
```


## Conversion of `Option<TValue>` to another Option type

### Map

`Map` changes value or/and type of `TValue`

```
Option<string> option = ...;

Option<string> result = option.Map(str => str + "suffix");
Option<string> result = option.Map(() => 1.ToString());
Option<string> result = option.Map("other string");

Option<int> result = option.Map(str => int.Parse(str));
Option<int> result = option.Map(() => int.Parse("123"));
Option<int> result = option.Map(15);

Option<object> upcasted = option.Map<object>(str => str);
Option<object> upcasted = option.Map<object>(() => 1.ToString());
Option<object> upcasted = option.Map<object>("other string");
```

### And

`And` is `And` (`Then`/`ContinueOnSome`) combinator.
If a first option is `None` then the `None` is returned.
If the first option is `Some` then a second option is returned.
The second `Option` factory method is only executed if the first `Option` is `Some`.

```
Option<int> option1 = ...;
Option<string> option2 = ...;

// If option1 is None then None is returned.
// Otherwise option2 is returned.
Option<string> result = option1.And(option2);
Option<string> result = option1.And(() => option2);
Option<string> result = option1.And(i => i > 10 Option<string>.Some(i.ToString()) : Option<string>.None());
```

Variants with upcast:
```
Option<int> option1 = ...;
Option<string> option2 = ...;

Option<object> upcasted = option1.And<object>(option2);
Option<object> upcasted = option1.And<object>(() => option2);
Option<object> upcasted = option1.And<object>(i => option2);
```

### Or

`Or` is `Or` (`Else`, `ContinueOnNone`) combinator.
If a first option is `Some` then the it is returned.
If the first option is `None` then a second option is returned.
The second `Option` factory method is only executed if the first `Option` is `None`.

```
Option<string> option1 = ...;
Option<string> option2 = ...;

// If option1 is Some then option1 is returned.
// Otherwise option2 is returned.
Option<string> result = option1.Or(option2); 
Option<string> result = option1.Or(() => option2);
```

Variants with upcast:
```
Option<string> option1 = ...;
Option<object> option2 = ...;

Option<object> upcasted = option1.Or(option2);
Option<object> upcasted = option1.Or(() => option2);
```

```
Option<object> option1 = ...;
Option<string> option2 = ...;

Option<object> upcasted = option1.Or(option2);
Option<object> upcasted = option1.Or(() => option2);
```

```
Option<string> option1 = ...;
Option<string> option2 = ...;

Option<object> upcasted = option1.Or<object>(option2);
Option<object> upcasted = option1.Or<object>(() => option2);
```

### Upcast

Only safe upcasts are allowed. That rule applies to all other methods which support upcasts.

```
Option<string> option = ...;

// Compiles
Option<object> objectOption = option.Upcast<object>();

// Does not compile
var result = objectOption.Upcast<string>();

```

### Select

`Select` is a mix of `Map` and `And`.
Like both of them, a factory method of a second `Option` or value is only executed if the first `Option` is `Some`.
Like `Map` it allows changing type and value by using the value factory.
Unlike `Map` and like `And` it allows creating a second `Option` which is a result of the whole operation.
It also allows you to change `Some` to `None` for some cases.

```
Option<string> option = ...;

Option<string> result = option.Select(str => str.Length > 5 ? Option.Some(str) : Option.None());
Option<string> result = option.Select(str => str + "suffix");
Option<int> result = option.Select(str => int.Parse(str));

Option<object> upcasted = option.Select<object>(str => str);
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
abstract Task<bool> IsValid(int index);

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

If a new method can return `GenericType<TValue>` or just `TValue` for some `Option<TValue>` or base type of `TValue` and has no other generic parameters, then the new method should be added as an extension method. In that case, the new method should receive a parameter of type `IOptionMatchable<TValue>` instead of `Option<TValue>`. That allows simple and safe upcasts by providing additional arguments or specifying one generic argument at a call site of the new method.
If the new method has exactly one extra generic parameter in addition to `TValue` of `Option<TValue>`, then the new method should be added directly into `Option.TValue.cs`. That also allows simple and safe upcasts by specifying one generic argument at a call site of the new method.
Please, add all other methods as extension methods.
