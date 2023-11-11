namespace FluentValidation.Tests.IPv4Tests
{
    using FluentValidation.TestHelper;
    using FluentValidation.Tests.Models;
    using IPv4.FluentValidation.Enums;

    public class SomeDataWithoutCascadeValidatorTests
    {
        private SomeDataWithoutCascadeValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new SomeDataWithoutCascadeValidator();
        }

        [Test]
        public void Should_Have_All_Possible_Errors()
        {
            //Arrange
            var bogus = new Bogus.Faker<SomeDataWithoutCascade>();
            bogus.CustomInstantiator(faker => new SomeDataWithoutCascade
            {
                IPv4Address = $"255.0.x128.1.-1",
                Address = faker.Address.StreetAddress(),
                Country = faker.Address.Country()
            });

            var model = bogus.Generate();

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(person => person.IPv4Address)
                    .WithErrorCode($"{IPv4ErrorCode.OctetCountValidator}");

            result.ShouldHaveValidationErrorFor(person => person.IPv4Address)
                    .WithErrorCode($"{IPv4ErrorCode.OctetRangeValidator}");

            result.ShouldHaveValidationErrorFor(person => person.IPv4Address)
                    .WithErrorCode($"{IPv4ErrorCode.OctetNumberValidator}");
        }
    }
}