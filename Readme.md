# Rtl.Configuration.FluentValidation
Rtl.Configuration.FluentValidation is based on package *Rtl.Configuration.Validation* and extends it by providing ability to use [FluentValidation](https://fluentvalidation.net/) library for creating sophisticated validators not possible with `DataAnnotations`

#### Example
Suppose there is config class:

```csharp
public class MyConfiguration
{
    [Required]
    public string Name { get; set; }

    [Range(0, 10)]
    public int Value { get; set; }
}
```

and validator:

```csharp
using FluentValidation;

public class MyConfigurationValidator : AbstractValidator<MyConfiguration>
{
    public MyConfigurationValidator()
    {
        RuleFor(x => x.Name).Length(2, 5);
        RuleFor(x => x.Value).Must(x => x % 2 == 0);
    }
}
  ```
The config class is registered like this:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddConfig<MyConfiguration, MyConfigurationValidator>(Configuration, "ConfigSectionName");
}
```

Now the config values are validated based on `DataAnnotations` attributes and on rules in `MyConfigurationValidator`

## Api
#### AddConfig
```csharp
public static IServiceCollection AddConfig<TConfig, TValidator>(this IServiceCollection services, IConfiguration configuration, string sectionName)
    where TConfig : class, new()
    where TValidator : AbstractValidator<TConfig>
```
Adds IOptions\<T> to IoC container, validates config before `Startup.Configure` is called using `DataAnnotations` attributes and `TValidator`

#### GetConfig
```csharp
public static TConfig GetConfig<TConfig, TValidator>(this IConfiguration configuration, string sectionName)
    where TConfig : class, new()
    where TValidator : AbstractValidator<TConfig>, new()
```
Gets config of type `T` from configuration, validates and returns it.
Use this method when you don't need to add `IOptions` and you want to get validated config inside `ConfigureServices` method and use right away