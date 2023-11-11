namespace FluentValidation.Tests.Models
{
    using FluentValidation;
    using IPv4.FluentValidation;

public class SomeDataValidator : AbstractValidator<SomeData>
{
    public SomeDataValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.IPv4Address).MustBeValidIPv4Address().WithName("IPv4Address");
    }
}
}