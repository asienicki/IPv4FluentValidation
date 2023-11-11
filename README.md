## IPv4 FluentValidation

Sample usage:
```csharp
public class SomeDataValidator : AbstractValidator<SomeData>
{
    public SomeDataValidator()
    {
        RuleFor(x => x.IPv4Address).MustBeValidIPv4Address().WithName("IPv4Address");
    }
}
```

Extension method **MustBeValidIPv4Address** provides the ability to validate the IP address.
For given string property, checks whether the IP address:
- contains the correct number of octets
- octet values are numbers
- octet values in the range 0-255