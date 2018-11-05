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