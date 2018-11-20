using Rtl.Configuration.Validation;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Rtl.Configuration.FluentValidation
{
    class OptionsValidationDelegator<TConfig, TValidator> : IOptionsValidator
        where TConfig : class, new()
        where TValidator : AbstractValidator<TConfig>
    {
        private readonly TValidator _validator;
        private readonly TConfig _options;

        public OptionsValidationDelegator(TValidator validator, IOptions<TConfig> options)
        {
            _validator = validator;
            _options = options.Value;
        }

        public void Validate()
        {
            _validator.ValidateAndThrow(_options);
        }
    }
}
