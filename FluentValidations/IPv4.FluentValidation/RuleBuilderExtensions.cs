using FluentValidation;
using IPv4.FluentValidation.PropertyValidators;

namespace IPv4.FluentValidation
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValidIPv4Address<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new OctetCountValidator<T>())
                .SetValidator(new OctetNumberValidator<T>())
                .SetValidator(new OctetRangeValidator<T>());
        }

    }
}