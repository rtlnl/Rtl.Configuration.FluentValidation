using FluentValidation;
using Microsoft.Extensions.Configuration;
using Rtl.Configuration.Validation;

namespace Rtl.Configuration.FluentValidation
{
    public static class ConfigurationExtensions
    {
        public static TConfig GetConfig<TConfig, TValidator>(this IConfiguration configuration, string sectionName)
            where TConfig : class, new()
            where TValidator : AbstractValidator<TConfig>, new()
        {
            var config = configuration.GetConfig<TConfig>(sectionName);

            var validator = new TValidator();
            validator.ValidateAndThrow(config);

            return config;
        }
    }
}
