using FluentValidation;
using FluentValidation.Validators;
using IPv4.FluentValidation.Enums;

namespace IPv4.FluentValidation.PropertyValidators
{
    public class OctetRangeValidator<T> : PropertyValidator<T, string>
    {
        public override bool IsValid(ValidationContext<T> context, string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            var octets = ipAddress.Split('.');

            foreach (var octet in octets)
            {
                var result = int.TryParse(octet, out var intOctetValue);

                if (!result || intOctetValue < 0 || intOctetValue >= 256)
                {
                    return false;
                }
            }

            return true;
        }

        public override string Name => $"{IPv4ErrorCode.OctetRangeValidator}";

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "{PropertyName} must have all octets in range between 0 and 255.";
    }
}