namespace FluentValidation.Tests.Models
{
    using FluentValidation;
    using IPv4.FluentValidation;

    public class SomeDataWithoutCascadeValidator : AbstractValidator<SomeDataWithoutCascade>
    {
        public SomeDataWithoutCascadeValidator()
        {
            RuleFor(x => x.IPv4Address).MustBeValidIPv4Address().WithName("IPv4Address");
        }
    }
}