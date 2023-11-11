namespace FluentValidation.Tests.IPv4Tests
{
    using FluentValidation.TestHelper;
    using FluentValidation.Tests.Models;
    using IPv4.FluentValidation.Enums;
    using System.ComponentModel.DataAnnotations;

    public class MustBeValidIPv4AddressOctetRangeTests
    {
        private SomeDataValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new SomeDataValidator();
        }

        [Test]
        public void Should_have_error_when_ip_v4_have_octet_exceeding_values()
        {
            //Arrange
            var bogus = new Bogus.Faker<SomeData>();
            bogus.CustomInstantiator(faker => new SomeData
            {
                IPv4Address = $"256.0.-128.-1",
                Address = faker.Address.StreetAddress(),
                Country = faker.Address.Country()
            });

            var model = bogus.Generate();

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(person => person.IPv4Address)
                .WithErrorCode($"{IPv4ErrorCode.OctetRangeValidator}");
        }

        [Test]
        public void Should_not_have_error_when_ip_v4_have_octet_values_in_correct_range()
        {
            //Arrange
            var bogus = new Bogus.Faker<SomeData>();
            bogus.CustomInstantiator(faker => new SomeData
            {
                IPv4Address = $"255.0.128.1",
                Address = faker.Address.StreetAddress(),
                Country = faker.Address.Country()
            });

            var model = bogus.Generate();

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(person => person.IPv4Address);
        }

        [Test]
        public void Should_not_have_error_when_ip_v4_have_octet_values_in_correct_range_even_if_had_to_much_octets()
        {
            //Arrange
            var bogus = new Bogus.Faker<SomeData>();
            bogus.CustomInstantiator(faker => new SomeData
            {
                IPv4Address = $"255.0.128.1.1",
                Address = faker.Address.StreetAddress(),
                Country = faker.Address.Country()
            });

            var model = bogus.Generate();

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(person => person.IPv4Address)
                    .WithErrorCode($"{IPv4ErrorCode.OctetCountValidator}")
                    .Only();
        }

    }
}