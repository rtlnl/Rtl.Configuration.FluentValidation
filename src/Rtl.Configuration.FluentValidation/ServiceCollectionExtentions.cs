using Rtl.Configuration.Validation;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rtl.Configuration.FluentValidation
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddConfig<TConfig, TValidator>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where TConfig : class, new()
            where TValidator : AbstractValidator<TConfig>
        {
            services.AddConfig<TConfig>(configuration, sectionName);

            services.AddTransient<TValidator>();
            services.AddTransient<IOptionsValidator, OptionsValidatationDelegator<TConfig, TValidator>>();

            return services;
        }
    }
}
