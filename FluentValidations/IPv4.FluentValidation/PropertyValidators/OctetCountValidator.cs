using FluentValidation;
using FluentValidation.Validators;
using IPv4.FluentValidation.Enums;
using System.Linq;

namespace IPv4.FluentValidation.PropertyValidators
{
    public class OctetCountValidator<T> : PropertyValidator<T, string>
    {
        private const int IP_V4_OCTETS_COUNT = 4;

        public override bool IsValid(ValidationContext<T> context, string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            var octects = ipAddress.Split('.');

            return octects.Length == IP_V4_OCTETS_COUNT && octects.All(x => !string.IsNullOrWhiteSpace(x));
        }

        public override string Name => $"{IPv4ErrorCode.OctetCountValidator}";

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "{PropertyName} must contain exacly 4 octets.";
    }
}