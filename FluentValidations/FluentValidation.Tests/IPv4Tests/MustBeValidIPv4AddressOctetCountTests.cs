namespace FluentValidation.Tests.IPv4Tests
{
    using FluentValidation.TestHelper;
    using FluentValidation.Tests.Models;
    using IPv4.FluentValidation.Enums;

    public class MustBeValidIPv4AddressOctetCountTests
    {
        private SomeDataValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new SomeDataValidator();
        }

        [Test]
        public void Should_have_error_when_ip_v4_have_too_much_octet()
        {
            //Arrange
            var bogus = new Bogus.Faker<SomeData>();
            bogus.CustomInstantiator(faker => new SomeData
            {
                IPv4Address = $"{faker.Internet.IpAddress()}.123",
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

        [Test]
        public void Should_have_error_when_ip_v4_have_not_enought_octet()
        {
            //Arrange
            var bogus = new Bogus.Faker<SomeData>();
            bogus.CustomInstantiator(faker => new SomeData
            {
                IPv4Address = "192.168.1",
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

        [Test]
        public void Should_have_error_when_ip_v4_have_not_enought_octet_dot_case()
        {
            //Arrange
            var bogus = new Bogus.Faker<SomeData>();
            bogus.CustomInstantiator(faker => new SomeData
            {
                IPv4Address = "192.168.1.",
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

        [Test]
        public void Should_have_error_when_ip_v4_have_not_enought_octet_empty_case()
        {
            //Arrange
            var bogus = new Bogus.Faker<SomeData>();
            bogus.CustomInstantiator(faker => new SomeData
            {
                IPv4Address = "...192",
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