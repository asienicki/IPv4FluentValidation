using FluentValidation;
using FluentValidation.Validators;
using IPv4.FluentValidation.Enums;
using System.Linq;

namespace IPv4.FluentValidation.PropertyValidators
{
    public class OctetNumberValidator<T> : PropertyValidator<T, string>
    {
        private const int IP_V4_OCTETS_COUNT = 4;
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

                if (!result)
                {
                    return false;
                }
            }

            return octets.Length == IP_V4_OCTETS_COUNT && octets.All(x => !string.IsNullOrWhiteSpace(x));
        }

        public override string Name => $"{IPv4ErrorCode.OctetNumberValidator}";

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "{PropertyName} must have all octets as numbers.";
    }
}