using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rtl.Configuration.Validation;
using System.Linq;

namespace Rtl.Configuration.FluentValidation
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddConfig<TConfig, TValidator>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where TConfig : class, new()
            where TValidator : AbstractValidator<TConfig>
        {
            if (services.Any(x => x.ImplementationType == typeof(OptionsValidationDelegator<TConfig, TValidator>)))
            {
                return services;
            }

            services.AddConfig<TConfig>(configuration, sectionName);

            services.AddTransient<TValidator>();
            services.AddTransient<IOptionsValidator, OptionsValidationDelegator<TConfig, TValidator>>();

            return services;
        }
    }
}
